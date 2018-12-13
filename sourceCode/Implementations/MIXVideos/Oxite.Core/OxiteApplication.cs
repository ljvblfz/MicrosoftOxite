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
using Microsoft.Practices.Unity;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite
{
    public class OxiteApplication : HttpApplication
    {
        private readonly ContainerFactory containerFactory;

        public OxiteApplication()
            : this(new ContainerFactory())
        {
        }

        public OxiteApplication(ContainerFactory containerFactory)
        {
            this.containerFactory = containerFactory;
        }

        protected void Application_Start()
        {
            Application["container"] = this.containerFactory.GetOxiteContainer();
            Application["bootStrappersLoaded"] = false;

            load();
        }

        protected void Application_End()
        {
            unload();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Site site = ((IUnityContainer)Application["container"]).Resolve<Site>();

            if (site.ID == Guid.Empty)
            {
                string setupUrl = new UrlHelper(new RequestContext(new HttpContextWrapper(Context), new RouteData())).Site();

                if (!Request.RawUrl.EndsWith(setupUrl, StringComparison.OrdinalIgnoreCase) && Request.RawUrl.IndexOf("/skins", StringComparison.OrdinalIgnoreCase) == -1 && !Request.RawUrl.StartsWith("/Content", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect(setupUrl, true);
                }
            }
            
            if (site.ID != Guid.Empty && !hasSameHostAsRequest(site.Host))
            {
                Uri hostAlias = site.HostRedirects.Where(hasSameHostAsRequest).FirstOrDefault();

                if (hostAlias != null)
                {
                    Response.RedirectLocation = makeHostMatchRequest(site.Host).ToString();
                    Response.StatusCode = 301;
                    Response.End();
                }
            }
        }

        public static void Load(HttpContextBase context)
        {
            IEnumerable<IBootStrapperTask> tasks = ((IUnityContainer)context.Application["container"]).ResolveAll<IBootStrapperTask>();
            bool bootStrappersLoaded = (bool)context.Application["bootStrappersLoaded"];
            IDictionary<string, object> state = (IDictionary<string, object>)context.Application["bootStrapperState"];

            if (state == null)
                context.Application["bootStrapperState"] = state = new Dictionary<string, object>();

            if (bootStrappersLoaded)
                foreach (IBootStrapperTask task in tasks)
                    task.Cleanup(state);

            foreach (IBootStrapperTask task in tasks)
                task.Execute(state);

            context.Application["bootStrappersLoaded"] = true;
        }

        private void load()
        {
            Load(new HttpContextWrapper(Context));
        }

        private void unload()
        {
            IDictionary<string, object> state = (IDictionary<string, object>)Context.Application["bootStrapperState"];

            foreach (IBootStrapperTask task in ((IUnityContainer)Context.Application["container"]).ResolveAll<IBootStrapperTask>())
                task.Cleanup(state);
        }

        private bool hasSameHostAsRequest(Uri url)
        {
            if (!string.Equals(url.Scheme, Request.Url.Scheme, StringComparison.OrdinalIgnoreCase))
                return false;

            if (!string.Equals(url.Host, Request.Url.Host, StringComparison.OrdinalIgnoreCase))
                return false;

            if (url.Port != Request.Url.Port)
                return false;

            return true;
        }

        private Uri makeHostMatchRequest(Uri url)
        {
            if (url == null) return null;

            UriBuilder builder = new UriBuilder(Request.Url);
            UriBuilder builder2 = new UriBuilder(url);

            builder.Scheme = builder2.Scheme;
            builder.Host = builder2.Host;
            builder.Port = builder2.Port;

            return builder.Uri;
        }
    }
}
