<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItemItems<Oxite.Modules.Conferences.Models.Speaker, Oxite.Modules.Conferences.Models.ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<Speakers xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
      <% Html.RenderPartialFromSkin("ItemXml", new OxiteViewModelItemItems<Speaker,ScheduleItem>(Model.Item, Model.Items)); %>
</Speakers>
