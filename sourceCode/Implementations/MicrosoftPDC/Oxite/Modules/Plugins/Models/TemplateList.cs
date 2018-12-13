//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;

namespace Oxite.Modules.Plugins.Models
{
    public class TemplateList : List<Template>
    {
        public void Add(string templateName, string selector, SelectorType selectorType)
        {
            Add(templateName, selector, selectorType, c => true);
        }

        public void Add(string templateName, string selector, SelectorType selectorType, string pageName)
        {
            Add(templateName, selector, selectorType, pageName, "");
        }

        public void Add(string templateName, string selector, SelectorType selectorType, string pageName, string modelTarget)
        {
            Add(new Template(templateName, selector, selectorType, c => string.Compare(c.PageName, pageName, true) == 0, modelTarget));
        }

        public void Add(string templateName, string selector, SelectorType selectorType, Func<TemplateContext, bool> pageCondition)
        {
            Add(templateName, selector, selectorType, pageCondition, "");
        }

        public void Add(string templateName, string selector, SelectorType selectorType, Func<TemplateContext, bool> pageCondition, string modelTarget)
        {
            Add(new Template(templateName, selector, selectorType, pageCondition, modelTarget));
        }
    }
}
