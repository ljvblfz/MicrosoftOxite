<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %><%
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
    <li class="<%=className.ToString() %>"><%
            Html.RenderPartialFromSkin("PostSummaryMedium", new OxiteViewModelPartial<Post>(Model, post)); %>
    </li><%
        counter++;
    } %>
</ul><% 
} 
else
{ //todo: (nheskew) need an Html.Message html helper extension method that takes a message %>
<div class="message info"><%=Model.Localize("NoneFound", "None found.")%></div><%        
} %>