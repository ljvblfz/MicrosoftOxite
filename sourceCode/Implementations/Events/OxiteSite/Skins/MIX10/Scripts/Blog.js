/// <reference path="jquery-1.3.2-vsdoc2.js" />
/// <reference path="pdc.js" />

$(document).ready(function()
{
	
	pdc.blog.init();
	//pdc.sharing.init();
	
});

pdc.blog = {
	"labels" : {},
	"init" : function() {
		pdc.blog.prepCommenterForm();
	},
	"prepCommenterForm" : function() {
		$("#addcomment")
			// Put labels inline
			.find("label")
			.each(function() {
				// Store each label's text for future reference
				// Use the label's form element's ID as a key
				var id = $(this).attr("for");
				var text = $(this).text()
				var $field = $("#" + id);
				pdc.blog.labels[id] = text;
				
				// Stuff the label text into the form field, unless there is already content in the field (due to server-side validation)
				if ($field.get(0).tagName.toLowerCase() == "input" && $field.val().length == 0) $field.val(text).addClass("blank");
				else if ($field.get(0).tagName.toLowerCase() == "textarea" && $field.text().length == 0) $field.text(text).addClass("blank");
				
				// Label no longer needs to be on the page for text inputs
				if ($field.get(0).tagName.toLowerCase() == "textarea" || $field.attr("type").toLowerCase() == "text") $(this).remove();
				
				// Wire up handlers for adding & removing the instructional text
				$field.focus(function(e) {
					if ($(this).val() == pdc.blog.labels[$(this).attr("id")]) {
						$(this).val("").removeClass("blank");
					}
				});
				$field.blur(function(e) {
					if ($(this).val() == "") {
						$(this).val(pdc.blog.labels[$(this).attr("id")]).addClass("blank");
					}
				});
			})
			.end()
			// Validate the form
			.submit(function(e) {
				return pdc.blog.validation.isValid();
			})
			.find("input[type='reset']").click(function(e) {
				e.preventDefault();
				
				// Clear any existing error messaging
				$("p.error", "#addcomment").remove();
				
				// Reset all text fields
				$("input[type='text'], textarea", "#addcomment").each(function() {
					var id = $(this).attr("id");
					$(this).val(pdc.blog.labels[id]).addClass("blank");
				});
				
				// Uncheck checkboxes
				$("input[type='checkbox']", "#addcomment").each(function() {
					$(this).get(0).checked = false;
				});
			});
	},
	"validation" : {
		"reEmail" : /email/,	//##TODO this regex should match what is used for server-side validation
		"reUrl" : /url/,	//##TODO this regex should match what is used for server-side validation
		"isValid" : function() {
			var valid = true;
			
			$("p.error", "#addcomment").remove();
			
			// All fields with class=required are required
			$("#addcomment .required").each(function() {
				if ($(this).val() == "" || $(this).hasClass("blank")) {
					$("<p/>")
						.text("This field is required")
						.addClass("error")
						.insertAfter(this);
						
					valid = false;
				}
			});
			
			// Email validation
			$emailfield = $("#commenteremail");
			if (!$emailfield.hasClass("blank") && !pdc.blog.validation.reEmail.test($emailfield.val())) {
				$("<p/>")
					.text("Please enter a valid email address.")
					.addClass("error")
					.insertAfter($emailfield);
					
				valid = false;
			}
			
			// URL validation
			$urlfield = $("#commenterurl");
			if (!$urlfield.hasClass("blank") && !pdc.blog.validation.reUrl.test($urlfield.val())) {
				$("<p/>")
					.text("Please enter a valid website URL.")
					.addClass("error")
					.insertAfter($urlfield);
					
				valid = false;
			}
			
			return valid;
		}
	}
	/** ONLY FOR USE WITH CLIENT-SIDE PAGINATION. NOT RECOMMENDED.
	"init" : function() {
			
		// If a hash exists, we should replace the default content with content that matches the hash
		if (location.hash.length > 0) {
			pdc.blog.parseUri();
			pdc.blog.navigate();
		} else {
			// Wire up events as-is
			pdc.blog.refresh();
		}
	},
	/// Wire up event handlers
	"refresh" : function() {
		// Paging
		pdc.blog.paging.init();
	},
	/// Get the results dictated by the current filter settings via AJAX
	"navigate" : function() {
		$.ajax({
			method : "GET",
			//##DEV url below
			url : "/Temp?request=blog&uri=" + pdc.blog.getUri(),
			//##INT url below
			//url : pdc.blog.getUri(),
			beforeSend : function() {
				//##TODO loading
				$("#primary").empty();
				window.scrollTo(0, 0);
			},
			complete : function() {
				//##TODO
			},
			success : function(html) {
				$("#primary").html(html);
				pdc.blog.refresh();
				
				// Add a hash to the location for back/next browser navigation and bookmarkability
				location = location.pathname + "#" + pdc.blog.getUri(true);
			},
			error : function() {
				location = pdc.blog.getUri();
			}
		});
	},
	/// construct the appropriate URI to GET the current filter stored in the page variables
	"getUri" : function(trimRoot) {
		var uri = trimRoot ? "" : "/blog";
		if (pdc.blog.paging.current > 1) uri += "/page/" + pdc.blog.paging.current;
		
		return uri;
	},
	/// Parse the current #hash path and update the page variables to match it
	"parseUri" : function() {
		var hash = location.hash;
		
		// Paging
		if (hash.indexOf("/page/") > -1) {
			page = hash.substr(hash.indexOf("/page/") + ("/page/").length);
			if (page.indexOf("/") > -1) page = page.substr(0, page.indexOf("/"));
			pdc.blog.paging.current = page;
		}
		
	},
	"paging" : {
		"current" : 1,
		"init" : function() {
			if ($("#blogpaging>a").length > 0) {
				$("#blogpaging>a").click(function(e) {
					e.preventDefault();
					
					// Extract the page to navigate to
					var href = $(this).attr("href");
					var page = href.substr(href.indexOf("/page/") + ("/page/").length);
					if (page.indexOf("/") > -1) page = page.substr(0, page.indexOf("/"));
					
					// Update page variable and go
					pdc.blog.paging.current = page;
					pdc.blog.navigate();
				});
			}
		}
	}
	*/
}