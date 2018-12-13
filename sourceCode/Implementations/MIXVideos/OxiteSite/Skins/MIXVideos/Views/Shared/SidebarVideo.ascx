<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelPartial<SidebarViewModel>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="MIXVideos.Oxite.Extensions" %>
<%@ Import Namespace="MIXVideos.Oxite.ViewModels" %><%

Post post = Model.PartialModel.Post; %><%
if (post != null) { %>
<div class="sub random">
    <h3><%=Model.RootModel.Localize("RandomVideo", "Random") %></h3>
    <ul class="posts medium">
        <li class="post lead">
            <div class="content">
                <div class="thumbnail"><%=Html.Thumbnail(post, (k, d) => Model.RootModel.Localize(k, d))%></div>
                <div class="details">
                    <div class="body"><%=Html.Link(post.Title.CleanText().Ellipsize(74, "&nbsp;&#8230;"), Url.Post(post), new { @class = "title" })%><br /><span class="posted"><%=post.Published.HasValue ? Html.ConvertToLocalTime(post.Published.Value, Model.RootModel).ToLongDateString() : "Draft" %></span> <%=post.GetBodyShort().Ellipsize(180, "&nbsp;&#8230;") %></div>                            
                    <div class="more"><%
                if (Model.RootModel.Site.HasMultipleAreas)
                    Response.Write(string.Format(
                        Model.RootModel.Localize("From {0} | "),
                        Html.Link(post.Area.Name.CleanText(), Url.Posts(post.Area))
                        ));
                
                if (post.Tags.Count > 0)
                {
                    Response.Write(
                        string.Format(
                            "{0} {1} | ", 
                            Model.RootModel.Localize("Filed under"),
                            Html.UnorderedList(
                                post.Tags,
                                (t, i) => Html.Link(t.GetDisplayName().CleanText(), Url.Posts(t), new { rel = "tag" }),
                                "tags"
                            )
                        )
                    );
                }
                %> <%=Html.Link("More &raquo;", Url.Post(post), new { @class = "arrow" }) %></div>
                </div>
            </div>
        </li>
    </ul>
</div><%
} %>

