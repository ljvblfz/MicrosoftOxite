//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

using Oxite.Configuration;
using Oxite.Infrastructure;

namespace Oxite.Modules.Setup.Models
{
    using System;
    using Oxite.Models;
    using System.Collections.Generic;
    using Oxite.Services;

    /// <summary>
    /// Class to contain all basic data needed during the initial site setup.
    /// </summary>
    public class SetupInput
    {
        #region Fields
        private SiteType siteType;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SetupInput class.
        /// </summary>
        public SetupInput()
        {
            this.SiteType = SiteType.PersonalBlog;
            this.StorageType = StorageType.Sql;
            this.Modules = new List<OxiteModuleConfigurationElement>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Id of the site.
        /// </summary>
        public Guid SiteID { get; set; }

        /// <summary>
        /// User name for the initial admin account.
        /// </summary>
        public string AdminUserName { get; set; }
        
        /// <summary>
        /// Display name for the initial admin account.
        /// </summary>
        public string AdminDisplayName { get; set; }
        
        /// <summary>
        /// Email for the initial admin account.
        /// </summary>
        public string AdminEmail { get; set; }
        
        /// <summary>
        /// Password for the initial admin account.
        /// </summary>
        public string AdminPassword { get; set; }
        
        /// <summary>
        /// Password confirmation entry for the initial admin account.
        /// </summary>
        public string AdminPasswordConfirm { get; set; }

        public string SiteDisplayName { get; set; }

        public string SiteDescription { get; set; }

        public StorageType StorageType { get; set; }

        public SiteType SiteType 
        {
            get
            {
                return this.siteType;
            }

            set
            {
                this.siteType = value;
                this.SetModuleDefaults();
            }
        }

        public List<OxiteModuleConfigurationElement> Modules { get; set; }
        #endregion

        public void InitializeModules(OxiteConfigurationSection config)
        {
            //TODO: (erikpo) Instead of directly sending down the config information, wrap it in some other class (like a view model)
            foreach (OxiteModuleConfigurationElement module in config.Modules)
            {
                this.Modules.Add(module);
                this.SetModuleDefaults();
            }
        }

        private void SetModuleDefaults()
        {
        }
    }
}
