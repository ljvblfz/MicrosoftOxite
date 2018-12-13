<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title>
	Browser Info | PDC
</title></head>
</div>
 
    <div>
    <p>Request.Browser</p>
    <ul>
    <li>Type: <%= Request.Browser.Type %></li>
    <li>Platform: <%= Request.Browser.Platform%></li>
    <li>Version: <%= Request.Browser.Version%></li>
    <li>Browser: <%= Request.Browser.Browser%></li>
    <li>EcmaScriptVersion: <%= Request.Browser.EcmaScriptVersion%></li>
    <li>SupportsCss: <%= Request.Browser.SupportsCss %></li>
    <li>IsMobileDevice: <%= Request.Browser.IsMobileDevice %></li>
    <li>MobileDeviceManufacturer: <%= Request.Browser.MobileDeviceManufacturer%></li>
    <li>MobileDeviceModel: <%= Request.Browser.MobileDeviceModel%></li>
    <li>Beta: <%= Request.Browser.Beta%></li>
    <li>Request.UserAgent:<br /><%= Request.UserAgent %></li>
    </ul>
    </div>
</body>
</html>
