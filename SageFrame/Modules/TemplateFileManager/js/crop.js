
function Init() {
    // Patch for IE to force "overflow: auto;"
    document.getElementById("imgContainer").style["position"] = "relative";
}

function showCoords(c) {
    $('#x1').val(c.x);
    $('#y1').val(c.y);
    $('#x2').val(c.x2);
    $('#y2').val(c.y2);
    $('#w').val(c.w);
    $('#h').val(c.h);

};

function ValidateSelected() {
    if (document.getElementById("w").value == "" || document.getElementById("w").value == "0"
        || document.getElementById("h").value == "" || document.getElementById("h").value == "0") {
        alert("No area to crop was selected");
        return false;
    }
}