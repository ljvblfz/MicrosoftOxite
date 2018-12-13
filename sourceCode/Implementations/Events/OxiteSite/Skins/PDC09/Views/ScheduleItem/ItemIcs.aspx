<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<ScheduleItem>>" %><%@ Import Namespace="Oxite.Extensions"%><%@ Import Namespace="Oxite.Modules.Conferences.Extensions"%><%@ Import Namespace="Oxite.Modules.Conferences.Models"%><%var scheduleItem = Model.Item;%>
BEGIN:VCALENDAR
PRODID:-//Microsoft Corporation//Outlook 12.0 MIMEDIR//EN
VERSION:2.0
METHOD:PUBLISH
X-MS-OLK-FORCEINSPECTOROPEN:TRUE
BEGIN:VEVENT
CLASS:PUBLIC
CREATED:<%=DateTime.UtcNow.ToiCalendarFormat() %>
<% if (!String.IsNullOrEmpty(scheduleItem.Body)){ %>DESCRIPTION:<%=scheduleItem.Body.Replace("\r","").Replace("\n","").Trim() %><%} %>
DTEND:<%=scheduleItem.End.ToiCalendarFormat() %>
DTSTAMP:<%=DateTime.UtcNow.ToiCalendarFormat() %>
DTSTART:<%=scheduleItem.Start.ToiCalendarFormat() %>
LAST-MODIFIED:<%=scheduleItem.Modified.ToiCalendarFormat() %>
<% if (!String.IsNullOrEmpty(scheduleItem.Location)){ %>LOCATION:<%=scheduleItem.Location.Trim() %><%} %>
PRIORITY:5
SEQUENCE:0
SUMMARY;LANGUAGE=en-us:<% if (!String.IsNullOrEmpty(scheduleItem.Title)){ %><%=scheduleItem.Title.Trim() %><%} %>
TRANSP:OPAQUE
UID:<%=scheduleItem.ID %>
X-MICROSOFT-CDO-BUSYSTATUS:BUSY
X-MICROSOFT-CDO-IMPORTANCE:1
X-MICROSOFT-DISALLOW-COUNTER:FALSE
X-MS-OLK-ALLOWEXTERNCHECK:TRUE
X-MS-OLK-AUTOFILLLOCATION:FALSE
X-MS-OLK-CONFTYPE:0
BEGIN:VALARM
TRIGGER:-PT15M
ACTION:DISPLAY
DESCRIPTION:Reminder
END:VALARM
END:VEVENT
END:VCALENDAR