//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Modules.Files.Models;

namespace Oxite.Modules.Files.ViewModels
{
    //TODO: (erikpo) This class should eventually move into a Oxite.Files.Media module
    public class PlayerViewModel
    {
        public PlayerViewModel(File media, File preview)
        {
            Media = media;
            Preview = preview;
        }

        public File Media { get; private set; }
        public File Preview { get; private set; }
    }
}
