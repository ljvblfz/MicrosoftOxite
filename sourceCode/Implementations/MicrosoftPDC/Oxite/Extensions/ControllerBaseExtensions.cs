//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Models;
using Oxite.Results;
using Oxite.ViewModels;

namespace Oxite.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static DialogResult Dialog(this ControllerBase controller, string message, DialogFormat format, params DialogButton[] buttons)
        {
            controller.ViewData.Model = new OxiteViewModelItem<Dialog>(new Dialog(message, format, buttons));

            return new DialogResult();
        }
    }
}
