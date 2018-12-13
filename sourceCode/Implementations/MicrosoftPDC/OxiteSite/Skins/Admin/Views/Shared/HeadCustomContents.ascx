<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.ViewModels"%>
<%@ Import Namespace="Oxite.Extensions" %><%
    Html.RenderCssFile("yui.reset.2.6.0.css");
    Html.RenderCssFile("base.css");
    Html.RenderScriptTag("http://ajax.microsoft.com/ajax/jQuery/jquery-1.3.2.js", "http://ajax.microsoft.com/ajax/jQuery/jquery-1.3.2.min.js"); %>