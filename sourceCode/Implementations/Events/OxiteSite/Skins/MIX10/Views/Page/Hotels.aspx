<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<string>>" MasterPageFile="../Shared/Site.master" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <h1><%=Model.Localize("Hotels") %></h1>
    <%=Html.Content("Intro") %>
    <div id="EventMap" class="map"></div>
    <%=Html.Content("Content") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Hotels"))%>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>