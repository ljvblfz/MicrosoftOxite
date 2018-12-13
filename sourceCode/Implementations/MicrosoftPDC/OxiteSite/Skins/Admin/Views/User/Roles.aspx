<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItemItems<UserAuthenticated, Role>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
            <div class="sections">
                <div class="primary">
                    <h2 class="title"><%=Model.Item.Name + " " + Model.Localize("Roles") %></h2><%=
                    Html.ValidationSummary() %>
                    <form action="" method="post">
                        <div><%=Html.UnorderedList(Model.Items, r => Html.CheckBox("role", m => Model.Item.IsInRole(r.Name), r.GetDisplayName())) %></div>
                        <div><input type="submit" name="saveRoles" class="button submit" value="<%=Model.Localize("Save") %>" /></div>
                        <div><%=Html.OxiteAntiForgeryToken() %></div>
                    </form>
                </div>
                <div class="secondary">
                    <div class="sub tasks">
                        <h3><%=Model.Localize("Users.Tasks", "User Tasks") %></h3><ul><li class="first"><%=Html.Link(string.Format(Model.Localize("Users.BackToEdit", "Back to Edit '{0}'"), Model.Item.Name), Url.UserEdit(Model.Item)) %></li><li class="last"><%=Html.Link(Model.Localize("Users.ChangePassword", "Change Password"), Url.UserChangePassword(Model.Item)) %></li></ul>
                    </div>
                </div>
            </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>