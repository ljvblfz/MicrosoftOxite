<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<div id="browser">
	<div id="tabs">
		<a href="/Sessions" class="tab" id="sessionstab">Sessions</a>
		<a href="/Schedule" class="tab" id="scheduletab">Schedule</a>	
		<!--<% Html.RenderPartialFromSkin("ScheduleShare"); %>-->
	</div>
	<div id="schedulegrid">
		<div class="controlbar">
		    <% Html.RenderPartialFromSkin("ScheduleUserFilter"); %>
			<ol class="days"><% string pageDayName = (ViewContext.RouteData.Values["dayName"] as string ?? "").ToLower(); %>
				<li class="first"><%=Html.Link("Monday", Url.ScheduleForMonday(), pageDayName == "monday" ? new { @class = "active" } : null) %></li>
				<li><%=Html.Link("Tuesday", Url.ScheduleForTuesday(), pageDayName == "tuesday" ? new { @class = "active" } : null) %></li>
				<li><%=Html.Link("Wednesday", Url.ScheduleForWednesday(), pageDayName == "wednesday" ? new { @class = "active" } : null) %></li>
				<li class="last"><%=Html.Link("Thursday", Url.ScheduleForThursday(), pageDayName == "thursday" ? new { @class = "active" } : null) %></li>
			</ol>
		</div>
		<div id="schedule">
            <% Html.RenderPartialFromSkin("ListByDateRangeAndUser"); %>
		</div>
	</div>
</div>