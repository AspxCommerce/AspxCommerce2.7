$(function () {
    var aspxCommonObj = function () {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        return aspxCommonInfo;
    };
    customersManagement = {
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
                type: customersManagement.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: customersManagement.config.contentType,
                cache: customersManagement.config.cache,
                async: customersManagement.config.async,
                url: customersManagement.config.url,
                data: customersManagement.config.data,
                dataType: customersManagement.config.dataType,
                success: customersManagement.ajaxSuccess,
                error: customersManagement.ajaxFailure
            });
        },
        HideDiv: function () {
            $("#divCustomerList").hide();
            $("#divAddNewCustomer").hide();
        },
        ClearForm: function () {
            $(".fristName").val('');
            $(".lastName").val('');
            $(".email").val('');
            $(".userName").val('');
            $(".password").val('');
            $(".confirmPassword").val('');
            $(".question").val('');
            $(".answer").val('');
            $("#gdvCustomerDetails .attrChkbox").each(function (i) {
                $(this).removeAttr("checked");
            });
        },
        DeleteMultipleCustomer: function (_CustomerIDs) {
            this.config.url = this.config.baseURL + "DeleteMultipleCustomersByCustomerID";
            this.config.data = JSON2.stringify({ customerIDs: _CustomerIDs, aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        ConfirmDeleteMultipleCustomer: function (CustomerIDs, event) {
            if (event) {
                customersManagement.DeleteMultipleCustomer(CustomerIDs);
            }
        },
        DeleteSingleCustomer: function (_customerid) {
            this.config.url = this.config.baseURL + "DeleteCustomerByCustomerID";
            this.config.data = JSON2.stringify({ customerId: parseInt(_customerid), aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        DeleteShopCartByID: function (_shoppingID, confirm) {
            this.config.url = this.config.baseURL + "DeleteCustShoppingCartByShopID";
            this.config.data = JSON2.stringify({ shoppingID: _shoppingID, customerID: $("#selectedCustomerID").val(), aspxCommonObj: aspxCommonObj() });
            if (confirm) {
                this.config.ajaxCallMode = 5;
            }
            else {
                this.config.ajaxCallMode = 0;
            }
            this.ajaxCall(this.config);
        },
        DeleteWishlistByID: function (_wishID, _userName, confirm) {
            this.config.url = this.config.baseURL + "DeleteCustWishlistByWishID";
            this.config.data = JSON2.stringify({ wishID: _wishID, userName: _userName, aspxCommonObj: aspxCommonObj() });
            if (confirm) {
                this.config.ajaxCallMode = 4;
            }
            else {
                this.config.ajaxCallMode = 0;
            }
            this.ajaxCall(this.config);
        },
        ConfirmSingleDelete: function (_customerid, event) {
            if (event) {
                customersManagement.DeleteSingleCustomer(_customerid);
            }
            return false;
        },
        ConfirmWishDelete: function (_wishID, event, confirm) {
            if (event) {
                customersManagement.DeleteWishlistByID(_wishID, $('#selectedCustomerUserName').val(), confirm);
            }
            return false;
        },
        DeleteCustWishItems: function (tblID, argus) {
            switch (tblID) {
                case "gdvCustWishlist":
                    customersManagement.DeleteCustWishItem(argus[0]);
                    break;
                default:
                    break;
            }
        },
        DeleteCustWishItem: function (_wishitemid) {
            var properties = {
                onComplete: function (e) {
                    customersManagement.ConfirmWishDelete(_wishitemid, e, true);
                }
            };
                       csscody.confirm("<h2>" + getLocale(AspxCustomerManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCustomerManagement, "Are you sure you want to delete customers wish items?") + "</p>", properties);
        },
        ConfirmDeleteMultipleWishlistItems: function (WishlistItemIDs, event) {
            if (event) {
                customersManagement.ConfirmWishDelete(WishlistItemIDs, event, false);

                customersManagement.BindCustWishList();
                csscody.info('<h2>' + getLocale(AspxCustomerManagement, "Successful Message") + '</h2><p>' + getLocale(AspxCustomerManagement, "Wishlist Items have been deleted successfully.") + '</p>');
            }
        },
        ConfirmShopCartDelete: function (_shoppingID, event, confirm) {
            if (event) {
                customersManagement.DeleteShopCartByID(_shoppingID, confirm);
            }
            return false;
        },
        DeleteCustShopCarts: function (tblID, argus) {
            switch (tblID) {
                case "gdvCustShoppCartDetails":
                    customersManagement.DeleteCustShopCart(argus[0]);
                    break;
                default:
                    break;
            }
        },
        DeleteCustShopCart: function (_shopcartid) {
            var properties = {
                onComplete: function (e) {
                    customersManagement.ConfirmShopCartDelete(_shopcartid, e, true);
                }
            };
                       csscody.confirm("<h2>" + getLocale(AspxCustomerManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCustomerManagement, "Are you sure you want to delete customer's wish items?") + "</p>", properties);
        },
        ConfirmDeleteMultipleSelectedShop: function (SelectedShopIDs, event) {
            if (event) {
                customersManagement.ConfirmShopCartDelete(SelectedShopIDs, event, false);

                customersManagement.BindCustShoppingCart();
                csscody.info('<h2>' + getLocale(AspxCustomerManagement, "Successful Message") + '</h2><p>' + getLocale(AspxCustomerManagement, "Shopping Cart Items have been deleted successfully.") + '</p>');
            }
        },
        DeleteCustomer: function (_customerid) {
            var properties = {
                onComplete: function (e) {
                    customersManagement.ConfirmSingleDelete(_customerid, e);
                }
            };
                       csscody.confirm("<h2>" + getLocale(AspxCustomerManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCustomerManagement, "Are you sure you want to delete customer?") + "</p>", properties);
        },
        DeleteCustomers: function (tblID, argus) {
            switch (tblID) {
                case "gdvCustomerDetails":
                    if (argus[3].toLowerCase() != "yes") {
                        customersManagement.DeleteCustomer(argus[0]);
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxCustomerManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCustomerManagement, "Sorry! You can not delete yourself.") + '</p>');
                    }
                    break;
                default:
                    break;
            }
        },
        ViewCustomerDetails: function (tblID, argus) {
            switch (tblID) {
                case "gdvCustomerDetails":
                    customersManagement.config.url = customersManagement.config.baseURL + "GetCustomerDetailsByCustomerID";
                    customersManagement.config.data = JSON2.stringify({ customerID: argus[0], aspxCommonObj: aspxCommonObj() });
                    customersManagement.config.ajaxCallMode = 3;
                    customersManagement.ajaxCall(customersManagement.config);
                    $("#selectedCustomerID").val(argus[0]);
                    $("#selectedCustomerUserName").val(argus[3]);
                    customersManagement.BindCustShoppingCart();
                    customersManagement.BindCustWishList();
                    customersManagement.BindCustRecentOrders();
                    break;
                default:
                    break;
            }
        },
        SearchCustomer: function() {
            customersManagement.BindCustomerDetails();
        },
        BindCustShoppingCart: function () {
            this.config.url = this.config.baseURL;
            this.config.method = "GetCustomerShoppingCartByCustomerID";
            this.config.data = { customerID: parseInt($("#selectedCustomerID").val()), aspxCommonObj: aspxCommonObj() };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCustShoppCartDetails_pagesize").length > 0) ? $("#gdvCustShoppCartDetails_pagesize :selected").text() : 10;

            $("#gdvCustShoppCartDetails").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                        { display: 'Shopping Cart ID', name: 'CartItemID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkboxShoppingItems', elemDefault: false, controlclass: 'attribHeaderChkboxShoppingItems' },
                        { display: 'Item ID', name: 'ItemID', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'center' },
                        { display: 'Item Name', name: 'ItemName', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'center' },
                        { display: 'SKU', name: 'SKU', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'center' },
                        { display: 'Price', name: 'Price', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'center' },
                        { display: 'Quantity', name: 'Quantity', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'center' },
                         { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                    ],
                buttons: [
                    { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '1', callMethod: 'customersManagement.DeleteCustShopCarts', arguments: '' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 6: { sorter: false} }
            });
        },
        BindCustWishList: function () {
            this.config.url = this.config.baseURL;
            this.config.method = "GetCustomerWishListByCustomerID";
            this.config.data = { customerID: $("#selectedCustomerID").val(), aspxCommonObj: aspxCommonObj() };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCustWishlist_pagesize").length > 0) ? $("#gdvCustWishlist_pagesize :selected").text() : 10;

            $("#gdvCustWishlist").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                  { display: 'Wish ID', name: 'WishItemID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkboxWishList', elemDefault: false, controlclass: 'attribHeaderChkboxWishList' },
                    { display: 'Item ID', name: 'ItemID', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Item Name', name: 'ItemName', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'SKU', name: 'SKU', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Username', name: 'UserName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }

                    ],
                buttons: [
                { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '1', callMethod: 'customersManagement.DeleteCustWishItems', arguments: '0' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 5: { sorter: false} }
            });
        },
        BindCustRecentOrders: function () {
            this.config.url = this.config.baseURL;
            this.config.method = "GetCustomerRecentOrdersByCustomerID";
            this.config.data = { customerID: $("#selectedCustomerID").val(), aspxCommonObj: aspxCommonObj() };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCustRecentOrders_pagesize").length > 0) ? $("#gdvCustRecentOrders_pagesize :selected").text() : 10;

            $("#gdvCustRecentOrders").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                     { display: 'Order ID', name: 'OrderID', cssclass: 'cssClassHeadCheckBox', coltype: 'label', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
                     { display: 'Order Date', name: 'OrderDate', cssclass: 'cssClassHeadCheckBox', coltype: 'label', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
                        { display: 'Bill to Name', name: 'Billing_Address', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                        { display: 'Shipped Name', name: 'Shipping_Address', cssclass: 'cssClassHeadCheckBox', coltype: 'label', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
                        { display: 'Order Total', name: 'OrderTotal', cssclass: 'cssClassHeadCheckBox', coltype: 'label', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' }
                    ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 2: { sorter: false} }
            });
        },
        BindCustomerDetails: function () {
            this.config.url = this.config.baseURL;
            this.config.method = "GetCustomerDetails";
            this.config.data = { customerName: $('#txtSearchUserName1').val(), aspxCommonObj: aspxCommonObj() };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvCustomerDetails_pagesize").length > 0) ? $("#gdvCustomerDetails_pagesize :selected").text() : 10;
            $("#gdvCustomerDetails").sagegrid({
                url: this.config.url,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxCustomerManagement,'Customer ID'), name: 'Customer_ID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', checkFor: '5', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
                    { display: getLocale(AspxCustomerManagement,'Customer Name'), name: 'Customer_Name', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxCustomerManagement,'Culture Name'), name: 'Culture_Name', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxCustomerManagement,'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxCustomerManagement,'Updated On'), name: 'UpdatedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd' },
                    { display: getLocale(AspxCustomerManagement,'is Same User'), name: 'is_same_user', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No', hide: true },
                    { display: getLocale(AspxCustomerManagement,'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxCustomerManagement,'View'), name: 'view', enable: true, _event: 'click', trigger: '1', callMethod: 'customersManagement.ViewCustomerDetails', arguments: '1' },
					{ display: getLocale(AspxCustomerManagement,'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '1', callMethod: 'customersManagement.DeleteCustomers', arguments: '5' }

                ],
                rp: perpage,
                nomsg: getLocale(AspxCustomerManagement,"No Records Found!"),
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 6: { sorter: false} }
            });
        },
        BindCustPersonalInfo: function (data) {
            var custInfo = "";

            $.each(data, function (index, value) {
                custInfo += "<div class='sfUserAccQuickInfo clearfix'>"
                custInfo += "<p><label>Last Logged In </label><span>" + (value.LastLoggedIn != null ? value.LastLoggedIn : "No record found") + "</span></p>";
                custInfo += "<p><label>Account Created On </label><span>" + value.AccountCreatedOn + "</span></p>";
                custInfo += value.LifeTimeSale != null ? "<p><label>Lifetime Sales </label><span>" + value.LifeTimeSales + "</span></p>" : '';
                custInfo += value.AverageSales != '' && value.AverageSales != 'undefined' && value.AverageSales != null ? "<p><label>Average Sales </label><span>" + value.AverageSales + "</span></p>" : '';
                custInfo += "</div>"
                               custInfo += "<div class='sfBillingAdd'>";
                custInfo += "<h3>Billing Address</h3>";
                if (value.BillingAddress != null && value.BillingAddress.FirstName != "") {
                    custInfo += "<p><label>First Name </label><span>" + value.BillingAddress.FirstName + "</span></p>";
                    custInfo += "<p><label>Last Name </label><span>" + value.BillingAddress.LastName + "</span></p>";
                    custInfo += "<p><label>Address 1 </label><span>" + value.BillingAddress.Address1 + "</span></p>";
                    custInfo += "<p><label>Address 2 </label><span>" + value.BillingAddress.Address2 + "</span></p>";
                    custInfo += "<p><label>Company </label><span>" + value.BillingAddress.Company + "</span></p>";
                    custInfo += "<p><label>City </label><span>" + value.BillingAddress.City + "</span></p>";
                    custInfo += "<p><label>State </label><span>" + value.BillingAddress.State + "</span></p>";
                    custInfo += "<p><label>Country </label><span>" + value.BillingAddress.Country + "</span></p>";
                    custInfo += "<p><label>Zip </label><span>" + value.BillingAddress.Zip + "</span></p>";
                }
                else {
                    custInfo += "<span>"+getLocale(AspxCustomerManagement,'The customer does not have a default Billing address')+"</span>";
                }
                custInfo += "</div>"

                               custInfo += "<div class='sfShippingAdd'>"
                custInfo += "<h3>Shipping Address</h3>";
                if (value.ShippingAddress != null && value.ShippingAddress.FirstName != "") {
                    custInfo += "<p><label>First Name</label><span>" + value.ShippingAddress.FirstName + "</span></p>";
                    custInfo += "<p><label>Last Name</label><span>" + value.ShippingAddress.LastName + "</span></p>";
                    custInfo += "<p><label>Address 1</label><span>" + value.ShippingAddress.Address1 + "</span></p>";
                    custInfo += "<p><label>Address 2</label><span>" + value.ShippingAddress.Address2 + "</span></p>";
                    custInfo += "<p><label>Company</label><span>" + value.ShippingAddress.Company + "</span></p>";
                    custInfo += "<p><label>City</label><span>" + value.ShippingAddress.City + "</span></p>";
                    custInfo += "<p><label>State</label><span>" + value.ShippingAddress.State + "</span></p>";
                    custInfo += "<p><label>Country</label><span>" + value.ShippingAddress.Country + "</span></p>";
                    custInfo += "<p><label>Zip</label><span>" + value.ShippingAddress.Zip + "</span></p>";
                }
                else {
                    custInfo += "<span>"+getLocale(AspxCustomerManagement,'The customer does not have a default Shipping address')+"</span>";
                }
                custInfo += "</div>"
            });
            $("#divCustDetail").html(custInfo);
        },
        ajaxSuccess: function (msg) {
            switch (customersManagement.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    customersManagement.BindCustomerDetails();
                    csscody.info('<h2>' + getLocale(AspxCustomerManagement, "Successful Message") + '</h2><p>' + getLocale(AspxCustomerManagement, "Customer has been deleted successfully.") + '</p>');
                    $('#divAttribForm').hide();
                    $('#divAttribGrid').show();
                    break;
                case 2:
                    customersManagement.BindCustomerDetails();
                    csscody.info('<h2>' + getLocale(AspxCustomerManagement, "Successful Message") + '</h2><p>' + getLocale(AspxCustomerManagement, "Selected customer(s) has been deleted successfully.") + '</p>');
                    break;
                case 3:
                    customersManagement.BindCustPersonalInfo(msg);
                    $('#divCustomerList').hide();
                    $('#divCustomerInfo').show();
                    break;
                case 4:
                    customersManagement.BindCustWishList();
                    csscody.info('<h2>' + getLocale(AspxCustomerManagement, "Successful Message") + '</h2><p>' + getLocale(AspxCustomerManagement, "Wishlist Item has been deleted successfully.") + '</p>');
                    break;
                case 5:
                    customersManagement.BindCustShoppingCart();
                    csscody.info('<h2>' + getLocale(AspxCustomerManagement, "Successful Message") + '</h2><p>' + getLocale(AspxCustomerManagement, "Shopping Cart Item has been deleted successfully.") + '</p>');
                    break;
            }
        },
        ajaxFailure: function () {
            switch (customersManagement.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h2>' + getLocale(AspxCustomerManagement, "Error Message") + '</h2><p>' + getLocale(AspxCustomerManagement, "Failed to delete Customer!") + '</p>');
                    break;
                case 2:
                    csscody.error('<h2>' + getLocale(AspxCustomerManagement, "Error Message") + '</h2><p>' + getLocale(AspxCustomerManagement, "Failed to delete Customer!") + '</p>');
                    break;
            }
        },
        sucessMessage: function () {
            csscody.info("<h2>" + getLocale(AspxCustomerManagement, "Successful Message") + "</h2><p>" + getLocale(AspxCustomerManagement, "Customer has been created successfully.") + "</p>");
        },
        LoadCustomerRssImage: function () {
            var pageurl = aspxRedirectPath + rssFeedUrl + pageExtension;
            $('#customerRssImage').parent('a').show();
            $('#customerRssImage').parent('a').removeAttr('href').prop('href', pageurl + '?type=rss&action=newcustomers');
            $('#customerRssImage').removeAttr('src').prop('src', aspxTemplateFolderPath + '/images/rss-icon.png');
            $('#customerRssImage').removeAttr('title').prop('title', 'New Customer Rss Feed');
            $('#customerRssImage').removeAttr('alt').prop('alt', 'New Customer Rss Feed');
        },
        init: function (config) {
            if (checkIfSucccess == 1) {
                customersManagement.BindCustomerDetails();
                customersManagement.HideDiv();
                $("#divCustomerList").show();
            } else {
                customersManagement.HideDiv();
                $("#divAddNewCustomer").show();
                customersManagement.ClearForm();
                return false;
            }
            $("#divCustomerList").show();
            $("#btnAddNewCustomer").click(function () {
                customersManagement.HideDiv();
                $("#divAddNewCustomer").show();
                customersManagement.ClearForm();
            });
            $("#btnBack").click(function () {
                customersManagement.HideDiv();
                $("#divCustomerList").show();
            });
            $("#btnPersonalInfoBack").click(function () {
                $("#divCustomerInfo").hide();
                $("#divCustomerList").show();
            });
            $('#btnDeleteSelectedCustomer').click(function () {
                var CustomerIDs = '';
                CustomerIDs = SageData.Get("gdvCustomerDetails").Arr.join(',');
                if (CustomerIDs.length>0) {
                    var properties = {
                        onComplete: function (e) {
                            customersManagement.ConfirmDeleteMultipleCustomer(CustomerIDs, e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxCustomerManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCustomerManagement, "Are you sure you want to delete the selected customer(s)?") + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxCustomerManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCustomerManagement, "Please select at least one customer before delete.") + '</p>');
                }
            });
            $('#btnDeleteSelectedShop').click(function () {
                var SelectedShopIDs = '';
                SelectedShopIDs = SageData.Get("gdvCustShoppCartDetails").Arr.join(',');
                if (SelectedShopIDs.length > 0) {
                    var properties = {
                        onComplete: function (e) {
                            customersManagement.ConfirmDeleteMultipleSelectedShop(SelectedShopIDs, e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxCustomerManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCustomerManagement, "Are you sure you want to delete the selected shopping cart items(s)?") + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxCustomerManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCustomerManagement, "Please select at least one shopping cart item before delete.") + '</p>');
                }
            });
            $("#btnSearchRegisteredUser").click(function () {
                customersManagement.SearchCustomer();
            });
            $('#btnDeleteSectedWish').click(function () {
                var WishListIDs = '';
                var WishListIDs = '';
                WishListIDs = SageData.Get("gdvCustWishlist").Arr.join(',');
                if (WishListIDs.length > 0) {
                    var properties = {
                        onComplete: function (e) {
                            customersManagement.ConfirmDeleteMultipleWishlistItems(WishListIDs, e);
                        }
                    }
                    csscody.confirm("<h2>" + getLocale(AspxCustomerManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxCustomerManagement, "Are you sure you want to delete the selected wishlist items(s)?") + "</p>", properties);
                } else {
                    csscody.alert('<h2>' + getLocale(AspxCustomerManagement, "Information Alert") + '</h2><p>' + getLocale(AspxCustomerManagement, "Please select at least one wishlist item before delete.") + '</p>');
                }
            });
            if (newCustomerRss.toLowerCase() == 'true') {
                customersManagement.LoadCustomerRssImage();
            }
        }
    };
    customersManagement.init();

       var qCustomerID = strDecrypt(getParameterByName("customerID"));
    var qUserName = strDecrypt(getParameterByName("userName"));
    if (qCustomerID.toString() != '' && qCustomerID.toString() != 'NaN' && qUserName != '') {

        var arguments = new Array();
        arguments[0] = parseInt(qCustomerID);
        arguments[3] = qUserName;

        customersManagement.ViewCustomerDetails("gdvCustomerDetails", arguments);
    }
});

