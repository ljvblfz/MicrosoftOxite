//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Repositories;
using Oxite.Infrastructure;

namespace Oxite.Services
{
    public class PluginService : IPluginService
    {
        private readonly IPluginRepository repository;

        public PluginService(IPluginRepository repository)
        {
            this.repository = repository;
        }

        #region IPluginService Members

        public IList<IPlugin> GetPlugins()
        {
            return repository.GetPlugins();
        }

        public IList<IPlugin> GetPluginsNotInstalled()
        {
            return repository.GetPluginsNotInstalled();
        }

        public IPlugin GetPlugin(Guid id)
        {
            return repository.GetPlugin(id);
        }

        public void Save(IPlugin plugin)
        {
            repository.Save(plugin);
        }

        #endregion
    }
}
