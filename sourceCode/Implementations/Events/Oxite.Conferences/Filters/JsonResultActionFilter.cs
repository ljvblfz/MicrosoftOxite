using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oxite.Modules.Conferences.Results;
using Oxite.ViewModels;

namespace Oxite.Modules.Conferences.Filters
{
    public class JsonResultFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;

            filterContext.Result = new JsonViewResult(actionName, false);
        }
    }

    public class JsonResultActionFilter : IActionFilter
    {
        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;

            if (model.GetType().GetGenericTypeDefinition() == typeof(OxiteViewModelItems<>))
            {
                var list = model.GetType().GetProperty("Items").GetValue(model, null);

                var count = (int)list.GetType().GetProperty("Count").GetValue(list, null);

                filterContext.Result = new JsonViewResult("JSON", count == 0);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
