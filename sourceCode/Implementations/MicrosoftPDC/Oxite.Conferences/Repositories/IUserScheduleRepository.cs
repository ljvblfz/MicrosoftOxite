// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;

namespace Oxite.Modules.Conferences.Repositories
{
    public interface IUserScheduleRepository
    {
        bool IsSchedulePublic(Guid userID);
        void MakeSchedulePublic(Guid userID);
        void MakeSchedulePrivate(Guid userID);
    }
}
