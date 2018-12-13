// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Modules.Plugins.Extensions;
using Oxite.Modules.Plugins.Models;
using Oxite.Plugins.Attributes;
using Oxite.Plugins.Models;
using Oxite.Plugins.Validators;
using Oxite.Validation;

namespace OxiteSite.Plugins.Oxite.SpamControl
{
    [DisplayName("Spam Control")]
    [Description("Checks incomming comments against an Akismet API compatible service and marks comments as spam as approprate. Also allows comments to be explicitly marked as spam or ham from where the comment is displayed.")]
    [Authors("Nathan Heskew", "Erik Porter")]
    [AuthorUrls("http://nathan.heskew.com", "http://erikporter.com")]
    [IconLarge("ham_32x32.png")]
    [Category("AntiSpam")]
    [Tags("AntiSpam", "Akismet", "TypePad")]
    [Version(1, 0)]
    [ApiKeyValidation]
    public class SpamControlPlugin
    {
        public const string Version = "Oxite | SpamControl/1.0";
        public const string AkismetApiVersion = "1.1";

        [Required, Order(1), Group("API Settings", 1), Appearance(Width = "56%")]
        [LabelText("API key for the Akismet (http://akismet.com/personal/) or TypePad AntiSpam (http://antispam.typepad.com/info/get-api-key.html) service.")]
        public string ApiKey { get; set; }

        //[Required, Order(2), Group("API Settings", 1), Appearance(Width = "56%")]
        //[LabelText("API endpoint (currently Akismet == rest.akismet.com and TypePad AntiSpam == api.antispam.typepad.com)")]
        public string ApiEndpoint { get; set; }
        public object ApiEndpointDefinition 
        {
            get
            {
                return new
                {
                    LabelText = "API endpoint (currently Akismet == rest.akismet.com and TypePad AntiSpam == api.antispam.typepad.com)",
                    Required = true,
                    Order = 2,
                    Group = new PropertyGroup("API Settings", 1),
                    Appearance = new { Width = "56%" },
                    StringValidation = new
                    {
                        RegularExpressionMatcher = new Regex(@"^(?:(?:[a-z0-9!#$%&'*+\-/=?^_`{|}~]+\.)+[a-z]+|(?:\d{1,3}\.){3}\d{1,3})$")
                    }
                };
            }
        }

        public bool IsCommentSpam(OxiteContext context, CommentIn comment)
        {
            if (context.User.IsAuthenticated) return false;

            SpamCandidate spamCandidate = new SpamCandidate
                {
                    blog = null,
                    comment_author = comment.CreatorName,
                    comment_author_email = comment.CreatorEmail,
                    comment_author_url = comment.CreatorUrl,
                    comment_content = comment.Body,
                    comment_type = "comment",
                    permalink = null,
                    referrer = context.HttpContext.Request.UrlReferrer,
                    user_agent = context.HttpContext.Request.UserAgent,
                    user_ip = context.HttpContext.Request.UserHostAddress
                };

            HttpWebRequest validationRequest = context.GeneratePostRequest(
                GetApiMethodUri("comment-check"),
                Version,
                spamCandidate.ToQueryString()
            );

            try
            {
                HttpWebResponse validationResponse = validationRequest.GetResponse() as HttpWebResponse;
                string responseCode = new StreamReader(validationResponse.GetResponseStream()).ReadToEnd();
                return bool.Parse(responseCode);
            }
            catch
            {
                return false;
            }
        }

        public void RegisterStyles(StyleList styles)
        {
            styles.Add("base.css", "ListForAdmin");
            styles.Add("base.css", "Item");
        }

        public void RegisterScripts(ScriptList scripts)
        {
            scripts.Add("base.js", "ListForAdmin");
            scripts.Add("base.js", "Item");
        }

        public void RegisterTemplates(TemplateList templates)
        {
            //templates.Add("MarkAsSpam", "ul.comments li div.flags", SelectorType.AppendTo, "ListForAdmin", "Comment");
            //templates.Add("MarkAsSpam", "ul.comments li div.flags", SelectorType.AppendTo, "Item", "Comment");
        }

        public void RegisterRoutes(RouteList routes)
        {
            routes.Add("MarkAsSpam", "Admin/Plugins/SpamControl/MarkAsSpam", new { role = "Admin" });
        }

        public void MarkAsSpam(OxiteContext context, FormCollection form)
        {
            SpamCandidate spamCandidate = new SpamCandidate
            {
                blog = null,
                comment_author = form["commentCreatorName"],
                comment_author_email = form["commentCreatorEmail"],
                comment_author_url = form["commentCreatorUrl"],
                comment_content = form["commentBody"],
                comment_type = "comment",
                permalink = null,
                referrer = null,
                user_agent = form["commentCreatorUserAgent"],
                user_ip = form["commentCreatorIP"]
            };

            context.GeneratePostRequest(GetApiMethodUri("submit-spam"), Version, spamCandidate.ToQueryString()).GetResponse();
        }

        public void MarkAsHam(OxiteContext context, SpamCandidate spamCandidate)
        {
            context.GeneratePostRequest(GetApiMethodUri("submit-ham"), Version, spamCandidate.ToQueryString()).GetResponse();
        }

        public Uri GetApiKeyVerificationUri(string methodPath)
        {
            if (!string.IsNullOrEmpty(ApiEndpoint))
                return new Uri(string.Format("http://{0}/{2}/{1}", ApiEndpoint, methodPath, AkismetApiVersion));

            return null;
        }

        public Uri GetApiMethodUri(string methodPath)
        {
            if (!string.IsNullOrEmpty(ApiKey) && !string.IsNullOrEmpty(ApiEndpoint))
                return new Uri(string.Format("http://{0}.{1}/{3}/{2}", ApiKey, ApiEndpoint, methodPath, AkismetApiVersion));

            return null;
        }
    }

    public class SpamCandidate
    {
        public Uri blog { get; set; }
        public string user_ip { get; set; }
        public string user_agent { get; set; }
        public Uri referrer { get; set; }
        public Uri permalink { get; set; }
        public string comment_type { get; set; }
        public string comment_author { get; set; }
        public string comment_author_email { get; set; }
        public string comment_author_url { get; set; }
        public string comment_content { get; set; }
    }

    public static class SpamControlPluginExtensions
    {
        public static string ToQueryString(this object obj)
        {
            StringBuilder queryStringBuilder = new StringBuilder();

            foreach (PropertyInfo propertyInfo in (obj.GetType()).GetProperties())
                queryStringBuilder.AppendFormat("{0}={1}&", propertyInfo.Name, HttpUtility.UrlEncodeUnicode((propertyInfo.GetValue(obj, null) ?? "").ToString()));

            return queryStringBuilder.Remove(queryStringBuilder.Length - 1, 1).ToString();
        }
    }

    public class ApiKeyValidator : IPluginValidator
    {
        public IEnumerable<ValidationError> Validate(IDictionary<string, KeyValuePair<Type, object>> pluginProperties)
        {
            //todo: (nheskew) skip key validation until we can get at the blog URI
            //note: (nheskew) error messages weren't showing up for me. don't know if it's my box but errors returned were just ignored on save
            return new List<ValidationError>();
            SpamControlPlugin spamControlPlugin = new SpamControlPlugin
            {
                ApiEndpoint = pluginProperties.ContainsKey("ApiEndpoint") ? pluginProperties["ApiEndpoint"].Value as string : null,
                ApiKey = pluginProperties.ContainsKey("ApiKey") ? pluginProperties["ApiKey"].Value as string : null
            };

            ApiKeyValidationResponse apiKeyValidationResponse = runApiKeyValidation(spamControlPlugin.GetApiKeyVerificationUri("verify-key"), new ApiKeyCandidate { blog = null, key = spamControlPlugin.ApiKey });

            List<ValidationError> errors = new List<ValidationError>();
            if (apiKeyValidationResponse == null)
            {
                //todo: (nheskew) add more debug info to the error message
                errors.Add(new ValidationError("ApiKey", spamControlPlugin, "Plugins.Errors.ApiKeyValidator.UnableToValidate", "en", "The API key validation completely failed."));
                return errors;
            }

            if (!apiKeyValidationResponse.IsValid)
                errors.Add(new ValidationError("ApiKey", spamControlPlugin, "Plugins.Errors.ApiKeyValidator.InvalidKey", "en", "API key valididation failed. Message: {1}", apiKeyValidationResponse.Status, apiKeyValidationResponse.Message));

            return errors;
        }

        private ApiKeyValidationResponse runApiKeyValidation(Uri keyVerificationUri, ApiKeyCandidate apiKeyCandidate)
        {
            try
            {
                HttpWebRequest validationRequest = formRequest(keyVerificationUri, SpamControlPlugin.Version, apiKeyCandidate.ToQueryString());
                return new ApiKeyValidationResponse(validationRequest.GetResponse() as HttpWebResponse);
            }
            catch
            {
                return null;
            }
        }

        //todo: (nheskew) make use of GetPostResponse off context - somehow
        private static HttpWebRequest formRequest(Uri uri, string userAgent, string payload)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);

            webRequest.Method = "POST";
            webRequest.UserAgent = userAgent;
            webRequest.ContentType = "application/x-www-form-urlencoded";

            webRequest.ContentLength = payload.Length;

            StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
            streamWriter.Write(payload);
            streamWriter.Flush();
            streamWriter.Close();

            return webRequest;
        }
    }

    public class ApiKeyValidationResponse
    {
        public ApiKeyValidationResponse(HttpWebResponse validationResponse)
        {
            IsValid = "valid" == new StreamReader(validationResponse.GetResponseStream()).ReadToEnd();
            Status = validationResponse.StatusCode;
            Message = validationResponse.GetResponseHeader("X-akismet-debug-help");
        }

        public readonly bool IsValid;
        public readonly HttpStatusCode Status;
        public readonly string Message;
    }

    public class ApiKeyValidationAttribute : DefinitionAttribute
    {
        public ApiKeyValidationAttribute() : base(new ApiKeyValidator()) { }
    }

    public class ApiKeyCandidate
    {
        public Uri blog { get; set; }
        public string key { get; set; }
    }
}
