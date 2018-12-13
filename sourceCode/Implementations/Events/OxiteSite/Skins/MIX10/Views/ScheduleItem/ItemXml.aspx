<%@ Page Language="C#" MasterPageFile="" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Oxite.Modules.Conferences.Models.ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.ViewModels" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.ViewModels"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<% // Reference Schema: http://codemash.org/rest/sessions/Oh-Crap-I-Forgot-or-Never-Learned-C %>
<% var session = Model.Item; %>
<Sessions xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <% Html.RenderPartialFromSkin("ItemXml", new OxiteViewModelItem<ScheduleItem>(session)); %>
</Sessions>