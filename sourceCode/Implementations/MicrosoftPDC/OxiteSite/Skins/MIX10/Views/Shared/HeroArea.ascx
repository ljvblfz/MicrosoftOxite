<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="slides" class="sliderwrapper">
    <div class="contentdiv"><a href="/Sessions"><img src="<%= Url.ImagePath("MIX10_Banner_Buxton.jpg", ViewContext) %>" alt="Bill Buxton" /></a></div>
    <div class="contentdiv"><a href="/About"><img src="<%= Url.ImagePath("MIX10_Banner_Guthrie.jpg", ViewContext) %>" alt="Scott Guthrie" /></a></div>
    <div class="contentdiv"><a href="/Registration"><img src="<%= Url.ImagePath("MIX10_Banner_Mandalay.jpg", ViewContext) %>" alt="Mandalay Bay" /></a></div>
    <div class="contentdiv"><a href="/OpenCall"><img src="<%= Url.ImagePath("MIX10_Banner_OpenCall.jpg", ViewContext) %>" alt="Open Call For Content" /></a></div>
    <div id="paginate-slides">
        <a href="/Sessions" class="toc current"><img src="<%= Url.ImagePath("MIX10_Thumb_Buxton.jpg", ViewContext) %>" alt="Bill Buxton" /></a> 
        <a href="/About" class="toc"><img src="<%= Url.ImagePath("MIX10_Thumb_Guthrie.jpg", ViewContext) %>" alt="Scott Guthrie" /></a> 
        <a href="/Registration" class="toc"><img src="<%= Url.ImagePath("MIX10_Thumb_Mandalay.jpg", ViewContext) %>" alt="Mandalay Bay"  /></a> 
        <a href="/OpenCall" class="toc"><img src="<%= Url.ImagePath("MIX10_Thumb_OpenCall.jpg", ViewContext) %>" alt="Open Call For Content"  /></a> 
    </div>
</div>
