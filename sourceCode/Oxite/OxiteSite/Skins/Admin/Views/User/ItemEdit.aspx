<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<User>>" %>
<%@ Import Namespace="Oxite.Infrastructure"%>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title"><%=Model.Localize("Users.Edit", "Edit User") %></h2>
    <%=Html.ValidationSummary() %>
    <form action="" method="post">
        <div><%=Html.TextBox("userName", m => m.Item.Name, "Name", true, new { size = 20, @class = "text" })%></div>
        <div><%=Html.TextBox("userDisplayName", m => m.Item.DisplayName, "Display Name", true, new { size = 40, @class = "text" })%></div>
        <div><%=Html.TextBox("userEmail", m => m.Item.Email, "E-Mail Address", true, new { size = 40, @class = "text" })%></div>
        <div class="buttons">
            <input type="submit" name="addUser" class="button submit" value="<%=Model.Localize("Users.Edit", "Edit User") %>"/>
            <%=Html.Link(
                Model.Localize("Cancel"),
                Url.ManageUsers(),
                new { @class = "cancel" })%>
            <%=Html.OxiteAntiForgeryToken() %>
        </div>
    </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Users"), Model.Localize("Edit")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>