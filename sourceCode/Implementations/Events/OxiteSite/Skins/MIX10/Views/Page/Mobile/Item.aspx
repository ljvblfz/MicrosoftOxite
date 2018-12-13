<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Oxite.Modules.CMS.Models.Page>>" MasterPageFile="../Shared/Site.Master" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server"><%=Html.PageTitle(Model.Item.Title)%></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MetaDescription" runat="server"><%=Html.PageDescription(Model.Item.Description) %></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SearchTags" runat="server"><link rel="canonical" href="<%=Url.AbsolutePath(Url.Page(Model.Item)) %>" /></asp:Content>

<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <%=Html.Content("ContentHeader") %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Content("Content") %>
</asp:Content>

