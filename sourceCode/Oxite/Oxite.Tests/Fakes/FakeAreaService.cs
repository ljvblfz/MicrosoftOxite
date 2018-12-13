//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Tests.Fakes
{
    public class FakeAreaService : IAreaService
    {
        public Dictionary<string, Area> StoredAreas { get; set; }

        public FakeAreaService()
        {
            this.StoredAreas = new Dictionary<string, Area>();
        }

        #region IAreaService Members

        public Area GetArea(Guid id)
        {
            return this.StoredAreas.Where(kvp => kvp.Value.ID == id).FirstOrDefault().Value;
        }

        public Area GetArea(AreaAddress areaAddress)
        {
            if (this.StoredAreas.ContainsKey(areaAddress.AreaName))
                return this.StoredAreas[areaAddress.AreaName];
            else
                return null;
        }

        public IEnumerable<Area> GetAreas()
        {
            return this.StoredAreas.Select(kvp => kvp.Value).ToList();
        }

        public IEnumerable<Area> FindAreas(AreaSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public ModelResult<Area> AddArea(AreaInput areaInput)
        {
            throw new NotImplementedException();
        }

        public ModelResult<Area> EditArea(AreaAddress areaAddress, AreaInput areaInput)
        {
            throw new NotImplementedException();
        }

        public ModelResult<Area> EditArea(AreaAddress areaAddress, ImportAreaInput areaInput)
        {
            throw new NotImplementedException();
        }

        public void RemoveArea(AreaAddress areaAddress)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
