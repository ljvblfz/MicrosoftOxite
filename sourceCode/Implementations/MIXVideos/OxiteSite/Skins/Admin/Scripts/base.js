window._emailRegex = /^[a-z0-9]+([-+\.]*[a-z0-9]+)*@[a-z0-9]+([-\.][a-z0-9]+)*$/i;

/** field hinting **/
$(document).ready(function() {
    $("form").each(function() { $(this).hintify() });
});
$.extend(jQuery.expr[":"], {
    textarea: function(a) { return $.nodeName(a, 'textarea'); }
});
$.fn.extend({
    hintify: function() {
        this.submit(function() {
            $("[_hint]", this).each(function() { $(this).removeHint() });
        });

        $(window).unload(function() {
            $("form [_hint]").each(function() { $(this).removeHint() });
        });

        $(":text[title],:textarea[title]", this).filter(":enabled").each(function() { $(this).hint() });

        return this;
    },
    hint: function() {
        var hintText = this.attr("title");
        if (!!hintText && (this.is(":text") || this.is(":textarea"))) {
            this.attr("_hint", hintText);
            this.addHint()
            this.focus(function() { $(this).removeHint(); });
            this.blur(function() { $(this).addHint(); });
        }
        return this;
    },
    addHint: function() {
        if ($.trim(this.val()) === "") {
            this.addClass("hinted");
            this.removeClass("active");
            this.val(this.attr("_hint"));
        } else {
            this.addClass("active");
        }
    },
    removeHint: function() {
        if ($.trim(this.val()) === this.attr("_hint")) {
            this.val("");
            this.removeClass("hinted");
        }
        this.addClass("active");
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
});
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

/** username in the login form gets focus on load **/
$(document).ready(function() {
    $("#login_username").focus();
});

/** archives **/
$(document).ready(function() {
    $('.archives ul.yearList li.previous').each(function() {
        $(this).click(function(ev) {
            if (!ev || $(ev.target).not("a").size()) {
                $(this).toggleClass("open");
                $(this).find("h4>span").toggle();
                $(this).children("ul").toggle();
            }
        });

        $(this).hoverClassIfy();
    });
});

/** list item highlighting - just comma seperate additional selectors for now because we like to try to make the browser work **/
$(document).ready(function() {
    $("ul.small li.comment,ul.small li.post,ul.medium li.comment.pendingapproval,ul.medium li.comment.normal").each(function() {
        $(this).hoverClassIfy();
        $(this).clickClassIfy();
    });
});
$.fn.extend({
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

/** flags **/
$(document).ready(function() {
    /* removal */
    $("form.remove.post").submit(function() {
        return window.confirm('really?');
    });
    $("form.flag.remove").submit(function() {
        var form = $(this);
        var comment = $(this).offsetParent("li.comment");
        comment.fadeTo(350, .4);
        $.ajax({
            type: "POST",
            url: this.action,
            data: { id: this.id.value, __RequestVerificationToken: this.__RequestVerificationToken.value },
            success: function(response) {
                if (response === "true") {
                    comment.animate({ height: 0, opacity: 0, marginTop: 0, marginBottom: 0, paddingTop: 0, paddingBottom: 0 }, 200); form = comment = 0;
                } else {
                    this.error();
                }
            },
            error: function() { comment.fadeTo(350, 1); form = comment = 0; }
        });
        return false;
    });
    /* approval */
    $("form.flag.approve").submit(function() {
        var form = $(this);
        var markers = $(".approve,.state", $(this).offsetParent("li.comment"));
        markers.fadeTo(350, .4);
        $.ajax({
            type: "POST",
            url: this.action,
            data: { id: this.id.value, __RequestVerificationToken: this.__RequestVerificationToken.value },
            success: function(response) {
                if (response === "true") {
                    markers.hide(200); form = markers = 0;
                } else {
                    this.error();
                }
            },
            error: function() { markers.fadeTo(350, 1); form = markers = 0; }
        });
        return false;
    });
});

/** highlight anchored element **/
$(document).ready(function() {
    var hash = window.location.hash;
    if (hash) {
        $(hash).each(function() { $(this).highlight() });
    }
});
/* really, really simple implementation. some todos:
    - listen to all hashed hrefs on the page so the highlight can change
    - make some time to think of other todos :P */
$.fn.extend({ 
    highlight: function(highlightColor) {
        this.addClass("highlight");
    }
});

/**************
     ADMIN
 **************/
/** add/edit post **/
$(document).ready(function() {
    if ($("#post_published").is(":enabled")) {
        $("#post_published").change(function() {
            $("#post_statePublished").attr("checked", "checked");
        });
        $("#post_statePublished").focus(function() {
            $("#post_published").focus();
            if ($.trim($("#post_published").val()) === "") {
                var date = new Date();
                $("#post_published").val(date.toShortString());
            }
            $("#post_published").blur();
        });
        $("#post_published").datepicker({
            duration: "",
            dateFormat: "yy/mm/dd '8:00 AM'",
            showOn: "button",
            buttonImage: window.calImgPath,
            buttonImageOnly: true,
            closeAtTop: false,
            isRTL: true
        });
    };

    $("input[name='postState']").change(function() {
        if ($("#post_statePublished").is(":checked")) {
            $("#post_published").addClass("active");
        } else {
            $("#post_published").removeClass("active");
        }
    });

    $("#post_title").change(function() {
        $("#post_slug").slugify($(this).val());
    });

    $.fn.extend({
        slugify: function(string) {
            if (!this.is(":enabled"))
                return;

            slug = $.trim(string);

            if (slug && slug !== "") {
                var cleanReg = new RegExp("[^A-Za-z0-9-]", "g");
                var spaceReg = new RegExp("\\s+", "g");
                var dashReg = new RegExp("-+", "g");

                slug = slug.replace(spaceReg, '-');
                slug = slug.replace(dashReg, "-");
                slug = slug.replace(cleanReg, "");

                if (slug.length * 2 < string.length) {
                    return "";
                }

                if (slug.Length > 100) {
                    slug = slug.substring(0, 100);
                }
            }

            this.val(slug);
        }
    });
});

Date.prototype.toShortString = function() {
    var y = this.getYear();
    var year = y % 100;
    year += (year < 38) ? 2000 : 1900;
    return (this.getMonth() + 1).toString() + "/" + this.getDate() + "/" + year + " " + this.toLocaleTimeString();
};

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
    getDataArray: function() {
        var data = [];
        var elements = this.context.elements;
        var element = i = 0;

        while (element = elements[i++]) {
            if (element.name !== "returnUri" && (!!(element.value) || element.value === "" || element.value === "0")) {
                data.push({ name: element.name, value: element.value });
            }
        }

        return data;
    }
});
$(document).ready(function() {
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
$(document).ready(function() {
    $("form#siteSettings span.hint.icons img").each(function() {
        $(this).click(function() {
            $("#favIconUrl").val($(this).attr("title"));
            $(this).siblings(".selected").removeClass("selected");
            $(this).addClass("selected");
        });
        $(this).hoverClassIfy();
    });
});
