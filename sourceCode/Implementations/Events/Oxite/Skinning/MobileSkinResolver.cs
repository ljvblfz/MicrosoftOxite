//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Oxite.Infrastructure;
using Oxite.Extensions;
using System.Web;

namespace Oxite.Skinning
{
    public class MobileSkinResolver : ISkinResolver
    {
        #region ISkinResolver Members

        private AppSettingsHelper appsettings;

        public MobileSkinResolver(AppSettingsHelper appSettings)
        {
            appsettings = appSettings;
        }


        public void Resolve(SkinResolverContext context, IList<string> skinPaths)
        {
            var request = context.RequestContext.HttpContext.Request;
            var browserInfo = request.Browser;
            bool isMobile = IsMobileDevice(browserInfo, request.UserAgent);
            string skin = request.Cookies.GetSelectedSkin(isMobile);

            if (skin == "Mobile")
            {
                List<string> newSkinPaths = new List<string>(skinPaths.Count);

                foreach (string skinPath in skinPaths)
                {
                    string newSkinPath = string.Format("{0}{1}/{2}", skinPath,
                                                   skinPath.EndsWith("/") ? "Devices" : "/Devices", skin);
                    if (!newSkinPaths.Contains(newSkinPath))
                        newSkinPaths.Add(newSkinPath);
                }

                foreach (string skinPath in newSkinPaths)
                {
                    if (!skinPaths.Contains(skinPath))
                        skinPaths.Add(skinPath);
                }
            }
        }

        #endregion

        #region Private Members

        private bool IsMobileDevice(HttpBrowserCapabilitiesBase browserInfo, string userAgent)
        {
            if (browserInfo.IsMobileDevice)
                return true;

            if ((userAgent.ToLower().Contains("windows phone"))
               || (userAgent.ToLower().Contains("windows mobile"))
               || (userAgent.ToLower().Contains("opera mobi"))
               || (userAgent.ToLower().Contains("ppc")))
                return true;

            return false;
        }

        #endregion
    }
}
