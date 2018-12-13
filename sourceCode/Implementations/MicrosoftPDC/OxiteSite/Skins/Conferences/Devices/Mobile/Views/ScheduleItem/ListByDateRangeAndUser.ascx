<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<% Html.RenderPartialFromSkin(string.Format("ScheduleFor{0}", ViewContext.RouteData.Values["dayName"])); %>