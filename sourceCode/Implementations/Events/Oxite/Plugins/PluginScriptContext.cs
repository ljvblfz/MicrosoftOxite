//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;

namespace Oxite.Plugins
{
    public class PluginScriptContext : RequestContext
    {
        public PluginScriptContext(PluginScriptContext context)
            : base(context.HttpContext, context.RouteData)
        {
            PageName = context.PageName;
        }

        public PluginScriptContext(ResultExecutedContext context)
            : base(context.HttpContext, context.RouteData)
        {
            PageName = context.Result is ViewResult ? ((ViewResult)context.Result).ViewName : null;
        }

        public string PageName { get; private set; }
    }
}
