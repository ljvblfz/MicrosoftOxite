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

namespace Oxite.Repositories.SqlServer
{
    public class SqlServerModuleRepository : IModuleRepository
    {
        private readonly OxiteDataContext context;

        public SqlServerModuleRepository(OxiteDataContext context)
        {
            this.context = context;
        }

        #region IModuleRepository Members

        public IQueryable<Module> GetModules(Guid siteID)
        {
            return
                from m in context.oxite_Modules
                where m.SiteID == siteID && m.Enabled
                orderby m.ModuleOrder
                select new Module(m.ModuleName, m.ModuleType, m.ModuleOrder, m.Enabled, m.IsSystem);
        }

        #endregion
    }
}
