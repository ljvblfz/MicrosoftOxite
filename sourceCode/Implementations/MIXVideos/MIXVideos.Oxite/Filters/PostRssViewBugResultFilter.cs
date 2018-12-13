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
    public class PostRssViewBugResultFilter : PostFeedViewBugResultFilter
    {
        public PostRssViewBugResultFilter()
            : base("Post-RSS", (doc, index) => findElements(doc, index))
        {
        }

        private static IEnumerable<XElement> findElements(XDocument doc, int index)
        {
            XElement element = doc.Element("rss").Element("channel").Elements("item").ElementAt(index);
            List<XElement> elements = new List<XElement>();

            if (element != null)
                element = element.Element("description");

            if (element != null)
                elements.Add(element);

            return elements;
        }
    }
}
