<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Oxite.Modules.Conferences.Models.ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
[<% var count = 0; foreach (var session in Model.Items) { count++;%>
    <% Html.RenderPartialFromSkin("ItemJson", new OxiteViewModelItem<ScheduleItem>(session)); %>
    <% if (count < Model.Items.Count()) {%>,<% } %>    
<% } %>]