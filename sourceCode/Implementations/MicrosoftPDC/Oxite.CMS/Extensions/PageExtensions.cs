//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;
using Oxite.Modules.CMS.Models;

namespace Oxite.Modules.CMS.Extensions
{
    public static class PageExtensions
    {
        public static Page Apply(this Page page, PageInput input, Guid siteID, UserAuthenticated creator, EntityState state)
        {
            return new Page(page.ID, new SiteSmall(siteID), input.TemplateName, input.Title, input.Description, input.Slug, input.Published);
        }
    }
}
