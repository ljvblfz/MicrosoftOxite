<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<Speaker>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>


<h2><%=Html.Link(Model.PartialModel.DisplayName.WidowControl(), Url.Speaker(Model.PartialModel))%></h2>
<% int itemCount = Model.PartialModel.ScheduleItems.Count();
	if (itemCount > 0){%>
	<p>Presenting at <%= Html.Link(string.Format("{0} Session{1}", itemCount.ToString(), itemCount > 1 ? "s" : string.Empty), Url.Speaker(Model.PartialModel))%></p>
<%} %>
