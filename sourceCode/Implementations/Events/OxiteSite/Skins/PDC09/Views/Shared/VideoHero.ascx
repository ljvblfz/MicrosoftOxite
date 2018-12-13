<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
    <div id="silverlightControlHost" style="height: 100;">
        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="805" height="370">
            <param name="source" value="http://video.microsoftpdc.com/ClientBin/Microsoft.Pdc.UI.Silverlight.xap?version=1" />
            <param name="minRuntimeVersion" value="3.0.40818.0" />            
            <param name="autoUpgrade" value="false" />
            <param name="onerror" value="onSilverlightError" />
            <param name="onload" value="onSilverlightLoad" />            
            <param name="initParams" value="SettingsUri=http://video.microsoftpdc.com/DataSources/Settings.xml" />
            <param name="EnableGPUAcceleration" value="true" />
            <param name="splashscreensource" value="SplashScreen.xaml"/>                     
            <div id="SLInstallFallback"></div>
        </object>
    </div>

