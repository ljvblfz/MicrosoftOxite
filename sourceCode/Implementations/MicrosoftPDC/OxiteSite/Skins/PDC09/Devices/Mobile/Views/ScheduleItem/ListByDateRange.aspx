<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Search.Extensions" %>

<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server">
	<%=Html.SearchTag("Section", "Sessions", false)%>
	<%=Html.SearchTag("PageType", "List", false)%>
	<%=Html.SearchTag("Section", Model.Container.Name, false)%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Schedule")) %>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <% Html.RenderPartialFromSkin("ScheduleBrowser"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div id="schedule">
      <% Html.RenderPartialFromSkin("ListByDateRange"); %>
	</div>
</asp:Content>

