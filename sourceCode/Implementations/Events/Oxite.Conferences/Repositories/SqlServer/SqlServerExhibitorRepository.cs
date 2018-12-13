// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Extensions;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Repositories.SqlServer
{
    public class SqlServerExhibitorRepository : IExhibitorRepository
    {
        private readonly OxiteConferencesDataContext context;

        public SqlServerExhibitorRepository(OxiteConferencesDataContext context)
        {
            this.context = context;
        }

        public IQueryable<Exhibitor> GetExhibitors(EventAddress eventAddress, ExhibitorFilterCriteria exhibitorFilterCriteria)
        {
            IQueryable<oxite_Conferences_Exhibitor> query = 
                from e in context.oxite_Conferences_Exhibitors 
                orderby e.Name
                select e;

            if (exhibitorFilterCriteria != null && exhibitorFilterCriteria.Event != null)
            {
                query = query.Where(
                    e => e.EventID == exhibitorFilterCriteria.Event.ID
                    );
            }

            if (exhibitorFilterCriteria != null && !String.IsNullOrEmpty(exhibitorFilterCriteria.Term))
            {
                var slug = exhibitorFilterCriteria.Term.CleanSlug();
                var lookup = query.ToList().ToDictionary(e => e.Name.CleanSlug(), e => e.Name);

                var name = lookup.ContainsKey(slug) ? lookup[slug] : null;

                if(name != null)
                {
                    query = query.Where(
                        e => e.Name.Equals(name)
                    );    
                }
                else
                {
                    // 404
                    return new List<Exhibitor>(0).AsQueryable();
                }
            }
            
            if (exhibitorFilterCriteria != null && exhibitorFilterCriteria.ParticipantLevels.Count() > 0)
            {
                var levels = exhibitorFilterCriteria.ParticipantLevels.ToArray();

                query = query.Where(e => levels.Contains(e.ParticipantLevel));
            }

            var list = query.OrderBy(e => e.ParticipantLevel).Select(e => projectExhibitor(e));

            return list;
        }

        public Exhibitor SaveExhibitor(EventAddress eventAddress, Exhibitor exhibitor)
        {
            oxite_Conferences_Exhibitor exhibitorToSave = null;

            if (exhibitor.ID != Guid.Empty)
                exhibitorToSave = context
                    .oxite_Conferences_Exhibitors
                    .FirstOrDefault(c => c.ExhibitorID == exhibitor.ID);

            if (exhibitorToSave == null)
            {
                exhibitorToSave = new oxite_Conferences_Exhibitor
                                      {
                                          EventID = exhibitor.EventID,
                                          Name = exhibitor.Name,
                                          ParticipantLevel = "Exhibitor",
                                          CreatedDate = DateTime.UtcNow,
                                          ModifiedDate = DateTime.UtcNow
                                      };

                context.oxite_Conferences_Exhibitors.InsertOnSubmit(exhibitorToSave);
            }
            else
            {
                exhibitorToSave.ModifiedDate = DateTime.UtcNow;
            }

            exhibitorToSave.Name = exhibitor.Name;
            exhibitorToSave.Description = exhibitor.Description;
            exhibitorToSave.SiteUrl = exhibitor.SiteUrl;
            exhibitorToSave.LogoUrl = exhibitor.LogoUrl;
            exhibitorToSave.ParticipantLevel = exhibitor.ParticipantLevel;
            exhibitorToSave.ContactName = exhibitor.ContactName;
            exhibitorToSave.ContactEmail = exhibitor.ContactEmail;

            context.SubmitChanges();

            return projectExhibitor(exhibitorToSave);
        }

        public void RemoveExhibitor(EventAddress eventAddress, Exhibitor exhibitor)
        {
            oxite_Conferences_Exhibitor exhibitorToRemove = null;

            if (exhibitor.ID != Guid.Empty)
            {
                exhibitorToRemove = context
                    .oxite_Conferences_Exhibitors
                    .FirstOrDefault(c => c.ExhibitorID == exhibitor.ID);
            }
        
            if (exhibitorToRemove == null)
            {
                return;
            }

            context.oxite_Conferences_Exhibitors.DeleteOnSubmit(exhibitorToRemove);
            context.SubmitChanges();
        }

        private static Exhibitor projectExhibitor(oxite_Conferences_Exhibitor e)
        {
            return new Exhibitor(
                e.ExhibitorID, 
                e.EventID,
                e.Name, 
                e.Description, 
                e.SiteUrl, 
                e.LogoUrl, 
                e.ParticipantLevel, 
                e.ContactName,
                e.ContactEmail,
                e.Location,
                e.Tags,
                e.CreatedDate, 
                e.ModifiedDate);
        }
    }
}
