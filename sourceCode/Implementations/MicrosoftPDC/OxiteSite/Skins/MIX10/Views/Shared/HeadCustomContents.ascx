<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.ViewModels"%>
<%@ Import Namespace="Oxite.Extensions" %>
<link href="/Skins/MIX10/Styles/screen.css?rev=12192009" media="screen, projection" rel="stylesheet" type="text/css" />
<link href="/Skins/MIX10/Styles/style.css?rev=12192009" media="screen, projection" rel="stylesheet" type="text/css" /><%
   // Html.RenderCssFile("style.css", "style.css", "screen, projection");
    Html.RenderCssFile("jquery.lightbox-0.5.css", "jquery.lightbox-0.5.css", "screen");
    Html.RenderCssFile("print.css", "print.css", "print");          
    Html.RenderScriptTag("http://ajax.microsoft.com/ajax/jQuery/jquery-1.3.2.js", "http://ajax.microsoft.com/ajax/jQuery/jquery-1.3.2.min.js");
    Html.RenderScriptTag("contentslider.js", "contentslider.js");
    Html.RenderScriptTag("jquery.lightbox-0.5.js", "jquery.lightbox-0.5.min.js");
                                                                                                              
    //Html.RenderScriptTag("searchindex.js", "searchindex.js"); // missing in template
    %>
    <!--[if lt IE 8]> <% Html.RenderCssFile("ie.css", "ie.css", "screen, projection"); %><![endif]-->
    <!--[if IE 8]> <% Html.RenderCssFile("ie_8.css", "ie.css", "screen, projection"); %><![endif]-->