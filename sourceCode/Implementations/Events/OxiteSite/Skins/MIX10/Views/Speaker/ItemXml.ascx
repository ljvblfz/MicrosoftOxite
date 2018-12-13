<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItemItems<Oxite.Modules.Conferences.Models.Speaker, Oxite.Modules.Conferences.Models.ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %>
<Speaker>
    <SpeakerURI>MIX10<%= Url.Speaker(Model.Item) %>/XML</SpeakerURI>
    <Name><%= Model.Item.DisplayName %></Name>
    <Biography><%= Model.Item.Bio %></Biography>
    <Sessions>
    <% var sessions = Model.Item.ScheduleItems; %>
    <% foreach (var session in sessions) {%>
          <SessionURI><%= Url.Session(session) %></SessionURI>
    <% } %>
    </Sessions>
    <TwitterHandle />
    <BlogURL />
</Speaker>