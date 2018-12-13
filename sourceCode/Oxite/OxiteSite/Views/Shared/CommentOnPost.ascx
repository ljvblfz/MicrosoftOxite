<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<Post>>" %>
<%@ Import Namespace="Oxite.Modules.Membership.Extensions"%>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
    <form method="post" id="comment" action="<%=Url.AddComment(Model.Item) %>#comment" class="<%=Model.GetUser().IsAuthenticated ? "user" : "anon" %>" ><%
        if (Request.QueryString.Get("pending") == bool.TrueString)
        { %>
        <div class="message info"><%=Model.Localize("PendingComment", "Thanks for the comment. It'll show up here pending admin approval.") %></div><%
        } %>
        <%=Html.ValidationSummary() %>
        <%
        if (Model.GetUser().IsAuthenticated)
            Html.RenderPartialFromSkin("CommentFormAuthenticated", new OxiteViewModelPartial<PostComment>(Model, null));
        else
            Html.RenderPartialFromSkin("CommentFormAnonymous", new OxiteViewModelPartial<PostComment>(Model, null));
        %>
    </form>
