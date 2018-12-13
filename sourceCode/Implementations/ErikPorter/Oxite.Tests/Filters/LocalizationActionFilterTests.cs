//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.ActionFilters;
using Oxite.Models;
using Oxite.Tests.Fakes;
using Oxite.ViewModels;
using Xunit;

namespace Oxite.Tests.Filters
{
    public class LocalizationActionFilterTests
    {
        [Fact]
        public void OnActionExecutedAddsAllPhrasesToModel()
        {
            FakeLocalizationService locService = new FakeLocalizationService();

            OxiteModel model = new OxiteModel();

            ActionExecutedContext context = new ActionExecutedContext()
            {
                Result = new ViewResult() { ViewData = new ViewDataDictionary(model) }
            };

            LocalizationActionFilter filter = new LocalizationActionFilter(locService);

            filter.OnActionExecuted(context);

            Assert.NotNull(model.GetModelItem<ICollection<Phrase>>());
            Assert.Same(locService.Phrases, model.GetModelItem<ICollection<Phrase>>());
        }
    }
}
