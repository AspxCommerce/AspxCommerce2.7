var buttonSafe = 0;
var AppointmentManage = "";
function parseJsonDate(jsonDate, id) {
    var dateArr = [];
    var jsonServiceDate = jsonDate;
    var serviceDate = new Date(parseInt(jsonServiceDate.replace("/Date(", '').replace(")/", ''), 10));
    var yearFrom = parseInt(serviceDate.getFullYear());
    dateArr.push(yearFrom);
    var monthFrom = parseInt((1 + serviceDate.getMonth()));
    dateArr.push(monthFrom);
    var daysFrom = parseInt(serviceDate.getDate());
    dateArr.push(daysFrom);
    dateArr.push(id);
    return dateArr;
}

var checkIntervalIsInDuration = function(intervalVal, bookedInterVal, x) {
    var boolval = false;
    var intervalValue = intervalVal.split('-');
    var intervalValueFrom = intervalValue[0].replace( /[^-0-9-:\.]+/g , '');
    var intervalValueTo = intervalValue[1].replace( /[^-0-9-:\.]+/g , '');

    var bookedIntervalValue = bookedInterVal.split('-');
    var bookedIntervalValueFrom = bookedIntervalValue[0].replace( /[^-0-9-:\.]+/g , '');
    var bookedIntervalValueTo = bookedIntervalValue[1].replace( /[^-0-9-:\.]+/g , '');
    if (x == 1) {
        if ((Date.parse("1/1/2013" + ' ' + intervalValueFrom) <= Date.parse("1/1/2013" + ' ' + bookedIntervalValueFrom)) &&
            (Date.parse("1/1/2013" + ' ' + intervalValueFrom) <= Date.parse("1/1/2013" + ' ' + bookedIntervalValueTo)) &&
                (Date.parse("1/1/2013" + ' ' + intervalValueTo) >= Date.parse("1/1/2013" + ' ' + bookedIntervalValueTo))) {
            boolval = true;
        } else if ((Date.parse("1/1/2013" + ' ' + intervalValueFrom) <= Date.parse("1/1/2013" + ' ' + bookedIntervalValueFrom)) &&
            (Date.parse("1/1/2013" + ' ' + intervalValueTo) >= Date.parse("1/1/2013" + ' ' + bookedIntervalValueFrom))) {
            boolval = true;
        }
    } else if (x == 0) {
        if ((Date.parse("1/1/2013" + ' ' + intervalValueFrom) <= Date.parse("1/1/2013" + ' ' + bookedIntervalValueFrom)) &&
            (Date.parse("1/1/2013" + ' ' + intervalValueTo) <= Date.parse("1/1/2013" + ' ' + bookedIntervalValueTo))) {
            if ((Date.parse("1/1/2013" + ' ' + intervalValueFrom) <= Date.parse("1/1/2013" + ' ' + bookedIntervalValueFrom)) &&
                (Date.parse("1/1/2013" + ' ' + intervalValueTo) <= Date.parse("1/1/2013" + ' ' + bookedIntervalValueFrom))) {
                boolval = false;
            } else {
                boolval = true;
            }
        } else if ((Date.parse("1/1/2013" + ' ' + intervalValueFrom) >= Date.parse("1/1/2013" + ' ' + bookedIntervalValueFrom)) &&
            (Date.parse("1/1/2013" + ' ' + intervalValueFrom) >= Date.parse("1/1/2013" + ' ' + bookedIntervalValueTo)) &&
                (Date.parse("1/1/2013" + ' ' + intervalValueTo) >= Date.parse("1/1/2013" + ' ' + bookedIntervalValueTo))) {
            boolval = false;

        } else if ((Date.parse("1/1/2013" + ' ' + intervalValueFrom) >= Date.parse("1/1/2013" + ' ' + bookedIntervalValueFrom)) &&
            (Date.parse("1/1/2013" + ' ' + intervalValueFrom) <= Date.parse("1/1/2013" + ' ' + bookedIntervalValueTo)) &&
                (Date.parse("1/1/2013" + ' ' + intervalValueTo) >= Date.parse("1/1/2013" + ' ' + bookedIntervalValueTo))) {
            boolval = true;

        } else if ((Date.parse("1/1/2013" + ' ' + intervalValueFrom) >= Date.parse("1/1/2013" + ' ' + bookedIntervalValueFrom)) &&
            (Date.parse("1/1/2013" + ' ' + intervalValueFrom) <= Date.parse("1/1/2013" + ' ' + bookedIntervalValueTo))) {
            if ((Date.parse("1/1/2013" + ' ' + intervalValueFrom) >= Date.parse("1/1/2013" + ' ' + bookedIntervalValueFrom)) &&
                (Date.parse("1/1/2013" + ' ' + intervalValueFrom) <= Date.parse("1/1/2013" + ' ' + bookedIntervalValueTo)) &&
                    (Date.parse("1/1/2013" + ' ' + intervalValueTo) >= Date.parse("1/1/2013" + ' ' + bookedIntervalValueTo))) {
                boolval = false;
            } else {
                boolval = true;
            }
        }
    }
    return boolval;
};
$(function() {
    var appointmentId = '';
    var isSystemUsed = false;
    var viewFlag = false;
    var serviceDateID = 0;
    var AspxCommonObj = function() {
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            CultureName: AspxCommerce.utils.GetCultureName(),
            UserName: AspxCommerce.utils.GetUserName()
        };
        return aspxCommonObj;
    };
    AppointmentManage = {
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
                type: AppointmentManage.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: AppointmentManage.config.contentType,
                cache: AppointmentManage.config.cache,
                async: AppointmentManage.config.async,
                data: AppointmentManage.config.data,
                dataType: AppointmentManage.config.dataType,
                url: AppointmentManage.config.url,
                success: AppointmentManage.ajaxSuccess,
                error: AppointmentManage.ajaxFailure
            });
        },
        vars: {
            OrderId: 0,
            PaymentMethodId: 0,
            ServiceCategoryId: 0,
            ServiceProductID: 0,
            UserName: ''
        },
        serviceData: {
            ServiceName: '',
            ServiceCategoryID: 0,
            ServiceProductName: '',
            ServiceProductID: 0,
            ServiceDuration: 0,
            ServiceProductPrice: 0,
            StoreLocationID: 0,
            StoreLocationName: '',
            ServiceProviderName: '',
            EmployeeID: 0,
            ServiceDate: '',
            ServiceDateID: 0,
            ProviderAvailability: '',
            PreferredTimeID:0,
            AppointmentBookTime: '',
            PaymentMethodName: '',
            PaymentMethodID: 0
        },
        LoadAppointmentAjaxImage: function() {
            $('#ajaxAppointmentImageLoad').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        ClearForm: function() {
            $("#ddlTitle").val(0);
            $("#txtFirstName").val('');
            $("#txtLastName").val('');
            $('input[name=gender]:checked').removeAttr('checked');
            $("#txtMobileNumber").val('');
            $("#txtPhoneNumber").val('');
            $("#txtEmailAddressAppointment").val('');
            $("#txtPreferredDate").val('');
            $("#ddlHour").val(00);
            $("#ddlMinute").val(00);
            $("#ddlAmPm").val("am");
            $("#txtTypeofTreatment").val('');
            $("#ddlStoreLocation").val(0);
            $('input[name=customerType]:checked').removeAttr('checked');
            $('input[name=membershipElite]:checked').removeAttr('checked');
        },
        GetDataForExport: function() {
            this.config.url = this.config.baseURL + "GetAppointmentDetailsForExport";
            this.config.data = JSON2.stringify({aspxCommonObj: AspxCommonObj() });
            this.config.ajaxCallMode = 11;
            this.ajaxCall(this.config);
        },
        ExportToCsvData: function() {
            AppointmentManage.GetDataForExport();
        },
        ExportDivDataToExcel: function() {
            AppointmentManage.GetDataForExport();
        },
        BindDataForExport: function(msg) {                 
            var exportData = '';
            exportData += '<thead><tr><th>Appointment ID</th><th>Order ID</th><th>Appointment Status</th><th>Store Branch</th><th>Service Name</th>';
            exportData += '<th>Provider Name</th><th>Product Name</th><th>Appointment Date</th><th>Provider Availability Time</th><th>Appointment Time</th><th>Service Duration</th>';
            exportData += '<th>Price</th><th>Payment Method</th><th>Title</th><th>Customer Name</th><th>Gender</th>';
            exportData += '<th>Email</th><th>Phone</th><th>Mobile</th><th>AddedOn</th>';
            exportData += '</tr></thead><tbody>';
            var length = msg.d.length;
            if (length > 0) {
                var value;
                for (var index = 0; index < length; index++) {
                    value = msg.d[index];
                    var date= 'new '+value.PreferredDate.replace( /[/]/gi , '');
                    date = eval(date);
                    date = formatDate(date, 'yyyy/MM/dd');
                    exportData += '<tr><td>' + value.AppointmentID + '</td><td>' + value.OrderID + '</td><td>' + value.AppointmentStatusName + '</td><td>' + value.StoreLocationName + '</td><td>' + value.ServiceCategoryName + '</td>';
                    exportData += '<td>'+ value.EmployeeName+'</td><td>' + value.ServiceProductName + '</td><td>' + date+ '</td><td>' + value.PreferredTime + '</td><td>' + value.PreferredTimeInterval + '</td><td>' + value.ServiceDuration + '</td>';
                    exportData += '<td>' + value.ServiceProductPrice + '</td><td>' + value.PaymentMethodName + '</td><td>' + value.Title + '</td><td>' + value.UserName + '</td><td>' + value.Gender + '</td>';
                    exportData += '<td>' + value.Email + '</td><td>' + value.PhoneNumber + '</td><td>' + value.MobileNumber + '</td><td>' + value.AddedOn + '</td></tr>';
                };
            } else {
                exportData += '<tr><td>'+getLocale(AspxBookAnAppointment,"No Records Found!")+'</td></tr>';
            }
            exportData += '</tbody>';

            $('#tblAppointmentExportData').html(exportData);
            $("input[id$='AppHdnValue']").val('<table>' + exportData + '</table>');
            $("input[id$='AppCsvHiddenValue']").val($("#tblAppointmentExportData").table2CSV());
            $("#tblAppointmentExportData").html('');
        },
        GetAppointmentStatusList: function() {
            this.config.url = this.config.baseURL + "GetAppointmentStatusList";
            this.config.data = JSON2.stringify({ aspxCommonObj: AspxCommonObj() });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },
        GetStoreProviderNameList: function(branchID) {
            this.config.url = this.config.baseURL + "GetBranchProviderNameList";
            this.config.data = JSON2.stringify({ offset: 1, limit: null, storeBranchId: branchID, aspxCommonObj: AspxCommonObj() });
            this.config.ajaxCallMode = 6;
            this.ajaxCall(this.config);
        },
        BindAppointmentList: function(userName, appointmentStatusName, branchName, employeeName) {
            var commonInfo = AspxCommonObj();
            commonInfo.UserName = userName;
            AppointmentManage.config.method = "GetBookAppointmentList";
            AppointmentManage.config.url = AppointmentManage.config.baseURL;
            AppointmentManage.config.data = { aspxCommonObj: commonInfo, appointmentStatusName: appointmentStatusName, branchName: branchName, employeeName: employeeName };
            var data = AppointmentManage.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#tblBookAppointment_pagesize").length > 0) ? $("#tblBookAppointment_pagesize :selected").text() : 10;

            $("#tblBookAppointment").sagegrid({
                url: AppointmentManage.config.url,
                functionMethod: AppointmentManage.config.method,
                colModel: [
                    { display: getLocale(AspxBookAnAppointment, "AppointmentID"), name: 'AppointmentID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'AppointmentChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxBookAnAppointment, "Order ID"), name: 'OrderID', cssclass: '', coltype: 'label', align: 'center', hide: true },
                    { display: getLocale(AspxBookAnAppointment, "Appointment Status"), name: 'AppointmentStatusAliasName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxBookAnAppointment, "Customer Name"), name: 'UserName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxBookAnAppointment, "Store Name"), name: 'StoreLocationName', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxBookAnAppointment, "Service Provider"), name: 'EmployeeName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxBookAnAppointment, "Appointment Time"), name: 'PreferredTimeInterval', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxBookAnAppointment, "Product Name"), name: 'ServiceProductName', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxBookAnAppointment, "Price"), name: 'ServiceProductPrice', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'left' },
                    { display: getLocale(AspxBookAnAppointment, "Added On"), name: 'AddedOn', cssclass: '', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxBookAnAppointment, "Actions"), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxBookAnAppointment,'View'), name: 'view', enable: true, _event: 'click', trigger: '1', callMethod: 'AppointmentManage.ViewAppointment', arguments: '1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,25,28,30,32' },
                    { display: getLocale(AspxBookAnAppointment,'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'AppointmentManage.DeleteAppointment', arguments: '1' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxBookAnAppointment,"No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 10: { sorter: false } }
            });

            $(document).ajaxStop(function () {
                if (buttonSafe == 0) {
                    $("input[value='Export to Excel']").hide();
                    $("input[value='Export to CSV']").hide();
                }
                

                if($('#tblBookAppointment').find('tbody tr').children().length > 1 && $('#tblBookAppointment').find('tbody tr').find('td').text() != getLocale(AspxBookAnAppointment,"No Records Found!")){
                    $("input[value='Export to Excel']").show();    
                    $("input[value='Export to CSV']").show();
                    buttonSafe = 1;    
                }
            });
        },
        ViewAppointment: function(tblID, argus) {
            switch (tblID) {
            case 'tblBookAppointment':
                viewFlag = true;
                AppointmentManage.GetAppointmentDetailByID(argus[0]);
                break;
            default:
                break;
            }
        },
        DeleteAppointment: function(tblID, argus) {
            switch (tblID) {
            case 'tblBookAppointment':
                var appointmentID = argus[0];
                var properties = {
                    onComplete: function(e) {
                        AppointmentManage.DeleteAppointmentByID(appointmentID, e);
                    }
                };
                csscody.confirm("<h2>"+getLocale(AspxBookAnAppointment,'Delete Confirmation')+"</h2><p>"+getLocale(AspxBookAnAppointment,'Are you sure you want to delete this appointment?')+"</p>", properties);
                break;
            default:
                break;
            }
        },
        ResetData: function() {
            $('#txtServiceName').val(AppointmentManage.serviceData.ServiceName);
            $('#ServiceCategoryID').val(AppointmentManage.serviceData.ServiceCategoryID);
            $('#txtProductName').val(AppointmentManage.serviceData.ServiceProductName);
            $('#ServiceProductID').val(AppointmentManage.serviceData.ServiceProductID);
            $('#txtServiceDuration').val(AppointmentManage.serviceData.ServiceDuration + ' ' + 'minutes');
            $('#txtPrice').val(AppointmentManage.serviceData.ServiceProductPrice);
            $('#txtPaymentMethod').val(AppointmentManage.serviceData.PaymentMethodName);
            $("#ddlStoreLocation").val(AppointmentManage.serviceData.StoreLocationID);
            $('#ddlStoreLocation').trigger('change');
            $("#ddlStoreServiceProvider").val(AppointmentManage.serviceData.EmployeeID);
            $("#txtPreferredDate").val(formatDate(AppointmentManage.serviceData.ServiceDate, "MM/dd/yyyy"));
            $("#txtPreferredTime").val(AppointmentManage.serviceData.ProviderAvailability);
            $('#txtPreferredTimeInterval').val(AppointmentManage.serviceData.AppointmentBookTime);

            $('.cssClassTimeList').hide();
            $('#txtPreferredTime').show();
            $('#btnResetData').hide();
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        },
        GetAppointmentDetailByID: function(appointmentID) {
            this.config.url = this.config.baseURL + "GetAppointmentDetailByID";
            this.config.data = JSON2.stringify({ appointmentID: appointmentID, aspxCommonObj: AspxCommonObj() });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        BindAppointmentDetail: function(msg) {

            $("#appointmentMainDiv").show();
            $("#divBookAppointManage").hide();
            var item;
            var length = msg.d.length;
            for (var index = 0; index < length; index++) {
                item = msg.d[index];
                appointmentId = item.AppointmentID;
                AppointmentManage.vars.OrderId = item.OrderID;
                AppointmentManage.vars.ServiceCategoryId = item.ServiceCategoryID;
                AppointmentManage.vars.ServiceProductID = item.ServiceProductID;
                AppointmentManage.vars.UserName = item.UserName;
                               $('#spanOrderID').html(item.OrderID);

                $('#txtServiceName').val(item.ServiceCategoryName);
                $('#ServiceCategoryID').val(item.ServiceCategoryID);
                $('#txtProductName').val(item.ServiceProductName);
                $('#ServiceProductID').val(item.ServiceProductID);
                $('#txtServiceDuration').val(item.ServiceDuration + ' ' + 'minutes');
                $('#txtPrice').val(item.ServiceProductPrice);
                $('#txtPaymentMethod').val(item.PaymentMethodName);

                $("#ddlTitle option:selected").text(item.Title);
                $("#ddlAppointmentStatusList").val(item.AppointmentStatusID);
                $("#txtFirstName").val(item.FirstName);
                $("#txtLastName").val(item.LastName);
                $('#chkGender label').each(function() {
                    var x = $(this).find('input[name=gender]').val();
                    if ((item.Gender).toLowerCase() == x) {
                        $(this).find('input[name=gender]').prop('checked', 'checked');
                    }
                });
                $("#txtMobileNumber").val(item.MobileNumber);
                $("#txtPhoneNumber").val(item.PhoneNumber);
                $("#txtEmailAddressAppointment").val(item.Email);
                var date = 'new ' + item.PreferredDate.replace( /[/]/gi , '');
                date = eval(date);
                $("#txtPreferredDate").val(formatDate(date, "MM/dd/yyyy"));
                $("#txtPreferredTime").val(item.PreferredTime);
                $('#txtPreferredTimeInterval').val(item.PreferredTimeInterval);
                $("#txtTypeofTreatment").val(item.TypeOfTreatment);
                $("#ddlStoreLocation").val(item.StoreLocation);

                AppointmentManage.GetStoreProviderNameList(item.StoreLocation);
                $("#ddlStoreServiceProvider").val(item.EmployeeID);
                $('#customerType label').each(function() {
                    var y = $(this).find('input[name=customerType]').val();
                    if ((item.TypeOfCustomer).toLowerCase() == y) {
                        $(this).find('input[name=customerType]').prop('checked', 'checked');
                    }
                });
                $('#membershipElite label').each(function() {
                    var x = $(this).find('input[name=membershipElite]').val();
                    if ((item.MembershipElite).toLowerCase() == x) {
                        $(this).find('input[name=membershipElite]').prop('checked', 'checked');
                    }
                });
                $('.cssClassDisable').prop('disabled', 'disabled');
                               $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });


                AppointmentManage.serviceData.ServiceName = item.ServiceCategoryName;
                AppointmentManage.serviceData.ServiceCategoryID = item.ServiceCategoryID;
                AppointmentManage.serviceData.ServiceProductName = item.ServiceProductName;
                AppointmentManage.serviceData.ServiceProductID = item.ServiceProductID;
                AppointmentManage.serviceData.ServiceDuration = item.ServiceDuration;
                AppointmentManage.serviceData.ServiceProductPrice = item.ServiceProductPrice;
                AppointmentManage.serviceData.StoreLocationID = item.StoreLocation;
                AppointmentManage.serviceData.StoreLocationName = item.StoreLocationName;
                AppointmentManage.serviceData.ServiceProviderName = item.EmployeeName;
                AppointmentManage.serviceData.EmployeeID = item.EmployeeID;
                AppointmentManage.serviceData.ServiceDate = date;
                AppointmentManage.serviceData.ServiceDateID = item.ServiceDateId;
                AppointmentManage.serviceData.ProviderAvailability = item.PreferredTime;
                AppointmentManage.serviceData.PreferredTimeID = item.PreferredTimeId;
                AppointmentManage.serviceData.AppointmentBookTime = item.PreferredTimeInterval;
                AppointmentManage.serviceData.PaymentMethodName = item.PaymentMethodName;
                AppointmentManage.serviceData.PaymentMethodID = item.PaymentMethodID;
                serviceDateID = item.ServiceDateId;   
                AppointmentManage.GetServicesDates(item.ServiceCategoryID, item.StoreLocation, item.EmployeeID);
              
            };
        },

        DeleteAppointmentByID: function(appointmentID, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeleteAppointment";
                this.config.data = JSON2.stringify({ appointmentID: appointmentID, aspxCommonObj: AspxCommonObj() });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            } else {
                return false;
            }
        },
        GetStoreLocation: function() {
            this.config.url = this.config.baseURL + "GetAllStoresForService";
            this.config.data = JSON2.stringify({ aspxCommonObj: AspxCommonObj(), serviceCategoryId: null });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },

        SaveUpateAppointment: function() {
            var preferredTime = '';
            if ($("#txtPreferredTime:visible").length > 0) {
                preferredTime = $.trim($("#txtPreferredTime").val());
            } else {
                var timeFrom = '';
                var timeTo = '';
                $('#idServiceTimeLi li').each(function() {
                    if ($(this).find('input[name=chkServiceTime]').prop('checked')) {
                        timeFrom = $(this).find('.cssClassTimeFrom span').html();
                        timeTo = $(this).find('.cssClassTimeTo span').html();
                    }
                });
                preferredTime = timeFrom + '-' + timeTo;
            }

            var appointmentInfo = {
                infoData: {
                    RowTotal: 0,
                    AppointmentID: appointmentId,
                    OrderID: AppointmentManage.vars.OrderId,
                    AppointmentStatusID: $("#ddlAppointmentStatusList option:selected").val(),
                    AppointmentStatusName: $("#ddlAppointmentStatusList option:selected").text(),
                    IsSystemUsed: Boolean(isSystemUsed),
                    Title: $.trim($("#ddlTitle option:selected").text()),//$("#ddlTitle option:selected").text().slice(0, $(this).val().length - 1),
                    UserName: AspxCommonObj().UserName,
                    FirstName: $.trim($("#txtFirstName").val()),
                    LastName: $.trim($("#txtLastName").val()),
                    Gender: $.trim($('input[name=gender]:checked').val()),
                    PhoneNumber: $.trim($("#txtPhoneNumber").val()),
                    MobileNumber: $.trim($("#txtMobileNumber").val()),
                    Email: $.trim($("#txtEmailAddressAppointment").val()),
                    PreferredDate: $.trim($("#txtPreferredDate").val()),
                    PreferredTime: preferredTime,
                    TypeOfTreatment: $.trim($("#txtTypeofTreatment").val()),
                    StoreLocation: $.trim($("#ddlStoreLocation option:selected").val()),
                    TypeOfCustomer: $.trim($('input[name=customerType]:checked').val()),
                    MembershipElite: $.trim($('input[name=membershipElite]:checked').val()),
                    ServiceCategoryID: AppointmentManage.vars.ServiceCategoryId,
                    ServiceCategoryName: $.trim($('#txtServiceName').val()),
                    ServiceProductID: AppointmentManage.vars.ServiceProductID,
                    ServiceProductName: $.trim($('#txtProductName').val()),
                    ServiceProductPrice: $.trim($('#txtPrice').val().replace( /[^-0-9\.]+/g , "")),
                    PaymentMethodID: AppointmentManage.vars.PaymentMethodId,
                    PaymentMethodName: $.trim($('#txtPaymentMethod').val()),
                    PreferredTimeInterval: $.trim($('#txtPreferredTimeInterval').val()),
                    ServiceDuration: $.trim($('#txtServiceDuration').val()),
                    EmployeeName: $.trim($("#ddlStoreServiceProvider option:selected").text()),                    EmployeeID: $('#ddlStoreServiceProvider option:selected').val()
                }
            };
            var commonInfo = AspxCommonObj();
                       if ($("#ddlAppointmentStatusList option:selected").val() != 0) {
                this.config.url = this.config.baseURL + "SaveBookAppointment";
                this.config.data = JSON2.stringify({ appointmentId: appointmentId, aspxCommonObj: commonInfo, obj: appointmentInfo.infoData });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            } else {
                $("#ddlAppointmentStatusList").addClass('sfListmenu error');
                return false;
            }
        },

        GetServicesDates: function(serviceId, branchId, empId) {
            var commonInfo = AspxCommonObj();
            commonInfo.UserName = null;
            var getServiceDateInfo = {
                ServiceID: serviceId,
                BranchID: branchId,
                EmployeeID: empId
            };
            this.config.url = this.config.baseURL + "GetServiceDates";
            this.config.data = JSON2.stringify({ getServiceDateObj: getServiceDateInfo, aspxCommonObj: commonInfo });
            this.config.ajaxCallMode = 7;
            this.ajaxCall(this.config);
        },
        BindServiceDates: function(msg) {
            var dates = [];
            var length = msg.d.length;
            if (length > 0) {
                var value;
                for (var index = 0; index < length; index++) {
                    value = msg.d[index];
                    if (index == 0) {
                        if (value.ServiceDateFrom != null && value.ServiceDateTo != null) {
                            var dateFrom = [];
                            var serviceDateFrm = value.ServiceDateFrom;
                            var serviceDateFrom = new Date(parseInt(serviceDateFrm.replace("/Date(", '').replace(")/", ''), 10));
                            var yearFrom = parseInt(serviceDateFrom.getFullYear());
                            dateFrom.push(yearFrom);
                            var monthFrom = parseInt((1 + serviceDateFrom.getMonth()));
                            dateFrom.push(monthFrom);
                            var daysFrom = parseInt(serviceDateFrom.getDate());
                            dateFrom.push(daysFrom);
                            dateFrom.push(value.ServiceDateID);
                            dates.push(dateFrom);
                            var dateTo = [];
                            var serviceDate2 = value.ServiceDateTo;
                            var serviceDateTo = new Date(parseInt(serviceDate2.replace("/Date(", '').replace(")/", ''), 10));
                            var yearTo = parseInt(serviceDateTo.getFullYear());
                            dateTo.push(yearTo);
                            var monthTo = parseInt((1 + serviceDateTo.getMonth()));
                            dateTo.push(monthTo);
                            var daysTo = parseInt(serviceDateTo.getDate());
                            dateTo.push(daysTo);
                            dateTo.push(value.ServiceDateID);
                            dates.push(dateTo);
                            for (var i = 1; i < (daysTo - daysFrom) ; i++) {
                                var dateRange = [];
                                var dateRangeFrm = value.ServiceDateFrom;
                                var dateRangeFrom = new Date(parseInt(dateRangeFrm.replace("/Date(", '').replace(")/", ''), 10));
                                var dateRangeyearFrom = parseInt(dateRangeFrom.getFullYear());
                                dateRange.push(dateRangeyearFrom);
                                var dateRangemonthFrom = parseInt((1 + dateRangeFrom.getMonth()));
                                dateRange.push(dateRangemonthFrom);
                                var dateRangedaysFrom = parseInt(dateRangeFrom.getDate());
                                dateRange.push(dateRangedaysFrom + i);
                                dateRange.push(value.ServiceDateID);
                                dates.push(dateRange);
                            }
                        } else if (value.ServiceDateFrom == null || value.ServiceDateTo == null) {
                            if (value.ServiceDateFrom != null) {
                                dates.push(parseJsonDate(value.ServiceDateFrom, value.ServiceDateID));
                            }
                            if (value.ServiceDateTo != null) {
                                dates.push(parseJsonDate(value.ServiceDateTo, value.ServiceDateID));
                            }
                        }
                    } else {
                        if (value.ServiceDateFrom != null && value.ServiceDateTo != null) {
                            var dateFromIndex = [];
                            var serviceDateFrmIndex = value.ServiceDateFrom;
                            var serviceDateFromIndex = new Date(parseInt(serviceDateFrmIndex.replace("/Date(", '').replace(")/", ''), 10));
                            var yearFromIndex = parseInt(serviceDateFromIndex.getFullYear());
                            dateFromIndex.push(yearFromIndex);
                            var monthFromIndex = parseInt((1 + serviceDateFromIndex.getMonth()));
                            dateFromIndex.push(monthFromIndex);
                            var daysFromIndex = parseInt(serviceDateFromIndex.getDate());
                            dateFromIndex.push(daysFromIndex);
                            dateFromIndex.push(value.ServiceDateID);
                                                       for (var a = 0; a < dates.length; a++) {
                                if (dates[a][0] == dateFromIndex[0] && dates[a][1] == dateFromIndex[1] && dates[a][2] == dateFromIndex[2]) {
                                    var xx = dates[a][3] + '-' + dateFromIndex[3];
                                    dates[a][3] = xx;
                                }
                            }
                            dates.push(dateFromIndex);
                            var dateToIndex = [];
                            var serviceDate2Index = value.ServiceDateTo;
                            var serviceDateToIndex = new Date(parseInt(serviceDate2Index.replace("/Date(", '').replace(")/", ''), 10));
                            var yearToIndex = parseInt(serviceDateToIndex.getFullYear());
                            dateToIndex.push(yearToIndex);
                            var monthToIndex = parseInt((1 + serviceDateToIndex.getMonth()));
                            dateToIndex.push(monthToIndex);
                            var daysToIndex = parseInt(serviceDateToIndex.getDate());
                            dateToIndex.push(daysToIndex);
                            dateToIndex.push(value.ServiceDateID);
                           
                            for (var b = 0; b < dates.length; b++) {
                                if (dates[b][0] == dateToIndex[0] && dates[b][1] == dateToIndex[1] && dates[b][2] == dateToIndex[2]) {
                                    var yy = dates[b][3] + '-' + dateToIndex[3];
                                    dates[b][3] = yy;
                                }
                            }
                            dates.push(dateToIndex);
                                                       for (var ii = 1; ii < (daysToIndex - daysFromIndex); ii++) {
                                var dateRangeIndex = [];
                                var dateRangeFrmIndex = value.ServiceDateFrom;
                                var dateRangeFromIndex = new Date(parseInt(dateRangeFrmIndex.replace("/Date(", '').replace(")/", ''), 10));
                                var dateRangeyearFromIndex = parseInt(dateRangeFromIndex.getFullYear());
                                dateRangeIndex.push(dateRangeyearFromIndex);
                                var dateRangemonthFromIndex = parseInt((1 + dateRangeFromIndex.getMonth()));
                                dateRangeIndex.push(dateRangemonthFromIndex);
                                var dateRangedaysFromIndex = parseInt(dateRangeFromIndex.getDate());
                                dateRangeIndex.push(dateRangedaysFromIndex + ii);
                                dateRangeIndex.push(value.ServiceDateID);
                                                               for (var c = 0; c < dates.length; c++) {
                                    if (dates[c][0] == dateRangeIndex[0] && dates[c][1] == dateRangeIndex[1] && dates[c][2] == dateRangeIndex[2]) {
                                        var zz = dates[c][3] + '-' + dateRangeIndex[3];
                                        dates[c][3] = zz;
                                    }
                                }
                                dates.push(dateRangeIndex);
                            }
                        } else if (value.ServiceDateFrom == null || value.ServiceDateTo == null) {
                            if (value.ServiceDateFrom != null) {
                                dates.push(parseJsonDate(value.ServiceDateFrom, value.ServiceDateID));
                            }
                            if (value.ServiceDateTo != null) {
                                dates.push(parseJsonDate(value.ServiceDateTo, value.ServiceDateID));
                            }
                        }
                    }                  
                };
                $('#txtPreferredDate').removeClass('hasDatepicker').datepicker(
                    {
                        dateFormat: 'mm/dd/yy',
                        changeMonth: true,
                        changeYear: true,
                        minDate: 0,
                        beforeShowDay: function(date) {
                            var year = date.getFullYear();
                            var month = date.getMonth();
                            var day = date.getDate();
                           
                            for (var i = 0; i < dates.length; i++)
                                
                            if (year == dates[i][0] && month == dates[i][1] - 1 && day == dates[i][2]) {
                                // return (true, "ui-state-highlight ui-state-active serviceId_" + dates[i][3] + "", dates[i][3]);
                                return [true, "ui-state-highlight ui-state-active serviceId_" + dates[i][3] + ""];
                                 }
                            return false;
                        },
                        onSelect: function (date) {
                            var dateVal = date.split('/');
                            var datevalue = 0;
                            if (dateVal[1].legth == 1) {
                                datevalue = '0' + dateVal[1];
                            } else {
                                datevalue = dateVal[1];
                            }
                            $('.cssClassTimeList').show();
                            $('#txtPreferredTime').hide();
                            $('#lblAvailableDateError').hide();
                            $('.ui-datepicker-calendar > tbody tr').each(function () {
                                $(this).find('td a').each(function () {
                                    if (parseInt($(this).html()) == datevalue) {
                                        serviceDateID = $(this).parent('td:eq(0)').attr('title');
                                    }
                                });
                            });
                            AppointmentManage.GetServiceAvailableTime();
                        }
                    });
            } else {
                $('#txtPreferredDate').removeClass('hasDatepicker').datepicker(
                    {
                        dateFormat: 'mm/dd/yy',
                        changeMonth: true,
                        changeYear: true,
                        minDate: 0,
                        beforeShowDay: function(date) {
                            var year = date.getFullYear(), month = date.getMonth(), day = date.getDate();
                                                       for (var i = 0; i < dates.length; i++)
                                if (year == dates[i][0] && month == dates[i][1] - 1 && day == dates[i][2]) {
                                                                       return [true, "ui-state-highlight ui-state-active serviceId_" + dates[i][3] + ""];
                                }
                            return [false];
                        },
                        onSelect: function(date) {
                            var dateVal = date.split('/');
                            var datevalue = 0;
                            if (dateVal[1].legth == 1) {
                                datevalue = '0' + dateVal[1];
                            } else {
                                datevalue = dateVal[1];
                            }
                            $('.cssClassTimeList').show();
                            $('#txtPreferredTime').hide();
                            $('#lblAvailableDateError').hide();
                            $('.ui-datepicker-calendar > tbody tr').each(function() {
                                $(this).find('td a').each(function() {
                                    if (parseInt($(this).html()) == datevalue) {
                                        serviceDateID = $(this).parent('td:eq(0)').attr('title');
                                    }
                                });
                            });
                            AppointmentManage.GetServiceAvailableTime();
                        }
                    });
            }          
        },
        GetServiceAvailableTime: function() {
            var commonInfo = AspxCommonObj();
            commonInfo.UserName = null;           
            var getServiceAvailbalbeTimeObj = {
                CategoryID: $.trim($('#ServiceCategoryID').val()),
                BranchID: $.trim($('#ddlStoreLocation option:selected').val()),
                EmployeeID: $('#ddlStoreServiceProvider option:selected').val(),
                ServiceDateID: AppointmentManage.serviceData.ServiceDateID,
                ServiceDate: $.trim($('#txtPreferredDate').val()),
                ItemID: $('#ServiceProductID').val()
            };           
            this.config.url = this.config.baseURL + "GetServiceAvailableTime";
            this.config.data = JSON2.stringify({ getServiceTimeObj: getServiceAvailbalbeTimeObj, aspxCommonObj: commonInfo });
            this.config.ajaxCallMode = 8;
            this.ajaxCall(this.config);
        },

        BindServiceTime: function(data) {
            if (data.d.length > 0) {
                var serviceTimeHeader = '';
                var serviceTime = '';
                serviceTimeHeader += '<li class="cssServiceTime"><label class="cssClassHeaderTimeFrom cssClassLabel">From</label>&nbsp;&nbsp;&nbsp;<label class="cssClassHeaderTimeTo cssClassLabel">To</label></li>';
                $.each(data.d, function(index, value) {
                    if (value.ServiceTimeFrom == '' && value.ServiceTimeTo == '') {
                        serviceTimeHeader = '';
                        serviceTimeHeader = '<li>Time is not allocated!</li>';
                    } else {
                        serviceTime += '<li class="cssServiceTime cssClassDuration_' + value.ServiceTimeID + '">';
                        serviceTime += '<input type="checkbox" name="chkServiceTime" value=' + value.ServiceTimeID + '>';
                        serviceTime += '<label class="cssClassTimeFrom"><span>' + value.ServiceTimeFrom + '</span></label>';
                        serviceTime += '&nbsp;&nbsp;&nbsp;<label class="cssClassTimeTo"><span>' + value.ServiceTimeTo + '</span></label>';
                        serviceTime += '<div class="cssClassServiceDuration"><ul class="cssClassServiceDurationUl_' + value.ServiceTimeID + '">';
                        serviceTime += '<li class="cssClassServiceDurationLi"></li></ul></div>';
                        serviceTime += '</li>';
                    }
                });
                serviceTimeHeader += serviceTime;

                $('.cssClassUlTimeList').html('').append(serviceTimeHeader);
                $('.cssClassAvailableTime').show();
                var container = $('#idServiceTimeLi');
                container.find('input[name=chkServiceTime]').click(function(e) {
                    if (this.checked) {
                        container.find('input[name=chkServiceTime]').not(this).removeAttr('checked');
                        $("#lblTimeError").html('');
                        var $obj = $(this).parent('li').parent('ul');
                        $obj.find('.cssServiceTime').each(function() {
                            if ($(this).find('input[name=chkServiceTime]').prop('checked') == true) {
                            } else if ($(this).find('input[name=chkServiceTime]').prop('checked') == false) {
                                $(this).find('.cssClassServiceDuration .cssClassServiceDurationLi').animate({
                                    width: ['toggle', 'swing'],
                                    height: ['toggle', 'swing'],
                                    opacity: 'toggle'
                                }, 1000);
                            } else {
                            }
                        });
                    } else {
                        $('.cssClassServiceDurationUl_' + $(this).val()).html('');
                    }
                });

                $('input[name=chkServiceTime]').on('click', function () {                   
                    if ($('input[name=chkServiceTime]:checked').length > 0) {
                        var x = $(this).parent('li').attr('class');
                        x = x.split('_');
                        var timeId = x[1];                      
                        AppointmentManage.GetBookedServiceTime(timeId);
                    } else {
                        $('#txtPreferredTimeInterval').val('');
                    }

                });
            } else {
                $('.cssClassAvailableTime').show();
                $('.cssClassUlTimeList').html('').html('<li>'+getLocale(AspxBookAnAppointment,"Time is not allocated!")+'</li>');
            }
        },
        GetBookedServiceTime: function(timeId) {
            var commonInfo = AspxCommonObj();
            commonInfo.UserName = null;
            var getServiceBookedTimeObj = {
                ServiceCategoryID: $.trim($('#ServiceCategoryID').val()),
                BranchID: $.trim($('#ddlStoreLocation option:selected').val()),
                EmployeeID: $('#ddlStoreServiceProvider option:selected').val(),
                ServiceDateID: AppointmentManage.serviceData.ServiceDateID,
                ServiceDate: $.trim($('#txtPreferredDate').val()),
                ServiceTimeID: timeId,
                ItemID: $('#ServiceProductID').val()
            };

            this.config.url = this.config.baseURL + "GetServiceBookedTime";
            this.config.data = JSON2.stringify({ bookedTimeObj: getServiceBookedTimeObj, aspxCommonObj: commonInfo });
            this.config.ajaxCallMode = 9;
            this.ajaxCall(this.config);
        },
        BindServiceIntervalTime: function(id, timeFrom, timeTo) {
            var sD = '';
            timeFrom = timeFrom.replace('AM', ' AM').replace('PM', ' PM');
            var amPmFrom = timeFrom.match( /[a-z]/gi ).join('');
            var amPmTo = timeTo.match( /[a-z]/gi ).join('');
            var x1 = Date.parse("1/1/2013" + ' ' + timeFrom);
            var x2 = Date.parse("1/1/2013" + ' ' + timeTo);
            if (x2 > x1) {
                sD = ((x2 - x1) / 1000 / 60);
            } else {              
                csscody.alert('<h2>' + getLocale(AspxBookAnAppointment, "Information Alert") + '</h2><p>' + getLocale(AspxBookAnAppointment, "Valid time duration does not exist.") + '</p>');
                return false;
            }
            var duration = parseInt($.trim($('#txtServiceDuration').val().replace( /[^-0-9\.]+/g , '')));
            var tF = timeFrom.replace( /[^-0-9-:\.]+/g , '');
            tF = tF.split( /:/ );
            var tT = timeTo.replace( /[^-0-9-:\.]+/g , '');
            tT = tT.split( /:/ );
                       var count = Math.floor(sD / duration);
            var serviceAvailableTime = '';
            var interval = [];
            var tfInitial = tF[0];
            var ttInitial = tT[0];
            for (var i = 0; i < count; i++) {
                var intervalFrom = '';
                var intervalTo = '';
                serviceAvailableTime += '<li class="cssClassServiceDurationLi">';
                serviceAvailableTime += '<input type="checkbox" name="chkServiceTimeInterval" value=' + i + '>';
                if (JSON2.stringify(duration * i).length == 1) {
                    intervalFrom = tF[0] + ':' + 0 + duration * i;
                    serviceAvailableTime += '<label class="cssClassServiceTimeFrom"><span>' + intervalFrom + ' ' + amPmFrom + '</span></label>';

                } else {
                    if (duration * i >= 60) {
                        var hrs = Math.floor((duration * i) / 60);
                        tF[0] = parseInt(tfInitial) + hrs;
                        if (tF[0] >= 12) {
                            amPmFrom = amPmTo;
                        }
                        if (tF[0] > 12) {
                            tF[0] = tF[0] % 12;
                        }
                        var mins = (duration * i) % 60;
                        if (JSON2.stringify(mins).length == 1) {
                            intervalFrom = tF[0] + ':' + '0' + '0';
                        } else {
                            intervalFrom = tF[0] + ':' + mins;
                        }
                    } else {
                        intervalFrom = tF[0] + ':' + duration * i;
                    }
                    serviceAvailableTime += '<label class="cssClassServiceTimeFrom"><span>' + intervalFrom + ' ' + amPmFrom + '</span></label>';

                }
                if (duration * (i + 1) >= 60) {
                    var hrsT = Math.floor((duration * (i + 1)) / 60);
                    tF[0] = parseInt(tfInitial) + hrsT;
                    if (tF[0] >= 12) {
                        amPmFrom = amPmTo;
                    }
                    if (tF[0] > 12) {
                        tF[0] = tF[0] % 12;
                    }
                    var minsT = (duration * (i + 1)) % 60;
                    if (JSON2.stringify(minsT).length == 1) {
                        intervalTo = tF[0] + ':' + '0' + '0';
                    } else {
                        intervalTo = tF[0] + ':' + minsT;
                    }
                    serviceAvailableTime += '&nbsp;&nbsp;&nbsp;<label class="cssClassServiceTimeTo"><span>' + intervalTo + ' ' + amPmFrom + '</span></label>';
                } else {
                    intervalTo = tF[0] + ':' + duration * (i + 1);
                    serviceAvailableTime += '&nbsp;&nbsp;&nbsp;<label class="cssClassServiceTimeTo"><span>' + intervalTo + ' ' + amPmFrom + '</span></label>';

                }
                serviceAvailableTime += '<input type="hidden" value="' + intervalFrom + ' ' + amPmFrom + '-' + intervalTo + ' ' + amPmFrom + '"/><span class="cssServiceBooked"></span></li>';

                interval.push(intervalFrom + ' ' + amPmFrom + '-' + intervalTo + ' ' + amPmFrom);
            }
            if (serviceAvailableTime != '') {
                $(".cssClassServiceDurationUl_" + id).html('').append(serviceAvailableTime);
            } else {
                $(".cssClassServiceDurationUl_" + id).addClass('sfIntervalNotAvailable').html('Not Available');
            }            

            var timeIntervalContainer = $(".cssClassServiceDurationUl_" + id);
            timeIntervalContainer.find('input[name=chkServiceTimeInterval]').click(function () {               
                if (this.checked) {
                    timeIntervalContainer.find('input[name=chkServiceTimeInterval]').not(this).removeAttr('checked');
                    var preferedTime = timeIntervalContainer.find('input[name=chkServiceTimeInterval]').parent('li').find('input[type=hidden]').val();
                
                    $("#txtPreferredTimeInterval").val(preferedTime);
                } else {
                }
            });          
            return interval;
        },
        BindServiceBookedTime: function(msg) {
            var timeFrom = '';
            var timeTo = '';
            var preferredTimeId = '';
            $('#idServiceTimeLi li').each(function() {
                if ($(this).find('input[name=chkServiceTime]').prop('checked')) {
                    timeFrom = $(this).find('.cssClassTimeFrom span').html();
                    timeTo = $(this).find('.cssClassTimeTo span').html();
                    preferredTimeId = $(this).find('input[name=chkServiceTime]').val();
                }
            });
            var length = msg.d.length;
            if (length > 0) {
                var intervalArr = AppointmentManage.BindServiceIntervalTime(preferredTimeId, timeFrom, timeTo);
                if (intervalArr.length > 0) {
                    var value;
                    for (var index = 0; index < length; index++) {
                        value = msg.d[index];
                        var intervalLength = intervalArr.length;
                        for (var i = 0; i < intervalLength; i++) {
                            var duration = parseInt($.trim($('#txtServiceDuration').val().replace( /[^-0-9\.]+/g , '')));
                            var bookedInterval = value.ServiceBookedInterval;
                            bookedInterval = bookedInterval.split('-');
                            var bookedIntervalF = bookedInterval[0];
                            var bookIff = bookedIntervalF.replace( /[^-0-9-:\.]+/g , '');
                            bookIff = bookIff.split( /:/ );

                            var bookIntervalT = bookedInterval[1];
                            var bookItt = bookIntervalT.replace( /[^-0-9-:\.]+/g , '');
                            bookItt = bookItt.split( /:/ );
                            var totalInterval = ((parseInt(bookItt[0]) - parseInt(bookIff[0])) * 60) + (parseInt(bookItt[1]) - parseInt(bookIff[1]));
                            if (duration < totalInterval) {
                                if (checkIntervalIsInDuration(intervalArr[i], value.ServiceBookedInterval, 0)) {
                                    var $hiden1 = $('#idServiceTimeLi li').find(".cssClassServiceDuration input:hidden[value='" + intervalArr[i] + "']");
                                    $hiden1.attr('name', value.AppointmentID);
                                    $hiden1.parent('li:eq(0)').find("input[name=chkServiceTimeInterval]").prop('disabled', 'disabled');                                    $hiden1.parent('li:eq(0)').addClass('booked');
                                }
                            } else if (totalInterval == duration) {
                                if (intervalArr[i] == value.ServiceBookedInterval) {
                                    var $hiden = $('#idServiceTimeLi li').find(".cssClassServiceDuration input:hidden[value='" + intervalArr[i] + "']");
                                    $hiden.attr('name', value.AppointmentID);
                                    $hiden.parent('li:eq(0)').find("input[name=chkServiceTimeInterval]").prop('disabled', 'disabled');
                                    $hiden.parent('li:eq(0)').addClass('booked');
                                }
                            } else if (duration > totalInterval) {
                                if (checkIntervalIsInDuration(intervalArr[i], value.ServiceBookedInterval, 1)) {
                                    var $hiden3 = $('#idServiceTimeLi li').find(".cssClassServiceDuration input:hidden[value='" + intervalArr[i] + "']");
                                    $hiden3.attr('name', value.AppointmentID);
                                    $hiden3.parent('li:eq(0)').find("input[name=chkServiceTimeInterval]").prop('disabled', 'disabled');
                                    $hiden3.parent('li:eq(0)').addClass('booked');
                                }
                            }
                        }
                    };
                } else {
                }
                $('#idServiceTimeLi li').each(function() {
                    $(this).find('.cssClassServiceDuration ul li').each(function() {
                        var interval = $(this).find('input[type=hidden]').val();
                        var appointmentID = $(this).find('input[type=hidden]').attr('name');
                        if (interval == AppointmentManage.serviceData.AppointmentBookTime
                            && appointmentID == appointmentId) {
                            $(this).find("input[name=chkServiceTimeInterval]").removeAttr("disabled").prop("checked", "checked");
                        }
                    });
                });
            } else {
                AppointmentManage.BindServiceIntervalTime(preferredTimeId, timeFrom, timeTo);
            }
        },
        BindServiceProviderEmployee: function(msg) {
            $("#ddlStoreServiceProvider").html('');
            var providerList = "<option value='0'> - All - </option>";
            var value;
            var length = msg.d.length;
            for (var index = 0; index < length; index++) {
                value = msg.d[index];
                providerList += "<option value=" + value.EmployeeID + ">" + value.EmployeeName + "</option>";
            };
            $("#ddlStoreServiceProvider").append(providerList);
        },
        GetServceProviderEmployee: function(serviceId, branchId) {
            var aspxCommonObj = AspxCommonObj();
            aspxCommonObj.UserName = null;
            this.config.url = this.config.baseURL + "GetServiceProviderNameListFront";
            this.config.data = JSON2.stringify({ serviceCategoryId: serviceId, storeBranchId: branchId, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 10;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (AppointmentManage.config.ajaxCallMode) {
            case 1:
                csscody.info("<h2>"+getLocale(AspxBookAnAppointment,'Information Message')+"</h2><p>"+getLocale(AspxBookAnAppointment,'Appointment has been deleted successfully.')+"</p>");
                AppointmentManage.BindAppointmentList(null, null, null, null);
                break;
            case 2:
                var length = msg.d.length;
                if (length > 0) {
                                                          AppointmentManage.BindAppointmentDetail(msg);
                }
                break;
            case 3:
                $("#ddlStoreLocation").html('');
                $("#ddlAllStoresSearch").html('');
                var storeLocation = "<option value='0'> - All - </option>";
                var value;
                var length = msg.d.length;
                for (var index = 0; index < length; index++) {
                    value = msg.d[index];
                    storeLocation += "<option value=" + value.LocationID + ">" + value.StoreName + "</option>";
                };
                $("#ddlStoreLocation").append(storeLocation);
                $("#ddlAllStoresSearch").append(storeLocation);
                break;
            case 4:
                $("#ddlAppointmentStatus,#ddlAppointmentStatusList").html('');
                var statusList = "<option value='0'> - All - </option>";
                var value;
                var length = msg.d.length;
                for (var index = 0; index < length; index++) {
                    value = msg.d[index];
                    statusList += "<option value=" + value.AppointmentStatusID + ">" + value.AppointmentStatusName + "</option>";
                };
                $("#ddlAppointmentStatus,#ddlAppointmentStatusList").append(statusList);
                break;
            case 5:
                viewFlag = false;
                $("#divBookAppointManage").show();
                $("#appointmentMainDiv").hide();
                $('.cssClassTimeList').hide();
                $('#txtPreferredTime').show();
                $('#btnResetData').hide();
                AppointmentManage.BindAppointmentList(null, null, null, null);
                csscody.info("<h2>"+getLocale(AspxBookAnAppointment,'Information Message')+"</h2><p>"+getLocale(AspxBookAnAppointment,'Appointment has been updated successfully and mail has been sent to user for notification.')+"</p>");
                break;
            case 6:
                if (viewFlag == false) {
                    $("#ddlStoreServiceProvidersSearch").html('');
                    var providerListSearch = "<option value='0'> - All - </option>";
                    var value;
                    var length = msg.d.length;
                    for (var index = 0; index < length; index++) {
                        value = msg.d[index];
                        providerListSearch += "<option value=" + value.EmployeeID + ">" + value.EmployeeName + "</option>";
                    };
                    $("#ddlStoreServiceProvidersSearch").append(providerListSearch);
                } else {
                    $("#ddlStoreServiceProvider").html('');
                    var providerList = "<option value='0'> - All - </option>";
                    var value;
                    var length = msg.d.length;
                    for (var index = 0; index < length; index++) {
                        value = msg.d[index];
                        providerList += "<option value=" + value.EmployeeID + ">" + value.EmployeeName + "</option>";
                    };
                    $("#ddlStoreServiceProvider").append(providerList);
                }
                break;
            case 7:
                AppointmentManage.BindServiceDates(msg);
                break;
            case 8:
                $('#txtPreferredTimeInterval').val('');
                AppointmentManage.BindServiceTime(msg);
                break;
            case 9:
                AppointmentManage.BindServiceBookedTime(msg);
                break;
            case 10:
                AppointmentManage.BindServiceProviderEmployee(msg);
                break;
            case 11:            
                AppointmentManage.BindDataForExport(msg);
                break;
            default:
                break;
            }
        },
        ajaxFailure: function(msg) {
            switch (AppointmentManage.config.ajaxCallMode) {
            case 0:
                break;
            case 8:
                $('.cssClassUlTimeList').html('').html('<li>'+getLocale(AspxBookAnAppointment,"Time is not allocated!")+'</li>');
                break;
            }
        },
        SearchAppointment: function() {
            var userName = $.trim($("#txtSearchUserName").val());
            if (userName.length < 1) {
                userName = null;
            }
            var appointmentStatusName = '';
            if ($("#ddlAppointmentStatus").val() == "0") {
                appointmentStatusName = null;
            } else {
                appointmentStatusName = $.trim($("#ddlAppointmentStatus option:selected").text());
            }
            var branchName = '';
            if ($("#ddlAllStoresSearch").val() == "0") {
                branchName = null;
            } else {
                branchName = $.trim($("#ddlAllStoresSearch option:selected").text());
            }

            var employeeName = '';
            if ($("#ddlStoreServiceProvidersSearch").val() == "0") {
                employeeName = null;
            } else {
                employeeName = $.trim($("#ddlStoreServiceProvidersSearch option:selected").text());
            }
            AppointmentManage.BindAppointmentList(userName, appointmentStatusName, branchName, employeeName);
        },
        EnableCheckBox: function() {
            var container = $('#chkGender');
            container.find('input[name=gender]').click(function(e) {
                if (this.checked) {
                    container.find('input[name=gender]').not(this).removeAttr('checked');
                    $("#genderError").html('');
                }
            });
            var customerContainer = $('#customerType');
            customerContainer.find('input[name=customerType]').click(function(e) {
                if (this.checked) {
                    customerContainer.find('input[name=customerType]').not(this).removeAttr('checked');
                    $("#customerTypeError").html('');
                }
            });
            var membershipEliteContainer = $('#membershipElite');
            membershipEliteContainer.find('input[name=membershipElite]').click(function(e) {
                if (this.checked) {
                    membershipEliteContainer.find('input[name=membershipElite]').not(this).removeAttr('checked');
                    $("#membershipeEliteError").html('');
                }
            });
        },
        Verifyform: function() {
            var isValid = false;
            var c = $("#form1").validate({
                rules: {
                    FirstName: {
                        required: true,
                        minlength: 2
                    },
                    LastName: {
                        required: true,
                        minlength: 2
                    },
                    MobileNumber: {
                        required: true,
                        number: true,
                        minlength: 10,
                        maxlength: 15
                    },
                    PhoneNumber: {
                        required: true,
                        number: true,
                        minlength: 7,
                        maxlength: 12
                    },
                    AppointmentEmail: {
                        required: true,
                        email: true
                    }
                },
                messages: {
                    FirstName: {
                        required: '* field required',
                        minlength: "* (at least 2 chars)"
                    },
                    LastName: {
                        required: '* field required',
                        minlength: "* (at least 2 chars)"
                    },
                    MobileNumber: {
                        required: '* field required',
                                               minlength: "*(at least 10 digits)",
                        maxlength: "*(at most 15 digits)"
                    },
                    PhoneNumber: {
                        required: '* field required',
                                               minlength: "*(at least 7 digits)",
                        maxlength: "*(at most 12 digits)"
                    },
                    AppointmentEmail: {
                        required: '* field required',
                        email: "Please enter valid email id"
                    },
                    TypeOfTreatment: {
                        required: '* field required'
                    }
                }
            });
            if (c.form()) {
                if ($("#ddlAppointmentStatusList").val() != 0) {
                    if ($("#ddlStoreLocation").val() != 0) {
                        $('#storeLocationError').html('');
                        if ($('#ddlStoreServiceProvider').val() != 0) {
                            $('#ServiceProviderError').html('');
                            if ($('#txtPreferredDate').val() != '') {

                                if ($("#txtPreferredTime:visible").length > 0) {
                                    if ($("#txtPreferredTime").val() != '') {
                                        isValid = true;
                                    } else {
                                        $('#lblTimeError').html('required').css("color", "red");
                                    }
                                } else {
                                    if ($('input[name=chkServiceTime]:checked').length > 0) {
                                        $("#lblTimeError").html('').removeClass("red");
                                        if ($('input[name=chkServiceTimeInterval]:checked').length > 0) {
                                            $('#lblBookTimeIntervalError').html('');
                                            isValid = true;
                                        } else {
                                            $('#lblBookTimeIntervalError').html().css("color", "red");
                                            return false;
                                        }
                                    } else {
                                        $("#lblTimeError").html("Select one available time").css("color", "red");
                                        return false;
                                    }
                                }
                            } else {
                                $("#lblAvailableDateError").html('require').css("color", "red");
                                return false;
                            }
                        } else {
                            $('#ServiceProviderError').html('require').css("color", "red");
                            return false;
                        }

                    } else {
                        $('#storeLocationError').html('required').css("color", "red");
                        return false;
                    }
                } else {
                    $('#statusError').html('required').css("color", "red");
                    return false;
                }
            } else {
                return false;
            }
            return isValid;
        },
        Init: function() {
            $("#txtPreferredDate").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                                       buttonImageOnly: true,
                    minDate: 0,
                    maxDate: "+1M+10D"
                }
            );
                       AppointmentManage.LoadAppointmentAjaxImage();
            AppointmentManage.BindAppointmentList(null, null, null, null);
            AppointmentManage.GetStoreLocation();
            AppointmentManage.EnableCheckBox();
            AppointmentManage.GetAppointmentStatusList();
            $("#btnSearchAppointment").click(function() {
                AppointmentManage.SearchAppointment();
            });
            $("#txtSearchUserName,#ddlAppointmentStatus").keyup(function(event) {
                if (event.keyCode == 13) {
                    AppointmentManage.SearchAppointment();
                }
            });
            $("#btnCancelAppointment").click(function() {
                viewFlag = false;
                $("#appointmentMainDiv").hide();
                $("#divBookAppointManage").show();
                $('.cssClassTimeList').hide();
                $('#txtPreferredTime').show();
                $('#btnResetData').hide();
            });

            $("#btnUpdateAppointment").click(function() {
                if (AppointmentManage.Verifyform()) {
                                       AppointmentManage.SaveUpateAppointment();
                } else {
                    csscody.info("<h2>"+getLocale(AspxBookAnAppointment,'Information Message')+"</h2><p>"+getLocale(AspxBookAnAppointment,'Invalid form data.')+"</p>");
                }
            });

            var titleOptions = '<option value="1">Mr</option>';
            titleOptions += '<option value="2">Ms</option>';
            titleOptions += '<option value="3">Mrs</option>';

            $("#ddlTitle").html('').append(titleOptions);

            $("#btnDeleteSelectedAppointment").click(function() {
                var appointmentIDs = '';
                appointmentIDs = SageData.Get("tblBookAppointment").Arr.join(',');
                if (appointmentIDs.length == 0) {
                    csscody.alert('<h2>'+getLocale(AspxBookAnAppointment,"Information Alert")+'</h2><p>' + getLocale(AspxBookAnAppointment, "Please select at least one appointment to delete.") + '</p>');
                    return false;
                }
                var properties = {
                    onComplete: function(e) {
                        AppointmentManage.DeleteAppointmentByID(appointmentIDs, e);
                    }
                };
                csscody.confirm("<h1>" + getLocale(AspxBookAnAppointment, "Delete Confirmation") + "</h1><p>" + getLocale(AspxBookAnAppointment, "Are you sure you want to delete the selected appointment(s)?") + "</p>", properties);

            });

            $("#ddlAllStoresSearch").change(function() {
                var branchID = $(this).val();
                AppointmentManage.GetStoreProviderNameList(branchID);
            });
            $('input[name=chkServiceTime]').on('click', function () {               
                if ($('input[name=chkServiceTime]:checked').length > 0) {
                    var x = $(this).parent('li').attr('class');
                    x = x.split('_');
                    var timeId = x[1];                 
                    AppointmentManage.GetBookedServiceTime(timeId);
                } else {
                    $('#txtPreferredTimeInterval').val('');
                }

            });

            $('input[name=chkServiceTimeInterval]').on('click', function() {
                if ($('input[name=chkServiceTimeInterval]:checked').length > 0) {
                    var interval = $(this).parents('li:eq(0)').find('input[type=hidden]').val();
                    $('#txtPreferredTimeInterval').val(interval);
                } else {
                    $('#txtPreferredTimeInterval').val('');
                }
            });
            $('#ddlStoreLocation').change(function() {
                $('#btnResetData').show();
                $('#storeLocationError').html('');
                $('#ddlStoreServiceProvider').val(0);
                $('#txtPreferredDate').val('');
                $('#txtPreferredTime').val('');
                $('#txtPreferredTimeInterval').val('');
                $('.cssClassTimeList').hide();
                $('#txtPreferredTime').show();
                if ($(this).val() != 0) {
                    var serviceID = $('#ServiceCategoryID').val();
                    var branchID = $('#ddlStoreLocation option:selected').val();
                    AppointmentManage.GetServceProviderEmployee(serviceID, branchID);
                } else {
                    $('#ddlStoreServiceProvider').html('<option value="0" class="sfLocale">- All -</option>');
                }
            });
            $('#ddlStoreServiceProvider').change(function() {

                $('#txtPreferredDate').val('');
                $('#txtPreferredTime').val('');
                $('#txtPreferredTimeInterval').val('');
                if ($(this).val() != 0) {
                                       $('#ServiceProviderError').html('');
                    var serviceId = $('#ServiceCategoryID').val();
                    var branchId = $('#ddlStoreLocation option:selected').val();
                    var empId = $('#ddlStoreServiceProvider option:selected').val();
                    AppointmentManage.GetServicesDates(serviceId, branchId, empId);
                } else {
                    $('.cssClassTimeList').hide();
                    $('#txtPreferredTime').show();
                }
            });
            $('#btnResetData').bind('click', function() {
                AppointmentManage.ResetData();
            });
        }
    };
    AppointmentManage.Init();
});