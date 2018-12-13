<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<Oxite.Modules.Conferences.Models.ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %>
<% // Reference Schema: http://codemash.org/rest/sessions.json/The-case-for-Griffon-developing-desktop-applications-for-fun-and-profit %>
<% var session = Model.Item;
   var speaker = session.Speakers.FirstOrDefault();
%>
{
    "URI":"<%= Url.Session(session) %>/JSON",
    "Title":<%= Html.JsonEncode(session.Title) %>,
    "Abstract":<%= Html.JsonEncode(session.Body) %>,
    "Start":"<%= session.Start.ToCodeMashJsonFormat() %>",
    "Room":<%= Html.JsonEncode(session.Location) %>,
    "Difficulty":"",
    "SpeakerName":<%= Html.JsonEncode(speaker != null ? speaker.DisplayName : "") %>,<% foreach (var tag in session.Tags) {%>
    "Technology":<%= Html.JsonEncode(tag.DisplayName) %>,<% } %>
    "Track":<%= Html.JsonEncode(session.Type) %>,
    "SpeakerURI":<%= Html.JsonEncode(speaker != null ? Url.Speaker(speaker) : "")%>,
    "Lookup":<%= Html.JsonEncode(session.Slug) %>
}