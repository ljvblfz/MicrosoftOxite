// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Linq;
using System.Web.Caching;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Repositories.SqlServer
{
    public class SqlServerSpeakerRepository : ISpeakerRepository
    {
        private readonly OxiteConferencesDataContext context;

        public SqlServerSpeakerRepository(OxiteConferencesDataContext context)
        {
            this.context = context;
        }

        static SqlServerSpeakerRepository()
        {
            _cache = System.Web.HttpContext.Current.Cache;   
        }

        #region ISpeakerRepository Members

        private static Cache _cache;

        public Speaker GetSpeaker(string name)
        {
            Speaker speaker = null;
            string cacheKey = "speaker:" + name;

            if (_cache != null)
            {
                speaker = _cache[cacheKey] as Speaker;
            }

            if (speaker != null)
                return speaker;

            System.Diagnostics.Debug.WriteLine("GetSpeaker: " + name);

            speaker = (
                from s in context.oxite_Conferences_Speakers
                where string.Compare(s.SpeakerName, name, true) == 0
                select projectSpeaker(s)
                ).FirstOrDefault();

            if (_cache != null)
            {
                _cache.Add(cacheKey, speaker, null, DateTime.Now.AddHours(1),
                           Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            return speaker;
        }

        public IQueryable<Speaker> GetSpeakers(EventAddress eventAddress, SpeakerFilterCriteria speakerFilterCriteria)
        {
            IQueryable<oxite_Conferences_Speaker> query = from s in context.oxite_Conferences_Speakers select s;

            if (!string.IsNullOrEmpty(speakerFilterCriteria.Term))
                query = query
                    .Where(
                    s =>
                    s.SpeakerName.Contains(speakerFilterCriteria.Term) ||
                    s.SpeakerFirstName.Contains(speakerFilterCriteria.Term) ||
                    s.SpeakerLastName.Contains(speakerFilterCriteria.Term) ||
                    s.Bio.Contains(speakerFilterCriteria.Term)
                    );

            if (eventAddress != null)
                query = query
                    .Where(s => s.oxite_Conferences_ScheduleItemSpeakerRelationships.Any(sis => string.Compare(sis.oxite_Conferences_ScheduleItem.oxite_Conferences_Event.EventName, eventAddress.EventName, true) == 0));

            return query.OrderBy(s => s.SpeakerLastName).Select(s => projectSpeaker(s));
        }

        #endregion

        #region Private Methods

        private static Speaker projectSpeaker( oxite_Conferences_Speaker s)
        {
            return new Speaker(
                s.SpeakerID,
                s.SpeakerName,
                s.SpeakerDisplayName,
                s.SpeakerFirstName,
                s.SpeakerLastName,
                s.Bio, s.LargeImage, s.SmallImage, s.Twitter,
                s.oxite_Conferences_ScheduleItemSpeakerRelationships.Select(
                    sis => projectScheduleItem(sis.oxite_Conferences_ScheduleItem)).ToList()
                );
        }

        private static ScheduleItem projectScheduleItem(oxite_Conferences_ScheduleItem si)
        {
            if (si == null)
            {
                return null;
            }

            if(si.PublishedDate.HasValue && si.PublishedDate.Value.ToUniversalTime() 
                > DateTime.UtcNow)
            {
                return null;
            }

            return new ScheduleItem(
                null,
                si.ScheduleItemID,
                si.Title,
                si.Body,
                si.Location,
                si.Code,
                si.Type,
                si.StartTime,
                si.EndTime,
                si.Slug,
                Enumerable.Empty<Speaker>(),
                si.oxite_Conferences_ScheduleItemTagRelationships.Select(sitr => new ScheduleItemTag(sitr.TagID, sitr.TagDisplayName)).ToList(),
                Enumerable.Empty<ScheduleItemComment>(),
                Enumerable.Empty<ScheduleItemUser>(),
                si.CreatedDate,
                si.ModifiedDate,
                si.PublishedDate,
                Enumerable.Empty<File>()
                );
        }

        #endregion
    }
}
