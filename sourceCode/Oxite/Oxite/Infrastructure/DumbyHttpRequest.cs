//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web;

namespace Oxite.Infrastructure
{
    public class DummyHttpRequest : HttpRequestBase
    {
        private readonly Uri requestUrl;
        private readonly Uri hostUrl;
        public DummyHttpRequest(Uri requestUrl, Uri hostUrl)
        {
            this.requestUrl = requestUrl;
            this.hostUrl = hostUrl;
        }

        public override Uri Url
        {
            get
            {
                return requestUrl;
            }
        }

        public override string ApplicationPath
        {
            get
            {
                return hostUrl.AbsolutePath;
            }
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get
            {
                if (hostUrl.AbsolutePath.Length > 1)
                    return "~" + requestUrl.AbsolutePath.Remove(0, hostUrl.AbsolutePath.Length);

                return "~" + requestUrl.AbsolutePath;
            }
        }

        public override string PathInfo
        {
            get
            {
                return "";
            }
        }

        public override System.Collections.Specialized.NameValueCollection ServerVariables
        {
            get
            {
                return new System.Collections.Specialized.NameValueCollection();
            }
        }
    }
}