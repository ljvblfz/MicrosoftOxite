<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<string>>" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <h1>Platform Momentum</h1>
    <%=Html.Content("Content") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">Partner Profiles :: Microsoft PDC09</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("partners.js");%>
</asp:Content>