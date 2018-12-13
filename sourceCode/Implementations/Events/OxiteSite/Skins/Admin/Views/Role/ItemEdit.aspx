<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Role>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title"><%=Model.Item == null ? Model.Localize("Roles.Add", "Add Role") : Model.Localize("Roles.Edit", "Edit Role") %></h2>
    <%=Html.ValidationSummary() %>
    <form action="" method="post">
        <div><%=Html.TextBox("roleName", m => m.Item != null ? m.Item.Name : "", "Name", true, new { size = 20, @class = "text" })%></div>
        <div><%=Html.CheckBox("roleTypeSite", m => m.Item != null ? (m.Item.Type & RoleType.Site) == RoleType.Site : false, "Site") %></div>
        <div><%=Html.CheckBox("roleTypeBlog", m => m.Item != null ? (m.Item.Type & RoleType.Blog) == RoleType.Blog : false, "Blog") %></div>
        <div><%=Html.CheckBox("roleTypePost", m => m.Item != null ? (m.Item.Type & RoleType.Post) == RoleType.Post : false, "Post") %></div>
        <div><%=Html.CheckBox("roleTypePage", m => m.Item != null ? (m.Item.Type & RoleType.Page) == RoleType.Page : false, "Page") %></div>
        <div class="buttons">
            <input type="submit" name="addRole" class="button submit" value="<%=Model.Item == null ? Model.Localize("Roles.Add", "Add Role") : Model.Localize("Roles.Edit", "Edit Role") %>" />
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
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Roles"), Model.Item == null ? Model.Localize("Add") : Model.Localize("Edit")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>