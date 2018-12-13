<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Speaker>>" %>

<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>

<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Speakers")) %>
</asp:Content>

<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server">
	<%=Html.SearchTag("Section", "Speakers", false)%>
	<%=Html.SearchTag("PageType", "List", false)%>
</asp:Content>

<asp:Content ID="robotBlock" ContentPlaceHolderID="robots" runat="server"><meta name="robots" content="noindex,follow" /></asp:Content>

<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
	 <% Html.RenderPartialFromSkin("SpeakerBrowser"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartialFromSkin("ListByEvent"); %>
   <%-- <%=Html.Content("SpeakersDescription") %>--%>
</asp:Content>
