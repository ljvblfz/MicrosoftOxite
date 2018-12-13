//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;

namespace Oxite.Models
{
    public class PostBase : EntityBase, INamedEntity
    {
        public User Creator { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string BodyShort { get; set; }
        public EntityState State { get; set; }
        public string Slug { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Published { get; set; }
        public IList<Comment> Comments { get; set; }
        public bool CommentingDisabled { get; set; }

        #region INamedEntity Members

        string INamedEntity.Name
        {
            get { return Slug; }
        }

        string INamedEntity.DisplayName
        {
            get { return Title; }
        }

        #endregion
    }
}
