<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<% 
    var sessions = Model.Items.Where(si => si.Start >= Model.Item.DateRange.StartDate && si.End <= Model.Item.DateRange.EndDate);
%>
<% if (sessions.Count() > 0) { %>
        	<tr class="expandableParent">
<%}else{%>
        	<tr>
<%}%>
        		<td class="time"><%=Model.Item.DateRange.StartDate.ToString("h:mm tt")%> – <%= Model.Item.DateRange.EndDate.ToString("h:mm tt")%></td>
                <td class="event"><%=Model.Item.Label %><% if (sessions.Count() > 0) { %> <span class="count">(<%=sessions.Count()%>)</span><%}%></td>
                <td class="location"><% if (sessions.Count() > 0) { %><div><a class="expand" href="#" id="expand_<%=Model.Item.DateRange.StartDate.ToString("ddhhmm")%>">show</a></div><%}else{%>&nbsp;<%}%></td>
        	</tr>
        	<tr class="expandableChild"><td colspan="3">
<% if (sessions.Count() > 0) { %>
<div class="sessions expand_<%=Model.Item.DateRange.StartDate.ToString("ddhhmm")%>">
    <% Html.RenderPartialFromSkin(
           "ScheduleItemList",
           new OxiteViewModelItems<ScheduleItem>(
               sessions,
               Model
               )
           ); %>
</div><%
} %>
</td></tr>