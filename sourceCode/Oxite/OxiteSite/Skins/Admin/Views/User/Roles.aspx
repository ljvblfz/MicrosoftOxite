<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItemItems<User, Role>>" %>
<%@ Import Namespace="Oxite.Infrastructure"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title"><%=Model.Item.Name + " " + Model.Localize("Roles") %></h2><%=
    Html.ValidationSummary() %>
    <form action="" method="post">
        <div><%=Html.UnorderedList(Model.Items, r => Html.CheckBox("role", m => Model.Item.IsInRole(r.Name), r.GetDisplayName())) %></div>
        <div><input type="submit" name="saveRoles" class="button submit" value="<%=Model.Localize("Save") %>" /></div>
        <div><%=Html.OxiteAntiForgeryToken() %></div>
    </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>