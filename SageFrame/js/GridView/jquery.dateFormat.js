﻿(function($) {
    $.format = (function() {

        var parseMonth = function(value) {

            switch (value) {
                case "Jan":
                    return "01";
                case "Feb":
                    return "02";
                case "Mar":
                    return "03";
                case "Apr":
                    return "04";
                case "May":
                    return "05";
                case "Jun":
                    return "06";
                case "Jul":
                    return "07";
                case "Aug":
                    return "08";
                case "Sep":
                    return "09";
                case "Oct":
                    return "10";
                case "Nov":
                    return "11";
                case "Dec":
                    return "12";
                default:
                    return value;
            }
        };

        var parseTime = function(value) {
            var retValue = value;
            if (retValue.indexOf(".") !== -1) {
                retValue = retValue.substring(0, retValue.indexOf("."));
            }

            var values3 = retValue.split(":");

            if (values3.length === 3) {
                hour = values3[0];
                minute = values3[1];
                second = values3[2];

                return {
                    time: retValue,
                    hour: hour,
                    minute: minute,
                    second: second
                };
            } else {
                return {
                    time: "",
                    hour: "",
                    minute: "",
                    second: ""
                };
            }
        };

        return {
            date: function(value, format) {
                //value = new java.util.Date()
                //2009-12-18 10:54:50.546
                try {
                    var year = null;
                    var month = null;
                    var dayOfMonth = null;
                    var time = null; //json, time, hour, minute, second
                    if (typeof value.getFullYear === "function") {
                        year = value.getFullYear();
                        month = value.getMonth() + 1;
                        dayOfMonth = value.getDate();
                        time = parseTime(value.toTimeString());
                    } else {
                        var values = value.split(" ");

                        switch (values.length) {
                            case 6: //Wed Jan 13 10:43:41 CET 2010
                                year = values[5];
                                month = parseMonth(values[1]);
                                dayOfMonth = values[2];
                                time = parseTime(values[3]);
                                break;
                            case 2: //2009-12-18 10:54:50.546
                                var values2 = values[0].split("-");
                                year = values2[0];
                                month = values2[1];
                                dayOfMonth = values2[2];
                                time = parseTime(values[1]);
                                break;
                            default:
                                return value;
                        }
                    }

                    var pattern = "";
                    var retValue = "";
                    //Issue 1 - variable scope issue in format.date 
                    //Thanks jakemonO
                    for (var i = 0; i < format.length; i++) {
                        var currentPattern = format.charAt(i);
                        pattern += currentPattern;
                        switch (pattern) {
                            case "dd":
                                retValue += dayOfMonth;
                                pattern = "";
                                break;
                            case "MM":
                                retValue += month;
                                pattern = "";
                                break;
                            case "yyyy":
                                retValue += year;
                                pattern = "";
                                break;
                            case "HH":
                                retValue += time.hour;
                                pattern = "";
                                break;
                            case "hh":
                                retValue += (time.hour === 0 ? 12 : time.hour < 13 ? time.hour : time.hour - 12);
                                pattern = "";
                                break;
                            case "mm":
                                retValue += time.minute;
                                pattern = "";
                                break;
                            case "ss":
                                retValue += time.second;
                                pattern = "";
                                break;
                            case "a":
                                retValue += time.hour > 12 ? "PM" : "AM";
                                pattern = "";
                                break;
                            case " ":
                                retValue += currentPattern;
                                pattern = "";
                                break;
                            case "/":
                                retValue += currentPattern;
                                pattern = "";
                                break;
                            case ":":
                                retValue += currentPattern;
                                pattern = "";
                                break;
                            default:
                                if (pattern.length === 2 && pattern.indexOf("y") !== 0) {
                                    retValue += pattern.substring(0, 1);
                                    pattern = pattern.substring(1, 2);
                                } else if ((pattern.length === 3 && pattern.indexOf("yyy") === -1)) {
                                    pattern = "";
                                }
                        }
                    }
                    return retValue;
                } catch (e) {
                    console.log(e);
                    return value;
                }
            }
        };
    } ());
} (jQuery));


//$(document).ready(function() {
//    $(".shortDateFormat").each(function(idx, elem) {
//        if ($(elem).is(":input")) {
//            $(elem).val($.format.date($(elem).val(), 'dd/MM/yyyy'));
//        } else {
//            $(elem).text($.format.date($(elem).text(), 'dd/MM/yyyy'));
//        }
//    });
//    $(".longDateFormat").each(function(idx, elem) {
//        if ($(elem).is(":input")) {
//            $(elem).val($.format.date($(elem).val(), 'dd/MM/yyyy hh:mm:ss'));
//        } else {
//            $(elem).text($.format.date($(elem).text(), 'dd/MM/yyyy hh:mm:ss'));
//        }
//    });
//});

