//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Oxite.Infrastructure;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Infrastructure
{
    public class AbsolutePathHelper
    {
        private readonly OxiteContext context;

        public AbsolutePathHelper(OxiteContext context)
        {
            this.context = context;
        }

        public string GetAbsolutePath(ScheduleItemComment comment)
        {
            if (comment == null) throw new ArgumentNullException("comment");
            if (string.IsNullOrEmpty(comment.Slug) || string.IsNullOrEmpty(comment.ScheduleItem.Slug) || string.IsNullOrEmpty(comment.ScheduleItem.EventName)) throw new ArgumentException();

            UriBuilder builder = new UriBuilder(context.Site.Host.Scheme, context.Site.Host.Host, context.Site.Host.Port, context.Site.Host.AbsolutePath);
            UrlHelper urlHelper = new UrlHelper(new RequestContext(new DummyHttpContext(null, context.Site.Host), new RouteData()), context.Routes);

            //TODO: (erikpo) It might make sense to move UrlHelperExtensions back into Oxite so this isn't duplicated
            //TODO: (erikpo) This is pointing to a route that's current in App_Code.  That route should move
            builder.Path = urlHelper.RouteUrl("PDC09SessionCommentPermalink", new { scheduleItemSlug = comment.ScheduleItem.Slug, commentSlug = comment.Slug });

            return builder.Uri.ToString();
        }

        private class DummyHttpContext : HttpContextBase
        {
            private readonly Uri requestUrl;
            private readonly Uri hostUrl;

            public DummyHttpContext(Uri requestUrl, Uri hostUrl)
            {
                this.requestUrl = requestUrl;
                this.hostUrl = hostUrl;
            }

            public override HttpRequestBase Request
            {
                get
                {
                    return new DummyHttpRequest(requestUrl, hostUrl);
                }
            }

            public override HttpResponseBase Response
            {
                get
                {
                    return new DummyHttpResponse();
                }
            }
        }

        private class DummyHttpRequest : HttpRequestBase
        {
            private readonly Uri requestUrl;
            private readonly Uri hostUrl;

            public DummyHttpRequest(Uri requestUrl, Uri hostUrl)
            {
                this.requestUrl = requestUrl;
                this.hostUrl = hostUrl;
            }

            public override Uri Url
            {
                get
                {
                    return requestUrl;
                }
            }

            public override string ApplicationPath
            {
                get
                {
                    return hostUrl.AbsolutePath;
                }
            }

            public override string AppRelativeCurrentExecutionFilePath
            {
                get
                {
                    if (hostUrl.AbsolutePath.Length > 1)
                        return "~" + requestUrl.AbsolutePath.Remove(0, hostUrl.AbsolutePath.Length);

                    return "~" + requestUrl.AbsolutePath;
                }
            }

            public override string PathInfo
            {
                get
                {
                    return "";
                }
            }

            public override System.Collections.Specialized.NameValueCollection ServerVariables
            {
                get
                {
                    return new System.Collections.Specialized.NameValueCollection();
                }
            }
        }

        private class DummyHttpResponse : HttpResponseBase
        {
            public override string ApplyAppPathModifier(string virtualPath)
            {
                return virtualPath;
            }
        }
    }
}
