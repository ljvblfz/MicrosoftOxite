<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Search.Extensions" %>
<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("Section", "Sessions", false)%><%=Html.SearchTag("PageType", "List", false)%>
<%=Html.SearchTag("Section", Model.Container.Name, false)%></asp:Content>
<asp:Content ID="robotBlock" ContentPlaceHolderID="robots" runat="server"><meta name="robots" content="noindex,follow" /></asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
		<div id="topnav">
		    <h1>Sessions</h1>
		    <ul id="navlist">
		        <li><a href="/schedule">Master Schedule</a></li>
		        <li><a href="/workshops">Workshops</a></li>
		        <li class="ncurrent"><a href="/Sessions">Sessions</a></li>
		        <li><a href="/speakers">Speakers</a></li>
		        <li><a href="/opencall">Open Call</a></li>
		    </ul>
		</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Content("SessionsDescription") %>
    <% if (Model.User.IsInRole("Admin")) { %><a href="<%= Url.RouteUrl("SummaryReport") %>"><%= Model.Localize("See Session Stats") %></a>
    <%}%>
<div id="sessions" class="sessions">
    <% Html.RenderPartialFromSkin(ViewContext.RouteData.Values.ContainsKey("action") ? ViewContext.RouteData.Values["action"] as string : "ListByEvent"); %>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="secondaryContent" runat="server">
<div id="right">
<h4>Filter Sessions</h4>
<div id="browser">
        <% Html.RenderPartialFromSkin("SessionBrowser"); %>
    </div>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Sessions")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadCssFiles" runat="server"><%
    Html.RenderCssFile("jquery.autocomplete.css"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("Sessions.js?ver=2");
    Html.RenderScriptTag("jquery.autocomplete.js");
    Html.RenderScriptTag("sessionbrowser.4.js"); %>
    <script type="text/javascript">/*<![CDATA[*/pdc.sessionBrowser.which = "sessions";//]]></script>
    <script type="text/javascript">/*<![CDATA[*/
        pdc.sessionBrowser.sessions.speakers = ("").split("|");
        pdc.sessionBrowser.sessions.tags = ("").split("|");
    //]]></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="bodyTag" runat="server" ><body id="sessionbrowser"></asp:Content>
