//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Conferences.Models
{
    public class EventAddress
    {
        public EventAddress(string eventName)
        {
            EventName = eventName;
        }

        public string EventName { get; private set; }
    }
}