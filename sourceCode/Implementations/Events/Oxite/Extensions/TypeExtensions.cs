//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Oxite.Extensions
{
    public static class TypeExtensions
    {
        public static object[] BuildTypeConstructorParametersFromContainer(this Type type, Func<ParameterInfo, object> getParameterObject)
        {
            ConstructorInfo constructor = type.GetLargestConstructor();
            ParameterInfo[] constructorParameters = constructor.GetParameters();
            List<object> parameters = new List<object>(constructorParameters.Length);

            foreach (ParameterInfo parameter in constructorParameters)
                parameters.Add(getParameterObject(parameter));

            return parameters.ToArray();
        }

        public static ConstructorInfo GetLargestConstructor(this Type type)
        {
            return type.GetLargestConstructor(BindingFlags.Public | BindingFlags.Instance);
        }

        public static ConstructorInfo GetLargestConstructor(this Type type, BindingFlags bindings)
        {
            ConstructorInfo foundConstructor = null;

            foreach (ConstructorInfo constructor in type.GetConstructors(bindings))
                if (foundConstructor == null || constructor.GetParameters().Length > foundConstructor.GetParameters().Length)
                    foundConstructor = constructor;

            return foundConstructor;
        }

        public static XElement SerializeValue(this Type type, object value)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);

            serializer.Serialize(writer, value);

            string xml = new UTF8Encoding().GetString(((MemoryStream)writer.BaseStream).ToArray());

            xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
            xml = string.Format("<Value>{0}</Value>", xml);

            return XElement.Parse(xml);
        }

        public static object DeserializeValue(this Type type, XElement value)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            MemoryStream stream = new MemoryStream(new UTF8Encoding().GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + value.Descendants().ElementAt(0).ToString()));
            XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);

            return serializer.Deserialize(stream);
        }
    }
}
