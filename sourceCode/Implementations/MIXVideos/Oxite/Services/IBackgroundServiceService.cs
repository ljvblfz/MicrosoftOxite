//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Oxite.BackgroundServices;

namespace Oxite.Services
{
    public interface IBackgroundServiceService
    {
        IList<IBackgroundService> GetBackgroundServices();
        IBackgroundService GetBackgroundService(Guid backgroundServiceID);
        NameValueCollection LoadSettings(IBackgroundService backgroundService);
        void Save(IBackgroundService backgroundService);
        void SaveSetting(IBackgroundService backgroundService, string name, string value);
    }
}
