//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Services
{
    public interface IConferencesFileService
    {
        File GetFile(ScheduleItem scheduleItem, FileAddress fileAddress);
        IEnumerable<File> GetFiles(ScheduleItem scheduleItem);
        ModelResult<File> AddFile(ScheduleItem scheduleItem, FileInput fileInput);
        ModelResult<File> AddFile(ScheduleItem scheduleItem, FileContentInput fileInput);
        ModelResult<File> EditFile(ScheduleItem scheduleItem, File fileToEdit, FileInput fileInput);
        ModelResult<File> EditFile(ScheduleItem scheduleItem, File fileToEdit, FileContentInput fileInput);
        bool RemoveFile(ScheduleItem scheduleItem, File fileToRemove);
    }
}