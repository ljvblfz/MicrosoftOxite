using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oxite.Modules.Conferences.Models;

namespace OxiteSite.App_Code.Modules.OxiteSite.ViewModels
{
    public class ColumnTag
    {
        public ColumnTag(ScheduleItemTag tag, string cssClass)
        {
            Tag = tag;
            CSSClass = cssClass;
        }
        public ScheduleItemTag Tag;
        public string CSSClass;
    }
}
