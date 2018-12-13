<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions" %>


<div id="logo"><a href="/"><img src="<%=ResolveClientUrl("../../Styles/images/") %>mix10.gif" alt="MIX10" /></a></div>
<ul id="toplinks">
	<li><a href="/Search" id="search">Search <img src="<%=ResolveClientUrl("../../Styles/images/") %>ico_search.gif" alt="" /></a></li>
	<li class="last"><% Html.RenderPartialFromSkin("LoginUserControl"); %></li>
</ul>
<br class="clearfloats" />