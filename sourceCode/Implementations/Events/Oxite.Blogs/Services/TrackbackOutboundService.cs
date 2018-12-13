//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Blogs.Repositories;

namespace Oxite.Modules.Blogs.Services
{
    public class TrackbackOutboundService : ITrackbackOutboundService
    {
        private readonly ITrackbackOutboundRepository repository;

        public TrackbackOutboundService(ITrackbackOutboundRepository repository)
        {
            this.repository = repository;
        }

        #region ITrackbackOutboundService Members

        public IEnumerable<TrackbackOutbound> GetUnsent(Guid postID)
        {
            return repository.GetUnsent(postID);
        }

        public void Save(IEnumerable<TrackbackOutbound> trackbacks)
        {
            repository.Save(trackbacks);
        }

        public void Remove(IEnumerable<TrackbackOutbound> trackbacks)
        {
            repository.Remove(trackbacks);
        }

        public IEnumerable<TrackbackOutbound> GetNext(bool executeOnAll, TimeSpan interval)
        {
            return repository.GetNext(executeOnAll, interval);
        }

        public void Save(TrackbackOutbound trackback)
        {
            repository.Save(trackback);
        }

        #endregion
    }
}
