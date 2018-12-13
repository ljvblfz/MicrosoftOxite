//---------------------------------------------------------------------
// <copyright file="DialogResult.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
namespace Oxite.Results
{
    using System.Web.Mvc;

    /// <summary>
    /// ViewResult to return from a Dialog Action.
    /// </summary>
    public class DialogResult : ViewResult
    {
        /// <summary>
        /// Initializes a new instance of the DialogResult class.
        /// </summary>
        public DialogResult()
        {
            this.ViewName = "Dialog";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            ViewData = context.Controller.ViewData;
            TempData = context.Controller.TempData;

            base.ExecuteResult(context);
        }
    }
}
