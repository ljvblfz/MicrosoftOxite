//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Hosting;
using Oxite.Models;

namespace Oxite.Skinning
{
    public class OxiteWebFormSkinEngine : OxiteSkinEngine
    {
        public OxiteWebFormSkinEngine(VirtualPathProvider virtualPathProvider)
            : base(virtualPathProvider)
        {
            this.masterFileExtensions = ".master";
            this.viewFileExtensions = ".aspx,.ascx";
        }
    }
}
