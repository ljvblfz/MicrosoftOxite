<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<%@ Import Namespace="Oxite.Extensions" %><% var scheduleItem = Model.PartialModel; %>
BEGIN:VEVENT
UID:<%=scheduleItem.ID %>
URL:<%= HttpUtility.HtmlEncode(Url.AbsolutePath("/Sessions"))%>
DTEND:<%=scheduleItem.End.ToiCalendarFormat() %>
DTSTAMP:<%=DateTime.UtcNow.ToiCalendarFormat() %>
DTSTART:<%=scheduleItem.Start.ToiCalendarFormat() %>
LAST-MODIFIED:<%=scheduleItem.Modified.ToiCalendarFormat() %>
SUMMARY:<%=HttpUtility.HtmlDecode(scheduleItem.Title.Trim()) %>
<% if (!String.IsNullOrEmpty(scheduleItem.Body)){ %>DESCRIPTION:<%=HttpUtility.HtmlDecode(scheduleItem.Body.Trim())%><%}%>
<% if (!String.IsNullOrEmpty(scheduleItem.Location)){ %>LOCATION:<%=HttpUtility.HtmlDecode(scheduleItem.Location.Trim())%><%}%>
END:VEVENT
