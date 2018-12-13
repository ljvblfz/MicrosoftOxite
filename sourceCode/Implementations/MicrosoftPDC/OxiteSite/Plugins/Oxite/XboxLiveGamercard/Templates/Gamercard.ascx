<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Oxite.Extensions" %>
<div>
    <h3>Xbox Gamercard</h3>
    <object data="data:application/x-silverlight-2," id="slPlugin" type="application/x-silverlight-2" width="100%" height="284">
        <param name="source" value="http://mschnlnine.vo.llnwd.net/d1/client/xbf/gc/app.xap" />
        <param name="background" value="Transparent" />
        <param name="initParams" value="gamertag=<%=((Plugin)ViewData["Plugin"]).ExtendedProperties.GetValue<string>("Gamertag")%>,themename=channel9" />
        <a href="http://go.microsoft.com/fwlink/?LinkID=124807">
        <img src="http://mschnlnine.vo.llnwd.net/d1/client/xbf/gc/install.jpg" alt="Get Microsoft Silverlight" style="border-style:none;" />
        </a>
    </object>
</div>
