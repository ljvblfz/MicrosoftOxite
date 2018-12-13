<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage<OxiteModelList<File>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartialFromSkin("ManageFiles"); %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Scripts"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>