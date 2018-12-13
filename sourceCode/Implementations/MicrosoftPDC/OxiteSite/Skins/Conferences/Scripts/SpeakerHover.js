/// <reference path="jquery-1.3.2-vsdoc2.js" />
/// <reference path="pdc.js" />

$(document).ready(function()
{
	
	pdc.speakerhover.init("body");
	
});

pdc.speakerhover = {
	"init" : function(context) {
		$("a.speaker", context).each(function() {
			var speakerUrl = $(this).attr("href");
			var speakerName = $(this).text();
			$(this).qtip({
				content : {
					//##DEV url below
					url : "/Temp?request=speakerhover&name=" + speakerName + "&url=" + speakerUrl
					//##INT url below
					//url : speakerUrl + "/hover"
				},
				position : {
					corner : {
						target : "bottomMiddle",
						tooltip : "topMiddle"
					}
				},
				adjust : {
					x : 0,
					y : 10
				},
				show : {
					delay: 200
				},
				hide : {
					fixed: true
				},
				style: { 
					width: 200,
					padding: 5,
					background: "#666",
					color: "#fff",
					border: {
						width: 0
					},
					tip: {
						corner : "topMiddle",
						color : "#666"
					}
				}

			});
		});
	}
}