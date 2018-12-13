<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItemComment>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions" %> 
<%@ Import Namespace="Oxite.Models.Extensions" %>
<span><%=Model.Localize("said") %><br /><%=
        Html.Link(Model.PartialModel.Created.ToRelativeDateTime(), Url.Comment(Model.PartialModel)) %></span>