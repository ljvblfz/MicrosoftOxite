<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<h2 class="title"><a href="<%= Url.Session(Model.PartialModel) %>"><%=Model.PartialModel.Title.WidowControl()%></a></h2>
<div class="meta clearfix">
    <ul class="clearfix">
        <%
            string speakerClass = "speaker";
            if (Model.User == null || !Model.User.IsAuthenticated)
            {
                speakerClass = "speaker";
            }
             %>
        <li class="<%=speakerClass %>"><%=Model.PartialModel.SpeakerLocationTime(Url, Html) %></li>
        <li class="overview"><% Html.RenderPartialFromSkin("ScheduleItemAdd"); %></li>
        <%
            int commentCount = Model.PartialModel.Comments.Count();
             %>
        <li class="share last"><div><%=Model.Localize("Share") %></div><% Html.RenderPartialFromSkin("ScheduleItemShare"); %></li>
        <li class="count"><a href="<%= Url.Session(Model.PartialModel) %>">Comments (<%=commentCount%>)</a></li>
    </ul>
</div>
<div class="content">
<p class="abstract"><%=Model.PartialModel.Body.CleanHtml().Ellipsize(700, s => s)%></p>
        <p class="tags"><%=Model.Localize("Tags") %>: <%
    if (Model.PartialModel.Tags.Count() > 0)
         %><%=string.Join(", ", Model.PartialModel.Tags.Where(t=> !String.IsNullOrEmpty(t.GetDisplayName())).Select(t => Html.Link(Html.Encode(t.GetDisplayName()), Url.Sessions(t), new { rel = "tag" })).ToArray()) %><%
    else
        %><%=Model.Localize("none") %>
    <% if (Model.PartialModel.Start.Year > 2009) {%>
        <br /><%=Model.Localize("Calendar") %>: <a href="<%= Url.Session(Model.PartialModel) %>/ICS"><%=Model.Localize("Add to Outlook") %></a>
        <% } %>

        </p>
</div>