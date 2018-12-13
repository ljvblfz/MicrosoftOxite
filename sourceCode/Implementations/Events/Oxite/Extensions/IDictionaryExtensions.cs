//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Routing;
using Oxite.Plugins.Validators;

namespace Oxite.Extensions
{
    public static class IDictionaryExtensions
    {
        public static T GetPluginValidator<T>(this IDictionary<string, object> items) where T : class, IPluginValidator
        {
            foreach (var item in items)
                if (item.Value is T)
                    return item.Value as T;

            return null;
        }

        public static T GetPluginPropertyValidator<T>(this IDictionary<string, object> items) where T : class, IPluginPropertyValidator
        {
            object validatorObject = items.ContainsKey("Validation") ? items["Validation"] : null;

            if (validatorObject == null) return null;

            T validator = validatorObject as T;

            if (validator != null) return validator;

            RouteValueDictionary validatorItems = new RouteValueDictionary(validatorObject);

            if (validatorItems.Count > 0)
            {
                validator = Activator.CreateInstance<T>();

                foreach (PropertyInfo property in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    if (validatorItems.ContainsKey(property.Name) && validatorItems[property.Name].GetType() == property.PropertyType)
                        property.SetValue(validator, validatorItems[property.Name], null);

                return validator;
            }

            return null;
        }
    }
}
