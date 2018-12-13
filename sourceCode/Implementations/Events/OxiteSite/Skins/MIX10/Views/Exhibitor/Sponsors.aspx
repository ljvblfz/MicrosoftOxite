﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Exhibitor>>" MasterPageFile="../Shared/Site.master" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<asp:Content ID="Title" ContentPlaceHolderID="Title" runat="server">
    <%= Html.PageTitle(Model.Localize("Sponsors"))%>
</asp:Content>
<asp:Content ID="Header" ContentPlaceHolderID="ContentHeader" runat="server">
    <%=Html.Content("ContentHeader") %>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.Content("Content") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="secondaryContent" runat="server">
    <%=Html.Content("SecondaryContent") %>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
    <% Html.RenderScriptTag("base.js");%>
</asp:Content>
<asp:Content ContentPlaceHolderID="bodyTag" runat="server"><body id="sponsors"></asp:Content>