<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<UserAuthenticated>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
            <div class="sections">
                <div class="primary">
                    <h2 class="title"><%=Model.Localize("Users.Edit", "Edit User") %></h2>
                    <%=Html.ValidationSummary() %>
                    <form action="" method="post">
                        <div><%=Html.TextBox("userName", m => m.Item.Name, "Name", true, new { size = 20, @class = "text" })%></div>
                        <div><%=Html.TextBox("userDisplayName", m => m.Item.DisplayName, "Display Name", true, new { size = 40, @class = "text" })%></div>
                        <div><%=Html.TextBox("userEmail", m => m.Item.Email, "E-Mail Address", true, new { size = 40, @class = "text" })%></div>
                        <div class="buttons">
                            <input type="submit" name="addUser" class="button submit" value="<%=Model.Localize("Users.Edit", "Edit User") %>"/>
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
                </div>
                <div class="secondary">
                    <div class="sub tasks">
                        <h3><%=Model.Localize("Users.Tasks", "User Tasks") %></h3><ul><li class="first"><%=Html.Link(Model.Localize("Users.ChangePassword", "Change Password"), Url.UserChangePassword(Model.Item)) %></li><li class="last"><%=Html.Link(Model.Localize("Users.ManageRoles", "Manage Roles"), Url.UserRoles(Model.Item)) %></li></ul>
                    </div>
                </div>
            </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Users"), Model.Localize("Edit")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>