<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<UserAuthenticated>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title"><%=Model.Localize("Users.Add", "Add User") %></h2>
    <%=Html.ValidationSummary() %>
    <form action="" method="post">
        <div><%=Html.TextBox("userName", m => "", "Name", true, new { size = 20, @class = "text" })%></div>
        <div><%=Html.TextBox("userDisplayName", m => "", "Display Name", true, new { size = 40, @class = "text" })%></div>
        <div><%=Html.TextBox("userEmail", m => "", "E-Mail Address", true, new { size = 40, @class = "text" })%></div>
        <div><%=Html.Password("userPassword", "", "Password", true, new { size = 40, @class = "text" })%></div>
        <div><%=Html.Password("userPasswordConfirm", "", "Password (Confirm)", true, new { size = 40, @class = "text" })%></div>
        <div class="buttons">
            <input type="submit" name="addUser" class="button submit" value="<%=Model.Localize("Users.Add", "Add User") %>"/>
            <%=Html.Button(
                "cancel",
                Model.Localize("Cancel"),
                new { @class = "cancel", onclick = string.Format("if (window.confirm('{0}')){{window.document.location='{1}';}}return false;", Model.Localize("really?"), Url.ManageUsers()) }
                )%>
            <%=Html.Link(
                Model.Localize("Cancel"),
                Url.ManageUsers(),
                new { @class = "cancel" })%>
            <%=Html.OxiteAntiForgeryToken() %>
        </div>
    </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Users"), Model.Localize("Add")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>