//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Routing;
using Oxite.Models;

namespace Oxite.Plugins
{
    public class PluginTemplate
    {
        internal PluginTemplate(Plugin plugin, string templateName, string selector, PluginTemplateSelectorType selectorType, Func<PluginTemplateContext, bool> forCurrentRequest, string modelTarget)
        {
            Plugin = plugin;
            TemplateName = templateName;
            Selector = selector;
            SelectorType = selectorType;
            ForCurrentRequest = forCurrentRequest;
            ModelTarget = modelTarget;
        }

        public Plugin Plugin { get; set; }
        public string TemplateName { get; private set; }
        public string Selector { get; private set; }
        public PluginTemplateSelectorType SelectorType { get; private set; }
        public Func<PluginTemplateContext, bool> ForCurrentRequest { get; private set; }
        public string ModelTarget { get; private set; }
    }
}
