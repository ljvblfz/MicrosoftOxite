<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%
    var userAuthenticated = Model.User.ToUserAuthenticated();
    var slug = Model.PartialModel.Slug;

    var userName = userAuthenticated != null ? userAuthenticated.Name : "";
    var add = Model.PartialModel.Users.SingleOrDefault(u => u.Username.Equals(userName)) == null;

    var addedClass = add ? "add" : "remove";
    var buttonText = add ? Model.Localize("AddToSchedule", "Add to my schedule")
                         : Model.Localize("RemoveFromSchedule", "On my schedule");

    var actionUrl = add ? string.Format("/Sessions/Add/{0}", slug)
                        : string.Format("/Sessions/Remove/{0}", slug);
    
%>
<% if(userAuthenticated != null) { %>
    <form method="post" action="<%= actionUrl %>">
	    <input class="addremove <%= addedClass %>" type="submit" value="<%= buttonText %>" />
	    <input type="hidden" class="hiddenid" value="<%= slug %>" />
    </form>
<% } %>