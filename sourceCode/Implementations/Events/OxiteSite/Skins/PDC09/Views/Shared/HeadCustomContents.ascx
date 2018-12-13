<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.ViewModels"%>
<%@ Import Namespace="Oxite.Extensions" %><%
    Html.RenderCssFile("screen.css?ver=11202009_2312");
    Html.RenderScriptTag("http://ajax.microsoft.com/ajax/jQuery/jquery-1.3.2.js", "http://ajax.microsoft.com/ajax/jQuery/jquery-1.3.2.min.js");
    Html.RenderScriptTag("jquery.qtip-1.0.0-rc3.js", "jquery.qtip-1.0.0-rc3.min.js"); %>