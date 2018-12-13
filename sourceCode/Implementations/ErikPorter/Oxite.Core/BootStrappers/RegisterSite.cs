//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Services;

namespace Oxite.BootStrappers
{
    public class RegisterSite : IBootStrapperTask
    {
        private readonly IUnityContainer container;

        public RegisterSite(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            AppSettingsHelper appSettings = container.Resolve<AppSettingsHelper>();
            Site site = container.Resolve<ISiteService>().GetSite(appSettings.GetString("SiteName", "Oxite"));

            if (site != null && site.ID != Guid.Empty)
                container.RegisterInstance(site);
        }

        public void Cleanup(IDictionary<string, object> state)
        {
        }

        #endregion
    }
}
