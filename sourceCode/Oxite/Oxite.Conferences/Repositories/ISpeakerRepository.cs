//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Repositories
{
    public interface ISpeakerRepository
    {
        Speaker GetSpeaker(string name);
        IPageOfItems<Speaker> GetSpeakers(PagingInfo pagingInfo, Event evnt, SpeakerFilterCriteria speakerFilterCriteria);
    }
}
