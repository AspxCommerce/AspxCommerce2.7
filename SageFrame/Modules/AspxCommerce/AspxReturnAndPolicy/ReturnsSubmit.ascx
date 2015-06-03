<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReturnsSubmit.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxReturnAndPolicy_ReturnsSubmit" %>

<script type="text/javascript">
    var ReturnsSubmit = '';
    var orderId = '';
    $(function() {

        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var customerId = AspxCommerce.utils.GetCustomerID();
        var ip = AspxCommerce.utils.GetClientIP();
        var countryName = AspxCommerce.utils.GetAspxClientCoutry();
        var sessionCode = AspxCommerce.utils.GetSessionCode();
        var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
        var aspxCommonObj = {
            StoreID: storeId,
            PortalID: portalId,
            UserName: userName,
            CultureName: cultureName,
            CustomerID: customerId,
            SessionCode: sessionCode
        };
        $(".sfLocale").localize({
            moduleKey: AspxReturnAndPolicy
        });

        // Numeric only control handler
        jQuery.fn.ForceNumericOnly =
            function() {
                return this.each(function() {
                    $(this).keydown(function(e) {
                        var key = e.charCode || e.keyCode || 0;
                        // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
                        return (
                            key == 8 ||
                                key == 9 ||
                                    key == 46 ||
                                        (key >= 37 && key <= 40) ||
                                            (key >= 48 && key <= 57) ||
                                                (key >= 96 && key <= 105));
                    });
                });
            };
        ReturnsSubmit = {
            config: {
                isPostBack: false,
                async: false,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath(),
                method: "",
                url: "",
                ajaxCallMode: "",
                error: "",
                sessionValue: ""
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: ReturnsSubmit.config.type,
                    contentType: ReturnsSubmit.config.contentType,
                    cache: ReturnsSubmit.config.cache,
                    async: ReturnsSubmit.config.async,
                    url: ReturnsSubmit.config.url,
                    data: ReturnsSubmit.config.data,
                    dataType: ReturnsSubmit.config.dataType,
                    success: ReturnsSubmit.config.ajaxCallMode,
                    error: ReturnsSubmit.config.error
                });
            },
            Vars: {
                OrderID: 0,
                ExpiresInDays: 0

            },

            Init: function() {               
                $("#btnProceedReturn").bind("click", function() {
                    ReturnsSubmit.ReturnSaveUpdate();
                });

                $("#btnGoBack").bind("click", function() {
                    ReturnsSubmit.LoadControl();
                });

                $("#tblItemTable thead tr th:first input:checkbox").click(function() {
                    var checkedStatus = this.checked;
                    $("#tblItemTable tbody tr td:first-child input:checkbox").each(function() {
                        this.checked = checkedStatus;
                    });
                });
            },

            ReturnSaveUpdate: function() {
                ReturnsSubmit.GetSettings();
                var orderID = $("#hdnOrderID").val();
                var addressID = $("#hdnAddressID").val();
                var customerID = $("#hdnCustomerID").val();
                var sessionCode = '<%=sessionCode %>';
                var storeID = '<%=storeID %>';
                var portalID = '<%=portalID %>';
                var cultureName = '<%=cultureName %>';
                var addedBy = '<%=userName %>';
                var count = "";
                //Get the multiple Ids of the item selected
                $('#tblItemTable tbody tr').filter(':has(:checkbox:checked)').each(function() {
                    //get row values
                    count = 1;
                    var orderDateCount = $(this).find("#hdnOrderDate").val();
                    var isReturnApplied = $(this).find("#hdnIsReturnApplied").val();
                    var itemID = $(this).find("#hdnItemID").val();
                    var itemTypeID = $(this).find("#hdnItemTypeID").val();
                    var itemName = $(this).find("#hdnItemName").val();
                    var varaintID = $(this).find("#hdnVariantID").val();
                    var costVariants = $(this).find("#hdnCostVariants").val();
                    var productStatusID = $(this).find("#ddlProductStatus option:selected").val();
                    var quantity = $(this).find("#txtQuantity").val();
                    var hdnQuantity = $(this).find("#hdnQuantity").val();

                    if (parseInt(quantity) > parseInt(hdnQuantity)) {
                        csscody.error('<h2>' + getLocale(AspxReturnAndPolicy, 'Error Message') + '</h2><p>' + getLocale(AspxReturnAndPolicy, 'Returned item is greater than the ordered Quantity!.') + '<br/>Returned failed for ' + itemName + '</p>');
                        return;

                    }
                    var returnReasonID = $(this).find("#ddlReturnReason option:selected").val();
                    var otherDetails = $(this).find("#txtAreaOtherDetails").val();
                    var returnShippingAddressID = $(this).find("#ddlShipping option:selected").val();
                    var aspxCommonObj = {
                        StoreID: storeId,
                        PortalID: portalId,
                        UserName: addedBy,
                        CultureName: cultureName,
                        CustomerID: customerID,
                        SessionCode: sessionCode
                    };
                    var ReturnSaveUpdateObj = {
                        OrderID: orderID,
                        ItemID: itemID,
                        ItemName: itemName,
                        CostVariantIDs: varaintID,
                        CostVariants: costVariants,
                        Quantity: quantity,
                        ProductStatusID: productStatusID,
                        ReturnReasonID: returnReasonID,
                        OtherDetails: otherDetails,
                        ReturnShippingAddressID: returnShippingAddressID
                    };
                    if (itemTypeID != 2 && itemTypeID != 3 && itemTypeID != 4) {
                        if (isReturnApplied == 0) {
                            if (orderDateCount != 0) {
                                if (eval(orderDateCount) <= eval(ReturnsSubmit.Vars.ExpiresInDays)) {
                                    var param = JSON2.stringify({ ReturnSaveUpdateObj: ReturnSaveUpdateObj, aspxCommonObj: aspxCommonObj });
                                    $.ajax({
                                        type: "POST",
                                        url: moduleRootPath + "UserDashBoardHandler.ashx/ReturnSaveUpdate",
                                        data: param,
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function() {
                                            csscody.info("<h2>" + getLocale(AspxReturnAndPolicy, "Successful Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Retun has been filed for Item '") + itemName + "  " + costVariants + getLocale(AspxReturnAndPolicy, "' Successfully.") + "</p>");
                                            $('#fade, #popuprel2').fadeOut();
                                            //ReturnsSubmit.LoadControl();
                                        },
                                        error: function() {
                                            csscody.error("<h2>" + getLocale(AspxReturnAndPolicy, "Error Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Failed to file return for item '") + itemName + "  " + costVariants + "'." + "</p>");
                                        }
                                    });
                                }
                                else {
                                    csscody.error("<h2>" + getLocale(AspxReturnAndPolicy, "Error Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "You can not return or exchange item '") + itemName + "  " + costVariants + getLocale(AspxReturnAndPolicy, "' for upto") + " " + ReturnsSubmit.Vars.ExpiresInDays + " " + getLocale(AspxReturnAndPolicy, "days from the purchase date.") + "</p>");
                                }
                            }
                            else {
                                csscody.error("<h2>" + getLocale(AspxReturnAndPolicy, "Error Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "You can not return or exchange item '") + itemName + "  " + costVariants + getLocale(AspxReturnAndPolicy, "' if your order is not Processed or Completed.") + "</p>");
                            }
                        }
                        else {
                            csscody.error("<h2>" + getLocale(AspxReturnAndPolicy, "Error Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Reuturn has already been filed for item '") + itemName + "  " + costVariants + "'." + "</p>");
                        }
                    }
                    else {
                        if (itemTypeID == 2) {
                            csscody.error("<h2>" + getLocale(AspxReturnAndPolicy, "Error Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Reuturn can not be filed for Downloadable Items '") + itemName + "  " + costVariants + "'." + "</p>");
                        }
                        if (itemTypeID == 3) {
                            csscody.error("<h2>" + getLocale(AspxReturnAndPolicy, "Error Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Reuturn can not be filed for Gift Cards '") + itemName + "  " + costVariants + "'." + "</p>");
                        }
                        if (itemTypeID == 4) {
                            csscody.error("<h2>" + getLocale(AspxReturnAndPolicy, "Error Message") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Reuturn can not be filed for Services '") + itemName + "  " + costVariants + "'." + "</p>");
                        }
                    }
                });

                if (count == "") {
                    csscody.alert("<h2>" + getLocale(AspxReturnAndPolicy, "Information Alert") + "</h2><p>" + getLocale(AspxReturnAndPolicy, "Please select at least one item before filing Return.") + "</p>");
                }

                ReturnsSubmit.LoadControlSame(orderID);
            },
            LoadControl: function() {
                var controlName = "Modules/AspxCommerce/AspxUserDashBoard/AccountDashboard.ascx";
                $.ajax({
                    type: "POST",
                    async: false,
                    url: AspxCommerce.utils.GetAspxServicePath() + "LoadControlHandler.aspx/Result",
                    data: "{ controlName:'" + AspxCommerce.utils.GetAspxRootPath() + controlName + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $('#divLoadUserControl').html(response.d);
                        AccountDashboard.GetMyOrders();
                    },
                    error: function() {
                        csscody.error('<h2>' + getLocale(AspxReturnAndPolicy, 'Error Message') + '</h2><p>' + getLocale(AspxReturnAndPolicy, 'Failed to load control!.') + '</p>');
                    }
                });
            },
            LoadControlSame: function(orderID) {
                var controlName = "Modules/AspxCommerce/AspxReturnAndPolicy/ReturnsSubmit.ascx";
                $.ajax({
                    type: "POST",
                    async: false,
                    url: AspxCommerce.utils.GetAspxServicePath() + "LoadControlHandler.aspx/Result",
                    data: "{ controlName:'" + AspxCommerce.utils.GetAspxRootPath() + controlName + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        $('#divLoadUserControl').html(response.d);
                        setTimeout(function () {
                            ReturnsSubmit.GetOrderDetails(orderID);
                        }, 500);
                    },
                    error: function() {
                        csscody.error('<h2>' + getLocale(AspxReturnAndPolicy, 'Error Message') + '</h2><p>' + getLocale(AspxReturnAndPolicy, 'Failed to load control!.') + '</p>');
                    }
                });
            },

            GetOrderDetails: function(orderID) {
                ReturnsSubmit.Vars.OrderID = orderID;
                ReturnsSubmit.GetAllOrderDetails(orderID);
            },
            GetAllOrderDetails: function(argus) {
                 orderId = argus;
                 this.config.method = "UserDashBoardHandler.ashx/GetMyOrderListForReturn";
                 this.config.url = moduleRootPath + this.config.method;
                this.config.data = JSON2.stringify({ orderID: orderId, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = ReturnsSubmit.BindMyOrders;
                this.ajaxCall(this.config);
            },
            BindMyOrders: function(msg) {
                var newVariantRow = '';
                var returnReason = '';
                var ItemCondition = '';
                var returnShippingAddressIds = '';
                returnReason = ReturnsSubmit.GetReturnReasonList();
                ItemCondition = ReturnsSubmit.GetProductStatusList();
                returnShippingAddressIds = ReturnsSubmit.BindUserAddress();
                $('#lblOrderID').html(orderId);
                var value;
                var length = msg.d.length;
                for (var index = 0; index < length; index++) {
                    value = msg.d[index];
                    newVariantRow += '<tr><td><input type="hidden" size="3" id="hdnIsReturnApplied" class="cssClassIsReturnApplied" value="' + value.IsReturnApplied + '"><input type="checkbox" id="chkSelect" class="cssClasschkSelect"></td>';
                    newVariantRow += '<td><input type="hidden" size="3" id="hdnOrderDate" class="cssClasshdnOrderDate" value="' + value.OrderDateCount + '"><input type="hidden" size="3" id="hdnItemID" class="cssClasshdnItemID" value="' + value.ItemID + '"><input type="hidden" size="3" id="hdnItemTypeID" class="cssClasshdnItemTypeID" value="' + value.ItemTypeID + '"><input type="hidden" size="3" id="hdnOrderID" class="cssClasshdnOrderID" value="' + value.OrderID + '"><input type="hidden" size="3" id="hdnCustomerID" class="cssClasshdnCustomerID" value="' + value.CustomerID + '"><input type="hidden" size="3" id="hdnAddressID" class="cssClasshdnCustomerID" value="' + value.AddressID + '"><label class="cssClasslblItemName">' + value.ItemName + '</label></td>';
                    newVariantRow += '<td><input type="hidden" id="hdnItemName" size="3" class="cssClasshdnItemName" value="' + value.ItemName + '"><input type="hidden" id="hdnCostVariants" size="3" class="cssClasshdnCostVariants" value="' + value.CostVariants + '"><input type="hidden"  id="hdnVariantID" size="3" class="cssClasshdnVariantID" value="' + value.VariantID + '"><label class="cssClasslblVariants">' + value.CostVariants + '</label></td>';
                    newVariantRow += '<td><input id="txtQuantity" class="sfinputbox" max =' + value.Quantity + ' value=' + value.Quantity + ' /><input type="hidden" id="hdnQuantity" value= "' + value.Quantity + '"/> </td>';
                    newVariantRow += returnReason;
                    newVariantRow += ItemCondition;
                    newVariantRow += '<td><textarea id="txtAreaOtherDetails" style="width: 100px; height: 50px;" rows="2" cols="15" class="cssClasstxtAreaOtherDetails"></textarea></td>';
                    newVariantRow += returnShippingAddressIds;
                    newVariantRow += '</td></tr>';
                };
                $("#divProductInfo").find('table>tbody').html(newVariantRow);
                $("#txtQuantity").ForceNumericOnly();

                if (newVariantRow == '') {
                    $("#divProductInfo").find('table').hide();
                    $("#divReturnHeading2").hide();
                    $("#divProductInfo").append("<p></br>" + getLocale(AspxReturnAndPolicy, "There is no item to return.") + "</br>" + getLocale(AspxReturnAndPolicy, "Return may be filed already for this Order") + "</p>");
                    $("#btnProceedReturn").parent('label').hide();
                }

            },
            GetSettings: function() {
                this.config.url = moduleRootPath + "UserDashBoardHandler.ashx/ReturnGetSettings";
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = ReturnsSubmit.GetSettingValue;
                this.ajaxCall(this.config);
            },
            GetSettingValue: function(msg) {
                var value;
                var length = msg.d.length;
                for (var index = 0; index < length; index++) {
                    value = msg.d[index];
                    ReturnsSubmit.Vars.ExpiresInDays = value.ExpiresInDays;
                };
            },
            GetProductStatusList: function() {
                var returnStatus = '';
                $.ajax({
                    type: "POST",
                    async: false,
                    url: moduleRootPath + "UserDashBoardHandler.ashx/BindProductStatusList",
                    contentType: "application/json; charset=utf-8",
                    data: JSON2.stringify({ aspxCommonObj: aspxCommonObj }),
                    dataType: 'json',
                    success: function(data) {
                        returnStatus += ' <td><select id="ddlProductStatus" class="cssClassddlReturnStatus">';
                        $.each(data.d, function(index, item) {
                            returnStatus += '<option value="' + item.Value + '">' + item.Text + '</option>';
                        });
                        returnStatus += ' </select></td>';
                    }

                });
                return returnStatus;
            },
            BindUserAddress: function() {
                var shipping = '';
                $.ajax({
                    type: "POST",
                    async: false,
                    url: this.config.baseURL + "AspxCoreHandler.ashx/GetAddressBookDetails",
                    contentType: "application/json; charset=utf-8",
                    data: JSON2.stringify({ aspxCommonObj: aspxCommonObj }),
                    dataType: 'json',
                    success: function(data) {

                        shipping += ' <td><select id="ddlShipping" class="cssClassddlShipping">';
                        $.each(data.d, function(index, item) {
                            var option = '';
                            option += item.FirstName + " " + item.LastName;
                            if (item.Address1 != "")
                                option += ", " + item.Address1;

                            if (item.City != "")
                                option += ", " + item.City;

                            if (item.State != "")
                                option += ", " + item.State;

                            if (item.Country != "")
                                option += ", " + item.Country;

                            if (item.Zip != "")
                                option += ", " + item.Zip;

                            if (item.Email != "")
                                option += ", " + item.Email;

                            if (item.Phone != "")
                                option += ", " + item.Phone;

                            if (item.Mobile != "")
                                option += ", " + item.Mobile;

                            if (item.Fax != "")
                                option += ", " + item.Fax;

                            if (item.Website != "")
                                option += ", " + item.Website;

                            if (item.Address2 != "")
                                option += ", " + item.Address2;

                            if (item.Company != "")
                                option += ", " + item.Company;
                            shipping += '<option value="' + item.AddressID + '">' + option + '</option>';
                        });
                        shipping += ' </select></td>';
                    }

                });
                return shipping;

            },

            GetReturnReasonList: function() {
                var returnReason = '';
                $.ajax({
                    type: "POST",
                    async: false,
                    url: moduleRootPath + "UserDashBoardHandler.ashx/BindReturnReasonList",
                    contentType: "application/json; charset=utf-8",
                    data: JSON2.stringify({ aspxCommonObj: aspxCommonObj }),
                    dataType: 'json',
                    success: function(data) {
                        returnReason += ' <td><select id="ddlReturnReason" class="cssClassddlReturnReason">';
                        $.each(data.d, function(index, item) {
                            returnReason += '<option value="' + item.Value + '">' + item.Text + '</option>';
                        });
                        returnReason += ' </select></td>';
                    }
                });
                return returnReason;
            }
        };
        ReturnsSubmit.Init();
    });

</script>

<div id="divReturnSubmit" class="cssClassdivReturnSubmit">
    <div id="divReturnHeading" class="cssClassdivReturnHeading">
        <h1>
            <span class="sfLocale cssClassReturnHeading ">Product Returns Form:</span></h1>
    </div>
    <div id="divReturnHeading2" class="cssClassdivReturnHeading2 cssClassTMar10 cssClassBMar10">
        <p class="sfLocale cssClassReturnHeading2">
            Please complete the form below to request return.</p>
    </div>  
  
</div>
<div id="divReturnInfo" class="sfFormwrapper">
    <div class="cssClassCommonBox Curve">
        <div id="divProductInfoHeading" class="cssClassHeader">
            <h3 class="sfLocale">
               <span class="sfLocale">Order ID: #
                <label class="cssClassLable" id="lblOrderID">
                </label></span> 
            </h3>
        </div>
        <div class="sfGridwrapper">
            <div id="divProductInfo" class="sfGridWrapperContent">
                <table class="sfGridWrapperTable" id="tblItemTable" cellspacing="0" cellpadding="0" border="0" width="100%">
                    <thead>
                        <tr class="cssClassHeading">
                            <th class="header sfLocale" style="background-image:none;">
                                <input type="checkbox" id="chkSelectAll" class="cssClasschkSelectAll" />
                            </th>
                            <th class="header sfLocale" style="background-image:none;">
                                Item Name
                            </th>
                            <th class="header sfLocale" style="background-image:none;">
                                Variants
                            </th>
                            <th class="header sfLocale" style="background-image:none;">
                                Qty
                            </th>
                            <th class="header sfLocale" style="background-image:none;">
                                Reason
                            </th>
                            <th classs="header sfLocale" style="background-image:none;">
                                Condition
                            </th>
                            <th class="header sfLocale" style="background-image:none;">
                                Other Faults
                            </th>
                            <th class="header sfLocale" style="background-image:none;">
                                Shipping Address
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divProceed" class="cssClassdivProceed">
    <label class="cssClassDarkBtn i-arrow-left"><button type="button" id="btnGoBack" class="cssClassbtnGoBack">
        <span class="sfLocale">Go Back</span></button></label>
    <label class="cssClassGreenBtn i-arrow-right"><button type="button" id="btnProceedReturn" class="cssClassbtnProceedReturn">
        <span class="sfLocale">Proceed Return</span></button></label>
</div>
<%--<div id="divLoadUserControl" class="cssClasMyAccountInformation">
    <div class="cssClassMyDashBoardInformation">
    </div>
</div>--%>
