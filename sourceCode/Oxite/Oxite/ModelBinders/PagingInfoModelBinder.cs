//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;
using Oxite.Infrastructure;

namespace Oxite.ModelBinders
{
    public class PagingInfoModelBinder : IModelBinder
    {
        private const string routeDataName = "pageNumber";
        private readonly Regex pageNumberRegex = new Regex(@"(?:(?<=^|/)Page(?<number>\d+)(?=$|/))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (!bindingContext.ValueProvider.ContainsKey(routeDataName))
            {
                string routeUrl = "";

                if (controllerContext.RouteData.Route is Route)
                    routeUrl = ((Route)controllerContext.RouteData.Route).Url;

                throw new InvalidOperationException(
                    string.Format(
                        "{0} requires a parameter of type {1} but the matching route url '{2}' does not contain {3} route data.",
                        controllerContext.Controller.GetType().FullName, typeof(PagingInfo).FullName, routeUrl,
                        routeDataName));
            }

            string pageNumber = bindingContext.ValueProvider[routeDataName].RawValue as string;
            int index = 0;

            if (!string.IsNullOrEmpty(pageNumber))
            {
                Match pageNumberMatch = pageNumberRegex.Match(pageNumber);

                if (pageNumberMatch.Groups["number"].Success && int.TryParse(pageNumberMatch.Groups["number"].Value, out index))
                    index = index > 0 ? index - 1 : 0;
            }

            return new PagingInfo(index, 10);
        }
    }
}