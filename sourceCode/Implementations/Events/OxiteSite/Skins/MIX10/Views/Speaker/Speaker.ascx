<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<Speaker>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<h3><%=Html.Link(Model.PartialModel.DisplayName.WidowControl(), Url.Speaker(Model.PartialModel))%></h3>
<% if (!String.IsNullOrEmpty(Model.PartialModel.Bio)) {%>
<p class="smallBio" style="min-height:65px;"><%
       string speakerImage = Url.SpeakerImage(Model.PartialModel, "sml");

     if (speakerImage != null)
     {
%>
<a href="<%=Url.Speaker(Model.PartialModel)%>" title="<%=Model.PartialModel.DisplayName%>" class="headshot">
<%=Html.Image(speakerImage, Model.PartialModel.DisplayName, null)%>
</a>
<%
     }

%><%=Model.PartialModel.Bio.Ellipsize(300, s => s) %></p>
<%} %>