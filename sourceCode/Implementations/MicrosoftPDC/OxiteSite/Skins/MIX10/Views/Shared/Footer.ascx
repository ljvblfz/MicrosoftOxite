<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="footer" class="finner">
    <div id="footerContent">
        <a class="flogo" href="/"><img src="<%= Url.ImagePath("logo_footer.png", ViewContext) %>" alt="Mix10" /></a>

            <ul id="fnav">
                <li><a href="/About">About</a></li>
                <li><a href="/Contact">Contact</a></li>
                <li><a href="/Terms">Terms</a></li>
                <li><a href="/Privacy">Privacy</a></li>
            </ul>
            <p class="copyright">
                This site is hosted for Microsoft by ORCS Web. Copyright 2007-2010 Microsoft</p>
        <div class="clear">
        </div>
    </div>    
</div>
