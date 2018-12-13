using System.Linq;
using Oxite.Extensions;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Extensions
{
    public static class ScheduleItemExtensions
    {
        public static string GetBodyShort(this ScheduleItem scheduleItem)
        {
            return scheduleItem.GetBodyShort(100);
        }
        public static string GetBodyShort(this ScheduleItem scheduleItem, int wordCount)
        {
            string bodyText = !string.IsNullOrEmpty(scheduleItem.Body)
                ? scheduleItem.Body.CleanHtmlTags("(/?p|br\\s*/|/?a\\s*[^<>]*?)").CleanWhitespace()
                : "";
            string previewText = "";

            if (!string.IsNullOrEmpty(bodyText))
                previewText = string.Join(" ", bodyText.Split(' ').Take(wordCount).ToArray()) +
                    (string.Compare(previewText, bodyText) == 0 ? "&#160;&#8230;" : "");

            return previewText;
        }
    }
}
