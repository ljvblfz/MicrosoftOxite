//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Filters;
using Oxite.Infrastructure;
using Oxite.Modules.Conferences.Controllers;
using Oxite.Modules.Conferences.Filters;
using Oxite.Modules.Conferences.ModelBinders;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Repositories;
using Oxite.Modules.Conferences.Repositories.SqlServer;
using Oxite.Modules.Conferences.Services;

namespace Oxite.Modules.Conferences
{
    public class ConferencesModule : IOxiteModule
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
                new { controller = "ScheduleItem", action = "List", dataFormat = ""},
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
                "ManageExhibitors",
                "Admin/Exhibitors",
                new { controller = "Exhibitor", action = "List", role = "Admin", validateAntiForgeryToken = true },
                new { /*eventName = new EventConstraint(container), */httpMethod = new HttpMethodConstraint("GET") },
                controllerNamespaces
                );

            routes.MapRoute(
                "AddExhibitor",
                "Admin/Exhibitors/New"/*"{eventName}/Exhibitors/{exhibitorSlug}"*/,
                new { controller = "Exhibitor", action = "Add", role = "Admin", validateAntiForgeryToken = true },
                new { /*eventName = new EventConstraint(container), */httpMethod = new HttpMethodConstraint("GET") },
                controllerNamespaces
                );

            routes.MapRoute(
                "EditExhibitor",
                "Admin/Exhibitors/{exhibitorSlug}"/*"{eventName}/Exhibitors/{exhibitorSlug}"*/,
                new { controller = "Exhibitor", action = "Edit", role = "Admin", validateAntiForgeryToken = true },
                new { /*eventName = new EventConstraint(container), */httpMethod = new HttpMethodConstraint("GET") },
                controllerNamespaces
                );

            routes.MapRoute(
                "SaveExhibitor",
                "Admin/Exhibitors/{exhibitorSlug}"/*"{eventName}/Exhibitors/{exhibitorSlug}"*/,
                new { controller = "Exhibitor", action = "Save", role = "Admin", validateAntiForgeryToken = true },
                new { /*eventName = new EventConstraint(container), */httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            routes.MapRoute(
                "RemoveExhibitor",
                "Admin/Exhibitors/{exhibitorSlug}/Remove"/*"{eventName}/Exhibitors/Remove/{exhibitorSlug}"*/,
                new { controller = "Exhibitor", action = "Remove", role = "Admin", validateAntiForgeryToken = true },
                new { /*eventName = new EventConstraint(container), */},
                controllerNamespaces
                );

            //routes.MapRoute(
            //    "RemoveScheduleItemComment",
            //    "Admin/Sessions/{scheduleItemSlug}/{commentSlug}/RemoveComment"/*"Admin/{eventName}/Sessions/{scheduleItemSlug}/{commentSlug}/RemoveComment"*/,
            //    new { controller = "Comment", action = "Remove", role = "Admin", validateAntiForgeryToken = true },
            //    new { /*eventName = new EventConstraint(container), */httpMethod = new HttpMethodConstraint("POST") },
            //    controllerNamespaces
            //    );

            //routes.MapRoute(
            //    "ApproveScheduleItemComment",
            //    "Admin/Sessions/{scheduleItemSlug}/{commentSlug}/ApproveComment"/*"Admin/{eventName}/Sessions/{scheduleItemSlug}/{commentSlug}/ApproveComment"*/,
            //    new { controller = "Comment", action = "Approve", role = "Admin", validateAntiForgeryToken = true },
            //    new { /*eventName = new EventConstraint(container), */httpMethod = new HttpMethodConstraint("POST") },
            //    controllerNamespaces
            //    );
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
            filterRegistry.Add(new[] { new DataFormatFilterCriteria("SIGN") }, typeof(SignResultActionFilter));
            filterRegistry.Add(new[] { new DataFormatFilterCriteria("ICS") }, typeof(IcsResultActionFilter));

            ControllerActionFilterCriteria ajaxActionCriteria = new ControllerActionFilterCriteria();
            ajaxActionCriteria.AddMethod<ScheduleItemController>(si => si.List(null));
            ajaxActionCriteria.AddMethod<ScheduleItemController>(si => si.ListByEvent(null, null));
            ajaxActionCriteria.AddMethod<ScheduleItemController>(si => si.ListByUser(null, null));
            ajaxActionCriteria.AddMethod<ScheduleItemController>(si => si.ListByEventAndTag(null, null, null));
            filterRegistry.Add(new[] { ajaxActionCriteria }, typeof(AjaxActionFilter));
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(ScheduleItemFilterCriteria)] = new ScheduleItemFilterCriteriaModelBinder();
            modelBinders[typeof(SpeakerFilterCriteria)] = new SpeakerFilterCriteriaModelBinder();
            modelBinders[typeof(EventAddress)] = new EventAddressModelBinder();
            modelBinders[typeof(ScheduleItemAddress)] = new ScheduleItemAddressModelBinder();
            modelBinders[typeof(ScheduleItemCommentAddress)] = new ScheduleItemCommentAddressModelBinder();
            modelBinders[typeof(SpeakerAddress)] = new SpeakerAddressModelBinder();
            modelBinders[typeof(ExhibitorInput)] = new ExhibitorInputModelBinder();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<IEventService, EventService>()
                .RegisterType<IScheduleItemService, ScheduleItemService>()
                .RegisterType<ISpeakerService, SpeakerService>()
                .RegisterType<IConferencesCommentService, ConferencesCommentService>()
                .RegisterType<IUserScheduleService, UserScheduleService>()
                .RegisterType<IExhibitorService, ExhibitorService>();

            //TODO: (erikpo) Once there is a xml file provider, put this in an if statement based off of the site setting for which provider to use
            container
                .RegisterType<OxiteConferencesDataContext>(new InjectionConstructor(new ResolvedParameter<string>("ApplicationServices")))
                .RegisterType<IEventRepository, SqlServerEventRepository>()
                .RegisterType<IScheduleItemRepository, SqlServerScheduleItemRepository>()
                .RegisterType<ISpeakerRepository, SqlServerSpeakerRepository>()
                .RegisterType<IConferencesCommentRepository, SqlServerConferencesCommentRepository>()
                .RegisterType<IUserScheduleRepository, SqlServerUserScheduleRepository>()
                .RegisterType<IExhibitorRepository, SqlServerExhibitorRepository>();
        }
        #endregion
    }
}
