//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Web;
using Oxite.Modules.CMS.Models;

namespace Oxite.Modules.CMS.Extensions
{
    public static class HttpRequestBaseExtensions
    {
        public static ContentItemsInput GetContentItemsInput(this HttpRequestBase request)
        {
            string[] contentItemNames = request.Form.GetValues("content");

            if (contentItemNames == null)
                return null;

            List<ContentItemInput> contentItems = new List<ContentItemInput>();

            foreach (string contentItemName in contentItemNames)
            {
                string publishedDateStringValue = request.Form.Get(string.Format("publishedDate_{0}", contentItemName));
                DateTime? publishedDate = null;

                if (!string.IsNullOrEmpty(publishedDateStringValue))
                {
                    DateTime publishedDateValue;
                    if (DateTime.TryParse(publishedDateStringValue, out publishedDateValue))
                        publishedDate = publishedDateValue;
                }

                contentItems.Add(
                    new ContentItemInput(
                        contentItemName,
                        request.Form.Get(string.Format("displayName_{0}", contentItemName)),
                        request.Form.Get(string.Format("body_{0}", contentItemName)),
                        publishedDate
                        )
                    );
            }

            return new ContentItemsInput(contentItems);
        }
    }
}
