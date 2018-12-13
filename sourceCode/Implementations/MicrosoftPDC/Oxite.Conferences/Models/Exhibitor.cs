//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;

namespace Oxite.Modules.Conferences.Models
{
    public class Exhibitor : EntityBase
    {
        public Exhibitor(Guid id)
            : base(id)
        {

        }

        public Exhibitor(Guid id, Guid eventId, string name, string description, string siteUrl, string logoUrl, string participantLevel, string contactName, string contactEmail, string location, string tags, DateTime created, DateTime modified) : this(id)
        {
            Name = name;
            EventID = eventId;
            Description = description;
            SiteUrl = siteUrl;
            LogoUrl = logoUrl;
            ParticipantLevel = participantLevel;
            ContactName = contactName;
            ContactEmail = contactEmail;
            Location =location;
            Tags = tags;
            CreatedDate = created;
            ModifiedDate = modified;
        }

        public Guid EventID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string SiteUrl { get; private set; }
        public string LogoUrl { get; private set; }
        public string ParticipantLevel { get; private set; }
        public string ContactName { get; private set; }
        public string ContactEmail { get; private set; }
        public string Location { get; private set; }
        public string Tags { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime ModifiedDate { get; private set; }
    }
}
