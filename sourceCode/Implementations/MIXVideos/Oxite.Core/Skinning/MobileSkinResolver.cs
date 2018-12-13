//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Oxite.Infrastructure;

namespace Oxite.Skinning
{
    public class MobileSkinResolver : ISkinResolver
    {
        private Regex uaRegex;

        public MobileSkinResolver()
        {
            uaRegex = new Regex("(up.browser|up.link|mmp|symbian|smartphone|midp|wap|phone|windows ce|pda|mobile|mini|palm)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        #region ISkinResolver Members

        public void Resolve(SkinResolverContext context, IList<string> skinPaths)
        {
            bool isMobile = false;
            string ua = context.RequestContext.HttpContext.Request.UserAgent ?? "";

            if (!isMobile)
            {
                if (uaRegex.IsMatch(ua))
                    isMobile = true;
            }

            if (!isMobile)
            {
                string[] uaPrefixes = new string[] { "w3c ", "acs-", "alav", "alca", "amoi", "audi", "avan", "benq", "bird", "blac", "blaz", "brew", "cell", "cldc", "cmd-", "dang", "doco", "eric", "hipt", "inno", "ipaq", "java", "jigs", "kddi", "keji", "leno", "lg-c", "lg-d", "lg-g", "lge-", "maui", "maxo", "midp", "mits", "mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", "noki", "oper", "palm", "pana", "pant", "phil", "play", "port", "prox", "qwap", "sage", "sams", "sany", "sch-", "sec-", "send", "seri", "sgh-", "shar", "sie-", "siem", "smal", "smar", "sony", "sph-", "symb", "t-mo", "teli", "tim-", "tosh", "tsm-", "upg1", "upsi", "vk-v", "voda", "wap-", "wapa", "wapi", "wapp", "wapr", "webc", "winw", "winw", "xda", "xda-" };

                foreach (string uaPrefix in uaPrefixes)
                {
                    if (ua.StartsWith(uaPrefix, StringComparison.OrdinalIgnoreCase))
                    {
                        isMobile = true;

                        break;
                    }
                }

                if (isMobile)
                {
                    if (ua.StartsWith("Opera/"))
                        isMobile = false;
                }
            }

            if (isMobile)
            {
                List<string> newSkinPaths = new List<string>(skinPaths.Count);

                foreach (string skinPath in skinPaths)
                    newSkinPaths.Add(string.Format("{0}{1}/{2}", skinPath, skinPath.EndsWith("/") ? "Devices" : "/Devices", "Generic"));

                foreach (string skinPath in newSkinPaths)
                    skinPaths.Add(skinPath);
            }
        }

        #endregion
    }
}
