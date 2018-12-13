<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>

<% 
	string pageDayName = (ViewContext.RouteData.Values["dayName"] as string ?? "").ToLower();
	string mondayClass = string.Empty;
	string tuesdayClass = string.Empty;
	string wednesdayClass = string.Empty;
	string thursdayClass = string.Empty;

	switch (pageDayName.ToLower())
	{
		case "monday":
			mondayClass = "active";
			break;
		case "tuesday":
			tuesdayClass = "active";
			break;
		case "wednesday":
			wednesdayClass = "active";
			break;
		case "thursday":
			thursdayClass = "active";
			break;
		default:
			break;
	}

   var path = Request.Url.AbsolutePath;
   var mineClass = "";
   if (path.Contains("/Mine"))
   {
      mineClass = "active";
      mondayClass = "";
      tuesdayClass = "";
      wednesdayClass = "";
      thursdayClass = "";
   }
   
   var mineHref = !path.Contains("/Mine") ? path + "/Mine" : path;  
%>

<ul>
	<li class="first <%=mondayClass %>"><%=Html.Link("Mon", Url.ScheduleForMonday()) %></li>
	<li class="<%=tuesdayClass %>"><%=Html.Link("Tues", Url.ScheduleForTuesday()) %></li>
	<li class="<%=wednesdayClass %>"><%=Html.Link("Wed", Url.ScheduleForWednesday()) %></li>
	<li class="last <%=thursdayClass %>"><%=Html.Link("Thurs", Url.ScheduleForThursday()) %></li>
	<%
    var userAuthenticated = Model.User.ToUserAuthenticated();
   %>
   <% if (userAuthenticated != null){ %>
    <li class="<%= mineClass %>">
        <a href="<%= mineHref %>" alt="my sessions">
            <%= Model.Localize("MySchedule", "My Schedule")%>
        </a>
    </li>
   <% } %>
</ul>
