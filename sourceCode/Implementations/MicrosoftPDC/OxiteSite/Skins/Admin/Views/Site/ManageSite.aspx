<%@ Page Language="C#" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Plugins.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title"><%=Model.Localize("ManageSite", "Manage Site")%></h2>
    <ul>
        <li><%=Html.Link(Model.Localize("Settings"), Url.Site()) %></li>
        <li><%=Html.Link(Model.Localize("Plugins"), Url.Plugins()) %></li>
    </ul>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("ManageSite", "Manage Site"))%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Scripts"><%
    Html.RenderScriptTag("base.js");
 %>
</asp:Content>