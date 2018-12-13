//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

using System.Text.RegularExpressions;

namespace Oxite.Modules.Conferences.Models
{
    public class SpeakerFilterCriteria : PagedFilterCriteria
    {
        private static readonly Regex speakerFilterCriteriaRegex =
            new Regex(@"^(?:(?<=^|/)filter/(?<term>[^/]+)(?=$|/))?$",
                      RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public SpeakerFilterCriteria() {}

        public SpeakerFilterCriteria(string rawData)
            :base(rawData)
        {
            if (!string.IsNullOrEmpty(rawData))
            {
                Match speakerFilterCriteriaMatch = speakerFilterCriteriaRegex.Match(rawData);

                if (speakerFilterCriteriaMatch.Success)
                {
                    if (speakerFilterCriteriaMatch.Groups["term"].Success)
                        Term = speakerFilterCriteriaMatch.Groups["term"].Value;
                }
            }
        }

        public new bool HasCriteria()
        {
            return !string.IsNullOrEmpty(Term)
                   || base.HasCriteria();
        }
    }
}