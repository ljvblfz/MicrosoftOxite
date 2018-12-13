//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Oxite.Infrastructure.XmlRpc
{
    public class XmlRpcParameter
    {
        private readonly XElement parameterElement;
        private readonly XmlRpcValue value;
        private readonly XElement valueElement;

        public XmlRpcParameter(XElement parameterElement)
        {
            this.parameterElement = parameterElement;
            valueElement = this.parameterElement.Element("value");
            value = new XmlRpcValue(valueElement);
        }

        public string AsString()
        {
            return value.AsString();
        }

        public int? AsInt()
        {
            return value.AsInt();
        }

        public bool? AsBool()
        {
            return value.AsBool();
        }

        public double? AsDouble()
        {
            return value.AsDouble();
        }

        public DateTime? AsDateTime()
        {
            return value.AsDateTime();
        }

        public byte[] AsBytes()
        {
            return value.AsBytes();
        }

        public object[] AsArray()
        {
            return value.AsArray();
        }

        public IDictionary<string, object> AsDictionary()
        {
            return value.AsDictionary();
        }

        public Type ComputeType()
        {
            return value.ComputeType();
        }

        public object AsType(Type type)
        {
            return value.AsType(type);
        }
    }
}
