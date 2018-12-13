<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<File>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartialFromSkin("ManageFiles"); %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Scripts"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>