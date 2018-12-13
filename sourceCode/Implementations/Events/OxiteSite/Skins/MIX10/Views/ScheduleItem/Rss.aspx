<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"
%><rss version="2.0" 
   xmlns:dc="http://purl.org/dc/elements/1.1/"
   xmlns:atom="http://www.w3.org/2005/Atom"
   xmlns:trackback="http://madskills.com/public/xml/rss/module/trackback/"
   xmlns:wfw="http://wellformedweb.org/CommentAPI/"
   xmlns:slash="http://purl.org/rss/1.0/modules/slash/"
   xmlns:media="http://search.yahoo.com/mrss/"
   xmlns:evnet="http://www.mscommunities.com/rssmodule/"
   xmlns:itunes="http://www.itunes.com/dtds/podcast-1.0.dtd"
   ><%
    Html.RenderPartialFromSkin("RssChannel", Model); %>
</rss>
