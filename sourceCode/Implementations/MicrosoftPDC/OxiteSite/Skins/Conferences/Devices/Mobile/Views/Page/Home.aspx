<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Blogs.Models"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle() %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<br />
November 17 – 19, 2009<br />
Workshops November 16<br />
Los Angeles Convention Center<br />
<p>
	The Professional Developers Conference (PDC) is the definitive event focused on the technical strategy of the Microsoft developer platform. Check out the links below to view the event schedule, view the list of sessions, get the latest event news and find out more about the area around the LA Convention Center.
</p>
</asp:Content>

<asp:Content ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("PageType", "List", false)%></asp:Content>