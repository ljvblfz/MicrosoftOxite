//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Infrastructure;
using Oxite.Repositories;

namespace Oxite.Services
{
    public class ViewTrackingService : IViewTrackingService
    {
        private readonly IViewTrackingRepository repository;
        private readonly ViewTrackingStore viewStore;

        public ViewTrackingService(IViewTrackingRepository repository, ViewTrackingStore viewStore)
        {
            this.repository = repository;
            this.viewStore = viewStore;
        }

        #region IViewTrackingService Members

        //public string[] GetViewTypes()
        //{
        //    return repository.GetViewTypes();
        //}

        //public void AddView(Guid postID, string viewType)
        //{
        //    string[] viewTypes = GetViewTypes();

        //    if (viewTypes.Contains(viewType, StringComparer.OrdinalIgnoreCase))
        //        viewStore.EnqueueView(postID, viewType);
        //}

        #endregion
    }
}
