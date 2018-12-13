<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelList<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="MIXVideos.Oxite.Extensions" %><%
if (((IPageOfList<Post>)Model.List).TotalItemCount > 0)
{ %><ul class="posts medium"><%
    int counter = 0;
    foreach (Post post in Model.List)
    {
        StringBuilder className = new StringBuilder("post", 15);
        
        if (post.Equals(Model.List.First())) { className.Append(" first"); }
        if (post.Equals(Model.List.Last())) { className.Append(" last"); }

        if (counter % 2 != 0) { className.Append(" odd"); }
        if (counter % 4 == 3) { className.Append(" fourth"); }
        if (counter % 4 == 0) { className.Append(" lead"); }
        %>
    <li class="<%=className.ToString() %>">
        <h2 class="title" title="<%=post.Title.CleanAttribute() %>"><%=Html.Link(post.Title.CleanText().Ellipsize(74, "&nbsp;&#8230;"), Url.Post(post)) %></h2>
        <div class="posted"><%=post.Published.HasValue ? Html.ConvertToLocalTime(post.Published.Value).ToLongDateString() : "Draft" %></div>
        <div class="content">
            <div class="thumbnail"><%=Html.Thumbnail(post, (k, d) => Model.Localize(k, d))%></div>
            <div class="details">
                <div class="body"><%=post.GetBodyShort().Ellipsize(280, "&nbsp;&#8230;") %></div>                            
                <div class="more"><%
                if (Model.Site.HasMultipleAreas)
                    Response.Write(string.Format(
                        Model.Localize("From {0} | "),
                        Html.Link(post.Area.Name.CleanText(), Url.Posts(post.Area))
                        ));
                
                %> <%=Html.Link("More &raquo;", Url.Post(post), new { @class = "arrow" }) %></div>
            </div>
        </div>
    </li><%
        counter++;
    } %>
</ul><% 
} 
else
{ //todo: (nheskew) need an Html.Message html helper extension method that takes a message %>
<div class="message info"><%=Model.Localize("NoneFound", "None found.")%></div><%        
} %>