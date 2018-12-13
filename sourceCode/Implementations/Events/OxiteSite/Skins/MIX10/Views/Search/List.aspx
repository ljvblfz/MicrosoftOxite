<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ISearchResult>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Search.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Search.Models" %>
<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("PageType", "List", false)%></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div id="windowsSearch"><%=Html.OpenSearchOSDXLink() %></div>
    <h1><%=Model.Localize("Search") %></h1>            
    <form method="get" action="" class="search main">
        <div class="main search">           
            <div class="controlbar">
                <input id="filter" name="Term" class="text" type="text" size="42" value="<%=Request["term"].CleanAttribute() %>" />
                <input class="search button" type="submit" value="Search" />
            </div>
            <%=Html.PageState((IPageOfItems<ISearchResult>)Model.Items, (k, v) => Model.Localize(k, v)) %><%
            Html.RenderPartialFromSkin("SearchResults");
            %><%=Html.SearchResultPager((IPageOfItems<ISearchResult>)Model.Items, (k, v) => Model.Localize(k, v))%>
        </div>
        <div class="searchResultsWrapper">
            <div class="searchResults">
                <p>results by</p>
                <img src="<%= Url.Content("~/Content/images/resultsby_logo_90x35.png") %>" />
            </div> 
        </div>
    </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Search")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyTag" runat="server" ><body id="searchResults"></asp:Content>