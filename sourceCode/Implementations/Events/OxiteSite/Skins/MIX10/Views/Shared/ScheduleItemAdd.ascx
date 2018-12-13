<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%
    var userAuthenticated = Model.User.ToUserAuthenticated();
    var slug = Model.PartialModel.Slug;

    var userName = userAuthenticated != null ? userAuthenticated.Name : "";
    
    bool onSchedule;
    if (String.IsNullOrEmpty(userName))
    {
        onSchedule = false;
    }
    else
    {
        onSchedule = Model.PartialModel.IsOnSchedule(this.Html);
    }

    var addedClass = !onSchedule ? "add" : "remove";
    var buttonText = !onSchedule ? Model.Localize("AddToSchedule", "Add this to my schedule")
                         : Model.Localize("RemoveFromSchedule", "On my schedule");

    var actionUrl = !onSchedule ? string.Format("/Sessions/Add/{0}", slug)
                        : string.Format("/Sessions/Remove/{0}", slug);
    
%>
<% if(userAuthenticated != null) { %>
    <form method="post" action="<%= actionUrl %>">
	    <input class="addremove <%= addedClass %>" type="submit" value="<%= buttonText %>" />
	    <input type="hidden" class="hiddenid" value="<%= slug %>" />
    </form>
<% } %>