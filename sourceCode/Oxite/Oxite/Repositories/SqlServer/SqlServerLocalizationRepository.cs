//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;

namespace Oxite.Repositories.SqlServer
{
    public class SqlServerLocalizationRepository : ILocalizationRepository
    {
        private readonly OxiteDataContext context;

        public SqlServerLocalizationRepository(OxiteDataContext context)
        {
            this.context = context;
        }

        #region ILocalizationRepository Members

        public IEnumerable<Phrase> GetPhrases()
        {
            return context.oxite_StringResources.Select(r => new Phrase(r.StringResourceKey, r.StringResourceValue, r.Language)).ToArray();
        }

        #endregion
    }
}
