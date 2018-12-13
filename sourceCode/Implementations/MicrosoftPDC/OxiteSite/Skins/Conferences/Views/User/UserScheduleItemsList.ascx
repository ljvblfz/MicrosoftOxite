<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<% var userDisplayName = ViewData["UserDisplayName"] ?? "?"; %>
<h2><%= userDisplayName %>'s Sessions</h2>
<%
    var showCheckbox = false;
    var userAuthenticated = Model.User.ToUserAuthenticated();
    if(userAuthenticated != null)
    {
        if(userDisplayName.Equals(userAuthenticated.DisplayName))
        {
            showCheckbox = true;
        }
    }
    var userScheduleIsPublic = ViewData["UserScheduleIsPublic"];
    var isChecked = Convert.ToBoolean(userScheduleIsPublic ?? "false");
%>
<% if (showCheckbox)
   { %>
    <form action="/Schedule/Share" method="post">
	    <label for="makeschedulepublic">
	        <input type="checkbox" id="makeschedulepublic" name="makeschedulepublic" <%= isChecked ? "checked" : "" %> />
	        <span><%=Model.Localize("Make my sessions public")%></span>
	    </label>
	    <div style="display:none;">
	        <input type="submit" id="schedulesharing" name="schedulesharing" value="Submit" />
	    </div>
    </form>	
<% } %>
<% if (Model.Items != null && Model.Items.Count() > 0) { %>
    <ul class="scheduleItems medium">
    <%
        int counter = 0;
        foreach (var scheduleItem in Model.Items)
        {
            var className = new StringBuilder(scheduleItem.Type.ToLower(), 25);

            if (scheduleItem.Equals(Model.Items.First())) { className.Append(" first"); }
            if (scheduleItem.Equals(Model.Items.Last())) { className.Append(" last"); }

            if (counter % 2 != 0) { className.Append(" odd"); }
    %>
    <li class="<%=className.ToString() %>"><%
        Html.RenderPartialFromSkin("ScheduleItem", new OxiteViewModelPartial<ScheduleItem>(Model, scheduleItem)); %>
    </li><% counter++; } %>
</ul>
<% } else {%>
    <p><%=Model.Localize("ScheduleEmptyOrPrivate", "This user has nothing scheduled, or has a private schedule.")%></p>
<% }%>    