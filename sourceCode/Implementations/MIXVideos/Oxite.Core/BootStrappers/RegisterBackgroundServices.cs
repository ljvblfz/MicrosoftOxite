//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Oxite.BackgroundServices;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.BootStrappers
{
    public class RegisterBackgroundServices : IBootStrapperTask
    {
        private IUnityContainer container;

        public RegisterBackgroundServices(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            Site site = container.Resolve<Site>();

            if (site.ID != Guid.Empty)
            {
                BackgroundServicesExecutor backgroundServicesExecutor = (BackgroundServicesExecutor)(state.ContainsKey("backgroundServicesExecutor") ? state["backgroundServicesExecutor"] : null);

                if (backgroundServicesExecutor == null)
                {
                    state["backgroundServicesExecutor"] = backgroundServicesExecutor = new BackgroundServicesExecutor(container);

                    backgroundServicesExecutor.Start();
                }
            }
        }

        public void Cleanup(IDictionary<string, object> state)
        {
            BackgroundServicesExecutor backgroundServicesExecutor = (BackgroundServicesExecutor)(state.ContainsKey("backgroundServicesExecutor") ? state["backgroundServicesExecutor"] : null);

            if (backgroundServicesExecutor != null)
                backgroundServicesExecutor.Stop();
        }

        #endregion
    }
}
