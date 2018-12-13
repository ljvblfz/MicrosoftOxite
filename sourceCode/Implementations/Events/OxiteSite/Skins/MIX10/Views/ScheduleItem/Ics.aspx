<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %><%@ Import Namespace="Oxite.Extensions" %><%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="OxiteSite" %><% var @event = ConfigurationResolver.GetEventName();
   string prodId;
   switch(@event)
   {
       case "pdc09":
           prodId = "MICROSOFTPDC";
           break;
        case "mix10":
           prodId = "MICROSOFTMIX";
           break;
        default:
           prodId = "MICROSOFTEVENTS";
           break;
   }
%>BEGIN:VCALENDAR
VERSION:2.0
PRODID:-//<%= prodId %>//EN<%Html.RenderPartialFromSkin("IcsFeedElements", Model); %>END:VCALENDAR