<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
        
<%
	string deviceCssfile = "screen_pocketIE6.css";
   if (Request.Browser.MobileDeviceModel.ToLower().Contains("iphone") || Request.Browser.MobileDeviceModel.ToLower().Contains("ipod"))
   {
      deviceCssfile = "screen_iphone.css";
   }
   else if (Request.Browser.MobileDeviceModel.ToLower().Contains("blackberry"))
   {
      deviceCssfile = "screen_blackberry.css";
   }
   else if (Request.Browser.Browser.ToLower().Contains("opera"))
   {
      deviceCssfile = "screen_opera.css";
   }
   else if (Request.UserAgent != null && Request.UserAgent.Contains("IEMobile 7.11") && Request.Browser.Browser.ToLower().Contains("ie"))
       {
           deviceCssfile = "screen_pocketIE7.css";
       }

   Html.RenderCssFile(deviceCssfile);	
%>
