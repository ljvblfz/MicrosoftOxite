// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Comments.Models;
using Oxite.Modules.Comments.Repositories;
using Oxite.Modules.Conferences.Extensions;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Repositories;
using Oxite.Modules.Messages.Models;
using Oxite.Modules.Messages.Repositories;
using Oxite.Plugins.Extensions;
using Oxite.Plugins.Models;
using Oxite.Repositories;
using Oxite.Validation;

namespace Oxite.Modules.Conferences.Services
{
    public class ConferencesCommentService : IConferencesCommentService
    {
        private readonly IConferencesCommentRepository conferencesCommentRepository;
        private readonly ICommentRepository commentRepository;
        private readonly IScheduleItemRepository scheduleItemRepository;
        private readonly IMessageOutboundRepository messageOutboundRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly ILocalizationRepository localizationRepository;
        private readonly UrlHelper urlHelper;
        private readonly IValidationService validator;
        private readonly IPluginEngine pluginEngine;
        private readonly IOxiteCacheModule cache;
        private readonly OxiteContext context;

        public ConferencesCommentService(IConferencesCommentRepository conferencesCommentRepository, ICommentRepository commentRepository, IScheduleItemRepository scheduleItemRepository, IMessageOutboundRepository messageOutboundRepository, ILanguageRepository languageRepository, ILocalizationRepository localizationRepository, UrlHelper urlHelper, IValidationService validator, IPluginEngine pluginEngine, IModulesLoaded modules, OxiteContext context)
        {
            this.conferencesCommentRepository = conferencesCommentRepository;
            this.commentRepository = commentRepository;
            this.scheduleItemRepository = scheduleItemRepository;
            this.messageOutboundRepository = messageOutboundRepository;
            this.languageRepository = languageRepository;
            this.localizationRepository = localizationRepository;
            this.urlHelper = urlHelper;
            this.validator = validator;
            this.pluginEngine = pluginEngine;
            this.cache = modules.GetModules<IOxiteCacheModule>().Reverse().First();
            this.context = context;
        }

        #region IConferencesCommentService Members

        public ValidationStateDictionary ValidateCommentInput(CommentInput commentInput)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();

            validationState.Add(typeof(CommentInput), validator.Validate(commentInput));

            return validationState;
        }

        public ModelResult<ScheduleItemComment> AddComment(ScheduleItem scheduleItem, CommentInput commentInput)
        {
            CommentIn commentIn = new CommentIn(commentInput);

            pluginEngine.ExecuteAll("ProcessInputOfComment", new { context, comment = commentIn });
            commentInput = commentIn.ToCommentInput();

            commentInput = pluginEngine.Process<CommentIn>("ProcessInputOfCommentOnAdd", new CommentIn(commentInput)).ToCommentInput();

            if (pluginEngine.AnyTrue("IsCommentSpam", new { context, comment = commentIn }))
                return new ModelResult<ScheduleItemComment>(new ValidationStateDictionary(typeof(CommentInput), new ValidationState(new[] { new ValidationError("Comment.IsSpam", commentInput, "The supplied comment was considered to be spam and was not added") })));

            ValidationStateDictionary validationState = ValidateCommentInput(commentInput);

            if (!validationState.IsValid) return new ModelResult<ScheduleItemComment>(validationState);

            EntityState commentState;

            try
            {
                commentState = context.User.IsAuthenticated ? EntityState.Normal : (EntityState)Enum.Parse(typeof(EntityState), context.Site.CommentStateDefault);
            }
            catch
            {
                commentState = EntityState.PendingApproval;
            }

            //TODO: (erikpo) Replace with some logic to set the language from the user's browser or from a dropdown list
            Language language = languageRepository.GetLanguage(context.Site.LanguageDefault ?? "en");
            ScheduleItemComment comment;

            using (TransactionScope transaction = new TransactionScope())
            {
                string commentSlug = generateUniqueCommentSlug(scheduleItem);

                comment = commentInput.ToComment(scheduleItem.Event.Name, scheduleItem.Slug, context.User.Cast<User>(), context.HttpContext.Request.GetUserIPAddress().ToLong(), context.HttpContext.Request.UserAgent, language, commentSlug, commentState);

                comment = conferencesCommentRepository.Save(comment);

                if (comment.State == EntityState.Normal)
                    invalidateCachedCommentDependencies(comment);

                transaction.Complete();
            }

            //TODO: (erikpo) The following calls to setup the subscription and send out emails for those subscribed needs to happen in the transaction (but can't currently because of issues with them being in different repositories

            //TODO: (erikpo) Move into a module
            if (commentInput.Subscribe)
            {
                if (context.User.IsAuthenticated)
                    scheduleItemRepository.AddSubscription(comment.ScheduleItem, comment.CreatorUserID);
                else
                    scheduleItemRepository.AddSubscription(comment);
            }

            //TODO: (erikpo) Move into a module
            messageOutboundRepository.Save(generateMessages(comment.ScheduleItem, comment));

            ScheduleItemSmallReadOnly scheduleItemProxy = new ScheduleItemSmallReadOnly(comment.ScheduleItem);
            CommentReadOnly commentProxy = new CommentReadOnly(comment, "");

            pluginEngine.ExecuteAll("CommentAdded", new { context, parent = scheduleItemProxy, comment = commentProxy });

            if (comment.State == EntityState.Normal)
                pluginEngine.ExecuteAll("CommentApproved", new { context, parent = scheduleItemProxy, comment = commentProxy });

            return new ModelResult<ScheduleItemComment>(comment, validationState);
        }

        #endregion

        #region Private Methods

        private string generateUniqueCommentSlug(ScheduleItem scheduleItem)
        {
            string commentSlug = null;
            bool isUnique = false;

            while (!isUnique)
            {
                commentSlug = Guid.NewGuid().ToString("N").Substring(0, 5);

                ScheduleItemComment foundComment = getComment(scheduleItem, commentSlug);

                isUnique = foundComment == null;
            }

            return commentSlug;
        }

        private ScheduleItemComment getComment(ScheduleItem scheduleItem, string commentSlug)
        {
            return getComment(conferencesCommentRepository.GetComment(scheduleItem.Event.Name, scheduleItem.Slug, commentSlug));
        }

        private ScheduleItemComment getComment(ScheduleItemCommentShell scheduleItemCommentShell)
        {
            if (scheduleItemCommentShell == null) return null;

            return new ScheduleItemComment(scheduleItemCommentShell.ScheduleItem, commentRepository.GetComment(scheduleItemCommentShell.CommentID), scheduleItemCommentShell.CommentSlug);
        }

        private void invalidateCachedCommentDependencies(ScheduleItemComment comment)
        {
            if (comment.Parent != null)
                cache.InvalidateItem(new ScheduleItemComment(comment.Parent.ID));

            cache.InvalidateItem(new ScheduleItem(comment.ScheduleItem.ID));
        }

        private IEnumerable<MessageOutbound> generateMessages(ScheduleItemSmall post, ScheduleItemComment comment)
        {
            IEnumerable<ScheduleItemSubscription> subscriptions = scheduleItemRepository.GetSubscriptions(post.EventName, post.Slug);
            List<MessageOutbound> messages = new List<MessageOutbound>();
            //TODO: (erikpo) Once the plugin model is done, get this from the plugin
            int retryCount = 4;

            foreach (ScheduleItemSubscription subscription in subscriptions)
            {
                string userName = subscription.UserName;

                MessageOutbound message = new MessageOutbound
                {
                    ID = Guid.NewGuid(),
                    To = !string.IsNullOrEmpty(userName) ? string.Format("{0} <{1}>", userName, subscription.UserEmail) : subscription.UserEmail,
                    Subject = string.Format(getPhrase("Messages.Formats.ReplySubject", context.Site.LanguageDefault, "RE: {0}"), post.Title),
                    Body = generateMessageBody(post, comment, context.Site),
                    RemainingRetryCount = retryCount
                };

                messages.Add(message);
            }

            return messages;
        }

        private string generateMessageBody(ScheduleItemSmall post, ScheduleItemComment comment, Site site)
        {
            string body = getPhrase("Messages.NewComment", site.LanguageDefault, getDefaultBody());
            //TODO: (erikpo) Change this to come from the user this message is going to if applicable
            double timeZoneOffset = site.TimeZoneOffset;

            body = body.Replace("{Site.Name}", site.DisplayName);
            body = body.Replace("{User.Name}", comment.CreatorName);
            body = body.Replace("{Post.Title}", post.Title);
            //TODO: (erikpo) Change the published date to be relative (e.g. 5 minutes ago)
            body = body.Replace("{Comment.Created}", comment.Created.AddHours(timeZoneOffset).ToLongTimeString());
            body = body.Replace("{Comment.Body}", comment.Body);
            body = body.Replace("{Comment.Permalink}", urlHelper.AbsolutePath(urlHelper.Comment(comment)));

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

        private string getPhrase(string key, string language)
        {
            return getPhrase(key, language, null);
        }

        private string getPhrase(string key, string language, string defaultValue)
        {
            Phrase phrase = localizationRepository.GetPhrases().Where(p => p.Key == key && p.Language == language).FirstOrDefault();

            if (phrase != null)
                return phrase.Value;

            if (defaultValue == null)
                return key;

            return defaultValue;
        }

        #endregion
    }
}
