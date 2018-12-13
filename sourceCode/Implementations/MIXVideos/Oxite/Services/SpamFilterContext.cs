using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Oxite.Models;

namespace Oxite.Services
{
    public class SpamFilterContext
    {
        public RequestContext RequestContext { get; set; }
        public Comment Comment { get; set; }
        public PostAddress PostAddress { get; set; }
        public UserBase AnonymousUser { get; set; }
    }
}
