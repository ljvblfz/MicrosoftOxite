////  --------------------------------
////  Copyright (c) Microsoft Corporation. All rights reserved.
////  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
////  http://www.codeplex.com/oxite/license
////  ---------------------------------
//using System;
//using System.Collections.Generic;
//using Oxite.Extensions;
//using Oxite.Infrastructure;
//using Oxite.Skinning;

//namespace OxiteSite.App_Code.Modules.OxiteSite.Skinning
//{
//    public class ConferenceSkinResolver : ISkinResolver
//    {
//        private readonly OxiteContext context;

//        public ConferenceSkinResolver(OxiteContext context)
//        {
//            this.context = context;
//        }

//        #region ISkinResolver Members

//        public void Resolve(SkinResolverContext skinResolverContext, IList<string> skinPaths)
//        {
//            // Support mobile skin resolution at conference level
//            var request = context.RequestContext.HttpContext.Request;
//            var browserInfo = request.Browser;

//            // Inform downstream resolvers (user agent changes)
//            // bool isMobile = MobileSkinResolver.IsMobileDevice(browserInfo, request.UserAgent);
//            var isMobile = context.RequestContext.HttpContext.Items["IsMobile"] != null
//                               ? Convert.ToBoolean(context.RequestContext.HttpContext.Items["IsMobile"])
//                               : false;

//            if(isMobile)
//            {
//                // We want to favor the selected skin over the conferences default layer; 
//                // skin paths are in reverse lookup order
//                skinPaths.Insert(1, "~/Skins/Conferences/Devices/Mobile");
//            }
//            else
//            {
//                skinPaths.Insert(1, "~/Skins/Conferences");
//            }
//        }

//        #endregion
//    }
//}