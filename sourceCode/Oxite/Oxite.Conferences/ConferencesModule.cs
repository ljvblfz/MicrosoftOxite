//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Configuration;
using Oxite.Filters;
using Oxite.Infrastructure;
using Oxite.Modules.Conferences.Controllers;
using Oxite.Modules.Conferences.ModelBinders;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Repositories;
using Oxite.Modules.Conferences.Repositories.SqlServer;
using Oxite.Modules.Conferences.Services;

namespace Oxite.Modules.Conferences
{
    public class ConferencesModule : IOxiteModule, IOxiteDataProvider
    {
        private readonly IUnityContainer container;

        public ConferencesModule(IUnityContainer container)
        {
            this.container = container;
        }

        #region IOxiteModule Members

        public void Initialize()
        {
        }

        public void Unload()
        {
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            string[] controllerNamespaces = new [] { "Oxite.Modules.Conferences.Controllers" };

            //todo: (nheskew) hook up an event constraint

            // Schedule Items

            //routes.MapRoute(
            //    "AllSessions",
            //    "{eventName}/Sessions",
            //    new { controller = "ScheduleItem", action = "ListByEvent" },
            //    null,
            //    controllerNamespaces
            //    );

            //routes.MapRoute(
            //    "Sessions",
            //    "{eventName}/Sessions/{*scheduleItemFilterCriteria}",
            //    new { controller = "ScheduleItem", action = "ListByEvent" },
            //    new { scheduleItemFilterCriteria = new IsScheduleItemFilterCriteria() },
            //    controllerNamespaces
            //    );

            //routes.MapRoute(
            //    "Session",
            //    "{eventName}/Sessions/{scheduleItemSlug}",
            //    new { controller = "ScheduleItem", action = "Item" },
            //    null,
            //    controllerNamespaces
            //    );

            // Speakers

            //routes.MapRoute(
            //    "AllSpeakers",
            //    "{eventName}/Speakers",
            //    new { controller = "Speaker", action = "ListByEvent" },
            //    null,
            //    controllerNamespaces
            //    );

            //routes.MapRoute(
            //    "Speakers",
            //    "{eventName}/Speakers/{*speakerFilterCriteria}",
            //    new { controller = "Speaker", action = "ListByEvent" },
            //    new { speakerFilterCriteria = new IsSpeakerFilterCriteria() },
            //    controllerNamespaces
            //    );

            //routes.MapRoute(
            //    "Speaker",
            //    "{eventName}/Speakers/{speakerName}",
            //    new { controller = "Speaker", action = "Item" },
            //    null,
            //    controllerNamespaces
            //    );

            routes.MapRoute(
                "ScheduleItems",
                "Sessions/{dataFormat}"/*"{eventName}/Sessions/{dataFormat}"*/,
                new { controller = "ScheduleItem", action = "List", dataFormat = "" },
                new { /*eventName = new EventConstraint(container), */dataFormat = "(|RSS|ATOM)" },
                controllerNamespaces
                );

            routes.MapRoute(
                "AddCommentToScheduleItem",
                "Sessions/{scheduleItemSlug}"/*"{eventName}/Sessions/{scheduleItemSlug}"*/,
                new { controller = "ScheduleItem", action = "Item" },
                new { /*eventName = new EventConstraint(container), */httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            routes.MapRoute(
                "RemoveScheduleItemComment",
                "Admin/Sessions/{scheduleItemSlug}/{commentSlug}/RemoveComment"/*"Admin/{eventName}/Sessions/{scheduleItemSlug}/{commentSlug}/RemoveComment"*/,
                new { controller = "Comment", action = "Remove", role = "Admin", validateAntiForgeryToken = true },
                new { /*eventName = new EventConstraint(container), */httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            routes.MapRoute(
                "ApproveScheduleItemComment",
                "Admin/Sessions/{scheduleItemSlug}/{commentSlug}/ApproveComment"/*"Admin/{eventName}/Sessions/{scheduleItemSlug}/{commentSlug}/ApproveComment"*/,
                new { controller = "Comment", action = "Approve", role = "Admin", validateAntiForgeryToken = true },
                new { /*eventName = new EventConstraint(container), */httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
            ControllerActionFilterCriteria ajaxActionCriteria = new ControllerActionFilterCriteria();
            ajaxActionCriteria.AddMethod<ScheduleItemController>(si => si.List(null));
            ajaxActionCriteria.AddMethod<ScheduleItemController>(si => si.ListByEvent(null, null));
            ajaxActionCriteria.AddMethod<ScheduleItemController>(si => si.ListByEventAndTag(null, null, null));
            filterRegistry.Add(new[] { ajaxActionCriteria }, typeof(AjaxActionFilter));
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(ScheduleItemFilterCriteria)] = new ScheduleItemFilterCriteriaModelBinder();
            modelBinders[typeof(SpeakerFilterCriteria)] = new SpeakerFilterCriteriaModelBinder();
            modelBinders[typeof(Event)] = container.Resolve<EventModelBinder>();
            modelBinders[typeof(ScheduleItem)] = container.Resolve<ScheduleItemModelBinder>();
            modelBinders[typeof(Speaker)] = container.Resolve<SpeakerModelBinder>();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<IEventService, EventService>()
                .RegisterType<IScheduleItemService, ScheduleItemService>()
                .RegisterType<ISpeakerService, SpeakerService>()
                .RegisterType<IConferencesCommentService, ConferencesCommentService>();
        }

        #endregion

        #region IOxiteDataProvider Members

        public void ConfigureProvider(OxiteConfigurationSection config, OxiteDataProviderConfigurationElement dataProviderConfig, IUnityContainer container)
        {
            if (dataProviderConfig.Category == "LinqToSql")
                container
                    .RegisterType<OxiteConferencesDataContext>(new InjectionConstructor(new ResolvedParameter<string>(!string.IsNullOrEmpty(dataProviderConfig.ConnectionString) ? dataProviderConfig.ConnectionString : config.Providers.DefaultConnectionString)))
                    .RegisterType<IEventRepository, SqlServerEventRepository>()
                    .RegisterType<IScheduleItemRepository, SqlServerScheduleItemRepository>()
                    .RegisterType<ISpeakerRepository, SqlServerSpeakerRepository>()
                    .RegisterType<IConferencesCommentRepository, SqlServerConferencesCommentRepository>();
        }

        #endregion
    }
}
