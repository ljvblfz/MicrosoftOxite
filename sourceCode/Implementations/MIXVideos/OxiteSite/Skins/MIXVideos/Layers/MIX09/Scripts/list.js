$(document).ready(function() {
    $(".post .title").each(function() {
        var speakers = $(this).parent().next().find(".speakerName");
        if (speakers.length > 1)
            $(this).append("<br/><span class='speakers'>" + speakers.map(function() { return $(this).text(); }).get().join(', ') + "</span>");
        else
            $(this).append("<br/><span class='speakers'>" + speakers.text() + "</span>");
           

    });
});