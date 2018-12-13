<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" MasterPageFile="..\Shared\Home.Master"  %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Blogs.Models"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<asp:Content ContentPlaceHolderID="HeaderSiteName" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="bodyTagVideo" runat="server" >
    <body id="homepage" onload="onLauncherPageLoad();" class="homepage hasVideo">
</asp:Content>
<asp:Content ContentPlaceHolderID="bodyTagNoVideo" runat="server" >
    <body id="homepage" class="homepage">
</asp:Content>
<asp:Content ContentPlaceHolderID="HeroBanner" runat="server">
   <%Html.RenderPartialFromSkin("HeroArea");%>
</asp:Content>
<asp:Content ContentPlaceHolderID="VideoPlayer" runat="server">
   <%Html.RenderPartialFromSkin("VideoArea");%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadScriptsNoVideo" runat="server">    
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadScriptsVideo" runat="server">    
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />    
    <!-- ****** START SILVERLIGHT INSTALL SCRIPT MODS **** -->
    <script type="text/javascript" src="http://ispss.istreamplanet.com/install/Silverlight.logger.js?version=3"></script>
    <script type="text/javascript" src="http://ispss.istreamplanet.com/install/SilverlightVersion.js?version=3"></script>

    <link href="http://ispss.istreamplanet.com/install/Silverlight.InstallMix10.css?version=3" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ispss.istreamplanet.com/install/Silverlight.InstallMix10.js?version=3"></script>
    <!-- ****** END SILVERLIGHT INSTALL SCRIPT MODS **** -->
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server"><%=Html.PageTitle() %></asp:Content>
<asp:Content ContentPlaceHolderID="HeadCustom" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="ScriptsVideo" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="ScriptsNoVideo" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("PageType", "List", false)%></asp:Content>