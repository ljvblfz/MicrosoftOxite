//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Filters;
using Oxite.Infrastructure;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Routing;
using Oxite.Routing;
using OxiteSite.App_Code.Modules.OxiteSite.Controllers;
using OxiteSite.App_Code.Modules.OxiteSite.Filters;
using OxiteSite.App_Code.Modules.OxiteSite.ModelBinder;
using OxiteSite.App_Code.Modules.OxiteSite.Repositories;
using OxiteSite.App_Code.Modules.OxiteSite.Repositories.SqlServer;
using OxiteSite.App_Code.Modules.OxiteSite.Services;
using OxiteSite.App_Code.Modules.OxiteSite.Skinning;

namespace OxiteSite.App_Code.Modules.OxiteSite
{
    public class OxiteSiteModule : IOxiteModule
    {
        private readonly IUnityContainer container;

        public OxiteSiteModule(IUnityContainer container)
        {
            this.container = container;

            registerSkinResolvers(container);
        }

        private static void registerSkinResolvers(IUnityContainer container)
        {
            ISkinResolverRegistry skinResolverRegistry = container.Resolve<ISkinResolverRegistry>();
            skinResolverRegistry.Add(container.Resolve<ConferenceSkinResolver>());
        }

        #region IOxiteModule Members

        public void Initialize()
        {
            //INFO: (erikpo) Run code here to initialize the app
        }

        public void Unload()
        {
            //INFO: (erikpo) Run code here to clean up before the app shuts down
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            string[] pdc09ControllerNamespaces = new[] { "OxiteSite.App_Code.Modules.OxiteSite.Controllers" };
            string[] externalControllerNamespaces = new[] { "Oxite.Modules.Conferences.Controllers" };

            // Resolve the event by configuration
            var eventNameValue = ConfigurationResolver.GetEventName();

            // Live ID
            routes.MapRoute(
                "PDCLiveIDSignOutImage",
                "WLID/ExpireCookie.aspx",
                new { controller = "User", action = "SignOutImage" },
                null,
                new [] { "Oxite.Modules.LiveID.Controllers" }
                );

            routes.MapRoute(
                "PDCLiveIDRegister",
                "Register",
                new { controller = "User", action = "Register" },
                null,
                pdc09ControllerNamespaces
                );

            // Home
            
            // You can't use a preprocessor directive in dynamic code (App_Code)
            

            routes.MapRoute(
                "Home",
                "",
                new { controller = "Page", action = "Home", eventName = eventNameValue, pageIndex = 0, pageSize = 7, pagePath = "home" },
                null,
                pdc09ControllerNamespaces
                );

            // Maps

            routes.MapRoute(
                "Hotels",
                "Hotels",
                new { controller = "Page", action = "Hotels", pagePath = "hotels" },
                null,
                pdc09ControllerNamespaces
                );

            routes.MapRoute(
                "Partners",
                "Partners",
                new { controller = "Page", action = "Partners", pagePath = "partners" },
                null,
                pdc09ControllerNamespaces
                );


            routes.MapRoute(
                  "MapsDefault",
                  "Maps",
                  new { controller = "Page", action = "Maps", pagePath = "Maps" },
                  null,
                  pdc09ControllerNamespaces
                  );

            routes.MapRoute(
                 "Maps",
                 "Maps/{mapType}",
                 new { controller = "Page", action = "Maps", pagePath = "Maps" },
                 null,
                 pdc09ControllerNamespaces
                 );

            // Sessions

            routes.MapRoute("PDC09SessionSiteMap", "SiteMap/Sessions",
                new { controller = "ScheduleItem", action = "SiteMap", eventName = eventNameValue, scheduleItemType = "Session", pagePath = "sessions", dataFormat = "" },
                externalControllerNamespaces
                );

            routes.MapRoute(
                "AllMyPDC09Sessions",
                "Sessions/Mine/{dataFormat}",
                new { controller = "ScheduleItem", action = "ListByUser", eventName = eventNameValue, scheduleItemType = "Session", pagePath = "sessions", dataFormat = "" },
                new { dataFormat = "(|RSS|ATOM|ICS)" },
                externalControllerNamespaces
                );
            
            routes.MapRoute(
               "MyPDC09Sessions",
               "Sessions/Mine/{*scheduleItemFilterCriteria}",
               new { controller = "ScheduleItem", action = "ListByUser", eventName = eventNameValue, scheduleItemType = "Session", pagePath = "sessions" },
               new { scheduleItemFilterCriteria = new IsScheduleItemFilterCriteria() },
               externalControllerNamespaces
               );

            routes.MapRoute(
                "MyPDC09SessionsUrl",
                "Sessions/Mine/{*scheduleItemFilterCriteria}",
                new { controller = "ScheduleItem", action = "ListByUser", eventName = eventNameValue, scheduleItemType = "Session", pagePath = "sessions" },
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                externalControllerNamespaces
                );

            // Videos
            
            routes.MapRoute(
                "AllPDC09SessionsWithVideo",
                "Videos",
                new { controller = "ScheduleItem", 
                    action = "ListByEventWithVideo", 
                    eventName = eventNameValue, 
                    scheduleItemType = "Video", 
                    pagePath = "videos", dataFormat = "" },
                null,
                externalControllerNamespaces
                );

            // Tags
            

            
            routes.MapRoute(
                "AllPDC09SessionsByTag",
                "Sessions/Tags/{tagName}",
                new { controller = "ScheduleItem", action = "ListByEventAndTag", eventName = eventNameValue, scheduleItemType = "Session", pagePath = "sessions" },
                null,
                externalControllerNamespaces
                );

            routes.MapRoute(
                "Workshops",
                "Workshops",
                new { controller = "ScheduleItem", action = "ListByEventAndWorkshops", eventName = eventNameValue, scheduleItemType = "Session", pagePath = "sessions", tagName = "Workshop" },
                null,
                externalControllerNamespaces
                );


            routes.MapRoute(
                "PDC09SessionsByTag",
                "Sessions/Tags/{tagName}/{*scheduleItemFilterCriteria}",
                new { controller = "ScheduleItem", action = "ListByEventAndTag", eventName = eventNameValue, scheduleItemType = "Session", pagePath = "sessions" },
                new { scheduleItemFilterCriteria = new IsScheduleItemFilterCriteria() },
                externalControllerNamespaces
                );

            routes.MapRoute(
                "MyPDC09SessionsByTag",
                "Sessions/Tags/{tagName}",
                new { controller = "ScheduleItem", action = "ListByEventAndTagAndUser", eventName = eventNameValue, scheduleItemType = "Session", pagePath = "sessions" },
                null,
                externalControllerNamespaces
                );

            routes.MapRoute(
                "AllPDC09Sessions",
                "Sessions/{dataFormat}",
                new { controller = "ScheduleItem", action = "ListByEvent", eventName = eventNameValue, scheduleItemType = "Session", pagePath = "sessions", dataFormat = "" },
                new { dataFormat = "(|RSS|ATOM|SIGN|ICS)" },
                externalControllerNamespaces
                );

            routes.MapRoute(
                "AllPDC09SessionsByFileFormat",
                "Sessions/{dataFormat}/{fileFormat}",
                new { controller = "ScheduleItem", 
                    action = "ListByEventWithVideo",
                      eventName = eventNameValue, 
                    scheduleItemType = "Video", 
                    pagePath = "sessions", 
                    dataFormat = "", fileFormat = "WMVHigh" },
                new { dataFormat = "RSS", fileFormat="(WMVHigh|WMV|MP4|PPT)"  },
                externalControllerNamespaces
                );
            

            routes.MapRoute(
                "PDC09Sessions",
                "Sessions/{*scheduleItemFilterCriteria}",
                new { controller = "ScheduleItem", action = "ListByEvent", eventName = eventNameValue, scheduleItemType = "Session", pagePath = "sessions" },
                new { scheduleItemFilterCriteria = new IsScheduleItemFilterCriteria() },
                externalControllerNamespaces
                );

            routes.MapRoute(
                "PDC09SessionsUrl",
                "Sessions/{*scheduleItemFilterCriteria}",
                new { controller = "ScheduleItem", action = "ListByEvent", eventName = eventNameValue, scheduleItemType = "Session", pagePath = "sessions" },
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                externalControllerNamespaces
                );

            routes.MapRoute(
               "PDC09SessionCal",
               "Sessions/{scheduleItemSlug}/ICS",
               new { controller = "ScheduleItem", action = "ItemIcs", eventName = eventNameValue, scheduleItemType = "Session" },
               null,
               externalControllerNamespaces
               );
            
            routes.MapRoute(
                "PDC09Session",
                "Sessions/{scheduleItemSlug}",
                new { controller = "ScheduleItem", action = "Item", eventName = eventNameValue , scheduleItemType = "Session" },
                null,
                externalControllerNamespaces
                );
            
            // Session Management (PRG)
            routes.MapRoute(
               "PDC09AddUserSession",
               "Sessions/Add/{scheduleItemSlug}",
               new { controller = "ScheduleItem", action = "AddToUser", eventName = eventNameValue, scheduleItemType = "Session" },
               new { httpMethod = new HttpMethodConstraint("POST") },
               externalControllerNamespaces
               );
            
            routes.MapRoute(
               "PDC09RemoveUserSession",
               "Sessions/Remove/{scheduleItemSlug}",
               new { controller = "ScheduleItem", action = "RemoveFromUser", eventName = eventNameValue, scheduleItemType = "Session" },
               new { httpMethod = new HttpMethodConstraint("POST") },
               externalControllerNamespaces
               );

            routes.MapRoute(
               "MyPDC09AddUserSession",
               "Sessions/Mine/Add/{scheduleItemSlug}",
               new { controller = "ScheduleItem", action = "AddToUser", eventName = eventNameValue, scheduleItemType = "Session" },
               new { httpMethod = new HttpMethodConstraint("POST") },
               externalControllerNamespaces
               );

            routes.MapRoute(
               "MyPDC09RemoveUserSession",
               "Sessions/Mine/Remove/{scheduleItemSlug}",
               new { controller = "ScheduleItem", action = "RemoveFromUser", eventName = eventNameValue, scheduleItemType = "Session" },
               new { httpMethod = new HttpMethodConstraint("POST") },
               externalControllerNamespaces
               );

            // Session Management (AJAX)
            routes.MapRoute(
               "PDC09AddUserSessionJson",
               "Sessions/Add/{scheduleItemSlug}/Json",
               new { controller = "ScheduleItem", action = "AddToUserJson", eventName = eventNameValue, scheduleItemType = "Session" },
               externalControllerNamespaces
               );

            routes.MapRoute(
               "MyPDC09RemoveUserSessionJson",
               "Sessions/Mine/Remove/{scheduleItemSlug}/Json",
               new { controller = "ScheduleItem", action = "RemoveFromUserJson", eventName = eventNameValue, scheduleItemType = "Session" },
               externalControllerNamespaces
               );

            routes.MapRoute(
               "MyPDC09AddUserSessionJson",
               "Sessions/Mine/Add/{scheduleItemSlug}/Json",
               new { controller = "ScheduleItem", action = "AddToUserJson", eventName = eventNameValue, scheduleItemType = "Session" },
               externalControllerNamespaces
               );

            routes.MapRoute(
               "PDC09RemoveUserSessionJson",
               "Sessions/Remove/{scheduleItemSlug}/Json",
               new { controller = "ScheduleItem", action = "RemoveFromUserJson", eventName = eventNameValue, scheduleItemType = "Session" },
               externalControllerNamespaces
               );

            // Schedule Sharing (PRG)
            routes.MapRoute(
               "PDC09ToggleUserShareSchedule",
               "Schedule/Share",
               new { controller = "ScheduleItem", action = "ShareSchedule", eventName = eventNameValue, scheduleItemType = "Session" },
               externalControllerNamespaces
               );

            // Schedule Sharing (AJAX)
            routes.MapRoute(
               "PDC09ToggleUserShareScheduleJson",
               "Schedule/Share/Json",
               new { controller = "ScheduleItem", action = "ShareScheduleJson", eventName = eventNameValue, scheduleItemType = "Session" },
               externalControllerNamespaces
               );

            routes.MapRoute(
                "PDC09SessionCommentPermalink",
                "Sessions/{scheduleItemSlug}#{commentSlug}",
                null,
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                externalControllerNamespaces
                );

            routes.MapRoute(
                "RemoveScheduleItemComment",
                "Admin/Sessions/{scheduleItemSlug}/{commentSlug}/RemoveComment",
                new { controller = "Comment", action = "Remove", eventName = eventNameValue, role = "Admin", validateAntiForgeryToken = true },
                new { httpMethod = new HttpMethodConstraint("POST") },
                externalControllerNamespaces
                );

            routes.MapRoute(
                "ApproveScheduleItemComment",
                "Admin/Sessions/{scheduleItemSlug}/{commentSlug}/ApproveComment",
                new { controller = "Comment", action = "Approve", eventName = eventNameValue, role = "Admin", validateAntiForgeryToken = true },
                new { httpMethod = new HttpMethodConstraint("POST") },
                externalControllerNamespaces
                );

            routes.MapRoute(
                "PDC09ManageExhibitors",
                "Admin/Exhibitors",
                new { controller = "Exhibitor", action = "List", eventName = eventNameValue, role = "Admin", validateAntiForgeryToken = true },
                new { httpMethod = new HttpMethodConstraint("GET") },
                externalControllerNamespaces
                );

            //routes.MapRoute(
            //    "PDC09ScheduleItemSiteMap",
            //    "SiteMap/Sessions",
            //    new { controller = "ScheduleItem", action = "SiteMap", eventName = "PDC09" },
            //    null,
            //    externalControllerNamespaces
            //    );

            // Schedules

            routes.MapRoute(
                "MySchedule",
                "Schedule/Mine/{dayName}",
                new { controller = "ScheduleItem", action = "ListByDateRangeAndUser", eventName = eventNameValue, pagePath = "schedule", dayName = "Monday" },
                new { dayName = "(Monday|Tuesday|Wednesday|Thursday)" },
                pdc09ControllerNamespaces
                );
            
            routes.MapRoute(
                "Schedule",
                "Schedule/{dayName}",
                new { controller = "ScheduleItem", action = "ListByDateRange", eventName = eventNameValue, pagePath = "schedule", dayName = "Monday" },
                new { dayName = "(Monday|Tuesday|Wednesday|Thursday)" },
                pdc09ControllerNamespaces
                );

            routes.MapRoute(
                "UserSchedule",
                "Schedule/{userName}",
                new { controller = "User", action = "Schedule", eventName = eventNameValue, pageIndex = 0, pageSize = 7, pagePath = "schedule" },
                externalControllerNamespaces
                );
            
            // Speakers
            routes.MapRoute(
                "SpeakerSiteMap",
                "SiteMap/Speakers",
                new { controller = "Speaker", action = "SiteMap", eventName = eventNameValue, pagePath = "speakers" },
                null,
                externalControllerNamespaces
                );
            routes.MapRoute(
                "AllPDC09Speakers",
                "Speakers",
                new { controller = "Speaker", action = "ListByEvent", eventName = eventNameValue, pagePath = "speakers" },
                null,
                externalControllerNamespaces
                );

            routes.MapRoute(
                "PDC09Speakers",
                "Speakers/{*speakerFilterCriteria}",
                new { controller = "Speaker", action = "ListByEvent", eventName = eventNameValue, pagePath = "speakers" },
                new { speakerFilterCriteria = new IsSpeakerFilterCriteria() },
                externalControllerNamespaces
                );

            routes.MapRoute(
                "PDC09Speaker",
                "Speakers/{speakerName}",
                new { controller = "Speaker", action = "Item", eventName = eventNameValue },
                new { speakerName = new IsSpeaker()},
                externalControllerNamespaces
                );

            routes.MapRoute(
                "PDC09SpeakerRedirect",
                "Speakers/{speakerName}",
                new { controller = "Redirection", action = "Speaker"},
                null,
                externalControllerNamespaces
                );

            // Exhibitors
            routes.MapRoute(
               "PDC09Exhibitors", "Exhibitors",
               new { controller = "Exhibitor", action = "Exhibitors", eventName = eventNameValue, pagePath = "exhibitors", path = "exhibitors", pageIndex = 0, pageSize = 100, },
               null,
               externalControllerNamespaces
               );

            // Sponsors
            routes.MapRoute(
                "PDC09Sponsors", "Sponsors",
                new { controller = "Exhibitor", action = "Sponsors", eventName = eventNameValue, pagePath = "sponsors", path = "sponsors", pageIndex = 0, pageSize = 100 },
                null,
                externalControllerNamespaces
                );

            routes.MapRoute(
                "PDC09SaveExhibitors", "Admin/Exhibitors/{exhibitorSlug}",
                new { controller = "Exhibitor", action = "Save", eventName = eventNameValue, path = "exhibitors" },
                new { httpMethod = new HttpMethodConstraint("POST") },
                externalControllerNamespaces
                );

            routes.MapRoute(
                "PDC09RemoveExhibitors", "Admin/Exhibitors/{exhibitorSlug}/Remove",
                new { controller = "Exhibitor", action = "Remove", eventName = eventNameValue, path = "exhibitors" },
                //new { httpMethod = new HttpMethodConstraint("POST") },
                externalControllerNamespaces
                );
            
            // Contact Form
            routes.MapRoute(
                "Contact", "Contact",
                new { controller = "Page", action = "Contact", path = "contact" },
                null,
                pdc09ControllerNamespaces
                );

            // Admin Summary Report
            routes.MapRoute(
                 "SummaryReport",
                 "Reports/Summary",
                 new { controller = "Reports", action = "Summary", pagePath = "Reports" },
                 null,
                 pdc09ControllerNamespaces
                 );
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
            //INFO: (erikpo) Register routes here
            routes.MapRoute("ViewASPX", "View.aspx", new { controller = "Redirection", action = "ViewASPX", path = "" },
                            null);

            routes.MapRoute("Mobile", "Mobile/{*path}", new { controller = "Redirection", action = "Mobile", path = "" },
                null);

            routes.MapRoute("Agenda", "Agenda/{*path}", new { controller = "Redirection", action = "Agenda", path = "" },
                null);


            routes.MapRoute("Content", "Content/sessionview.aspx", new { controller = "Redirection", action = "Content", path = "" },
                null);

            routes.MapRoute("ShowPost", "ShowPost.aspx", new { controller = "Redirection", action = "OldPage", path = "" },
                null);


            routes.MapRoute("OldTerms", "Terms.aspx", new { controller = "Redirection", action = "OldPage", path = "/terms" },
                null);

            routes.MapRoute("OldLetterPage", "Letter.aspx", new { controller = "Redirection", action = "OldPage", path = "/" },
                null);

            routes.MapRoute("OldSponsors", "Sponsors/PartnerExpo.aspx", new { controller = "Redirection", action = "OldPage", path = "/Exhibitors" },
                null);
            routes.MapRoute("OldPartnerExpo", "PartnerExpo.aspx", new { controller = "Redirection", action = "OldPage", path = "/Exhibitors" },
                null);
            routes.MapRoute("OldSponsorLounges", "Sponsors/Lounges.aspx", new { controller = "Redirection", action = "OldPage", path = "/Exhibitors" },
                null);


            routes.MapRoute("OldPrivacy", "Privacy.aspx", new { controller = "Redirection", action = "OldPage", path = "/privacy" },
                null);

            routes.MapRoute("OldGoodsPage", "Registration/TheGoods.aspx", new { controller = "Redirection", action = "OldPage", path = "/" },
                null);
            routes.MapRoute("OldSponsorMSR", "Sponsors/MSR.aspx", new { controller = "Redirection", action = "OldPage", path = "/Sponsors" },
                null);

            routes.MapRoute("OldSponsorDefault", "Sponsors/default.aspx", new { controller = "Redirection", action = "OldPage", path = "/Sponsors" },
                null);


            routes.MapRoute("oldDownloadsPage", "content/downloads.aspx", new { controller = "Redirection", action = "OldPage", path = "/" },
                null);
            routes.MapRoute("oldSupportPage", "content/support.aspx", new { controller = "Redirection", action = "OldPage", path = "/" },
                null);

            routes.MapRoute("oldHotelsPage", "Registration/HotelInformation.aspx", new { controller = "Redirection", action = "OldPage", path = "/Hotels" },
                null);

            routes.MapRoute("downloadDetails", "downloads/details.aspx", 
                new { controller = "Redirection", action = "OldPage", path = "/" },
                null);

            routes.MapRoute("oldOpportunities", "Sponsors/Opportunities.aspx",
                new { controller = "Redirection", action = "OldPage", path = "/PartnerOpportunities" },
                null);

            routes.MapRoute("oldFindHotelPage", "FindAHotel/{*hotelName}",
                new { controller = "Redirection", action = "OldPage", path = "/Hotels" },
                null);

            routes.MapRoute("oldSharedPages", "pages/{*pageName}",
                new { controller = "Redirection", action = "OldPage", path = "/" },
                null);

            routes.MapRoute("learnPages", "en/Learn/Pages/{*learnPath}",
                new { controller = "Redirection", action = "OldPage", path = "/" },
                null);

            routes.MapRoute("oldBlog", "blogs/{*blogPath}",
                new { controller = "Redirection", action = "OldPage", path = "/" },
                null);

            routes.MapRoute("OldRSS", "rss/pdcsessionsrss.aspx", new { controller = "Redirection", action = "OldRSS", path = "" },
                null);

            routes.MapRoute("CurrentPress", "press", new { controller = "Redirection", action = "CurrentPress", path = "" },
                null);


            //The file '/blogs/developers/default.aspx' does not exist.
            //The file '/en/Learn/Pages/SuccessStories.aspx' does not exist.
            //The file '/downloads/details.aspx' does not exist.
            //The file '/FindAHotel/LosAngelesCaliforniaPlaza.aspx' does not exist.
            //The file '/pages/shared/maps.aspx' does not exist.
            //The file '/Sponsors/Opportunities.aspx' does not exist.

            //The file '/rss/pdcsessionsrss.aspx' does not exist.
            //The file '/Agenda/Preconference.aspx' does not exist.
            //The file '/Social/Bling.aspx' does not exist.
            //The file '/Agenda/Sessions.aspx' does not exist.
            //The file '/Letter.aspx' does not exist.
            //The file '/Mobile/Sessions/SessionsList.aspx' does not exist.
            //The file '/content/sessionview.aspx' does not exist.
            //The file '/Agenda/Symposia.aspx' does not exist.

        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(UserRegistrationActionFilter));

            ControllerActionFilterCriteria ajaxActionCriteria = new ControllerActionFilterCriteria();
            ajaxActionCriteria.AddMethod<ScheduleItemController>(si => si.ListByDateRange(null, null));
            ajaxActionCriteria.AddMethod<ScheduleItemController>(si => si.ListByDateRangeAndUser(null, null));
            filterRegistry.Add(new[] { ajaxActionCriteria }, typeof(AjaxActionFilter));

            ControllerActionFilterCriteria scheduleItemsTagListActionCriteria = new ControllerActionFilterCriteria();
            scheduleItemsTagListActionCriteria.AddMethod<Oxite.Modules.Conferences.Controllers.ScheduleItemController>(si => si.ListByEvent(null, null));
            scheduleItemsTagListActionCriteria.AddMethod<Oxite.Modules.Conferences.Controllers.ScheduleItemController>(si => si.ListByUser(null, null));
            scheduleItemsTagListActionCriteria.AddMethod<Oxite.Modules.Conferences.Controllers.ScheduleItemController>(si => si.ListByEventAndTag(null, null, null));
            filterRegistry.Add(new[] { scheduleItemsTagListActionCriteria }, typeof(ScheduleItemsTagListActionFilter));

            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(Last3HeadlinesActionFilter));
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(DateRangeAddress)] = new DateRangeAddressModelBinder();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<IPDC09Service, PDC09Service>();

            //TODO: (erikpo) Once there is a xml file provider, put this in an if statement based off of the site setting for which provider to use
            container
                .RegisterType<OxitePDC09DataContext>(new InjectionConstructor(new ResolvedParameter<string>("ApplicationServices")))
                .RegisterType<IRegistrationRepository, SqlServerPDC09Repository>();
        }

        #endregion
    }
}
