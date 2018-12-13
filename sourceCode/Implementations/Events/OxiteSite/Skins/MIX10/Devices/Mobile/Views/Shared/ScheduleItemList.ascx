<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>


<%
if (Model.Items != null && Model.Items.Count() > 0)
{ %><ul class="scheduleItems medium">
<%
    int counter = 0;
    string lastStartTime = "";

    foreach (ScheduleItem scheduleItem in Model.Items)
    {
        StringBuilder className = new StringBuilder(scheduleItem.Type.ToLower(), 25);

        if (scheduleItem.Equals(Model.Items.First())) { className.Append(" first"); }
        if (scheduleItem.Equals(Model.Items.Last())) { className.Append(" last"); }

        if (counter % 2 != 0) { className.Append(" odd"); }
        string startTime = "";

        if (scheduleItem.Start.Year > 2000)
        {
            startTime = scheduleItem.Start.ToString("dddd h:mm tt");
        }
        
        %>
    <li class="<%=className.ToString() %>"><%
                                               if (lastStartTime != startTime)
                                               { %>
<h2 class="timeHeading"><% = startTime %></h2>
                                              <%
                                                   lastStartTime = startTime;
                                               }
        Html.RenderPartialFromSkin("ScheduleItem", new OxiteViewModelPartial<ScheduleItem>(Model, scheduleItem)); %>
    </li><%
        counter++;
    } %>
</ul><% 
} 
else
{ //todo: (nheskew) need an Html.Message html helper extension method that takes a message %>
<div class="message info"><%=Model.Localize("Schedule.NoneFound", "There were no items found.")%></div><%        
} %>