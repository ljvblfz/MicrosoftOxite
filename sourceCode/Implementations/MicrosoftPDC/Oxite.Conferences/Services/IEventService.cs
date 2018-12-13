// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Services
{
    public interface IEventService
    {
        bool GetEventExists(string eventName);
        Event GetEvent(EventAddress eventAddress);
    }
}
