<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Search.Extensions" %>
<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("Section", "Sessions", false)%><%=Html.SearchTag("PageType", "List", false)%>
<%=Html.SearchTag("Section", Model.Container.Name, false)%></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
		<div id="topnav">
            <h1><%=Model.Localize("Schedule") %></h1>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Content("Content") %>
    <% Html.RenderPartialFromSkin("ScheduleBrowser"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Schedule")) %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("Sessions.js?ver=2");
    Html.RenderScriptTag("SessionBrowser.4.js?rev=04152010"); %>
    <script type="text/javascript">/*<![CDATA[*/
        pdc.sessionBrowser.which = "schedule";
        pdc.sessionBrowser.schedule.day = "<%=ViewContext.RouteData.Values["dayName"] %>";
    //]]></script>
</asp:Content>
