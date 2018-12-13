<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<PostComment>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %><%
if (Model.Items.Count() > 0)
{ %><ul class="comments medium"><%
    int counter = 0;
    foreach (PostComment comment in Model.Items)
    {
        StringBuilder className = new StringBuilder("comment", 40);

        if (comment.Equals(Model.Items.First())) { className.Append(" first"); }
        if (comment.Equals(Model.Items.Last())) { className.Append(" last"); }
        if (Model.User.IsInRole("Admin")) { className.AppendFormat(" {0}", comment.State.ToString().ToLower()); }        
        if (counter % 2 != 0) { className.Append(" odd"); }

        className.Append(comment.CreatorUserID != Guid.Empty
            ? " anon" 
            : string.Format(
                " {0} {1}",
                comment.CreatorUserID == comment.CreatorUserID ? "author" : "user",
                comment.CreatorName.CleanAttribute()
                )
            ); 
        %>
    <li class="<%=className.ToString() %>">
        <% Html.RenderPartialFromSkin("Comment", new OxiteViewModelPartial<PostComment>(Model, comment)); %>
    </li><%
        counter++;
    } %>
</ul><% 
} %>