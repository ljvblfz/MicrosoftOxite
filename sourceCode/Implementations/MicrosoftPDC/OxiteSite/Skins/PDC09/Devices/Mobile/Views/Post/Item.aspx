<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Post>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Tags.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
	<%=Html.PageTitle(Model.Container.GetDisplayName(), Model.Item.GetDisplayName()) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MetaDescription" runat="server">
	<%=Html.PageDescription(Model.Item.GetBodyShort()) %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="SearchTags" runat="server">
	<%=Html.SearchTag("Section", "Blogs", false)%>
	<%=Html.SearchTag("Title", Model.Item.Title.CleanText(), false)%>
	<%=Html.SearchTag("Author", Model.Item.Creator.Name.CleanText(), false) %>
	<%=Html.SearchTag("Section", Model.Container.Name, false)%>
	<%=Html.SearchTag("Tags", string.Join(", ", Model.Item.Tags.Select(s => Html.Link(s.DisplayName, Url.Tag(s), new { @class = "tag" })).ToArray()), true) %>
	<link rel="canonical" href="<%=Url.AbsolutePath(Url.Post(Model.Item)) %>" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadCustom" runat="server"><%
    Response.Write(Html.PingbackDiscovery(Model.Item)); %>
</asp:Content>

<asp:Content ID="subnav" ContentPlaceHolderID="ContentHeader" runat="server">
	<div class="breadcrumb"><br /><a href="/WhatsHappening">&#0171; back to news</a></div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="post">
	<h1><%=Html.Link(Model.Item.Title.CleanText().WidowControl(), Url.Post(Model.Item)) %></h1>
	<div class="posted">Posted <%=Html.Published() %> by <%=Model.Item.Creator.Name.CleanText() %></div>
	<div class="content"><%=Model.Item.Body %></div>
	<div class="meta">
		<p>
			<%=Model.Localize("Tags")%>:  <% IEnumerable<Tag> tags = Model.Item.GetTags(); if (tags.Count() > 0) { %> 
				<%= string.Join(", ", tags.Select(t => Html.Link(t.GetDisplayName().CleanText(), Url.Posts(t), new { rel = "tag" })).ToArray())%>
			<% } else { %><%=Model.Localize("none") %><% } %>
		</p>
	</div>
</div>
</asp:Content>

