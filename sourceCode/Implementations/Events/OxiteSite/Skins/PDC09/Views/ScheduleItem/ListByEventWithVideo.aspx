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
    <h1><%=Model.Localize("Videos") %></h1>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <p>This page provides a handy 'all-in-one' list of sessions that are available for watching on demand. 
    Sessions need to be encoded, edited and uploaded before they appear here, so if the session you want isn't here
    just check back after another day.</p>
    <p><em>Sessions with titles in <strong>bold</strong> are available for smooth streaming playback</em></p>
    <h3>Using Mike Swanson's downloader and renamer</h3>
    <p>If you’d like to download all of the keynote and session content, download a recent build of <a href="http://curl.haxx.se/">cURL (~250K)</a>,
     and extract it to your folder-of-choice. Then, download <a href="http://ecn.channel9.msdn.com/o9/pdc/PDC09DownloaderCSR.zip">PDC09Downloader.zip (1.49KB)</a> and extract 
     the PDC09Downloader.bat file to the same folder. From a command prompt, start PDC09Downloader by 
     passing it one of the following parameters: WMVHIGH, WMV, MP4, PPTX. Then wait. 
     For files that aren’t available, cURL will download a file that is around 221 bytes in size 
     (if you change the extension to .htm and open it, you’ll see that the file is simply an HTML "not found" error page).</p>
    <p>To rename the files, first, download the <a href="http://ecn.channel9.msdn.com/o9/pdc/PDC09RenamerCSR.zip">PDC09 Renamer batch file (4.52KB)</a>. Then, extract 
    the PDC09Renamer.bat file to the folder that contains your downloaded files, and from a command prompt,
     type PDC09Renamer WMV to rename all of the .WMV files to the full session title. By changing the parameter,
      you can also rename your PPTX and MP4 files.</p>
      
    <div id="browser">
	    <div id="tabs">
		    <a href="/Sessions" class="tab">Sessions</a>
		    <a href="/Schedule" class="tab">Schedule</a>		    
		    <a href="/Videos" class="tab" id="videostab">Videos</a>		    
	    </div>
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
