<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteModelList<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %><%
    string[] mediaTypes = new string[] { "WMV", "WMVHigh", "WMVStreaming", "WMA", "MP3", "MP4" };
    if (Model.List.Any(p => p.Files.Any(f => mediaTypes.Contains(f.TypeName, StringComparer.OrdinalIgnoreCase))))
    {
        %><rss version="2.0" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:media="http://search.yahoo.com/mrss/"><%
    }
    else
    {
        %><rss version="2.0" xmlns:dc="http://purl.org/dc/elements/1.1/"><%
    }
    Html.RenderPartialFromSkin("RssChannel", Model); %>
</rss>
