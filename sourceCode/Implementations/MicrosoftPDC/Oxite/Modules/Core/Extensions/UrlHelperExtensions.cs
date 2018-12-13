using System.Web.Mvc;

namespace Oxite.Modules.Core.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ViewTrack(this UrlHelper helper, string entityType, string viewType, string ID)
        {
            return helper.RouteUrl("RecordView", new {entityType, viewType, ID});
        }
    }
}