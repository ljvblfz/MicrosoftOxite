<%@ Page Language="C#" AutoEventWireup="true" ContentType="text/xml" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Speaker>>" %>
<%@ OutputCache Duration="21600" VaryByParam="none" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.ViewModels"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Search.Extensions"
 %><?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9"><%
    foreach (Speaker speaker in Model.Items)
    { %>
   <url>
        <loc><%=Url.AbsolutePath(Url.Speaker(speaker)) %></loc>
    </url><%
    } %>
</urlset>