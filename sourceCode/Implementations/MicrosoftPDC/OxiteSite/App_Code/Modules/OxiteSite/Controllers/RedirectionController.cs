// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Modules.Conferences.Services;
using Oxite.Results;

namespace OxiteSite.App_Code.Modules.OxiteSite.Controllers
{
    public class RedirectionController : Controller
    {
        private readonly IEventService eventService;
        private readonly IScheduleItemService scheduleItemService;
        private readonly OxiteContext oxiteContext;

        public RedirectionController(IEventService eventService, IScheduleItemService scheduleItemService, OxiteContext context)
        {
            this.eventService = eventService;
            this.scheduleItemService = scheduleItemService;
            this.oxiteContext = context;
            ValidateRequest = false;
        }



        public object Mobile()
        {
            return new PermanentRedirectResult(oxiteContext.Site.Host.ToString());
        }

        public object ViewASPX()
        {
            return new PermanentRedirectResult(oxiteContext.Site.Host.ToString());
        }

        public object Social()
        {
            return new PermanentRedirectResult(oxiteContext.Site.Host.ToString());
        }

        public object Content()
        {
            return new PermanentRedirectResult(new Uri(oxiteContext.Site.Host,"/Sessions").ToString());
        }

        public object Agenda()
        {
            return new PermanentRedirectResult(new Uri(oxiteContext.Site.Host, "/Schedule").ToString());
        }

        public object OldRSS()
        {
            return new PermanentRedirectResult(new Uri(oxiteContext.Site.Host, "/rss").ToString());
        }

        public object OldBling()
        {
            return new PermanentRedirectResult(new Uri(oxiteContext.Site.Host, "/art").ToString());
        }

        public object OldPage(string path)
        {
            return new PermanentRedirectResult(new Uri(oxiteContext.Site.Host, path).ToString());
        }

        public object CurrentPress()
        {
            // http://www.microsoft.com/presspass/events/pdc/
            return new RedirectResult(new Uri("http://www.microsoft.com/presspass/events/pdc/").ToString());
        }

        public object Speaker(string speakerName)
        {
            string newSpeakerName;

            if (speakerName == null)
                return null;

            switch (speakerName.ToLower().Trim())
            {
                case "richci": newSpeakerName = "Richard-Ciapala"; break;
                case "smarx": newSpeakerName = "Steve-Marx"; break;
                case "sramchandani": newSpeakerName = "Seema-Ramchandani"; break;
                case "michelelerouxbustamante": newSpeakerName = "Michele-Leroux-Bustamante"; break;
                case "pstubbs": newSpeakerName = "Paul-Stubbs"; break;
                case "bjabes": newSpeakerName = "Boris-Jabes"; break;
                case "shanselman": newSpeakerName = "Scott-Hanselman"; break;
                case "jdurant": newSpeakerName = "John-Durant"; break;
                case "cboyd": newSpeakerName = "Chas-Boyd"; break;
                case "dbox": newSpeakerName = "Don-Box"; break;
                case "mrussinovich": newSpeakerName = "Mark-Russinovich"; break;
                case "hwilson": newSpeakerName = "Hervey-Wilson"; break;
                case "dcampbell": newSpeakerName = "David-Campbell"; break;
                case "adem": newSpeakerName = "Ade-Miller"; break;
                case "kcorby": newSpeakerName = "Karen-Corby"; break;
                case "juvallowy": newSpeakerName = "Juval-Lowy"; break;
                case "cmayo": newSpeakerName = "Chris-Mayo"; break;
                case "andrewbrust": newSpeakerName = "Andrew-Brust"; break;
                case "toddgirvin": newSpeakerName = "Todd-Girvin"; break;
                case "canderson": newSpeakerName = "Chris-Anderson"; break;
                case "hsutter": newSpeakerName = "Herb-Sutter"; break;
                case "nellis": newSpeakerName = "Nigel-Ellis"; break;
                case "bsmith": newSpeakerName = "Burton-Smith"; break;
                case "mammerlaan": newSpeakerName = "Mike-Ammerlaan"; break;
                case "dondemsak": newSpeakerName = "Don-Demsak"; break;
                case "chrisauld": newSpeakerName = "Chris-Auld"; break;
                case "bschmidt": newSpeakerName = "Bob-Schmidt"; break;
                case "richardgriffin": newSpeakerName = "Richard-Griffin"; break;
                case "abybee": newSpeakerName = "Andrew-Bybee"; break;
                case "stoub": newSpeakerName = "Stephen-Toub"; break;
                case "iangriffiths": newSpeakerName = "Ian-Griffiths"; break;
                case "christullier": newSpeakerName = "Chris-Tullier"; break;

                default:
                    return null;
            }

            return new PermanentRedirectResult(new Uri(oxiteContext.Site.Host, "/Speakers/" + newSpeakerName).ToString());

       }


    }
}
