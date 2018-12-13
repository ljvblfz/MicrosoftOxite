//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Models.Extensions;
using Oxite.Plugins.Trackbacks.Models;
using Oxite.Plugins.Trackbacks.Repositories;
using Oxite.Plugins.Trackbacks.Services;

namespace Oxite.Plugins.Trackbacks
{
    public class Program
    {
        public void Register(IPluginContext context)
        {
            context.Plugin.DisplayName = "Oxite Trackbacks";

            context.Plugin.Settings["ExecuteOnAll"] = true.ToString();
            context.Plugin.Settings["Interval"] = TimeSpan.FromMinutes(10).Ticks.ToString();
            context.Plugin.Settings["RetryCount"] = 28.ToString();

            context.Container.RegisterType<OxiteDataContext>(new InjectionConstructor(new ResolvedParameter<string>("ApplicationServices")));
            context.Container.RegisterType<ITrackbackOutboundRepository, TrackbackOutboundRepository>();
            context.Container.RegisterType<ITrackbackOutboundService, TrackbackOutboundService>();

            context.Plugin.BackgroundServices.Add(typeof(BackgroundServices.SendTrackbacksBackgroundService));

            context.EventAdd("PostSaved", s => PostSaved(context, s));
        }

        private void PostSaved(IPluginContext context, object state)
        {
            //TODO: (erikpo) Move this decision out of the plugin itself and back into the plumbing
            if (context.Plugin.Enabled)
            {
                ITrackbackOutboundService trackbackOutboundService = context.Container.Resolve<ITrackbackOutboundService>();
                Post post = (Post)state;

                if (post.Published.HasValue)
                {
                    string postUrl = context.Container.Resolve<AbsolutePathHelper>().GetAbsolutePath(post);
                    IEnumerable<TrackbackOutbound> trackbacksToAdd = extractTrackbacks(context, post, postUrl, post.Area.DisplayName);
                    IEnumerable<TrackbackOutbound> unsentTrackbacks = trackbackOutboundService.GetUnsent(post.ID);
                    IEnumerable<TrackbackOutbound> trackbacksToRemove = trackbacksToAdd.Where(tb => !unsentTrackbacks.Contains(tb) && !tb.Sent.HasValue);

                    trackbackOutboundService.Remove(trackbacksToRemove);
                    trackbackOutboundService.Save(trackbacksToAdd);
                }
                else
                {
                    //TODO: (erikpo) Remove all outbound trackbacks
                }
            }
        }

        private static IEnumerable<TrackbackOutbound> extractTrackbacks(IPluginContext context, Post post, string postUrl, string postAreaTitle)
        {
            //INFO: (erikpo) Trackback spec: http://www.sixapart.com/pronet/docs/trackback_spec
            Regex r =
                new Regex(
                    @"(?<HTML><a[^>]*href\s*=\s*[\""\']?(?<HRef>[^""'>\s]*)[\""\']?[^>]*>(?<Title>[^<]+|.*?)?</a>)",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled
                    );
            MatchCollection m = r.Matches(post.Body);
            List<TrackbackOutbound> trackbacks = Enumerable.Empty<TrackbackOutbound>().ToList();

            if (m.Count > 0)
            {
                int retryCount = int.Parse(context.Plugin.Settings["RetryCount"]);

                trackbacks = new List<TrackbackOutbound>(m.Count);

                foreach (Match match in m)
                {
                    trackbacks.Add(
                        new TrackbackOutbound
                        {
                            TargetUrl = match.Groups["HRef"].Value,
                            PostID = post.ID,
                            PostTitle = post.Title,
                            PostBody = post.GetBodyShort(),
                            PostAreaTitle = postAreaTitle,
                            PostUrl = postUrl,
                            RemainingRetryCount = retryCount
                        }
                        );
                }
            }

            return trackbacks;
        }
    }
}
