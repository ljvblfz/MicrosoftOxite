<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.Tags.Models"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.ViewModels"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<div class="controlbar">
	<form action="" method="get">
	    <%=Html.TextBox("term", Request["term"], new { id = "filter" }) %>
		<input id="filtersubmit" class="search button" type="submit" value="<%=Model.Localize("Sessions.Search", "Search") %>" />
	</form>
	<% Html.RenderPartialFromSkin("ScheduleUserFilter"); %>
	<div id="currentTag"><%
   var scheduleItemTag = Model.GetModelItem<ScheduleItemTag>();
   if (scheduleItemTag != null)
	{ %>
	    <%=string.Format(Model.Localize("SessionBrowser.CurrentTagFormat", "Tag: {0}"), string.Format("<span class=\"current tag\">{0}</span>", scheduleItemTag.DisplayName))%>&nbsp;&nbsp;&nbsp;<%=Html.Link(Model.Localize("Filter.Remove", "&nbsp; &nbsp;"), Url.Sessions(new ScheduleItemFilterCriteria{ Term = Request["term"] }), new { @class = "closebutton", title = Model.Localize("Filter.Clear", "clear") }) %><%
	}    %></div>
</div>
<%
TagListViewModel tagListViewModel = Model.GetModelItem<TagListViewModel>();
if (tagListViewModel != null && tagListViewModel.Tags.Count() > 0)
{ %>
<div id="sessiontags">
	<h5>Tags</h5>
	<%=Html.UnorderedList(
        tagListViewModel.Tags.OrderBy(t => t.DisplayName),
        (t, i) =>  Html.Link(t.DisplayName.Ellipsize(21,s => s.CleanText()), Url.Sessions(t)), null,
        sit => scheduleItemTag != null && sit.ID == scheduleItemTag.ID ? "current tag "  : "tag "  ,
	    sit => ""   
        )%>
	<br />
	&nbsp;
</div><%
} %>