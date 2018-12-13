<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../../../Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="sections">
    <div class="primary">
        <form method="post" id="signIn" action="" class="login">
            <h2 class="title"><%=Model.Localize("SignInTitle", "Login")%></h2>
            <div><%=Model.Localize("SignInHelp", "Please enter your username and password below.") %></div>
            <%=Html.ValidationSummary() %>
            <div class="username">
                <label for="login_username"><%=Model.Localize("Username") %></label>
                <%=Html.TextBox("username", Request["username"], new { id = "login_username", @class = "text" }) %>
            </div>
            <div class="password">
                <label for="login_password"><%=Model.Localize("Password") %></label>
                <%=Html.Password("password", Request["password"], new { id = "login_password", @class = "text" })%>
            </div>
            <div class="remember">
                <%=Html.CheckBox("rememberMe", Request.Form.IsTrue("rememberMe"), new { id = "login_remember" })%>
                <label for="login_remember"><%=Model.Localize("RememberUserLabel", "Remember me?") %></label>
            </div>
            <div class="submit">
                <input type="submit" value="<%=Model.Localize("SignInButton", "Login") %>" id="login_submit" class="submit button" />
            </div>
        </form>
    </div>
    <div class="secondary"><%
        Html.RenderPartialFromSkin("SideBar"); %>
    </div>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("SignInTitle", "Login")) %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>