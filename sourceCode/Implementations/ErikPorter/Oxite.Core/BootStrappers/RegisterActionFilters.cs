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
    public class RegisterActionFilters : IBootStrapperTask
    {
        private IUnityContainer container;

        public RegisterActionFilters(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            IActionFilterRegistry actionFilterRegistry = container.Resolve<ActionFilterRegistry>();

            actionFilterRegistry.Clear();

            container.Resolve<IRegisterActionFilters>().RegisterFilters(actionFilterRegistry);

            foreach (IRegisterActionFilters actionFilters in container.ResolveAll<IRegisterActionFilters>())
                actionFilters.RegisterFilters(actionFilterRegistry);

            container.RegisterInstance(actionFilterRegistry);
        }

        public void Cleanup(IDictionary<string, object> state)
        {
        }

        #endregion
    }
}
