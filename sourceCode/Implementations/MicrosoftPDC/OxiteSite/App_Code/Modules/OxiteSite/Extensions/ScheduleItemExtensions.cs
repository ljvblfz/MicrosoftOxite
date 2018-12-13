using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Modules.Conferences.Models;
using OxiteSite.App_Code.Modules.OxiteSite.ViewModels;

namespace OxiteSite.App_Code.Modules.OxiteSite.Extensions
{
    public static class ScheduleItemExtensions
    {
 
        public static IEnumerable<ColumnTag> Columns(this IEnumerable<ScheduleItemTag> items, int columns)
        {
            List<ColumnTag> output = new List<ColumnTag>();

            int rows = items.Count() / columns;
            if (rows * columns < items.Count())
            {
                rows++;
            }

            List<ScheduleItemTag>[] results = new List<ScheduleItemTag>[rows];

            int row = 0;
            int col = 0;
            foreach (ScheduleItemTag s in items)
            {
                if (results[row] == null)
                    results[row] = new List<ScheduleItemTag>();

                results[row].Add(s);
                if (row < rows - 1)
                {
                    row++;
                }
                else
                {
                    row = 0;
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < results[i].Count; j++)
                {
                    string cssClass = "";
                    if (results[i].Count < columns && j == results[i].Count-1)
                    {
                        cssClass = "span" + (columns - results[i].Count);
                    }
                    output.Add(new ColumnTag(results[i].ElementAt(j), cssClass));   
                }
            }

            return output;
        }


        public static string GetClassNameFromType(this ScheduleItem item)
        {
            string cls = "session";

            if (item.Type.Equals("workshop", StringComparison.InvariantCultureIgnoreCase))
                cls = "workshop";

            return cls;
        }

        public static string SpeakerLocationTime(this ScheduleItem item, UrlHelper urlHelper, HtmlHelper htmlHelper)
        {
            string result = "";

            if (item != null)
            {
                string speakers = "";
                string location = "";
                string time = "";
                if (item.Speakers != null && item.Speakers.Count() > 0)
                {
                    speakers = string.Join(", ",
                                           item.Speakers.Select(
                                               s =>
                                               htmlHelper.Link(s.DisplayName.WidowControl(), urlHelper.Speaker(s),
                                                               new {@class = "speaker"})).ToArray());
                }
                if (!string.IsNullOrEmpty(item.Location))
                {
                    location = item.Location;
                }

                if (item.Start != item.End)
                {
                    time = string.Format("{0:dddd} at {0:h:mm tt}", item.Start);
                }


                if (!string.IsNullOrEmpty(speakers))
                {
                    result = speakers;
                }

                if (!string.IsNullOrEmpty(location))
                {
                    result += " in&nbsp;" + location.Replace(" ", "&nbsp;");
                }

                if (!string.IsNullOrEmpty(time))
                {
                    result += " on&nbsp;" + time.Replace(" ", "&nbsp;");
                }
            }

            if (result == "")
                result = "&nbsp;";

            return result;

        }

    }
}
