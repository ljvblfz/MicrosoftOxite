//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Plugins.Messages.Models;
using Oxite.Plugins.Messages.Repositories;
using Oxite.Plugins.Messages.Services;
using Oxite.Services;

namespace Oxite.Plugins.Messages
{
    public class Program
    {
        public void Register(IPluginContext context)
        {
            context.Plugin.DisplayName = "Oxite Messages";

            context.Plugin.Settings["ExecuteOnAll"] = true.ToString();
            context.Plugin.Settings["Interval"] = TimeSpan.FromMinutes(2).Ticks.ToString();
            context.Plugin.Settings["FromEmailAddress"] = "";
            context.Plugin.Settings["SmtpClient.Host"] = "";
            context.Plugin.Settings["SmtpClient.Port"] = "";
            context.Plugin.Settings["SmtpClient.UseDefaultCredentials"] = "";
            context.Plugin.Settings["SmtpClient.Credentials.Username"] = "";
            context.Plugin.Settings["SmtpClient.Credentials.Password"] = "";
            context.Plugin.Settings["SmtpClient.Credentials.Domain"] = "";
            context.Plugin.Settings["SmtpClient.EnableSsl"] = "";

            context.Container.RegisterType<OxiteDataContext>(new InjectionConstructor(new ResolvedParameter<string>("ApplicationServices")));
            context.Container.RegisterType<IMessageOutboundRepository, MessageOutboundRepository>();
            context.Container.RegisterType<IMessageOutboundService, MessageOutboundService>();

            context.Plugin.BackgroundServices.Add(typeof(BackgroundServices.SendMessagesBackgroundService));

            context.EventAdd("CommentAdded", s => CommentAdded(context, s));
        }

        private void CommentAdded(IPluginContext pluginContext, object state)
        {
            //TODO: (erikpo) Move this decision out of the plugin itself and back into the plumbing
            if (pluginContext.Plugin.Enabled)
            {
                ParentAndChild<Post, Comment> parentChild = (ParentAndChild<Post, Comment>)state;

                pluginContext.Container.Resolve<IMessageOutboundService>().Save(generateMessages(pluginContext, parentChild.Parent, parentChild.Child));
            }
        }

        private IEnumerable<MessageOutbound> generateMessages(IPluginContext pluginContext, Post post, Comment comment)
        {
            ILocalizationService localizationService = pluginContext.Container.Resolve<ILocalizationService>();
            IPostService postService = pluginContext.Container.Resolve<IPostService>();
            IEnumerable<PostSubscription> subscriptions = postService.GetSubscriptions(post);
            List<MessageOutbound> messages = new List<MessageOutbound>();
            Site site = pluginContext.Container.Resolve<Site>();
            AbsolutePathHelper absolutePathHelper = pluginContext.Container.Resolve<AbsolutePathHelper>();
            int retryCount = int.Parse(pluginContext.Plugin.Settings["RetryCount"]);

            foreach (PostSubscription subscription in subscriptions)
            {
                MessageOutbound message = new MessageOutbound
                {
                    ID = Guid.NewGuid(),
                    To = string.Format("{0} <{1}>", subscription.User.DisplayName, subscription.User.Email),
                    Subject = string.Format(getPhrase(localizationService, "Messages.Formats.ReplySubject", site.LanguageDefault, "RE: {0}"), post.Title),
                    Body = generateMessageBody(post, comment, site, localizationService, absolutePathHelper),
                    RemainingRetryCount = retryCount
                };

                messages.Add(message);
            }

            return messages;
        }

        private string generateMessageBody(Post post, Comment comment, Site site, ILocalizationService localizationService, AbsolutePathHelper absolutePathHelper)
        {
            string body = getPhrase(localizationService, "Messages.NewComment", site.LanguageDefault, getDefaultBody());
            //TODO: (erikpo) Change this to come from the user this message is going to if applicable
            double timeZoneOffset = site.TimeZoneOffset;

            body = body.Replace("{Site.Name}", site.DisplayName);
            body = body.Replace("{User.Name}", comment.Creator.DisplayName);
            body = body.Replace("{Post.Title}", post.Title);
            //TODO: (erikpo) Change the published date to be relative (e.g. 5 minutes ago)
            body = body.Replace("{Comment.Created}", comment.Created.Value.AddHours(timeZoneOffset).ToLongTimeString());
            body = body.Replace("{Comment.Body}", comment.Body);
            body = body.Replace("{Comment.Permalink}", absolutePathHelper.GetAbsolutePath(post, comment).Replace("%23", "#"));

            return body;
        }

        private static string getDefaultBody()
        {
            return
                "<h1>New Comment on {Site.Name}</h1>" +
                "<h2>{User.Name} commented on '{Post.Title}' at {Comment.Created}</h2>" +
                "<p>{Comment.Body}</p>" +
                "<a href=\"{Comment.Permalink}\">{Comment.Permalink}</a>";
        }

        private string getPhrase(ILocalizationService localizationService, string key, string language)
        {
            return getPhrase(localizationService, key, language, null);
        }

        private string getPhrase(ILocalizationService localizationService, string key, string language, string defaultValue)
        {
            Phrase phrase = localizationService.GetTranslations(language).Where(p => p.Key == key && p.Language == language).FirstOrDefault();

            if (phrase != null)
                return phrase.Value;

            if (defaultValue == null)
                return key;

            return defaultValue;
        }
    }
}
