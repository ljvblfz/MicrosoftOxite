<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItemItems<Oxite.Modules.Conferences.Models.Speaker, Oxite.Modules.Conferences.Models.ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %>
<%@ Import Namespace="Oxite.Extensions" %>
{
	"SpeakerURI":<%= Html.JsonEncode("MIX10/Speakers/" + Model.Item.Name + "/JSON") %>,
	"Name":<%= Html.JsonEncode(Model.Item.DisplayName) %>,
	"SpeakerLookup":<%= Html.JsonEncode(Model.Item.Name) %>,
	"Biography":<%= Html.JsonEncode(Model.Item.Bio) %>,
	"Sessions": [
	    <% var count = 0; foreach (var session in Model.Items) { count++; %>
		<%= Html.JsonEncode(Url.Session(session))%>
		<% if(count < Model.Items.Count()) { %>,<% } %>		
		<% } %>],
	"ContactInfo":null,
	"TwitterHandle":"",
	"BlogURL":""
}