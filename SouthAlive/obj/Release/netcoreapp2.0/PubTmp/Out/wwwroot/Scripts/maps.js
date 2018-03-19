/*
    IMPORTANT NOTE:
    All alerts should be replaced later to make the site look better and function better

    Fuctions to be called from html:
        initMap()               called from callback when initialising the map      Requires an element with an id of 'map'
        toggleContinuous()      called from the continuous button                   Requires element id to be called 'Continuous'
        reset()                 called from reset button
        undo()                  called from undo button

    all other functions are called from within this javascript file

*/

//these global variables are used throughout the script
var markers = [];
var polylines = [];
var continuous = true;
var map = null;
var popup;
var dd = [];
var Registered = false;
var User = "";
var X1;
var X2;
var Y1;
var Y2;
//please change this key when you are testing, thank you
var APIKEY = "AIzaSyCxWZ7IyrkpV6g-5UQ90gnQDa_cu3v8Dac"

/**
    function to initialise the map
**/
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
    if (Registered) { map.addListener('click', function (f) { clicked(f.latLng) }); }
    var direction = new google.maps.DirectionsService();
    var direction = new google.maps.DirectionsService();
    direction.route(
        {
            origin: { lat: 0, lng: 0 },
            destination: { lat: 0, lng: 0 },
            travelMode: google.maps.TravelMode.WALKING
        },
        function () { initpoly(); })
}

function addpoly(poly, user, userpoly, id) {
    dd.push({ poly: poly, name: user, isLoggedUser: userpoly, id:id });
}

function initpoly() {
    for (var i = 0; i < dd.length; i++) {
        var col = "#DDDD00";
        var op = false
        if (dd[i].isLoggedUser) { col = "#0000FF"; op = true }
        var z = addALine(dd[i].poly, col, i, op )
                z.addListener("click", function (event,index=this.x) {
                    if (popup != null) { popup.close(map); }
                    var extension = "";
                    if (dd[index].isLoggedUser) { extension = '<a href = "/ZeroRubbish/Delete?id=' + dd[index].id + '">Delete</a>'; }
                    popup = new google.maps.InfoWindow({
                        content: '<strong>' + dd[index].name + '</strong> has selected to clean this steet. ' + extension,
                        position: event.latLng
                    })
                    popup.open(map, z)
                })
            }
}
function save() {
    var namein = document.getElementById("Name");
    var phonein = document.getElementById("PhoneNumber");
    var name = ""
    var phone = ""
    if (namein != null) {
        name = namein.value;

        phone = phonein.value;
    }
    if (namein == null || (name != "" && phone != "")) {
    var list = [];
    for (var i = 0; i < polylines.length; i++) {
        if (polylines[i].map == map) { list.push(polylines[i].encodedData); }
    }
    
        var postdata = { lines: list, name: name, phone: phone };
        $.ajax({
            type: "POST",
            url: "ZeroRubbish/add",
            data: postdata,
            success: function (re) {
                window.location = "/ZeroRubbish";
            },
            dataType: "json",
            traditional: true
        })
    }
    else { alert("Please enter your details.") }
}


function addALine(data, color, display, loggedin) {
    var decodedSets = new google.maps.geometry.encoding.decodePath(data);
    var op = 0.6;
    if (loggedin){op=0.7}
    if (display==-1){op=0.9}
    var z = new google.maps.Polyline({
        path: decodedSets,
        geodesic: true,
        strokeColor: color,
        strokeOpacity: op,
        strokeWeight: 6,
        encodedData: data,
        x: display
    })

    z.setMap(map);

    return z;
}

/**
    this function is just a simple way of converting Points which cannot be used into strings which can be used
**/
function pointToString(point) {
    return (String(point).substring(1, String(point).length - 1));
}

/**
    this runs whenever the user clicks on the map
**/
function clicked(pos) {
    //If an infoWindow is left open, and then the undo command is used, the infoWindow may re-appear, so it is closed upon any click on the map.
    if (popup != null) { popup.close(map); }
    //gets the point at where the user clicks and turns it into a useable variable
    var strPoint = pointToString(pos);
    $.get("https://roads.googleapis.com/v1/nearestRoads?points=" + strPoint + "&key=" + APIKEY,
        function (point) { addSnappedPoint(point) });
}

/**
    This function places the snapped marker on the map
**/
function addSnappedPoint(point) {
    if (point.snappedPoints != undefined) {
        if (point.snappedPoints[0].location.latitude > Y1 && point.snappedPoints[0].location.longitude > X1 && point.snappedPoints[0].location.latitude < Y2 && point.snappedPoints[0].location.longitude < X2) {
            var pos = { lat: point.snappedPoints[0].location.latitude, lng: point.snappedPoints[0].location.longitude };
            var marker = new google.maps.Marker({
                position: pos,
                map: map,
                starting: true //this variable is not required by google maps API, it is a nice way to track where polylines begin
            });
            if (markers.length > 0) {
                markers[markers.length - 1].setAnimation(null)
                if (markers[markers.length - 1].starting || continuous) {
                    marker.starting = false
                }
            }
            //markers are animated to be a visual cue when selecting a street
            //a marker bouncing signals that the next marker will connect to the bouncing marker
            if (marker.starting || continuous) { marker.setAnimation(google.maps.Animation.BOUNCE) }

            //markers are added to an array so they can be removed if the user wishes
            markers.push(marker);
            if (!marker.starting) {
                var points = [pointToString(markers[markers.length - 1].position)];
                var i = markers.length - 1;
                while (!markers[i].starting) {
                    i--;
                    points.push(pointToString(markers[i].position));
                }
                //google maps allows a maximum of 23 waypoints, so a round number of 20 points can be selected for a single route
                if (points.length < 21) {
                    addLine(points);
                }
                else {
                    alert("You cannot select anymore points for this route (maximum 20), please start a new route by turning continuous off.");
                    //the marker is removed so that lone markers aren't on the map
                    marker.setMap(null);
                    markers.pop();
                }
            }
        }
        else {

        }
    }
    else {
        //Google maps returns an undefined value when the point sent away is not close enough to a road
        alert("The point you tried selecting was not close enough to a road, please try again.");
    }
}

/**
    This function adds a polyline that follows roads to the map
**/
function addLine(points) {
    var direction = new google.maps.DirectionsService();
    var wayPts = [];
    for (var i = 1; i < points.length - 1; i++) {
        wayPts.push({
            location: points[i],
            stopover: true
        });
    }
    direction.route(
        {
            origin: points[0],
            waypoints: wayPts,
            destination: points[points.length - 1],
            //the walking travelmode ignores the direction of the street, it also allows polylines to go down walkways
            travelMode: google.maps.TravelMode.WALKING
        },
        function (endRoute, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                var z = addALine(endRoute.routes[0].overview_polyline, "#0000FF", -1)
                if (!markers[markers.length - 2].starting) { polylines[polylines.length - 1].setMap(null) }
                polylines.push(z);
                //Checks when a polyline is clicked, when it is clicked, it opens an info window telling the user that they are cleaning that street
                z.addListener("click", function (event) {
                    if (popup != null) { popup.close(map); }
                    popup = new google.maps.InfoWindow({
                        content: '<strong>You</strong> have selected this steet.',
                        position: event.latLng
                    })
                    popup.open(map, z)
                })
            }
        }
    );
}

/**
    A function to undo the last click to the map 
**/
function undo() {
    if (markers.length > 0) {
        //polylines created by the last marker need to be removed
        if (!markers[markers.length - 1].starting) {
            polylines[polylines.length - 1].setMap(null);
            polylines.pop();
        }
        markers[markers.length - 1].setMap(null);
        markers.pop();
        //can possibly set a marker to be animated if necessary
        if (markers.length > 0) {
            if (markers[markers.length - 1].starting || continuous) {
                markers[markers.length - 1].setAnimation(google.maps.Animation.BOUNCE);
            }
            //old polylines may need to be re-shown
            if (!markers[markers.length - 1].starting) { polylines[polylines.length - 1].setMap(map); }
        }
    }
}

/**
    This function resets all user input when the user clicks the reset button
**/
function reset() {
    var a = markers.length;
    //clears all markers
    for (var i = 0; i < a; i++) {
        markers[markers.length - 1].setMap(null);
        markers.pop();
    }
    a = polylines.length;
    //clears all polylines from the user
    for (var i = 0; i < a; i++) {
        polylines[polylines.length - 1].setMap(null);
        polylines.pop();
    }
}

/**
    This toggle whether continuous selection is on or off
**/
function toggleContinuous() {
    continuous = !continuous;
    //if continuous is turned on, markers should be animated to show which marker will be continued off
    if (continuous) {
        if (markers.length > 0) { markers[markers.length - 1].setAnimation(google.maps.Animation.BOUNCE) }
        document.getElementById('Continuous').innerText = "Continuous: on";
    }
    //if continuous is turned off, markers should no longer be animated
    else {
        if (markers.length > 0) {
            if (!markers[markers.length - 1].starting) {
                markers[markers.length - 1].setAnimation(null)
            }
        }
        document.getElementById('Continuous').innerText = "Continuous: off";
    }
}