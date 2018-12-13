<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Speaker>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.ViewModels"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<% 
	string section = (ViewContext.RouteData.Values["pagePath"] as string ?? string.Empty).ToLower();
	string allSessionsClass = string.Empty;
	string byTechnologyClass = string.Empty;
	string bySpeakerClass = string.Empty;

	if (string.IsNullOrEmpty(section))
	{
		section = (ViewContext.RouteData.Values["scheduleItemType"] as string ?? string.Empty).ToLower();
	}
	

	switch (section.ToLower())
	{
		case "session":
		case "sessions":
			allSessionsClass = "active";
			break;
		case "speakers":
			bySpeakerClass = "active";
			break;
		default:
			break;
   }

   var path = Request.Url.AbsolutePath;
   var mineClass = !path.Contains("/Mine") ? "" : "active"; ;
%>

<ul>
	<li class="<%=allSessionsClass %>"><a href="/Sessions">All&nbsp;Sessions</a></li>
	<li class="<%=bySpeakerClass %>"><a href="/Speakers">By&nbsp;Speaker</a></li>
	<%
    var userAuthenticated = Model.User.ToUserAuthenticated();
   %>
   <% if (userAuthenticated != null){ %>
    <li>
        <a class="<%= mineClass %>" href="Sessions/Mine" alt="my sessions">
            <%= Model.Localize("MySessions", "My Sessions")%>
        </a>
    </li>
   <% } %>
</ul>