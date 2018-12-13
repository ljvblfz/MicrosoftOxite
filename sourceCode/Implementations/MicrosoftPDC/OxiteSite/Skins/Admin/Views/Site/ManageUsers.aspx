<%@ Page Language="C#" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title"><%=Model.Localize("ManageUsers", "Manage Users")%></h2>
    <ul>
        <li><%=Html.Link(Model.Localize("Users.Find", "Find Users"), Url.UserFind()) %></li>
        <li><%=Html.Link(Model.Localize("Users.Add", "Add User"), Url.UserAdd()) %></li>
        <li><%=Html.Link(Model.Localize("Roles.Find", "Find Roles"), Url.RoleFind()) %></li>
    </ul>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("ManageUsers", "Manage Users"))%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Scripts"><%
    Html.RenderScriptTag("base.js");
 %>
</asp:Content>