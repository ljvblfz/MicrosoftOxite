<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItemItems<ScheduleItemTag, ScheduleItem>>" MasterPageFile="../Shared/Site.Master"%>

<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Search.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Tags.Models"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.ViewModels"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Sessions")) %>
</asp:Content>

<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server">
	<%=Html.SearchTag("Section", "Sessions", false)%>
	<%=Html.SearchTag("PageType", "List", false)%>
	<%=Html.SearchTag("Section", Model.Container.Name, false)%>
</asp:Content>

<asp:Content ID="robotBlock" ContentPlaceHolderID="robots" runat="server">
    <meta name="robots" content="noindex,follow" />
</asp:Content>


<asp:Content ID="additionalCSS" ContentPlaceHolderID="HeadCssFiles" runat="server">

<%--<style type="text/css">
	div#pagingContainer ul.paging li.selected
	{  
		background: expression( this.previousSibling == null ? '#ffffff' : '#df8536'); 
		color: expression( this.previousSibling == null ? '#5f779c' : '#ffffff');
		font-weight: expression( this.previousSibling == null ? 'normal' : 'bold');
	}
</style>--%>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader" runat="server">
  <%  Model.AddModelItem(Model.Item);
            Html.RenderPartialFromSkin("SessionBrowser", new OxiteViewModelItems<ScheduleItem>(Model.Items, Model)); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<% ScheduleItemTag scheduleItemTag = Model.GetModelItem<ScheduleItemTag>(); %>
	<% TagListViewModel tagListViewModel = Model.GetModelItem<TagListViewModel>(); %>
	
	<% if (Model.GetModelItem<ScheduleItemTag>() != null) {%>
	<br />Sessions tagged with <strong><%= Model.GetModelItem<ScheduleItemTag>().GetDisplayName() %></strong><br />
	<% } %>
	
	<div id="sessions" class="sessions">
		<% Html.RenderPartialFromSkin("ListByEvent", new OxiteViewModelItems<ScheduleItem>(Model.Items, Model)); %>		
	</div>
	
</asp:Content>