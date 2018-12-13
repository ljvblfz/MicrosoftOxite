//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;

namespace Oxite.Modules.Setup.Services
{
    using Oxite.Models;
    using Oxite.Modules.Setup.Models;
    using Oxite.Services;
    using Oxite.Validation;

    /// <summary>
    /// Interface defining the service that contains the setup actions.
    /// </summary>
    public interface ISetupService
    {
        /// <summary>
        /// Create site based on user input.  Includes creating site entry, Admin account, anonymous user account,
        /// and default blog.
        /// </summary>
        /// <param name="input">SetupInput instance containing all user entered information about the site and its
        /// components.</param>
        /// <param name="validationState"></param>
        /// <returns>Site instance containing details of the new site.  Null if setup is unsuccessful.</returns>
        Site SetupSite(SetupInput input, out ValidationStateDictionary validationState);
    }
}
