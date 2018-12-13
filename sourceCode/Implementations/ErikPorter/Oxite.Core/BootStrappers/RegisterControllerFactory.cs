//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;

namespace Oxite.BootStrappers
{
    public class RegisterControllerFactory : IBootStrapperTask
    {
        private IUnityContainer container;

        public RegisterControllerFactory(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            ControllerBuilder.Current.SetControllerFactory(container.Resolve<OxiteControllerFactory>());
        }

        public void Cleanup(IDictionary<string, object> state)
        {
        }

        #endregion
    }
}
