<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PostComment>>" %>
<%@Import Namespace="Oxite.Extensions" %>
<%@Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %><%
if (Model.PartialModel != null)
{
    Html.RenderPartialFromSkin("ManageComment"); 
%><div class="contents"><%
            if (Model.User.IsInRole("Admin") && Model.PartialModel.State == EntityState.PendingApproval)
            {
            %><span class="state" title="<%=Model.Localize("PendingApproval", "Pending Approval") %>"><%=Model.Localize("PendingApproval", "Pending Approval") %></span><%
            } %>
            <div class="name" id="<%=Model.PartialModel.Slug %>">
                <div><%=Html.LinkOrDefault(Html.Gravatar(Model, Model.PartialModel, "48"), Model.PartialModel.CreatorUrl.CleanHref(), new { @class = "avatar" })%></div>
                <p class="comment">
                    <strong><%=Html.LinkOrDefault(Model.PartialModel.CreatorName.CleanText(), Model.PartialModel.CreatorUrl.CleanHref(), new { rel = "nofollow" })%></strong>
                    <% Html.RenderPartialFromSkin("CommentCreatorInfo"); %>
                </p>
            </div>
            <div class="text"><%
                if (Model.PartialModel.Parent != null) { %>
                <p><%=Model.Localize("ReplyTo", "In reply to") %>: <%=Html.Link(Model.PartialModel.Parent.CreatorName.CleanText(), Url.Comment(Model.PartialModel.Parent, Model.PartialModel.Post)) %></p> <%
                } %>
                <p><%=Model.PartialModel.Body.CleanCommentBody() %></p>
            </div>
        </div><%
        if (ViewContext.HttpContext.Request.QueryString["to"] == Model.PartialModel.Slug)
        {
            ViewData["CommentingOnComment"] = Model.PartialModel.Slug;
            %><div id="reply"><%
            Html.RenderPartialFromSkin("CommentOnComment");
            %></div><%
        }
        else
        {
            %><div class="commentReply"><%=Html.Link(Model.Localize("Reply"), Url.CommentReply(Model.PartialModel), new { @class = "reply", id = Model.PartialModel.Slug })%></div><%
        }
}
        %>