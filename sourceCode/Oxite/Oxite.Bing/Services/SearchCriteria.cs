using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oxite.Modules.Bing.Services
{
    public abstract class bingSearchCriteria
    {
        public abstract string Render();
        public bool Not = false;
    }


    public enum JoinType
    {
        And = 0,
        Or = 1
    }

    public class CriteriaCollection : bingSearchCriteria
    {
        public List<bingSearchCriteria> Items = new List<bingSearchCriteria>();

        public JoinType JoinType = JoinType.And;

        public override string Render()
        {
            StringBuilder output = new StringBuilder();
            if (Not)
                output.Append("-");

            output.Append("(");

            for (int i = 0; i < Items.Count; i++)
            {
                bingSearchCriteria sc = Items[i];
                output.Append(sc.Render());

                if (i == Items.Count - 1)
                {
                }
                else
                {
                    switch (JoinType)
                    {
                        case JoinType.And:
                            output.Append(" ");
                            break;

                        default:
                            output.Append(" OR ");
                            break;
                    }
                }
            }
            output.Append(")");

            return output.ToString();
        }
    }

    public class SearchTerm : bingSearchCriteria
    {
        public string Term = "";
        public bool ExactMatch;

        public override string Render()
        {
            string result = Term.Trim();
            if (ExactMatch)
                result =  "\"" + result + "\"";
            
            if (Not)
                result = "-" + result;

            return result;
        }
    }

    public class SiteRestriction : bingSearchCriteria
    {
        public string Site = "";

        public override string Render()
        {
            string result = "site:" + Site.Trim();

            if (Not)
                result = "-" + result;

            return result;
        }
    }

    public class SearchTag : bingSearchCriteria
    {
        public string TagName = "";
        public string Value = "";
        public bool ExactMatch;

        public override string Render()
        {
            string result = "meta:Search." + TagName.Trim();

            if (ExactMatch)
            {
                result = result + "(\"" + Value.Trim() + "\")";
            }
            else
            {
                result = result + "(" + Value.Trim() + ")";
            }

            if (Not)
                result = "-" + result;

            return result;
        }

    }
}
