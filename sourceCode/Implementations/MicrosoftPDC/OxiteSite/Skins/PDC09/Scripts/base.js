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
            $("div.share").live("click", function(e) {
                if ($(e.target).is("a")) { return; }
                var ul = $("ul", this);
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
            var ul = $("div.share ul:visible");
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
        if (window.console) {  window.console.error(message); }
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
    if(value) { 
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
