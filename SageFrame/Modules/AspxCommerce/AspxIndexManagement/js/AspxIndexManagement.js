var IndexManage;
$(function () {
    IndexManage = {
        init: function () {
            IndexManage.BindIndexDetails();

            $('#btnReIndexAll').click(function () {
                IndexManage.config.url = IndexManage.config.baseURL + "ReIndexAllTables";
                IndexManage.config.data = JSON2.stringify({});
                IndexManage.config.ajaxCallMode = 2;
                IndexManage.ajaxCall(IndexManage.config);
            });
        },
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxservicePath + "AspxCoreHandler.ashx/",
            method: "",
            url: "",
            ajaxCallMode: 0
        },
        ajaxCall: function (config) {
            $.ajax({
                type: 'POST', beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: "application/json; charset=utf-8",
                cache: false,
                async: false,
                data: IndexManage.config.data,
                dataType: 'json',
                url: IndexManage.config.url,
                success: IndexManage.ajaxSuccess,
                error: IndexManage.ajaxFailure
            });
        },
        HideAll: function () {
            $("#divIndexManagement").hide();
        },
        ReIndex: function (_tableName, argus) {
            IndexManage.config.url = IndexManage.config.baseURL + "ReIndex";
            IndexManage.config.data = JSON2.stringify({ tableName: argus[0] });
            IndexManage.config.ajaxCallMode = 1;
            IndexManage.ajaxCall(IndexManage.config);
        },
        ReBuild: function (_tableName, argus) {
            IndexManage.config.url = IndexManage.config.baseURL + "ReBuild";
            IndexManage.config.data = JSON2.stringify({ tableName: argus[0] });
            thIndexManageis.config.ajaxCallMode = 1;
            IndexManage.ajaxCall(IndexManage.config);
        },
        BindIndexDetails: function () {
            this.config.method = "GetIndexedTables";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvIndexedTables_pagesize").length > 0) ? $("#gdvIndexedTables_pagesize :selected").text() : 10;

            $("#gdvIndexedTables").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxIndexManagementLocal, 'Table Name'), name: 'TableName', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxIndexManagementLocal, 'Index Name'), name: 'IndexName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxIndexManagementLocal, 'External Fragmentation'), name: 'ExternalFragmentation', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxIndexManagementLocal, 'Internal Fragmentation'), name: 'InternalFragmentation', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxIndexManagementLocal, 'IsRebuild'), name: 'IsRebuild', cssclass: 'cssClassHide', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxIndexManagementLocal, 'Status'), name: 'IsReady', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxIndexManagementLocal, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxIndexManagementLocal, 'ReBuild'), name: 'rebuild', enable: true, _event: 'click', trigger: '1', callMethod: 'IndexManage.ReBuild', arguments: '' },
                    { display: getLocale(AspxIndexManagementLocal, 'ReIndex'), name: 'reindex', enable: true, _event: 'click', trigger: '1', callMethod: 'IndexManage.ReIndex', arguments: '' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxIndexManagementLocal, "No Records Found!"),
                param: {},
                current: current_,
                pnew: offset_,
                sortcol: { 10: { sorter: false} }
            });

        },

        ViewOrders: function (tblID, argus) {
            switch (tblID) {
                case "gdvOrderDetails":
                    IndexManage.HideAll();
                    oid = argus[0];
                    $('#' + lblOrderForm1).html("Order ID: " + argus[0]);
                    $("#divOrderDetailForm").show();
                    IndexManage.BindAllOrderDetailsForm(argus[0]);
                    break;
                default:
                    break;
            }
        },
        ajaxSuccess: function (data) {
            switch (IndexManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    IndexManage.BindIndexDetails();
                    csscody.info('<h2>' + getLocale(AspxIndexManagementLocal, "Successful Message") + '</h2><p>' + getLocale(AspxIndexManagementLocal, "Table optimized with success") + '</p>');
                    break;
                case 2:
                    IndexManage.BindIndexDetails();
                    csscody.info('<h2>' + getLocale(AspxIndexManagementLocal, "Successful Message") + '</h2><p>' + getLocale(AspxIndexManagementLocal, "You database was re-indexed with success") + '</p>');
                    break;
            }
        }
    };
    IndexManage.init();
});
