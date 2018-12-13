//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;

namespace Oxite.Repositories
{
    public interface ITrackbackOutboundRepository
    {
        IEnumerable<TrackbackOutbound> GetUnsent(Guid postID);
        IEnumerable<TrackbackOutbound> GetNext(bool executeOnAll, TimeSpan interval);
        void Save(TrackbackOutbound trackback);
        void Save(IEnumerable<TrackbackOutbound> trackbacks);
        void Remove(IEnumerable<TrackbackOutbound> trackbacks);
    }
}
