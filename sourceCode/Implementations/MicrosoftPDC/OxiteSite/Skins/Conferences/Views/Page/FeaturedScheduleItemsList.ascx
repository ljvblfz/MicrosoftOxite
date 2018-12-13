<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<% if (Model.Items != null && Model.Items.Count() > 0) { %>
<ul class="scheduleItems medium">
<%
    int counter = 0;
    foreach (ScheduleItem scheduleItem in Model.Items)
    {
        StringBuilder className = new StringBuilder(scheduleItem.Type.ToLower(), 25);

        if (scheduleItem.Equals(Model.Items.First())) { className.Append(" first"); }
        if (scheduleItem.Equals(Model.Items.Last())) { className.Append(" last"); }

        if (counter % 2 != 0) { className.Append(" odd"); }
        %>
    <li class="<%=className.ToString() %>"><%
        Html.RenderPartialFromSkin("ScheduleItem", new OxiteViewModelPartial<ScheduleItem>(Model, scheduleItem)); %>
    </li><%
        counter++;
    } %>
</ul><% 
} %>