<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>


<ul>
	<li class="first"><a href="/Contact">Contact Us</a></li>
	<li><a href="/Terms">Terms of Use</a></li>
	<li class="last"><a href="/Privacy">Privacy</a></li>
</ul>
<ul>
	<li class="first fullsite"><a href="/switch/Default">Full Site</a></li>
	<li class="last"><a href="/RSS">Subscribe</a></li>
</ul>

<%--<ul class="credits">
	<li><span><%=Model.Localize("PoweredBy", "Powered by") %> </span><%=Html.Link("Oxite", Url.Oxite()) %></li>
	<li><%=Html.Link("Icons by famfamfam", "http://www.famfamfam.com", new { id = "famfamfam", title = Model.Localize("IconsByFamFamFam", "Icons by famfamfam") })%></li>
</ul>--%>
