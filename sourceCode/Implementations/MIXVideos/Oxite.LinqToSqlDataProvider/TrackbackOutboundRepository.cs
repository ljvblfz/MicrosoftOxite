//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Oxite.Models;
using Oxite.Repositories;

namespace Oxite.LinqToSqlDataProvider
{
    public class TrackbackOutboundRepository : ITrackbackOutboundRepository
    {
        private OxiteLinqToSqlDataContext context;

        public TrackbackOutboundRepository(OxiteLinqToSqlDataContext context)
        {
            this.context = context;
        }

        #region ITrackbackRepository Members

        public void Save(IEnumerable<TrackbackOutbound> trackbacks)
        {
            if (trackbacks != null && trackbacks.Count() > 0)
            {
                context.oxite_TrackbackOutbounds.InsertAllOnSubmit(
                    trackbacks.Select(
                        tb =>
                            new oxite_TrackbackOutbound
                            {
                                TrackbackOutboundID = Guid.NewGuid(),
                                TargetUrl = tb.TargetUrl,
                                PostID = tb.PostID,
                                PostUrl = tb.PostUrl,
                                PostAreaTitle = tb.PostAreaTitle,
                                PostTitle = tb.PostTitle,
                                PostBody = tb.PostBody,
                                RemainingRetryCount = (byte)tb.RemainingRetryCount,
                                LastAttemptDate = null,
                                SentDate = null,
                                IsSending = false
                            }
                        )
                    );

                context.SubmitChanges();
            }
        }

        public void Remove(IEnumerable<TrackbackOutbound> trackbacks)
        {
            if (trackbacks != null && trackbacks.Count() > 0)
            {
                context.oxite_TrackbackOutbounds.DeleteAllOnSubmit(
                    context.oxite_TrackbackOutbounds.Where(
                        tb => trackbacks.Select(tbInner => tbInner.ID).Contains(tb.TrackbackOutboundID)
                        )
                    );

                context.SubmitChanges();
            }
        }

        public IEnumerable<TrackbackOutbound> GetUnsent(Guid postID)
        {
            return
                from tb in context.oxite_TrackbackOutbounds
                where tb.PostID == postID && !tb.SentDate.HasValue
                select new TrackbackOutbound
                {
                    ID = tb.TrackbackOutboundID,
                    TargetUrl = tb.TargetUrl,
                    PostID = tb.PostID,
                    PostUrl = tb.PostUrl,
                    PostAreaTitle = tb.PostAreaTitle,
                    PostTitle = tb.PostTitle,
                    PostBody = tb.PostBody,
                    RemainingRetryCount = tb.RemainingRetryCount,
                    Sent = tb.SentDate
                };
        }

        public IEnumerable<TrackbackOutbound> GetNext(bool executeOnAll, TimeSpan interval)
        {
            IEnumerable<TrackbackOutbound> trackbacks = Enumerable.Empty<TrackbackOutbound>();

            using (TransactionScope transaction = new TransactionScope())
            {
                IQueryable<oxite_TrackbackOutbound> query =
                    from t in context.oxite_TrackbackOutbounds
                    where !t.IsSending && t.RemainingRetryCount > 0 && (!t.LastAttemptDate.HasValue || t.LastAttemptDate.Value.Add(interval) <= DateTime.Now.ToUniversalTime())
                    select t;

                if (!executeOnAll)
                {
                    query = query.Take(1);
                }

                trackbacks = query.Select(
                    tb => new TrackbackOutbound()
                    {
                        ID = tb.TrackbackOutboundID,
                        TargetUrl = tb.TargetUrl,
                        PostTitle = tb.PostTitle,
                        PostAreaTitle = tb.PostAreaTitle,
                        PostBody = tb.PostBody,
                        PostUrl = tb.PostUrl,
                        RemainingRetryCount = tb.RemainingRetryCount
                    }
                    ).ToList();

                if (query.Count() > 0)
                    query.ToList().ForEach(tb => tb.IsSending = true);

                context.SubmitChanges();

                transaction.Complete();
            }

            return trackbacks;
        }

        public void Save(TrackbackOutbound trackback)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                oxite_TrackbackOutbound tb = (
                    from t in context.oxite_TrackbackOutbounds
                    where t.TrackbackOutboundID == trackback.ID
                    select t
                    ).First();

                tb.SentDate = trackback.Sent;
                tb.RemainingRetryCount = (byte)trackback.RemainingRetryCount;
                tb.IsSending = false;
                tb.LastAttemptDate = DateTime.Now.ToUniversalTime();

                context.SubmitChanges();

                transaction.Complete();
            }
        }

        #endregion
    }
}
