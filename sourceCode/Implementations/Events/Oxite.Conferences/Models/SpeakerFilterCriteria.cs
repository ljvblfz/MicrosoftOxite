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

        

        public SpeakerFilterCriteria()
        {
            //pageSizeDefault = 10;
        }

        public SpeakerFilterCriteria(string rawData)
            :base(rawData)
        {
            //pageSizeDefault = 10;
            if (string.IsNullOrEmpty(rawData)) return;
            Match speakerFilterCriteriaMatch = speakerFilterCriteriaRegex.Match(rawData);

            if (!speakerFilterCriteriaMatch.Success) return;
            if (speakerFilterCriteriaMatch.Groups["term"].Success)
                Term = speakerFilterCriteriaMatch.Groups["term"].Value;            
        }

        public string Term { get; set; }

        public new bool HasCriteria()
        {
            return !string.IsNullOrEmpty(Term)
                   || base.HasCriteria();
        }
    }
}