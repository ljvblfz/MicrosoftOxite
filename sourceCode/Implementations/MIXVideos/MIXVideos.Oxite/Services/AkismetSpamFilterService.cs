using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oxite.Services;
using System.Net;
using Oxite.Infrastructure;
using System.IO;
using Oxite.Models;
using System.Web;

namespace MIXVideos.Oxite.Services
{
    public class AkismetSpamFilterService : ISpamFilterService
    {
        private AppSettingsHelper settings;
        private Site site;
        private AbsolutePathHelper pathHelper;

        private List<KeyValuePair<string, Func<SpamFilterContext, string>>> parameters= new List<KeyValuePair<string, Func<SpamFilterContext, string>>>();

        public AkismetSpamFilterService(AppSettingsHelper settings, Site site, AbsolutePathHelper pathHelper)
        {
            this.settings = settings;
            this.site = site;
            this.pathHelper = pathHelper;
            BuildParameters();
        }

        private void BuildParameters()
        {
            AddParameter("blog", c => this.site.Host.ToString());
            AddParameter("user_ip", c => c.RequestContext.HttpContext.Request.UserHostAddress);
            AddParameter("user_agent", c => c.Comment.CreatorUserAgent);
            AddParameter("referrer", c => c.RequestContext.HttpContext.Request.UrlReferrer.ToString());
            AddParameter("permalink", c => pathHelper.GetAbsolutePath(c.PostAddress));
            AddParameter("comment_type", c => "comment");
            AddParameter("comment_author", c => c.AnonymousUser.Name);
            AddParameter("comment_author_email", c => c.AnonymousUser.Email);
            AddParameter("comment_author_url", c => c.AnonymousUser.Url);
            AddParameter("comment_content", c => c.Comment.Body);
        }

        private void AddParameter(string name, Func<SpamFilterContext, string> value)
        {
            parameters.Add(new KeyValuePair<string, Func<SpamFilterContext, string>>(name, value));
        }

        #region ISpamFilterService Members

        public bool IsSpam(SpamFilterContext context)
        {
            string requestUrl = settings.GetString("AkismetUrl");
            if (string.IsNullOrEmpty(requestUrl))
                return false;

            HttpWebRequest req = WebRequest.Create(requestUrl) as HttpWebRequest;

            req.Method = "POST";
            
            string parameterString = parameters.Aggregate<KeyValuePair<string,Func<SpamFilterContext,string>>,string>("", (prev, curr) => string.IsNullOrEmpty(prev) ?
                string.Format("{0}={1}", curr.Key, context.RequestContext.HttpContext.Server.UrlEncode(curr.Value(context))) :
                string.Format("{0}&{1}={2}", prev, curr.Key, context.RequestContext.HttpContext.Server.UrlEncode(curr.Value(context))));

            byte[] parameterStringBytes = new ASCIIEncoding().GetBytes(parameterString);

            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = parameterStringBytes.Length;
            req.UserAgent = "Oxite/1.0";

            StreamWriter writer = new StreamWriter(req.GetRequestStream());
            writer.Write(parameterString);

            writer.Flush();
            writer.Close();
            try
            {
                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                string responseCode = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                return bool.Parse(responseCode);
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
