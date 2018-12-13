//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Oxite.BackgroundServices;

namespace Oxite.Repositories
{
    public interface IBackgroundServiceRepository
    {
        IList<IBackgroundService> GetBackgroundServices();
        IBackgroundService GetBackgroundService(Guid backgroundServiceID);
        bool GetBackgroundServiceExists(Guid backgroundServiceID);
        void Save(IBackgroundService backgroundService);
        void Save(IBackgroundService backgroundService, NameValueCollection settings);
        NameValueCollection GetBackgroundServiceSettings(Guid backgroundServiceID);
        void SaveSetting(Guid backgroundServiceID, string name, string value);
    }
}
