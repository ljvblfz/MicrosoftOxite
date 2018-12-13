// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------

using System;
using System.Text;
using System.Text.RegularExpressions;

//todo: (nheskew)really, really think about sticking paging params into the query string
namespace Oxite.Modules.Conferences.Models
{
    public class PagedFilterCriteria
    {
        protected int pageIndexDefault = 0;
        protected int pageSizeDefault = 7;

        private readonly Regex pageFilterCriteriaRegex =
            new Regex(@"(?:(?<=^|/)Page(?<pageIndex>\d+)(?=$|/))?(?:(?<=^|/)Count(?<pageSize>\d*\w*)(?=$|/))?",
                      RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private int? _pageIndex;
        private int? _pageSize;
        public string Term { get; set; }

        public PagedFilterCriteria() { }

        public PagedFilterCriteria(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
            {
                return;
            }

            var pageFilterCriteriaMatches = pageFilterCriteriaRegex.Matches(rawData);
            Group pageIndex = null;
            Group pageSize = null;

            foreach (Match match in pageFilterCriteriaMatches)
            {
                if(match.Groups["pageIndex"].Success)
                {
                    pageIndex = match.Groups["pageIndex"];
                }
                if(match.Groups["pageSize"].Success)
                {
                    pageSize = match.Groups["pageSize"];
                }
            }

            if(pageIndex == null && pageSize == null)
            {
                return;
            }
                
            if (pageIndex != null && pageIndex.Success || 
                pageSize != null && pageSize.Success)
            {
                if (pageIndex != null)
                {
                    int pi;
                    if (pageIndex.Success &&
                        int.TryParse(pageIndex.Value, out pi))
                    {
                        _pageIndex = pi > 0 ? pi - 1 : 0;
                    }
                    else
                    {
                        _pageIndex = pageIndexDefault;
                    }
                }
                else
                {
                    _pageIndex = pageIndexDefault;
                }

                if (pageSize != null)
                {
                    int ps;
                    if (pageSize.Success && int.TryParse(pageSize.Value, out ps))
                    {
                        _pageSize = ps;
                    }
                    else if (pageSize.Value.Equals("All", StringComparison.InvariantCultureIgnoreCase))
                    {
                        _pageSize = 100000;
                    }
                    else
                    {
                        _pageSize = pageSizeDefault;
                    }
                }
                else
                {
                    _pageSize = pageSizeDefault;
                }
            }
        }

        public int PageIndex
        {
            get
            {
                return _pageIndex ?? pageIndexDefault;
            }

            set { _pageIndex = value; }
        }

        public int PageSize
        {
            get
            {
                return _pageSize ?? pageSizeDefault;
            }

            set { _pageSize = value; }
        }

        public virtual bool HasCriteria()
        {
            return !string.IsNullOrEmpty(Term)
                || _pageIndex != null
                || _pageSize != null;
        }

        public virtual string ToUrl()
        {
            StringBuilder sb = new StringBuilder();

            if (_pageIndex != null)
                sb.AppendFormat("Page{0}/", _pageIndex + 1);

            if (_pageSize != null && _pageSize != pageSizeDefault)
                sb.AppendFormat("Count{0}/", _pageSize);

            if (!string.IsNullOrEmpty(Term))
                sb.AppendFormat("?Term={0}", Term);

            return sb.ToString();
        }
    }
}
