//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Repositories
{
    public interface ISpeakerRepository
    {
        Speaker GetSpeaker(string name);
        IQueryable<Speaker> GetSpeakers(EventAddress eventAddress, SpeakerFilterCriteria speakerFilterCriteria);
    }
}
