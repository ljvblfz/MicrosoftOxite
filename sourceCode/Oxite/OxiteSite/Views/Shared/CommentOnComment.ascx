<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PostComment>>" %>
<%@ Import Namespace="Oxite.Modules.Membership.Extensions"%>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
    <form method="post" id="comment" action="<%=Url.AddComment(Model.PartialModel.Post) %>#comment" class="<%=Model.GetUser().IsAuthenticated ? "user" : "anon" %>">
        <div class="replyingto"><%=
            Model.Localize("ReplyingTo", "Replying to") %> <%=
            Html.Link(Model.PartialModel.CreatorName.CleanText(), Url.CommentPermalinkReply(Model.PartialModel)) %>. <%=
            Html.Link(Model.Localize("Cancel"), Url.Comment(Model.PartialModel)) %> - <%=
            Html.Link(Model.Localize("CommentOnPost", "Comment on the post"), Url.CommentOnPost(Model.PartialModel.Post)) %></div><%
        if (Request.QueryString.Get("pending") == bool.TrueString)
        { %>
        <div class="message info"><%=Model.Localize("PendingComment", "Thanks for the comment. It'll show up here pending admin approval.") %></div><%
        } %>
        <%=Html.ValidationSummary() %>
        <fieldset>
            <%=Html.Hidden("parentID", Model.PartialModel.ID.ToString()) %>
        </fieldset>
        <%
        if (Model.GetUser().IsAuthenticated)
            Html.RenderPartialFromSkin("CommentFormAuthenticated");
        else
            Html.RenderPartialFromSkin("CommentFormAnonymous");
        %>
    </form>
