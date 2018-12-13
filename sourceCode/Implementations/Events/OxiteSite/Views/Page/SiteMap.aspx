<%@ Page Language="C#" AutoEventWireup="true" ContentType="text/xml" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Oxite.Modules.CMS.Models.Page>>" %>
<%@ OutputCache Duration="21600" VaryByParam="none" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Models"
 %><?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9"><%
    foreach (Oxite.Modules.CMS.Models.Page page in Model.Items)
    { %>
   <url>
        <loc><%=Url.AbsolutePath(Url.Page(page)) %></loc>
    </url><%
    } %>
</urlset>