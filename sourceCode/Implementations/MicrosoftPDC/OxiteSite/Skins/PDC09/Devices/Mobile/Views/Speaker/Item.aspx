<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItemItems<Speaker, ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.ViewModels"%>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>

<asp:Content ID="description" ContentPlaceHolderID="MetaDescription" runat="server">
	<%=Html.PageDescription(Model.Item.Bio.CleanHtml()) %>
</asp:Content>
<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server">
	<%=Html.SearchTag("Section", "Speakers", false)%>
	<%=Html.SearchTag("Title", Model.Item.DisplayName, true)%>
	<link rel="canonical" href="<%=Url.AbsolutePath(Url.Speaker(Model.Item)) %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Speakers"), Model.Item.DisplayName) %>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
	<%  Model.AddModelItem(Model.Item);
            Html.RenderPartialFromSkin("SessionBrowser", new OxiteViewModelItems<ScheduleItem>(Model.Items, Model)); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
   var referrerPath = Request.UrlReferrer != null ? Request.UrlReferrer.AbsolutePath : string.Empty;
   var linkText = "speakers list";
   var breadcrumbUrl = referrerPath;

   if (referrerPath == "/Sessions/Mine")
      linkText = "my sessions";
   else if (referrerPath == "/Sessions")
      linkText = "session list";
   else
      breadcrumbUrl = "/Speakers";
%>
<div class="breadcrumb"><br /><a href="<%= breadcrumbUrl %>">&#0171; back to <%= linkText %></a></div>

<div id="speaker">
	<h2><%= Html.Encode(Model.Item.DisplayName) %></h2>
	<p><%= Html.Encode(Model.Item.Bio) %></p>

	<%if (Model.Items.Count() > 0) { %>
	<div class="sessions byspeaker">
		<p><%= string.Format("Session{0}", Model.Items.Count() > 1 ? "s" : string.Empty) %> <%= Html.Encode(Model.Item.FirstName)%> is presenting at:</p>
		<%foreach (var session in Model.Items){ %>
		<div class="session">
			<h2><%=Html.Link(session.Title, Url.Session(session))%></h2>		
			<%if (session.Speakers.Count<Speaker>() > 0) { %>
			<p class="speakerday">
				With <%=string.Join(", ", session.Speakers.Select(s => Html.Link(s.DisplayName, Url.Speaker(s), new { @class = "speaker" })).ToArray())%><br />
			</p>
			<% } %>
		</div>
		<%} %>
	</div>
	<%} %>
</div>
</asp:Content>