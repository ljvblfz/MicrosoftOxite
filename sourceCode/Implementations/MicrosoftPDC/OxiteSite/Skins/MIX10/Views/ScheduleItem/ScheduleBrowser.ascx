<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<div id="schedule">
<% Html.RenderPartialFromSkin("ScheduleForSunday"); %>
<% Html.RenderPartialFromSkin("ScheduleForMonday"); %>
<% Html.RenderPartialFromSkin("ScheduleForTuesday"); %>
<% Html.RenderPartialFromSkin("ScheduleForWednesday"); %>
</div>