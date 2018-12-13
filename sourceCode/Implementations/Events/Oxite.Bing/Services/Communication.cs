using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using Oxite.Modules.Search.Models;

namespace Oxite.Modules.Bing.Services
{
    internal class Communication
    {
        private string apiKey = null;
        private const string TargetURL = "http://api.search.live.net/xml.aspx?AppId={0}&Query={1}&Sources=Web&Web.Offset={2}&Web.Count={3}&Web.Options=DisableHostCollapsing+DisableQueryAlterations";

        public Communication(string apiKey)
        {
            this.apiKey = apiKey;            
        }

        public SearchResults DoQuery(BingSearchCriteria criteria, int pageSize, int pageIndex)
        {
            int offset = pageSize*pageIndex;

            string urlToRequest = string.Format(TargetURL, apiKey, HttpUtility.UrlEncode(criteria.Render()), offset, pageSize);
            WebClient wc = new WebClient {Encoding = System.Text.Encoding.UTF8};
            string result = wc.DownloadString(urlToRequest);

            //string result = File.ReadAllText("Results.xml");

            SearchResults results = parseResponse(result);

            results.Offset = offset;
            results.PageSize = pageSize;
            results.PageIndex = pageIndex;

            return results;
        }

        private static SearchResults parseResponse(string result)
        {
            SearchResults results = new SearchResults();

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(result);

            // Add the default namespace to the namespace manager.
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(
                xdoc.NameTable);
            nsmgr.AddNamespace(
                "api",
                "http://schemas.microsoft.com/LiveSearch/2008/04/XML/element");

            XmlNode root = xdoc.DocumentElement;

            if (root == null)
            {
                return results;
            }

            XmlNodeList errors = root.SelectNodes(
                "./api:Errors/api:Error",
                nsmgr);

            if (errors != null && errors.Count > 0)
            {
                // There are errors in the response. Display error details.
                foreach (XmlNode node in errors)
                {
                    if (node != null)
                    {
                        results.Errors.Add(new SearchError() { Description = node.InnerText, Name = node.Name});
                    }
                }
            }
            else
            {
                // There were no errors in the response. Parse the
                // Web results.

                // Add the Web SourceType namespace to the namespace manager.
                nsmgr.AddNamespace(
                    "web",
                    "http://schemas.microsoft.com/LiveSearch/2008/04/XML/web");

                XmlNode web = root.SelectSingleNode("./web:Web", nsmgr);
                XmlNodeList webResults = web.SelectNodes(
                    "./web:Results/web:WebResult",
                    nsmgr);

                string version = root.SelectSingleNode("./@Version", nsmgr).InnerText;
                string searchTerms = root.SelectSingleNode(
                    "./api:Query/api:SearchTerms",
                    nsmgr).InnerText;
                int offset;
                int.TryParse(
                    web.SelectSingleNode("./web:Offset", nsmgr).InnerText,
                    out offset);
                int total;
                int.TryParse(
                    web.SelectSingleNode("./web:Total", nsmgr).InnerText,
                    out total);

                results.TotalResultCount = total;
                results.Results = new List<ISearchResult>();

                if (webResults != null)
                {
                    foreach (XmlNode webResult in webResults)
                    {
                        SearchResult sr = new SearchResult
                                              {
                                                  Title = webResult.SelectSingleNode("./web:Title", nsmgr).InnerText,
                                                  URL = webResult.SelectSingleNode("./web:Url", nsmgr).InnerText,
                                                  Description =
                                                      webResult.SelectSingleNode("./web:Description", nsmgr).InnerText,
                                                  ResultDateTime =
                                                      DateTime.Parse(
                                                      webResult.SelectSingleNode("./web:DateTime", nsmgr).InnerText)
                                              };
                        XmlNodeList searchTags =webResult.SelectNodes(
                            "./web:SearchTags/web:WebSearchTag",
                            nsmgr);

                        sr.MetaData = new Dictionary<string, string>();

                        if (searchTags == null)
                        {
                            continue;
                        }

                        foreach (XmlNode tag in searchTags)
                        {
                            string name = tag.SelectSingleNode("./web:Name", nsmgr).InnerText;
                            string value = tag.SelectSingleNode("./web:Value", nsmgr).InnerText;
                            if (!String.IsNullOrEmpty(value))
                            {
                                value = value.Replace("\"", "");
                            }
                            if (sr.MetaData.ContainsKey(name))
                                sr.MetaData[name] = sr.MetaData[name] + "," + value;
                            else
                                sr.MetaData.Add(name, value);
                        }

                        if (sr.MetaData != null && sr.MetaData.ContainsKey("search.title"))
                        {
                            sr.Title = sr.MetaData["search.title"];
                        }

                        results.Results.Add(sr);
                    }
                }
            }

            return results;
        }

    }

    public class SearchResults : Oxite.Models.IPageOfItems<ISearchResult>
    {
        public int TotalResultCount;
        public int Offset;
        public int PageSize;
        public int PageIndex;
        public List<ISearchResult> Results;
        public List<SearchError> Errors;

        int Oxite.Models.IPageOfItems<ISearchResult>.PageIndex
        {
            get { return PageIndex; }
        }

        int Oxite.Models.IPageOfItems<ISearchResult>.PageSize
        {
            get { return PageSize; }
        }

        public int TotalPageCount
        {
            get { return (TotalResultCount / PageSize) + 1; }
        }

        public int TotalItemCount
        {
            get { return TotalResultCount; }
        }


        public IEnumerator<ISearchResult> GetEnumerator()
        {
            return Results.GetEnumerator();
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Results.GetEnumerator();
        }


    }

    public class SearchError
    {
        public string Name;
        public string Description;
    }

    public class SearchResult : ISearchResult
    {
        public string Title;
        public string Description;
        public string URL;
        public string DisplayURL;
        public DateTime ResultDateTime;
        public Dictionary<string, string> MetaData = new Dictionary<string, string>();

        #region ISearchResult Members

        string ISearchResult.Title
        {
            get { return Title; }
        }

        public string Body
        {
            get { return Description; }
        }

        public string Url
        {
            get { return URL; }
        }

        public IDictionary<string, object> Values
        {
            get { return (IDictionary<string, object>)MetaData; }
        }


        DateTime ISearchResult.ResultDateTime
        {
            get { return ResultDateTime; }
        }

        #endregion
    }
}
