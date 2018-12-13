<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<UserAuthenticated>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
            <div class="sections">
                <div class="primary">
                    <h2 class="title"><%=Model.Localize("ChangePassword", "Change Password") %></h2><%=
                    Html.ValidationSummary() %>
                    <form action="" method="post">
                        <div><%=Html.Password("userPassword", null, Model.Localize("NewPassword", "New Password"), new { size = 42, @class = "text" })%></div>
                        <div><%=Html.Password("userPasswordConfirm", null, Model.Localize("NewPasswordConfirm", "New Password (Confirm)"), new { size = 42, @class = "text" })%></div>
                        <div><input type="submit" name="submit" class="submit button" value="<%=Model.Localize("ChangePassword", "Change Password") %>" /><%=
                            Html.OxiteAntiForgeryToken() %></div>
                    </form>
                </div>
                <div class="secondary">
                    <div class="sub tasks">
                        <h3><%=Model.Localize("Users.Tasks", "User Tasks") %></h3><ul><li class="first"><%=Html.Link(string.Format(Model.Localize("Users.BackToEdit", "Back to Edit '{0}'"), Model.Item.Name), Url.UserEdit(Model.Item)) %></li><li class="last"><%=Html.Link(Model.Localize("Users.ManageRoles", "Manage Roles"), Url.UserRoles(Model.Item)) %></li></ul>
                    </div>
                </div>
            </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>