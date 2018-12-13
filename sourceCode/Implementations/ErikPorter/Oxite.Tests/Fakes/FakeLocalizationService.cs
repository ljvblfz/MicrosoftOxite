//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Services;

namespace Oxite.Tests.Fakes
{
    public class FakeLocalizationService : ILocalizationService
    {
        public List<Phrase> Phrases { get; set; }

        public FakeLocalizationService()
        {
            this.Phrases = new List<Phrase>();
        }

        #region ILocalizationService Members

        public ICollection<Oxite.Models.Phrase> GetTranslations()
        {
            return this.Phrases;
        }

        public ICollection<Oxite.Models.Phrase> GetTranslations(string languageCode)
        {
            return this.Phrases.Where(p => p.Language == languageCode).ToList();
        }

        #endregion
    }
}
