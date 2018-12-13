<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("Section", "Blog", false)%><%=Html.SearchTag("PageType", "List", false)%>
<%=Html.SearchTag("Section", Model.Container.Name, false)%></asp:Content>
<asp:Content ID="robotBlock" ContentPlaceHolderID="robots" runat="server"><meta name="robots" content="noindex,follow" /></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1><% Html.RenderPartialFromSkin("ArchiveBreadcrumb"); %></h1>
    <%=Html.PageState((IPageOfItems<Post>)Model.Items, (k, v) => Model.Localize(k, v)) %><% 
    Html.RenderPartialFromSkin("PostListMedium");
    %><%=Html.PostArchiveListPager((IPageOfItems<Post>)Model.Items, (k, v) => Model.Localize(k, v)) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server"><%
    ArchiveData archiveData = ((ArchiveContainer)Model.Container).ArchiveData; %>
    <%=Html.PageTitle(
        Model.Localize("ArchiveTitle","Archives"),
        archiveData.Year.ToString(),
        archiveData.Month > 0 ? new DateTime(archiveData.Year, archiveData.Month, 1).ToString("MMMM") : null,
        archiveData.Day > 0 ? archiveData.Day.ToString() : null
        ) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>
