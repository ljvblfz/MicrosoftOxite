using System.Web.Mvc;
using Oxite.Modules.Conferences.Results;
using Oxite.ViewModels;

namespace Oxite.Modules.Conferences.Filters
{
    public class XmlResultFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;

            filterContext.Result = new XmlResult(actionName, false);
        }
    }

    public class XmlResultActionFilter : IActionFilter
    {
        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;

            if (model.GetType().GetGenericTypeDefinition() == typeof(OxiteViewModelItems<>))
            {
                var list = model.GetType().GetProperty("Items").GetValue(model, null);

                var count = (int)list.GetType().GetProperty("Count").GetValue(list, null);

                filterContext.Result = new XmlResult("XML", count == 0);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
