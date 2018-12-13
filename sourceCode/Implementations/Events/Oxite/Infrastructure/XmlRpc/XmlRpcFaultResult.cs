//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Xml.Linq;

namespace Oxite.Infrastructure.XmlRpc
{
    public class XmlRpcFaultResult : ActionResult
    {
        private readonly int faultCode;
        private readonly string faultString;

        public XmlRpcFaultResult(int faultCode, string faultString)
        {
            this.faultCode = faultCode;
            this.faultString = faultString;
        }

        public XmlRpcFaultResult()
        {
            faultCode = 0;
            faultString = "Unspecified Error";
        }

        public string FaultString
        {
            get { return faultString; }
        }

        public int FaultCode
        {
            get { return faultCode; }
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
                        new XElement("fault",
                            new XElement("value",
                                new XElement("struct",
                                    new XElement("member",
                                        new XElement("name", "faultCode"),
                                        new XElement("value",
                                            new XElement("int", FaultCode)
                                            )
                                        ),
                                        new XElement("member",
                                            new XElement("name", "faultString"),
                                            new XElement("value",
                                                new XElement("string", faultString)
                                                )
                                            )
                                    )
                                )
                            )
                        )
                    );

            response.Save(context.HttpContext.Response.Output);
        }
    }
}
