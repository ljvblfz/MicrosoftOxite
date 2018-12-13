using System;
using MIXVideos.Oxite.Models;
using MIXVideos.Oxite.Repositories;
using Oxite.BackgroundServices;
using Oxite.Services;

namespace MIXVideos.Oxite.BackgroundServices
{
    public class SaveViewsBackgroundService : BackgroundServiceBase
    {
        private readonly IPostViewRepository postViewRepository;
        private readonly PostViewStore viewStore;

        public SaveViewsBackgroundService(IBackgroundServiceService backgroundServiceService, IPostViewRepository postViewRepository, PostViewStore viewStore)
            : base(backgroundServiceService)
        {
            this.postViewRepository = postViewRepository;
            this.viewStore = viewStore;

            ID = new Guid("{034DDBAA-3484-4e6d-BBA6-6E7981B28C0B}");
            Name = "Oxite View Tracking";
            Category = "Background Services";
        }

        #region Methods

        public override void Run()
        {
            PostViewStoreItem view;

            while (viewStore.DequeueView(out view))
                postViewRepository.Save(view.PostID, view.ViewType, view.Count);
        }

        protected override void OnInitializeSettings()
        {
            base.OnInitializeSettings();

            Settings["Interval"] = TimeSpan.FromMinutes(1).Ticks.ToString();
        }

        #endregion
    }
}
