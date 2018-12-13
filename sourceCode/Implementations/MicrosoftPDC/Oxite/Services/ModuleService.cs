//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Repositories;
using Oxite.Services;
using Oxite.Infrastructure;

namespace Oxite.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository repository;
        private readonly OxiteContext context;

        public ModuleService(IModuleRepository repository, OxiteContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        #region IModuleService Members

        public IEnumerable<Module> GetModules()
        {
            return repository.GetModules(context.Site.ID).ToArray();
        }

        #endregion
    }
}
