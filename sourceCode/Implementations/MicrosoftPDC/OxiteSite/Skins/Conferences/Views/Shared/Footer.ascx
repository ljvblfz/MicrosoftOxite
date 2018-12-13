<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<ul>
    <li><a href="/switch/Mobile">Mobile Site</a></li>
    <li><a href="/contact">Contact Us</a></li>
    <li><a href="/terms">Terms Of Use</a></li>
    <li><a href="/privacy">Privacy Statement</a></li>
    <li><a href="/rss">RSS Feed</a></li>
</ul>
<ul class="credits">
 <li><span><%=Model.Localize("hostedby", "This site is hosted by Microsoft by ORCSWEB. Copyright 2009 Microsoft.")%> </span></li>
</ul>