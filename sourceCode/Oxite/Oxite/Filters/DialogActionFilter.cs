//---------------------------------------------------------------------
// <copyright file="DialogActionFilter.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
namespace Oxite.Filters
{
    using System.Web.Mvc;
    using Oxite.Extensions;
    using Oxite.Models;
    using Oxite.Results;

    /// <summary>
    /// ActionFilter to use a DialogSelectionResult to determine redirect actions.
    /// </summary>
    public class DialogActionFilter : IActionFilter
    {
        #region IActionFilter Members
        /// <summary>
        /// Parse the DialogSelectionResult and redirect to the appropriate place.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            DialogSelectionResult dialogSelectionResult = filterContext.Result as DialogSelectionResult;

            if (dialogSelectionResult == null) return;

            if (filterContext.HttpContext.Request.IsJQueryAjaxRequest() && dialogSelectionResult.IsClientRedirect)
                filterContext.Result = new JsonResult { Data = new AjaxRedirect(dialogSelectionResult.ReturnUrl) };

            if (!(filterContext.Result is JsonResult))
                filterContext.Result = new RedirectResult(dialogSelectionResult.ReturnUrl);
        }

        /// <summary>
        /// Set the result to a cancel DialogSelectionResult if cancel was selected.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey("dialogSelection"))
            {
                DialogSelection dialogSelection = filterContext.ActionParameters["dialogSelection"] as DialogSelection;

                if (dialogSelection != null && dialogSelection.Equals(DialogButton.Cancel))
                    filterContext.Result = new JsonResult { Data = new { cancel = 1 } };
            }
        }
        #endregion
    }
}
