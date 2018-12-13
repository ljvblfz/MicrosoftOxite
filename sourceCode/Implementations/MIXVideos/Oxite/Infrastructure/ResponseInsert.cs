//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Oxite.Infrastructure
{
    public class ResponseInsert
    {
        private readonly string value;
        private readonly ResponseInsertMode mode;
        private readonly Func<XDocument, IEnumerable<XElement>> selectorFunction;
        private readonly Action<IEnumerable<XElement>, ResponseInsertMode, string> insertValueFunction;

        public ResponseInsert(string value, ResponseInsertMode mode, string selector)
            : this(value, mode, generateSelectorFunction(selector))
        {
        }

        public ResponseInsert(string value, ResponseInsertMode mode, string selector, Action<IEnumerable<XElement>, ResponseInsertMode, string> insertValueFunction)
            : this(value, mode, generateSelectorFunction(selector), (e, m, v) => defaultInsertValue(e, m, v))
        {
        }

        public ResponseInsert(string value, ResponseInsertMode mode, Func<XDocument, IEnumerable<XElement>> selectorFunction)
            : this(value, mode, selectorFunction, (e, m, v) => defaultInsertValue(e, m, v))
        {
        }

        public ResponseInsert(string value, ResponseInsertMode mode, Func<XDocument, IEnumerable<XElement>> selectorFunction, Action<IEnumerable<XElement>, ResponseInsertMode, string> insertValueFunction)
        {
            this.value = value;
            this.mode = mode;
            this.selectorFunction = selectorFunction;
            this.insertValueFunction = insertValueFunction;
        }

        public void Apply(XDocument doc, ref bool modifiedDoc)
        {
            IEnumerable<XElement> elements = selectorFunction(doc);

            if (elements != null && elements.Count() > 0)
            {
                insertValueFunction(elements, mode, value);

                modifiedDoc = true;
            }
        }

        private static void insertValueOnElements(IEnumerable<XElement> elements, Action<XElement> method)
        {
            foreach (XElement element in elements)
                method(element);
        }

        private static void defaultInsertValue(IEnumerable<XElement> elements, ResponseInsertMode mode, string value)
        {
            XElement element = XElement.Parse(value, LoadOptions.PreserveWhitespace);

            switch (mode)
            {
                case ResponseInsertMode.ReplaceWith:
                    insertValueOnElements(elements, e => e.ReplaceWith(addNamespace(e, element)));
                    break;
                case ResponseInsertMode.InsertBefore:
                    insertValueOnElements(elements, e => e.AddBeforeSelf(addNamespace(e, element)));
                    break;
                case ResponseInsertMode.InsertAfter:
                    insertValueOnElements(elements, e => e.AddAfterSelf(addNamespace(e, element)));
                    break;
                case ResponseInsertMode.AppendTo:
                    insertValueOnElements(elements, e => e.Add(addNamespace(e, element)));
                    break;
                case ResponseInsertMode.PrependTo:
                    insertValueOnElements(elements, e => e.AddFirst(addNamespace(e, element)));
                    break;
            }
        }

        private static Func<XDocument, IEnumerable<XElement>> generateSelectorFunction(string selector)
        {
            if (selector.StartsWith("."))
            {
                string className = selector.Substring(1);

                return new Func<XDocument, IEnumerable<XElement>>(doc => findElements(doc, new Func<XElement, bool>(e => e.Attribute("class") != null && (e.Attribute("class").Value == className || e.Attribute("class").Value.Split(' ').Contains(className)))));
            }
            else if (selector.StartsWith("#"))
            {
                string id = selector.Substring(1);

                return new Func<XDocument, IEnumerable<XElement>>(doc => findElements(doc, new Func<XElement, bool>(e => e.Attribute("id") != null && e.Attribute("id").Value == id)));
            }
            else
            {
                return new Func<XDocument, IEnumerable<XElement>>(doc => findElements(doc, new Func<XElement,bool>(e => e.Name.LocalName == selector)));
            }
        }

        private static IEnumerable<XElement> findElements(XDocument doc, Func<XElement, bool> selectorMatcher)
        {
            List<XElement> elements = new List<XElement>(5);

            findElements(selectorMatcher, doc.Root, elements);

            return elements;
        }

        private static void findElements(Func<XElement, bool> selectorMatcher, XElement currentElement, List<XElement> elements)
        {
            if (selectorMatcher(currentElement))
                elements.Add(currentElement);

            if (currentElement.HasElements)
                foreach (XElement element in currentElement.Elements())
                    findElements(selectorMatcher, element, elements);
        }

        private static XElement addNamespace(XElement namespacedElement, XElement element)
        {
            XNamespace ns = namespacedElement.Name.NamespaceName;

            return new XElement(
                ns + element.Name.LocalName,
                element.Attributes(),
                addNamespace(ns, element.Nodes())
                );
        }

        private static IEnumerable<XNode> addNamespace(XNamespace ns, IEnumerable<XNode> nodes)
        {
            List<XNode> newNodes = new List<XNode>();

            if (nodes.Count() > 0)
            {
                foreach (XNode node in nodes)
                {
                    if (node is XElement)
                    {
                        XElement element = (XElement)node;

                        newNodes.Add(
                            new XElement(
                                ns + element.Name.LocalName,
                                element.Attributes(),
                                addNamespace(ns, element.Nodes())
                                )
                            );
                    }
                    else
                        newNodes.Add(node);
                }
            }

            return newNodes;
        }
    }
}
