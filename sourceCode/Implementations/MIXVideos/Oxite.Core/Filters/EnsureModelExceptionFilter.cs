//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.ViewModels;

namespace Oxite.Filters
{
    public class EnsureModelExceptionFilter : IExceptionFilter
    {
        #region IExceptionFilter Members

        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.ViewData.Model = new ExceptionOxiteModel();
        }

        #endregion
    }
}
