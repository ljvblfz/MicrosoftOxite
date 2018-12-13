window._emailRegex = /^[a-z0-9]+([-+\.]*[a-z0-9]+)*@[a-z0-9]+([-\.][a-z0-9]+)*$/i;

/// <reference path="jquery-1.3.2-vsdoc2.js" />
var pdc = {
    "sessions": {},
    "sessionBrowser": {},
    "twitterFeed": {},
    "heroBanner": {},
    "blog": {},
    "speakerhover": {},
    "speakers": {},
    "sharing": {
        "init": function() {
            $("div.meta li.share").live("click", function(e) {
                if ($(e.target).is("a")) { return; }
                pdc.sharing.bodyClick();
                var ul = $("ul", this);
                ul.width(ul.width()); /* this is needed for bug in ie7 */
                if (ul.is(":visible")) {
                    ul.slideUp(100);
                } else {
                    ul.slideDown(200);
                }
                return false;
            });

            $("body").die("click", pdc.sharing.bodyClick);
            $("body").live("click", pdc.sharing.bodyClick);
        },
        "bodyClick": function() {
            var ul = $("div.meta li.share ul:visible");
            if (ul && !ul.data("opening")) {
                ul.slideUp(100);
            }
        }
    }
}

/** async content loading and handling redirects **/
jQuery.extend({
    _ajax: jQuery.ajax,
    ajax: function(options) {
        var complete = options.complete;

        options.complete = function(response, status) {
            var responseText = response && response.responseText;

            if (responseText && response.getResponseHeader("Content-Type") && "application/json" == response.getResponseHeader("Content-Type").split(";")[0].toLowerCase()) {
                var data = $.httpData(response, "json", options);

                if (data) {
                    if (data.cancel) {
                        status = "cancelled";
                    }

                    if (typeof data.url === "string") {
                        document.location = data.url;
                        return false;
                    }
                }
            } else if (responseText && response.getResponseHeader("X-Oxite-Dialog")) {
                return $(responseText).lightbox(function(response, status) {
                    if (typeof complete === "function") {
                        complete(response, status);
                    }
                    complete = 0;
                });
            }

            if (typeof complete === "function") {
                complete(response, status);
                complete = 0;
            }
        }

        return jQuery._ajax(options);
    },
    error: function(message) {
        if (window.console) { window.console.error(message); }
        return this;
    }
});
$.fn.extend({
    lightbox: function(callback) {
        var container = this;

        if (!($.lightbox)) {
            $("body").append($("<div id=\"lightbox\" style=\"position:absolute;left:-9999em;top:0\"></div>"));
            $.lightbox = $("div#lightbox");

            $.lightbox.close = function() {
                $.lightbox.children().fadeOut(0).end().css({ left: "-9999em" });
            }
        }

        if (!($.lightbox) && typeof callback === "function") { return callback(response, "catastrophe"); }

        $.lightbox.data(
            "callback",
            function(response, status) {
                $.lightbox.close();
                if (typeof callback === "function") {
                    callback(response, status);
                }
                container = callback = 0;
            }
        );

        $.lightbox.width($("body").outerWidth());
        $.lightbox.empty().append(container);
        container.fadeOut(0);

        setTimeout(function() {
            var height = { window: $(window).height(), body: $("body").height() };
            var paddingTop = (($(window).height()) / 2) - container.outerHeight() + $(window).scrollTop();
            $.lightbox.height((height.window > height.body ? height.window : height.body) - paddingTop);
            $.lightbox.css({ paddingTop: paddingTop }).css({ top: 0, left: 0, paddingTop: paddingTop });
            container.fadeIn(0);
        },
        50);
    },
    fill: function(url, data, callback, selector) {
        var container = this;
        var loadingMessage = $("div#loadingMessage");

        if (loadingMessage.length < 1) {
            loadingMessage = $("<div id=\"loadingMessage\" style=\"position:absolute;left:-9999em;top:0\" class=\"loading message\"></div>");
            $("body").append(loadingMessage);
            loadingMessage.data("borderTopWidth", parseInt(loadingMessage.css("borderTopWidth"), 10) || 0);
            loadingMessage.data("borderLeftWidth", parseInt(loadingMessage.css("borderLeftWidth"), 10) || 0);
        }

        loadingMessage.height(container.height() - loadingMessage.data("borderTopWidth"));
        loadingMessage.width(container.outerWidth() - loadingMessage.data("borderLeftWidth"));

        var containerPosition = container.position();
        loadingMessage.css({ top: containerPosition.top, left: containerPosition.left }).fadeIn(300);

        $.ajax({
            url: url,
            type: !data ? "GET" : "POST",
            data: data,
            dataType: "html",
            complete: function(response, status) {
                if (response.responseText.split("</body>").length > 1) {
                    status = "error";
                }
                if (status === "success") {

                    var loadingContainer = $("#loadingContainer");

                    if (loadingContainer.length < 1) {
                        loadingContainer = $("<div id=\"loadingContainer\" style=\"position:absolute;left:-9999em;top:0\"></div>");
                        $("body").append(loadingContainer);
                    }

                    var fillMarkup = selector
                        ? $(selector, $(response.responseText))
                        : $(response.responseText);

                    loadingContainer.width(container.width());
                    loadingContainer.html(fillMarkup.parent().html());

                    setTimeout(function() {
                        var realHeight = loadingContainer.height();

                        container.add(loadingMessage).animate({ height: realHeight }, 500, "swing", function() {
                            $(this).height("auto");

                            container.html(loadingContainer.html());

                            if (callback) {
                                try {
                                    container.each(callback, [response.responseText, status, response]);
                                } catch (e) {
                                    $.error(e);
                                }
                            }

                            loadingMessage.fadeOut(400, function() { loadingMessage.css({ left: "-9999em" }) });
                        });
                    },
                    50);

                } else {
                    loadingMessage.fadeOut(400, function() { loadingMessage.css({ left: "-9999em" }) });

                    if (status !== "cancelled") {
                        /* todo: (nheskew) deal with it */
                    }
                }
            },
            error: function(response, status) {
                loadingMessage.fadeOut(400, function() { loadingMessage.css({ left: "-9999em" }) });
            }
        });
    }
});


/*** jquery live and let die plugin ***/
jQuery.fn.lald = function(arg1, arg2, arg3) {
//live and let die
	var self = this;
	var handler = arg3 ? arg3 : arg2;

	self.live(arg1, arg2, arg3);

	$(function() {
		self.die(arg1, handler);
		$(self.selector).bind(arg1, arg2, arg3);
	});

	return this;
}

jQuery.fn.lad = function(arg1, arg2, arg3) {
//live and die
	var self = this;
	var handler = arg3 ? arg3 : arg2;

	self.live(arg1, arg2, arg3);

	$(function() {
		self.die(arg1, handler);
	});

	return this;
}

/*** gravatar fetch and alt change ***/
$(document).ready(function() {
    $('#comment_email').blur(function() {
        var email = $(this).val();
        if (email.indexOf("@") > 0 && window._emailRegex.test(email)) {
            $.post(window.computeHashPath, { value: email }, function(emailHash) { if (emailHash && emailHash.length < 50) { $('#comment_grav').changeGravatarSrcTo(emailHash); } });
        } else {
            var emailHash = $('#comment_hashedEmail') ? $('#comment_hashedEmail').val() : "";
            $('#comment_grav').changeGravatarSrcTo(emailHash);
        }
    });
    $('#comment_name').blur(function() {
        $('#comment_grav').changeGravatarAltTo($(this).val());
    });

    // intercept day paging in schedule browser
    $('.days > li > a').click(function() {
        var day = $(this).text();
        var mineUrl = $('#mySessions').attr('href');
        var allUrl = $('#allSessions').attr('href');
        $('#mySessions').attr('href', replaceDays(mineUrl, day));
        $('#allSessions').attr('href', replaceDays(allUrl, day));
    });
});

function replaceDays(value, day) {
    if (value) {
        value = value.replace("Monday", day).replace("Tuesday", day).replace("Wednesday", day).replace("Thursday", day).replace("Friday", day);
        var anyMatch = value.match(/Monday/) || value.match(/Tuesday/) || value.match(/Wednesday/) || value.match(/Thursday/) || value.match(/Friday/);
        if (!anyMatch) value = value + '/' + day;
        return value;
    }
};

$.fn.extend({
    changeGravatarSrcTo: function(emailHash) {
        var gravatar = $(this).find("img.gravatar");

        var gravatarUrlParts = gravatar.attr("src").split("?");
        var gravatarPathParts = gravatarUrlParts[0].split("/");

        gravatarPathParts[gravatarPathParts.length - 1] = emailHash;

        gravatar.attr("src", gravatarPathParts.join("/") + "?" + gravatarUrlParts[1]);
    },
    changeGravatarAltTo: function(name) {
        var gravatar = $(this).find("img.gravatar");
        if ($.trim(name) !== "") {
            gravatar.attr("title", name + " (gravatar)");
        } else {
            gravatar.attr("title", "(gravatar)");
        }
    }
});

//page search box UI

$(document).ready(function() {
    var defaultClass = 'default';
    var defaultValues = {
        defaultStr: '',
        sessionbrowser: 'Search Sessions',
        speakerList: 'Search Speakers',
        searchResults: 'Search MIX10'
    };

    var getDefaultValue = function() {
        var bodyID = $('body').attr('id');
        if (bodyID && defaultValues[bodyID]) {
            return defaultValues[bodyID];
        } else {
            return defaultValues.defaultStr;
        }
    }

    var $inputFilter = $('input#filter');


    $inputFilter.data('defaults', {
        defaultValue: getDefaultValue(),
        defaultClass: defaultClass
    }).addClass(
       defaultClass
    ).val(
       getDefaultValue()
    ).focus(function() {
        var $this = $(this);
        if (($this.val() == $inputFilter.data('defaults').defaultValue) && $this.hasClass($this.data('defaults').defaultClass)) {
            $this.val(
                ''
            ).removeClass(
                $this.data('defaults').defaultClass
            );
        } else {
            $this.select();
        };
    }).blur(function() {
        var $this = $(this);
        if ($this.val() == '') {
            $this.addClass(
                $this.data('defaults').defaultClass
            ).val(
                $this.data('defaults').defaultValue
            );
        };
    });
});


//site search box UI

$(document).ready(function() {
    var containerID = 'search';
    var selectedClass = 'selected';

    var webSearch = {
        'action': 'http://www.bing.com',
        'inputName': 'q',
        'className': 'bing'
    };

    var tabs = {
        'mix10': {
            'action': '/search',
            'inputName': 'term'
        },
        'web': webSearch
    };

    var $searchContainer = $('#' + containerID);
    var $searchForm = $searchContainer.find('form');
    var $searchField = $searchForm.find('input[type="text"]');

    var changeSearchForm = function(action, inputName) {
        $searchForm.attr(
            'action', action
        );
        $searchField.attr(
            'name', inputName
        );
    };

    for (var tab in tabs) {
        var $tab = $searchContainer.find('.' + tab);
        $tab.data(
            'props', tabs[tab]
        ).click(function() {
            var $this = $(this);
            var props = $this.data('props');

            $this.addClass(
                selectedClass
            ).siblings(
                'li'
            ).removeClass(
                selectedClass
            );

            changeSearchForm(props.action, props.inputName);
        });

        if ($tab.hasClass(selectedClass)) {
            $tab.css('minWidth', $tab.width());
        } else {
            $tab.addClass(
                selectedClass
            ).css(
                'minWidth', $tab.width()
            ).removeClass(
                selectedClass
            );
        }
    }

    $searchContainer.find(
        '.' + webSearch.className
    ).click(function(event) {
        if ($searchField.val() != '') {
            event.preventDefault();
            changeSearchForm(webSearch.action, webSearch.inputName);
            $searchForm.submit();
        }
    });
});

//Translation widget

$('#signin div.translate a').lald('click', function(event) {
	$('#MicrosoftTranslatorWidget').toggle();

	event.preventDefault();
});


$('#MSTWGoImage').live('click', function(event) {
	$('#MicrosoftTranslatorWidget').hide();
	$('body').addClass('translated');
});

$('#MSTTExitLink').live('click', function(event) {
	$('body').removeClass('translated');
});

$(document).ready(function() {
	if(document.cookie.indexOf('mstmode=auto')>=0){
		$('body').addClass('translated');
	}
});
