/// <reference path="jquery-1.3.2-vsdoc2.js" />
/// <reference path="base.js" />

$(document).ready(function()
{
	pdc.speakers.init();	
});

pdc.speakers = {
    "names": "",
    "autocomplete": {},
    "init": function() {
        $("#filtersubmit").live("click", function(e) {
            var path = location.pathname.replace(/\/tags\/[^\/]*\/?/i, "/")
                .replace(/\/page\d+\/?/i, "/")
                .replace(/\/$/, "");

            var term = $("#filter").val();

            var post = document.location.protocol + "//" +
                       document.location.host + path +
                       "?term=" + term;

            document.location.href = post;
            return false;
        });

        $("#filter").autocomplete(pdc.speakers.names);

        $.each(pdc.speakers.names, function(i, val) {
            pdc.speakers.autocomplete[val] = "speaker:";
        });

        $("#filter").result(function(event, data, formatted) {
            if (pdc.speakers.autocomplete[formatted]) $("#filter").val(pdc.speakers.autocomplete[formatted] + $("#filter").val());
        });
    }
}