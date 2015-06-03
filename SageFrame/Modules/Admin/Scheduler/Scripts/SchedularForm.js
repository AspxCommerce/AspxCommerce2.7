var currentElement = "";
var url = SchedularModuleFilePath + "WebServices/SchedulerWebService.asmx/";
var offset_ = 1;
var current_ = 1;
var perpage = 10;
var upload;
var form1;
var UserModuleID = SchedularModuleID;
/*document ready */
$(document).ready(function () {
    $.validator.addMethod("ValidDates", function (value, element) {
        if (value != "__/__/____") {
            var startdate = new Date($.trim($("#txtStartDate").val()));
            var enddate = new Date($.trim(value));

            return this.optional(element) || enddate >= startdate;
        } else {
            return true;
        }
    });
    $.validator.addMethod("ValidNameSpace", function (value, element) {
        var pattern = /\w+\.\w+,\s\w+/g;
        if (pattern.test(value)) {
            return true;
        } else {
            return false;
        }
    });
    ImageUploader();
    form1 = $("#form1").validate({
        rules: {
            endDates: "ValidDates",
            txtAssemblyName: "ValidNameSpace"
        },
        messages: {
            endDates: "* Enter valid End Date.",
            txtAssemblyName: "Please enter valid namespace"
        },
        submitHandler: function (form) {
            upload.submit();
            upload.enable();
            return true;
        }
    });
    GetSchedularListAll();
    ListAllTasks();
    $('#popupDatepicker').datepick({
        multiSelect: 10
    });
    $('#txtEndDate').datepick();
    $('#txtStartDate').datepick();
    $("#txtStartDate").mask("99/99/9999");
    $("#txtEndDate").mask("99/99/9999");
    $("#running_mode").change(function () {
        ResetInput();
        switch ($(this).val()) {
            case "":
                HideAll();
                break;
            case "0":
                //hourly
                Showspan(0);
                break;
            case "1":
                //daily
                Showspan(1);
                break;

            case "2":
                //weekly
                Showspan(2);
                break;

            case "3":
                //weeknumber
                $("#s2").show();
                $("#s3").show();
                break;

            case "5":
                //once
                HideAll();
                break;

            case "4":
                //calendar
                Showspan(5);
                break;
        }
    });
    for (var i = 1; i < 60; i++) {
        $("#txtRepeatMins").append("<option value='" + i + "'>" + i + "</option>");
    }
});

/************************************WCF call function*************************************************************************/

function ResetInput() {
    $("#s2 :checked").removeAttr("checked");
    $("#s3 :checked").removeAttr("checked");
    $("#Weekly").val("");
    $("textarea").val("");
    $("#txtRepeatHrs").val("");

}

function Showspan(divNum) {
    for (var i = 0; i <= 5; i++) {
        i != divNum ? $("#s" + i).hide() : $("#s" + i).show();
    }
}

function HideAll() {
    for (var i = 0; i <= 5; i++) {
        $("#s" + i).hide();
    }
}

function FillWeeks() {
    var weekDropDown = ["Select", "1st Week", "2nd Week", "3rd Week", "4th Week"];
    $("#s3").prepend("<span style='float:left'>On Week of Month :</span>");
    for (var i = 0; i < weekDropDown.length; i++) {
        $("#s3>#Weekly").append("<option value=" + i + ">" + weekDropDown[i] + "</option>");
    }
    //$("#s3").after("<br>");
}

function FillMonthly() {
    var month = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"];
    var col = 1;
    var str = "<ul style='list-style-type: none;'><li>";
    $.each(month, function (index, val) {
        str += "<input type='checkbox' name='chkmonth' value='" + index + "'/><label for='" + index + "'>" + val + "</label>";
        col++;
    });
    str += "</li></ul>";
    $("#s3").append(str);
}

function AddTask() {
    var _assemblyFileName = $("#uploadFileName").val();
    var _scheduleName = $('#txtTaskName').val();
    var _fullNameSpace = $('#txtAssemblyName').val();
    var _startDate = $('#txtStartDate').val();
    var _endDate = $('#txtEndDate').val();
    var _startHour = $('#txtHours').val() == "" ? 0 : $('#txtHours').val();
    var _startMin = $('#txtMins').val() == "" ? 0 : $('#txtMins').val();
    var _repeatWeeks = $('#txtRepeatWeeks').val() == "" || $('#txtRepeatWeeks').val() == null ? 0 : $('#txtRepeatWeeks').val();
    var _repeatDays = 0;
    var _everyHour = 0;
    var _everyMin = 0;
    var _dependencies = "None";
    var _retryTimeLapse = $('#txtRetryFreq').val().length > 0 ? $('#txtRetryFreq').val() : 0;
    var _retryFrequencyUnit = $('#ddlRetryUnits option:selected').val();
    var _attachToEvent = $('#ddlEventList option:selected').val();
    var _catchUpEnabled = $("#chkIsCatchUpEnabled").is(":checked") ? true : false;
    var _isEnable = true;
    var _servers = "none";
    var _createdByUserID = "superuser";
    var _runningMode = $('#running_mode').val();
    var daysArr = new Array();
    var monthsArr = new Array();
    var weekNumberArr = 0;
    var _weekOfMonth = 0;
    var _dates = "";
    switch (_runningMode) {
        case "0":
            //hourly
            _everyHour = $('#txtRepeatHrs').val() == "" || $('#txtRepeatHrs').val() == null ? 0 : $('#txtRepeatHrs').val();
            _everyMin = $('#txtRepeatMins').val() == "" || $('#txtRepeatMins').val() == null ? 0 : $('#txtRepeatMins').val();
            break;

        case "1":
            //daily
            _repeatDays = $('#RepeatDays').val() == "" || $('#RepeatDays').val() == null ? 0 : $('#RepeatDays').val();
            $("#s1").show();
            break;

        case "2":
            //weekly
            $(":checked[name=chkday]").each(function (index) {
                daysArr.push(parseInt($(this).val()));
            });
            break;

        case "3":
            //weeknumber                    
            $(":checked[name=chkday]").each(function (index) {
                daysArr.push($(this).val());
            });
            $(":checked[name=chkmonth]").each(function (index) {
                monthsArr.push($(this).val());
            });
            _weekOfMonth = $("#Weekly").val();
            break;

        case "4":
            //calendar
            _dates = $("#popupDatepicker").val();
            break;
    }
    var param = {
        ScheduleName: _scheduleName,
        FullNameSpace: _fullNameSpace,
        StartDate: _startDate,
        EndDate: _endDate,
        StartHour: parseInt(_startHour),
        StartMin: parseInt(_startMin),
        RepeatWeeks: parseInt(_repeatWeeks),
        RepeatDays: parseInt(_repeatDays),
        WeekOfMonth: parseInt(_weekOfMonth),
        EveryHour: parseInt(_everyHour),
        EveryMin: parseInt(_everyMin),
        Dependencies: _dependencies,
        RetryTimeLapse: parseInt(_retryTimeLapse),
        RetryFrequencyUnit: parseInt(_retryFrequencyUnit),
        AttachToEvent: parseInt(_attachToEvent),
        CatchUpEnabled: _catchUpEnabled,
        Servers: _servers,
        IsEnable: _isEnable,
        CreatedByUserID: _createdByUserID,
        RunningMode: _runningMode,
        WeekDays: daysArr,
        Months: monthsArr,
        Dates: _dates,
        AssemblyFileName: _assemblyFileName,
        PortalID: SageFramePortalID,
        userModuleId: SchedularModuleID,
        UserName: SageFrameUserName
        , secureToken: SageFrameSecureToken
    };
    param = JSON2.stringify(param);
    var startdate = new Date($.trim($("#txtStartDate").val()));
    var enddate = new Date($.trim($("#txtEndDate").val()));
    $.jsonRequest(url + "AddNewSchedule", param, RefreshGrid);
    csscody.alert("<h1>Add Confirmation</h1><p>Your task has been added.</p>");
    $('#lblFile').text("");
    $("#BoxOverlay").css("z-index", "8999");
}
/*** Retrieve Task***/

function GetTask(id) {
    var param = {
        id: id,
        PortalID: SageFramePortalID,
        userModuleId: SchedularModuleID,
        UserName: SageFrameUserName,
        secureToken: SageFrameSecureToken
    };
    param = JSON2.stringify(param);
    $.jsonRequest(url + "GetTask", param, DisplayRecord);
}

function DisplayRecord(data) {
    ShowNewTaskPopUp(false);
    BindTask(data);
}

function GetShortDate(strdate) {
    if (strdate.indexOf(" ") > 5) {
        var date = new Date(strdate.substring(0, strdate.indexOf(" ")));
        return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear();
    }
}


function BindTask(data) {
    ResetInput();
    $("#sid").val(data.ScheduleID);
    $('#txtTaskName').val(data.ScheduleName);
    $('#txtAssemblyName').val(data.FullNamespace);
    $('#txtStartDate').val(GetShortDate(data.StartDate));
    $('#txtEndDate').val(GetShortDate(data.EndDate));
    $('#txtHours').val(data.StartHour);
    $('#txtMins').val(data.StartMin);
    $('#txtRepeatWeeks').val(data.RepeatWeeks);
    $('#txtRetryFreq').val(data.RetryTimeLapse);
    $('#ddlRetryUnits').val(data.RetryFrequencyUnit);
    $('#ddlEventList').val(data.AttachToEvent);
    $('#running_mode').val(data.RunningMode);
    if (data.IsEnable) {
        console.log(1);
        $("#chkIsEnabled").prop("checked", true);
    }
    else {
        console.log(2);
        $("#chkIsEnabled").prop("checked", false);
    }
    if (data.CatchUpEnabled) {
        $("#chkIsCatchUpEnabled").attr("checked", "checked");
    }
    var daysArr = new Array();
    var monthsArr = new Array();
    var weekNumberArr = 0;
    var _weekOfMonth = 0;
    var _dates = "";
    switch (data.RunningMode) {
        case 0:
            //hourly
            $('#txtRepeatHrs').val(data.EveryHours);
            $('#txtRepeatMins').val(data.EveryMin);
            HideAll();
            Showspan(0);
            break;

        case 1:
            //daily
            $('#RepeatDays').val(data.RepeatDays);
            HideAll();
            Showspan(1);
            break;

        case 2:
            //weekly
            var p = JSON2.stringify({
                ScheduleID: data.ScheduleID,
                PortalID: SageFramePortalID,
                userModuleId: SchedularModuleID
             , UserName: SageFrameUserName
            });
            $.jsonRequest(url + "GetScheduleWeeks", p, function (d) {
                if (d != undefined && d.length > 0) {
                    $.each(d, function (i) {
                        $(":checkbox[name=chkday]").each(function (index) {
                            var element = parseInt($(this).val());
                            if (element == parseInt(d[i].WeekDayID)) $(this).attr("checked", "checked");
                        });
                    });
                }
            });
            HideAll();
            Showspan(2);
            break;

        case 3:
            //weeknumber
            var p = JSON2.stringify({
                ScheduleID: data.ScheduleID,
                PortalID: SageFramePortalID,
                userModuleId: SchedularModuleID
             , UserName: SageFrameUserName
            });
            $.jsonRequest(url + "GetScheduleWeeks", p, function (d) {
                if (d != undefined && d.length > 0) {
                    $.each(d, function (i) {
                        $(":checkbox[name=chkday]").each(function (index) {
                            var element = parseInt($(this).val());
                            if (element == parseInt(d[i].WeekDayID)) $(this).attr("checked", "checked");
                        });
                    });
                }
            });

            $.jsonRequest(url + "GetScheduleMonths", p, function (d) {
                if (d != undefined) {
                    $.each(d, function (i) {
                        $(":checkbox[name=chkmonth]").each(function (index) {
                            var element = parseInt($(this).val());
                            console.log(element + " == " + parseInt(d[i].MonthID));
                            if (element == parseInt(d[i].MonthID)) $(this).attr("checked", "checked");
                        });
                    });
                }
            });
            HideAll();
            $("#Weekly").val(data.WeekOfMonth);
            $("#s2").show();
            $("#s3").show();
            break;

        case 4:
            //calendar
            var p = JSON2.stringify({
                ScheduleID: data.ScheduleID,
                PortalID: SageFramePortalID,
                userModuleId: SchedularModuleID
             , UserName: SageFrameUserName
            });
            $.jsonRequest(url + "GetScheduleDates", p, function (d) {
                var specificDate = "";
                $.each(d, function (i) {
                    var strdate = d[i].Schedule_Date;
                    var test = 'new ' + strdate.replace(/[/]/gi, '');
                    date = eval(test);
                    lastStartTime = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
                    specificDate += lastStartTime + ",";
                });
                specificDate = specificDate.substring(0, specificDate.lastIndexOf(","));
                $("#popupDatepicker").val(specificDate);
            });
            HideAll();
            Showspan(5);
            break;

        case 5:
            HideAll();
            break;
    }
}

function UpdateSchedule() {
    var _scheduleID = $('#sid').val();
    var _scheduleName = $('#txtTaskName').val();
    var _fullNameSpace = $('#txtAssemblyName').val();
    var _startDate = $('#txtStartDate').val();
    var _endDate = $('#txtEndDate').val();
    var _startHour = $('#txtHours').val() == "" ? 0 : $('#txtHours').val();
    var _startMin = $('#txtMins').val() == "" ? 0 : $('#txtMins').val();
    var _repeatWeeks = $('#txtRepeatWeeks').val() == "" || $('#txtRepeatWeeks').val() == null ? 0 : $('#txtRepeatWeeks').val();
    var _repeatDays = 0;
    var _everyHour = 0;
    var _everyMin = 0;
    var _dependencies = "None";
    var _retryTimeLapse = $('#txtRetryFreq').val() == "" ? 0 : $('#txtRetryFreq').val();
    var _retryFrequencyUnit = $('#ddlRetryUnits option:selected').val();
    var _attachToEvent = $('#ddlEventList option:selected').val();
    var _catchUpEnabled = $("#chkIsCatchUpEnabled").is(":checked") ? true : false;
    var _isEnable = $("#chkIsEnabled").is(":checked") ? true : false;
    var _servers = "none";
    var _createdByUserID = "superuser";
    var _runningMode = $('#running_mode').val();
    var daysArr = new Array();
    var monthsArr = new Array();
    var weekNumberArr = 0;
    var _weekOfMonth = 0;
    var _dates = "";
    switch (_runningMode) {
        case "0":
            //hourly
            _everyHour = $('#txtRepeatHrs').val() == "" || $('#txtRepeatHrs').val() == null ? 0 : $('#txtRepeatHrs').val();
            _everyMin = $('#txtRepeatMins').val() == "" || $('#txtRepeatMins').val() == null ? 0 : $('#txtRepeatMins').val();
            break;

        case "1":
            //daily
            _repeatDays = $('#RepeatDays').val() == "" || $('#RepeatDays').val() == null ? 0 : $('#RepeatDays').val();
            $("#s1").show();
            break;

        case "2":
            //weekly
            $(":checked[name=chkday]").each(function (index) {
                daysArr.push(parseInt($(this).val()));
            });
            break;

        case "3":
            //weeknumber
            $(":checked[name=chkday]").each(function (index) {
                daysArr.push($(this).val());
            });

            $(":checked[name=chkmonth]").each(function (index) {
                monthsArr.push($(this).val());
            });
            _weekOfMonth = $("#Weekly").val();
            break;

        case "4":
            //calendar                    
            _dates = $("#popupDatepicker").val();
            break;
    }
    var param = {
        ScheduleID: _scheduleID,
        ScheduleName: _scheduleName,
        FullNameSpace: _fullNameSpace,
        StartDate: _startDate,
        EndDate: _endDate,
        StartHour: parseInt(_startHour),
        StartMin: parseInt(_startMin),
        RepeatWeeks: parseInt(_repeatWeeks),
        RepeatDays: parseInt(_repeatDays),
        WeekOfMonth: parseInt(_weekOfMonth),
        EveryHour: parseInt(_everyHour),
        EveryMin: parseInt(_everyMin),
        Dependencies: _dependencies,
        RetryTimeLapse: parseInt(_retryTimeLapse),
        RetryFrequencyUnit: parseInt(_retryFrequencyUnit),
        AttachToEvent: parseInt(_attachToEvent),
        CatchUpEnabled: _catchUpEnabled,
        Servers: _servers,
        IsEnable: _isEnable,
        CreatedByUserID: _createdByUserID,
        RunningMode: _runningMode,
        WeekDays: daysArr,
        Months: monthsArr,
        Dates: _dates,
        PortalID: SageFramePortalID,
        userModuleId: SchedularModuleID,
        UserName: SageFrameUserName,
        secureToken: SageFrameSecureToken
    };
    param = JSON2.stringify(param);
    $.jsonRequest(url + "UpdateSchedule", param, RefreshGrid);
    $('#newScheduleDiv').hide();
    $('#fade1').remove();
    csscody.alert("<h1>Update Confirmation</h1><p>Your task has been updated.</p>");
    $("#BoxOverlay").css("z-index", "8999");
}

function RunScheduleNow(id, args) {
    var param = {
        id: id,
        PortalID: SageFramePortalID,
        userModuleId: SchedularModuleID,
        UserName: SageFrameUserName,
        secureToken: SageFrameSecureToken
    };
    param = JSON2.stringify(param);
    $.jsonRequest(url + "RunScheduleNow", param, function (data) {
        $.facebox.close();
        jQuery(document).trigger('loading.facebox');
        viewrecord(args);
        RefreshGrid();
    });
}

function UpdateMsgDiv(data) {

}
/*End of wcf functions*/

function RefreshGrid() {
    GetSchedularListAll();
}

function InitalizeContextMenu() {

}

function ShowNewTaskPopUp(isNewSchedule) {
    $("#chkIsEnabled").prop("checked", true);
    GetCurrentDate();
    FillWeeks();
    FillMonthly();
    AddAllDayTask();
    BindEvents();
    $('#txtSpecificDate').datepicker();
    $("span.fileUploadMsg").html("");
    FillHourMin();
    $("label[class=error]").remove();
    $("span.error").hide();
    $("input.error").removeClass("error");
    $("#lblFile").text("");
    if (isNewSchedule) {
        HideAll();
        ResetInput();

        $("#uploadFileName").attr("class", "required");
        $("span.headerSpan").html("Add New Task");
        $(".btnupdateTaskspan").hide();
        $(".btnAddTaskspan").show();
        $("#trDllUpload").show();
        $("#fileupload").removeAttr("disabled");
        $("#fileupload").click(function () {
            $("span.fileUploadMsg").html("");
            $("#uploadFileName").val("");
        });
        $('#newScheduleDiv input[type="text"]').val("");
        $('#newScheduleDiv select').val("");
        var currentDate = new Date();
        $('#txtHours').val(currentDate.getHours());
        $('#txtMins').val(currentDate.getMinutes());
        $('#txtStartDate').val(currentDate.getMonth() + 1 + "/" + currentDate.getDate() + "/" + currentDate.getFullYear());
        $("#txtAssemblyName").removeAttr("disabled");
        $("#btnAddTask").click(function () {
            var upfile = $("#uploadFileName").val();

            if (upfile.length < 1 || (upfile.length > 1 && $("#lblFile").text() == '')) {
                $("span.error").show();
                $("span.fileUploadMsg").attr("class", "error").html("Please select the dll file").show();
            }
        });

        $('#ddlEventList').val(1);
        $('#ddlRetryUnits').val(1);
    } else {
        $("#uploadFileName").removeAttr("class");
        $("span.headerSpan").html("Edit Task");
        $(".btnAddTaskspan").hide();
        $(".btnupdateTaskspan").unbind().bind("click", function () {
            if (form1.form()) {
                UpdateSchedule();
            }
        }).show();
        $("#trDllUpload").hide();
        $("#txtAssemblyName").attr("disabled", "true");
    }
    $('#txtHours').removeAttr("style");
    $('#txtMins').removeAttr("style");
    $('#running_mode').removeAttr("style");
    $('#ddlRetryUnits').removeAttr("style");
    $('#ddlEventList').removeAttr("style");
    $('#txtRepeatHrs').removeAttr("style");
    $('#txtRepeatMins').removeAttr("style");
    $('#newScheduleDiv').removeClass('invisibleDiv').addClass('loading-visible').fadeIn("slow");
    $('body').append("<div id='fade1' style='display:block'></div>");
    return false;
}

function ShowAllDayNewTaskPopUp() {
    $('#allDayNewSchedule').removeClass('invisibleDiv').addClass('loading-visible').fadeIn("slow");
}

function ListAllTasks() {
    BindEvents();
}

function BindAllTasks(data) {
    $('#scheduleListAll table tr>td').parent().remove();
    for (var i in data) {
        var task = data[i].ScheduleName;
        var id = data[i].ScheduleID;
        var nextStart = new Date(data[i].NextStart);
        var IsEnable = data[i].IsEnable;
        var lastStart = data[i].HistoryStartDate;
        var lastEndDate = data[i].HistoryEndDate;
        var taskDiv = $('#scheduleListAll table');
        var row = $("<tr/>");
        row.append("<td>" + task + "</td>");
        row.append("<td>retry </td>");
        row.append("<td>N:" + nextStart.getFullYear() + '/' + nextStart.getMonth() + '/' + nextStart.getDate() + ' ' + nextStart.getHours() + ':' + nextStart.getMinutes() + ':' + nextStart.getSeconds() + "</td>");
        var lastStartTime = "Not started";
        if (lastStart != null) {
            var test = 'new ' + lastStart.replace(/[/]/gi, '');
            date = eval(test);
            lastStartTime = 'S: &nbsp;' + date.getFullYear() + '/' + date.getMonth() + '/' + date.getDate() + ' ' + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds() + ' &nbsp;';
        }
        row.append("<td>" + lastStartTime + "</td>");
        var lastEnd = "NA";
        if (lastEndDate != null) {
            console.log(lastEndDate);
            var endDate = 'new ' + lastEndDate.replace(/[/]/gi, '');
            endDate = eval(endDate);
            lastEnd = ' &nbsp; E:' + endDate.getFullYear() + '/' + endDate.getMonth() + '/' + endDate.getDate() + ' ' + endDate.getHours() + ':' + endDate.getMinutes() + ':' + endDate.getSeconds() + ' &nbsp;';
        }
        row.append("<td>" + lastEnd + "</td>");
        row.append('<td><span class="detail"><img src="images/history.png" /></span></td>');
        var isChecked = IsEnable == true ? "checked" : "";
        row.append('<td><span  class="isEnable "></span><input  class="detail" type="checkbox" value="" id="a"' + isChecked + ' /></td>').change(function () {
            var flag = false;
            if ($(this).find(":checked").length > 0) {
                flag = true;
            }
            var param = {
                scheduleId: parseInt($(this).attr("id")),
                isEnable: Boolean(flag),
                PortalID: SageFramePortalID,
                userModuleId: SchedularModuleID
         , UserName: SageFrameUserName
            }
            param = JSON2.stringify(param);
            $.jsonRequest(url + "UpdateScheduleEnableStatus", param, successFunction);
        });
        row.append('<td><span class="delete"><img src="images/delete.png"/></span></td>');
        taskDiv.append(row);
    }
    $('div.cssAllSchedule table span.delete').bind("click", function () {
        var id = $(this).closest("div").prop("id");
    });
}

function successFunction(data) {
    $('#trDllUpload').slideDown("slow");

}

function AddAllDayTask() {
    $('#btnAddAllDayTask').click(function () {
        var task = $('#txtAllDayTaskName').val();
        task = '<div class="allDayScheduleItem">' + task + ' <span class="delete"><img src="images/delete.png" /></span></div>';
        $(currentElement).append(task);
        CloseTaskPopUp();
        BindEvents();
    });
}

function BindEvents() {
    $('.cssClassScheduleItem span.delete').bind("click", function () {
        $(this).closest("div").remove();
    });
    $('.allDayScheduleItem span.delete').bind("click", function () {
        $(this).closest("div").remove();
    });
    $('#bottomControlDiv span.task').bind("click", function () {
        ShowNewTaskPopUp(true);
    });
    $('.cssClassScheduleItem').bind("click", function () {
        $('#popDiv').removeClass('invisibleDiv').addClass('loading-visible');
    });
    $('a.close').bind("click", function () {
        $('#popDiv').addClass('invisibleDiv').removeClass('loading-visible');
    });
    $('#schedulerHeader span.all').bind("click", function () {
        $('#scheduleListAll').fadeIn("slow");
        $('#scheduler,#scheduleListWeekly,#taskHistoryDiv,#activeTasksDiv,#scheduleListMonthly').hide();
    });
    $('#schedulerHeader span.day').bind("click", function () {
        $('#scheduler').fadeIn("slow");
        $('#scheduleListWeekly,#taskHistoryDiv,#activeTasksDiv,#scheduleListMonthly').hide();
        $('#scheduleListAll').hide();
    });
    $('#schedulerHeader span.week').bind("click", function () {
        $('#scheduleListWeekly').fadeIn("slow");
        $('#scheduler,#scheduleListAll,#taskHistoryDiv,#activeTasksDiv,#scheduleListMonthly').hide();
    });
    $('#schedulerHeader span.month').bind("click", function () {
        $('#scheduleListMonthly').fadeIn("slow");
        $('#scheduler,#scheduleListAll,#taskHistoryDiv,#activeTasksDiv,#scheduleListWeekly').hide();
        $('#schedulerHeader span.month').unbind("click");
    });
    $('.closePopUp').on("click", function () {
        CloseTaskPopUp();
        //$("input.error").removeClass("error");
        //$("label.error, span.error").hide();

    });
    $('#bottomControlDiv span.history').bind("click", function () {
        $('#taskHistoryDiv').fadeIn("slow");
        $('#scheduler,#scheduleListAll,#scheduleListWeekly,#activeTasksDiv,#scheduleListMonthly').hide();
    });
    $('#bottomControlDiv span.active').bind("click", function () {
        $('#activeTasksDiv').fadeIn("slow");
        $('#scheduler,#scheduleListAll,#scheduleListWeekly,#taskHistoryDiv,#scheduleListMonthly').hide();
    });
    $('#nextDayTasks').bind("click", function () {
        alert("Loading next day task");
    });
    $('#prevDayTasks').bind("click", function () {
        alert("Loading previous day task");
    });
    $('#imgAddTimeFrame').bind("click", function () {
        ToggleTimeFrame();
        if ($(this).attr("src") == "images/add.png") {
            $(this).attr("src", "images/delete.png")
        }
        else if ($(this).attr("src") == "images/delete.png") {
            $(this).attr("src", "images/add.png")
        }
    });
}

function ToggleTimeFrame() {
    $('#timeFrameDiv').slideToggle("slow");
}

function InitializeDragAndDrop() {
    $('.cssClassScheduleItem').draggable({
        stop: function (event, ui) { }
    });
}

function InitializeActivityIndicator() {
    $('body').append('<div id="ajaxBusy"><p><img src="images/ajax-loader.gif"></p></div>');
    $('#ajaxBusy').css({
        display: "none",
        margin: "0px",
        paddingLeft: "0px",
        paddingRight: "0px",
        paddingTop: "0px",
        paddingBottom: "0px",
        position: "absolute",
        right: "3px",
        top: "3px",
        width: "auto"
    });

    // Ajax activity indicator bound to ajax start/stop document events
    $(document).ajaxStart(function () {
        $('#ajaxBusy').show();
    }).ajaxStop(function () {
        $('#ajaxBusy').hide();
    });
}

function CloseTaskPopUp() {
    $('#newScheduleDiv').hide().addClass('invisibleDiv').removeClass('loading-visible');
    $("#fade1").remove();
}

function GetCurrentDate() {
    var now = new Date();
    var date = "Date:" + now.getDate() + "-" + now.getMonth() + "-" + now.getFullYear();
    $('.date').text(date);
}

function backgroundFilter() {
    var div;
    if (document.getElementById) div = document.getElementById('backgroundFilter');
    else if (document.all) div = document.all['backgroundFilter'];
    if (div.style.display == '' && div.offsetWidth != undefined && div.offsetHeight != undefined) div.style.display = (div.offsetWidth != 0 && div.offsetHeight != 0) ? 'block' : 'none';
    div.style.display = (div.style.display == '' || div.style.display == 'block') ? 'none' : 'block';
}

function errorFn() {
}

function ImageUploader() {
    $("span.fileUploadMsg").html();
    var uploadFlag = false;
    upload = new AjaxUpload($('#fileUpload'), {
        action: SchedularModuleFilePath + 'UploadHandler.ashx?userModuleId=' + SchedularModuleID + "&portalID=" +
                    SageFramePortalID + "&sageFrameSecureToken=" + SageFrameSecureToken + "&userName=" + SageFrameUserName,
        name: 'myfile[]',
        multiple: true,
        data: {},
        autoSubmit: false,
        responseType: 'json',
        onChange: function (file, ext) {
            var param = JSON2.stringify({
                FileName: file,
                portalID: SageFramePortalID,
                userModuleId: SchedularModuleID,
                sageFrameSecureToken: SageFrameSecureToken,
                userName: SageFrameUserName
            });
            $.jsonRequest(url + "isFileUniqueTask", param, function (data) {
                if (data == "0") {
                    $("span.fileUploadMsg").html("<b>File with name '" + file + "' already exists!!<br> Please enter unique filename.</b>");
                } else {
                    $("span.fileUploadMsg").html("");
                    $("#uploadFileName").val(file);
                    $('#lblFile').text(file);
                    $("span.error").hide();
                    uploadFlag = true;
                }
            });
        },
        onSubmit: function (file, ext) {
            if (ext != "exe") {
                if (ext && /^(dll)$/i.test(ext)) {
                    if (!uploadFlag) return false;
                } else {
                    $("span.fileUploadMsg").html("Not a valid dll file!");
                    return false;
                }
            }
            else {
                $("span.fileUploadMsg").html("Not a valid dll file!");
                return false;
            }
        },
        onComplete: function (file, response) {
            if (response == "LargeImagePixel") {
                return ConfirmDialog(this, 'message', "The image size is too large in pixel");
            }
            if ($("#uploadFileName").val().length < 1) {
                $("span.fileUploadMsg").html("Please select dll file!");
            }
            else if (uploadFlag) {
                AddTask();
                ListAllTasks();
                CloseTaskPopUp();
                BindEvents();
                uploadFlag = false;
            }
        }
    });
}

function FillHourMin() {
    var option = "<option value=0>HH</option>";
    var mins = ["00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10"];
    for (var i = 1; i <= 24; i++) {
        option += "<option value=" + i + ">" + i + "</option>";
    }
    $("#txtHours").html(option);
    $("#txtRepeatHrs").html(option);

    /*Minutes*/
    var minOption = "<option value=''>MM</option>";
    for (i = 0; i < mins.length; i++) {
        minOption += "<option value=" + i + ">" + mins[i] + "</option>";
    }
    for (i = 11; i < 60; i++) {
        minOption += "<option value=" + i + ">" + i + "</option>";
    }
    $("#txtMins").append(minOption).show();
    $("#txtRepeatMins").html(minOption);
}

function GetSchedularListAll() {
    $("#gdvSchedularListAll").sagegrid({
        url: url,
        functionMethod: 'GetAllTasks',
        colModel: [
            {
                display: 'ScheduleID',
                name: 'schedule_id',
                coltype: 'checkbox',
                align: 'center',
                elemClass: 'EmailsChkbox',
                elemDefault: false,
                controlclass: 'itemsHeaderChkbox',
                hide: true
            },
        {
            display: 'Schedule Name',
            name: 'schedule_name',
            coltype: 'label',
            align: 'center'
        },
        {
            display: 'Full Namespace',
            name: 'full_namespace',
            coltype: 'label',
            align: 'left'
        },
        {
            display: 'Start Date',
            name: 'start_date',
            coltype: 'label',
            align: 'center',
            type: 'date',
            format: 'dd/MM/yyyy HH:mm:ss'
        },
        {
            display: 'End Date',
            name: 'end_date',
            coltype: 'label',
            align: 'center',
            type: 'date',
            format: 'dd/MM/yyyy HH:mm:ss'
        },
        {
            display: 'StartHour',
            name: 'start_hour',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'StartMin',
            name: 'start_min',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'RepeatWeeks',
            name: 'repeat_weeks',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'RepeatDays',
            name: 'repeat_days',
            sortable: false,
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'WeekOfMonth',
            name: 'week_OfMonth',
            sortable: false,
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'EveryHours',
            name: 'every_hours',
            sortable: false,
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'EveryMin',
            name: 'every_min',
            sortable: false,
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'ObjectDependencies',
            name: 'object_dependencies',
            sortable: false,
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'Retry Time Lapse',
            name: 'retry_time_lapse',
            sortable: false,
            coltype: 'label',
            align: 'center',
            hide: false
        },
        {
            display: 'RetryFrequencyUnit',
            name: 'retry_frequency_unit',
            sortable: false,
            coltype: 'label',
            align: 'center',
            hide: true
        },
        {
            display: 'AttachToEvent',
            name: 'attach_to_event',
            sortable: false,
            coltype: 'label',
            align: 'center',
            hide: true
        },
        {
            display: 'CatchUpEnabled',
            name: 'catch_up_enabled',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'Servers',
            name: 'servers',
            cssclass: '',
            sortable: true,
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'CreatedByUserID',
            name: 'created_by_userid',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'CreatedOnDate',
            name: 'create_ondate',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'LastModifiedbyUserID',
            name: 'lastmodified_byuserid',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'LastModifiedDate',
            name: 'last_modified_date',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'IsEnable',
            name: 'is_enable',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },

        {
            display: 'ScheduleHistoryID',
            name: 'schedule_history_id',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'Next Run',
            name: 'next_start',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            type: 'date',
            format: 'dd/MM/yyyy HH:mm:ss'
        },
        {
            display: 'HistoryStartDate',
            name: 'history_start_date',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            type: 'date',
            format: 'dd/MM/yyyy HH:mm:ss',
            hide: true
        },
        {
            display: 'Last Run',
            name: 'history_end_date',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            type: 'date',
            format: 'dd/MM/yyyy HH:mm:ss'
        },
        {
            display: 'RunningMode',
            name: 'runningMode',
            cssclass: '',
            coltype: 'label',
            align: 'left'
        },
        {
            display: 'AssemblyFileName',
            name: 'assemblyFileName',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'Actions',
            name: 'action',
            cssclass: 'cssClassAction',
            sortable: false,
            coltype: 'label',
            align: 'center'
        }
        ],

        buttons: [
            {
                display: 'Edit',
                name: 'edit',
                enable: true,
                _event: 'click',
                trigger: '1',
                arguments: '8,9,16'
            },
        {
            display: 'Delete',
            name: 'delete',
            enable: true,
            _event: 'click',
            trigger: '2',
            arguments: '1,28'
        },
        {
            display: 'History',
            name: 'history',
            enable: true,
            _event: 'click',
            trigger: '3',
            arguments: '1,2,3,4,27'
        }
        ],

        rp: perpage,
        nomsg: "No Records Found!",
        param: {
            PortalID: SageFramePortalID,
            userModuleId: SchedularModuleID
         , UserName: SageFrameUserName
         , secureToken: SageFrameSecureToken
        },
        current: current_,
        pnew: offset_,
        sortcol: {
            0: {
                sorter: false
            },
            1: {
                sorter: false
            },
            19: {
                sorter: false
            }
        }
    });
}

function editrecord(args) {
    var scheduleId = args[0];
    GetTask(scheduleId);
}

function deleterecord(args) {
    var scheduleId = args[0];
    if (parseInt(scheduleId) > 0) {
        var properties = {
            onComplete: function (e) {
                DeleteTask(scheduleId, args[4], e);
            }
        }
        csscody.confirm("<h1>Delete Confirmation</h1><p>Do you want to delete all selected items?</p>", properties);
        $("#BoxOverlay").css("z-index", "8999");
    }
}

function viewrecord(args) {
    var scheduleId = args[0];
    var ScheduleName = args[3];
    var ScheduleFullNameSpace = args[4];
    var ScheduleStartDate = args[5];
    var scheduleEndDate = args[6];
    var runningMode = args[7];
    $("span.spanScheduleNamespace").html(ScheduleFullNameSpace);
    $("span.spanScheduleName").html(ScheduleName);
    $("span.spanScheduleStartDate").html(ScheduleStartDate);
    $("span.spanScheduleEndDate").html(scheduleEndDate);
    $("span.spanScheduleRunningMode").html(runningMode);
    GetScheduleHistoryList(scheduleId);
    $(document).bind('reveal.facebox', function () {
        $("input.btnRunSchedule").unbind().bind("click", function () {
            RunScheduleNow(scheduleId, args);
        });
    });
}

function DeleteTask(id, AssemblyFileName, e) {
    if (e) {
        var param = {
            ScheduleID: parseInt(id),
            AssemblyFileName: AssemblyFileName,
            PortalID: SageFramePortalID,
            userModuleId: SchedularModuleID
         , UserName: SageFrameUserName,
            secureToken: SageFrameSecureToken
        };
        param = JSON2.stringify(param);
        $.jsonRequest(url + "DeleteTask", param, RefreshGrid);
    }
}

function GetScheduleHistoryList(id) {
    GetSchedularHistoryList(id);
}

function GetSchedularHistoryList(id) {
    $("#gdvSchedularHistoryList").sagegrid({
        url: url,
        functionMethod: 'GetAllScheduleHistory',
        colModel: [
            {
                display: 'ScheduleHistoryID',
                name: 'schedule_history_id',
                coltype: 'checkbox',
                align: 'center',
                elemClass: 'EmailsChkbox',
                elemDefault: false,
                controlclass: 'itemsHeaderChkbox',
                hide: true
            },
        {
            display: 'ScheduleID',
            name: 'schedule_id',
            coltype: 'checkbox',
            align: 'center',
            hide: true
        },
        {
            display: 'StartDate',
            name: 'start_date',
            coltype: 'label',
            align: 'center',
            type: 'date',
            format: 'dd/MM/yyyy HH:mm:ss'
        },
        {
            display: 'EndDate',
            name: 'end_date',
            coltype: 'label',
            align: 'center',
            type: 'date',
            format: 'dd/MM/yyyy HH:mm:ss'
        },
        {
            display: 'Status',
            name: 'return_text',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'ReturnText',
            name: 'return_text',
            cssclass: '',
            coltype: 'label',
            align: 'left'
        },
        {
            display: 'Server',
            name: 'return_text',
            cssclass: '',
            coltype: 'label',
            align: 'left',
            hide: true
        },
        {
            display: 'NextStart',
            name: 'end_date',
            coltype: 'label',
            align: 'center',
            type: 'date',
            format: 'dd/MM/yyyy HH:mm:ss'
        }

        ],

        rp: perpage,
        nomsg: "No Records Found!",
        param: {
            ScheduleID: id,
            PortalID: SageFramePortalID,
            userModuleId: SchedularModuleID,
            UserName: SageFrameUserName,
            secureToken: SageFrameSecureToken
        },
        current: current_,
        pnew: offset_,
        SuccessPara: {
            div: '#scheduleHistoryList'
        }
    });
}

(function ($) {
    $.jsonRequest = function (url, para, successFn) {
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            data: para,
            dataType: "json",
            success: function (msg) {
                successFn(msg.d);
            },
            error: function (msg) {
                errorFn();
            }
        });
    }
})(jQuery);