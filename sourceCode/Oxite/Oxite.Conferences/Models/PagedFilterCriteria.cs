// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------

using System.Text;
using System.Text.RegularExpressions;

//todo: (nheskew)really, really think about sticking paging params into the query string
namespace Oxite.Modules.Conferences.Models
{
    public class PagedFilterCriteria
    {
        private const int pageIndexDefault = 0;
        private const int pageSizeDefault = 7;

        private readonly Regex pageFilterCriteriaRegex =
            new Regex(@"(?:(?<=^|/)Page(?<pageIndex>\d+)(?=$|/))?(?:(?<=^|/)Count(?<pageSize>\d+)(?=$|/))?",
                      RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private int? pageIndex;
        private int? pageSize;
        public string Term { get; set; }

        public PagedFilterCriteria() { }

        public PagedFilterCriteria(string rawData)
        {
            if (!string.IsNullOrEmpty(rawData))
            {
                Match pageFilterCriteriaMatch = pageFilterCriteriaRegex.Match(rawData);

                if (pageFilterCriteriaMatch.Groups["pageIndex"].Success || pageFilterCriteriaMatch.Groups["pageSize"].Success)
                {
                    int pi;
                    if (pageFilterCriteriaMatch.Groups["pageIndex"].Success &&
                        int.TryParse(pageFilterCriteriaMatch.Groups["pageIndex"].Value, out pi))
                        pageIndex = pi > 0 ? pi - 1 : 0;
                    else
                        pageIndex = pageIndexDefault;

                    int ps;
                    if (pageFilterCriteriaMatch.Groups["pageSize"].Success &&
                        !int.TryParse(pageFilterCriteriaMatch.Groups["pageSize"].Value, out ps))
                        pageSize = ps;
                    else
                        pageSize = pageSizeDefault;
                }
            }
        }

        public int PageIndex
        {
            get
            {
                return pageIndex ?? pageIndexDefault;
            }

            set { pageIndex = value; }
        }

        public int PageSize
        {
            get
            {
                return pageSize ?? pageSizeDefault;
            }

            set { pageSize = value; }
        }

        public virtual bool HasCriteria()
        {
            return !string.IsNullOrEmpty(Term)
                || pageIndex != null
                || pageSize != null;
        }

        public virtual string ToUrl()
        {
            StringBuilder sb = new StringBuilder();

            if (pageIndex != null)
                sb.AppendFormat("Page{0}/", pageIndex + 1);

            if (pageSize != null && pageSize != pageSizeDefault)
                sb.AppendFormat("Count{0}/", pageSize);

            if (!string.IsNullOrEmpty(Term))
                sb.AppendFormat("?Term={0}", Term);

            return sb.ToString();
        }
    }
}
