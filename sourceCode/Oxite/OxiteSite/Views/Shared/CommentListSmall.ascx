<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItemItems<Post, PostComment>>" %>
<%@ Import Namespace="Oxite.Modules.Membership.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %><%
if (((IPageOfItems<PostComment>)Model.Items).TotalItemCount > 0)
{ %><ul class="comments small"><%
    int counter = 0;
    foreach (PostComment postAndComment in Model.Items)
    {
        StringBuilder className = new StringBuilder("comment", 40);

        if (postAndComment.Equals(Model.Items.First())) { className.Append(" first"); }
        if (postAndComment.Equals(Model.Items.Last())) { className.Append(" last"); }

        if (Model.GetUser().IsInRole("Admin")) { className.AppendFormat(" {0}", postAndComment.State.ToString().ToLower()); }

        if (counter % 2 != 0) { className.Append(" odd"); }

        className.Append(postAndComment.CreatorUserID == Guid.Empty
            ? " anon" 
            : string.Format(
                " {0} {1}",
                postAndComment.CreatorUserID == postAndComment.CreatorUserID ? "author" : "user",
                postAndComment.CreatorName.CleanAttribute()
                )
            ); 
        %>
    <li class="<%=className.ToString() %>">
            <div class="meta" id="<%=postAndComment.Slug %>"><%
                if (Model.GetUser().IsInRole("Admin") && postAndComment.State == EntityState.PendingApproval)
                {
                %><span class="state" title="<%=Model.Localize("PendingApproval", "Pending Approval") %>"><%=Model.Localize("PendingApproval", "Pending Approval") %></span><%
                } %>
                <span class="name"><%=Html.LinkOrDefault(postAndComment.CreatorName.CleanText(), postAndComment.CreatorUrl.CleanHref()) %></span>
                <span class="when"> - <%= 
                    Html.Link(
                        Html.ConvertToLocalTime(postAndComment.Created).ToString("MMMM dd, yyyy - h:mm tt"), //todo: (nheskew) localize date format
                        Model.GetUser().IsInRole("Admin") && postAndComment.State == EntityState.PendingApproval
                            ? Url.ManageComment(postAndComment) //todo: (nheskew)need the route to work with a page of comments
                            : Url.Comment(postAndComment)
                        )%></span>
            </div>
            <div class="post"><%=postAndComment.Post.Title.CleanText() %></div>
            <div class="text"><%=postAndComment.Body.CleanText() %></div>
    </li><%
        counter++;
    } %>
</ul><% 
} 
else
{ //todo: (nheskew) need an Html.Message html helper extension method that takes a message %>
<div class="message info"><%=Model.Localize("NoneFound", "None found.") %></div><%        
} %>