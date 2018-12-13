//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Models;
using Oxite.Services;

namespace Oxite.Infrastructure
{
    public class OxiteRouteUrlModifier : IRouteModifier
    {
        private readonly OxiteContext context;

        public OxiteRouteUrlModifier(OxiteContext context)
        {
            this.context = context;
        }

        #region IRouteUrlModifier Members

        public string ModifyUrl(string baseRouteUrl)
        {
            if (context.Site.RouteUrlPrefix == null)
                return "oxite.aspx/" + baseRouteUrl;
            else if (context.Site.RouteUrlPrefix != "")
                return context.Site.RouteUrlPrefix + "/" + baseRouteUrl;
            else
                return baseRouteUrl;
        }

        #endregion
    }
}
