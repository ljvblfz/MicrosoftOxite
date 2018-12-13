//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Oxite.Infrastructure.XmlRpc
{
    public class XmlRpcResult : ActionResult
    {
        private readonly XElement returnValueElement;

        public XmlRpcResult(object returnValue)
        {
            returnValueElement = ToTypeElement(returnValue);
        }

        private static XElement ToTypeElement(object value)
        {
            if (value is int)
                return new XElement("int", value.ToString());
            if (value is double)
                return new XElement("double", value.ToString());
            if (value is DateTime)
                return new XElement("dateTime.iso8601", ((DateTime) value).ToString("yyyyMMddTHH:mm:ss"));
            if (value is byte[])
                return new XElement("base64", Convert.ToBase64String((byte[]) value));
            if (value is bool)
                return new XElement("boolean", ((bool) value) ? "1" : "0");
            if (value is object[])
                return new XElement("array", new XElement("data", ((object[]) value).Select(o => new XElement("value", ToTypeElement(o)))));
            if (value is IDictionary<string, object>)
                return new XElement("struct", ((IDictionary<string, object>) value).Select(kvp => new XElement("member", new XElement("name", kvp.Key), new XElement("value", ToTypeElement(kvp.Value)))));

            return new XElement("string", value.ToString());
        }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from <see cref="T:System.Web.Mvc.ActionResult"/>.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            XDocument response =
                new XDocument(
                    new XElement("methodResponse",
                        new XElement("params",
                            new XElement("param",
                                new XElement("value", returnValueElement)
                                )
                            )
                        )
                    );

            context.HttpContext.Response.ContentType = "text/xml";
            response.Save(context.HttpContext.Response.Output, SaveOptions.None);
        }
    }
}
