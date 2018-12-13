<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"
%><?xml version="1.0" encoding="utf-8"?>
<topics><%
    Html.RenderPartialFromSkin("SignFeedElements", Model); %>
</topics>
