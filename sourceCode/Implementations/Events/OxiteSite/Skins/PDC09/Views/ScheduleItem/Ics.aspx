<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %><%@ Import Namespace="Oxite.Extensions" %><%@ Import Namespace="Oxite.Modules.Conferences.Models"
%>BEGIN:VCALENDAR
VERSION:2.0
PRODID:-//MICROSOFTPDC//EN
<%Html.RenderPartialFromSkin("IcsFeedElements", Model); %>
END:VCALENDAR