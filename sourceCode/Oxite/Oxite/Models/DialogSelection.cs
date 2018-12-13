//---------------------------------------------------------------------
// <copyright file="DialogSelection.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
namespace Oxite.Models
{
    /// <summary>
    /// Class to represent a DialogSelection.
    /// </summary>
    public class DialogSelection : DialogButton
    {
        /// <summary>
        /// Initializes a new instance of the DialogSelection class.
        /// </summary>
        /// <param name="name">Name of the dialog selection.</param>
        /// <param name="returnUrl">Return url for the selection.</param>
        public DialogSelection(string name, string returnUrl)
            : base(name, null, false, null)
        {
            this.ReturnUrl = returnUrl;
        }

        /// <summary>
        /// Return url for the selection.
        /// </summary>
        public string ReturnUrl { get; private set; }
    }
}
