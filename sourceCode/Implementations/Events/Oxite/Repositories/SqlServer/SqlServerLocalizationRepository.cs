//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using Oxite.Models;
using Oxite.Repositories;

namespace Oxite.Repositories.SqlServer
{
    public class SqlServerLocalizationRepository : ILocalizationRepository
    {
        private OxiteDataContext context;

        public SqlServerLocalizationRepository(OxiteDataContext context)
        {
            this.context = context;
        }

        #region ILocalizationRepository Members

        public IQueryable<Phrase> GetPhrases()
        {
            return from r in context.oxite_StringResources
                   select new Phrase()
                   {
                       Key = r.StringResourceKey,
                       Value = r.StringResourceValue,
                       Language = r.Language
                   };
        }

        #endregion
    }
}
