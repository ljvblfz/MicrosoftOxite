//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Oxite.Modules.Conferences.Models
{
    public class ScheduleItemFilterCriteria : PagedFilterCriteria
    {
        private static readonly Regex scheduleItemFilterCriteriaRegex =
            new Regex(@"^(?:(?<=^|/)(?<mine>Mine)(?=$|/))?(?:(?<=^|/)(?<video>WithVideo)(?=$|/))?(?:(?<=^|/)(?<popular>ByPopularity)(?=$|/))?$",
                      RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public ScheduleItemFilterCriteria() { }

        public ScheduleItemFilterCriteria(string rawData)
            : base(rawData)
        {
            if (string.IsNullOrEmpty(rawData)) return;
            Match scheduleItemFilterCriteriaMatch = scheduleItemFilterCriteriaRegex.Match(rawData);

            if (!scheduleItemFilterCriteriaMatch.Success) return;
            if (scheduleItemFilterCriteriaMatch.Groups["mine"].Success)
                ForUser = true;

            if (scheduleItemFilterCriteriaMatch.Groups["video"].Success)
                WithFileTypes.Add("video");

            if (scheduleItemFilterCriteriaMatch.Groups["popular"].Success)
                OrderByPopular = true;
        }

        public string ScheduleItemType { get; set; }
        public string SpeakerName { get; set; }
        public string DayName { get; set; }
        public bool OrderByPopular { get; set; }
        public bool ForUser { get; set; }
        public List<string> WithFileTypes = new List<string>();

        public override bool HasCriteria()
        {
            return !string.IsNullOrEmpty(ScheduleItemType)
                   || !string.IsNullOrEmpty(SpeakerName)
                   || !string.IsNullOrEmpty(DayName)
                   || OrderByPopular
                   || ForUser
                   || WithFileTypes.Count > 0
                   || base.HasCriteria();
        }

        public override string ToUrl()
        {
            StringBuilder sb = new StringBuilder();

            if (ForUser)
                sb.Append("Mine/");

            if (WithFileTypes.Contains("video"))
                sb.Append("WithVideo/");

            if (OrderByPopular)
                sb.Append("ByPopularity/");

            return sb + base.ToUrl();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (ForUser)
                sb.Append("Mine/");

            if (WithFileTypes.Contains("video"))
                sb.Append("WithVideo/");

            if (OrderByPopular)
                sb.Append("ByPopularity/");

            if (!string.IsNullOrEmpty(ScheduleItemType))
                sb.AppendFormat("ScheduleItemType/{0}/", ScheduleItemType);

            if (!string.IsNullOrEmpty(SpeakerName))
                sb.AppendFormat("SpeakerName/{0}/", SpeakerName);

            if (!string.IsNullOrEmpty(DayName))
                sb.AppendFormat("DayName/{0}/", DayName);

            if (!string.IsNullOrEmpty(Term))
                sb.AppendFormat("?Term={0}", Term);

            return sb.ToString();
        }
    }
}