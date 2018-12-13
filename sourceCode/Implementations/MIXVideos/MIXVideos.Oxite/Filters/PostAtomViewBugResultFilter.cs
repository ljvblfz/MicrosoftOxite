//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MIXVideos.Oxite.Filters
{
    public class PostAtomViewBugResultFilter : PostFeedViewBugResultFilter
    {
        public PostAtomViewBugResultFilter()
            : base("Post-ATOM", (doc, index) => findElements(doc, index))
        {
        }

        private static IEnumerable<XElement> findElements(XDocument doc, int index)
        {
            XNamespace ns = XNamespace.Get(doc.Elements().First().Attribute("xmlns").Value);
            XElement element = doc.Element(ns + "feed").Elements(ns + "entry").ElementAt(index);
            List<XElement> elements = new List<XElement>();

            if (element != null)
                element = element.Element(ns + "content");

            if (element != null)
                elements.Add(element);

            return elements;
        }
    }
}
