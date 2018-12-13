<%@ control language="C#" inherits="System.Web.Mvc.ViewUserControl<Oxite.ViewModels.OxiteViewModel>" %>
<%@ import namespace="Oxite.Extensions" %>
<div id="livestream" class="videowrapper">
    <div class="contentdiv">
        <div id="silverlightControlHost">
            <object data="data:application/x-silverlight-2," type="application/x-silverlight-2"
                width="940" height="406">
                <param name="source" value="http://ispss.istreamplanet.com/ClientBin/Microsoft.Mix10.UI.Silverlight.xap?version=3" />

                <param name="minRuntimeVersion" value="3.0.50106.0" />
                <param name="autoUpgrade" value="false" />
                <param name="enableHtmlAccess" value="true" />
                <param name="onerror" value="onSilverlightError" />
                <param name="onload" value="onSilverlightLoad" />
                <param name="initParams" value="SettingsUri=http://ispss.istreamplanet.com/datasources/Settings.xml" />
                <param name="splashscreensource" value="SplashScreen.xaml" />
            </object>
        </div>
    </div>
</div>
    

