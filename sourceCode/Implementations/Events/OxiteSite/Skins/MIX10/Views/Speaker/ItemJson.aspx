<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItemItems<Oxite.Modules.Conferences.Models.Speaker, Oxite.Modules.Conferences.Models.ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
[ <% Html.RenderPartialFromSkin("ItemJson", new OxiteViewModelItemItems<Speaker, ScheduleItem>(Model.Item, Model.Items)); %>
]