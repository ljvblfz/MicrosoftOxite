/// <reference path="jquery-1.3.2-vsdoc2.js" />
/// <reference path="pdc.js" />

$(document).ready(function()
{
    pdc.sessionBrowser.init();
		
	if (pdc.sessionBrowser.which == "sessions") {
	    pdc.sessionBrowser.sessions.init();		
	} else {
	    pdc.sessionBrowser.schedule.init();	    
	}
});

pdc.sessionBrowser = {
    "which": "",
    "init": function() {
        // Rearrange the Share My Schedule markup and wire up event handlers
//        var $share = $("#sharemyschedule").remove();
//        var sharelabel = $("h4", $share).text();
//        var $scheduleurlinput = $("<input/>").attr("id", "scheduleurlinput").val($("#scheduleurl", $share).text());

        // alter the markup of the enhanced div
//        $share
//			.click(function(e) { e.stopPropagation(); })
//			.attr("id", "sharemyschedulepopup")
//			.find("h4").remove().end()
//			.find("#schedulesharing").hide().end()
//			.find("#scheduleurl").remove().end()
//			.append($scheduleurlinput)
//			.find("#scheduleurlinput").focus(function() {
//			    $(this).get(0).select();
//			}).end()
//			.find("form").prependTo($share).submit(function(e) { e.stopPropagation(); });

        // add the button to the DOM and wire it up
//        $("<span/>")
//			.attr("id", "sharemyschedulelabel")
//			.html(sharelabel)
//			.appendTo("#tabs")
//			.qtip({
//			    content: {
//			        prerender: true,
//			        text: $share
//			    },
//			    position: {
//			        corner: {
//			            target: "topMiddle",
//			            tooltip: "bottomMiddle"
//			        },
//			        adjust: {
//			            x: 0
//			        }
//			    },
//			    show: {
//			        when: { event: "click" }
//			    },
//			    hide: {
//			        when: { event: "unfocus" }
//			    },
//			    style: {
//			        width: 240,
//			        padding: 5,
//			        background: "#666",
//			        color: "#fff",
//			        border: {
//			            width: 0
//			        },
//			        tip: {
//			            corner: "bottomMiddle",
//			            color: "#666",
//			            size: { x: 16, y: 8 }
//			        }
//			    }
//			});


//        // A click anywhere else will dismiss the box
//        $("body").click(function(e) {
//            $share.hide();
//        });

        // Wireup AJAX for checkbox
//        $("#makeschedulepublic").click(function(e) {
//            var postUrl = "Schedule/Share/Json";
//            var postData = "schedulesharing=Submit";
//            var cb = $(this).get(0).checked;
//            if (cb) {
//                postData += "&makeschedulepublic=true";
//            }

//            // Expected result:
//            // Success: { result : "success" }
//            // Error: { result : "error", message : "[error messaging]" }
//            $.ajax({
//                method: "POST",
//                dataType: "json",
//                //##DEV url below
//                url: postUrl,
//                //##INT url below
//                //url : "/Sessions",
//                data: postData,
//                beforeSend: function() {
//                    $("#makeschedulepublic").attr("disabled", "disabled");
//                    $("body").addClass("wait");
//                },
//                complete: function() {
//                    $("#makeschedulepublic").attr("disabled", "");
//                    $("body").removeClass("wait");
//                },
//                success: function(data) {
//                    if (data.result == "error") {
//                        // In case of AJAX failure, submit the form via HTTP
//                        $("#schedulesharing").trigger("click");
//                    }
//                },
//                error: function(xhr, ajaxOptions, thrownError) {
//                    // In case of AJAX failure, submit the form via HTTP
//                    $("#schedulesharing").trigger("click");
//                }
//            });
//        });
    },

    // "wireUpAddRemove" is in sessions.js

    "collapseSession": function($overview, speedOverride) {
        var speed = speedOverride ? speedOverride : 250;
        var $details = $overview.next();
        $details.slideUp(speed);
        $overview.data("expanded", false);

        // if all items are collapsed, action button should be "expand"
        var expandIt = true;
        $("div.session>div.overview").each(function(i) {
            if (!$(this).data("expanded")) { expandIt = false; }
        });
        if (expandIt) {
            $("#scheduleExpando")
				.text(pdc.sessionBrowser.sessions.expandText).
				data("action", "expand");
        }
    },
    "expandSession": function($overview) {
        var $details = $overview.next();
        $details.slideDown(500);
        $overview.data("expanded", true);

        // if at least one item is expanded, action button should be "collapse"
        $("#scheduleExpando").text(pdc.sessionBrowser.sessions.collapseText).data("action", "collapse");
    },
    "sessions": {
        "speakers": null,
        "tags": null,
        "test": null,
        "autocomplete": {},
        "expandText": "expand all",
        "collapseText": "minimize all",
        "filter": {
            "main": "",
            "tag": "",
            "search": "",
            "speaker": "",
            "id": ""
        },
        "init": function() {
            // shortcut
            var _ps = pdc.sessionBrowser.sessions;

            // If a hash exists, we should replace the default content with content that matches the hash
            _ps.parseUri();
            if (location.hash.length > 0) {
                _ps.navigate();
                _ps.updateNavigation();
            }

            // Paging
            _ps.paging.init();

            // Tag clicks for each session
            _ps.wireUpTags("p.tags");

            $("#currentTag .closebutton").live("click", function() {
                pdc.sessionBrowser.sessions.filter.tag = "";
                pdc.sessionBrowser.sessions.paging.current = 1;
                pdc.sessionBrowser.sessions.navigate();
                pdc.sessionBrowser.sessions.updateNavigation();
                pdc.sessionBrowser.sessions.clearTagState();
            });

            // Prepare tags div
            _ps.prepareTags();

            //------- HIJACK FILTER FORM

            $("div.controlbar>form").bind("submit", function(e) {
                e.preventDefault();
                $("#filtersubmit", this).get(0).blur();

                // Reset paging when filter is changed
                _ps.paging.current = 1;

                _ps.filter.search = $("#filter", this).val();

                _ps.navigate();
            });

            //------- WIRE UP AUTOCOMPLETE

            $("#filter").autocomplete(_ps.tags.concat(_ps.speakers));

            // convert the arrays into dictionaries
            // the value of each is what will be prepended to the term in the filter box autocomplete
            jQuery.each(_ps.tags, function(i, val) {
                //_ps.autocomplete[val] = "tag:";
            });
            jQuery.each(_ps.speakers, function(i, val) {
                //_ps.autocomplete[val] = "speaker:";
            });
            $("#filter").result(function(event, data, formatted) {
                if (_ps.autocomplete[formatted]) $("#filter").val(_ps.autocomplete[formatted] + $("#filter").val());
            });

            // Hijack all user filters
            /*
            $("ol.userFilter>li>a").click(function(e) {
            e.preventDefault();
            pdc.sessionBrowser.sessions.navigate($(this).attr("href"));
            pdc.sessionBrowser.sessions.updateNavigation();
            });*/

            //pdc.sessionBrowser.wireUpAddRemove(false);
        },
        /// Take the tags div at the bottom of the page, move it to the top, 
        ///		wire up buttons to hide/show it,
        "prepareTags": function() {
            var $tags = $("#sessiontags");
            pdc.sessionBrowser.sessions.wireUpTags("#sessiontags>ul>li", true);
        },
        ///	Wire up each individual tag to kick off a search
        "wireUpTags": function(selector, closeTagsDiv) {
            $("a", selector).live("click", function(e) {
                e.preventDefault();
                var pathParts = $(this).attr("href").split("/");
                pdc.sessionBrowser.sessions.filter.tag = pathParts[pathParts.length - 1];
                pdc.sessionBrowser.sessions.paging.current = 1;
                pdc.sessionBrowser.sessions.navigate();
                pdc.sessionBrowser.sessions.updateNavigation();

                //if (closeTagsDiv) $("div.showtags").trigger("click");
            });
        },
        /// Get the results dictated by the current filter settings via AJAX
        "navigate": function(url) {
            pdc.sessionBrowser.sessions.setTagState();
            $("#sessions").fill(url || pdc.sessionBrowser.sessions.getUri(), null, function(html) {
                // Add a hash to the location for back/next browser navigation and bookmarkability
                location = location.pathname + "#" + pdc.sessionBrowser.sessions.getUri(true);

                // Keep form actions up to date to maintain state when AJAX fails
                $("form").each(function() {
                    var action = $(this).attr("action");
                    if (action.toLowerCase().indexOf(location.pathname.toLowerCase()) > -1) {
                        $(this).attr("action", location.pathname + "#" + pdc.sessionBrowser.sessions.getUri(true));
                    }
                });

                pdc.sessions.wireUpAddRemove();
            });
        },
        /// construct the appropriate URI to GET the current filter stored in the page variables
        "getUri": function(trimRoot) {
            var uri = trimRoot ? "" : this.getUriRoot(location.pathname.toLowerCase());  //"/sessions";
            var filter = pdc.sessionBrowser.sessions.filter;
            var paging = pdc.sessionBrowser.sessions.paging;

            if (filter.main.length > 0) uri += "/" + filter.main;
            if (filter.tag.length > 0) uri += "/tags/" + filter.tag;
            if (paging.current > 1) uri += "/page" + paging.current;
            if (filter.search.length > 0) uri += "?term=" + filter.search;

            return uri;
        },
        "getUriRoot": function(uri) {
            return uri.replace(/\/tags\/[^\/]*\/?/i, "/")
                .replace(/\/page\d+\/?/i, "/")
                .replace(/\/$/, "");
        },
        /// Parse the current #hash path and update the page variables to match it
        "parseUri": function() {
            var hash = location.hash;
            var filter = pdc.sessionBrowser.sessions.filter;

            var search = "", tag = "";
            //##TODO rewrite to user regular expressions
            //##		e.g. "/video/tag/WFC" should match a tag
            //##			 "/search/tag/page/2" should not
            if (location.search.toLowerCase().indexOf("term=") > -1) {
                search = location.search.substr(location.search.toLowerCase().indexOf("search=") + ("search=").length);
                if (search.indexOf("&") > -1) search = search.substr(0, search.indexOf("&"));
            } else if (hash.toLowerCase().indexOf("term=") > -1) {
                search = hash.substr(hash.toLowerCase().indexOf("term=") + ("term=").length);
                if (search.indexOf("&") > -1) search = search.substr(0, search.indexOf("&"));
                hash = hash.split("?")[0];
            }

            filter.search = search;

            // Filter by filter text; only one of the four can be populated at one time
            if (location.pathname.toLowerCase().indexOf("/tags/") > -1) {
                tag = location.pathname.substr(location.pathname.toLowerCase().indexOf("/tags/") + ("/tags/").length);
                if (tag.indexOf("/") > -1) tag = tag.substr(0, tag.indexOf("/"));
            } else if (hash.toLowerCase().indexOf("/tags/") > -1) {
                tag = hash.substr(hash.toLowerCase().indexOf("/tags/") + ("/tags/").length);
                if (tag.indexOf("/") > -1) tag = tag.substr(0, tag.indexOf("/"));
            }

            filter.tag = tag;

            // Paging
            if (hash.indexOf("/page") > -1) {
                page = hash.substr(hash.indexOf("/page") + ("/page").length);
                if (page.indexOf("/") > -1) page = page.substr(0, page.indexOf("/"));
                pdc.sessionBrowser.sessions.paging.current = page;
            }
        },
        /// Update the navigation (filter bar text entry, pagination, etc.) to match the current page variables
        "updateNavigation": function() {
            var filter = pdc.sessionBrowser.sessions.filter;

            $("#filter").removeClass("default").val(filter.search);

            // Synchronize paging links
            //##TODO this is unnecessary? Should be since HTML fragment will include up to date paging control
            //## what about when synchronizing paging from #hash?
        },
        "setTagState": function() {
            var tagLink = $("#sessiontags li a[href$='/" + pdc.sessionBrowser.sessions.filter.tag + "']");

            this.clearTagState();

            if (tagLink.length < 1) { return; }

            var tagName = tagLink.text();
            $("#currentTag").html("Tag: <span class=\"current tag\">" + tagName + "</span>&nbsp;<a class=\"closebutton\">&nbsp; &nbsp; &nbsp;</a>");
            $("#sessiontags li a:contains('" + tagName + "')").parent().addClass("current");
        },
        "clearTagState": function() {
            $("#currentTag").html("");
            $("#sessiontags li.current").removeClass("current");
        },
        "paging": {
            "current": 1,
            "init": function() {
                $(".paging a").live("click", function() {
                    // Extract the page to navigate to
                    var href = $(this).attr("href");
                    var page = href.substr(href.toLowerCase().indexOf("/page") + ("/page").length);
                    if (page.indexOf("/") > -1) page = page.substr(0, page.indexOf("/"));

                    if (href == "/Page1/CountAll") {
                        url = "/Sessions" + href;
                    }
                    else {
                        // Update page variable and go
                        pdc.sessionBrowser.sessions.paging.current = page;
                        url = href;
                    }

                    $("html, body").animate({ scrollTop: $("#browser").offset().top - 15 }, 500);
                    pdc.sessionBrowser.sessions.navigate(url);
                    pdc.sessionBrowser.sessions.updateNavigation();

                    return false;
                });
            }
        }
    },
    "schedule": {
        "day": "Monday",
        "expandText": "expand time slot",
        "collapseText": "close time slot",
        "init": function() {
            var schedule = $("#schedule");
            // If a hash exists, we should replace the default content with content that matches the hash
            if (location.hash.length > 0) {
                pdc.sessionBrowser.schedule.parseUri();
                pdc.sessionBrowser.schedule.navigate();
                pdc.sessionBrowser.schedule.updateNavigation();
            }

            // Add class to collapse appropriate sessions
            schedule.addClass("enabled");

            //------- EXPAND/COLLAPSE TIMESLOT
            //
            // add to all Session blocks, unless there's nothing to expand
            pdc.sessionBrowser.schedule.updateTimeslotHeaders();

            $(".slottoggle", schedule).live("click", function(e) {
                var $toggle = $(this);
                var $notadded = $toggle.closest("div.timeslot").next("div.sessions").find("li.session:not(.added),li.workshop:not(.added),li.seminar:not(.added)");
                if ($toggle.data("expanded")) {
                    $toggle.data("expanded", false);
                    
                    // Collapse all sessions which have not been added to the schedule
                    $notadded.slideUp(200);

                    // Update button text & style
                    $toggle.text(pdc.sessionBrowser.schedule.expandText)
						   .removeClass("shown");
                } else {
                    $toggle.data("expanded", true);
                    
                    // Expand all sessions which have not been added to the schedule
                    $notadded.slideDown(300);

                    // Update button text & style
                    $toggle.text(pdc.sessionBrowser.schedule.collapseText)
						   .addClass("shown");
                }
            });

            //------- EXPAND/COLLAPSE INDIVIDUAL DETAILS
            //
            //            $(".session .overview, .workshop .overview", schedule).live("click", function() {
            //                if ($(this).data("expanded")) {
            //                    pdc.sessionBrowser.collapseSession($(this));
            //                } else {
            //                    pdc.sessionBrowser.expandSession($(this));
            //                }
            //            });

            //------- DAY NAVIGATION
            //
            // Hijack days buttons
            $("ol.days>li>a").click(function(e) {
                e.preventDefault();
                pdc.sessionBrowser.schedule.day = $(this).text();
                pdc.sessionBrowser.schedule.navigate($(this).attr("href"));
                pdc.sessionBrowser.schedule.updateNavigation();
                pdc.sessions.wireUpAddRemove(true); 
            });
        },
        /// Get the results dictated by the current filter settings via AJAX
        "navigate": function(url) {
            $("#schedule").fill(
                url || pdc.sessionBrowser.schedule.getUri(),
                null,
                function() {
                    location = location.pathname + (pdc.sessionBrowser.schedule.getUri(true).length > 1 ? "#" + pdc.sessionBrowser.schedule.getUri(true) : "");
                    pdc.sessionBrowser.schedule.updateTimeslotHeaders();
                    pdc.sessions.wireUpAddRemove(true); 
                });            
        },
        /// construct the appropriate URI to GET the current filter stored in the page variables
        "getUri": function(trimRoot) {
            var uri = trimRoot ? "" : "/schedule";
            return uri + "/" + pdc.sessionBrowser.schedule.day;
        },
        /// Parse the current #hash path and update the page variables to match it
        "parseUri": function() {
            var hash = location.hash;
            var day = hash.substr(hash.indexOf("#/") + ("#/").length);
            pdc.sessionBrowser.schedule.day = day;
        },
        /// Update the day navigation to match currently selected day
        "updateNavigation": function() {
            $("ol.days>li>a").removeClass("active");
            $("ol.days>li>a:contains('" + pdc.sessionBrowser.schedule.day + "')").addClass("active");
        },
        "updateTimeslotHeaders": function() {
            $("div.sessions", $("#schedule")).each(function() {
                pdc.sessionBrowser.schedule.updateTimeslotHeader($(this));
            });
        },
        "updateTimeslotHeader": function($sessions) {
            // Add the button but only show it if there are sessions in this slot that have not yet been added
            $div = $sessions.prev();

            if ($div.is(".hassessions")) {
                $("<span />")
				    .addClass("slottoggle")
				    .text(pdc.sessionBrowser.schedule.expandText)
				    .prependTo($div.find("div.timeslotdesc"))
				    .parent()
				    .data("expanded", false);

                if ($sessions.children("div:not(.added)").size() > 0) $("span.slottoggle").show();
            }
        }
    }
}
