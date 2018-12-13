//---------------------------------------------------------------------
// <copyright file="DialogButton.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
namespace Oxite.Models
{
    using System;

    /// <summary>
    /// Class to represent a DialogButton.
    /// </summary>
    public class DialogButton : IEquatable<DialogButton>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the DialogButton class.
        /// </summary>
        /// <param name="name">Name of the button.</param>
        /// <param name="displayName">Text to display in the button.</param>
        /// <param name="postData">Determines whether or not there is associated post data from the view asking for a dialog.
        /// If true, all form data is rendered to hidden fields.</param>
        public DialogButton(string name, string displayName, bool postData)
            : this(name, displayName, postData, name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DialogButton class.
        /// </summary>
        /// <param name="name">Name of the button.</param>
        /// <param name="displayName">Text to display in the button.</param>
        /// <param name="postData">Determines whether or not there is associated post data from the view asking for a dialog.
        /// If true, all form data is rendered to hidden fields.</param>
        /// <param name="cssClass">Css class to apply to the button.</param>
        public DialogButton(string name, string displayName, bool postData, string cssClass)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.PostData = postData;
            this.CssClass = cssClass;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns a list of Default DialogButtons
        /// </summary>
        public static DialogButton[] Default
        {
            get { return new DialogButton[] { OK }; }
        }

        /// <summary>
        /// Returns a DialogButton representing a selection of OK
        /// </summary>
        public static DialogButton OK
        {
            get { return new DialogButton("ok", "OK", true); }
        }

        /// <summary>
        /// Returns a DialogButton representing a selection of Cancel
        /// </summary>
        public static DialogButton Cancel
        {
            get { return new DialogButton("cancel", "Cancel", false); }
        }

        /// <summary>
        /// Returns a DialogButton representing a selection of Apply
        /// </summary>
        public static DialogButton Apply
        {
            get { return new DialogButton("apply", "Apply", true); }
        }

        /// <summary>
        /// Returns a DialogButton representing a selection of Yes
        /// </summary>
        public static DialogButton Yes
        {
            get { return new DialogButton("yes", "Yes", true); }
        }

        /// <summary>
        /// Returns a DialogButton representing a selection of No
        /// </summary>
        public static DialogButton No
        {
            get { return new DialogButton("no", "No", false); }
        }

        /// <summary>
        /// Name of the button.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Text to display in the button.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Determines whether or not there is associated post data from the view asking for a dialog.
        /// If true, all form data is rendered to hidden fields.
        /// </summary>
        public bool PostData { get; private set; }

        /// <summary>
        /// Css class to apply to the button.
        /// </summary>
        public string CssClass { get; private set; }
        #endregion

        /// <summary>
        /// Create a DialogButton using only the name.
        /// </summary>
        /// <param name="name">Name of dialog button.</param>
        /// <returns>A DialogButton with Name, DisplayName, and CssClass set to the given name and
        /// a PostData value of false.</returns>
        public static DialogButton FromName(string name)
        {
            return new DialogButton(name, name, false, name);
        }

        #region IEquatable<DialogButton> Members
        /// <summary>
        /// Determines if the current DialogButton is equivilent to the give one.
        /// </summary>
        /// <param name="other">DialogButton to check for equivilence.</param>
        /// <returns>True if the DialogButtons are equivilent.  False otherwise.</returns>
        public bool Equals(DialogButton other)
        {
            return string.Compare(this.Name, other.Name, true) == 0;
        }

        #endregion
    }
}
