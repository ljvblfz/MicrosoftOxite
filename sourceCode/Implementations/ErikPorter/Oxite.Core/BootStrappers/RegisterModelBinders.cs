//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.BootStrappers
{
    public class RegisterModelBinders : IBootStrapperTask
    {
        private IUnityContainer container;

        public RegisterModelBinders(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            ModelBinderDictionary modelBinders = container.Resolve<ModelBinderDictionary>();

            modelBinders.Clear();

            container.Resolve<IRegisterModelBinders>().RegisterModelBinders(modelBinders);

            foreach (IRegisterModelBinders modelBinderRegistry in container.ResolveAll<IRegisterModelBinders>())
                modelBinderRegistry.RegisterModelBinders(modelBinders);
        }

        public void Cleanup(IDictionary<string, object> state)
        {
        }

        #endregion
    }
}
