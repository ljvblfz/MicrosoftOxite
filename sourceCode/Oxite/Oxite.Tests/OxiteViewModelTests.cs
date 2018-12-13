//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.ViewModels;
using Xunit;

namespace Oxite.Tests
{
    public class OxiteViewModelTests
    {
        [Fact]
        public void AddModelItemThenGetModelItemReturnsItem()
        {
            OxiteViewModel model = new OxiteViewModel();

            OxiteViewModelTests modelItem = new OxiteViewModelTests();

            model.AddModelItem(modelItem);

            var actualItem = model.GetModelItem<OxiteViewModelTests>();

            Assert.Same(modelItem, actualItem);
        }

        [Fact]
        public void GetModelReturnsNullIfNotFound()
        {
            OxiteViewModel model = new OxiteViewModel();

            Assert.Null(model.GetModelItem<OxiteViewModelTests>());
        }
    }
}
