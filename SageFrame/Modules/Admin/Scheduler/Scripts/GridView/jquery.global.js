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

function getFunction(trigger, argus) {

    switch (trigger) {
        case '1':
            editrecord(argus)
            break;
        case '2':
            deleterecord(argus)
            break;
        case '3':
            viewrecord(argus)
            break;
        case '4':
            activerecord(argus)
            break;
        case '5':
            deactiverecord(argus)
            break;

    }
}