var AppointmentStatusMgmt = '';
var editFlag = 0;
$(function() {
    var AspxCommonObj = function() {
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            CultureName: AspxCommerce.utils.GetCultureName(),
            UserName: AspxCommerce.utils.GetUserName()
        };
        return aspxCommonObj;
    };
    AppointmentStatusMgmt = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: aspxservicePath + "AspxServiceItemsHandler.ashx/",
            url: "",
            method: "",
            ajaxCallMode: 0
        },
        ajaxCall: function(config) {
            $.ajax({
                type: AppointmentStatusMgmt.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: AppointmentStatusMgmt.config.contentType,
                cache: AppointmentStatusMgmt.config.cache,
                async: AppointmentStatusMgmt.config.async,
                data: AppointmentStatusMgmt.config.data,
                dataType: AppointmentStatusMgmt.config.dataType,
                url: AppointmentStatusMgmt.config.url,
                success: AppointmentStatusMgmt.ajaxSuccess,
                error: AppointmentStatusMgmt.ajaxFailure
            });
        },
        BindAppointmentStatusInGrid: function(appontmentStatusName, isActive) {
            this.config.method = "GetAppointmentStatusListGrid";
            this.config.url = this.config.baseURL;
            this.config.data = { aspxCommonObj: AspxCommonObj(), statusName: appontmentStatusName, isActive: isActive };
            var data = this.config.data;

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#tblAppointmentStatusDetails_pagesize").length > 0) ? $("#tblAppointmentStatusDetails_pagesize :selected").text() : 10;

            $("#tblAppointmentStatusDetails").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxBookAnAppointment, "Appointment Status ID"), name: 'AppointmentStatusID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', checkFor: '5', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox', hide: true },
                    { display: getLocale(AspxBookAnAppointment, "Appointment Status Name"), name: 'AppointmentStatus', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxBookAnAppointment, "Appointment Tool Tip"), name: 'AliasToolTip', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxBookAnAppointment, "In System"), name: 'IsSystemUsed', cssclass: '', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxBookAnAppointment, "IsActive"), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxBookAnAppointment, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [{ display: getLocale(AspxBookAnAppointment,'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'AppointmentStatusMgmt.EditAppointmentStatus', arguments: '1,2' }
               ],
                rp: perpage,
                nomsg: getLocale(AspxBookAnAppointment,"No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 5: { sorter: false} }
            });
        },
        EditAppointmentStatus: function(tblID, argus) {
            switch (tblID) {
                case "tblAppointmentStatusDetails":
                    editFlag = argus[0];
                    $('#divAppointmentStatusDetail').hide();
                    $('#divEditAppointmentStatus').show();
                   // $('#' + lblHeading).html(getLocale(AspxBookAnAppointment, "Edit Appointment Status: ") + "'" + argus[3] + "'");
                    $('#txtAppointmentStatusName').val(argus[3]);
                    $("#btnSaveAppointmentStatus").attr("name", argus[0]);
                    break;
                default:
                    break;
            }
        },
        SaveAppointmentStatus: function(AppointmentStatusID) {
            editFlag = AppointmentStatusID;
            var appointmentStatusName = $('#txtAppointmentStatusName').val();
            var SaveAppointmetStatusObj = {
                AppointmentStatusID: AppointmentStatusID,
                AppointmentStatusAliasName: appointmentStatusName,
                IsActive: true
            };
            this.config.method = "AddUpdateAppointmentStatus";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({
                aspxCommonObj: AspxCommonObj(),
                appointmentStatusObj: SaveAppointmetStatusObj
            });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        SearchAppointmentStatus: function() {
            var txtSearchStatusName = $.trim($("#txtSearchStatusName").val());
            if (txtSearchStatusName.length < 1) {
                txtSearchStatusName = null;
            }
            var isAct = $.trim($("#ddlStatusVisibitity").val()) == "" ? null : ($.trim($("#ddlStatusVisibitity").val()) == "True" ? true : false);
            AppointmentStatusMgmt.BindAppointmentStatusInGrid(txtSearchStatusName, isAct);
        },
        ClearForm: function() {
            $("#btnSaveAppointmentStatus").removeAttr("name");
            $('#txtSearchStatusName').val('');
            $('#txtAppointmentStatusName').val('');

            $('#txtAppointmentStatusName').removeClass('error');
            $('#txtAppointmentStatusName').parents('td').find('label').remove();
            $('#csErrorLabel').html('');
        },
        ajaxSuccess: function(data) {
            switch (AppointmentStatusMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    $("#divAppointmentStatusDetail").show();
                    $("#divEditAppointmentStatus").hide();
                    AppointmentStatusMgmt.BindAppointmentStatusInGrid(null, null);
                    AppointmentStatusMgmt.ClearForm();
                    if (editFlag > 0) {
                        csscody.info('<h2>' + getLocale(AspxBookAnAppointment, "Information Message") + '</h2><p>' + getLocale(AspxBookAnAppointment, "Appointment status has been updated successfully.") + '</p>');
                    } else {
                        csscody.info('<h2>' + getLocale(AspxBookAnAppointment, "Information Message") + '</h2><p>' + getLocale(AspxBookAnAppointment, "Appointment status has been saved successfully.") + '</p>');
                    }
                    break;
            }
        },
        ajaxFailure: function(data) {
            switch (AppointmentStatusMgmt.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>' + getLocale(AspxBookAnAppointment, 'Error Message') + '</h2><p>' + getLocale(AspxBookAnAppointment, "Failed to save appointment status") + '</p>');
                    break;
            }
        },
        Init: function() {
            AppointmentStatusMgmt.BindAppointmentStatusInGrid(null, true);
            $("#btnAppointmentBack").bind('click', function() {
                $("#divAppointmentStatusDetail").show();
                $("#divEditAppointmentStatus").hide();
                //$('#' + lblHeading).html(getLocale(AspxBookAnAppointment, "Appointment Status Management"));
            });
            $("#btnAppointmentBack").bind('click', function() {
                $("#divAppointmentStatusDetail").show();
                $("#divEditAppointmentStatus").hide();
            });
            $('#txtSearchStatusName,#ddlStatusVisibitity').keyup(function(event) {
                if (event.keyCode == 13) {
                    AppointmentStatusMgmt.SearchAppointmentStatus();
                }
            });

            $('#btnSaveAppointmentStatus').bind('click', function() {
                AspxCommerce.CheckSessionActive(AspxCommonObj());
                if (AspxCommerce.vars.IsAlive) {
                    var v = $("#form1").validate({
                        messages: {
                            StatusName: {
                                required: '*',
                                minlength: "*  " + getLocale(AspxBookAnAppointment, "(at least 2 chars)")
                            }
                        }
                    });

                    if (v.form()) {
                        var appointmentStatusID = $(this).attr("name");
                        AppointmentStatusMgmt.SaveAppointmentStatus(appointmentStatusID);
                    }
                } else {
                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + LoginURL + pageExtension;
                }
            });
        }
    };
    AppointmentStatusMgmt.Init();
});
