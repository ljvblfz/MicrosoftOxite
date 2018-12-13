<%@ Page Language="C#" AutoEventWireup="true" ContentType="text/xml" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<DateTime>>" %>
<%@ OutputCache Duration="21600" VaryByParam="none" %>
<%@ Import Namespace="Oxite.Extensions"%><%@ Import Namespace="Oxite.Modules.CMS.Extensions"
 %><?xml version="1.0" encoding="UTF-8"?>
<sitemapindex xmlns="http://www.sitemaps.org/schemas/sitemap/0.9"><%
    foreach (DateTime dt in Model.Items)
    { %>
   <sitemap>
      <loc><%=Url.AbsolutePath(Url.SiteMap(dt.Year, dt.Month))%></loc><%--
      Add last modified date for all but the current month's sitemap --%>
   </sitemap><%
    } %>
</sitemapindex>
