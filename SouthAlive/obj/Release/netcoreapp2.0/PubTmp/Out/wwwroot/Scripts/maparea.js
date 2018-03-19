var X1;
var X2;
var Y1;
var Y2;

var aX1;
var aX2;
var aY1;
var aY2;

var click = 1;
var rectangle2;

function initMap() {
    //position where the map will be centered on
    var pos = { lat: -46.426585, lng: 168.373375 };
    map = new google.maps.Map(document.getElementById('map'),
        {
            streetViewControl: false,
            zoom: 15,
            center: pos
        });
    //checks for when the user clicks on the map
    map.addListener('click', function (f) { clicked(f.latLng) });
    var rectangle1 = new google.maps.Rectangle({
        strokeColor: '#FFFF00',
        map:map,
        bounds:{
            north: Y1,
            south: Y2,
            east: X2,
            west: X1
        }
    });
    /*
    rectangle2 = new google.maps.Rectangle({
        strokeColor: '#FFFF00',
        map: map,
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: '#aaaa00',
        fillOpacity: 0.35,
        bounds: {
            north: Y1,
            south: Y2,
            east: X2,
            west: X1
        }
    });*/
}
function pointToDoubles(point) {
    var s = (String(point).substring(1, String(point).length - 1));
    var a = -parseFloat(s.match(/\d+/) + "." + s.substring(s.indexOf(".")).match(/\d+/));
    s = s.substring(s.indexOf(","));
    var b = parseFloat(s.match(/\d+/) + "." + s.substring(s.indexOf(".")).match(/\d+/));
    alert(a+"\n" +b)
    return { lat: a, lng: b };
}

function clicked(point) {
    latlng = pointToDoubles(point)
    if (click == 1) {
        rectangle2.bounds.north = latlng.lat;
        rectangle2.bounds.west = latlng.lng;
        click = 2;
    }
    else {
        rectangle2.bounds.south = latlng.lat;
        rectangle2.bounds.east = latlng.lng;
        click = 1;
    }
}