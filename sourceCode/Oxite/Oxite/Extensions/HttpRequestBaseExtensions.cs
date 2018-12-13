//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Net;
using System.Web;

namespace Oxite.Extensions
{
    public static class HttpRequestBaseExtensions
    {
        public static IPAddress GetUserIPAddress(this HttpRequestBase request)
        {
            IPAddress address;

            if (!IPAddress.TryParse(request.UserHostAddress, out address))
            {
                address = null;
            }

            return address;
        }

        public static string GenerateAntiForgeryToken(this HttpRequestBase request, string key, string salt)
        {
            return (key + salt + request.UserAgent).ComputeHash();
        }

        public static DateTime? IfModifiedSince(this HttpRequestBase request)
        {
            string ifModifiedSinceValue = request.Headers["If-Modified-Since"];
            DateTime ifModifiedSince;

            if (!string.IsNullOrEmpty(ifModifiedSinceValue) && DateTime.TryParse(ifModifiedSinceValue, out ifModifiedSince))
                return ifModifiedSince;

            return null;
        }

        public static bool IsJQueryAjaxRequest(this HttpRequestBase request)
        {
            return request.Headers["X-Requested-With"] == "XMLHttpRequest"
                || request.Params["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}