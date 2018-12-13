// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Services;
using Oxite.ViewModels;

namespace OxiteSite.App_Code.Modules.OxiteSite.Controllers
{
    public class PageController : Controller
    {
        private readonly IEventService eventService;
        private readonly IScheduleItemService scheduleItemService;

        private readonly AppSettingsHelper appSettings;

        public PageController(IEventService eventService, IScheduleItemService scheduleItemService, AppSettingsHelper appSettings)
        {
            this.eventService = eventService;
            this.scheduleItemService = scheduleItemService;
            this.appSettings = appSettings;

            ValidateRequest = false;
        }

        public OxiteViewModelItems<ScheduleItem> Home(int pageIndex, int pageSize, EventAddress eventAddress)
        {
            IPageOfItems<ScheduleItem> scheduleItems = scheduleItemService.GetScheduleItemsByFlag(pageIndex, pageSize, eventAddress, "featured");

            return new OxiteViewModelItems<ScheduleItem>(scheduleItems);
        }


        public object Hotels()
        {
            return new OxiteViewModelItems<string>();
        }

        public object Partners()
        {
            return new OxiteViewModelItems<string>();
        }

        public OxiteViewModelItems<string> Maps(string mapType)
        {
           return new OxiteViewModelItems<string>(new string[] { mapType });
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public object Contact()
        {
            return new OxiteViewModelItems<string>();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public object Contact(string subject, string email, string message)
        {

            string targetEmailAddress = appSettings.GetString("contact.email");
            string targetEmailSubject = appSettings.GetString("contact.subject");


            //TODO: (duncanma) Move the following validation logic into a validator

            if (string.IsNullOrEmpty(subject))
                ModelState.AddModelError("subject", "You must specify a subject when submitting feedback.");

            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);

            if (string.IsNullOrEmpty(email))
                ModelState.AddModelError("email", "You must specify an email when submitting feedback.");
            else
            {
                if (!re.IsMatch(email))
                    ModelState.AddModelError("email", "You must specify a valid email to submit feedback.");
            }

            if (string.IsNullOrEmpty(message))
                ModelState.AddModelError("message", "You must specify a message to be sent as feedback.");



            if (ViewData.ModelState.IsValid && email != null)
            {

                SmtpClient sc = new SmtpClient("localhost")
                                    {
                                        Credentials = new System.Net.NetworkCredential("mail@on10.net", "br97snws1a3")
                                    };


                string emailTo = targetEmailAddress;
                string emailFrom = email;
                string messageSubject = targetEmailSubject + subject;
                string messageBody = message;

                MailMessage mm = new MailMessage(emailFrom, emailTo, messageSubject, messageBody);

                sc.Send(mm);
                return new OxiteViewModelItems<string>(new string[] { "Feedback Sent" });
            }

            return new OxiteViewModelItems<string>();

        }


    }
}
