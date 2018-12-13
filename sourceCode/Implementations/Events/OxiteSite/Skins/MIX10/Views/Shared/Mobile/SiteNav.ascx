<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
	string path = Request.Url.AbsolutePath.ToLower();
	string scheduleClass = path.Contains("schedule") ? "active" : string.Empty;
	string newsClass = path.Contains("news") ? "active" : string.Empty;
    string sessionsClass = path.Contains("session") ? "active" : string.Empty;
	string speakersClass = path.Contains("speaker") ? "active" : string.Empty;
%>

<ul>
   <li class="first <%=scheduleClass %>"><a href="/Schedule">Schedule</a></li>
	<li class="<%=sessionsClass %>"><a href="/Sessions">Sessions</a></li>
	<li class="<%=speakersClass %>"><a href="/Speakers">Speakers</a></li>
    <li class="<%=newsClass %>"><a href="/News">News</a></li>	
</ul>
