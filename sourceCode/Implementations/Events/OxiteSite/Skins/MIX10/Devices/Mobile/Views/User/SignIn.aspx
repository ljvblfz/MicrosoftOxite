<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
   
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	 <h1><%=Model.Localize("User.SignIn", "Sign In") %></h1>
    <form method="post" id="signIn" action="">
        <p><%=Model.Localize("SignInHelp", "Please enter your username and password below.") %></p>
        <%=Html.ValidationSummary() %>
        <fieldset class="username">
            <label for="login_username"><%=Model.Localize("Username") %></label>
            <%=Html.TextBox("username", Request["username"], new { id = "login_username", @class = "text" }) %>
        </fieldset>
        <fieldset class="password">
            <label for="login_password"><%=Model.Localize("Password") %></label>
            <%=Html.Password("password", Request["password"], new { id = "login_password", @class = "text" })%>
        </fieldset>
        <fieldset class="remember">
            <%=Html.CheckBox("rememberMe", Request.Form.IsTrue("rememberMe"), new { id = "login_remember" })%>
            <label for="login_remember" class="forCheckbox"><%=Model.Localize("RememberUserLabel", "Remember me?") %></label>
        </fieldset>
        <input type="submit" value="<%=Model.Localize("SignInButton", "Login") %>" id="login_submit" />
    </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("SignInTitle", "Login")) %>
</asp:Content>