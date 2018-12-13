// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;

namespace Oxite.Modules.Conferences.Services
{
    public interface IUserScheduleService
    {
        bool IsUserSchedulePublic(Guid userID);
        void MakeUserSchedulePublic(Guid userID);
        void MakeUserSchedulePrivate(Guid userID);
    }
}
