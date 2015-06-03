<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServicesItems.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxItemsManagement_ServicesItems" %>

<script type="text/javascript">
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxItemsManagement
        });
    });
    var serviceManage = "";
    var lblserviceDetail = '<%=lblserviceDetails.ClientID %>';
    var lblServiceEmp = '<%=lblServiceEmp.ClientID %>';
    var lblBooking = '<%=lblBooking.ClientID %>';
    $(function () {
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        serviceManage = {
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
                method: ""
            },
            vars: {
                branchID: 0
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: serviceManage.config.type,
                    contentType: serviceManage.config.contentType,
                    cache: serviceManage.config.cache,
                    async: serviceManage.config.async,
                    data: serviceManage.config.data,
                    dataType: serviceManage.config.dataType,
                    url: serviceManage.config.url,
                    success: serviceManage.ajaxSuccess,
                    error: serviceManage.ajaxFailure
                });
            },
            SearchService: function () {
                var serviceNm = $.trim($("#txtServiceName").val());
                var branchNm = $.trim($("#txtStoreBranchNm").val());
                serviceManage.GetAllServices(serviceNm, branchNm);
            },
            SearchEmp: function () {
                var serviceEmpNm = $.trim($("#txtServiceEmpNm").val());
                serviceManage.BindServiceEmployees($("#hdnServiceID").val(), serviceEmpNm);
            },
            SearchServiceEmp: function () {
                var itemNm = $.trim($("#txtItemName").val());
                serviceManage.BindServiceEmpDetails($("#hdnServiceID").val(), $("#hdnEmployeeID").val(), itemNm);
            },
            SearchBooking: function () {
                var statusId = $("#selectAppointmentStatus option:selected").val();
                if (statusId == 0) {
                    statusId = null;
                }
                serviceManage.BindBookingDetails($("#hdnEmployeeID").val(), statusId);
            },
            GetAllServices: function (serviceNm, branchNm) {
                this.config.method = "GetAllServiceList";
                this.config.data = { aspxCommonInfo: aspxCommonObj, serviceName: serviceNm, branchName: branchNm };
                var data = this.config.data;
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvServiceList_pagesize").length > 0) ? $("#gdvServiceList_pagesize :selected").text() : 10;

                $("#gdvServiceList").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                        { display: 'service_ID', name: 'tax_customer_class_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TaxCustomerClassChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox', hide: true },
                        { display: getLocale(AspxItemsManagement, 'Service Name'), name: 'serviceName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxItemsManagement, 'Branch Name'), name: 'branchName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxItemsManagement, 'Branch ID'), name: 'branchID', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                        { display: getLocale(AspxItemsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                    ],

                    buttons: [
                        { display: getLocale(AspxItemsManagement, 'View'), name: 'view', enable: true, _event: 'click', trigger: '1', callMethod: 'serviceManage.ViewServiceEmployee', arguments: '0,1,2,3,4,5,6' }
                    ],
                    txtClass: 'sfInputbox',
                    rp: perpage,
                    nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                    param: data,
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 4: { sorter: false } }
                });

            },
            ViewServiceEmployee: function (tblID, argus) {
                switch (tblID) {
                    case "gdvServiceList":
                        $("#hdnServiceID").val(argus[0]);
                        $("#" + lblserviceDetail).html(getLocale(AspxItemsManagement, "Service Providers For ") + argus[4] + ' ' + getLocale(AspxItemsManagement, "In ") + argus[5] + ' ' + getLocale(AspxItemsManagement, "Branch:"));
                        serviceManage.HideDiv();
                        serviceManage.BindServiceEmployees(argus[0], argus[6], null);
                        $("#divServiceEmployee").show();
                        break;
                }
            },
            HideDiv: function () {
                $("#divServicesGrid").hide();
                $("#divServiceEmployee").hide();
                $("#divServiceEmpDetails").hide();
                $("#divBookedDetails").hide();
            },
            Clear: function () {
                $("#txtServiceEmpNm").val('');
                $("#txtItemNm").val('');
            },
            BindServiceEmployees: function (Id, branchID, serviceEmpNm) {
                this.config.method = "GetServiceEmployee";
                this.config.data = { aspxCommonInfo: aspxCommonObj, serviceId: Id, branchID: branchID, serviceEmpName: serviceEmpNm };
                var data = this.config.data;
                var offset_ = 1;
                var current_ = 1;
                serviceManage.vars.branchID = branchID;
                var perpage = ($("#gdvServiceEmp_pagesize").length > 0) ? $("#gdvServiceEmp_pagesize :selected").text() : 10;

                $("#gdvServiceEmp").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                        { display: 'service_ID', name: 'tax_customer_class_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TaxCustomerClassChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox', hide: true },
                        { display: 'Service Employee ID', name: 'serviceEmployeeId', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                        { display: getLocale(AspxItemsManagement, 'Service Provider Name'), name: 'serviceEmployeeName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'Employee ID', name: 'employeeId', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                        { display: getLocale(AspxItemsManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                    ],

                    buttons: [
                        { display: getLocale(AspxItemsManagement, 'View Available Time'), name: 'view', enable: true, _event: 'click', trigger: '1', callMethod: 'serviceManage.ViewAvailableTime', arguments: '1,2,3,4,5' },
                        { display: getLocale(AspxItemsManagement, 'Booked Time'), name: 'bookedtime', enable: true, _event: 'click', trigger: '1', callMethod: 'serviceManage.BookingDetails', arguments: '1,2,3,4,5' }
                    ],
                    txtClass: 'sfInputbox',
                    rp: perpage,
                    nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                    param: data,
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 2: { sorter: false } }
                });
            },
            ViewAvailableTime: function (tblID, argus) {
                switch (tblID) {
                    case "gdvServiceEmp":
                        $("#hdnEmployeeID").val(argus[3]);
                        $("#" + lblServiceEmp).html("'" + argus[4] + "'" + getLocale(AspxItemsManagement, "Available Time:"));
                        serviceManage.HideDiv();
                        serviceManage.BindServiceEmpDetails(argus[0], argus[3], null);
                        $("#divServiceEmpDetails").show();
                        break;
                }
            },
            GetAppointmentStatusList: function () {
                this.config.url = this.config.baseURL + "GetAppointmentStatusList";
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },

            BookingDetails: function (tblID, argus) {
                switch (tblID) {
                    case "gdvServiceEmp":
                        $("#hdnEmployeeID").val(argus[3]);
                        $("#" + lblBooking).html("'" + argus[4] + "'" + getLocale(AspxItemsManagement, "Booking Details:"));
                        serviceManage.HideDiv();
                        serviceManage.BindBookingDetails(argus[3], null);
                        $("#divBookedDetails").show();
                        break;
                }
            },
            BindServiceEmpDetails: function (Id, empId, itemNm) {
                this.config.method = "GetServiceEmployeeDetails";
                this.config.data = { aspxCommonInfo: aspxCommonObj, serviceId: Id, employeeId: empId, branchID: serviceManage.vars.branchID, itemName: itemNm };
                var data = this.config.data;
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gvdServiceEmpDetails_pagesize").length > 0) ? $("#gvdServiceEmpDetails_pagesize :selected").text() : 10;

                $("#gvdServiceEmpDetails").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                        { display: 'service_ID', name: 'tax_customer_class_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TaxCustomerClassChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox', hide: true },
                        { display: getLocale(AspxItemsManagement, 'Item Name'), name: 'itemName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'ItemID', name: 'itemID', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                        { display: getLocale(AspxItemsManagement, 'Service Date From'), name: 'serviceDateFrom', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                        { display: getLocale(AspxItemsManagement, 'Service Date To'), name: 'serviceDateTo', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                        { display: getLocale(AspxItemsManagement, 'Service Time From'), name: 'serviceTimeFrom', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxItemsManagement, 'Service Time To'), name: 'serviceTimeTo', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxItemsManagement, 'Price'), name: 'price', cssclass: '', controlclass: 'cssClassFormatCurrency', coltype: 'currency', align: 'right' },
                        { display: 'Actions', name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center', hide: true }
                    ],

                    buttons: [
                    ],
                    txtClass: 'sfInputbox',
                    rp: perpage,
                    nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                    param: data,
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 2: { sorter: false } }
                });
                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
            },
            BindBookingDetails: function (empId, statusId) {
                this.config.method = "GetBookingDetails";
                this.config.data = { aspxCommonInfo: aspxCommonObj, employeeId: empId, statusId: statusId, branchID: serviceManage.vars.branchID };
                var data = this.config.data;
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gvdBookingDetails_pagesize").length > 0) ? $("#gvdBookingDetails_pagesize :selected").text() : 10;

                $("#gvdBookingDetails").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                        { display: 'AppointmentID', name: 'appointmentID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'TaxCustomerClassChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox', hide: true },
                        { display: getLocale(AspxItemsManagement, 'Appointment Status Name'), name: 'statusName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxItemsManagement, 'Customer Name'), name: 'customerName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxItemsManagement, 'Mobile Number'), name: 'mobile', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxItemsManagement, 'Email'), name: 'email', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxItemsManagement, 'Preferred Date'), name: 'preferredDate', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxItemsManagement, 'Preferred Time'), name: 'preferedTime', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxItemsManagement, 'Service'), name: 'preferedTime', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'Actions', name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center', hide: true }
                    ],

                    buttons: [
                    ],
                    txtClass: 'sfInputbox',
                    rp: perpage,
                    nomsg: getLocale(AspxItemsManagement, "No Records Found!"),
                    param: data,
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 2: { sorter: false } }
                });
            },
            ajaxSuccess: function (msg) {
                switch (serviceManage.config.ajaxCallMode) {
                    case 1:
                        $("#selectAppointmentStatus").html('');
                        var statusList = "<option value=0>" + getLocale(AspxItemsManagement, "--Select--") + "</option>";
                        $.each(msg.d, function (index, value) {
                            statusList += "<option value=" + value.AppointmentStatusID + ">" + value.AppointmentStatusName + "</option>";
                        });
                        $("#selectAppointmentStatus").append(statusList);
                        break;
                }
            },

            init: function () {
                serviceManage.HideDiv();
                $("#divServicesGrid").show();
                serviceManage.GetAppointmentStatusList();
                serviceManage.GetAllServices(null, null);
                $("#btnBack").click(function () {
                    serviceManage.HideDiv();
                    serviceManage.Clear();
                    $("#divServicesGrid").show();
                    $("#hdnServiceID").val('');
                });
                $("#btnBackServiceEmp").click(function () {
                    serviceManage.HideDiv();
                    serviceManage.Clear();
                    $("#divServiceEmployee").show();
                    $("#hdnEmployeeID").val('');
                });
                $("#btnBackToEmp").click(function () {
                    serviceManage.HideDiv();
                    serviceManage.Clear();
                    $("#divServiceEmployee").show();
                });
            }
        };
        serviceManage.init();
    });
</script>

<div id="divServicesGrid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text="Service Management"
                    meta:resourcekey="lblTitleResource2"></asp:Label>
            </h1>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Service Name:</label>
                                <input type="text" id="txtServiceName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Branch Name:</label>
                                <input type="text" id="txtStoreBranchNm" class="sfTextBoxSmall" />
                            </td>
                            <td>

                                <button type="button" onclick="serviceManage.SearchService()" class="sfBtn">
                                    <span class="sfLocale icon-search">Search</span></button>

                            </td>
                        </tr>
                    </table>
                </div>
                <table id="gdvServiceList" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divServiceEmployee">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblserviceDetails" runat="server"
                    meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfButtonwrapper">
            <button type="button" id="btnBack" class="sfBtn"><span class="sfLocale icon-arrow-slim-w">Back</span></button>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Service Provider Name:</label>
                                <input type="text" id="txtServiceEmpNm" class="sfTextBoxSmall" />
                            </td>
                            <td>

                                <button type="button" onclick="serviceManage.SearchEmp()" class="sfBtn">
                                    <span class="sfLocale icon-search">Search</span></button>



                            </td>
                        </tr>
                    </table>
                </div>
                <table id="gdvServiceEmp" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divServiceEmpDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblServiceEmp" runat="server"
                    meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfButtonwrapper">
            <button type="button" id="btnBackServiceEmp" class="sfBtn"><span class="sfLocale icon-arrow-slim-w">Back</span></</button>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Item Name:</label>
                                <input type="text" id="txtItemName" class="sfTextBoxSmall" />
                            </td>
                            <td>

                                <button type="button" onclick="serviceManage.SearchServiceEmp()" class="sfBtn">
                                    <span class="sfLocale icon-search">Search</span></button>

                            </td>
                        </tr>
                    </table>
                </div>
                <table id="gvdServiceEmpDetails" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divBookedDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblBooking" runat="server" meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfButtonwrapper cssClassPaddingNone">
            <button type="button" id="btnBackToEmp" class="sfBtn"><span class="sfLocale icon-arrow-slim-w">Back</span></button>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Status Name:</label>
                                <select id="selectAppointmentStatus"></select>
                            </td>
                            <td>


                                <button type="button" onclick="serviceManage.SearchBooking()" class="sfBtn">
                                    <span class="sfLocale icon-search">Search</span></button>

                            </td>
                        </tr>
                    </table>
                </div>
                <table id="gvdBookingDetails" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hdnServiceID" />
<input type="hidden" id="hdnEmployeeID" />
