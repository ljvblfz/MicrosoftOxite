//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Models;
using Oxite.Results;
using Oxite.ViewModels;

namespace Oxite.Infrastructure
{
    public class OxiteControllerActionInvoker : ControllerActionInvoker
    {
        private readonly IFilterRegistry filterRegistry;

        public OxiteControllerActionInvoker(IFilterRegistry filterRegistry)
        {
            this.filterRegistry = filterRegistry;
        }

        protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue)
        {
            if (actionReturnValue == null)
            {
                controllerContext.Controller.ViewData.Model = new OxiteModel { Container = new NotFoundPageContainer() };

                return new NotFoundResult();
            }

            if (typeof(ActionResult).IsAssignableFrom(actionReturnValue.GetType()))
                return actionReturnValue as ActionResult;

            controllerContext.Controller.ViewData.Model = actionReturnValue;

            return new ViewResult { ViewData = controllerContext.Controller.ViewData, TempData = controllerContext.Controller.TempData };
        }

        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            FilterInfo baseFilters = base.GetFilters(controllerContext, actionDescriptor);

            FilterInfo registeredFilters = filterRegistry.GetFilters(new FilterRegistryContext(controllerContext, actionDescriptor));

            foreach (IActionFilter actionFilter in registeredFilters.ActionFilters)
                baseFilters.ActionFilters.Insert(0, actionFilter);
            foreach (IAuthorizationFilter authorizationFilter in registeredFilters.AuthorizationFilters)
                baseFilters.AuthorizationFilters.Add(authorizationFilter);
            foreach (IExceptionFilter exceptionFilter in registeredFilters.ExceptionFilters)
                baseFilters.ExceptionFilters.Add(exceptionFilter);
            foreach (IResultFilter resultFilter in registeredFilters.ResultFilters)
                baseFilters.ResultFilters.Add(resultFilter);

            return baseFilters;
        }
    }
}
