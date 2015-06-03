/*
*	jquery.form-validation-and-hints.js
*	2009-2012, keikendo.com 
*	Licensed under GPL license (http://www.opensource.org/licenses/gpl-license.php).
*	@version 0.203
*
* http://www.keikendo.com/form-validation-and-hints/
*  
*/


/* --- TO SET --- */
// Prefix used to hook CSS classes to the script 
classprefix = 'verify'; // <input class="verifyInteger" type="text" name="mail" />

// Set your validation rules 
function isTypeValidExt(classprefix, type, value) {
    /* RULE EXAMPLE (Accept only integer values)
    if( type == classprefix + 'Integer' ) {
    return ( ( value.match(/^[\d|,|\.|\s]*$/) ) && ( value != '' ) );
    } 
    */
    return true;
}

var annoy = true;
function debug(msg) { if (annoy) { annoy = confirm(msg); } }




/* --- DOCUMENT READY --- */
$(document).ready(function () {
    mustCheck = true;

    $("." + classprefix + "Cancel").click(function (event) {
        mustCheck = false;
    });

    // HINTS: Add *title hints to form elements
    for (var i = 0; i < document.forms.length; i++) {
        var fe = document.forms[i].elements;
        for (var j = 0; j < fe.length; j++) {
            if ((fe[j]).title.indexOf("**") == 0) {
                if ((fe[j]).value == "" || (fe[j]).value == titleHint) {
                    var titleHint = (fe[j]).title.substring(2);
                    (fe[j]).value = titleHint;
                }
            } else if (((fe[j]).type == "text" || (fe[j]).type == "password" || (fe[j]).tagName == 'TEXTAREA') && (fe[j]).title.indexOf("*") == 0) {
                addHint((fe[j]));
                $(fe[j]).blur(function (event) { addHint(this); });
                $(fe[j]).focus(function (event) { removeHint(this); });
            }
        }
    }

    // VALIDATION:
    $("FORM").submit(function (event) {
        if (mustCheck) {
            // Prevent submit if validation fails
            if (!checkForm(this)) {
                event.preventDefault();
            }
        } else {
            mustCheck = !mustCheck;
        }
    });
}); // end jQuery $(document).ready



/* --- FUNCTIONS --- */
function addHint(field) {
    var titleHint = field.title.substring(1);
    if (field.value == "" || field.value == titleHint) {
        //in "password" inputs, set to "text" to show hint, preserve type in class attribute
        if (field.type == "password") {
            $(field).addClass("password");
            var newObject = changeInputType(field, "text");//returns false for non-ie
            if (document.all) { // if IE
                field = newObject;
            }
        } //end type == "password"
        $(field).addClass("hint");
        field.value = titleHint;
    } //end value==""
} //end addHint


function removeHint(field) { //only on INPUT.text items 
    var titleHint = field.title.substring(1);
    if (field.value == "" || field.value == titleHint) {
        $(field).removeClass('hint');
        field.value = "";
        //re-set password type if appropiate
        if ($(field).hasClass("password")) {
            var newObject = changeInputType(field, "password"); //returns false for non-ie
            if (newObject) { ///IE, element was replaced: reset focus
                $(newObject).focus();
                $(newObject).select();
            }
        } //end hasClass("password")
    } //end value == titleHint
    //}//end what.title	
} //end rmhint

function checkleapyear(date) {
    var check = false;
    //2012/04/17
    var adata = date.split('/');
    var mm = parseInt(adata[1], 10);
    var dd = parseInt(adata[2], 10);
    var yyyy = parseInt(adata[0], 10);
    var xdata = new Date(yyyy, mm - 1, dd);
    if ((xdata.getFullYear() == yyyy) && (xdata.getMonth() == mm - 1) && (xdata.getDate() == dd)) {
        check = true;
    }
    else {
        check = false;
    }

    return check;
}

function isValidDate(date) {

    if (date != "") {
        if (date.match(/^(?:(19|20)[0-9]{2}[\- \/.](0[1-9]|1[012])[\- \/.](0[1-9]|[12][0-9]|3[01]))$/)) {

            return checkleapyear(date);
        }
        else {
            return false;
        }
    }
    else {
        // return true;
        return false;
    }
    //    var valid = true;
    //    date = date.split('/');

    //    var year = parseInt(date[0]);
    //    var month = parseInt(date[1], 10);
    //    var day = parseInt(date[2], 10);

    //    if ((month < 1) || (month > 12)) valid = false;
    //    else if ((day < 1) || (day > 31)) valid = false;
    //    else if (((month == 4) || (month == 6) || (month == 9) || (month == 7) || (month == 11)) && (day > 30)) valid = false;
    //    else if ((month == 1) && (((year % 400) == 0) || ((year % 4) == 0)) && ((year % 100) != 0) && (day >= 29)) valid = false;
    //    else if ((month == 1) && ((year % 100) == 0) || (day > 28)) valid = false;
    //    return valid;
}

function changeInputType(oldObject, oType) {
    //based on http://arjansnaterse.nl/changing-type-attribute-in-ie
    //used to simulate change of INPUT type in IE
    if (!document.all) {
        oldObject.type = oType;
        return false;
    } else {
        //ie can't change INPUT's type, must create new element
        var newObject = document.createElement('input');
        newObject.type = oType;
        if (oldObject.size) { newObject.size = oldObject.size; }
        if (oldObject.title){ newObject.title = oldObject.title;}
        if (oldObject.value){ newObject.value = oldObject.value;}
        if (oldObject.name){ newObject.name = oldObject.name;}
        if (oldObject.id){ newObject.id = oldObject.id;}
        if (oldObject.className){ newObject.className = oldObject.className;}
        oldObject.parentNode.replaceChild(newObject, oldObject);
        //live()
        //newObject.blur = oldObject.blur;
        //newObject.focus = oldObject.focus;
        //$(newObject).blur(function(event){ addHint(this); });
        //$(newObject).focus(function(event){ removeHint(this); });
        return newObject;
    } //end document.all
}

function checkForm(form) {
    var send = true;
    var password = '';
    radioGroups = Array();
    checkboxGroups = Array();
    $('.required').find('.cke_skin_v2 iframe').each(function () {
        if ($(this).contents().find("body").text() != '' && $(this).contents().find("body").text() != null) {
            $(this).parents('.required').find('.iferror').html('');
            $(this).parents().removeClass("diverror");
        }
        else {

            $(this).parents('.required').find('.iferror').html(getLocale(CoreJsLanguage,"Mandatory Fields"));
            $(this).parents('table').parent('span').parent('span').parent('span').parent('div').addClass("diverror");
            $(this).parents('.required').find('.iferror').show();
            send = false;
        }
    });

    //for category managemnet
    if ($('.activeto').length != 0) {
        if (isValidDate($('.activefrom').val())) {
            if (isValidDate($('.activeto').val())) {
                if (Date.parse($('.activefrom').val()) > Date.parse($('.activeto').val())) {
                    if (send){ moveTo($('.activeto'));}
                    showErrorOn($('.activeto'));
                    $('.activeto').parents('.required').find('.iferror').html(getLocale(CoreJsLanguage, 'Active To date must be higher date than Active From date!'));
                    send = false;
                    //$(To[vt]).css("background-color","#FFCA85");   
                }
            }
            else {
                showErrorOn($('.activeto'));
                $('.activeto').parents('.required').find('.iferror').html(getLocale(CoreJsLanguage,'You have entered invalid Active To date!'));
                send = false;
            }
        }
        else {
            showErrorOn($('.activefrom'));
            $('.activefrom').parents('.required').find('.iferror').html(getLocale(CoreJsLanguage,'You have entered invalid Active From date!'));
            send = false;
        }
    }

    //for special item
    if ($('.classSpecialTo').length != 0) {
        if ($('.SpecialDropDown').val() == 5) {
            if (isValidDate($('.classSpecialFrom').val())) {
                if (isValidDate($('.classSpecialTo').val())) {
                    if (Date.parse($('.classSpecialFrom').val()) > Date.parse($('.classSpecialTo').val())) {
                        if (send){ moveTo($('.classSpecialTo'));}
                        showErrorOn($('.classSpecialTo'));
                        showErrorOn($('.classSpecialFrom'));
                        //    $('.classSpecialTo').parents('.required').find('.iferror').html('Special To date must be higher date than Special From date!');
                        $('.classSpecialTo').next('span').html('Special To date must be higher date than Special From date!');
                        send = false;
                        //$(To[vt]).css("background-color","#FFCA85");   
                    }
                } else {
                    showErrorOn($('.classSpecialTo'));
                    //  $('.classSpecialTo').parents('.required').find('.iferror').html('You have entered invalid Active To date!');
                    $('.classSpecialTo').next('span').html('You have entered invalid Special To date!');
                    send = false;
                }
            } else {
                showErrorOn($('.classSpecialFrom'));
                //  $('.classSpecialFrom').parents('.required').find('.iferror').html('You have entered invalid Active From date!');
                $('.classSpecialFrom').next('span').html('You have entered invalid Special From date!');
                send = false;
            }
        } else {
            send = true;
        }
    }

    //for special Price
    if ($('.classItemSpecialPrice').val() != '') {
        if ($('.classSpecialPriceTo').length != 0) {
            if (isValidDate($('.classSpecialPriceFrom').val())) {
                if (isValidDate($('.classSpecialPriceTo').val())) {
                    if (Date.parse($('.classSpecialPriceFrom').val()) > Date.parse($('.classSpecialPriceTo').val())) {
                        if (send){ moveTo($('.classSpecialPriceTo'));}
                        showErrorOn($('.classSpecialPriceTo'));
                        showErrorOn($('.classSpecialPriceFrom'));
                        $('.classSpecialPriceTo').next('span').html('Special Price To date must be higher date than Special Price From date!');
                        send = false;
                        return false;
                    }
                } else {
                    showErrorOn($('.classSpecialPriceTo'));
                    $('.classSpecialPriceTo').next('span').html('You have entered invalid Special To date!');
                    send = false;
                    return false;
                }
            } else {
                showErrorOn($('.classSpecialPriceFrom'));
                $('.classSpecialPriceFrom').next('span').html('You have entered invalid Special From date!');
                send = false;
                return false;
            }
        }
    }

    //for feature item
    if ($('.classFeaturedTo').length != 0) {
        if ($('.FeaturedDropDown').val() == 3) {
            if (isValidDate($('.classFeaturedFrom').val())) {
                if (isValidDate($('.classFeaturedTo').val())) {
                    if (Date.parse($('.classFeaturedFrom').val()) > Date.parse($('.classFeaturedTo').val())) {
                        if (send){ moveTo($('.classFeaturedTo'));}
                        showErrorOn($('.classFeaturedTo'));
                        showErrorOn($('.classFeaturedFrom'));
                        $('.classFeaturedTo').next('span').html('Featured To date must be higher date than Featured From date!');
                        send = false;
                        //$(To[vt]).css("background-color","#FFCA85");   
                    }
                } else {
                    showErrorOn($('.classFeaturedTo'));
                    $('.classFeaturedTo').next('span').html('You have entered invalid Featured To date!');
                    send = false;
                }
            } else {
                showErrorOn($('.classFeaturedFrom'));
                $('.classFeaturedFrom').next('span').html('You have entered invalid Featured From date!');
                send = false;
            }
        } else {
            send = true;
        }
    }
    //for ActiveFrom and ActiveTo date item
    if ($('.classActiveTo').length != 0) {      
        if (isValidDate($('.classActiveFrom').val())) {
            if (isValidDate($('.classActiveTo').val())) {
                if (Date.parse($('.classActiveFrom').val()) > Date.parse($('.classActiveTo').val())) {
                    if (send){ moveTo($('.classActiveTo'));}
                    showErrorOn($('.classActiveTo'));
                    showErrorOn($('.classActiveFrom'));
                    $('.classActiveTo').next('span').html(getLocale(CoreJsLanguage, "Active To date must be higher date than Active From date!"));
                    send = false;
                    //$(To[vt]).css("background-color","#FFCA85");   
                }
            }
            else {
                showErrorOn($('.classActiveTo'));
                //  $('.classSpecialTo').parents('.required').find('.iferror').html('You have entered invalid Active To date!');
                $('.classActiveTo').next('span').html(getLocale(CoreJsLanguage,"You have entered invalid Active To date!"));
                send = false;
            }
        }
        else {
            showErrorOn($('.classActiveFrom  '));
            $('.classActiveFrom ').next('span').html(getLocale(CoreJsLanguage,"You have entered invalid Active From date!"));
            send = false;
        }
    }

   // for NewFrom and NewTo date item
    if ($('.classNewTo').length != 0) {
        if ($.trim($('.classNewFrom').val()) != '') {
            if (isValidDate($('.classNewFrom').val())) {
                if (isValidDate($('.classNewTo').val())) {
                    if (Date.parse($('.classNewFrom').val()) > Date.parse($('.classNewTo').val())) {
                        if (send){ moveTo($('.classNewTo'));}
                        showErrorOn($('.classNewTo'));
                        showErrorOn($('.classNewFrom'));
                        $('.classNewTo').next('span').html('New To date must be higher date than From date!');
                        send = false;
                        //$(To[vt]).css("background-color","#FFCA85");   
                    }
                }
                else {
                    showErrorOn($('.classNewTo'));
                    $('.classNewTo').next('span').html('You have entered invalid New To date!');
                    send = false;
                }
            }
            else {
                showErrorOn($('.classNewFrom'));
                $('.classNewFrom').next('span').html('You have entered invalid New From date!');
                send = false;
            }
        }
    }
    $(form).removeClass("haserrors");

    //inputs = $(form).find('INPUT[class*="' + classprefix + '"]');	
    inputs = $(form).find('INPUT[class*="' + classprefix + '"], INPUT:not(:file), TEXTAREA, .required SELECT').filter(':not(:disabled)').not('.class-text');

    $.each(inputs, function (i, val) {
        input = $(val);
        //        if (input.attr('offsetWidth') != 0) {
        switch (input.attr('type')) {
            case 'select-one':
                if (input.get(0)[input.attr('selectedIndex')].text == '') {
                    if (send){ moveTo(input);}
                    showErrorOn(input);
                    send = false;
                }
                break;

            case 'radio':
                if (radioGroups != undefined) {
                    if (window.radioGroups[input.prop('name')] === undefined) { radioGroups[input.prop('name')] = new Array(); }
                    radioGroups[input.prop('name')][radioGroups[input.prop('name')].length] = input;
                }
                break;

            //                case 'checkbox':     
            //                    if (!input.prop('checked')) {     
            //                        if (send) moveTo(input);     
            //                        showErrorOn(input);     
            //                        send = false;     
            //                    }     
            //                    break;     

            case 'file':
                if (!isFilled(input)) {
                    if (send){ moveTo(input);}
                    showErrorOn(input);
                    send = false;
                }
                break;

            case 'password':
                if (input.hasClass(classprefix + 'PasswordConfirm')) {
                    if (input.val() != password) {
                        if (send){ moveTo(input);}
                        showErrorOn(input);
                        send = false;
                    }
                } else {
                    password = input.val();
                }
                break;
            case 'textarea':
                if ((isFilled(input) || isRequired(input)) && (!isValid(input))) {
                    if (send){ moveTo(input);}
                    showErrorOn(input);
                    send = false;
                }
                break;
            case 'text':
                if ((isFilled(input) || isRequired(input)) && (!isValid(input))) {
                    if (send){ moveTo(input);}
                    showErrorOn(input);
                    send = false;
                }
                break;

            default:
                break;
        }
    });
    if (radioGroups.length > 0) {
        for (var i in radioGroups) {
            for (var j in radioGroups[i]) {
                if (radioGroups[i][j].prop('checked')) {
                    if (radioGroups[i][j].val() !== '') {
                        rmErrorClass(radioGroups[i][j]);
                    } else {
                        showErrorOn(radioGroups[i][j]);
                        send = false;
                    }
                    break;
                }
            }
            if (!radioGroups[i][j].prop('checked')) {
                for (var j in radioGroups[i]) {
                    if (send && j == 0) { moveTo(radioGroups[i][j]); }
                    showErrorOn(radioGroups[i][j]);
                }
                send = false;
            }
        }
    }
    if (checkboxGroups.length > 0) {

        for (var i in checkboxGroups) {
            for (var j in checkboxGroups[i]) {
                if (checkboxGroups[i][j].prop('checked')) {
                    rmErrorClass(radioGroups[i][j]);
                    break;
                }
            }
            if (!checkboxGroups[i][j].prop('checked')) {
                for (var j in checkboxGroups[i]) {
                    if (send && j == 0) { moveTo(checkboxGroups[i][j]); }
                    showErrorOn(checkboxGroups[i][j]);
                }
                send = false;
            }
        } 
    }

    // Add class haserrors to each row that has at least one field with errors.
    var rows = $(form).find('DIV.row');
    $.each(rows, function (i, val) {
        var row = $(val);
        rowHasErrors(row);
    });

    return send;
}

function rowHasErrors(row) {
    var haserrors = $(row).find('.error');
    if (haserrors.length > 0) {
        $(row).addClass('haserrors');
        return true;
    }
    $(row).removeClass('haserrors');
    return false;
}

function isRequired(input) {
    return input.parents(".required").length != 0;
}

function isFilled(input) {
    hintText = '';
    if (typeof (input.attr('title')) !== 'undefined') {
        //clear HINTs before validation
        if (input.attr('title').indexOf("**") == 0) {
            var hintText = input.attr('title').substring(2);
        } else if (input.attr('title').indexOf("*") == 0) {
            var hintText = input.attr('title').substring(1);
        } //end clear hints
        return input.val() != hintText && input.val() != '';
    }
    return input.val();
}

function isValid(input) {
    if (!isFilled(input)) { return false; }
    string = input.prop('class');
    value = input.val();
    start = string.indexOf(classprefix);
    type = '';
    result = true;
    while (result) {
        if (
			start == -1 ||
			string.charAt((start + classprefix.length)) == ' ' ||
			string.charAt((start + classprefix.length)) != string.charAt((start + classprefix.length)).toUpperCase()
		) {
            break;
        } else {
            for (i = start; i < string.length; i++) {
                if (string.charAt(i) == ' ') {
                    break;
                }
                type += string.charAt(i);
            }            
            if (!isTypeValid(input, type, value)) {
                result = false;
                break;
            }
            start = string.indexOf(classprefix, start + 1);
        }
    }
    return result;
}

function isTypeValid(input, type, value) {
    
    if (type == classprefix + 'Text') {
        return true;
    }

    if (type == classprefix + 'Integer') {
        return ((value.match(/^(\+|-)?\d+$/)) && (value != ''));
        //return ((value.match(/^[\d|,|\.|\s]*$/)) && (value != ''));
    }

    if (type == classprefix + 'Url') {
        return (value.match(/^(https?:\/\/)?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?(([0-9]{1,3}\.){3}[0-9]{1,3}|([0-9a-z_!~*'()-]+\.)*([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\.[a-z]{2,6})(:[0-9]{1,4})?((\/?)|(\/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+\/?)$/));
    }

    if (type == classprefix + 'MultipleWords') {
        return value.match(/^.*[^^]\s[^$].*$/);
    }

    if (type == classprefix + 'Email') {
        if (value.indexOf("@example.com") > -1) { return false; }
        var emailFilter = /^.+@.+\..{2,}$/;
        var illegalChars = /[\(\)\<\>\,\;\:\\\/\"\[\]]/;
        if (!(emailFilter.test(value)) || value.match(illegalChars)) { return (false); } else { return (true); }
        return false;
    }

    if (type == classprefix + 'AlphabetsOnly') {
        if (value.match(/^[a-zA-Z]+$/)) {
            return true;
        }
        else {
            return false;
        }
    }

    if (type == classprefix + 'AlphaNumeric') {
        if (value.match(/^[a-zA-Z0-9]*/)) {
            return true;
        }
        else {
            return false;
        }
    }

    if (type == classprefix + 'Decimal') {
        if (value.match(/^[0-9]*(\.[0-9]+)?$/)) {//^\$?[0-9][0-9]{0,30}(,[0-9]{3})*(\.[0-9]{0,2})?$/
            return true;
        } else {
            return false;
        }
    }

    if (type == classprefix + 'Price') {
        ///^\$?[1-9][0-9]{0,10}(,[0-9]{3})*(\.[0-9]{0,2})?$/        
        var reg = /[1-9]+(?:\.[0-9]+)?/;
        return reg.test(value);
        //        if (value.match(/^\$?[0-9]{0,10}(,[0-9]{3})*(\.[0-9]{0,2})?$/)) {
        //            if (value.match(/\.[1-9]$/)) {
        //                value += "0";
        //            }
        //            else if (value.match(/\.$/)) {
        //                value += "00";
        //            }
        //            else if (!value.match(/\.[0-9]{2}$/)) {
        //                value += ".00";
        //            }
        //            return true;
        //        }
        //        else {
        //            return false;
        //        }
    }

    if (typeof isTypeValidExt == 'function') {
        fr = isTypeValidExt(classprefix, type, value);
        if (isTypeValidExt(classprefix, type, value) === false) {
            return false;
        } else {
            return true;
        }
    }
    return true;
}

function moveTo(input) {
    var targetOffset = input.offset().top - 40;
    $('html,body').animate({ scrollTop: targetOffset }, 200);
    if (!$.browser.msie) {
        input.get(0).focus();
    }
}

function showErrorOn(input) {
    /* BUG FIXED FOR IE: when submited it auto focuses to the first required field, so the hint and red box aren't there. Might be confusing to a user!

    input.bind('focus.rmErrorClass', function(){
    rmErrorClass( this );
    });
    */
    input.bind('mousedown.rmErrorClass', function () {
        rmErrorClass(this);
    });
    input.bind('keydown.rmErrorClass', function () {
        rmErrorClass(this);
    });
    input.bind('change.rmErrorClass', function () {
        rmErrorClass(this);
    });
    input.bind('focus.rmErrorClass', function () {
        rmErrorClass(this);
    });
    input.bind('blur.rmErrorClass', function () {
        rmErrorClass(this);
    });
    input.addClass("error");
    //input.parents(".required, .field, TR").addClass("error");
    //input.parents(".field").addClass("error");
    input.parents(".text").addClass("error");
    input.parents(".field").addClass("diverror");
    //var index = input.parents(".field").parent('DIV').html();
    //input.parents(".field").prev('DIV').attr("aria-expanded", "true");
    //alert(index);
    input.next("span.iferror").show();
}

function rmErrorClass(elm) {
    var ediv = $(elm).parents(".diverror");
    var etag = $(elm).parents(".error");
    var eform = $(elm).parents('FORM');
    $(ediv).removeClass("diverror");
    $(elm).removeClass("error");
    $(elm).next("span.iferror").hide();
    $(elm).unbind('.rmErrorClass'); //no further clicks will trigger rmErrorClass();
    if (etag) { $(etag).removeClass("error"); }
    var row = $(elm).closest('.row.haserrors');
    rowHasErrors(row);
}

