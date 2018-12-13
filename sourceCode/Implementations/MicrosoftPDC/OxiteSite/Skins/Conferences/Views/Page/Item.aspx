<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Oxite.Modules.CMS.Models.Page>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server"><% 
    Html.RenderPartialFromSkin("ManagePage"); %>
    <h1><%=Model.Item.Title %></h1>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Content("Content") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server"><%=Html.PageTitle(Model.Item.Title)%></asp:Content>
<asp:Content ContentPlaceHolderID="MetaDescription" runat="server"><%=Html.PageDescription(Model.Item.Description) %></asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="SearchTags" runat="server"><link rel="canonical" href="<%=Url.AbsolutePath(Url.Page(Model.Item)) %>" /></asp:Content>