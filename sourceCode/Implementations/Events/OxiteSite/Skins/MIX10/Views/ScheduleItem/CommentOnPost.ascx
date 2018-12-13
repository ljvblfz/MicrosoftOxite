<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
    <form method="post" id="comment" action="/MIX10/Sessions/<%= Model.Item.Slug %>#comment" class="<%=Model.User.IsAuthenticated ? "user" : "anon" %>" ><%
        if (Request.QueryString.Get("pending") == bool.TrueString)
        { %>
        <div class="message info"><%=Model.Localize("PendingComment", "Thanks for the comment. It'll show up here pending admin approval.") %></div><%
        } %>
        <%=Html.ValidationSummary() %>
        <%
        //if (Model.User.IsAuthenticated)
        //    Html.RenderPartialFromSkin("CommentFormAuthenticated", new OxiteViewModelPartial<ScheduleItemComment>(Model, null));
        //else
        Html.RenderPartialFromSkin("CommentFormAnonymous", new OxiteViewModelPartial<ScheduleItemComment>(Model, null));
        %>
    </form>
