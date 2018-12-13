// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using Oxite.Models;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Repositories;
using Oxite.Services;

namespace Oxite.Modules.Conferences.Services
{
    public class SpeakerService : ISpeakerService
    {
        private readonly ISpeakerRepository repository;

        public SpeakerService(ISpeakerRepository repository)
        {
            this.repository = repository;
        }

        #region ISpeakerService Members

        public IPageOfItems<Speaker> GetSpeakers(SpeakerFilterCriteria speakerFilterCriteria)
        {
            return GetSpeakers(null, speakerFilterCriteria);
        }

        public IPageOfItems<Speaker> GetSpeakers(EventAddress eventAddress, SpeakerFilterCriteria speakerFilterCriteria)
        {
            return repository.GetSpeakers(null, speakerFilterCriteria).GetPage(speakerFilterCriteria.PageIndex, speakerFilterCriteria.PageSize);
        }

        public Speaker GetSpeaker(SpeakerAddress speakerAddress)
        {
            return repository.GetSpeaker(speakerAddress.SpeakerName);
        }

        #endregion
    }
}
