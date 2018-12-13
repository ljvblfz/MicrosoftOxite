//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Conferences.Models
{
    public class SpeakerAddress
    {
        public SpeakerAddress(string speakerName)
        {
            SpeakerName = speakerName;
        }

        public string SpeakerName { get; private set; }

        public EventAddress ToEventAddress()
        {
            return new EventAddress(SpeakerName);
        }
    }
}