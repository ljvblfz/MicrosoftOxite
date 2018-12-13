/// <reference path="jquery-1.3.2-vsdoc2.js" />
/// <reference path="pdc.js" />
$(document).ready(function() {
    pdc.heroBanner.init();
});

pdc.heroBanner = {
    "numberOfSections": 0,
    // Augments the markup to support paging and wires up all handlers
    "init": function() {
        var heroBanner = $("#herobanner");
        // Set items up for dynamic display
        heroBanner.addClass("heroenabled");
        heroBanner.append("&nbsp;");
        var heroSections = $("h3.herosection", heroBanner);

        // Activate section paging and nav buttons
        heroSections.each(function(i, elm) {
            var $h3 = $(this);
            var $div = $h3.next();
            var curSection = pdc.heroBanner.numberOfSections;

            var templateType = $div.attr("class");
            templateType = templateType.substr(templateType.indexOf("herotype"), ("herotype").length + 1);
            pdc.heroBanner.paging.init(i, pdc.heroBanner.paging.itemsPerPage[templateType], templateType);

            $h3.remove().appendTo(heroBanner).css("display", "inline").click(function(e) {
                pdc.heroBanner.sectionNav(curSection);
            });

            pdc.heroBanner.numberOfSections++;
        });

        //disable the auto rotate when any area of the banner is clicked
        heroBanner.click(function() {
            window.clearTimeout(pdc.heroBanner.autorotate.timer);
            pdc.heroBanner.autorotate = false;
        });

        // Show first section, first item
        pdc.heroBanner.sectionNav(0);

        //start the banner rotation
        if ($("div.herosection:first div.heropage").length > 1) {
            pdc.heroBanner.autorotate.timer = window.setTimeout("pdc.heroBanner.autorotate.rotate()", pdc.heroBanner.autorotate.delay);
        }
    },
    "currentSection": 0,
    "sectionNav": function(index) {
        // Navigate to first page in selected section
        pdc.heroBanner.paging.goTo(index, 0);

        // Hide all sections and show selected section
        $("#herobanner>div").hide();
        $("#herobanner>div:eq(" + index + ")").show();

        // Hide all pagination and show pagination for selected section
        $("div.pagination").hide();

        var pageHero = $("#pagHero" + index);
        if (pageHero.length > 1) {
            pageHero.show();
        }

        // Highlight selected section in nav
        $("h3.herosection").removeClass("heroactive");
        $("#herobanner>h3:eq(" + index + ")").addClass("heroactive");
        pdc.heroBanner.currentSection = index;
    },
    "paging": {
        "itemsPerPage": {
            "herotype1": 1,
            "herotype2": 2,
            "herotype3": 2
        },
        "pages": [],
        "currentPage": 0,
        "init": function(index, itemsperpage, templateType) {
            var $page;
            var $items = $("#herobanner>div:eq(" + index + ")>div");

            pdc.heroBanner.paging.pages[index] = 0;

            // Split items into page divs
            $items.each(function(i, elm) {
                if (i % itemsperpage == 0) {
                    // Create a new page
                    $page = $("<div />").addClass("heropage");
                }

                // Move this item to the current page
                $page.append(this);

                // are we at the end of the page or the end of the items collection?
                if ((i + 1) % itemsperpage == 0 || (i + 1) >= $items.length) {
                    // Move the page to the section
                    $("#herobanner>div:eq(" + index + ")").append($page);
                    pdc.heroBanner.paging.pages[index]++;
                }
            });
            //if (templateType == "herotype1") {
            // Add the pagination div
            var $pagination = $("<div></div").hide();
            $pagination.addClass("pagination").appendTo("#herobanner").attr("id", "pagHero" + index);

            // Add buttons to the pagination div for each page div
            $("#herobanner>div:eq(" + index + ")>div").each(function(i, elm) {
                var $pageButton = $("<span/>").appendTo($pagination);
                $pageButton.append("<span />");
                var ii = i + 1;
                $pageButton.find("span").text(ii);
                $pageButton.click(function(e) {
                    pdc.heroBanner.paging.goTo(index, parseInt($("span", this).text()) - 1);
                });
            });
            //}
            // Add back/next buttons
            $("<div />")
				.addClass("pagnav pagnavback")
				.appendTo("#herobanner>div:eq(" + index + ")")
				.html("<img src='/Skins/PDC09/Styles/images/btn_hero_back.gif' />")
				.click(function(e) {
				    pdc.heroBanner.paging.moveBy(index, -1);
				});

            $("<div />")
				.addClass("pagnav pagnavnext")
				.appendTo("#herobanner>div:eq(" + index + ")")
				.html("<img src='/Skins/PDC09/Styles/images/btn_hero_next.gif' />")
				.click(function(e) {
				    pdc.heroBanner.paging.moveBy(index, 1);
				});
        },
        "moveBy": function(sectionIndex, amount, autowrap) {
            //auto wrap back to page 0 if past the pageCount
            var goToPage = (pdc.heroBanner.paging.currentPage + amount);
            var direction = (amount > 0) ? "right" : "left";

            // Navigate; if page is outside the bounds of the section, only auto-wrap if specified
            if ((goToPage >= 0 && goToPage < pdc.heroBanner.paging.pages[sectionIndex]) || autowrap) {
                pdc.heroBanner.paging.goTo(sectionIndex, (goToPage == pdc.heroBanner.paging.pages[sectionIndex]) ? 0 : goToPage, direction);
            }
        },
        "locked": false,
        "goTo": function(sectionIndex, pageIndex, direction) {
            if (direction == null) direction = "none";
            if (this.locked) return false;

            // Make sure we're in the bounds of this section
            if (pageIndex >= 0 && pageIndex < pdc.heroBanner.paging.pages[sectionIndex]) {

                // Hide all pages and show the selected page
                //$("#herobanner>div:eq(" + sectionIndex + ")>div:not(.pagnav)").hide();
                $("#herobanner>div:eq(" + sectionIndex + ")>div:not(.pagnav):not(" + pdc.heroBanner.paging.currentPage + ")").hide();

                $currentpage = $("#herobanner>div:eq(" + sectionIndex + ")>div:not(.pagnav):eq(" + pdc.heroBanner.paging.currentPage + ")");
                $nextpage = $("#herobanner>div:eq(" + sectionIndex + ")>div:not(.pagnav):eq(" + pageIndex + ")");

                switch (direction) {
                    case "left":
                        pdc.heroBanner.paging.locked = true;
                        $currentpage.hide("slide", { direction: "right" }, 500);
                        $nextpage.show("slide", { direction: "left" }, 500, function() { pdc.heroBanner.paging.locked = false; });
                        break;
                    case "right":
                        pdc.heroBanner.paging.locked = true;
                        $currentpage.hide("slide", { direction: "left" }, 500);
                        $nextpage.show("slide", { direction: "right" }, 500, function() { pdc.heroBanner.paging.locked = false; });
                        break;
                    default:
                        $currentpage.hide();
                        $nextpage.show();
                }

                // Clear highlighting and highlight selected pagination button
                $("#pagHero" + sectionIndex + ">span").removeClass("pageactive");
                $("#pagHero" + sectionIndex + ">span:eq(" + pageIndex + ")").addClass("pageactive");

                // Keep index up to date
                pdc.heroBanner.paging.currentPage = pageIndex;

                // Keep back/next buttons up to date
                $("div.pagnav").removeClass("paginactive");
                if (pageIndex == 0) $("div.pagnavback").addClass("paginactive");
                if ((pageIndex + 1) == pdc.heroBanner.paging.pages[sectionIndex]) $("div.pagnavnext").addClass("paginactive");
            }
        }
    },
    "autorotate":
    {
        "rotate": function() {
            if (pdc.heroBanner.autorotate.active) {
                pdc.heroBanner.paging.moveBy(pdc.heroBanner.currentSection, 1, true);
                pdc.heroBanner.autorotate.timer = window.setTimeout("pdc.heroBanner.autorotate.rotate()", pdc.heroBanner.autorotate.delay);
            }
        },
        "delay": 5000, //how long to wait
        "timer": null, //global timer
        "active": true //to rotate, or not to rotate
    }
}