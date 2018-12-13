//---------------------------------------------------------------------
// <copyright file="DialogFormat.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
namespace Oxite.Models
{
    using System;

    /// <summary>
    /// Class to represent the type of "dialog box" that should be displayed.
    /// </summary>
    public class DialogFormat : IEquatable<DialogFormat>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the DialogFormat class.
        /// </summary>
        /// <param name="name">Name of the format.</param>
        public DialogFormat(string name)
            : this(name, name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DialogFormat class.
        /// </summary>
        /// <param name="name">Name of the format.</param>
        /// <param name="cssClass">Css class to apply to this format.</param>
        public DialogFormat(string name, string cssClass)
        {
            this.Name = name;
            this.CssClass = cssClass;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Instance of the DialogFormat class representing an informational dialog.
        /// </summary>
        public static DialogFormat Info
        {
            get { return new DialogFormat("info"); }
        }

        /// <summary>
        /// Instance of the DialogFormat class representing a question dialog.
        /// </summary>
        public static DialogFormat Question
        {
            get { return new DialogFormat("question"); }
        }

        /// <summary>
        /// Instance of the DialogFormat class representing a warning dialog.
        /// </summary>
        public static DialogFormat Warning
        {
            get { return new DialogFormat("warning"); }
        }

        /// <summary>
        /// Instance of the DialogFormat class representing an error dialog.
        /// </summary>
        public static DialogFormat Error
        {
            get { return new DialogFormat("error"); }
        }

        /// <summary>
        /// Name of the format.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Css class to apply to the format.
        /// </summary>
        public string CssClass { get; private set; }
        #endregion

        #region IEquatable<DialogFormat> Members
        /// <summary>
        /// Determines whether or not the current DialogFormat instance is equivilent to the given
        /// instance.
        /// </summary>
        /// <param name="other">Instance to check for equilivence.</param>
        /// <returns>True if the instances are equilivent.  False otherwise.</returns>
        public bool Equals(DialogFormat other)
        {
            return string.Compare(this.Name, other.Name, true) == 0;
        }
        #endregion
    }
}
