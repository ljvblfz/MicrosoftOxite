using System.Linq;
using System.Web.Mvc;
using Oxite.Filters;
using Oxite.Infrastructure;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Services;
using Oxite.ViewModels;

namespace OxiteSite.App_Code.Modules.OxiteSite.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IScheduleItemService _scheduleItemService;
        private readonly OxiteContext _context;

        public ReportsController(IScheduleItemService scheduleItemService, OxiteContext context)
        {
            _scheduleItemService = scheduleItemService;
            _context = context;
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        [XlsResultActionFilter("Summary")]
        public OxiteViewModelItems<ScheduleItem> Summary(EventAddress eventAddress)
        {
            if(!_context.User.IsInRole("Admin"))
            {
                return new OxiteViewModelItems<ScheduleItem>();
            }
            
            var criteria = new ScheduleItemFilterCriteria {PageIndex = 0, PageSize = 1000000};
            var sessions = _scheduleItemService.GetScheduleItemsUncached(eventAddress, criteria);

            return new OxiteViewModelItems<ScheduleItem>(sessions);
        }
    }
}
