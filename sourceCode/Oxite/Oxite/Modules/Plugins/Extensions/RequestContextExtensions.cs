// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.IO;
using System.Net;
using System.Text;
using Oxite.Infrastructure;

namespace Oxite.Modules.Plugins.Extensions
{
    public static class OxiteContextExtensions
    {
        public static HttpWebRequest GeneratePostRequest(this OxiteContext oxiteContext, Uri uri, string userAgent, string payload)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);

            webRequest.Method = "POST";
            webRequest.UserAgent = !string.IsNullOrEmpty(userAgent) ? userAgent : "Oxite"; //todo: (nheskew) need better UA string
            webRequest.ContentType = "application/x-www-form-urlencoded";

            byte[] payloadBytes = new ASCIIEncoding().GetBytes(payload);
            webRequest.ContentLength = payloadBytes.Length;

            StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
            streamWriter.Write(payload);
            streamWriter.Flush();
            streamWriter.Close();

            return webRequest;
        }
    }
}
