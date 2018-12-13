<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Modules.Search.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<form id="search" method="get" action="<%=Url.Search() %>">
    <fieldset>
		<label for="globalsearchterm"><%=Model.Localize("Search") %>:</label>
        <%= Html.TextBox("term", "", new { id = "globalsearchterm", @class = "text" })%>
		<input class="search button" type="submit" value="<%=Model.Localize("Site.Search", "Search") %>" />
	</fieldset>
</form>