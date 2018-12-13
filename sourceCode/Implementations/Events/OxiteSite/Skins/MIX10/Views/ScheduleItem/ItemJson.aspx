<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Oxite.Modules.Conferences.Models.ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
[ <% Html.RenderPartialFromSkin("ItemJson", new OxiteViewModelItem<ScheduleItem>(Model.Item)); %>
]