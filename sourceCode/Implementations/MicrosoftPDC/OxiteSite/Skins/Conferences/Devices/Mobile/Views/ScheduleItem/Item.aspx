<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<ScheduleItem>>" %>

<%@ Import Namespace="Oxite.Modules.Conferences.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Tags.Models"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Sessions"), Model.Item.Title) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MetaDescription" runat="server"><%=Html.PageDescription(Model.Item.GetBodyShort()) %></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SearchTags" runat="server">
<%=Html.SearchTag("Section", "Sessions", false)%>
<%=Html.SearchTag("Speakers", string.Join(", ", Model.Item.Speakers.Select(s => Html.Link(s.DisplayName, Url.Speaker(s), new { @class = "speaker" })).ToArray()), true) %>
<%=Html.SearchTag("Title", Model.Item.Title, true)%>
<%=Html.SearchTag("Tags", string.Join(", ", Model.Item.Tags.Select(s => Html.Link(s.DisplayName, Url.Tag(s), new { @class = "tag" })).ToArray()), true) %>
<link rel="canonical" href="<%=Url.AbsolutePath(Url.Session(Model.Item)) %>" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentHeader" runat="server">
    <%  Html.RenderPartialFromSkin("ItemBrowser"); %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
<%
   var referrerPath = Request.UrlReferrer != null ? Request.UrlReferrer.AbsolutePath : string.Empty;
   var linkText = "session list";
   var breadcrumbUrl = referrerPath;

   if (referrerPath == "/Sessions/Mine")
      linkText = "my sessions";
   else if (referrerPath.Contains("/Speakers/"))
      linkText = "speaker page";
   else
      breadcrumbUrl = "/Sessions";
%>        
<div class="breadcrumb"><br /><a href="<%= breadcrumbUrl %>">&#0171; back to <%= linkText %></a></div>
<div class="session">
	<h2><%=Html.Link(Model.Item.Title.CleanText().WidowControl(), Url.Session(Model.Item)) %></h2>
	<%if (Model.Item.Speakers.Count<Speaker>() > 0) { %>
	<p class="speaker">
		With <%=string.Join(", ", Model.Item.Speakers.Select(s => Html.Link(s.DisplayName, Url.Speaker(s), new { @class = "speaker" })).ToArray()) %>
	</p>
	<% } %>
	<p class="spacetime">
	<%if (Model.Item.Start >= new DateTime(2009, 11, 17)) { %>
		<%= string.Format("{0}", Model.Item.Start.ToString("dddd"))%>
		<%= string.Format(" | {0} - {1}", Model.Item.Start.ToString("h:mmtt").ToLower(), Model.Item.End.ToString("h:mmtt").ToLower()) %>
		<%= string.Format(" | {0}", Model.Item.Location)%>
   <%}%>
	</p>
			
	<p><%= Html.Encode(Model.Item.Body) %></p>	
	
	<% Html.RenderPartialFromSkin("ScheduleItemAdd", new OxiteViewModelPartial<ScheduleItem>(Model, Model.Item)); %>
	
	<div class="meta">
		<p>
			<%=Model.Localize("Tags")%>:  <% IEnumerable<Tag> tags = Model.Item.GetTags(); if (tags.Count() > 0) { %> 
				<%= string.Join(", ", tags.Select(t => Html.Link(t.GetDisplayName().CleanText(), Url.Sessions(t), new { rel = "tag" })).ToArray()) %>
			<% } else { %><%=Model.Localize("none") %><% } %>
		</p>
	</div>
</div>
</asp:Content>