<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserDownloadableProducts.ascx.cs"
    Inherits="Modules_AspxUserDashBoard_UserDownloadableProducts" %>

<script type="text/javascript">

    //<![CDATA[
    var UserDownloadable = "";
    aspxCommonObj.UserName = AspxCommerce.utils.GetUserName();
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxUserDashBoard
        });
    });

    UserDownloadable = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: moduleRootPath,
            url: "",
            method: "",
            ajaxCallMode: "",
            error: ""

        },

        vars: {
            isRemainDownload: ''
        },

        ajaxCall: function(config) {
            $.ajax({
                type: UserDownloadable.config.type,
                beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", userModuleIDUD);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: UserDownloadable.config.contentType,
                cache: UserDownloadable.config.cache,
                async: UserDownloadable.config.async,
                data: UserDownloadable.config.data,
                dataType: UserDownloadable.config.dataType,
                url: UserDownloadable.config.url,
                success: UserDownloadable.config.ajaxCallMode,
                error: UserDownloadable.config.error
            });
        },

        init: function() {
            UserDownloadable.BindCustomerDownLoadItemsGrid(null, null);
            UserDownloadable.BindCustomerDownLoadItems(null, null);

            $("#btnDeleteCustDownloadableItem").click(function() {
                var orderItem_Ids = '';
                                $(".orderitemsChkbox").each(function(i) {
                    if ($(this).prop("checked")) {
                        orderItem_Ids += $(this).val() + ',';
                    }
                });
                if (orderItem_Ids != "") {
                    var properties = {
                        onComplete: function(e) {
                            UserDownloadable.DeleteCustomerDownloadableItem(orderItem_Ids, e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxUserDashBoard, "Delete Confirmation") + "</h2><p>" + getLocale(AspxUserDashBoard, "Do you want to delete all selected Order Items?") + "</p>", properties);
                } else {
                    csscody.alert("<h2>" + getLocale(AspxUserDashBoard, "Information Alert") + "</h2><p>" + getLocale(AspxUserDashBoard, "You need to select at least one item before you can do this.") + "<br/>" + getLocale(AspxUserDashBoard, "To select one or more items, just check the box before each item.") + "</p>");
                }
                return false;
            });

            $("#btnDeleteItems").click(function() {
                var orderItem_Ids = '';
                                $(".orderdownloadChkbox").each(function(i) {
                    if ($(this).prop("checked")) {
                        orderItem_Ids += $(this).val() + ',';
                    }
                });
                if (orderItem_Ids != "") {
                    var properties = {
                        onComplete: function(e) {
                            UserDownloadable.DeleteCustomerDownloadableItem(orderItem_Ids, e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxUserDashBoard, "Delete Confirmation") + "</h2><p>" + getLocale(AspxUserDashBoard, "Do you want to delete all selected Order Items?") + "</p>", properties);
                } else {
                    csscody.alert("<h2>" + getLocale(AspxUserDashBoard, "Information Alert") + "</h2><p>" + getLocale(AspxUserDashBoard, "You need to select at least one item before you can do this.") + "<br/>" + getLocale(AspxUserDashBoard, "To select one or more items, just check the box before each item.") + "</p>");
                }
                return false;
            });
        },
        SearchItems: function() {
            var sku = $.trim($("#txtSearchSKU").val());
            var Nm = $.trim($("#txtSearchName").val());
            if (sku.length < 1) {
                sku = null;
            }
            if (Nm.length < 1) {
                Nm = null;
            }
            var isAct = $.trim($("#ddlIsActive").val()) == "" ? null : ($.trim($("#ddlIsActive").val()) == "True" ? true : false);

            UserDownloadable.BindCustomerDownLoadItemsGrid(sku, Nm);
        },
        SearchItem: function() {
            var sku = $.trim($("#txtsku").val());
            var Nm = $.trim($("#txtNm").val());
            if (sku.length < 1) {
                sku = null;
            }
            if (Nm.length < 1) {
                Nm = null;
            }
            var isAct = $.trim($("#ddlIsActive").val()) == "" ? null : ($.trim($("#ddlIsActive").val()) == "True" ? true : false);

            UserDownloadable.BindCustomerDownLoadItems(sku, Nm);
        },
        BindCustomerDownLoadItemsGrid: function(sku, Nm) {
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCustomerDownLoadItems_pagesize").length > 0) ? $("#gdvCustomerDownLoadItems_pagesize :selected").text() : 10;

            $("#gdvCustomerDownLoadItems").sagegrid({
                url: this.config.baseURL + "UserDashBoardHandler.ashx/",
                functionMethod: 'GetCustomerDownloadableItems',
                colModel: [
                    { display: 'OrderItemID', name: 'orderitemid', cssclass: 'cssClassHeadCheckBox', controlclass: 'classClassDownCheckBox', coltype: 'checkbox', align: 'center', elemDefault: false, elemClass: 'orderitemsChkbox' },
                    { display: 'OrderItemID#', name: 'order_item_id', cssclass: '', coltype: 'label', align: 'left', controlclass: '', hide: true },
                    { display: 'OrderID#', name: 'orderid', cssclass: '', coltype: 'label', align: 'left', controlclass: '', hide: true },
                    { display: 'RandomNo', name: 'random_no', cssclass: '', controlclass: '', coltype: 'label', align: 'left', controlclass: '', hide: true },
                    { display: 'ItemID', name: 'itemid', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'SKU', name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxUserDashBoard, 'Item Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Sample Link', name: 'sample_link', cssclass: '', controlclass: 'cssSClassDownload', coltype: 'linklabel', align: 'left', value: '9', downloadarguments: '14,4', downloadmethod: 'UserDownloadable.DownloadSampleFile', hide: true },
                    { display: getLocale(AspxUserDashBoard, 'Actual Link'), name: 'actual_link', cssclass: '', controlclass: 'cssAClassDownload cssDClassDownload', coltype: 'download', align: 'left', value: '10', randomValue: '3', downloadarguments: '14,4,11,1,3', downloadmethod: 'UserDownloadable.DownloadActualFile' },
                    { display: 'Sample File', name: 'sample_file', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Actual File', name: 'actual_file', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Order Status ID', name: 'orderstatusid', cssclass: '', coltype: 'label', align: 'left', controlclass: '', hide: true },
                    { display: getLocale(AspxUserDashBoard, 'Status'), name: 'status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxUserDashBoard, 'Download'), name: 'download', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxUserDashBoard, 'Remaining Download'), name: 'remaindownload', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxUserDashBoard, 'Last Download Date'), name: 'lastdownload', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxUserDashBoard, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center', hide: true }
                ],

                buttons: [
                                ],
                rp: perpage,
                nomsg: getLocale(AspxUserDashBoard, "No Records Found!"),
                param: { sku: sku, name: Nm, aspxCommonObj: aspxCommonObj, isActive: true },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 16: { sorter: false} }
            });
        },
        BindCustomerDownLoadItems: function(sku, Nm) {
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvTable_pagesize").length > 0) ? $("#gdvTable_pagesize :selected").text() : 10;

            $("#gdvTable").sagegrid({
                url: this.config.baseURL + "UserDashBoardHandler.ashx/",
                functionMethod: 'GetCustomerDownloadableItems',
                colModel: [
                    { display: 'OrderItemID', name: 'orderDownloadid', cssclass: 'cssClassHeadCheckBox', controlclass: 'classClassCheckBox', coltype: 'checkbox', align: 'center', elemDefault: false, elemClass: 'orderdownloadChkbox' },
                    { display: 'OrderItemID#', name: 'order_item_id', cssclass: '', coltype: 'label', align: 'left', controlclass: '', hide: true },
                    { display: 'OrderID#', name: 'orderid', cssclass: '', coltype: 'label', align: 'left', controlclass: '', hide: true },
                    { display: 'RandomNo', name: 'random_no', cssclass: '', controlclass: '', coltype: 'label', align: 'left', controlclass: '', hide: true },
                    { display: 'ItemID', name: 'itemid', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'SKU', name: 'sku', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxUserDashBoard, 'Item Name'), name: 'item_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Sample Link', name: 'sample_link', cssclass: '', controlclass: 'cssSClassDownload', coltype: 'linklabel', align: 'left', value: '9', downloadarguments: '14,4', downloadmethod: 'UserDownloadable.DownloadSampleFile', hide: true },
                    { display: getLocale(AspxUserDashBoard, 'Actual Link'), name: 'actual_link', cssclass: '', controlclass: 'cssAClassDownload cssDClassDownload', coltype: 'download', align: 'left', value: '10', randomValue: '3', downloadarguments: '14,4,11,1,3', downloadmethod: 'UserDownloadable.DownloadActualFile',hide:true },
                    { display: 'Sample File', name: 'sample_file', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Actual File', name: 'actual_file', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Order Status ID', name: 'orderstatusid', cssclass: '', coltype: 'label', align: 'left', controlclass: '', hide: true },
                    { display: getLocale(AspxUserDashBoard, 'Status'), name: 'status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxUserDashBoard, 'Download'), name: 'download', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxUserDashBoard, 'Remaining Download'), name: 'remaindownload', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxUserDashBoard, 'Last Download Date'), name: 'lastdownload', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxUserDashBoard, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],

                buttons: [
                { display: getLocale(AspxUserDashBoard, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'UserDownloadable.DeleteCustomerDownloadItem', arguments: '2' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxUserDashBoard, "No Records Found!"),
                param: { sku: sku, name: Nm, aspxCommonObj: aspxCommonObj, isActive: false },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 16: { sorter: false} }
            });
        },
        DownloadActualFile: function(argus) {         
            var itemid = argus[1];
            var orderItemId = argus[3];
            UserDownloadable.config.method = "UserDashBoardHandler.ashx/CheckRemainingDownload";
            UserDownloadable.config.url = UserDownloadable.config.baseURL + UserDownloadable.config.method;            
            UserDownloadable.config.data = JSON2.stringify({ itemId: itemid, orderItemId: orderItemId, aspxCommonObj: aspxCommonObj });
            UserDownloadable.config.ajaxCallMode = UserDownloadable.SetRemainingDownload;
            UserDownloadable.ajaxCall(UserDownloadable.config);
                                    if (!UserDownloadable.vars.isRemainDownload) {
                csscody.alert("<h2>" + getLocale(AspxUserDashBoard, "Information Alert") + "</h2><p>" + getLocale(AspxUserDashBoard, "The download exceeds the maximum download limit!") + "</p>");
                return false;
            } else if (argus[2] == 3 && argus[0] > 0 && UserDownloadable.vars.isRemainDownload) {
                $(".cssDClassDownload_" + argus[4] + "").jDownload({
                    root: aspxfilePath,
                    dialogTitle: getLocale(AspxUserDashBoard, 'AspxCommerce download actual item:'),
                    stop: function() {
                        UserDownloadable.UpdateDownloadCount(itemid, orderItemId);
                    }
                });
            } else {
                csscody.alert("<h2>" + getLocale(AspxUserDashBoard, "Information Alert") + "</h2><p>" + getLocale(AspxUserDashBoard, "Your order is not completed. Try later!!") + "</p>");
                return false;
            }
        },

        DownloadSampleFile: function(argus) {
            $(".cssSClassDownload").jDownload({
                root: aspxfilePath,
                dialogTitle: getLocale(AspxUserDashBoard, 'AspxCommerce download sample item:')
            });
        },

        UpdateDownloadCount: function(itemid, orderItemId) {
            var itemID = itemid;
            this.config.method = "UserDashBoardHandler.ashx/UpdateDownloadCount";
            this.config.url = this.config.baseURL + this.config.method;         
            this.config.data = JSON2.stringify({ itemID: itemID, orderItemID: orderItemId, downloadIP: downloadIP, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = UserDownloadable.BindDownloadableItemOnUpdate;
            this.ajaxCall(this.config);
        },

        DeleteCustomerDownloadItem: function(tblID, argus) {
            var properties = {
                onComplete: function(e) {
                    UserDownloadable.DeleteCustomerDownloadableItem(argus[0], e);
                }
            }
            csscody.confirm("<h2>" + getLocale(AspxUserDashBoard, "Delete Confirmation") + "</h2><p>" + getLocale(AspxUserDashBoard, "Are you sure you want to delete this?") + "</p>", properties);

        },

        DeleteCustomerDownloadableItem: function(_OrderItemID, event) {
            if (event) {
                this.config.method = "UserDashBoardHandler.ashx/DeleteCustomerDownloadableItem";
                this.config.url = this.config.baseURL + this.config.method;                
                this.config.data = JSON2.stringify({ orderItemID: _OrderItemID, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = UserDownloadable.BindDownloadItemOnDelete;
                this.config.error = UserDownloadable.GetDownloadItemErrorMsg;
                this.ajaxCall(this.config);
            }
            return false;
        },

        SetRemainingDownload: function(data) {
            UserDownloadable.vars.isRemainDownload = data.d;
        },

        BindDownloadableItemOnUpdate: function() {
            UserDownloadable.BindCustomerDownLoadItemsGrid(null, null);
            UserDownloadable.BindCustomerDownLoadItems(null, null);
        },

        BindDownloadItemOnDelete: function() {
            csscody.alert("<h2>" + getLocale(AspxUserDashBoard, "Successful Message") + "</h2><p>" + getLocale(AspxUserDashBoard, "Downloadable item has been deleted successfully.") + "</p>");
            UserDownloadable.BindCustomerDownLoadItemsGrid(null, null);
            UserDownloadable.BindCustomerDownLoadItems(null, null);
        },

        GetDownloadItemErrorMsg: function() {
            csscody.error("<h2>" + getLocale(AspxUserDashBoard, "Error Message") + "</h2><p>" + getLocale(AspxUserDashBoard, "Failed to downloadable item delete!") + "</p>");
        }

    };
    $(function(){
        UserDownloadable.init();
    });
    //]]>
</script>

<div id="gdvDownLoadableItems_grid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2 id="lblTitle" class="sfLocale">Your remaining items to download
            </h2>
        </div>
         <div class="sfGridwrapper">
        <div class="sfGridWrapperContent">
<div class="cssClassSearchPanel sfFormwrapper clearfix">
<div class="sfFloatNone">
<div class="sfCol_42"><label class="cssClassLabel sfLocale"> Name:</label>
<input type="text" id="txtSearchName" class="sfTextBoxSmall" /></div>
<div class="sfCol_41">
<label class="cssClassLabel sfLocale">SKU:</label>
<input type="text" id="txtSearchSKU" class="sfTextBoxSmall" /></div>
<div class="sfButtonwrapper sfCol_17">
<label class="cssClassOrangeBtn icon-search"><button type="button" onclick="UserDownloadable.SearchItems()"><span class="sfLocale">Search</span></button></label>
</div>
</div>
</div>
<div class="loading">
 <img id="ajaxUserDashBoardDownloadImage" src=""  class="sfLocale" alt="loading...."/>
                </div>
               <div class="log">
                </div>
               
                <table class="sfGridWrapperTable" id="gdvCustomerDownLoadItems" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                  </div>
            </div>
        <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
              <label class="cssClassDarkBtn icon-delete">      
                        <button type="button" id="btnDeleteCustDownloadableItem" class="sfLocale">
                            Delete All Selected</button></label>
                  
                </div>
            </div>
    </div>
</div>
<div id="gdvDownlodableItems">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2 id="lblCDI" class="sfLocale">
                Your download completed items
            </h2>
            
        </div>
          <div class="sfGridwrapper">
        <div class="sfGridWrapperContent">
                <div class="cssClassSearchPanel sfFormwrapper clearfix">
 <div class="sfFloatNone">               
<div class="sfCol_42"><label class="cssClassLabel sfLocale">Name:</label>
<input type="text" id="txtNm" class="sfTextBoxSmall" /></div>
<div class="sfCol_41">
<label class="cssClassLabel sfLocale">SKU:</label>
<input type="text" id="txtsku" class="sfTextBoxSmall" /></div>
 <div class="sfButtonwrapper sfCol_17">
   <label class="cssClassOrangeBtn icon-search"><button type="button" onclick="UserDownloadable.SearchItem()"><span class="sfLocale">Search</span></button></label>
   </div>
</div>
</div>
           
<div class="loading">
<img id="Img1" src=""  class="sfLocale" alt="loading...."/>
                </div>
               <div class="log">
                </div>
               
                <table class="sfGridWrapperTable"  id="gdvTable" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                 </div>
             </div>
        <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <label class="cssClassDarkBtn icon-delete">
<button type="button" id="btnDeleteItems">
 <span class="sfLocale">Delete All Selected</span></button></label>
                   
                </div>
            </div>
           
    </div>
</div>
