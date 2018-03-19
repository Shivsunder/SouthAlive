function bold() {document.execCommand("bold", true, null)}
function italics() { document.execCommand("italic", true, null) }
function underline() { document.execCommand("underline", true, null) }
function bulletpoint() { document.execCommand("insertOrderedList", true, null) }


function textcolor() {
    var color = document.getElementById("forecolor").value;
    document.execCommand("forecolor", true, color)
}
function highlight() {
    var color = document.getElementById("backcolor").value;
    document.execCommand("backcolor", true, color)
}
