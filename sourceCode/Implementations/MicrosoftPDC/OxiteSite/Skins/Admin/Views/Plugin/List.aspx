<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<PluginContainer>>" %>
<%@ Import Namespace="Oxite.Plugins"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Infrastructure" %>
<%@ Import Namespace="Oxite.Modules.Plugins.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div id="refreshPlugins" class="info message">
        <form method="POST" action="<%=Url.PluginsNotInstalledRefresh() %>">
            <%=Model.Localize("Plugins.RefreshMessage", "If you have uploaded new plugins then you might need to refresh the list in order for them to be discovered.") %>
            <input type="image" src="<%=Url.CssPath("images/arrow_refresh.png", ViewContext) %>" alt="<%=Model.Localize("Refresh") %>" title="<%=Model.Localize("Refresh") %>" class="ibutton image add" />
            <%=Html.OxiteAntiForgeryToken() %>
        </form>
    </div>
    <% bool? installed = (bool?) ViewContext.RouteData.Values["installed"]; %>
    <ul class="tab links" id="pluginListTabs">
        <li id="allPlugins" class="first<%=installed == null ? " active" : "" %>"><%=Html.Link(Model.Localize("Plugins.All", "All"), Url.Plugins()) %></li>
        <li id="installedPlugins"<%=installed != null && (bool)installed ? " class=\"active\"" : "" %>><%=Html.Link(Model.Localize("Plugins.Installed", "Installed"), Url.PluginsInstalled()) %></li>
        <li id="notInstalledplugins" class="last<%=installed != null && !(bool)installed ? " active" : "" %>"><%=Html.Link(Model.Localize("Plugins.NotInstalled", "Not Installed"), Url.PluginsNotInstalled()) %></li>
    </ul>
    <div class="sections plugins">
        <div class="lone" id="pluginList">
            <% Html.RenderPartialFromSkin("List"); %>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Plugins")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <h2><%=Model.Localize("Plugins")%></h2>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js");
 %>
</asp:Content>