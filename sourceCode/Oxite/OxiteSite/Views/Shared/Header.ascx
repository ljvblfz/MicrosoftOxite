<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Modules.Membership.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions" %>
<div id="pageTitle">
    <h1><a href="<%=Url.Posts() %>"><%=Model.Site.DisplayName %></a></h1>
</div>
<div id="logindisplay"><% Html.RenderPartialFromSkin("LoginUserControl"); %></div>
<div id="menucontainer">
    <ul class="menu">
        <li class="home"><%=Html.Link(Model.Localize("HomeMenuItem", "Home"), Url.Posts()) %></li>
        <li class="about"><%=Html.Link(Model.Localize("AboutMenuItem", "About"), Url.Page("About")) %></li>
        <li class="subscribe"><%=Html.Link(Model.Localize("Subscribe"), Url.Posts("RSS")) %></li>
    </ul>
    <%-- until style + behavior are shared -> <% Html.RenderPartialFromSkin("AdminMenu"); %>--%><% if (Model.GetUser().IsInRole("admin")) {
    %><ul class="admin menu"><li><%=Html.Link(Model.Localize("Admin.Dashboard", "Admin"), Url.Admin(), new { @class = "admin dashboard" })%></li></ul><%
    } %>
</div>