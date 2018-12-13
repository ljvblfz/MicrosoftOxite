<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%
    var path = Request.Url.AbsolutePath;
    var allClass = path.Contains("/Mine") ? "": "active";
    var allHref = path.Contains("/Mine") ? path.Replace("/Mine", "") : path;
    var mineClass = !path.Contains("/Mine") ? "" : "active";
    var mineHref = !path.Contains("/Mine") ? path + "/Mine" : path;
%>
<%
    var userAuthenticated = Model.User.ToUserAuthenticated();
%>
<% if (userAuthenticated != null){ %>
<h5>Show Me:</h5>
<ul id="showmetabs">
<li>
        <a id="showallsessions" class="<%= allClass %> first1" href="<%= allHref %>" alt="all">
            <%= Model.Localize("AllSessions", "All Sessions")%>
        </a>
</li>
<li>
        <a id="showmysessions" class="<%= mineClass %>" href="<%= mineHref %>" alt="my sessions">
            <%= Model.Localize("MySessions", "My Sessions")%>
        </a>
</li>
</ul>    
<% } %>