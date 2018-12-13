//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;

namespace Oxite.Modules.Conferences.Models
{
    public class ExhibitorFilterCriteria : PagedFilterCriteria
	{
        public Event Event { get; set; }
        public ICollection<string> ParticipantLevels { get; set; }

        public ExhibitorFilterCriteria()
        {
            ParticipantLevels = new List<string>(0);
        }
	}
}
