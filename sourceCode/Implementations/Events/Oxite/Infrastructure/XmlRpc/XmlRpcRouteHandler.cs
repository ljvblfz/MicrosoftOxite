//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;

namespace Oxite.Infrastructure.XmlRpc
{
    public class XmlRpcRouteHandler : IRouteHandler
    {
        #region IRouteHandler Members

        /// <summary>
        /// Provides the object that processes the request.
        /// </summary>
        /// <returns>
        /// An object that processes the request.
        /// </returns>
        /// <param name="requestContext">An object that encapsulates information about the request.</param>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            try
            {
                XDocument document = XDocument.Load(new StreamReader(requestContext.HttpContext.Request.InputStream));
                XElement methodCall = document.Element("methodCall");

                if (methodCall != null)
                {
                    requestContext.RouteData.Values["action"] = methodCall.Element("methodName").Value;

                    XElement parameters = methodCall.Element("params");

                    if (parameters != null)
                        requestContext.RouteData.Values["parameters"] = parameters.Elements("param").Select(e => new XmlRpcParameter(e)).ToList();
                }


                requestContext.RouteData.DataTokens.Add("IsXmlRpc", true);

                return new MvcHandler(requestContext);
            }
            catch
            {
                return new XmlRpcFaultHttpHandler(new XmlRpcFaultResult(0, "Error parsing request"));
            }
        }

        #endregion
    }
}
