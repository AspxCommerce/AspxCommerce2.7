// Define Global Varaiblse

// ID =#
// Class =.

var deletBtn = '.delete';
var editBtn = '.edit';
var addBtn = '.add';

$(function() {

    $(deletBtn).click(function() {
        var chkids;
        $('.chkbox').each(function() {
            if ($(this).is(':checked')) {
                chkids = chkids + $(this).attr('value');
            }
        })
    })


});

function GlobalClearGrid(gid) {
    $('#' + gid).find('thead').remove();
    $('#' + gid).find('tbody').remove();
    $('#' + gid).css('display', 'none');
    $('#' + gid + '_Pagination').remove();
}

function getFunction(tblID, trigger, method, argus) {
    switch (trigger) {
        case '1':
            eval(method)(tblID, argus);
            break;
        case '2':
            eval(method)(tblID, argus);
            break;
        case '3':
            eval(method)(tblID, argus);
            break;
        case '4':
            eval(method)(tblID, argus);
            break;
        case '5':
            eval(method)(tblID, argus);
            break;

    }
}

function getPopUpFunction(method, argus, popUpId) {
    eval(method)(argus, popUpId);
}

function getDownloadFunction(method, argus) {
    eval(method)(argus);
}

//function trim(value) {
//    var temp = value;
//    var obj = /^(\s*)([\W\w]*)(\b\s*$)/;
//    if (obj.test(temp)) { temp = temp.replace(obj, '$2'); }
//    var obj = / +/g;
//    temp = temp.replace(obj, " ");
//    if (temp == " ") { temp = ""; }
//    return temp;
//};

Date.prototype.toMSJSON = function() {
    var date = '/Date(' + this.getTime() + ')/';
    return date;
};