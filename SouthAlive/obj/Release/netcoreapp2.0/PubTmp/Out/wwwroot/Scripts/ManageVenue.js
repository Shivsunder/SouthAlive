var index = 1;
var days = [{ id: 0, name: "Sunday" }, { id: 1, name: "Monday" }, { id: 2, name: "Tuesday" }, { id: 3, name: "Wednesday" }, { id: 4, name: "Thursday" }, { id: 5, name: "Friday" }, { id: 6, name: "Saturday" }];

function change() {
    var type = document.getElementById("timetype");
    var txt = type.options[type.selectedIndex].text;
    if (txt == "Custom") {
        document.getElementById("predefined").style.display = "none";
        document.getElementById("custom").style.display = "block";
    }
    else {
        document.getElementById("custom").style.display = "none";
        document.getElementById("predefined").style.display = "block";
    }
}
function updatemin(a) {
    var t = document.getElementById("open"+a).value;
    document.getElementById("close"+a).min = t;
}
function updatemax(a) {
    var t = document.getElementById("close"+a).value;
    document.getElementById("open"+a).max = t;
}
function todate(time) {
    var hour = parseInt(time.match(/\d+/));
    var min = time.substring(time.indexOf(":")).match(/\d+/);
    if (time.indexOf("pm") != -1 || time.indexOf("PM") != -1) { if (hour != 12) { hour += 12 }; }
    return hour+":"+min;
}
function send() {
    var name = document.getElementById("Name").value;
    var description = document.getElementById("Description").value;
    var address = document.getElementById("Address").value;
    var type = document.getElementById("timetype");
    var price = document.getElementById("Price");
    var timetype = type.value;
    var t = type.options[type.selectedIndex].text;
    var opening = [];
    var closing = [];
    var day = []

    if (t == "Custom") {
        for (var i = 0; i < index; i++) {
            day.push(document.getElementById("sday" + i).value);
            opening.push(todate(document.getElementById("open"+i).value));
            closing.push(todate(document.getElementById("close" + i).value));
        }
    }
    else {
        day.push(-1);
        opening.push(todate(document.getElementById("open").value));
        closing.push(todate(document.getElementById("close").value));
    }

    var postdata = { name: name, description: description, address: address, timetype: timetype, price: price, opening: opening, closing: closing, day: day };

    //alert(postdata.day[0] + ": " + postdata.opening[0] + " - " + postdata.closing[0]);

    $.ajax({
        type: "POST",
        url: "/Venue/AddVenue",
        data: postdata,
        success: function (re) {
            window.location = "/Venue";
        },
        dataType: "json",
        traditional: true
    })
}

function changeday(id) {
    document.getElementById("disday" + id).innerHTML = days[document.getElementById("sday" + id).value].name;
}

function addDay() {
    if (index < 7) {
        var got = days.slice();
        for (var i = 0; i < index; i++)
        {
            var day = document.getElementById("sday" + i).value;
            tmp = days[day];
            got.splice(got.indexOf(tmp),1)
        }
        var day = got[0];
        var div = document.getElementById("customdiv");

        div.appendChild(document.createElement("hr"));

        a = document.createElement("a");
        a.setAttribute("href", "#day" + index)
        a.setAttribute("data-toggle", "collapse")
        a.setAttribute("data-parent", "#customdiv")
        a.innerHTML = '<h4 id="disday'+index+'">'+day.name+"</h4>";
        div.appendChild(a);

        var newday = document.createElement("div");
        newday.id = "day" + index;
        newday.setAttribute("class", "collapse in")
        var html = '<h5>Day</h5>'
        html += '<select class="form-control" id = "sday' + index + '" onchange="changeday(' + index + ')">';
        days.forEach(function(d) {
            if (d == day) { html += '<option value="' + d.id + '" selected = "selected">' + d.name + '</option>';}
            else { html += '<option value="' + d.id + '">' + d.name + '</option>';}
        }
        )
        html += '</select > <br><br>'
        html += '<h5>Opening Time</h5>'
        html += '<input type="time" id="open' + index+'" onchange="updatemin(' + index + ')" /><br><br>'
        html += '<h5>Closing Time</h5>'
        html += '<input type="time" id="close' + index+'" onchange="updatemax(' + index + ')" /><br><br />'
        newday.innerHTML = html;
        div.appendChild(newday);
        index++;
    }
}