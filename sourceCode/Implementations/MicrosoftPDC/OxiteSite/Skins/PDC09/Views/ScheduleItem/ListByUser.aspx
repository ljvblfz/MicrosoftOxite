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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
    <h1><%=Model.Localize("Sessions") %></h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Content("SessionsDescription") %>
    <div id="browser">
	    <div id="tabs">
		    <a href="/Sessions" class="tab" id="sessionstab">Sessions</a>
		    <a href="/Schedule" class="tab" id="scheduletab">Schedule</a>
		    <!--<% Html.RenderPartialFromSkin("ScheduleShare"); %>-->
	    </div>
        <%  Html.RenderPartialFromSkin("MySessionBrowser"); %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Sessions")) %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="HeadCssFiles" runat="server"><%
    Html.RenderCssFile("jquery.autocomplete.css"); %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts" runat="server"><%
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
