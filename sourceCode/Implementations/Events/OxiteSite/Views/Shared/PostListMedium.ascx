<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models.Extensions" %><%
if (Model.Items != null && ((IPageOfItems<Post>)Model.Items).TotalItemCount > 0)
{ %><ul class="posts medium"><%
    int counter = 0;
    foreach (Post post in Model.Items)
    {
        StringBuilder className = new StringBuilder("post", 15);
        
        if (post.Equals(Model.Items.First())) { className.Append(" first"); }
        if (post.Equals(Model.Items.Last())) { className.Append(" last"); }

        if (counter % 2 != 0) { className.Append(" odd"); }
        %>
    <li class="<%=className.ToString() %>">
        <h2 class="title"><%=Html.Link(post.Title.CleanText(), Url.Post(post)) %></h2>
        <div class="posted"><%=post.Published.HasValue ? Html.ConvertToLocalTime(post.Published.Value).ToLongDateString() : "Draft" %></div>
        <div class="content"><%=post.GetBodyShort() %></div>                            
        <div class="more"><%
        if (Model.Site.HasMultipleBlogs)
            Response.Write(string.Format(
                Model.Localize("From the {0} Blog. | "),
                Html.Link(post.Blog.Name.CleanText(), Url.Posts(post.Blog))
                ));
        
        if (post.Tags.Count() > 0)
        {
            Response.Write(
                string.Format(
                    "{0} {1} | ", 
                    Model.Localize("Filed under"),
                    Html.UnorderedList(
                        post.Tags,
                        (t, i) => Html.Link(t.GetDisplayName().CleanText(), Url.Posts(t), new { rel = "tag" }),
                        "tags"
                    )
                )
            );
        }
        %><%=Html.Link(string.Format("{0} comment{1}", post.Comments.Count(), post.Comments.Count() == 1 ? "" : "s"), string.Format("{0}#comments", Url.Post(post)))
        %> <%=Html.Link("&raquo;", Url.Post(post), new { @class = "arrow" }) %></div>
    </li><%
        counter++;
    } %>
</ul><% 
} 
else
{ //todo: (nheskew) need an Html.Message html helper extension method that takes a message %>
<div class="message info"><%=Model.Localize("NoneFound", "None found.")%></div><%        
} %>