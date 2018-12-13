//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Oxite.Infrastructure.XmlRpc
{
    internal class XmlRpcValue
    {
        private readonly XElement valueElement;

        public XmlRpcValue(XElement valueElement)
        {
            this.valueElement = valueElement;
        }

        private T ParseScalar<T>(string typeElementName, Func<XElement, T> converter)
        {
            XElement typeElement = valueElement.Element(typeElementName);

            return typeElement != null ? converter(typeElement) : default(T);
        }

        public string AsString()
        {
            return ParseScalar("string", e => e.Value);
        }

        public int? AsInt()
        {
            int? asInt = ParseScalar<int?>("int", e => int.Parse(e.Value));

            return asInt ?? ParseScalar<int?>("i4", e => int.Parse(e.Value));
        }

        public bool? AsBool()
        {
            return
                ParseScalar<bool?>(
                    "boolean",
                    e =>
                    {
                        switch (e.Value)
                        {
                            case "1":
                                return true;
                            case "0":
                                return false;
                            default:
                                return null;
                        }
                    }
                    );
        }

        public double? AsDouble()
        {
            return ParseScalar("double", e => (double?) double.Parse(e.Value));
        }

        public DateTime? AsDateTime()
        {
            return ParseScalar("dateTime.iso8601", e => (DateTime?) DateTime.ParseExact(e.Value, new[] { "r", "s", "u", "yyyyMMddTHHmmss", "yyyyMMddTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal));
        }

        public byte[] AsBytes()
        {
            return ParseScalar("base64", e => Convert.FromBase64String(e.Value));
        }

        private object AsObject()
        {
            XElement typeElement = valueElement.Elements().First();

            switch (typeElement.Name.LocalName)
            {
                case "i4":
                case "int":
                    return AsInt();
                case "boolean":
                    return AsBool();
                case "string":
                    return AsString();
                case "double":
                    return AsDouble();
                case "dateTime.iso8601":
                    return AsDateTime();
                case "base64":
                    return AsBytes();
                case "array":
                    return AsArray();
                case "struct":
                    return AsDictionary();
                default:
                    return null;
            }
        }

        public object[] AsArray()
        {
            XElement typeElement = valueElement.Element("array");

            if (typeElement != null)
            {
                XElement dataElement = typeElement.Element("data");

                return dataElement.Elements().Select(e => new XmlRpcValue(e).AsObject()).ToArray();
            }

            return null;
        }

        public IDictionary<string, object> AsDictionary()
        {
            XElement typeElement = valueElement.Element("struct");

            return
                typeElement != null
                ? typeElement.Elements().ToDictionary(e => e.Element("name").Value, e => new XmlRpcValue(e.Element("value")).AsObject())
                : null;
        }

        public Type ComputeType()
        {
            return AsObject().GetType();
        }

        public object AsType(Type type)
        {
            object properType = AsObject();

            return type.IsAssignableFrom(properType.GetType()) ? properType : null;
        }
    }
}
