//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Oxite.Infrastructure
{
    public class ResponseInsert
    {
        private readonly Func<int, string> getValue;
        private readonly ResponseInsertMode mode;
        private readonly Func<string, IEnumerable<string>> selectorFunction;
        private readonly Func<string, IEnumerable<string>, ResponseInsertMode, Func<int, string>, string> insertValueFunction;

        public ResponseInsert(Func<int, string> getValue, ResponseInsertMode mode, string selector)
            : this(getValue, mode, generateSelectorFunction(selector))
        {
        }

        public ResponseInsert(Func<int, string> getValue, ResponseInsertMode mode, string selector, Func<string, IEnumerable<string>, ResponseInsertMode, Func<int, string>, string> insertValueFunction)
            : this(getValue, mode, generateSelectorFunction(selector), insertValueFunction)
        {
        }

        public ResponseInsert(Func<int, string> getValue, ResponseInsertMode mode, Func<string, IEnumerable<string>> selectorFunction)
            : this(getValue, mode, selectorFunction, defaultInsertValue)
        {
        }

        public ResponseInsert(Func<int, string> getValue, ResponseInsertMode mode, Func<string, IEnumerable<string>> selectorFunction, Func<string, IEnumerable<string>, ResponseInsertMode, Func<int, string>, string> insertValueFunction)
        {
            this.getValue = getValue;
            this.mode = mode;
            this.selectorFunction = selectorFunction;
            this.insertValueFunction = insertValueFunction;
        }

        public void Apply(ref string doc, ref bool modifiedDoc)
        {
            IEnumerable<string> elements = selectorFunction(doc);

            if (elements != null && elements.Count() > 0)
            {
                doc = insertValueFunction(doc, elements, mode, getValue);

                modifiedDoc = true;
            }
        }

        private static string insertValueOnElements(string doc, IEnumerable<string> elements, Func<string, int, string> getElementReplacement)
        {
            int index = 0;

            foreach (string element in elements)
            {
                doc = doc.Replace(element, getElementReplacement(element, index));

                index++;
            }

            return doc;
        }

        private static string defaultInsertValue(string doc, IEnumerable<string> elements, ResponseInsertMode mode, Func<int, string> getValue)
        {
            switch (mode)
            {
                case ResponseInsertMode.ReplaceWith:
                    return insertValueOnElements(doc, elements, (e, i) => replaceWith(getValue(i)));
                case ResponseInsertMode.InsertBefore:
                    return insertValueOnElements(doc, elements, (e, i) => insertBefore(e, getValue(i)));
                case ResponseInsertMode.InsertAfter:
                    return insertValueOnElements(doc, elements, (e, i) => insertAfter(e, getValue(i)));
                case ResponseInsertMode.AppendTo:
                    return insertValueOnElements(doc, elements, (e, i) => appendTo(e, getValue(i)));
                case ResponseInsertMode.PrependTo:
                    return insertValueOnElements(doc, elements, (e, i) => prependTo(e, getValue(i)));
                case ResponseInsertMode.Wrap:
                    return insertValueOnElements(doc, elements, (e, i) => wrap(e, getValue(i)));
                case ResponseInsertMode.Remove:
                    return insertValueOnElements(doc, elements, (e, i) => remove(e));
            }

            return doc;
        }

        private static string replaceWith(string value)
        {
            return value;
        }

        private static string insertBefore(string element, string value)
        {
            return value + element;
        }

        private static string insertAfter(string element, string value)
        {
            return element + value;
        }

        private static readonly Regex closingTagRegex = new Regex(@"(</[^<>]+>)$", RegexOptions.Compiled | RegexOptions.Singleline);
        private static string appendTo(string element, string value)
        {
            return closingTagRegex.Replace(element, new MatchEvaluator(m => value + m.Value));
        }

        private static readonly Regex openingTagRegex = new Regex(@"^(<[^<>]+>)", RegexOptions.Compiled | RegexOptions.Singleline);
        private static string prependTo(string element, string value)
        {
            return openingTagRegex.Replace(element, new MatchEvaluator(m => m.Value + value));
        }

        private static string wrap(string element, string value)
        {
            return string.Format(value, element);
        }

        private static string remove(string element)
        {
            return "";
        }

        
        private static readonly Regex whitespaceRegex = new Regex(@"\s+", RegexOptions.Compiled | RegexOptions.Singleline);
        private static readonly Regex compactSelectorRegex = new Regex(@"\s*([>+~,=!^$():\[\]])\s*", RegexOptions.Compiled | RegexOptions.Singleline);
        //todo: (nheskew) add filters and attributes
        private static readonly Regex simpleSelectorRegex = new Regex(@"^(?<tag>\w+)?(?:(?:\.(?<class>[a-z][\w-_]*))|(?:#(?<id>[a-z][\w-_.:]*)))*$", RegexOptions.Compiled | RegexOptions.Singleline);
        private static readonly Regex voidElementNameRegex = new Regex("^(?:br|img|input)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Func<string, IEnumerable<string>> generateSelectorFunction(string cssSelector)
        {
            StringBuilder pattern = new StringBuilder();

            string[] selectors = compactSelectorRegex.Replace(cssSelector.Trim(), "$1").Split(',');
            foreach (string selector in selectors)
            {
                //todo: (nheskew) other hierarchy selectors (>, + and ~)
                if (selector.Contains(">") || selector.Contains("+") || selector.Contains("~"))
                    throw new InvalidOperationException("Child and sibling selectors are not yet implemented :|");

                string selectorPattern = constructAncestorDescendantPattern(selector);

                pattern.AppendFormat("{1}(?:{0})", selectorPattern, pattern.Length > 0 ? "|" : "");
            }

            Regex selectorRegex = new Regex(pattern.ToString(), RegexOptions.IgnoreCase | RegexOptions.Singleline);

            return doc => findElements(doc, selectorRegex);
        }

        private static string constructAncestorDescendantPattern(string selector)
        {
            StringBuilder ancestorDescendantPattern = new StringBuilder();

            string[] selectorParts = whitespaceRegex.Split(selector);
            foreach (string selectorPart in selectorParts)
                ancestorDescendantPattern.AppendFormat(
                    selectorPart == selectorParts.First()
                        ? "(?<={0}"
                        : ".*?{0}",
                    selectorPart == selectorParts.Last()
                        ? string.Format(")(?<element>{0})", constructNodePatternFormat(selectorPart))
                        : constructNodePatternFormat(selectorPart)
                    );

            return ancestorDescendantPattern.ToString();
        }

        //todo: (nheskew) hook up filters and attributes
        private static string constructNodePatternFormat(string selector)
        {
            StringBuilder nodePatternFormat = new StringBuilder("<");
            string nodeName = @"\w+";

            MatchCollection selectorPartMatches = simpleSelectorRegex.Matches(selector);
            foreach (Match selectorPartMatch in selectorPartMatches)
            {
                if (selectorPartMatch.Groups["tag"].Success)
                    nodeName = selectorPartMatch.Groups["tag"].Value;

                nodePatternFormat.Append(nodeName);

                if (selectorPartMatch.Groups["id"].Success)
                    nodePatternFormat.Append(constructNodeIdPattern(selectorPartMatch.Groups["id"].Captures));

                if (selectorPartMatch.Groups["class"].Success)
                    nodePatternFormat.Append(constructNodeClassPattern(selectorPartMatch.Groups["class"].Captures));
            }

            nodePatternFormat.Append("[^>]*/?>");

            return nodePatternFormat.ToString();
        }

        private static string constructNodeIdPattern(CaptureCollection idCaptureCollection)
        {
            return string.Format("\\s+id=[\"'](?:{0})[\"']", idCaptureCollection[0].Value);
        }

        //todo: (nheskew) look for multiple class names on the element and deal with the edges of the class name a _lot_ better
        private static string constructNodeClassPattern(CaptureCollection classNameCaptureCollection)
        {
            return string.Format("\\s+class=[\"'](?:[^\"']*\\s+)?{0}(?:\\s+[^\"']*)?[\"']", classNameCaptureCollection[0].Value);
        }

        private static IEnumerable<string> findElements(string doc, Regex selectorRegex)
        {
            List<string> elements = new List<string>(5);

            MatchCollection matches = selectorRegex.Matches(doc);

            foreach (Match match in matches)
                if (match.Groups["element"].Success)
                    foreach (Group group in match.Groups["element"].Captures)
                        elements.Add(
                            group.Value.EndsWith("/>") || voidElementNameRegex.IsMatch(group.Value)
                                ? group.Value
                                : findElementWithContents(doc, group)
                            );

            return elements;
        }

        private static readonly Regex findElementRegex = new Regex(@"(</?(?<tag>\w+)[^>]*/?>)", RegexOptions.Compiled | RegexOptions.Singleline);
        private static string findElementWithContents(string doc, Group startTagGroup)
        {
            string elementStartToDocEnd = doc.Substring(startTagGroup.Index);

            int tagCount = 0;
            int endClosingTagIndex = 0;
            string tagName = "";

            MatchCollection tagMatches = findElementRegex.Matches(elementStartToDocEnd);
            foreach (Match tagMatch in tagMatches)
            {
                string tag = tagMatch.Value;
                if (tagMatch == tagMatches[0])
                    tagName = tagMatch.Groups["tag"].Value;

                if (tagName == tagMatch.Groups["tag"].Value && !tag.EndsWith("/>"))
                {
                    if (tag.StartsWith("</"))
                        --tagCount;
                    else
                        ++tagCount;
                }

                endClosingTagIndex = tagMatch.Index + tagMatch.Length;

                if (tagCount == 0)
                    break;
            }

            return elementStartToDocEnd.Substring(0, endClosingTagIndex);
        }
    }
}
