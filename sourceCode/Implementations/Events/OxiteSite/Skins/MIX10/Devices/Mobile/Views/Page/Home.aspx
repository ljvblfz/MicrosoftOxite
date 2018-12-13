<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Oxite.Modules.Conferences.Models.ScheduleItem>>" MasterPageFile="../Shared/Site.Master"%>
<%@ Import Namespace="Oxite.Extensions"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle() %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<br />
March 15-17th, 2010<br />
Workshops March 14th<br />
Las Vegas<br />
<p>
	A 3 day conference for web designers and developers building the world's most innovative web sites.
	Check out the links below to view the event schedule, view the list of sessions, and get the latest event news.
</p>
</asp:Content>

<asp:Content ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("PageType", "List", false)%></asp:Content>