<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PostComment>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<div class="avatar"><%=Html.Gravatar(Model, "48")%></div>
<fieldset class="comment">
    <legend><%=Model.Localize("your comment") %></legend>
    <div>
        <label for="comment_body"><%=Model.Localize("Leave a comment...") %></label><%=Html.ValidationMessage("Comment.Body") %>
        <%=Html.TextArea("body", Request.Form["body"] ?? "", 12, 60, new { id = "comment_body", @class = "authed", title = Model.Localize("comment_body", "Leave a comment...") })%>
    </div>
    <div class="subscribe">
        <%=Html.CheckBox("subscribe", Request.Form.IsTrue("subscribe"), Model.Localize("Subscribe?"), !string.IsNullOrEmpty(Model.User.Email))%>
    </div>
    <div class="submit">
        <input type="submit" value="<%=Model.Localize("Submit Comment") %>" id="comment_submit" class="submit button" />
        <%=Html.OxiteAntiForgeryToken() %>
    </div>
</fieldset>