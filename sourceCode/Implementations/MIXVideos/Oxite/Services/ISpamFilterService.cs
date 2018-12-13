using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oxite.Models;
using System.Web.Routing;

namespace Oxite.Services
{
    public interface ISpamFilterService
    {
        bool IsSpam(SpamFilterContext context);
    }
}
