// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Linq;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Modules.Conferences.Extensions;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Services;
using Oxite.Modules.Membership.Services;
using Oxite.ViewModels;

namespace Oxite.Modules.Conferences.Controllers
{
    public class ExhibitorController : Controller
    {
        private readonly IExhibitorService exhibitorService;
        private readonly IEventService eventService;
        private readonly IUserService userService;

        public ExhibitorController()
        {
            ValidateRequest = false;
        }

        public ExhibitorController(IExhibitorService exhibitorService, IEventService eventService, IUserService userService)
            : this()
        {
            this.exhibitorService = exhibitorService;
            this.eventService = eventService;
            this.userService = userService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Exhibitors(EventAddress eventAddress, int pageIndex, int pageSize)
        {
            var @event = eventService.GetEvent(eventAddress);

            var criteria = new ExhibitorFilterCriteria
            {
                Event = @event,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var exhibitors = exhibitorService.GetExhibitors(eventAddress, criteria);
            

            return View(new OxiteViewModelItems<Exhibitor>(exhibitors));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Sponsors(EventAddress eventAddress, int pageIndex, int pageSize)
        {
            var @event = eventService.GetEvent(eventAddress);

            var criteria = new ExhibitorFilterCriteria
                               {
                                   Event = @event,
                                   ParticipantLevels = new[] {"Platinum", "Gold", "Silver"},
                                   PageIndex = pageIndex,
                                   PageSize = pageSize
                               };

            var exhibitors = exhibitorService.GetExhibitors(eventAddress, criteria);
            
            return View(new OxiteViewModelItems<Exhibitor>(exhibitors));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult List(EventAddress eventAddress)
        {
            var userAuthenticated = userService.GetUser(User.Identity.Name);

            if (userAuthenticated == null || !userAuthenticated.IsInRole("Admin"))
            {
                return null;
            }

            var exhibitors = exhibitorService.GetExhibitors(eventAddress).ToList();

            if (eventAddress == null) return null;

            return View(new OxiteViewModelItems<Exhibitor>(exhibitors));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(EventAddress eventAddress, string exhibitorSlug)
        {
            var userAuthenticated = userService.GetUser(User.Identity.Name);

            if (userAuthenticated == null || !userAuthenticated.IsInRole("Admin"))
            {
                return null;
            }

            var slug = exhibitorSlug.CleanSlug();

            Exhibitor exhibitor;

            if(slug.Equals("New"))
            {
                exhibitor = new Exhibitor(Guid.Empty);
            }
            else
            {
                var exhibitorFilterCriteria = new ExhibitorFilterCriteria { Term = slug };

                var result = exhibitorService.GetExhibitors(eventAddress, exhibitorFilterCriteria);

                exhibitor = result.SingleOrDefault();
            }

            if (exhibitor == null)
            {
                return null;
            }

            return View(new OxiteViewModelItem<Exhibitor>(exhibitor));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add(EventAddress eventAddress)
        {
            return Edit(eventAddress, "New");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(EventAddress eventAddress, ExhibitorInput exhibitorInput)
        {
            var userAuthenticated = userService.GetUser(User.Identity.Name);

            if (userAuthenticated == null || !userAuthenticated.IsInRole("Admin"))
            {
                return null;
            }

            if (eventAddress == null)
            {
                return null;
            }

            var @event = eventService.GetEvent(eventAddress);

            var existingExhibitor = exhibitorService.GetExhibitor(eventAddress, exhibitorInput.Name);
            var exhibitorId = existingExhibitor != null ? existingExhibitor.ID : Guid.Empty;
            var exhibitorCreated = existingExhibitor != null ? existingExhibitor.CreatedDate : DateTime.Now;

            var exhibitor = new Exhibitor(exhibitorId, @event.ID, exhibitorInput.Name,
                                          exhibitorInput.Description, exhibitorInput.SiteUrl,
                                          exhibitorInput.LogoUrl, exhibitorInput.ParticipantLevel,
                                          exhibitorInput.ContactName, exhibitorInput.ContactEmail,
                                          exhibitorInput.Location, exhibitorInput.Tags,
                                          exhibitorCreated,
                                          DateTime.UtcNow);
            
            ModelResult<Exhibitor> results = exhibitorService.SaveExhibitor
                (eventAddress, exhibitor);

            if (!results.IsValid)
            {
                ModelState.AddModelErrors(results.ValidationState);
            }
            
            return new RedirectResult(Url.EditExhibitor(exhibitor.Name.CleanSlug()));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Remove(EventAddress eventAddress, string exhibitorSlug)
        {
            var userAuthenticated = userService.GetUser(User.Identity.Name);

            if (userAuthenticated == null || !userAuthenticated.IsInRole("Admin"))
            {
                return null;
            }

            if (eventAddress == null)
            {
                return null;
            }

            var exhibitor = exhibitorService.GetExhibitor(eventAddress, exhibitorSlug.CleanSlug());
            if(exhibitor == null)
            {
                return null;
            }
            
            exhibitorService.RemoveExhibitor(eventAddress, exhibitor);
            
            return new RedirectResult(Url.ManageExhibitors());
        }
    }
}
