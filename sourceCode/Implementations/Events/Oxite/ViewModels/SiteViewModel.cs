//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;

namespace Oxite.ViewModels
{
    public class SiteViewModel
    {
        public SiteViewModel(Site currentSite, string configSiteName)
        {
            ID = currentSite.ID;
            DisplayName = currentSite.DisplayName ?? "My Oxite Site";
            Description = currentSite.Description;
            PageTitleSeparator = currentSite.PageTitleSeparator ?? " - ";
            TimeZoneOffset = currentSite.TimeZoneOffset;
            IncludeOpenSearch = currentSite.IncludeOpenSearch;
            LanguageDefault = currentSite.LanguageDefault;
            GravatarDefault = currentSite.GravatarDefault;
            PostEditTimeout = currentSite.PostEditTimeout;
            SkinsPath = currentSite.SkinsPath ?? "/Skins";
            SkinsScriptsPath = currentSite.SkinsScriptsPath ?? "/Scripts";
            SkinsStylesPath = currentSite.SkinsStylesPath ?? "/Styles";
            Skin = currentSite.Skin ?? "Default";
            AdminSkin = currentSite.AdminSkin ?? "Admin";
            FavIconUrl = currentSite.FavIconUrl ?? "/Content/icons/flame.ico";
            HasMultipleBlogs = currentSite.HasMultipleBlogs;
            CommentingDisabled = currentSite.CommentingDisabled;
            PluginsPath = currentSite.PluginsPath ?? "/Plugins";
        }

        public Guid ID { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public string PageTitleSeparator { get; private set; }
        public double TimeZoneOffset { get; private set; }
        public bool IncludeOpenSearch { get; set; }
        public string LanguageDefault { get; set; }
        public string GravatarDefault { get; set; }
        public short PostEditTimeout { get; set; }
        public string SkinsPath { get; private set; }
        public string SkinsScriptsPath { get; private set; }
        public string SkinsStylesPath { get; private set; }
        public string Skin { get; private set; }
        public string AdminSkin { get; private set; }
        public string FavIconUrl { get; private set; }
        public bool HasMultipleBlogs { get; private set; }
        public bool CommentingDisabled { get; private set; }
        public string PluginsPath { get; private set; }
    }
}
