//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;

namespace Oxite.Infrastructure
{
    public interface IOxiteModule
    {
        void Initialize();
        void Unload();
        void RegisterRoutes(RouteCollection routes);
        void RegisterCatchAllRoutes(RouteCollection routes);
        void RegisterFilters(IFilterRegistry filterRegistry);
        void RegisterModelBinders(ModelBinderDictionary modelBinders);
        void RegisterWithContainer();
    }
}
