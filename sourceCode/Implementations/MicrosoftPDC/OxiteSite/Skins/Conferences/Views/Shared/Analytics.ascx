<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.ViewModels"%>
<%@ Import Namespace="Oxite.Extensions"%>
<!-- WebTrends -->
<%
    Html.RenderScriptTag("webtrends.js"); %>
<script type="text/javascript">/*<![CDATA[*/var _tag=new WebTrends();_tag.dcsGetId(function(){_tag.dcsCollect();});//]]></script>
<noscript><div><img alt="DCSIMG" id="DCSIMG" width="1" height="1" src="http://m.webtrends.com/dcs1wotjh10000w0irc493s0e_6x1g/njs.gif?dcsuri=/nojavascript&amp;WT.js=No&amp;WT.tv=8.6.1"/></div></noscript>
<!-- End WebTrends -->