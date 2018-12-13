//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;

namespace Oxite.BootStrapperTasks
{
    public class LoadBackgroundServices : IBootStrapperTask
    {
        private readonly IUnityContainer container;

        public LoadBackgroundServices(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            //TODO: (erikpo) Change the following to load up background services that are registered in Oxite.config instead of just looking in modules since they can be anywhere
            IModulesLoaded modulesLoaded = container.Resolve<IModulesLoaded>();
            IBackgroundServiceRegistry backgroundServicesRegistry = this.container.Resolve<BackgroundServiceRegistry>();

            foreach (IOxiteModule module in modulesLoaded.GetModules())
            {
                IOxiteBackgroundService backgroundServices = module as IOxiteBackgroundService;

                if (backgroundServices != null)
                    backgroundServices.RegisterBackgroundServices(backgroundServicesRegistry);
            }

            container.RegisterInstance(backgroundServicesRegistry);

            foreach (IBackgroundServiceExecutor executor in backgroundServicesRegistry.GetBackgroundServices())
                executor.Start();
        }

        public void Cleanup(IDictionary<string, object> state)
        {
            IBackgroundServiceRegistry backgroundServicesRegistry = this.container.Resolve<BackgroundServiceRegistry>();

            foreach (IBackgroundServiceExecutor executor in backgroundServicesRegistry.GetBackgroundServices())
                executor.Stop();
        }

        #endregion
    }
}