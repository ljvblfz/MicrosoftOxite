<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.ViewModels"%>
<%@ Import Namespace="Oxite.Extensions" %>
<link href="/Sessions/RSS" rel="alternate" title="Sessions (RSS)" type="application/rss+xml" />
<link href="/Skins/MIX10/Styles/print.css?rev=02092010" media="print" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Scripts/Reform.js"></script>
<% var printable = ViewData["Printable"] != null ? Convert.ToBoolean(ViewData["Printable"]) : false; %>
<%     Html.RenderScriptTag("http://ajax.microsoft.com/ajax/jQuery/jquery-1.3.2.js", "http://ajax.microsoft.com/ajax/jQuery/jquery-1.3.2.min.js");
 %>
<% if (!printable){ %>
    <link href="/Skins/MIX10/Styles/screen.css?rev=03092010" media="screen, projection" rel="stylesheet" type="text/css" />
    <link href="/Skins/MIX10/Styles/style.css?rev=03172010" media="screen, projection" rel="stylesheet" type="text/css" /><%
    Html.RenderCssFile("jquery.lightbox-0.5.css", "jquery.lightbox-0.5.css", "screen");
    Html.RenderScriptTag("contentslider.js", "contentslider.js");
    Html.RenderScriptTag("jquery.lightbox-0.5.js", "jquery.lightbox-0.5.min.js");%>
    <!--[if lt IE 8]><link href="/Skins/MIX10/Styles/ie.css?rev=01142010" media="screen, projection" rel="stylesheet" type="text/css" /> <![endif]-->
    <!--[if IE 8]> <% Html.RenderCssFile("ie_8.css", "ie.css", "screen, projection"); %><![endif]-->
<%} else {%>
<link href="/Skins/MIX10/Styles/printable.css?rev=02092010" media="screen, projection" rel="stylesheet" type="text/css" />
<% } %>