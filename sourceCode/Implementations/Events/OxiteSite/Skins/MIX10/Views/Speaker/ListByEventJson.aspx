<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Oxite.Modules.Conferences.Models.Speaker>>" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<%@ Import Namespace="Oxite.Extensions" %>
[ <% var count = 0; foreach (var speaker in Model.Items) { count++; %>
  <% Html.RenderPartialFromSkin("ItemJson", new OxiteViewModelItemItems<Speaker, ScheduleItem>(speaker, speaker.ScheduleItems)); %>
  <% if(count < Model.Items.Count()) { %>,<% } %>		
  <% } %>
]
