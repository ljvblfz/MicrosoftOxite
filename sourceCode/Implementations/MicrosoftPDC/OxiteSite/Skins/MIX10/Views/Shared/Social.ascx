<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Oxite.Extensions" %>
<h4 class="split">Connect with MIX10</h4>
<ul id="social_icons">
    <li><a href="/News/RSS">
        <img src="<%= Url.ImagePath("rss.jpg", ViewContext) %>" alt="feed of MIX10 news"></a></li>
    <li><a href="https://www.ustechsregister.com/mixmailinglist/main.aspx">
        <img src="<%= Url.ImagePath("mail.jpg", ViewContext) %>" alt="mail"></a></li>
    <li><a href="http://twitter.com/mixevent">
        <img src="<%= Url.ImagePath("twitter.jpg", ViewContext) %>" alt="twitter"></a></li>
    <li><a href="http://www.flickr.com/photos/45356797@N08/sets/">
        <img src="<%= Url.ImagePath("flickr.jpg", ViewContext) %>" alt="flickr"></a></li>
    <li><a href="http://www.facebook.com/pages/MIX10/60101929630">
        <img src="<%= Url.ImagePath("facebook.jpg", ViewContext) %>" alt="facebook"></a></li>
</ul>
