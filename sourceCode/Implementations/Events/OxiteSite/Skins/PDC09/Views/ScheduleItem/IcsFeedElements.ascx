<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %><%
    foreach (ScheduleItem scheduleItem in Model.Items)
    {
        if (scheduleItem.Start != scheduleItem.End)
        {
            Html.RenderPartialFromSkin("IcsFeedEntry", new OxiteViewModelPartial<ScheduleItem>(Model, scheduleItem));
        }
    } %>