<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.Tags.Models"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.ViewModels"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<% 

	string path = Request.Url.AbsolutePath.ToLower();
	
	string allSessionsClass = path.Contains("session") ? "active" : string.Empty;
	string bySpeakerClass = path.Contains("speaker") ? "active" : string.Empty;
	string mineClass = path.Contains("/mine") ? "active" : string.Empty;
	   
%>

<ul>
	<li class="<%=allSessionsClass %>"><a href="/Sessions">All&nbsp;Sessions</a></li>
	<li class="<%=bySpeakerClass %>"><a href="/Speakers">By&nbsp;Speaker</a></li>
	<%
    var userAuthenticated = Model.User.ToUserAuthenticated();
   %>
   <% if (userAuthenticated != null){ %>
    <li>
        <a class="<%= mineClass %>" href="/Sessions/Mine" alt="my sessions">
            <%= Model.Localize("MySessions", "My Sessions")%>
        </a>
    </li>
   <% } %>
</ul>