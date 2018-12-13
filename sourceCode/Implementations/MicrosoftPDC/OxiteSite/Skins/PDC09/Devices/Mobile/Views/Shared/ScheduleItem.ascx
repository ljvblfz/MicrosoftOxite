<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>


<h2><%=Html.Link(Model.PartialModel.Title.WidowControl(), Url.Session(Model.PartialModel)) %></h2>
<%if (Model.PartialModel.Speakers.Count<Speaker>() > 0)
  { %>
<p class="speakerday">
	With 
	<%=string.Join(", ", Model.PartialModel.Speakers.Select(s => Html.Link(s.DisplayName, Url.Speaker(s), new { @class = "speaker" })).ToArray()) %>
</p>
<% } %>

<%--
	Removed this as per the creative
	<% Html.RenderPartialFromSkin("ScheduleItemAdd", new OxiteViewModelPartial<ScheduleItem>(Model, Model.PartialModel)); %>
--%>