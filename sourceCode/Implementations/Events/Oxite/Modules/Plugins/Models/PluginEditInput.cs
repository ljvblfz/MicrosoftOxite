//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Oxite.Infrastructure;

namespace Oxite.Modules.Plugins.Models
{
    public class PluginEditInput
    {
        public PluginEditInput(string virtualPath, string code, bool? enabled, NameValueCollection extendedPropertyValues)
        {
            VirtualPath = virtualPath;
            Code = code;
            Enabled = enabled;
            PropertyValues = extendedPropertyValues;
        }

        public PluginEditInput(string code, bool enabled, IEnumerable<ExtendedProperty> extendedProperties)
        {
            Code = code;
            Enabled = enabled;

            NameValueCollection extendedPropertiesCollection = new NameValueCollection(extendedProperties.Count());

            extendedProperties.ToList().ForEach(ep => extendedPropertiesCollection.Add(ep.Name, ep.Value != null ? ep.Value.ToString() : ""));

            PropertyValues = extendedPropertiesCollection;
        }

        public string VirtualPath { get; private set; }
        public string Code { get; private set; }
        public bool? Enabled { get; private set; }
        public NameValueCollection PropertyValues { get; private set; }

        public IEnumerable<ExtendedProperty> GetExtendedProperties(IEnumerable<ExtendedProperty> pluginExtendedProperties)
        {
            List<ExtendedProperty> extendedProperties = new List<ExtendedProperty>();

            if (PropertyValues == null)
                PropertyValues = new NameValueCollection();

            foreach (ExtendedProperty item in pluginExtendedProperties)
            {
                string value = PropertyValues[item.Name];

                extendedProperties.Add(new ExtendedProperty(item.Name, item.Type, !string.IsNullOrEmpty(value as string) ? parse(item.Type, value) : null));
            }

            return extendedProperties;
        }

        private static object parse(Type type, string value)
        {
            if (type == typeof(string))
                return value;

            if (type == typeof(string[]))
            {
                //TODO: (erikpo) Get the split character from meta data

                return value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (type == typeof(bool))
            {
                bool boolValue;
                return bool.TryParse(value, out boolValue) && boolValue;
            }

            if (type == typeof(byte))
            {
                byte byteValue;
                return byte.TryParse(value, out byteValue) ? byteValue : default(byte);
            }

            if (type == typeof(short))
            {
                short shortValue;
                return short.TryParse(value, out shortValue) ? shortValue : default(short);
            }
            
            if (type == typeof(int))
            {
                int intValue;
                return int.TryParse(value, out intValue) ? intValue : default(int);
            }
            
            if (type == typeof(long))
            {
                long longValue;
                return long.TryParse(value, out longValue) ? longValue : default(long);
            }
            
            if (type == typeof(float))
            {
                float floatValue;
                return float.TryParse(value, out floatValue) ? floatValue : default(float);
            }
            
            if (type == typeof(double))
            {
                double doubleValue;
                return double.TryParse(value, out doubleValue) ? doubleValue : default(double);
            }
            
            if (type == typeof(decimal))
            {
                decimal decimalValue;
                return decimal.TryParse(value, out decimalValue) ? decimalValue : default(decimal);
            }
            
            if (type == typeof(DateTime))
            {
                DateTime dateTimeValue;
                return DateTime.TryParse(value, out dateTimeValue) ? dateTimeValue : default(DateTime);
            }
            
            if (type == typeof(Guid))
            {
                try
                {
                    return new Guid(value);
                }
                catch
                {
                    return Guid.Empty;
                }
            }

            return null;
        }
    }
}
