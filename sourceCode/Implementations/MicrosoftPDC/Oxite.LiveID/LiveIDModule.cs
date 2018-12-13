//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Passport.RPS;
using Microsoft.Practices.Unity;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.LiveID.Extensions;
using Oxite.Modules.LiveID.Filters;
using Oxite.Modules.Membership.Services;
using Oxite.Services;

namespace Oxite.Modules.LiveID
{
    public class LiveIDModule : IOxiteAuthenticationModule
    {
        private readonly IUnityContainer container;

        public LiveIDModule(IUnityContainer container)
        {
            this.container = container;
        }

        #region IOxiteAuthenticationModule Members

        public IUser GetUser(RequestContext context)
        {
            
            HttpRequest request = (HttpRequest)context.HttpContext.Items["originalRequest"];

            if (context.HttpContext.Items.Contains("CalledLiveIDAlready"))
            {
                if (context.HttpContext.Items.Contains(typeof(IUser).FullName) )
                {
                    return context.HttpContext.Items[typeof (IUser).FullName] as IUser;
                }
            }
                

            context.HttpContext.Items.Add("CalledLiveIDAlready", "Did it");

            HttpResponse response = (HttpResponse)context.HttpContext.Items["originalResponse"];
            IUserService userService = container.Resolve<IUserService>();

                // Get the RPS object preinitialized in the Global.asax
                RPS myRps = GetRPS(context);
                if (myRps == null) return null;
                string siteName = GetSiteName();

                // Create other base RPS objects.
                RPSHttpAuth httpAuth = new RPSHttpAuth(myRps);
                RPSPropBag authPropBag = new RPSPropBag(myRps);
                RPSDomainMap domainMap = new RPSDomainMap(myRps);

                RPSServerConfig mainConfig = new RPSServerConfig(myRps);
                RPSPropBag siteConfig = (RPSPropBag)mainConfig["Sites"];

                RPSPropBag mySiteConfig = (RPSPropBag)siteConfig[siteName];

                int siteID = Convert.ToInt32(mySiteConfig["SiteID"]);

                // Set returnUrl and siteID in authPropBag.

                // Determine if SSL should be used.
                string returnUrl;
                if (context.HttpContext.Request.IsSecureConnection)
                {
                    returnUrl = "https://"
                        + context.HttpContext.Request.ServerVariables["SERVER_NAME"]
                        + context.HttpContext.Request.Path;
                }
                else
                {
                    returnUrl = "http://"
                      + context.HttpContext.Request.ServerVariables["SERVER_NAME"]
                      + context.HttpContext.Request.Path;
                }

                authPropBag["ReturnURL"] = returnUrl;
                authPropBag["SiteID"] = siteID;

                // Create ticket object and populate the authPropBag with 
                // values from the Request object.
                RPSTicket ticket = httpAuth.Authenticate(siteName, request, authPropBag);

                // Get the user's 'authState' to determine if user is currently signed in.
                uint authState = (uint)authPropBag["RPSAuthState"];

                if (ticket == null)
                {
                    //No RPSTicket found.  Check for AuthState=2 (Maybe) state.
                    if (authState == 2)
                    {
                        // RPS Maybe state detected. Indicates a ticket is present, 
                        // but cannot be read. Redirect to SilentAuth URL to obtain 
                        // a fresh RPS Ticket. Write RPS response headers to write 
                        // the Maybe state cookie to prevent looping.
                        string rpsHeaders = (string)authPropBag["RPSRespHeaders"];
                        if (rpsHeaders != "")
                        {
                            httpAuth.WriteHeaders(response, rpsHeaders);
                        }

                        string silentAuthUrl =
                            domainMap.ConstructURL(
                                "SilentAuth",
                                siteName,
                                null,
                                authPropBag);
                        context.HttpContext.Response.Redirect(silentAuthUrl);
                    }
                    else
                    {
                        // User is not signed in.  Show page with Sign-in option.

                        //Message.Text = "A ticket was not detected. "
                        //    + "Click the sign in button below to sign in.";
                        //ShowTicketProperties.Text = "";
                        //bool showSignIn = true;
                        //bool isSecure = Request.IsSecureConnection;

                        //LogoButton.Text =
                        //    httpAuth.LogoTag(
                        //        showSignIn,
                        //        isSecure,
                        //        null,
                        //        null,
                        //        siteName,
                        //        authPropBag);
                    }
                }
                else
                {
                    // RPS ticket found. Ensure ticket is valid (signature is valid 
                    // and policy criteria such as time window and use of SSL are met).

                    bool isValid = ticket.Validate(authPropBag);

                    if (!isValid)
                    {
                        // RPS ticket exists, but is not valid. Refresh ticket by 
                        // redirecting user to Auth URL. If appropriate Login server 
                        // cookies exist, ticket refresh will be transparent to the user.
                        context.HttpContext.Response.Redirect(
                            domainMap.ConstructURL(
                                "Auth",
                                siteName,
                                null,
                                authPropBag));
                    }
                    else
                    {
                        //Get RPS response headers from authPropBag and write to response stream
                        string rpsHeaders = (string)authPropBag["RPSRespHeaders"];
                        if (rpsHeaders != "")
                        {
                            httpAuth.WriteHeaders(response, rpsHeaders);
                        }

                        UserAuthenticated user = userService.GetUserByModuleData("LiveID", (string)ticket.Property["HexPUID"]);

                        if (user == null)
                        {
                            UrlHelper urlHelper = new UrlHelper(context);

                            if (!urlHelper.IsRegisterRoute() && !urlHelper.IsSignOutRoute() && !urlHelper.IsErrorRoute())
                                context.HttpContext.Response.Redirect(urlHelper.Register(), true);

                            IDictionary<string, object> values = new Dictionary<string, object>();

                            values.Add("PUID", (string)ticket.Property["HexPUID"]);
                            values.Add("FirstName", (string)ticket.ProfileProperty["firstname"]);
                            values.Add("LastName", (string)ticket.ProfileProperty["lastname"]);

                            return new UserUnregistered("Unregistered User", "LiveID", values);
                        }
                        else
                        {
                            user.AuthenticationValues["PUID"] = (string)ticket.Property["HexPUID"];
                            user.AuthenticationValues["FirstName"] = (string)ticket.ProfileProperty["firstname"];
                            user.AuthenticationValues["LastName"] = (string)ticket.ProfileProperty["lastname"];
                        }

                        return user;
                    }
                }


            return new UserAnonymous();
        }

        private string GetSiteName()
        {
            return container.Resolve<ISiteService>().GetSite().Host.Host;
        }

        private static RPS GetRPS(RequestContext context)
        {
            return context.HttpContext.Application["globalRPS"] as RPS;
        }

        public string GetRegistrationUrl(RequestContext context)
        {
            return new UrlHelper(context).Register();
        }

        public string GetSignInUrl(RequestContext context)
        {
            return GetLiveIDUrl(container.Resolve<RouteCollection>(), context, "Auth");
        }

        public string GetSignOutUrl(RequestContext context)
        {
            return GetLiveIDUrl(container.Resolve<RouteCollection>(), context, "Logout");
        }

        #endregion

        #region IOxiteModule Members

        public void Initialize()
        {
            RPS globalRPS = new RPS();

            HttpContext.Current.Application["globalRPS"] = globalRPS;
            
            globalRPS.Initialize(null);
        }

        public void Unload()
        {
            HttpContext context = HttpContext.Current;
            RPS globalRPS = context.Application["globalRPS"] as RPS;

            if (globalRPS != null)
            {
                globalRPS.Shutdown();

                context.Application["globalRPS"] = null;
            }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            string[] controllerNamespaces = new string[] { "Oxite.Modules.LiveID.Controllers" };

            routes.MapRoute(
                "LiveIDRegister",
                "Register",
                new { controller = "User", action = "Register" },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "LiveIDSignOutImage",
                "SignOut.gif",
                new { controller = "User", action = "SignOutImage" },
                null,
                controllerNamespaces
                );
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(P3PHeaderResultFilter));
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
        }

        public void RegisterWithContainer()
        {
        }

        #endregion

        #region Static Methods

        public string GetLiveIDUrl(RouteCollection routes, RequestContext context, string urlType)
        {
            RPS globalRPS = GetRPS(context);

            if (globalRPS == null) return "";

            RPSPropBag loginUrlProperties = new RPSPropBag(globalRPS);

            loginUrlProperties["ReturnURL"] = new UrlHelper(context, routes).AbsolutePath(context.HttpContext.Request.Url.AbsolutePath);

            string siteName = GetSiteName();

            try
            {
                return new RPSDomainMap(globalRPS).ConstructURL(urlType, siteName, null, loginUrlProperties);
            }
            catch
            {
                return "";
            }

        }

        #endregion
    }
}
