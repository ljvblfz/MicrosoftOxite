//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.Skinning;

namespace Oxite.BootStrappers
{
    public class RegisterSkinResolvers : IBootStrapperTask
    {
        private IUnityContainer container;

        public RegisterSkinResolvers(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            ISkinResolverRegistry skinResolverRegistry = container.Resolve<SkinResolverRegistry>();

            skinResolverRegistry.Default = container.Resolve<ISkinResolver>();

            foreach (ISkinResolver skinResolver in container.ResolveAll<ISkinResolver>())
                skinResolverRegistry.Add(skinResolver);

            container.RegisterInstance(skinResolverRegistry);
        }

        public void Cleanup(IDictionary<string, object> state)
        {
        }

        #endregion
    }
}
