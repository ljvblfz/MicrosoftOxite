<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Exhibitor>>" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>

<asp:Content ID="Title" ContentPlaceHolderID="Title" runat="server">
    <%= Html.PageTitle(Model.Localize("Exhibitors"))%>
</asp:Content>
<asp:Content ID="Header" ContentPlaceHolderID="ContentHeader" runat="server">
    <h1><%= Model.Localize("Event Sponsors")%></h1>    
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
<%--    <%= Html.Content("SponsorsIntro") %>--%>
    <%= Html.ManageExhibitors() %>
    <% Html.RenderPartialFromSkin("SponsorList"); %>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <% Html.RenderScriptTag("base.js");%>
</asp:Content>
