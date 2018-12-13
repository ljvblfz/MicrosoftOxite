<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Oxite.Modules.Conferences.Models.ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<%@ Import Namespace="Oxite.ViewModels" %>
<Sessions xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <% foreach(var session in Model.Items) { %>
    <% Html.RenderPartialFromSkin("ItemXml", new OxiteViewModelItem<ScheduleItem>(session)); %>
<% } %>
</Sessions>

