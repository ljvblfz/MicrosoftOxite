//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Models;
using Oxite.Repositories;

namespace Oxite.Repositories.SqlServer
{
    public class SqlServerLanguageRepository : ILanguageRepository
    {
        private OxiteDataContext context;

        public SqlServerLanguageRepository(OxiteDataContext context)
        {
            this.context = context;
        }

        #region ILanguageRepository Members

        public Language GetLanguage(string name)
        {
            return (
                from l in context.oxite_Languages
                where l.LanguageName == name
                select new Language(l.LanguageID)
                {
                    Name = l.LanguageName,
                    DisplayName = l.LanguageDisplayName
                }
                ).FirstOrDefault();
        }

        public void Save(Language language)
        {
            oxite_Language dbLanguage = null;
            Guid languageID = language.ID;

            if (languageID != Guid.Empty)
            {
                dbLanguage = (from l in context.oxite_Languages where l.LanguageID == languageID select l).FirstOrDefault();
            }

            if (dbLanguage == null)
            {
                if (languageID == Guid.Empty)
                {
                    languageID = Guid.NewGuid();
                }

                dbLanguage = new oxite_Language();
                dbLanguage.LanguageID = languageID;

                context.oxite_Languages.InsertOnSubmit(dbLanguage);
            }

            dbLanguage.LanguageName = language.Name;
            dbLanguage.LanguageDisplayName = language.DisplayName;

            context.SubmitChanges();
        }

        #endregion
    }
}
