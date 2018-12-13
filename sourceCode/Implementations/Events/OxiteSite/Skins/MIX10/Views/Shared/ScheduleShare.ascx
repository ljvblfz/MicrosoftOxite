<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%
    var userAuthenticated = Model.User.ToUserAuthenticated();
    var userName = userAuthenticated != null ? userAuthenticated.Name : "";
    var scheduleSlug = string.Format("{0}://{1}/Schedule/{2}", Request.Url.Scheme, Request.Url.Authority, userName);

    var userScheduleIsPublic = ViewData["UserScheduleIsPublic"];
    var isPublic = Convert.ToBoolean(userScheduleIsPublic ?? "false");
%>
<% if(userAuthenticated != null) { %>
<div id="sharemyschedule">
	<h4>Share my schedule »</h4>
	<strong>URL:</strong>
	<span id="scheduleurl"><%= scheduleSlug %></span>
	<form action="/Schedule/Share" method="post">
		<label for="makeschedulepublic">
		    <input type="checkbox" id="makeschedulepublic" name="makeschedulepublic" <%= isPublic ? "checked" : "" %> />
		    <span><%=Model.Localize("Make my schedule public")%></span>
		</label>
		<input type="submit" id="schedulesharing" name="schedulesharing" value="Submit" />
	</form>
</div>
<% } %>