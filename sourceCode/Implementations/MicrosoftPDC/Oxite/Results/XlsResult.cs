using System.Web.Mvc;

namespace Oxite.Results
{
    public class XlsResult : ViewResult
    {
        public XlsResult(string viewName)
        {
            ViewName = viewName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            TempData = context.Controller.TempData;
            ViewData = context.Controller.ViewData;

            base.ExecuteResult(context);

            context.HttpContext.Response.ContentType = "application/vnd.ms-excel";
        }
    }
}
