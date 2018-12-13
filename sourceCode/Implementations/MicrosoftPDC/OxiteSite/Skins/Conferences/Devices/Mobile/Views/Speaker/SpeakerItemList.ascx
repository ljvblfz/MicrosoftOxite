<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Speaker>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>

<% if (Model.Items != null && Model.Items.Count() > 0) { %>

<div class="items">
<% foreach (Speaker speaker in Model.Items) { %>
	<div class="item">
		<% Html.RenderPartialFromSkin("Speaker", new OxiteViewModelPartial<Speaker>(Model, speaker)); %>
	</div>
<%} %>
</div>

<% } else { //todo: (nheskew) need an Html.Message html helper extension method that takes a message %>
	<div class="message info"><%=Model.Localize("Speakers.NoneFound", "There were no items found.")%></div>
<% } %>