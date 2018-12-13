<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<Oxite.Modules.Conferences.Models.ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %>
<% // Reference Schema: http://codemash.org/rest/sessions.json/The-case-for-Griffon-developing-desktop-applications-for-fun-and-profit %>
<% var session = Model.Item;
   var speaker = session.Speakers.FirstOrDefault();
%>
<Session>
    <URI><%= Html.Encode(Url.Session(session))%>/XML</URI>
    <Title><%= session.Title %></Title>
    <Abstract><%= session.Body %></Abstract> <% //Ellipsize(100,s => s.CleanText()) %>
    <Start><%= session.Start.ToCodeMashXmlFormat() %></Start>
    <Room><%= session.Location %></Room>
    <Difficulty></Difficulty>
    <SpeakerName><%= speaker != null ? speaker.DisplayName : "" %></SpeakerName>
    <% foreach (var tag in session.Tags) {%> <Technology><%=tag.DisplayName%></Technology> <% } %>
    <Track><%= session.Type %></Track>
    <SpeakerURI><%= Html.Encode(speaker != null ? Url.Speaker(speaker) : "") %>/XML</SpeakerURI>
</Session>