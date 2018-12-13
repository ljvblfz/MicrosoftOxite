//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Routing;
using Oxite.Models;
using Oxite.Plugins.Models;

namespace Oxite.Plugins
{
    /// <summary>
    /// Handles plugins based on invocation through Reflection
    /// </summary>
    public class ReflectionPluginEngine
    {
        private readonly Hashtable methods;
        private readonly Hashtable cached;

        public ReflectionPluginEngine()
        {
            methods = Hashtable.Synchronized(new Hashtable());
            cached = Hashtable.Synchronized(new Hashtable());
        }

        public TResult Execute<TResult>(IEnumerable<PluginContainer> plugins, string pluginName, string operation, object parameters)
        {
            PluginContainer plugin = plugins.FirstOrDefault(p => p.IsLoaded && p.Instance.GetType().FullName == pluginName);

            if (plugin == null) return default(TResult);

            IDictionary<string, object> parametersDictionary = getParameters(parameters, plugin);
            MethodInfo method = findMethod(plugin.Instance.GetType().GetMethods(), operation, parametersDictionary);

            if (method == null) return default(TResult);

            return (TResult)method.Invoke(plugin.Instance, alignMethodParameters(method, parametersDictionary));
        }

        public TResult ExecuteFirst<TResult>(IEnumerable<PluginContainer> plugins, string operation, object parameters)
        {
            MethodInfo method = null;
            PluginContainer foundPlugin = null;
            TResult result = default(TResult);
            IDictionary<string, object> parametersDictionary = null;

            foreach (PluginContainer plugin in plugins)
            {
                parametersDictionary = getParameters(parameters, plugin);
                method = findMethod(plugin.Instance.GetType().GetMethods(), operation, parametersDictionary);

                if (method != null)
                {
                    foundPlugin = plugin;
                    break;
                }
            }

            if (method != null)
                result = (TResult)method.Invoke(foundPlugin.Instance, alignMethodParameters(method, parametersDictionary));

            return result;
        }

        public void ExecuteAll(IEnumerable<PluginContainer> plugins, string operation, object parameters)
        {
            MethodInfo method;
            IDictionary<string, object> parametersDictionary;

            foreach (PluginContainer plugin in plugins)
            {
                parametersDictionary = getParameters(parameters, plugin);
                method = findMethod(plugin.Instance.GetType().GetMethods(), operation, parametersDictionary);

                if (method != null)
                    method.Invoke(plugin.Instance, alignMethodParameters(method, parametersDictionary));
            }
        }
        
        public T GetMethod<T>(IEnumerable<PluginContainer> plugins, string operation) where T : class
        {
            Hashtable operationMethods = methods[operation] as Hashtable;
            Hashtable operationCached = cached[operation] as Hashtable;
            if (operationMethods == null)
            {
                operationMethods = Hashtable.Synchronized(new Hashtable());
                methods.Add(operation, operationMethods);
                operationCached = Hashtable.Synchronized(new Hashtable());
                cached.Add(operation, operationCached);
            }

            Delegate processDelegate = (Delegate)operationMethods[typeof(T)];

            if (processDelegate == null)
            {
                if (operationCached[typeof(T)] == null)
                {
                    foreach (PluginContainer plugin in plugins)
                    {
                        if (plugin.IsLoaded)
                        {
                            MethodInfo processMethod = plugin.Instance.GetType().GetMethod(operation, BindingFlags.Instance | BindingFlags.Public);

                            if (processMethod == null) continue;

                            Delegate newDelegate = Delegate.CreateDelegate(typeof(T), plugin.Instance, processMethod, false);

                            if (newDelegate != null)
                                processDelegate = processDelegate != null ? Delegate.Combine(processDelegate, newDelegate) : newDelegate;
                        }
                    }

                    operationMethods[typeof(T)] = processDelegate;
                    operationCached[typeof(T)] = true;
                }
            }

            return (T)(object)processDelegate;
        }

        public T Process<T>(IEnumerable<PluginContainer> plugins, string operation, T input) where T : class
        {
            Func<T, T> methods = GetMethod<Func<T, T>>(plugins, operation);

            if (methods != null)
            {
                foreach (Func<T, T> pluginMethod in methods.GetInvocationList())
                {
                    if (input == null) return null;

                    input = pluginMethod(input);
                }
            }

            return input;
        }

        public bool AnyTrue(IEnumerable<PluginContainer> plugins, string operation, object parameters)
        {
            MethodInfo method;
            IDictionary<string, object> parametersDictionary = parameters != null
                ? new Dictionary<string, object>(new RouteValueDictionary(parameters))
                : new Dictionary<string, object>();

            foreach (PluginContainer plugin in plugins)
            {
                method = findMethod(plugin.Instance.GetType().GetMethods(), operation, parametersDictionary);

                if (method != null && (bool)method.Invoke(plugin.Instance, alignMethodParameters(method, parametersDictionary)))
                    return true;
            }

            return false;
        }

        public bool AllTrue(IEnumerable<PluginContainer> plugins, string operation, object parameters)
        {
            MethodInfo method;
            IDictionary<string, object> parametersDictionary = parameters != null
                ? new Dictionary<string, object>(new RouteValueDictionary(parameters))
                : new Dictionary<string, object>();

            foreach (PluginContainer plugin in plugins)
            {
                method = findMethod(plugin.Instance.GetType().GetMethods(), operation, parametersDictionary);

                if (method != null)
                    if (!(bool)method.Invoke(plugin.Instance, alignMethodParameters(method, parametersDictionary)))
                        return false;
            }

            return true;
        }

        private static IDictionary<string, object> getParameters(object parameters, PluginContainer pluginContainer)
        {
            IDictionary<string, object> parametersDictionary =
                parameters != null
                    ? new Dictionary<string, object>(new RouteValueDictionary(parameters))
                    : new Dictionary<string, object>();

            //TODO: (erikpo) This shouldn't be done in the plugin engine
            if (pluginContainer.Tag != null && pluginContainer.Tag is Plugin)
                parametersDictionary.Add("properties", new PluginProperties(((Plugin)pluginContainer.Tag).ExtendedProperties));

            return parametersDictionary;
        }

        private static MethodInfo findMethod(IEnumerable<MethodInfo> methods, string operation, IDictionary<string, object> parameters)
        {
            IEnumerable<MethodInfo> foundMethods = methods.Where(m => m.Name == operation);

            if (foundMethods.Count() == 0) return null;

            if (foundMethods.Count() == 1) return foundMethods.ElementAt(0);

            Dictionary<MethodInfo, int> matches = new Dictionary<MethodInfo, int>(foundMethods.Count());

            foreach (MethodInfo method in foundMethods)
            {
                int matchCount = 0;

                foreach (ParameterInfo parameter in method.GetParameters())
                {
                    IEnumerable<KeyValuePair<string, object>> matchesByType = parameters.Where(p => p.Value.GetType() == parameter.ParameterType);

                    if (matchesByType.Count() == 1 || (matchesByType.Count() > 1 && matchesByType.Where(m => string.Compare(m.Key, parameter.Name, true) == 0).Any()))
                    {
                        matchCount++;

                        break;
                    }
                }

                matches.Add(method, matchCount);
            }

            return matches.OrderByDescending(m => m.Value).First().Key;
        }

        private static object[] alignMethodParameters(MethodInfo method, IDictionary<string, object> parameters)
        {
            List<object> methodParameters = new List<object>(5);

            foreach (ParameterInfo parameter in method.GetParameters())
            {
                IEnumerable<KeyValuePair<string, object>> matchesByType = parameters.Where(p => p.Value.GetType() == parameter.ParameterType);

                if (matchesByType.Count() == 1)
                    methodParameters.Add(matchesByType.ElementAt(0).Value);
                else if (matchesByType.Count() > 0)
                    methodParameters.Add(matchesByType.Where(m => string.Compare(m.Key, parameter.Name, true) == 0).Select(m => m.Value).FirstOrDefault() ?? matchesByType.ElementAt(0).Value);
            }

            return methodParameters.ToArray();
        }
    }
}
