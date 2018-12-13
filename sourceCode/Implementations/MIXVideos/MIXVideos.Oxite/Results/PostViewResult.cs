//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web;
using System.Web.Mvc;

namespace MIXVideos.Oxite.Results
{
    public class PostViewResult : FileContentResult
    {
        public PostViewResult()
            : base(
                new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61, 0x01, 0x00, 0x01, 0x00, 0x91, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x21, 0xf9, 0x04, 0x09, 0x00, 0x00, 0x00, 0x00, 0x2c, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x08, 0x04, 0x00, 0x01, 0x04, 0x04, 0x00, 0x3b, 0x00 },
                "image/gif"
            )
        {
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            response.AppendHeader("Content-Length", FileContents.Length.ToString());
            response.Cache.SetLastModified(DateTime.Now);
            response.Cache.SetExpires(DateTime.Now.AddHours(12));
            response.Cache.SetCacheability(HttpCacheability.Private);
            response.StatusCode = 200;

            base.WriteFile(response);
        }
    }
}
