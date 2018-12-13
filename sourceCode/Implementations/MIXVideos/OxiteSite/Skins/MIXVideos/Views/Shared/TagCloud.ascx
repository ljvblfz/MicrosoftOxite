<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelPartial<TagCloudViewModel>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %><%
    if (Model.PartialModel != null)
    {
        IList<KeyValuePair<Tag, int>> tags = Model.PartialModel.Tags;

        if (tags.Count > 0)
        { %>
        <div class="sub tags">
            <h3><%=Model.RootModel.Localize("Tags") %></h3>
            <%
            double? averagePostCount = null;
            double? standardDeviationPostCount = null;
            
            Response.Write(
                Html.UnorderedList(
                    tags.OrderBy(kvp => kvp.Key.Name),
                    t => string.Format(
                        "<a href=\"{2}\" rel=\"tag\" class=\"t{3}\">{0} ({1})</a> ",
                        t.Key.DisplayName,
                        t.Value,
                        Model.RootModel.Container is Area ? Url.Posts(Model.RootModel.Container as Area, t.Key) : Url.Posts(t.Key),
                        t.Key.GetTagWeight(tags, ref averagePostCount, ref standardDeviationPostCount)
                    ),
                    "tagCloud"
                )
            ); %>
        </div><%
        }
    } %>