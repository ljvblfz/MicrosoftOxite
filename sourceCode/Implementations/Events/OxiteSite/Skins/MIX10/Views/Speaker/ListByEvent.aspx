<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Speaker>>" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("Section", "Speakers", false)%><%=Html.SearchTag("PageType", "List", false)%></asp:Content>
<asp:Content ID="robotBlock" ContentPlaceHolderID="robots" runat="server"><meta name="robots" content="noindex,follow" /></asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
		<div id="topnav">
		    <h1>Speakers</h1>
		    <ul id="navlist">
		        <li><a href="/schedule">Master Schedule</a></li>
		        <li><a href="/workshops">Workshops</a></li>
		        <li><a href="/sessions">Sessions</a></li>
		        <li><a href="/Videos">Videos</a></li>
		        <li class="ncurrent"><a href="/speakers">Speakers</a></li>
		        <li><a href="/opencall">Open Call</a></li>
		    </ul>
		</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Content("SpeakersDescription") %>
    <% Html.RenderPartialFromSkin("SpeakerBrowser"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Speakers")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); 
    Html.RenderScriptTag("Speakers.js"); %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyTag" runat="server" ><body id="speakerList"></asp:Content>