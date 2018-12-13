//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;

namespace Oxite.Modules.Conferences.Repositories
{
    public interface IConferencesFileRepository
    {
        File GetFile(Guid scheduleItemID, string fileUrl);
        IEnumerable<File> GetFiles(Guid scheduleItemID);
        File Save(Guid scheduleItemID, File file);
        bool Remove(Guid scheduleItemID, Guid fileID);
    }
}