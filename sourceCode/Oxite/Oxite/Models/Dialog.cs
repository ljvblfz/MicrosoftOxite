//---------------------------------------------------------------------
// <copyright file="Dialog.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
namespace Oxite.Models
{
    /// <summary>
    /// Class to hold data needed to populate a "dialog box".
    /// </summary>
    public class Dialog
    {
        /// <summary>
        /// Initializes a new instance of the Dialog class.
        /// </summary>
        /// <param name="message">Message to displayed in the dialog.</param>
        /// <param name="format">Determines what type of dialog should be displayed.  ie: DialogFormat.Warning</param>
        /// <param name="buttons">List of DialogButtons that should be available in the Dialog.</param>
        public Dialog(string message, DialogFormat format, params DialogButton[] buttons)
        {
            //TODO: (erikpo) Instead of just passing message, pass something like "LocalizedString" or something that can pass along information so the message can be localized
            this.Message = message;
            this.Format = format;
            this.Buttons = buttons != null && buttons.Length > 0 ? buttons : DialogButton.Default;
        }

        /// <summary>
        /// Message to displayed in the dialog.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Determines what type of dialog should be displayed.  ie: DialogFormat.Warning
        /// </summary>
        public DialogFormat Format { get; private set; }

        /// <summary>
        /// List of DialogButtons that should be available in the Dialog.
        /// </summary>
        public DialogButton[] Buttons { get; private set; }
    }
}
