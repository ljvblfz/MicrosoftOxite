//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;

namespace Oxite.Results
{
    public class DialogSelectionResult : ActionResult
    {
        public DialogSelectionResult(string returnUrl)
            : this(returnUrl, false) { }

        public DialogSelectionResult(string returnUrl, bool isCancelled)
        {
            ReturnUrl = returnUrl;
            IsCancelled = isCancelled;
        }

        public string ReturnUrl { get; private set; }
        public bool IsCancelled { get; private set; }
        public bool IsClientRedirect { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
        }
    }
}
