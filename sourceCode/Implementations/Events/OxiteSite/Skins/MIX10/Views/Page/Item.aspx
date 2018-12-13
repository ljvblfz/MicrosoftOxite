<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Oxite.Modules.CMS.Models.Page>>" MasterPageFile="../Shared/Site.master" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<asp:Content ContentPlaceHolderID="bodyTag" runat="server">
    <body id="<%= Model.Item.Slug%>">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <%=Html.Content("ContentHeader") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%Html.RenderPartialFromSkin("ManagePage"); %>
    <%=Html.Content("Content") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="secondaryContent" runat="server">
    <%=Html.Content("SecondaryContent") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server"><%=Html.PageTitle(Model.Item.Title)%></asp:Content>
<asp:Content ContentPlaceHolderID="MetaDescription" runat="server"><%=Html.PageDescription(Model.Item.Description) %></asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptVariablesPost" runat="server">
    <script type="text/javascript">
        $(function() {
            $('a.lightbox').lightBox(); // Select all links with lightbox class
        });
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="SearchTags" runat="server"><link rel="canonical" href="<%=Url.AbsolutePath(Url.Page(Model.Item)) %>" /></asp:Content>