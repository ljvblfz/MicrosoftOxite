/// <reference path="jquery-1.3.2-vsdoc2.js" />
/// <reference path="pdc.js" />

$(document).ready(function() {
    pdc.sharing.init();
    pdc.sessions.wireUpAddRemove(false);
});

pdc.sessions =
{
    // Session removal can occur on the featured page, outside of the session browser
    "buttonTextAdd": "Add this to my schedule",
    "buttonTextRemove": "On my schedule",
    "wireUpAddRemove": function(fromSchedule) {
        $("li.overview>form").bind("click", function(e) {
            e.stopPropagation();
        });
        $("li.overview>form").bind("submit", function(e) {
            e.preventDefault();
            $("input.addremove", this).get(0).blur();

            var sessionId = $("input.hiddenid", this).val();
            var $form = $(this);
            var action = $("input.addremove", $form).hasClass("remove") ? "Remove" : "Add";

            if (action == "Add") {
                // Adding class before the AJAX call to avoid orphaning sessions when clicking buttons in rapid succession
                $("input.addremove", $form).addClass("remove");
                $("input.addremove", $form).removeClass("add");
            } else {
                $("input.addremove", $form).removeClass("remove");
                $("input.addremove", $form).addClass("add");
            }

            var postData = {};
            var path = location.pathname.replace("/", "");
            var postUrl = "/Sessions/" + action + "/" + sessionId + "/Json";

            // Expected result:
            // Success: { result : "success" }
            // Error: { result : "error", message : "[error messaging]" }
            $.ajax({
                method: "POST",
                dataType: "json",
                //##DEV url below
                url: postUrl,
                data: postData,
                //processData: false,
                //##INT url below
                //url : "/Sessions",
                beforeSend: function() {
                    $("input.addremove", $form).attr("disabled", "disabled").val("saving").addClass("saving");
                },
                complete: function() {
                    $("input.addremove", $form).attr("disabled", "").removeClass("saving");
                },
                success: function(data) {
                    if (data.result == "success") {
                        $session = $form.parents("div.session");
                        if (action == "Add") {
                            $("input.addremove", $form).val(pdc.sessions.buttonTextRemove);
                            //		$session.addClass("added");

                            if (fromSchedule) {
                                // Check to see if *all* sessions for this slot have been added to the schedule
                                if ($session.parent().find("div.session").size() == $session.parent().find("div.session").filter(".added").size()) {
                                    $session.parent().prev().find("span.slottoggle").hide();
                                }
                            }
                        } else {
                            $("input.addremove", $form).val(pdc.sessions.buttonTextAdd);
                            var $timeslot = $session.parent().prev();

                            if (fromSchedule) {
                                // animate item before changing its class, which will remove it from the layout
                                // but only if the slot is collapsed
                                //		$session.removeClass("added");
                                if ($timeslot.find("div.timeslotdesc").data("expanded") == false) {
                                    $session
                                    //ie bug		.css("overflow", "hidden")
										    .animate({ "height": "0px", "padding": "0" }, 250, function() {
										        pdc.sessionBrowser.collapseSession($("div.overview", this));
										    });
                                }

                                // Since at least one session is removed from this slot, the expand/collapse button should be visible
                                $timeslot.find("span.slottoggle").show();
                            } else {
                                // $session.removeClass("added");
                            }
                        }
                    }
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    $form.get(0).submit();
                }
            });
        });
    }
}

$(document).ready(function() {
	$('#schedule a.expand').click(function(event){
		this.blur();
		var $this = $(this);
		var id = $this.attr('id');
		var $sessions = $('.' + id);
		if($sessions.is(':visible')){
			$this.html('show');
		}else{
			$this.html('hide');
		}
		$sessions.slideToggle();


		event.preventDefault();
	});
})