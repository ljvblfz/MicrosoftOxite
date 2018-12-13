//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Oxite.Infrastructure
{
    public class FileEngineResult
    {
        public FileEngineResult(IEnumerable<string> searchedLocations)
        {
            SearchedLocations = searchedLocations;
        }

        public FileEngineResult(string filePath, IViewEngine viewEngine)
            : this(Enumerable.Empty<string>())
        {
            FilePath = filePath;
            ViewEngine = viewEngine;
        }

        public IEnumerable<string> SearchedLocations { get; private set; }
        public string FilePath { get; private set; }
        public IViewEngine ViewEngine { get; private set; }
    }
}
