//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web;

namespace Oxite.Infrastructure
{
    public class DummyHttpContext : HttpContextBase
    {
        private readonly Uri requestUrl;
        private readonly Uri hostUrl;

        public DummyHttpContext(Uri requestUrl, Uri hostUrl)
        {
            this.requestUrl = requestUrl;
            this.hostUrl = hostUrl;
        }

        public override HttpRequestBase Request
        {
            get
            {
                return new DummyHttpRequest(requestUrl, hostUrl);
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return new DummyHttpResponse();
            }
        }
    }
}