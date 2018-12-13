<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Dialog>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div>
    <% Html.RenderPartialFromSkin("Dialog"); %>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Scripts"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>