<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Speaker>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.ViewModels"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<div class="controlbar">
	<form action="" method="get">
	    <%=Html.TextBox("term", Request["term"], new { id = "filter" }) %>
		<input id="filtersubmit" class="search button" type="submit" value="<%=Model.Localize("Speakers.Search", "Search") %>" />
	</form>
</div>
<div id="speakers" class="speakers">
    <% Html.RenderPartialFromSkin("ListByEvent"); %>
</div>