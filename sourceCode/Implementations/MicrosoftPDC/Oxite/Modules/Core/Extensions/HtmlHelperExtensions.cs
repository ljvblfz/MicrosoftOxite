using System.Web.Mvc;

namespace Oxite.Modules.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string WebBug(this HtmlHelper helper, string entityType, string ID)
        {
            return string.Format("<img src=\"{0}\" />",
                                 new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection).ViewTrack(
                                     entityType, "web", ID));
        }
    }
}