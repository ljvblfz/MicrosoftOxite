<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItemComment>>" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@Import Namespace="Oxite.Extensions" %>
<%@Import Namespace="Oxite.Models.Extensions" %><%
if (Model.PartialModel != null)
{
    Html.RenderPartialFromSkin("ManageComment"); 
%><div class="contents"><%
            if (Model.User.IsInRole("Admin") && Model.PartialModel.State == EntityState.PendingApproval)
            {
            %><span class="state" title="<%=Model.Localize("PendingApproval", "Pending Approval") %>"><%=Model.Localize("PendingApproval", "Pending Approval") %></span><%
            } %>
            <div class="name" id="<%=Model.PartialModel.Slug %>">
                <div><%=Html.LinkOrDefault(Html.Gravatar(Model.PartialModel, "65"), Model.PartialModel.CreatorUrl.CleanHref(), new { @class = "avatar" })%></div>
                <p class="comment">
                    <strong><%=Html.LinkOrDefault(Model.PartialModel.CreatorName.CleanText(), Model.PartialModel.CreatorUrl.CleanHref(), new { rel = "nofollow" })%></strong>
                    <% Html.RenderPartialFromSkin("CommentCreatorInfo"); %>
                </p>
            </div>
            <div class="text"><%
                if (Model.PartialModel.Parent != null) { %>
                <p><%=Model.Localize("ReplyTo", "In reply to") %>: <%=Html.Link(Model.PartialModel.Parent.CreatorName.CleanText(), Url.Comment(Model.PartialModel)) %></p> <%
                } %>
                <p><%=Model.PartialModel.Body.CleanCommentBody() %></p>
            </div>
        </div><%
} %>