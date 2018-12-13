<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Oxite.Extensions" %>
<a href="/"><h1 class="branding">Mix10</h1></a>
<p class="intro">A 3 day conference for web designers and developers building the world's most innovative web sites.<br />
<strong>March 15-17th, 2010, Las Vegas</strong></p>
<div id="signin">
	<div class="translate">
		<a href="#">Translate</a>
		<div id="MicrosoftTranslatorWidget" style="width: 335px; min-height: 57px; border-color: #344537; background-color: #172827; background-image: url(img/cback.gif)"><noscript><a href="http://www.microsofttranslator.com/bv.aspx?a=http%3a%2f%2flive.visitmix.com%2f">Translate this page</a><br />Powered by <a href="http://www.microsofttranslator.com">Microsoft® Translator</a></noscript></div> <script type="text/javascript"> /* <![CDATA[ */ setTimeout(function() { var s = document.createElement("script"); s.type = "text/javascript"; s.charset = "UTF-8"; s.src = "http://api.microsofttranslator.com/V1/Widget.svc/Embed?appId=H5_fs-ijBdCGxQxPSyCCxdh03oWeOt3S&from=en&layout=ts"; var p = document.getElementsByTagName('head')[0] || document.documentElement; p.insertBefore(s, p.firstChild); }, 0); /* ]]> */ </script>
	</div> | <% Html.RenderPartialFromSkin("LoginUserControl"); %></div>
<div id="search">	
    <ul>
        <li class="first selected mix10">MIX10</li>
        <li class="web">Web</li>
    </ul>
    <span class="g l t lt"></span><span class="g l m lm"></span><span class="g l b lb"></span><span class="g c t ct"></span><span class="g c b cb"></span><span class="g r t rt"></span><span class="g r m rm"></span><span class="g r b rb"></span>     
    <form action="/search">
        <div class="inputHolder">
            <input type="text" id="searchme" class="text" value="" name="term" />
            <div class="submitHolder">
                <input class="theSubmit" type="image" src="/Skins/MIX10/Styles/img/searchSprite.png" onclick="searchIndex(); return false;" />
            </div>
        </div>
    </form>
    <a href="http://www.bing.com" class="bing logo"></a>
</div>
