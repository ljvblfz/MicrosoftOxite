<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("User.Register.Title", "Register")) %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h1><%=Model.Localize("User.Register", "Register") %></h1>
    <form method="post" id="register" action="" class="register">
        <p><%=Model.Localize("User.Register.Help", "This is some text explaining why we need this information and what it will be used for.") %></p>
        <%=Html.ValidationSummary() %>
        <fieldset class="username">
            <label for="username"><%=Model.Localize("Username") %></label>
            <%=Html.TextBox("username", Request["username"] ?? ((string)Model.User.ToUserUnregistered().AuthenticationValues["FirstName"] + (string)Model.User.ToUserUnregistered().AuthenticationValues["LastName"]), new { id = "username", @class = "text" })%>            
        </fieldset>
        <fieldset class="displayName">
            <label for="displayName"><%=Model.Localize("Display Name") %></label>
            <%=Html.TextBox("displayName", Request["displayName"] ?? string.Format("{0} {1}", Model.User.ToUserUnregistered().AuthenticationValues["FirstName"], Model.User.ToUserUnregistered().AuthenticationValues["LastName"]), new { id = "displayName", @class = "text" })%>            
        </fieldset>
        <input type="submit" value="Register" />
    </form>
</asp:Content>
