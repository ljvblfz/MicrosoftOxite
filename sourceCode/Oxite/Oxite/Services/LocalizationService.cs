//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Repositories;

namespace Oxite.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ILocalizationRepository repository;

        public LocalizationService(ILocalizationRepository repository)
        {
            this.repository = repository;
        }

        #region ILocalizationService Members

        public IEnumerable<Phrase> GetTranslations()
        {
            return repository.GetPhrases();
        }

        public IEnumerable<Phrase> GetTranslations(string languageCode)
        {
            return repository.GetPhrases().Where(p => p.Language == languageCode);
        }

        #endregion
    }
}
