//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System;

namespace Oxite.Infrastructure
{
    public interface IOxiteEvents
    {
        void Add(string eventName, Action<object> method);
        void Remove(string eventName, Action<object> method);
        IList<OxiteEvent> GetEvents();
        OxiteEvent GetEvent(string eventName);
        void FireEvent(string eventName, object state);
    }
}
