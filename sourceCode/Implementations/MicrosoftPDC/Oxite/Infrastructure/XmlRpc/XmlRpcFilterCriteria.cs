//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Infrastructure.XmlRpc
{
    public class XmlRpcFilterCriteria : IFilterCriteria
    {
        public bool Match(FilterRegistryContext context)
        {
            return context.ControllerContext.RouteData.DataTokens.ContainsKey("IsXmlRpc") && ((bool)context.ControllerContext.RouteData.DataTokens["IsXmlRpc"]);
        }
    }
}
