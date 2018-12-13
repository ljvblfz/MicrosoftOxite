<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions"%>

<asp:Content ContentPlaceHolderID="Title" runat="server">
    <% var userDisplayName = ViewData["UserDisplayName"] ?? "?"; %>
    <%= userDisplayName + "'s Schedule :: " + Html.PageTitle() %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
<div id="sessions" class="sessions">
   <% Html.RenderPartialFromSkin("UserScheduleItemsList"); %>
   <div class="pager">
       <a href="/Sessions" class="allSessions"><%=Model.Localize("see all sessions »")%></a>
   </div>
</div>
</asp:Content>
