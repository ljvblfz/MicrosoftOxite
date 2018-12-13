//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;

namespace Oxite.Skinning
{
    public class SkinResolverRegistry : ISkinResolverRegistry
    {
        private readonly IUnityContainer container;
        private readonly List<ISkinResolver> skinResolvers;

        public SkinResolverRegistry(IUnityContainer container)
        {
            this.container = container;
            this.skinResolvers = new List<ISkinResolver>(5);
        }

        #region ISkinResolverRegistry Members

        public ISkinResolver Default { get; set; }

        public void Add(ISkinResolver skinResolver)
        {
            this.skinResolvers.Add(skinResolver);
        }

        public IEnumerable<IOxiteViewEngine> GenerateViewEngines(SkinResolverContext context, string skin)
        {
            List<IOxiteViewEngine> viewEngines = new List<IOxiteViewEngine>(10);
            List<string> skinPaths = new List<string>(20);

            skinPaths.Add("~/");

            if (Default != null)
                Default.Resolve(context, skinPaths);

            foreach (ISkinResolver skinResolver in skinResolvers)
                skinResolver.Resolve(context, skinPaths);

            if (skinPaths.Count > 0)
            {
                skinPaths.Reverse();

                foreach (string skinPath in skinPaths)
                {
                    foreach (IViewEngine viewEngine in ViewEngines.Engines)
                    {
                        if (viewEngine is IOxiteViewEngine)
                        {
                            IOxiteViewEngine viewEngineInstance = (IOxiteViewEngine)container.Resolve(viewEngine.GetType());

                            viewEngineInstance.SetRootPath(EnsurePath(skinPath));

                            viewEngines.Add(viewEngineInstance);
                        }
                    }
                }
            }

            return viewEngines;
        }

        #endregion

        protected virtual string EnsurePath(string path)
        {
            if (!path.StartsWith("~/"))
            {
                if (!path.StartsWith("/"))
                    path = "/" + path;
                if (!path.StartsWith("~"))
                    path = "~" + path;
            }

            return path;
        }
    }
}
