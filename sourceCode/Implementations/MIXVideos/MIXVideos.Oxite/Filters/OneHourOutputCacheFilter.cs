using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MIXVideos.Oxite.Filters
{
    public class OneHourOutputCacheFilter : OutputCacheAttribute
    {
        public OneHourOutputCacheFilter()
        {
            this.Duration = TimeSpan.FromHours(1).Seconds;
            this.VaryByParam = "foo";
            this.Location = System.Web.UI.OutputCacheLocation.Any;
        }
    }
}
