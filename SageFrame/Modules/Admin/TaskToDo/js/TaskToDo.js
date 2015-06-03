(function($) {
    var Offset = 0;

    var str = ' ';
    var TotalTodayTask = 0;
    var TotalTask = 0

    $.CreateTask = function(p) {
        p = $.extend
                ({
                    BaseUrl: '',
                    UserModuleId: ''
                }, p);
        var TaskToDo = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: { data: '' },
                dataType: 'json',
                ajaxCallMode: 0,
                baseURL: p.BaseUrl + "TaskToDoWebService.asmx/",
                PortalID: SageFramePortalID,
                UserModuleID: p.UserModuleId,
                CultureName: SageFrameCurrentCulture,
                UserName: SageFrameUserName
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: TaskToDo.config.type,
                    contentType: TaskToDo.config.contentType,
                    async: TaskToDo.config.async,
                    cache: TaskToDo.config.cache,
                    url: TaskToDo.config.url,
                    data: TaskToDo.config.data,
                    dataType: TaskToDo.config.dataType,
                    success: TaskToDo.ajaxSuccess,
                    error: TaskToDo.ajaxFailure
                });
            },
            init: function() {
                TaskToDo.BindEvents();
            },
            ajaxFailure: function() {

            },
            ajaxSuccess: function(data) {
                switch (TaskToDo.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:

                        break;
                    case 2:
                        Offset = 0;
                        TaskToDo.GetTask();
                        break;
                    case 3:
                        TaskToDo.BindTask(data);
                        $('#dvTask').show();
                        $('#dvSpecificTask').hide();
                        break;
                    case 4:
                        $('#txtAddNote').val('');
                        $('#txtDate').val('');
                        TaskToDo.ClearData();
                        Offset = 0;
                        TaskToDo.GetTask();
                        break;

                }
            },
            BindEvents: function() {

                $('.addNew').hide();
                $('#BtnTaskBack').hide();
                str = "Today";
                TaskToDo.GetTask();

                $('#tabTaskList ul li').click(function() {
                    $('#txtSearchDate').val('');
                    str = $(this).find('a').html();
                    Offset = 0;
                    TaskToDo.GetTask();
                });
                $('#AddTaskTab').click(function() {
                    $('#dvSearch').hide();
                    $('#dvaddNew').show();
                    $('#tabTaskList').hide();
                    $('#AddTaskTab').hide();
                });
                $('#btnTaskCancel').click(function() {
                    TaskToDo.ClearData();
                    $('#dvSearch').show();
                    $('#AddTaskTab').show();
                    $('#tabTaskList').show();
                    $('#dvaddNew').hide();
                });
                $('#txtSearchDate').datepicker({ onClose: function(dateText, inst) {
                    if ($('#txtSearchDate').val() != "") {
                        $("#divSearch").show();
                        $('#txtSearchDate').focus();
                        $('#txtSearchDate').css({
                            'width': 100
                        });
                    }
                }
                });
                $('#txtSearchDate').click(function() {
                    $('#txtSearchDate').focus();
                    $('#txtSearchDate').css({
                        'width': 100
                    });
                });

                $('#btnSearchTab').click(function() {
                    $('#txtSearchDate').focus();
                    $('#txtSearchDate').css({
                        'width': 100
                    });
                });
                $('#txtDate').datepicker({

//                    dateFormat: 'yy-mm-dd',
//                    changeMonth: true,
//                    changeYear: true,
//                    yearRange: '-100:+0'

                });
                $('#btnAddNewTask').click(function() {
                    var v = $('#form1').validate
                         ({
                             rules: {
                                 txtAddNote: { required: true },
                                 txtDate: { required: true }
                             },
                             messages: {
                                 txtAddNote: { required: "Your task is empty" },
                                 txtDate: { required: "Pick date for your Task" }
                             }
                         });
                    if (v.form()) {
                        var Note = $('#txtAddNote').val();
                        var Date = $('#txtDate').val();
                        var id = 0;
                        TaskToDo.saveTask(Note, Date, id);

                    }
                });

                $('#txtSearchDate').change(function() {
                    if ($('#txtSearchDate').val() != '') {
                        Offset = 0;
                        TaskToDo.GetTask();
                    }
                });

                $('#dvScroll').bind('scroll', function() {
                    var outerHeight = $(this)[0].scrollHeight;
                    var iScrollHeight = $(this).scrollTop();
                    var iHeight = $(this).innerHeight();
                    if (iHeight + iScrollHeight >= outerHeight) {
                        if (TotalTask > Offset) {
                            TaskToDo.GetTask();
                        }
                    }
                });
                $('#dvTask,#dvSearchTaskList').on('click', '.sftaskNote', function() {
                    $('.active').find('.note').show();
                    $('.active').find('.EditTaskWrapper').remove();
                    $('.tasktodo').removeClass('active');
                    $(this).parent().parent().addClass('active');
                    var id = $(this).parent('div').parent('div').prop('id');
                    TaskToDo.EditTask(id);
                    $('#' + id).find('.note').hide();
                });

                $('#dvTask,#dvSearchTaskList').on('click', '.btnDelete', function() {
                    var Id = $(this).parent('div').parent('div').prop('id');
                    var ID = Id.split('_');
                    var id = ID[1];
                    jConfirm('Are you sure you want to delete task?', 'Delete', function(r) {
                        if (r) {
                            TaskToDo.DeleteTask(id);
                        }
                    });

                });


                $('#dvTask,#dvSearchTaskList').on('click', '#btnTaskListCancel', function() {
                    $(this).parent('div').parent('div').find('.note').show();
                    $(this).parent('div').parent('div').find('.EditTaskWrapper').hide();
                    $(this).parent().parent().removeClass('active');
                });
                $('#dvTask,#dvSearchTaskList').on('click', '#btnTaskListEdit', function() {
                    var v = $('#form1').validate
                         ({
                             rules: {
                                 txtEditNote: { required: true },
                                 txtEditDate: { required: true }
                             },
                             messages: {
                                 txtEditNote: { required: "Task field is empty." },
                                 txtEditDate: { required: "Pick date for your task." }
                             }
                         });
                    if (v.form()) {
                        var id = id = $(this).parent('div').parent('div').prop('id');
                        id = id.replace('todaytask_', '');
                        var Note = $('#EditNote').val();
                        var Date = $('#EditDate').val();
                        TaskToDo.saveTask(Note, Date, id);

                    }
                });
                $('#dvTask,#dvSearchTaskList').on('input propertychange', '.sfInputbox ', function(e) {
                    if ($(this).val().length >= 200) {
                        $(this).val($(this).val().substring(0, 200));
                    }
                });
                $('#BtnTaskBack').click(function() {
                    $('#tabTaskList').show();
                    $('#AddTaskTab').show();
                    $('#BtnTaskBack').hide();
                    $('#dvSearchTaskList').hide();
                    $('#txtSearchDate').val('');
                    $('#dvSearch').show();
                    $('#divSearch').hide();

                    $('#txtSearchDate').css({
                        'width': 20
                    });
                    Offset = 0;
                    TaskToDo.GetTask();
                    $('#tabTaskList').tabs({ active: 1 });

                });
            },
            ClearData: function() {
                $('#dvSearch').show();
                $('#AddTaskTab').show();
                $('#tabTaskList').show();
                $('#dvaddNew').hide();
                $('#dvaddNew').hide();
                $('#tabTaskList').show();
                var id = 0;
            },
            saveTask: function(Note, Date, id) {
                TaskToDo.config.method = "SaveTask";
                TaskToDo.config.url = TaskToDo.config.baseURL + TaskToDo.config.method;
                TaskToDo.config.data = JSON2.stringify({
                    Note: Note,
                    date: Date,
                    CultureField: TaskToDo.config.CultureName,
                    PortalID: TaskToDo.config.PortalID,
                    UserModuleId: TaskToDo.config.UserModuleID,
                    UserName: TaskToDo.config.UserName,
                    Id: id,
                    secureToken: SageFrameSecureToken
                });
                TaskToDo.config.ajaxCallMode = 4;
                TaskToDo.ajaxCall(TaskToDo.config);
            },
            EditTask: function(id) {
                var ID = id.split('_');
                var Data = '';
                myData = JSON2.stringify({
                    Id: ID[1],
                    PortalID: TaskToDo.config.PortalID,
                    UserModuleId: TaskToDo.config.UserModuleID,
                    CultureCode: TaskToDo.config.CultureName,
                    secureToken: SageFrameSecureToken,
                    UserName: TaskToDo.config.UserName
                });
                $.ajax({
                    type: TaskToDo.config.type,
                    async: false,
                    url: TaskToDo.config.baseURL + "GetTaskContent",
                    data: myData,
                    contentType: TaskToDo.config.contentType,
                    dataType: TaskToDo.config.dataType,
                    success: function(data) {
                        if (data.d.length > 0) {
                            $.each(data.d, function(index, value) {
                                Data += '<div class="EditTaskWrapper" >';
                                Data += '<textarea id="EditNote" Name="txtEditNote" class="sfInputbox ">';
                                Data += value.Note;
                                Data += '</textarea>';
                                Data += ' <input type="text" id="EditDate" readonly="readonly" name="txtEditDate" class="sftxt txtDatepicker" value=' + value.Released + ' />';
                                Data += ' <i  class="sfLocale icon-update sfBtn" id="btnTaskListEdit">Update</i>';
                                Data += ' <i  class="sfLocale icon-close sfBtn  " id="btnTaskListCancel" >Cancel</i>';
                                $('#' + id).append(Data);
                                $('#' + id).find('.note').hide();
                                $('#EditDate').datepicker({
//                                    dateFormat: 'yy-mm-dd',
//                                    changeMonth: true,
//                                    changeYear: true,
//                                    yearRange: '-100:+0'

                                });
                            });
                        }
                    }
                });
            },
            DeleteTask: function(id) {
                TaskToDo.config.method = "DeleteTask";
                TaskToDo.config.url = TaskToDo.config.baseURL + TaskToDo.config.method;
                TaskToDo.config.data = JSON2.stringify({
                    Id: id,
                    UserName: TaskToDo.config.UserName,
                    PortalID: TaskToDo.config.PortalID,
                    UserModuleId: TaskToDo.config.UserModuleID,
                    CultureCode: TaskToDo.config.CultureName,
                    secureToken: SageFrameSecureToken
                });
                TaskToDo.config.ajaxCallMode = 2;
                TaskToDo.ajaxCall(TaskToDo.config);
            },
            GetTask: function() {
                TaskToDo.config.method = "GetTask";
                TaskToDo.config.url = TaskToDo.config.baseURL + TaskToDo.config.method;
                TaskToDo.config.data = JSON2.stringify({
                    CultureField: TaskToDo.config.CultureName,
                    PortalID: TaskToDo.config.PortalID,
                    UserModuleId: TaskToDo.config.UserModuleID,
                    offset: Offset,
                    UserName: TaskToDo.config.UserName,
                    str: str,
                    secureToken: SageFrameSecureToken,
                    SearchDate: $('#txtSearchDate').val()
                });
                TaskToDo.config.ajaxCallMode = 3;
                TaskToDo.ajaxCall(TaskToDo.config);
            },
            BindTask: function(data) {
                var Task = '';
                if (data.d.length > 0) {
                    $.each(data.d, function(index, value) {
                        var date1 = value.FullDate;
                        var date = date1.substring(0, 2);
                        var month = date1.substring(3, 5);
                        var year = date1.substring(6, 10);
                        var date = new Date(year, month - 1, date);
                        var today = new Date();
                        var yyyy = today.getFullYear().toString();
                        var mm = (today.getMonth() + 1).toString(); // getMonth() is zero-based
                        var dd = today.getDate().toString();
                        today = new Date(yyyy, mm - 1, dd);
                        var diff = date.getTime() - today.getTime();

                        Task += '<div class="tasktodo clearfix"  id="todaytask_' + value.TaskID + '">';
                        Task += '<div class="note"><span class="sftaskNote sflabel">';
                        Task += value.Note + '</span><span class="taskDate sflabel">';
                        if (diff == 0) {
                            Task += "Today";
                        }
                        else if (diff == -86400000) {
                            Task += "Yesterday";
                        }
                        else if (diff == 86400000) {
                            Task += "Tommorow";
                        }
                        else {
                            Task += value.Released;
                        }
                        Task += '</span>';
                        Task += '<i class="btnDelete icon-delete" ></i></div></div>';
                        TotalTask = value.Total;
                    });
                }
                else {
                    Task = "<h4 class='sfNoRecord'> Record not found. </h4>";
                };
                if ($('#txtSearchDate').val() != '') {
                    $('#dvSearchTaskList').html(Task);
                    $('#tabTaskList').hide();
                    $('#AddTaskTab').hide();
                    $('#BtnTaskBack').show();
                    $('#dvSearchTaskList').show();
                    $('#dvTask').html('');
                    $('#dvTask').hide();
                }
                else {
                    if (Offset == 0) {
                        $('#dvSearchTaskList').hide();
                        $('#dvSearchTaskList').html('');
                        $('#dvTask').html(Task);
                    } else {
                        $('#dvTask').append(Task);
                    }
                    Offset = Offset + 5;

                }
            }
        };
        TaskToDo.init();
    };
    $.fn.TaskEdit = function(p) {
        $.CreateTask(p);
    };
})(jQuery);