//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;

namespace Oxite.Results
{
    public class DialogResult : ViewResult
    {
        public DialogResult()
        {
            ViewName = "Dialog";
        }

        public override void ExecuteResult(ControllerContext context)
        {
            ViewData = context.Controller.ViewData;
            TempData = context.Controller.TempData;

            base.ExecuteResult(context);
        }
    }
}
