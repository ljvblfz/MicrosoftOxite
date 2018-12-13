$(function() {
    $("form.flag.spam").submit(function() {
        var form = $(this);
        var comment = $(this).offsetParent("li.comment");
        $.ajax({
            type: "POST",
            url: this.action,
            data: form.getDataArray(),
            success: function(response) {
                $("form.flag.remove", comment).trigger("submit");
                form = comment = 0;
            },
            error: function() { form = comment = 0; }
        });
        return false;
    });
});