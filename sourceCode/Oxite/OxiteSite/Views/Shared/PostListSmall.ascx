<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %><%
if (((IPageOfItems<Post>)Model.Items).TotalItemCount > 0)
{ %><ul class="posts small"><%
    int counter = 0;
    foreach (Post post in Model.Items)
    {
        StringBuilder className = new StringBuilder("post", 15);
        
        if (post.Equals(Model.Items.First())) { className.Append(" first"); }
        if (post.Equals(Model.Items.Last())) { className.Append(" last"); }

        if (counter % 2 != 0) { className.Append(" odd"); }
        %>
    <li class="<%=className.ToString() %>">
        <div><span class="title"><%=Html.Link(post.Title.CleanText(), Url.Post(post))
            %></span> <span class="comments">- <%=Html.Link(string.Format("{0} comment{1}", post.Comments.Count(), post.Comments.Count() == 1 ? "" : "s"), string.Format("{0}#comments", Url.Post(post))) 
            %></span></div>
        <div class="info"><%
        if (Model.Site.HasMultipleBlogs)
            Response.Write(string.Format(
                Model.Localize("<span>From the {0} Blog. | </span>"),
                Html.Link(post.Blog.Name.CleanText(), Url.Posts(post.Blog))
                )); %><span class="posted"><%=post.Published.HasValue ? Html.ConvertToLocalTime(post.Published.Value).ToLongDateString() : "Draft" %></span></div>
    </li><%
        counter++;
    } %>
</ul><% 
} 
else
{ //todo: (nheskew) need an Html.Message html helper extension method that takes a message %>
<div class="message info"><%=Model.Localize("NoneFound", "None found.") %></div><%        
} %>