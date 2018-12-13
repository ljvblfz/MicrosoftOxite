//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web;
using System.Web.Routing;

namespace Oxite.Infrastructure
{
    public class SkinResolverContext
    {
        public SkinResolverContext(RequestContext requestContext, string skin)
        {
            this.RequestContext = requestContext;
            this.Skin = skin;
        }

        public RequestContext RequestContext { get; private set; }
        public string Skin { get; private set; }
    }
}
