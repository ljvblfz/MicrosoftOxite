//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;

namespace Oxite.Plugins
{
    public class PluginTemplateRegistry : List<PluginTemplate>
    {
        //TODO: (erikpo) Need to add a method to obtain a lock so multiple methods can be called while the items are locked

        public void Add(Plugin plugin, string templateName, string selector, PluginTemplateSelectorType selectorType)
        {
            Add(plugin, templateName, selector, selectorType, c => true);
        }

        public void Add(Plugin plugin, string templateName, string selector, PluginTemplateSelectorType selectorType, string pageName)
        {
            Add(plugin, templateName, selector, selectorType, pageName, "");
        }

        public void Add(Plugin plugin, string templateName, string selector, PluginTemplateSelectorType selectorType, string pageName, string modelTarget)
        {
            Add(plugin, templateName, selector, selectorType, c => string.Compare(c.PageName, pageName, true) == 0, modelTarget);
        }

        public void Add(Plugin plugin, string templateName, string selector, PluginTemplateSelectorType selectorType, Func<PluginTemplateContext, bool> forCurrentRequest)
        {
            Add(plugin, templateName, selector, selectorType, forCurrentRequest, "");
        }

        public void Add(Plugin plugin, string templateName, string selector, PluginTemplateSelectorType selectorType, Func<PluginTemplateContext, bool> forCurrentRequest, string modelTarget)
        {
            lock (this)
            {
                this.Add(new PluginTemplate(plugin, templateName, selector, selectorType, forCurrentRequest, modelTarget));
            }
        }

        public void UpdatePlugin(Plugin plugin)
        {
            lock (this)
            {
                this.Where(pt => pt.Plugin.ID == plugin.ID).ToList().ForEach(pt => pt.Plugin = plugin);
            }
        }
    }
}
