/// <reference path="jquery-1.3.2.js" />
$(function() {
/** list items that contain lists **/
    $("li ul").closest("li").addClass("hasList");
});

$.fn.extend({
/* really, really simple implementation. some todos:
    - listen to all hashed hrefs on the page so the highlight can change
    - make some time to think of other todos :P */
    highlight: function(callback, data) {
        this.bind("highlight", data || callback, data && callback);
    },
/** hover and click "class"ification **/
    hoverClassIfy: function() {
        $(this).mouseover(function() {
            $(this).addClass("hover");
        });

        $(this).mouseout(function() {
            $(this).removeClass("hover");
        });

        return this;
    },
    clickClassIfy: function() {
        $(this).click(function(ev) {
            if (!($(ev.target).is("a"))) {
                $(this).toggleClass("active");
            }
        });
    }
});

/**************
     ADMIN
 **************/
/** file manager **/
$.fn.extend({
    interceptRemoveFile: function() {
        this.submit(function() {
            var form = this;
            var file = $(form).parent("div.manageFile");
            file.fadeTo(350, .4);
            $.ajax({
                type: "POST",
                url: form.action,
                data: $(form).getDataArray(),
                success: function(response) {
                    if (response === "true") {
                        file.hide(200);
                    } else {
                        this.error();
                    }
                },
                error: function() { file.fadeTo(350, 1); form = file = 0; }
            });
            return false;
        });
    },
    interceptEditFile: function() {
        this.submit(function() {
            var form = this;
            var file = $(form).parent("div.manageFile");
            file.fadeTo(350, .4);
            $.ajax({
                type: "POST",
                url: form.action,
                data: $(form).getDataArray(),
                success: function(response) {
                    if (response === "true") {
                        file.fadeTo(350, 1);
                    } else {
                        this.error();
                    }
                },
                error: function() { file.fadeTo(350, 1); form = file = 0; }
            });
            return false;
        });
    },
    /* quick and brutal :| */
    getDataArray: function(existing) {
        var data = [];
        var elements = $("input[name]", this);

        elements.each(function() {
            var element = $(this);
            if (element.attr("name") !== "returnUri" && (!!(element.val()) || element.val() === "" || element.val() === "0")) {
                data.push({ name: element.attr("name"), value: element.val() });
            }
        });

        return existing instanceof Array ? $.merge(existing, data) : data;
    }
});
$(function() {
    /* add */
    $("form#addExistingFile").submit(function() {
        var form = this;
        var firstFile = $(form).next("div.manageFile")[0];
        $.ajax({
            type: "POST",
            url: form.action,
            data: $(form).getDataArray(),
            success: function(response) {
                $(form.existingFileUrl)
                    .add(form.existingFileTypeName)
                    .add(form.fileMimeType)
                    .add(form.existingFileSizeInBytes)
                    .each(function() { this.value = ""; $(this).trigger("blur") });

                var manageNewFile = document.createElement("div");
                $(manageNewFile).html(response);
                manageNewFile = $(manageNewFile).children();

                manageNewFile.hide();
                if (firstFile) {
                    $(firstFile).before(manageNewFile);
                } else {
                    $(form).after(manageNewFile);
                }
                $("form.flag.removeFile", manageNewFile).interceptRemoveFile();
                $("form.editFile", manageNewFile).interceptEditFile();
                manageNewFile.show(200);

                form = firstFile = 0;
            },
            error: function() { form = firstFile = 0; }
        });
        return false;
    });
    /* remove */
    $("form.flag.removeFile").interceptRemoveFile();
    $("form.editFile").interceptEditFile();
});

/** site settings icon picker **/
$(function() {
    $("form#siteSettings span.hint.icons img").each(function() {
        $(this).click(function() {
            $("#favIconUrl").val($(this).attr("title"));
            $(this).siblings(".selected").removeClass("selected");
            $(this).addClass("selected");
        });
        $(this).hoverClassIfy();
    });
});

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
                if (response.responseText.split("</body>").length > 1)
                    status = "error";

                if (status !== "success") {
                    loadingMessage.fadeOut(300, function() { loadingMessage.css({ left: "-9999em" }) });

                    if (status !== "cancelled") {
                        /* todo: (nheskew) deal with it */
                    }

                    return;
                }

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
                        container.html(loadingContainer.html());
                        loadingMessage.fadeOut(400, function() { loadingMessage.css({ left: "-9999em" }) });
                    });

                    if (callback)
                        container.each(callback, [response.responseText, status, response]);
                },
                50);
            },
            error: function(response, status) {
                loadingMessage.fadeOut(300, function() { loadingMessage.css({ left: "-9999em" }) });
            }
        });
    }
});

/** tabset **/
$(function() {
    $("ul.tab.panes,ul.tab.links").each(function() {
        var tabset = $(this);
        var tabs = $("<ul class=\"tabs\"></ul>");
        tabs.append($("<li class=\"filler\"></li>"));

        var tabsTarget = tabset.attr("id") && $("#" + tabset.attr("id").replace(/tabs/i, ""));

        tabset.children("li").each(function() {
            var tabLabel = tabset.hasClass("panes")
                ? $("h3", this)
                : $("a", this);

            var tabLabelClassName = $(this).removeClass("first").removeClass("last").attr("class");
            var tab = $("<li>" + tabLabel.html() + "</li>");
            tab.attr("class", tabLabelClassName);
            tab.attr("id", $(this).attr("id") ? $(this).attr("id") + "Tab" : null);

            if (tabset.hasClass("panes")) {
                tabLabel.addClass("used");
                tab.data("pane", $(this));
            } else {
                tab.data("dataUri", tabLabel.attr("href"));
                tab.data("dataTarget", tabsTarget);
            }

            $(tab).hoverClassIfy().highlight(function() { tab.selectTab(); });
            tabs.append(tab);
            tab.click(function() { $(this).selectTab(); });
        });

        tabs.append($("<li class=\"filler\"></li>"));
        $("li:first", tabs).addClass("first");
        $("li:last", tabs).addClass("last");
        $("li.active", tabs).prev("li").addClass("beforeActive").end().next("li").addClass("afterActive");

        return tabset.hasClass("panes")
            ? tabset.before(tabs)
            : tabset.replaceWith(tabs);
    });
});
$.fn.extend({
    selectTab: function(uri, data) {
        var tab = $(this);
        var tabPane = tab.data("pane");
        var tabDataUri = tab.data("dataUri");
        var tabDataTarget = tab.data("dataTarget");

        var selectedTab = tab.siblings("li.active");
        var selectedPane = tabPane && tabPane.siblings("li.active");
        if (!selectedTab) return;

        selectedTab.removeClass("active").prev("li").removeClass("beforeActive").end().next("li").removeClass("afterActive");
        tab.addClass("active").prev("li").addClass("beforeActive").end().next("li").addClass("afterActive");

        if (selectedPane) {
            selectedPane.removeClass("active");
            tab.data("pane").addClass("active");
        }

        if (uri || tabDataUri) {
            tabDataTarget.fill(uri || tabDataUri, uri && data);
        }
    },
    isSelectedTab: function() {
        return this.hasClass("active");
    }
});

/** enable/disable checkbox **/
$(function() {
    $("input:checkbox,input:radio").each(function() {
        var checkbox = $(this);

        if (!checkbox.data("label")) {
            checkbox.data("label", checkbox.siblings("label[for='" + checkbox.attr("id") + "']"));
            checkbox.data("label").addClass("forCheckbox").addClass(checkbox.is(":checked") ? "" : "disabled");
            checkbox.addClass("checkbox").addClass("withLabel");
        }

        checkbox.change(function() {
            var cb = $(this);

            // a little extra for radio buttons
            if (cb.is(":radio")) {
                $("input[name='" + cb.attr("name") + "']").each(function() { $(this).data("label").addClass("disabled"); });
            }

            if (cb.is(":checked")) {
                cb.data("label").removeClass("disabled");
            } else {
                cb.data("label").addClass("disabled");
            }
        });
    });
});

/** highlight anchored element **/
$(function() {
    /** highlight anchored element **/
    var hash = window.location.hash;
    if (hash) {
        $(hash).trigger("highlight");
    }
});

/** simple toggle **/
$(function() {
    $(".toggle").live("click", function() {
        var toggler = $(this);
        var togglee = toggler.data("togglee");

        if (!togglee) { return; }

        if (togglee.is(":visible")) {
            togglee.slideUp(100);
            toggler.removeClass("on");
            return false;
        }
        else {
            togglee.slideDown(300, function() { this.focus(); });
            toggler.addClass("on");
        }

        return toggler;
    });
});
$.fn.extend({
    toggles: function(togglee, callback) {
        this.addClass("toggle").data("togglee", togglee);
        if (togglee.is(":visible")) {
            this.addClass("on");
        }
    }
});

/** TinyMCE - textarea editor **/
$(function() {
	window.tinyMceLoading=1;
	window.tinyMCEPreInit = {
	    suffix: "",
	    base: "/skins/admin/scripts/tiny_mce",
	    jquery: "/skins/admin/scripts/jquery-1.3.2.js"
	};
	window.tinyMCE_GZ = { loaded:1 };
	$.ajax({
		type: "GET",
		url: window.tinyMCEPreInit.base+"/tiny_mce.js",
		data: null,
		success: function(){
			window.tinymce.dom.Event._pageInit();
			window.tinyMceLoaded = 1;
			window.tinyMceLoading = 0;
			window.tinyMCE.init({
				mode:"specific_textareas",
				editor_selector:"html",
				theme:"c9",
				dialog_type:0,
				convert_urls:false,
				remove_script_host:false,
				inline_styles:false,
				remove_linebreaks:false,
				jquery:window.tinyMCEPreInit.jquery,
				content_css:"/skins/pdc09/styles/screen.5.css",
				plugins:"searchreplace,inlinepopups,paste",
				theme_advanced_resizing_min_height:100,
				theme_advanced_buttons1:"search,replace,|,cut,copy,paste,|,undo,redo,|,image,|,link,unlink,charmap,|,bold,italic,sub,sup,|,numlist,bullist,formatselect,|,code",
				theme_advanced_buttons2:"",
				theme_advanced_buttons3:"",
				theme_advanced_blockformats:"p,h2,h3,h4,blockquote,pre",
				theme_advanced_resizing:true
			});
		},
		dataType:"script",
		cache:true
	});
});

/** dialog **/
$(function() {
    $("#lightbox #dialog form input:submit").live("click", function() {
        var form = $(this).closest("form");
        
        $.ajax({
            url: form.attr("action"),
            type: "POST",
            data: form.getDataArray(),
            dataType: "html",
            complete: $.lightbox.data("callback"),
            error: function(response, status) { $.lightbox.close(); }
        });
        
        $.lightbox.close();
        return false; 
    });
});

/** page: plugin management **/
$(function() {
    $("#refreshPlugins input.ibutton").click(function() {
        var form = $(this).closest("form");
        $("#allPluginsTab").selectTab(form.attr("action"), form.getDataArray());
        return false;
    });
    $("body.plugin.list form.installPlugin input.ibutton,body.plugin.list form.uninstallPlugin input.ibutton," +
      "body.plugin.list form.installPlugin input.submit,body.plugin.list form.uninstallPlugin input.submit").live("click", function() {
        var form = $(this).closest("form");
        if ($("#installedPluginsTab").isSelectedTab()) {
            $("#installedPluginsTab").selectTab(form.attr("action"), form.getDataArray());
        } else if ($("#notInstalledpluginsTab").isSelectedTab()) {
            $("#notInstalledpluginsTab").selectTab(form.attr("action"), form.getDataArray());
        } else {
            $("#allPluginsTab").selectTab(form.attr("action"), form.getDataArray());
        }
        return false;
    });
    $("body.plugin.list form.enablePlugin input.ibutton,body.plugin.list form.disablePlugin input.ibutton").live("click", function() {
        var form = $(this).closest("form");
        form.closest("li").fill(form.attr("action"), form.getDataArray(), null, "#" + form.closest(".plugin.info").attr("id"));
        return false;
    });
});

/** page: add/edit post/page **/
$(function() {
    $("fieldset.excerpt label[for='bodyShort']", $("body.itemadd.post,body.itemedit.post")).each(function() {
        var label = $(this);
        label.toggles(label.siblings(".html.editor,textarea"));
    });
    $("#post_published:enabled,#page_published:enabled").each(function() {
        var publishedInput = $(this);
        var publishedStateInput = $("#post_statePublished,#page_statePublished");

        var currentDateTime, setCurrentDateTimeOnLoad, gettingCurrentDateTime;

        var setCurrentDateTime = function() {
            if (!currentDateTime) {
                $.post(
                    window.__getDateTimePath,
                    null,
                    function(datetime) {
                        if (!currentDateTime) { currentDateTime = datetime; }
                        if (setCurrentDateTimeOnLoad && $.trim(publishedInput.val()) === "") { publishedInput.val(currentDateTime); }
                        gettingCurrentDateTime = 0;
                    }
                );
            } else if ($.trim(publishedInput.val()) === "") {
                publishedInput.val(currentDateTime);
            }
        };

        if ($.trim(publishedInput.val()) === "" && window.__getDateTimePath) {
            gettingCurrentDateTime = 1;
            setCurrentDateTime();
        }

        publishedInput.change(function() {
            if ($(this).val() !== "") {
                publishedStateInput.attr("checked", "checked");
            }
        });

        publishedStateInput.focus(function() {
            publishedInput.focus();
            if ($.trim(publishedInput.val()) === "") {
                setCurrentDateTimeOnLoad = 1;
                setCurrentDateTime();
            } else {
                setCurrentDateTimeOnLoad = 0;
            }
            publishedInput.blur();
        });

        publishedInput.unload(function() {
            publishedInput = null;
            publishedStateInput = null;
        });
    });

    $("#post_title,#page_title").change(function() {
        $("#post_slug,#page_slug").slugify($(this).val());
    });

    $.fn.extend({
        slugify: function() {
            var cleanReg = new RegExp("[^A-Za-z0-9-]", "g");
            var spaceReg = new RegExp("\\s+", "g");
            var dashReg = new RegExp("-+", "g");

            return function(string) {
                if (!this.is(":enabled"))
                    return;

                slug = $.trim(string);

                if (slug && slug !== "") {

                    slug = slug.replace(spaceReg, '-').replace(dashReg, "-").replace(cleanReg, "");

                    if (slug.length < 1) {
                        return "";
                    }

                    if (slug.Length > 100) {
                        slug = slug.substring(0, 100);
                    }
                }

                this.val(slug.toLowerCase());
            }
        } ()
    });
});
