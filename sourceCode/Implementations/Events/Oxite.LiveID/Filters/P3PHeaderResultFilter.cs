//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;

namespace Oxite.Modules.LiveID.Filters
{
    public class P3PHeaderResultFilter : IResultFilter
    {
        #region IResultFilter Members

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.AddHeader("P3P", "CP=\"BUS CUR CONo FIN IVDo ONL OUR PHY SAMo TELo\"");
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        #endregion
    }
}
