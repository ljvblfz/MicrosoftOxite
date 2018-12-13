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
		        <li><a href="/Schedule">Master Schedule</a></li>
		        <li class="ncurrent"><a href="/Workshops">Workshops</a></li>
		        <li><a href="/Sessions">Sessions</a></li>
		        <li><a href="/Speakers">Speakers</a></li>
		    </ul>
		</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Content("Content") %>
<div id="sessions" class="sessions">
<% Html.RenderPartialFromSkin(
       "ScheduleItemList",
       new OxiteViewModelItems<ScheduleItem>(
           Model.Items,
           Model
           )
       ); %>
</div>
    <%=Html.Content("LowerContent") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="secondaryContent" runat="server">
    <%=Html.Content("SecondaryContent") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Workshops")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadCssFiles" runat="server"><%
    Html.RenderCssFile("jquery.autocomplete.css"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("Sessions.js?ver=2");
    Html.RenderScriptTag("jquery.autocomplete.js");
    Html.RenderScriptTag("SessionBrowser.4.js?rev=04152010"); %>
</asp:Content>
