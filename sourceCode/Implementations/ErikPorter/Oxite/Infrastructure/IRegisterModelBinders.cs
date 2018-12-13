//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;

namespace Oxite.Infrastructure
{
    public interface IRegisterModelBinders
    {
        void RegisterModelBinders(ModelBinderDictionary modelBinders);
    }
}
