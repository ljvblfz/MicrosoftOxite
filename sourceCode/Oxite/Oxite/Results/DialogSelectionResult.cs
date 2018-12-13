//---------------------------------------------------------------------
// <copyright file="DialogSelectionResult.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
namespace Oxite.Results
{
    using System.Web.Mvc;

    /// <summary>
    /// ActionResult representing a selection made within a "dialog box".
    /// </summary>
    public class DialogSelectionResult : ActionResult
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the DialogSelectionResult class.
        /// </summary>
        /// <param name="returnUrl">Return url for selection.</param>
        public DialogSelectionResult(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Return url for selection.
        /// </summary>
        public string ReturnUrl { get; private set; }

        /// <summary>
        /// Determines whether or not an AjaxRedirect is used.
        /// </summary>
        public bool IsClientRedirect { get; set; }
        #endregion

        /// <summary>
        /// Enables processing of the result of an action method.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param>
        public override void ExecuteResult(ControllerContext context)
        {
        }
    }
}
