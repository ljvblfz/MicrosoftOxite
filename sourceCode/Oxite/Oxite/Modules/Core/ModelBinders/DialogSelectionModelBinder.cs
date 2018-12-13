//---------------------------------------------------------------------
// <copyright file="DialogSelectionModelBinder.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------

namespace Oxite.Modules.Core.ModelBinders
{
    using System.Web;
    using System.Web.Mvc;
    using Oxite.Models;

    public class DialogSelectionModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            if (string.Compare(request.HttpMethod, HttpVerbs.Post.ToString(), true) == 0 && request.Form.Count > 0 && !string.IsNullOrEmpty(request.Form["selectedButton"]))
            {
                return new DialogSelection(request.Form["selectedButton"], request.Form["returnUrl"]);
            }

            return null;
        }
    }
}
