//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Blogs.ViewModels
{
    public class TrackbackViewModel
    {
        public TrackbackViewModel()
        {
            ErrorCode = 0;
        }

        public TrackbackViewModel(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public int ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}
