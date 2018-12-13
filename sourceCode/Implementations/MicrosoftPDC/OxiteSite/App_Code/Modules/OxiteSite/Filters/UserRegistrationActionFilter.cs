//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Configuration;
using System.Net;
using System.Web.Mvc;
using Oxite.Models;
using Oxite.ViewModels;
using OxiteSite.App_Code.Modules.OxiteSite.Models;
using OxiteSite.App_Code.Modules.OxiteSite.Services;

namespace OxiteSite.App_Code.Modules.OxiteSite.Filters
{
    public class UserRegistrationActionFilter : IActionFilter
    {
        private readonly IPDC09Service pdc09Service;

        public UserRegistrationActionFilter(IPDC09Service pdc09Service)
        {
            this.pdc09Service = pdc09Service;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;

            if (model != null)
            {
                UserAuthenticated user = model.User.ToUserAuthenticated();

                if (user != null)
                {
                    UserRegistration registration = pdc09Service.GetUserRegistration(user);
                    bool isRegistered = registration.IsRegistered;

                    if (!isRegistered && (!registration.LastRegistrationCheck.HasValue || registration.LastRegistrationCheck.Value.AddMinutes(15) < DateTime.UtcNow))
                    {
                        using (RegistrationService.PDC09Service service = new RegistrationService.PDC09Service())
                        {
                            service.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["PDCRegistration.Username"], ConfigurationManager.AppSettings["PDCRegistration.Password"]);

                            try
                            {
                                isRegistered = service.IsRegistered((string) user.AuthenticationValues["PUID"]);
                            }
                            catch {}
                        }

                        pdc09Service.SetUserRegistration(user, isRegistered);
                    }

                    user.AuthenticationValues["IsRegistered"] = isRegistered;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
