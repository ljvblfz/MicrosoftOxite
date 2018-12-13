<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="pageTitle">
    <h1><%=Html.SkinImage("/images/oxite_medium.png", "Oxite", new { width = 100, height = 49 }) %></h1>
    <%--<a href="<%=Url.Posts() %>"><%=Model.Site.DisplayName %></a>--%>
</div>
<div id="logindisplay"><% Html.RenderPartialFromSkin("LoginUserControl"); %></div>
<div id="menucontainer">
    <% Html.RenderPartialFromSkin("Menu"); %>
</div>
