<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="right">
    <% Html.RenderPartialFromSkin("News"); %>
    <a href="/SyncClient"><img src="/Content/images/sessionbrowser.jpg" alt="MIX10 Session Browser (Beta)" /></a>
    <% Html.RenderPartialFromSkin("Social"); %>
</div>
