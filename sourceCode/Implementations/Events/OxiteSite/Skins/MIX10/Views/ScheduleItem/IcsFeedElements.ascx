<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %><%
    foreach (var scheduleItem in Model.Items)
    {
        if (scheduleItem.Start != scheduleItem.End)
        {
            Html.RenderPartialFromSkin("IcsFeedEntry", new OxiteViewModelPartial<ScheduleItem>(Model, scheduleItem));
        }
    } %>