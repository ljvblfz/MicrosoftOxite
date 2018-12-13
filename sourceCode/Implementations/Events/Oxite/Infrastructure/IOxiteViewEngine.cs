//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;

namespace Oxite.Infrastructure
{
    public interface IOxiteViewEngine : IViewEngine
    {
        void SetRootPath(string rootPath);
        void SetRootPath(string rootPath, bool onlySearchRootPathForPartialViews);
        FileEngineResult FindScriptsFile(string fileName);
        FileEngineResult FindStylesFile(string fileName);
        FileEngineResult FindFile(string fileName);
    }
}
