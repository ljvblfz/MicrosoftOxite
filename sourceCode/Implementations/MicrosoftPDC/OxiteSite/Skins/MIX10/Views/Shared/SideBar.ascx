<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="right">
    <% Html.RenderPartialFromSkin("News"); %>
    <% Html.RenderPartialFromSkin("Social"); %>
</div>
