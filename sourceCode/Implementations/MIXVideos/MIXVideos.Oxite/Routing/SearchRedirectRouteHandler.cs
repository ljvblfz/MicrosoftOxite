using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;

namespace MIXVideos.Oxite.Routing
{
    public class SearchRedirectRouteHandler : IRouteHandler
    {
        #region IRouteHandler Members

        public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new SearchRedirectHttpHandler(requestContext);
        }

        #endregion

        class SearchRedirectHttpHandler : IHttpHandler
        {
            private RequestContext requestContext;
            public SearchRedirectHttpHandler(RequestContext requestContext)
            {
                this.requestContext = requestContext;
            }

            #region IHttpHandler Members

            public bool IsReusable
            {
                get { return false; }
            }

            public void ProcessRequest(HttpContext context)
            {
                string searchPath = RouteTable.Routes.GetVirtualPath(this.requestContext, "PostsBySearch", new RouteValueDictionary(new { term = context.Request.QueryString["selectedSearch"] })).VirtualPath;
                context.Response.Redirect(searchPath, false);
            }
        }
        #endregion
    }
}
