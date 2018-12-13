using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oxite.Services
{
    public class NaiveSpamFilterService : ISpamFilterService
    {
        #region ISpamFilterService Members

        public bool IsSpam(SpamFilterContext context)
        {
            return false;
        }

        #endregion
    }
}
