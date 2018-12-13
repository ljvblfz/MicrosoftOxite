<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>

<div id="browser">
	<div id="tabs">
		<a href="/Sessions" class="tab" id="sessionstab">Sessions</a>
		<a href="/Schedule" class="tab" id="scheduletab">Schedule</a>
		<a href="/Videos" class="tab" id="videostab">Videos</a>
		<!--<% Html.RenderPartialFromSkin("ScheduleShare"); %>-->
	</div>
	<div id="schedulegrid">
		<div class="controlbar">
		    <% var path = Request.Url.AbsolutePath;
               var pageDayName = (ViewContext.RouteData.Values["dayName"] as string ?? "");
		       var userAuthenticated = Model.User.ToUserAuthenticated();
               var mine = path.Contains("/Mine");
               var allClass = mine ? "" : "active";
               var allHref = mine ? path.Replace("/Mine", "") : path;
               var mineClass = !mine ? "" : "active";

               var addition = (path.Contains(pageDayName)) ? "/" + pageDayName : "";
               path = path.Replace(pageDayName, "").Replace("/", "").Replace("ScheduleMine", "Schedule/Mine");
		       var mineHref = !mine ? "/" + path + "/Mine" + addition : "/" + path;
		    %>
            <ol class="days"><% pageDayName = pageDayName.ToLowerInvariant(); %>
				<li class="first"><%=Html.Link("Monday", mine ? Url.MyScheduleForMonday() : Url.ScheduleForMonday(), pageDayName == "monday" ? new { @class = "active" } : null) %></li>
				<li><%=Html.Link("Tuesday", mine ? Url.MyScheduleForTuesday() : Url.ScheduleForTuesday(), pageDayName == "tuesday" ? new { @class = "active" } : null)%></li>
				<li><%=Html.Link("Wednesday", mine ? Url.MyScheduleForWednesday() : Url.ScheduleForWednesday(), pageDayName == "wednesday" ? new { @class = "active" } : null)%></li>
				<li class="last"><%=Html.Link("Thursday", mine ? Url.MyScheduleForThursday() : Url.ScheduleForThursday(), pageDayName == "thursday" ? new { @class = "active" } : null)%></li>
			</ol>
            <% if (userAuthenticated != null){ %>
            <ol class="userFilter">
                <li class="first">
                    show: 
                    <a id="allSessions" class="<%= allClass %>" href="<%= allHref %>" alt="all">
                        <%= Model.Localize("AllSessions", "all sessions")%>
                    </a>
                </li>
                <li>
                    <a id="mySessions" class="<%= mineClass %>" href="<%= mineHref %>" alt="my sessions">
                        <%= Model.Localize("MySessions", "my sessions")%>
                    </a>
                </li>
            </ol>
            <% } %>
		</div>
		<div id="schedule">
            <% Html.RenderPartialFromSkin("ListByDateRange"); %>
		</div>
	</div>
</div>