//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.Plugins.Models
{
    public class Template
    {
        internal Template(string templateName, string selector, SelectorType selectorType, Func<TemplateContext, bool> pageCondition, string modelTarget)
        {
            TemplateName = templateName;
            Selector = selector;
            SelectorType = selectorType;
            PageCondition = pageCondition;
            ModelTarget = modelTarget;
        }

        public string TemplateName { get; private set; }
        public string Selector { get; private set; }
        public SelectorType SelectorType { get; private set; }
        public Func<TemplateContext, bool> PageCondition { get; private set; }
        public string ModelTarget { get; private set; }
    }
}
