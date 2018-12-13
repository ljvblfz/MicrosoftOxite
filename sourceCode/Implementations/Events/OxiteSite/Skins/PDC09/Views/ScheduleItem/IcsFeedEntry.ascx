<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItem>>" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<%@ Import Namespace="Oxite.Modules.Tags.Models" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %><%ScheduleItem scheduleItem = Model.PartialModel;
%>BEGIN:VEVENT
UID:<%=scheduleItem.ID %>
DTEND:<%=scheduleItem.End.ToiCalendarFormat() %>
DTSTAMP:<%=DateTime.UtcNow.ToiCalendarFormat() %>
DTSTART:<%=scheduleItem.Start.ToiCalendarFormat() %>
LAST-MODIFIED:<%=scheduleItem.Modified.ToiCalendarFormat() %>
SUMMARY:<%=HttpUtility.HtmlDecode(scheduleItem.Title.Trim()) %>
<% if (!String.IsNullOrEmpty(scheduleItem.Body)){ %>DESCRIPTION:<%=HttpUtility.HtmlDecode(scheduleItem.Body.Trim())%><%}%>
<% if (!String.IsNullOrEmpty(scheduleItem.Location)){ %>LOCATION:<%=HttpUtility.HtmlDecode(scheduleItem.Location.Trim())%><%}%>
END:VEVENT
