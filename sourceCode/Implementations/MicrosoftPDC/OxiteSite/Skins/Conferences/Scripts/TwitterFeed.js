/// <reference path="jquery-1.3.2-vsdoc2.js" />
/// <reference path="pdc.js" />

$(document).ready(function() {
	// Phase 1: no live updates
//	pdc.twitterFeed.lastTweetId = pdc.twitterFeed.getLastTweetId();
//	pdc.twitterFeed.timer = setTimeout("pdc.twitterFeed.checkForNewTweets()", pdc.twitterFeed.interval);
});

pdc.twitterFeed = {
	lastTweetId: 0,
	timer: null,
	interval: 30000,
	checkForNewTweets: function() {
		$.ajax({
			type: "GET",
			//##DEV url below
			url: "/Data/SampleTwitterFeed.xml",
			//##INT url below
			//url : /Twitter,
			data: {},
			beforeSend: function() { clearTimeout(pdc.twitterFeed.timer); },
			complete: function() { pdc.twitterFeed.timer = setTimeout("pdc.twitterFeed.checkForNewTweets()", pdc.twitterFeed.interval); },
			success: function(xml, textStatus) {
				if ($("entry", xml).length > 0) {
					// Get first Tweet ID
					var firstId = $("entry:first>id", xml).text();
					firstId = firstId.substr(firstId.lastIndexOf(":") + 1);

					// Add all new tweets to the top of the list if there have been additions
					if (pdc.twitterFeed.lastTweetId < firstId) {
						$("entry", xml).each(function() {
							// Extract Tweet ID
							var thisId = $("id", this).text();
							thisId = thisId.substr(thisId.lastIndexOf(":") + 1);

							// prepend to the list as long as the tweets are more recent than the last logged tweet
							if (thisId > pdc.twitterFeed.lastTweetId) {
								if (pdc.twitterFeed.lastTweetId > 0) {
									$("#tweet" + pdc.twitterFeed.lastTweetId).before(pdc.twitterFeed.formatItem(this));
								} else {
									$("#twitter>ul").append(pdc.twitterFeed.formatItem(this));
								}
								$("#tweet" + thisId).slideDown("slow");
							}
						});
					}

					// remove any error messaging
					if ($("#twitter>ul>li").length > 1) $("#tweetError,#tweetNoEntries").remove();

					pdc.twitterFeed.lastTweetId = pdc.twitterFeed.getLastTweetId();
				}
			},
			error: function(XMLHttpRequest, textStatus, errorThrown) {
				//##TODO error handling
			}
		});
	},
	formatItem: function(item) {
		var id, text, from_user;

		id = $("id", item).text();
		id = id.substr(id.lastIndexOf(":") + 1);

		text = $("title", item).text();

		from_user = $("author>name", item).text();
		from_user = from_user.substr(0, from_user.indexOf(" "));
		from_user_link = $("author>uri", item).text();

		return "<li id=\"tweet" + id + "\" style=\"display: none;\">" +
		"<a href=\"" + from_user_link + "\">@" + from_user + "</a>: " +
		text +
		"</li>";
	},
	getLastTweetId: function() {
		return $("#twitter>ul>li:first").attr("id").substring(5);
	}
};
