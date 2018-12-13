//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using MIXVideos.Oxite.Models;
using MIXVideos.Oxite.Repositories;

namespace MIXVideos.Oxite.Services
{
    public class PostViewService : IPostViewService
    {
        private readonly IPostViewRepository repository;
        private readonly PostViewStore viewStore;

        public PostViewService(IPostViewRepository repository, PostViewStore viewStore)
        {
            this.repository = repository;
            this.viewStore = viewStore;
        }

        #region IPostViewService Members

        public string[] GetViewTypes()
        {
            return repository.GetViewTypes();
        }

        public void AddView(Guid postID, string viewType)
        {
            string[] viewTypes = GetViewTypes();

            if (viewTypes.Contains(viewType, StringComparer.OrdinalIgnoreCase))
                viewStore.EnqueueView(postID, viewType);
        }

        #endregion
    }
}
