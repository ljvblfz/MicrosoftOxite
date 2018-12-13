<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PostComment>>" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<fieldset class="info">
    <div id="comment_grav"><%= Html.Gravatar(Model, "65")%></div>
    <p class="gravatarhelp"><%= string.Format(Model.Localize("&lt;-- It's a {0}"), Html.Link(Model.Localize("gravatar"), "http://gravatar.com/site/signup")) %></p>
    <div class="name">
        <label for="comment_name"><%=Model.Localize("Name") %></label>
        <%
            string userName = Model.User.Name;
            if (!String.IsNullOrEmpty(Model.User.DisplayName))
                userName = Model.User.DisplayName;
        %>
        <%=Html.TextBox("name", Request.Form["name"] ?? userName, new { id = "comment_name", @class = "text", title = Model.Localize("comment_name", "Your name...") })%><%= Html.ValidationMessage("UserBase.Name", "You must provide a name.") %>
    </div>
    <div class="email">
        <label for="comment_email"><%=Model.Localize("Email") %></label>
        <%=Html.TextBox("email", Request.Form["email"] ?? Model.User.Email, new { id = "comment_email", @class = "text", title = Model.Localize("comment_email","Your email...") }) %><%= Html.ValidationMessage("PostSubscription.Email", "Valid email required to subscribe.") %><%= Html.ValidationMessage("UserBase.Email", "Invalid email.") %>
        <span><%=Model.Localize("email saved for notifications but never distributed") %></span>
    </div>
    <div class="url">
        <label for="comment_url"><%=Model.Localize("URL") %></label>
        <%=Html.TextBox("url", Request.Form["url"] ?? Model.User.Url, new { id = "comment_url", @class = "text", title = Model.Localize("comment_url", "Your home on the interwebs (URL)...") })%><%= Html.ValidationMessage("UserBase.Url", "URL looks a little off.") %>
    </div>
    <div class="remember">
        <%=Html.CheckBox("remember", Request.Form.IsTrue("remember") || !string.IsNullOrEmpty(Model.User.Email), new { id = "comment_remember" })%>
        <label for="comment_remember"><%=Model.Localize("Remember your info?") %></label>
    </div>
<%--    <div class="subscribe">
        <%=Html.CheckBox("subscribe", Request.Form.IsTrue("subscribe"), new { id = "comment_subscribe" })%>
        <label for="comment_subscribe"><%=Model.Localize("Subscribe?") %></label>
    </div>
--%>    <div class="submit">
        <input type="submit" value="<%=Model.Localize("Submit Comment") %>" id="comment_submit" class="submit button" />
    </div>
</fieldset>
<fieldset class="comment">
    <label for="comment_body"><%= Model.Localize("Leave a comment...") %></label><%=Html.ValidationMessage("Comment.Body") %>
    <%=Html.TextArea("body", Request.Form["body"] ?? "", 12, 60, new { id = "comment_body", title = Model.Localize("comment_body", "Leave a comment...") })%>
</fieldset>