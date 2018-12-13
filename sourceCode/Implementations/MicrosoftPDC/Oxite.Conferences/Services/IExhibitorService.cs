// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Services
{
    public interface IExhibitorService
    {
        IPageOfItems<Exhibitor> GetExhibitors(EventAddress eventAddress, ExhibitorFilterCriteria exhibitorFilterCriteria);
        IEnumerable<Exhibitor> GetExhibitors(EventAddress eventAddress);
        Exhibitor GetExhibitor(EventAddress eventAddress, string name);
        ModelResult<Exhibitor> SaveExhibitor(EventAddress eventAddress, Exhibitor exhibitor);
        void RemoveExhibitor(EventAddress eventAddress, Exhibitor exhibitor);
    }
}