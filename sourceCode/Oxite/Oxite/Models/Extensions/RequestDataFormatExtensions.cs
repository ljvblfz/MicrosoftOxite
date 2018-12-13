//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Models.Extensions
{
    public static class RequestDataFormatExtensions
    {
        public static bool IsFeed(this RequestDataFormat dataFormat)
        {
            return dataFormat == RequestDataFormat.RSS || dataFormat == RequestDataFormat.ATOM;
        }
    }
}
