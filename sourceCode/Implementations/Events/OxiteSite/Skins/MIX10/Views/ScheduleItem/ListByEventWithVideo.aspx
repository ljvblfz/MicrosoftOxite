<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Blogs.Models"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Search.Extensions" %>
<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server">
<%=Html.SearchTag("Section", "Videos", false)%>
<%=Html.SearchTag("PageType", "List", false)%>
<%=Html.SearchTag("Section", Model.Container.Name, false)%></asp:Content>
<asp:Content ID="robotBlock" ContentPlaceHolderID="robots" runat="server">
    <meta name="robots" content="noindex,follow" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    		<div id="topnav">
		    <h1>Videos</h1>
		    <ul id="navlist">
		        <li><a href="/schedule">Master Schedule</a></li>
		        <li><a href="/workshops">Workshops</a></li>
		        <li><a href="/Sessions">Sessions</a></li>
		        <li class="ncurrent"><a href="/Videos">Videos</a></li>
		        <li><a href="/speakers">Speakers</a></li>
		        <li><a href="/opencall">Open Call</a></li>
		    </ul>
		</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">


    <p>This page provides a handy "all-in-one" list of MIX10 session recordings that are available to download and watch on-demand. 
    While we commit to publishing the highest quality video of a session within 24 hours of its completion, 
    it often takes many hours to encode, edit, and upload each video before it becomes available. 
    So, if the session you’re looking for isn't here, be sure to check back later.</p>

    <p>Eventually, each session will be offered as a high-quality WMV, 
    a regular quality WMV, and as a MP4 file for mobile devices. For most sessions, the PowerPoint deck will also be provided.</p>      
    <h3>Using Mike Swanson's downloader and renamer</h3>
    <p>If you’d like to download all of the keynote and session content, download a recent build of <a href="http://curl.haxx.se/download.html">cURL</a> (~250K), 
    and extract it to your folder-of-choice. Then, download <a href="/Content/MIX10Downloader.zip">MIX10Downloader.zip</a> (1.39KB) and extract the MIX10Downloader.bat
     file to the same folder. From a command prompt, start MIX10Downloader by passing it one of 
     the following parameters: WMVHIGH, WMV, MP4, PPTX. Then wait. For files that aren’t available, 
     cURL will download a file that is around 1,245 bytes in size (if you change the extension to .htm and open it, you’ll 
     see that the file is simply an HTML "not found" error page).</p>
     <p>To rename the files, first, download <a href="/Content/MIX10Renamer.zip">MIX10Renamer.zip</a> (4.09KB). Then, extract the MIX10Renamer.bat file to the
      folder that contains your downloaded files, and from a command prompt, type MIX10Renamer WMV to rename all of the
       .WMV files to the full session title. By changing the parameter, you can also rename your PPTX and MP4 files.</p>
    <div id="browser">
        <% Html.RenderPartialFromSkin("VideoBrowser"); %>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Videos")) %>
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
<asp:Content ContentPlaceHolderID="HeadCustom" runat="server">
<%
    Html.RenderFeedDiscoveryRss("WMVHigh (RSS)", Url.Sessions("RSS", "WMVHigh"));
    Html.RenderFeedDiscoveryRss("WMV (RSS)", Url.Sessions("RSS", "WMV"));
    Html.RenderFeedDiscoveryRss("MP4 (RSS)", Url.Sessions("RSS", "MP4"));
    Html.RenderFeedDiscoveryRss("PPT (RSS)", Url.Sessions("RSS", "PPT"));
    %>
</asp:Content>
<asp:Content ContentPlaceHolderID="bodyTag" runat="server" ><body id="videolist"></asp:Content>