<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Speaker>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%><%
if (Model.Items != null && Model.Items.Count() > 0)
{ %><ul class="speakers medium">
<%
    int counter = 0;
    foreach (Speaker speaker in Model.Items)
    {
        StringBuilder className = new StringBuilder(25);

        if (speaker.Equals(Model.Items.First())) { className.Append(" first"); }
        if (speaker.Equals(Model.Items.Last())) { className.Append(" last"); }

        if (counter % 2 != 0) { className.Append(" odd"); }
        %>
    <li class="<%=className.ToString() %>"><%
        Html.RenderPartialFromSkin("Speaker", new OxiteViewModelPartial<Speaker>(Model, speaker)); %>
    </li><%
        counter++;
    } %>
</ul><% 
} 
else
{ //todo: (nheskew) need an Html.Message html helper extension method that takes a message %>
<div class="message info"><%=Model.Localize("Speakers.NoneFound", "There were no items found.")%></div><%        
} %>