//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Specialized;
using Oxite.Infrastructure;
using Oxite.Repositories;

namespace Oxite.BackgroundServices
{
    public class ViewTrackingSaveBackgroundService : IBackgroundService
    {
        private readonly IViewTrackingRepository viewRepository;
        private readonly ViewTrackingStore viewStore;

        public ViewTrackingSaveBackgroundService(IViewTrackingRepository viewRepository, ViewTrackingStore viewStore)
        {
            this.viewRepository = viewRepository;
            this.viewStore = viewStore;
        }

        #region IBackgroundService Members

        public void Run(NameValueCollection settings)
        {
            //ViewStoreItem view;

            //while (viewStore.DequeueView(out view))
            //    viewRepository.Save(view.PostID, view.ViewType, view.Count);
        }

        #endregion
    }
}
