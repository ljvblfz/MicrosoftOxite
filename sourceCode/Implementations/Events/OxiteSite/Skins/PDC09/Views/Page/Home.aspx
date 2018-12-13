<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Home.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Blogs.Models"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<asp:Content ContentPlaceHolderID="HeaderSiteName" runat="server"><h1 id="siteName"><%=Html.Link("Microsoft PDC09", Url.Home()) %></h1></asp:Content>
<asp:Content ContentPlaceHolderID="HeroBanner" runat="server">
<% Html.RenderPartialFromSkin("HeroArea"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <%=Html.Content("Content") %>
    <div id="sessions" class="sessions">
        <%
            Html.RenderPartialFromSkin("FeaturedScheduleItemsList"); %>
        <div class="pager"><a href="/Sessions" class="allSessions"><%=Model.Localize("see all sessions »")%></a></div>
    </div>
    <h2>PDC Videos from Channel 9</h2>
    <div class="sessions">
        <%
            Html.RenderPartialFromSkin("Channel9Videos"); %>
        <div class="pager"><a href="http://channel9.msdn.com" class="allSessions"><%=Model.Localize("see more on Channel 9 »")%></a></div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <img alt="adTrackImage" height="1" width="1" src="http://view.atdmt.com/action/MMN_Worktank_Landing" />
    <div class="vevent hidden">
        <a class="url" href="http://microsoftpdc.com/">http://microsoftpdc.com</a> <span
            class="summary">Microsoft PDC09</span>:
        <abbr class="dtstart" title="2009-11-16">
            November 16</abbr>-
        <abbr class="dtend" title="2009-11-20">
            19</abbr>, at the <span class="location vcard"><span class="fn org">Los Angeles Convention
                Center</span>, <span class="adr"><span class="locality">Los Angeles</span>,
                    <abbr class="region" title="California">
                        CA</abbr>
                    <abbr class="country-name" title="United States">
                        USA</abbr></span> <span class="geo"><span class="latitude">34.04103</span> <span
                            class="longitude">-118.26932</span> </span></span>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle() %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadCustom" runat="server">
    <meta http-equiv="pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="-1"/><%
    Html.RenderFeedDiscoveryRss("Sessions (RSS)", Url.Sessions("RSS"));
    Html.RenderFeedDiscoveryAtom("Sessions (ATOM)", Url.Sessions("ATOM"));
    Html.RenderFeedDiscoveryRss("Behind the Scenes Blog (RSS)", Url.Posts(new Blog("BehindTheScenes"), "RSS"));
    Html.RenderFeedDiscoveryRss("Behind the Scenes Blog (ATOM)", Url.Posts(new Blog("BehindTheScenes"), "ATOM"));
    Html.RenderFeedDiscoveryRss("What's Happening (RSS)", Url.Posts(new Blog("WhatsHappening"), "RSS"));
    Html.RenderFeedDiscoveryRss("What's Happening (ATOM)", Url.Posts(new Blog("WhatsHappening"), "ATOM"));
    Html.RenderLiveWriterManifest();
    %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("jquery-ui-1.7.2.custom.min.js");
    Html.RenderScriptTag("HeroBanner.js");
    Html.RenderScriptTag("Sessions.js?ver=2");
%>
</asp:Content>
<asp:Content ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("PageType", "List", false)%></asp:Content>