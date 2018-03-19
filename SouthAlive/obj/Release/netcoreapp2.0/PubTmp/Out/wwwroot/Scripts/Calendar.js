var monthdata = [];
var index = 0;
var lastmonth;
var id;
var price = 0;

function setPrice(p) {
    price = p;
}

function prev(save) {
    var cal = document.getElementById("calendar");
    if (save) { lastmonth = cal.innerHTML; }
    cal.innerHTML = monthdata[index - 1];
    index--;
}

function reprice() {
    var div = document.getElementById("cost");
    var end = document.getElementById("endTime").value;
    var start = document.getElementById("startTime").value;
    var s = parseInt(start.match(/\d+/));
    if (start.indexOf(":30") != -1) { s += .5 }
    var e = parseInt(end.match(/\d+/));
    if (end.indexOf(":30") != -1) { e += .5 }
    var pr = "$" + ((e - s) * price);
    if (pr.indexOf(".") != -1) { pr = pr + "0"; }
    else { pr = pr + ".00";}
    div.innerHTML = "Cost: " + pr;
}

function nextm() {
    var cal = document.getElementById("calendar");
    if (index == monthdata.length - 1) { cal.innerHTML = lastmonth; }
    else { cal.innerHTML = monthdata[index + 1]; }
    index++;
}
function chooseDate(day, month, year) {
    $.ajax({
        type: "POST",
        url: "/Venue/GetForm",
        data: {day:day, month: month, year: year, id: id },
        success: function (data) {
            document.getElementById("form").innerHTML = data;
            index++;
        },
        dataType: "html",
        traditional: true
    })
}

function setid(tid){
    id = tid;
}

function getmonth(month,year)
{
    if (month < 12) { month++ }
    else { month = 1; year++ }
    var pred = document.getElementById("prem");
    if (pred != null) { pred.setAttribute("onclick", "prev(false)"); }
    var next = document.getElementById("nm");
    next.setAttribute("onclick", "nextm()");
    var cal = document.getElementById("calendar");
    monthdata.push(cal.innerHTML);

    $.ajax({
        type: "POST",
        url: "/Venue/NextMonth",
        data: {month:month,year:year,id:id},
        success: function (data) {
            cal.innerHTML = data;
            index++;
        },
        dataType: "html",
        traditional: true
    })
}