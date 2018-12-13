using System.Web.Mvc;
using Oxite.Results;
using Oxite.ViewModels;

namespace Oxite.Filters
{
    public class XlsResultActionFilter : ActionFilterAttribute
    {
        private readonly string _feedType;

        public XlsResultActionFilter(string viewName)
        {
            _feedType = viewName;
        }

        #region IActionFilter Members

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;
            if (model.GetType().GetGenericTypeDefinition() != typeof (OxiteViewModelItems<>))
            {
                return;
            }

            filterContext.Result = new XlsResult(_feedType);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) { }

        #endregion
    }
}
