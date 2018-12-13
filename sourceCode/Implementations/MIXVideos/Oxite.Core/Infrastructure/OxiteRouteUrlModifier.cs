//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Models;

namespace Oxite.Infrastructure
{
    public class OxiteRouteUrlModifier : IRouteModifier
    {
        private Site site;

        public OxiteRouteUrlModifier(Site site)
        {
            this.site = site;
        }

        #region IRouteUrlModifier Members

        public string ModifyUrl(string baseRouteUrl)
        {
            if (site.RouteUrlPrefix == null)
                return "oxite.aspx/" + baseRouteUrl;
            else if (site.RouteUrlPrefix != "")
                return site.RouteUrlPrefix + "/" + baseRouteUrl;
            else
                return baseRouteUrl;
        }

        #endregion
    }
}
