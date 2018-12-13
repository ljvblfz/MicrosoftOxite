<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="twitter" class="bucket">
		<h3>PDC Twitterfeed</h3>
	<div id="twtr-profile-widget"></div>
	<script type="text/javascript" src="http://widgets.twimg.com/j/1/widget.js"></script>
	<script type="text/javascript">/*<![CDATA[*/
	new TWTR.Widget({
	  id: 'twtr-profile-widget',
	  loop: false,
	  width: 185,
	  height: 300,
	  theme: {
		shell: {
			background: '#ffffff',
			color: '#333333'
		},
		tweets: {
			background: '#ffffff',
			color: '#60789c',
			links: '#60789c'
		}
	  }
	}).render().setSearch("from:PDC09").start();
	//]]></script>
</div>