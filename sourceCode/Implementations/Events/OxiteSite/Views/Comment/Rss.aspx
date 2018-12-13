<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<PostComment>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models"
%><rss version="2.0" xmlns:dc="http://purl.org/dc/elements/1.1/"><%
    Html.RenderPartialFromSkin("RssChannel", Model); %>
</rss>