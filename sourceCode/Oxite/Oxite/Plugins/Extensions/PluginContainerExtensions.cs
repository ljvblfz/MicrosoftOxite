//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Plugins.Attributes;
using Oxite.Plugins.Models;

namespace Oxite.Plugins.Extensions
{
    public static class PluginContainerExtensions
    {
        public static void Initialize(this PluginContainer pluginContainer)
        {
            pluginContainer.ExecuteMethod("Initialize");
        }

        public static void Unload(this PluginContainer pluginContainer)
        {
            pluginContainer.ExecuteMethod("Unload");
        }

        public static object ExecuteMethod(this PluginContainer pluginContainer, string methodName, params object[] parameters)
        {
            MethodInfo method = pluginContainer.GetMethod(methodName);

            if (method != null)
                return method.Invoke(pluginContainer.Instance, parameters);

            return null;
        }

        public static MethodInfo GetMethod(this PluginContainer pluginContainer, string methodName)
        {
            if (pluginContainer.IsLoaded)
                return pluginContainer.Instance.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);

            return null;
        }

        public static bool HasMethod(this PluginContainer pluginContainer, string methodName)
        {
            return pluginContainer.GetMethod(methodName) != null;
        }

        public static string GetTemplatesPath(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetPath("/templates");
        }

        public static string GetScriptsPath(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetPath("/scripts");
        }

        public static string GetStylesPath(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetPath("/styles");
        }

        public static string GetImagesPath(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetPath("/images");
        }

        public static string GetPath(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetPath(null);
        }

        public static string GetPath(this PluginContainer pluginContainer, string subPath)
        {
            string roothPath = System.IO.Path.GetDirectoryName(pluginContainer.VirtualPath).Replace("\\", "/");

            if (roothPath.EndsWith("/"))
                roothPath = roothPath.Substring(0, roothPath.Length - 1);

            if (!string.IsNullOrEmpty(subPath) && !subPath.StartsWith("/"))
                subPath = "/" + subPath;

            return roothPath + (subPath ?? "");
        }

        public static string GetImagePath(this PluginContainer pluginContainer, string path)
        {
            if (!string.IsNullOrEmpty(path) && !(path.StartsWith("http://") || path.StartsWith("https://")))
                return pluginContainer.GetImagesPath() + (!path.StartsWith("/") ? "/" : "") + path;

            return path;
        }

        public static string GetClassName(this PluginContainer pluginContainer)
        {
            if (pluginContainer.Instance != null)
                return pluginContainer.Instance.GetType().FullName;

            return "";
        }

        public static string GetVirtualPath(this PluginContainer pluginContainer)
        {
            if (!string.IsNullOrEmpty(pluginContainer.VirtualPath))
                return pluginContainer.VirtualPath;

            return "";
        }

        public static string GetFallBackDisplayName(this PluginContainer pluginContainer)
        {
            string className = pluginContainer.GetClassName();

            if (!string.IsNullOrEmpty(className))
                return className;

            string virtualPath = pluginContainer.GetVirtualPath();

            if (!string.IsNullOrEmpty(virtualPath))
                return virtualPath.GetFileName();

            return "";
        }

        public static IEnumerable<ExtendedProperty> GetPropertiesUsingDefaultValues(this PluginContainer pluginContainer)
        {
            return getProperties(pluginContainer, true);
        }

        public static IEnumerable<ExtendedProperty> GetProperties(this PluginContainer pluginContainer)
        {
            return getProperties(pluginContainer, false);
        }

        private static IEnumerable<ExtendedProperty> getProperties(PluginContainer pluginContainer, bool useDefaultValues)
        {
            List<ExtendedProperty> properties = new List<ExtendedProperty>();

            if (pluginContainer.IsLoaded)
            {
                IEnumerable<PropertyInfo> reflectedProperties = pluginContainer.GetReflectedProperties();

                foreach (PropertyInfo property in reflectedProperties)
                {
                    if (property.CanRead && property.CanWrite)
                    {
                        object value = property.GetValue(pluginContainer.Instance, null);

                        if (useDefaultValues)
                        {
                            object defaultValue = pluginContainer.GetDefaultValue(property.Name);

                            if (defaultValue != null)
                            {
                                if (defaultValue.GetType() != property.PropertyType)
                                    throw new InvalidOperationException(string.Format("DefaultValueAttribute on property {0} is invalid. Default value type '{1}' does not match property type '{2}'", property.Name, defaultValue.GetType().FullName, value.GetType().FullName));

                                value = defaultValue;
                            }
                        }

                        properties.Add(new ExtendedProperty(property.Name, property.PropertyType, useDefaultValues ? pluginContainer.GetDefaultValue(property.Name) ?? property.GetValue(pluginContainer.Instance, null) : property.GetValue(pluginContainer.Instance, null)));
                    }
                }
            }

            return properties;
        }

        public static void ApplyProperties(this PluginContainer pluginContainer, IEnumerable<ExtendedProperty> properties)
        {
            if (pluginContainer.IsLoaded)
            {
                IEnumerable<PropertyInfo> reflectedProperties = pluginContainer.GetReflectedProperties();

                foreach (ExtendedProperty item in properties)
                {
                    PropertyInfo property = reflectedProperties.FirstOrDefault(p => string.Compare(p.Name, item.Name, true) == 0);

                    if (property != null && property.CanWrite)
                        property.SetValue(pluginContainer.Instance, item.Value, null);
                }
            }
        }

        public static IEnumerable<PropertyInfo> GetReflectedProperties(this PluginContainer pluginContainer)
        {
            if (pluginContainer.IsLoaded)
            {
                IEnumerable<PropertyInfo> props = pluginContainer.Instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.Name != "Definition" && !p.Name.EndsWith("Definition"));
                List<PropertyInfo> properties = new List<PropertyInfo>(props.Count());

                foreach (PropertyInfo property in props)
                {
                    IgnoreAttribute[] ignores = (IgnoreAttribute[])property.GetCustomAttributes(typeof(IgnoreAttribute), true);

                    if (ignores == null || ignores.Length == 0 || !(bool)ignores[0].Value)
                        properties.Add(property);
                }

                return properties;
            }

            return Enumerable.Empty<PropertyInfo>();
        }

        public static T GetDefinition<T>(this PluginContainer pluginContainer, string definitionName)
        {
            IDictionary<string, object> definitions = pluginContainer.Definitions;

            return !string.IsNullOrEmpty(definitionName) && definitions.ContainsKey(definitionName) && definitions[definitionName] is T
                       ? (T)definitions[definitionName]
                       : default(T);
        }

        public static string[] GetAuthors(this PluginContainer pluginContainer)
        {
            object authorsValue = pluginContainer.GetDefinition<object>("Authors");

            if (authorsValue == null) return new string[0];

            if (authorsValue is string[]) return (string[])authorsValue;

            if (authorsValue is string) return ((string)authorsValue).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(a => a.Trim()).ToArray();
            
            return new string[0];
        }

        public static string[] GetAuthorUrls(this PluginContainer pluginContainer)
        {
            object authorUrlsValue = pluginContainer.GetDefinition<object>("AuthorUrls");

            if (authorUrlsValue == null) return null;

            if (authorUrlsValue is string[]) return (string[])authorUrlsValue;

            if (authorUrlsValue is string) return ((string)authorUrlsValue).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(au => au.Trim()).ToArray();

            return new string[0];
        }

        public static string GetBackgroundImage(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetImagePath(pluginContainer.GetDefinition<string>("BackgroundImage"));
        }

        public static string GetCategory(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetDefinition<string>("Category");
        }

        public static string GetDescription(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetDefinition<string>("Description");
        }

        public static string GetDisplayName(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetDefinition<string>("DisplayName") ?? pluginContainer.GetFallBackDisplayName();
        }

        public static DateTime? GetDateModified(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetDateModified(new HttpContextWrapper(HttpContext.Current));
        }

        public static DateTime? GetDateModified(this PluginContainer pluginContainer, HttpContextBase httpContextBase)
        {
            return pluginContainer.VirtualPath.FileModifiedDate(httpContextBase);
        }

        public static string GetIconLarge(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetImagePath(pluginContainer.GetDefinition<string>("IconLarge") ?? pluginContainer.GetDefinition<string>("IconSmall"));
        }

        public static string GetIconLargeError(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetImagePath(pluginContainer.GetDefinition<string>("IconLargeError")) ?? pluginContainer.GetIconLarge();
        }

        public static string GetIconLargeDisabled(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetImagePath(pluginContainer.GetDefinition<string>("IconLargeDisabled")) ?? pluginContainer.GetIconLarge();
        }

        public static string GetIconSmall(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetImagePath(pluginContainer.GetDefinition<string>("IconSmall") ?? pluginContainer.GetDefinition<string>("IconLarge"));
        }

        public static string GetIconSmallError(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetImagePath(pluginContainer.GetDefinition<string>("IconSmallError")) ?? pluginContainer.GetIconSmall();
        }

        public static string GetIconSmallDisabled(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetImagePath(pluginContainer.GetDefinition<string>("IconSmallDisabled")) ?? pluginContainer.GetIconSmall();
        }

        public static string[] GetTags(this PluginContainer pluginContainer)
        {
            object tagsValue = pluginContainer.GetDefinition<object>("Tags");

            if (tagsValue == null) return new string[0];

            string[] tagsList = new string[0];

            if (tagsValue is string[])
                tagsList = (string[])tagsValue;
            else if (tagsValue is string)
                tagsList = ((string)tagsValue).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

            Array.Sort(tagsList);

            return tagsList;
        }

        public static string GetHomePage(this PluginContainer pluginContainer)
        {
            return pluginContainer.GetDefinition<string>("HomePage");
        }

        public static Version GetVersion(this PluginContainer pluginContainer)
        {
            return parseVersion(pluginContainer.GetDefinition<object>("Version"));
        }

        public static Version GetOxiteMinVersion(this PluginContainer pluginContainer)
        {
            return parseVersion(pluginContainer.GetDefinition<object>("OxiteMinVersion"));
        }

        public static Version GetOxiteMaxVersion(this PluginContainer pluginContainer)
        {
            return parseVersion(pluginContainer.GetDefinition<object>("OxiteMaxVersion"));
        }

        public static T GetPropertyDefinition<T>(this PluginContainer pluginContainer, string propertyName, string attributeName)
        {
            IDictionary<string, object> propertyDefinitions = pluginContainer.GetPropertyDefinitions(propertyName);

            return !string.IsNullOrEmpty(attributeName) && propertyDefinitions.ContainsKey(attributeName) && propertyDefinitions[attributeName] is T
                       ? (T)propertyDefinitions[attributeName]
                       : default(T);
        }

        public static object GetDefaultValue(this PluginContainer pluginContainer, string propertyName)
        {
            return pluginContainer.GetPropertyDefinition<object>(propertyName, "DefaultValue");
        }

        public static PropertyGroup GetGroup(this PluginContainer pluginContainer, string propertyName)
        {
            object groupValue = pluginContainer.GetPropertyDefinition<object>(propertyName, "Group");

            if (groupValue == null) return null;

            if (groupValue is PropertyGroup) return (PropertyGroup)groupValue;

            IDictionary<string, object> values = new RouteValueDictionary(groupValue);
            object nameValue = values.ContainsKey("Name") ? values["Name"] : null;
            object orderValue = values.ContainsKey("Order") ? values["Order"] : null;

            if (nameValue != null || orderValue != null)
            {
                string name = nameValue as string;
                int order = int.MaxValue;

                if (orderValue is int)
                    order = (int)orderValue;

                return new PropertyGroup(name, order);
            }

            return null;
        }

        public static string GetHelpText(this PluginContainer pluginContainer, string propertyName)
        {
            return pluginContainer.GetPropertyDefinition<string>(propertyName, "HelpText");
        }

        public static string GetHelpUrl(this PluginContainer pluginContainer, string propertyName)
        {
            return pluginContainer.GetPropertyDefinition<string>(propertyName, "HelpUrl");
        }

        public static string GetLabelText(this PluginContainer pluginContainer, string propertyName)
        {
            return pluginContainer.GetPropertyDefinition<string>(propertyName, "LabelText");
        }

        public static PropertyAppearance GetAppearance(this PluginContainer pluginContainer, string propertyName)
        {
            object appearanceValue = pluginContainer.GetPropertyDefinition<object>(propertyName, "Appearance");

            if (appearanceValue == null) return null;

            if (appearanceValue is PropertyAppearance)
            {
                PropertyAppearance propertyAppearance = (PropertyAppearance)appearanceValue;

                propertyAppearance.Width = verifyWidth(propertyAppearance.Width);
                propertyAppearance.Height = verifyHeight(propertyAppearance.Height);

                return propertyAppearance;
            }
            
            IDictionary<string, object> values = new RouteValueDictionary(appearanceValue);
            object widthValue = values.ContainsKey("Width") ? values["Width"] : null;
            object heightValue = values.ContainsKey("Height") ? values["Height"] : null;

            if (widthValue != null || heightValue != null)
                return new PropertyAppearance() { Width = verifyWidth(widthValue), Height = verifyHeight(heightValue) };

            return null;
        }

        public static int GetOrder(this PluginContainer pluginContainer, string propertyName)
        {
            object orderValue = pluginContainer.GetPropertyDefinition<object>(propertyName, "Order");

            if (orderValue is int) return (int)orderValue;

            if (orderValue is string)
            {
                int order;

                if (int.TryParse((string)orderValue, out order)) return order;
            }

            return int.MaxValue;
        }

        public static bool GetRequired(this PluginContainer plugin, string propertyName)
        {
            bool? required = plugin.GetPropertyDefinition<bool?>(propertyName, "Required");

            if (required.HasValue)
                return required.Value;

            PropertyInfo property = plugin.GetReflectedProperties().FirstOrDefault(rp => rp.Name == propertyName);

            if (property != null)
                return !property.PropertyType.IsClass;

            return false;
        }

        #region Private Methods

        private static Version parseVersion(object value)
        {
            if (value == null) return null;

            if (value is Version) return (Version)value;

            if (value is string)
            {
                try
                {
                    return new Version((string)value);
                }
                catch
                {
                    return null;
                }
            }

            RouteValueDictionary dictionary = new RouteValueDictionary(value);
            int major = dictionary.ContainsKey("Major") ? (dictionary["Major"] as int?).GetValueOrDefault(0) : 0;
            int minor = dictionary.ContainsKey("Minor") ? (dictionary["Minor"] as int?).GetValueOrDefault(0) : 0;
            int build = dictionary.ContainsKey("Build") ? (dictionary["Build"] as int?).GetValueOrDefault(-1) : -1;
            int revision = dictionary.ContainsKey("Revision") ? (dictionary["Revision"] as int?).GetValueOrDefault(-1) : -1;

            if (major > 0 && minor > 0)
                if (build >= 0)
                    if (revision >= 0)
                        return new Version(major, minor, build, revision);
                    else
                        return new Version(major, minor, build);
                else
                    return new Version(major, minor);

            return null;
        }

        private static string verifyWidth(object widthValue)
        {
            if (widthValue == null) return null;

            if (widthValue is string)
            {
                string width = ((string)widthValue).Trim();

                if (width == "") return null;

                if (string.Compare(width, "auto", true) == 0 || string.Compare(width, "inherit", true) == 0)
                    return width.ToLower();
                
                return verifyUnit(width);
            }

            if (widthValue is double)
                return new Unit((double)widthValue).ToString();
            if (widthValue is int)

                return new Unit((int)widthValue).ToString();

            return null;
        }

        private static string verifyHeight(object heightValue)
        {
            if (heightValue == null) return null;

            if (heightValue is string)
            {
                string height = ((string)heightValue).Trim();

                if (height == "") return null;

                if (string.Compare(height, "auto", true) == 0)
                    return height.ToLower();
                
                return verifyUnit(height);
            }
            
            if (heightValue is double)
                return new Unit((double)heightValue).ToString();
            
            if (heightValue is int)
                return new Unit((int)heightValue).ToString();

            return null;
        }

        private static string verifyUnit(string potentialUnitValue)
        {
            try
            {
                Unit potentialUnit = new Unit(potentialUnitValue);

                return potentialUnit.ToString();
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
