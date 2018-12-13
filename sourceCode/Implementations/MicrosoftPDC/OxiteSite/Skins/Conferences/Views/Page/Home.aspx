<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Blogs.Models"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<asp:Content ContentPlaceHolderID="HeaderSiteName" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="HeroBanner" runat="server">
<% Html.RenderPartialFromSkin("HeroArea"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server"><%=Html.PageTitle() %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadCustom" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("PageType", "List", false)%></asp:Content>