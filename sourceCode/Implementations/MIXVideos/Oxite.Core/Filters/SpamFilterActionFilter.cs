using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oxite.Models;
using Oxite.Services;

namespace Oxite.Filters
{
    public class SpamFilterActionFilter : IActionFilter
    {
        private ISpamFilterService spamFilter;
        public SpamFilterActionFilter(ISpamFilterService spamFilter)
        {
            this.spamFilter = spamFilter;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Comment incomingComment = filterContext.ActionParameters["commentInput"] as Comment;
            PostAddress postAddress = filterContext.ActionParameters["postAddress"] as PostAddress;
            UserBase user = filterContext.ActionParameters["userBaseInput"] as UserBase;

            if (incomingComment != null)
            {
                SpamFilterContext context = new SpamFilterContext() 
                { 
                    Comment = incomingComment, 
                    PostAddress = postAddress,
                    AnonymousUser = user,
                    RequestContext = filterContext.RequestContext
                };
                if (this.spamFilter.IsSpam(context))
                    incomingComment.State = EntityState.Removed;
            }
        }

        #endregion
    }
}
