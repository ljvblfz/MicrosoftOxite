<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%><%
var sessions = Model.Items.Where(si => si.Start == Model.Item.DateRange.StartDate);
 %>
<div class="timeslot<%=sessions.Count() > 0 ? " hassessions" : "" %>">
	<h3><%=Model.Item.DateRange.StartDate.ToString("h:mm tt")%> - <%=Model.Item.DateRange.EndDate.ToString("h:mm tt")%></h3>
	<div class="timeslotdesc"><span class="sessionlabel"><%=Model.Item.Label%><%=sessions.Count() > 0 ? string.Format(" ({0})", sessions.Count()) : "" %></span></div>
</div><%
if (sessions.Count() > 0) { %>
<div class="sessions">
    <% Html.RenderPartialFromSkin(
           "ScheduleItemList",
           new OxiteViewModelItems<ScheduleItem>(
               Model.Items.Where(si => si.Start == Model.Item.DateRange.StartDate),
               Model
               )
           ); %>
</div><%
} %>