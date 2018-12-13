//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;

namespace Oxite.Infrastructure.XmlRpc
{
    public class XmlRpcControllerActionInvoker : ControllerActionInvoker
    {
        /// <summary>
        /// Gets the parameter values.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param><param name="actionDescriptor">The action descriptor.</param>
        /// <returns>
        /// The parameter values.
        /// </returns>
        protected override IDictionary<string, object> GetParameterValues(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            if (controllerContext.RouteData.DataTokens.ContainsKey("IsXmlRpc") && (bool) controllerContext.RouteData.DataTokens["IsXmlRpc"])
            {
                IList<XmlRpcParameter> parameters = controllerContext.RouteData.Values["parameters"] as IList<XmlRpcParameter>;
                IDictionary<string, object> mappedParameters = XmlRpcParameterMapper.Map(actionDescriptor.GetParameters(), parameters);

                foreach (KeyValuePair<string, object> mappedParameter in mappedParameters)
                    controllerContext.RouteData.Values.Add(mappedParameter.Key, mappedParameter.Value);
            }

            return base.GetParameterValues(controllerContext, actionDescriptor);
        }

        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            FilterInfo baseFilters = base.GetFilters(controllerContext, actionDescriptor);

            baseFilters.ExceptionFilters.Add(new XmlRpcFaultExceptionFilter());

            return baseFilters;
        }
    }
}
