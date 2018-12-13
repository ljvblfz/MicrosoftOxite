$(document).ready(function() {
    pdc.classics.init();
});

pdc.classics = {
    init: function() {
        $("body").append("<div id='lightbox'><div id='lightboxcontent'></div></div>");
        $("div#lightboxcontent").click(function(e) {
            $("div#lightbox").toggle();
        });

        $("div.classic img").click(function(e) {
            $("div#lightbox").toggle();
        });
    }
}