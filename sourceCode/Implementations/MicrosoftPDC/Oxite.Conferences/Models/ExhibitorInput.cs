//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Runtime.Serialization;

namespace Oxite.Modules.Conferences.Models
{
    [DataContract]
    public class ExhibitorInput
    {
        public ExhibitorInput(Guid? id,
                              string name, 
                              string participantLevel, 
                              string siteUrl, 
                              string logoUrl, 
                              string description,
                              string contactName,
                              string contactEmail,
                              string location,
                              string tags)
        {
            Id = id;
            Name = name;
            ParticipantLevel = participantLevel;
            SiteUrl = siteUrl;
            LogoUrl = logoUrl;
            Description = description;
            ContactName = contactName;
            ContactEmail = contactEmail;
            Location = location;
            Tags = tags;
        }

        public Guid? Id { get; private set; }
        public string Name { get; private set; }
        public string ParticipantLevel { get; private set; }
        public string SiteUrl { get; private set; }
        public string LogoUrl { get; private set; }
        public string Description { get; private set; }
        public string ContactName { get; private set; }
        public string ContactEmail { get; private set; }
        public string Location { get; private set; }
        public string Tags { get; private set; }
    }
}
