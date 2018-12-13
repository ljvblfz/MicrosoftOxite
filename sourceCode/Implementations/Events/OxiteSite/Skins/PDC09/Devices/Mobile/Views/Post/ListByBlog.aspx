<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Site.HasMultipleBlogs ? Model.Container.GetDisplayName() : null) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MetaDescription" runat="server">
    <%=Html.PageDescription(((Blog)Model.Container).Description) %>
</asp:Content>

<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server">
	<%=Html.SearchTag("Section", "Blog", false)%>
	<%=Html.SearchTag("PageType", "List", false)%>
	<%=Html.SearchTag("Section", Model.Container.Name, false)%>
</asp:Content>

<asp:Content ID="subNav" ContentPlaceHolderID="ContentHeader" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<% Html.RenderPartialFromSkin("PostListMedium"); %>
	<div id="blogPagerContainer">
		<%= Html.PageState((IPageOfItems<Post>)Model.Items, (k, v) => Model.Localize(k, v))%>
		<%= Html.PostListByBlogPager((IPageOfItems<Post>)Model.Items, (k,v) => Model.Localize(k,v), Model.Container.Name) %>
	</div>
</asp:Content>





