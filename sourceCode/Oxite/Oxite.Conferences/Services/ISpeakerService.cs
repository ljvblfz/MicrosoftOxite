// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using Oxite.Models;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Services
{
    public interface ISpeakerService
    {
        Speaker GetSpeaker(string speakerName);
        IPageOfItems<Speaker> GetSpeakers(SpeakerFilterCriteria speakerFilterCriteria);
        IPageOfItems<Speaker> GetSpeakers(Event evnt, SpeakerFilterCriteria speakerFilterCriteria);
    }
}
