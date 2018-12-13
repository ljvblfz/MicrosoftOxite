<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ISearchResult>>" MasterPageFile="../Shared/Site.Master" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Search.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Search.Models" %>

<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server">
	<%=Html.SearchTag("PageType", "List", false)%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Search")) %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">         
    <form id="search" method="get" action="">
		<label for="Term">Search Site</label>
		<input id="filter" name="Term" type="text" class="text" value="<%=Request["term"].CleanAttribute() %>" />
		<input type="image" id="searchsubmit" src="<%=ResolveClientUrl("../../Styles/images/") %>btn_search.gif" alt="Search" />
		
		<% if (!string.IsNullOrEmpty(Request["Term"])) { %>
		<p>
			You search for &quot;<%=Request.QueryString["Term"]%>&quot; returned <%= string.Format("{0} result{1}", ((IPageOfItems<ISearchResult>)Model.Items).TotalItemCount, ((IPageOfItems<ISearchResult>)Model.Items).TotalItemCount == 1 ? string.Empty : "s")%>.<br />
		</p>
		<%Html.RenderPartialFromSkin("SearchResults");%>
		<% if (Model.Items != null && ((IPageOfItems<ISearchResult>)Model.Items).TotalItemCount > 10){ %>
			<div id="searchResultPageState">
				<%= Html.PageState((IPageOfItems<ISearchResult>)Model.Items, (k, v) => Model.Localize(k, v))%>
			</div>
		<%} %>
		<%=Html.SearchResultPager((IPageOfItems<ISearchResult>)Model.Items, (k, v) => Model.Localize(k, v))%>
		<%} %>
    </form>
</asp:Content>

