//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.Services;
using System.Collections.Generic;
using Oxite.Models;

namespace Oxite.BootStrappers
{
    public class RegisterPlugins : IBootStrapperTask
    {
        private IUnityContainer container;

        public RegisterPlugins(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            IPluginRegistry pluginRegistry = container.Resolve<IPluginRegistry>();
            IPluginService pluginService = container.Resolve<IPluginService>();

            pluginRegistry.Clear();

            foreach (IPlugin plugin in pluginService.GetPlugins())
            {
                Assembly assembly = Assembly.LoadFrom(string.Format(@"{0}\{1}\{2}\bin\{2}.dll", container.Resolve<string>("PluginsFolder"), plugin.Category, plugin.Name));
                Type programType = assembly.GetExportedTypes().Where(t => t.Name == "Program").FirstOrDefault();

                if (programType != null)
                {
                    container.RegisterType(programType);

                    object program = container.Resolve(programType);

                    MethodInfo registerMethod = program.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(m => m.Name == "Register").FirstOrDefault();

                    if (registerMethod != null)
                    {
                        ParameterInfo[] parameters = registerMethod.GetParameters();

                        if (parameters.Length == 1 && parameters[0].ParameterType == typeof(IPluginContext))
                        {
                            IPluginContext context = new PluginContext(plugin, container);

                            registerMethod.Invoke(program, new object[] { context });

                            context.Merge(plugin);

                            pluginService.Save(plugin);

                            pluginRegistry.Add(plugin);
                        }
                    }
                }
            }

            container.RegisterInstance(pluginRegistry);
        }

        public void Cleanup(IDictionary<string, object> state)
        {
        }

        #endregion
    }
}
