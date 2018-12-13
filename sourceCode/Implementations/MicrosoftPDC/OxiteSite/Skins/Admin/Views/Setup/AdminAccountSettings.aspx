<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master"
    Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<UserInputAdd>>" %>

<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Membership.Models" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%=Model.Localize("Site.Add", "Initial Site Setup") %></h2>
    <%=Html.ValidationSummary() %>
    <form action="" method="post" id="siteSettings">
    <div>
        <%=Html.TextBox("userName", m => "", "Username", true, new { size = 20, @class = "text" })%></div>
    <div>
        <%=Html.TextBox("userDisplayName", m => "", "Display Name", true, new { size = 20, @class = "text" })%></div>
    <div>
        <%=Html.TextBox("userEmail", m => "", "Email", true, new { size = 20, @class = "text" })%></div>
    <div>
        <%=Html.Password("userPassword", "", "Password", true, new { size = 40, @class = "text" })%></div>
    <div>
        <%=Html.Password("userPasswordConfirm", "", "Password (Confirm)", true, new { size = 40, @class = "text" })%></div>
    <div class="buttons">
        <input type="submit" name="submit" class="button submit" value="<%=Model.Localize("Setup.ImportBlog", "Next: Import Blog")%>" />
    </div>
    </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Site"), Model.Localize("Setup")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <%
        Html.RenderScriptTag("base.js"); %>
</asp:Content>
