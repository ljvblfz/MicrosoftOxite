//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;

namespace Oxite.BootStrappers
{
    public class RegisterFilters : IBootStrapperTask
    {
        private IUnityContainer container;

        public RegisterFilters(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            IFilterRegistry filterRegistry = container.Resolve<FilterRegistry>();

            filterRegistry.Clear();

            container.Resolve<IRegisterFilters>().RegisterFilters(filterRegistry);

            foreach (IRegisterFilters registerFilters in container.ResolveAll<IRegisterFilters>())
                registerFilters.RegisterFilters(filterRegistry);

            container.RegisterInstance(filterRegistry);
        }

        public void Cleanup(IDictionary<string, object> state)
        {
        }

        #endregion
    }
}
