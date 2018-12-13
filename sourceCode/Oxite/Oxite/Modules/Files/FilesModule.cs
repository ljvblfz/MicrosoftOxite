//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;

namespace Oxite.Modules.Files
{
    public class FilesModule : IOxiteModule
    {
        private readonly IUnityContainer container;

        public FilesModule(IUnityContainer container)
        {
            this.container = container;
        }

        #region IOxiteModule Members

        public void Initialize()
        {
        }

        public void Unload()
        {
        }

        public void RegisterRoutes(RouteCollection routes)
        {
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
        }

        public void RegisterWithContainer()
        {
        }

        #endregion
    }
}
