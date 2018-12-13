using System;
using System.Web.Mvc;
using Oxite.Repositories;

namespace Oxite.Modules.Core.Controllers
{
    public class ViewController : Controller
    {
        private readonly IViewRepository repository;

        public ViewController(IViewRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult AddView(string entityType, string viewType, string id)
        {

            try
            {
                Guid parsedId = new Guid(id);

                repository.AddView(viewType, entityType, parsedId, HttpContext.Request.UserHostAddress);
            }
            catch
            {
                // intentionally eat all errors. Prefer page load over missed view
            }

            return File(Convert.FromBase64String("R0lGODlhAQABAIAAANvf7wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw=="), "image/gif");                
        }
    }
}