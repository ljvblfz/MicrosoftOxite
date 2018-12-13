//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;

namespace Oxite.BootStrappers
{
    public class RegisterRoutes : IBootStrapperTask
    {
        private IUnityContainer container;

        public RegisterRoutes(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            RouteCollection routes = container.Resolve<RouteCollection>();

            routes.Clear();

            foreach (IRegisterRoutes routeRegistry in container.ResolveAll<IRegisterRoutes>().Reverse())
                routeRegistry.RegisterRoutes(routes);

            container.Resolve<IRegisterRoutes>().RegisterRoutes(routes);
        }

        public void Cleanup(IDictionary<string, object> state)
        {
        }

        #endregion
    }
}
