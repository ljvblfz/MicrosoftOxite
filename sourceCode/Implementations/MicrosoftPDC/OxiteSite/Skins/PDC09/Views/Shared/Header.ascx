<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions" %>
<div id="globals">
    <div id="userinfo"><% Html.RenderPartialFromSkin("LoginUserControl"); %></div>
    <% Html.RenderPartialFromSkin("SiteSearchControl"); %>
</div>
<div id="banner">
</div>