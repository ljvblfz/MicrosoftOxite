//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;

namespace OxiteSite.App_Code.Modules.OxiteSite
{
    public class OxiteSiteModule : IOxiteModule
    {
        private readonly IUnityContainer container;

        public OxiteSiteModule(IUnityContainer container)
        {
            this.container = container;
        }

        #region IOxiteModule Members

        public void Initialize()
        {
            //INFO: (erikpo) Run code here to initialize the app
        }

        public void Unload()
        {
            //INFO: (erikpo) Run code here to clean up before the app shuts down
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            //INFO: (erikpo) Register routes here
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
            //INFO: (erikpo) Register routes here
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
            //INFO: (erikpo) Register filters here
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            //INFO: (erikpo) Register model binders here
        }

        public void RegisterWithContainer()
        {
            //INFO: (erikpo) Register site specific IBootStrapperTask implementations here
        }

        #endregion
    }
}
