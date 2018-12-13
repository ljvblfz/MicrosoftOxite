<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>

<% var sessions = Model.Items.Where(si => si.Start == Model.Item.DateRange.StartDate); %>

<tr>
	<td class="time"><%=Model.Item.DateRange.StartDate.ToString("h:mmtt").ToLower()%> - <%=Model.Item.DateRange.EndDate.ToString("h:mmtt").ToLower()%></td>
	<td class="desc"><%=Html.Encode(Model.Item.Label)%><%=sessions.Count() > 0 ? string.Format(" ({0})", sessions.Count()) : string.Empty %></td>
</tr>