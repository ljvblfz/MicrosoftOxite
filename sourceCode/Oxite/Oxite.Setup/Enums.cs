//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
namespace Oxite.Modules.Setup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum SiteType
    {
        PersonalBlog,
        CorporateBlog,
        ECommerce,
        Community
    }

    public enum StorageType
    {
        Xml,
        Sql
    }
}
