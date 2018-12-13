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
<ol class="userFilter">
    <li class="first">
        show: 
        <a class="<%= allClass %>" href="<%= allHref %>" alt="all">
            <%= Model.Localize("AllSessions", "all sessions")%>
        </a>
    </li>
    <li>
        <a class="<%= mineClass %>" href="<%= mineHref %>" alt="my sessions">
            <%= Model.Localize("MySessions", "my sessions")%>
        </a>
    </li>
</ol>
<% } %>