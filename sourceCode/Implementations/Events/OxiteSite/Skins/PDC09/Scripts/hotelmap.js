/// <reference path="jquery-1.3.2-vsdoc2.js" />

var map = null;
var itemArray = new Array();
function EventMapLoad() {
    var loc = new VELatLong(34.04531, -118.26352);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle1.gif'/></div>");
    shape.SetTitle("<a href='http://www.figueroahotel.com/' target='_blank'>Figueroa Hotel</a>");
    var figueroa = $("div#figueroa").html();    
    shape.SetDescription(figueroa);

    map.AddShape(shape);
    var loc = new VELatLong(34.04991, -118.25438);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle2.gif'/></div>");
    shape.SetTitle("<a href='http://www.hiltoncheckers.com/' target='_blank'>Hilton Checkers</a>");
    var checkers = $("div#checkers").html();
    shape.SetDescription(checkers);
    

    map.AddShape(shape);
    var loc = new VELatLong(34.04485, -118.26460);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle3.gif'/></div>");
    shape.SetTitle("<a href='http://www.hicitycenter.com/' target='_blank'>Holiday Inn City Center</a>");
    var hicc = $("div#hicc").html();
    shape.SetDescription(hicc);

    map.AddShape(shape);
    var loc = new VELatLong(34.05313, -118.24792);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle4.gif'/></div>");
    shape.SetTitle("<a href='http://www.kawadahotel.com/' target='_blank'>Kawada Hotel</a>");
    var kawada = $("div#kawada").html();
    shape.SetDescription(kawada);

    map.AddShape(shape);
    var loc = new VELatLong(34.04630, -118.25487);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle5.gif'/></div>");
    shape.SetTitle("<a href='http://www.laac.com/' target='_blank'>Los Angeles Athletic Club</a>");
    var laac = $("div#laac").html();
    shape.SetDescription(laac);

    map.AddShape(shape);
    var loc = new VELatLong(34.05486, -118.25499);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle6.gif'/></div>");
    shape.SetTitle("<a href='http://www.marriott.com/hotels/travel/laxdt-los-angeles-marriott-downtown/' target='_blank'>Los Angeles Marriott Downtown</a>");
    var marriott = $("div#marriott").html();
    shape.SetDescription(marriott);

    map.AddShape(shape);
    var loc = new VELatLong(34.04947, -118.25383);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle7.gif'/></div>");
    shape.SetTitle("<a href='http://www.millenniumhotels.com/millenniumlosangeles/' target='_blank'>Millennium Biltmore</a>");
    var biltmore = $("div#biltmore").html();
    shape.SetDescription(biltmore);

    map.AddShape(shape);
    var loc = new VELatLong(34.05005, -118.24030);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle8.gif'/></div>");
    shape.SetTitle("<a href='http://www.miyakoinn.com/' target='_blank'>Miyako Hotel</a>");
    var miyako = $("div#miyako").html();
    shape.SetDescription(miyako);

    map.AddShape(shape);
    var loc = new VELatLong(34.05138, -118.24249);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle9.gif'/></div>");
    shape.SetTitle("<a href='http://www.kyotograndhotel.com/' target='_blank'>Kyoto Grand Hotel</a>");
    var kyoto = $("div#kyoto").html();
    shape.SetDescription(kyoto);

    map.AddShape(shape);
    var loc = new VELatLong(34.04715, -118.26029);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle10.gif'/></div>");
    shape.SetTitle("<a href='http://www.ohotelgroup.com/' target='_blank'>O Hotel</a>");
    var ohotel = $("div#ohotel").html();
    shape.SetDescription(ohotel);

    map.AddShape(shape);
    var loc = new VELatLong(34.05267, -118.24954);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle11.gif'/></div>");
    shape.SetTitle("<a href='http://www.omnihotels.com/FindAHotel/LosAngelesCaliforniaPlaza.aspx' target='_blank'>Omni Los Angeles</a>");
    var omni = $("div#omni").html();
    shape.SetDescription(omni);

    map.AddShape(shape);
    var loc = new VELatLong(34.04807, -118.25760);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle12.gif'/></div>");
    shape.SetTitle("<a href='http://www.starwoodhotels.com/sheraton/property/overview/index.html?propertyID=1598' target='_blank'>Sheraton Los Angeles</a>");
    var sheraton = $("div#sheraton").html();
    shape.SetDescription(sheraton);

    map.AddShape(shape);
    var loc = new VELatLong(34.05304, -118.25659);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle13.gif'/></div>");
    shape.SetTitle("<a href='http://www.starwoodhotels.com/westin/property/overview/index.html?propertyID=1004' target='_blank'>Westin Bonaventure</a>");
    var bonaventure = $("div#bonaventure").html();
    shape.SetDescription(bonaventure);

    map.AddShape(shape);
    var loc = new VELatLong(34.05031, -118.25980);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle circle'><img src='http://st1.maps.live.com/i/pins/redcircle14.gif'/></div>");
    shape.SetTitle("<a href='http://www.wilshiregrand.com' target='_blank'>Wilshire Grand</a>");
    var wilshire = $("div#wilshire").html();
    shape.SetDescription(wilshire);
    
    map.AddShape(shape);
    
    var loc = new VELatLong(34.04103, -118.26932);
    var shape = new VEShape(VEShapeType.Pushpin, loc);
    shape.SetCustomIcon("<div class='pinStyle' style='background: url(http://st1.maps.live.com/i/pins/poi_search.gif) no-repeat 0 0; ><div class='pinText'>CC</div></div>");
    shape.SetTitle("<a href='http://www.lacclink.com/' target='_blank'>Los Angeles Convention Center</a>");
    shape.SetDescription("1201 S Figueroa St<br />Los Angeles, CA 90015");

    map.AddShape(shape);
}
function CreateMap() {
    map = new VEMap('EventMap');
    map.onLoadMap = EventMapLoad;
    map.LoadMap(new VELatLong(34.04807, -118.25760), 14, VEMapStyle.Road, false);
}
window.onload = CreateMap;
