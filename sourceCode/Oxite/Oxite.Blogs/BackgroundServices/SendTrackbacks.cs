//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Oxite.Configuration;
using Oxite.Configuration.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Services;

namespace Oxite.Modules.Blogs.BackgroundServices
{
    public class SendTrackbacks : IBackgroundService
    {
        private readonly ITrackbackOutboundService trackbackOutboundService;

        public SendTrackbacks(ITrackbackOutboundService trackbackOutboundService)
        {
            this.trackbackOutboundService = trackbackOutboundService;
        }

        #region Methods

        public void Initialize(OxiteModuleConfigurationElement moduleConfiguration)
        {
        }

        public void Run(OxiteModuleConfigurationElement moduleConfiguration)
        {
            AppSettingsHelper settings = new AppSettingsHelper(moduleConfiguration.Settings.ToNameValueCollection());
            //TODO: (erikpo) Refactor GetNext call to not need an interval if possible
            TimeSpan interval = settings.GetTimeSpan("SendTrackbacks.Interval", TimeSpan.FromMinutes(1));
            int blockSize = settings.GetInt32("SendTrackbacks.BlockSize", 1);

            foreach (TrackbackOutbound trackback in trackbackOutboundService.GetNext(interval, blockSize))
            {
                try
                {
                    sendTrackback(trackback);

                    trackback.MarkAsCompleted();
                }
                catch
                {
                    trackback.MarkAsFailed();
                }
                finally
                {
                    trackbackOutboundService.Save(trackback);
                }
            }
        }

        public void Unload(OxiteModuleConfigurationElement moduleConfiguration)
        {
        }

        #endregion

        #region Private Methods

        private static void sendTrackback(TrackbackOutbound trackback)
        {
            WebClient wc = new WebClient();
            string pageText = wc.DownloadString(trackback.TargetUrl);
            string trackBackItem = getTrackBackText(pageText, trackback.TargetUrl, trackback.PostUrl);

            if (trackBackItem != null)
            {
                if (!trackBackItem.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
                {
                    trackBackItem = "http://" + trackBackItem;
                }

                sendPing(
                    trackBackItem,
                    string.Format(
                        "title={0}&url={1}&blog_name={2}&excerpt={3}",
                        HttpUtility.HtmlEncode(trackback.PostTitle),
                        HttpUtility.HtmlEncode(trackback.PostUrl),
                        HttpUtility.HtmlEncode(trackback.PostBlogTitle),
                        HttpUtility.HtmlEncode(trackback.PostBody)
                        )
                    );
            }
        }

        private static string getTrackBackText(string pageText, string url, string postUrl)
        {
            if (!Regex.IsMatch(pageText, postUrl, RegexOptions.IgnoreCase | RegexOptions.Singleline))
            {
                const string sPattern = @"<rdf:\w+\s[^>]*?>(</rdf:rdf>)?";
                Regex r = new Regex(sPattern, RegexOptions.IgnoreCase);

                for (Match m = r.Match(pageText); m.Success; m = m.NextMatch())
                {
                    if (m.Groups.ToString().Length > 0)
                    {
                        string text = m.Groups[0].ToString();

                        if (text.IndexOf(url, StringComparison.OrdinalIgnoreCase) > 0)
                        {
                            Regex reg = new Regex("trackback:ping=\"([^\"]+)\"", RegexOptions.IgnoreCase);
                            Match m2 = reg.Match(text);

                            if (m2.Success)
                                return m2.Result("$1");

                            return text;
                        }
                    }
                }
            }

            return null;
        }

        private static void sendPing(string trackBackItem, string parameters)
        {
            StreamWriter myWriter = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(trackBackItem);

            request.Method = "POST";
            request.ContentLength = parameters.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = false;

            //TODO: (erikpo) Log the response or error returned

            try
            {
                myWriter = new StreamWriter(request.GetRequestStream());
                myWriter.Write(parameters);

                myWriter.Flush();

                WebResponse response = request.GetResponse();
            }
            catch { }
            finally
            {
                if (myWriter != null) myWriter.Close();
            }
        }

        #endregion
    }
}
