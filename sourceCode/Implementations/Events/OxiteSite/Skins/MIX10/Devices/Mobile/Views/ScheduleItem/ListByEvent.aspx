<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" MasterPageFile="../Shared/Site.Master"%>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Search.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Sessions")) %>
</asp:Content>

<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server">
	<%=Html.SearchTag("Section", "Sessions", false)%>
	<%=Html.SearchTag("PageType", "List", false)%>
	<%=Html.SearchTag("Section", Model.Container.Name, false)%>
</asp:Content>

<asp:Content ID="additionalCSS" ContentPlaceHolderID="HeadCssFiles" runat="server">
</asp:Content>

<asp:Content ID="robotBlock" ContentPlaceHolderID="robots" runat="server"><meta name="robots" content="noindex,follow" /></asp:Content>

<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server"> 
	<div id="sessions" class="sessions">  
		<% Html.RenderPartialFromSkin("ListByEvent"); %>
	</div>
</asp:Content>


