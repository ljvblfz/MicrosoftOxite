//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Infrastructure;

namespace Oxite.Models
{
    public class Site : IExtendedPropertyStore, ISecureEntity
    {
        public IList<Uri> HostRedirects { get; set; }
        public Uri Host { get; set; }
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string LanguageDefault { get; set; }
        public double TimeZoneOffset { get; set; }
        public string PageTitleSeparator { get; set; }
        public string FavIconUrl { get; set; }
        public string CommentStateDefault { get; set; }
        public bool IncludeOpenSearch { get; set; }
        public bool AuthorAutoSubscribe { get; set; }
        public short PostEditTimeout { get; set; }
        public string GravatarDefault { get; set; }
        public string SkinsPath { get; set; }
        public string SkinsScriptsPath { get; set; }
        public string SkinsStylesPath { get; set; }
        public string Skin { get; set; }
        public string AdminSkin { get; set; }
        public bool HasMultipleBlogs { get; set; }
        public string RouteUrlPrefix { get; set; }
        public bool CommentingDisabled { get; set; }
        public string PluginsPath { get; set; }

        #region IExtendedPropertyStore Members

        //TODO: (erikpo) Once Site is switched to BuildByConstructor, change the setter to private
        public IEnumerable<ExtendedProperty> ExtendedProperties { get; set; }

        public string ScopeType
        {
            get { return this.GetType().FullName; }
        }

        public string ScopeKey
        {
            get { return ID.ToString("N"); }
        }

        #endregion

        #region ISecureEntity Members

        public bool IsInRole(User user, string role)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
