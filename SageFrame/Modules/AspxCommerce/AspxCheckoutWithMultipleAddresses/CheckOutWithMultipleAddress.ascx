<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckOutWithMultipleAddress.ascx.cs"
    Inherits="Modules_AspxCheckoutInformationContent_CheckOutWithMultipleAddress" %>

<script type="text/javascript">
    //<![CDATA[
    var CheckOut;
    $(function() {

        $(".sfLocale").localize({
            moduleKey: AspxCheckoutWithMultipleAddresses
        });

        var cartItemCount = 0;
        CheckOut = {

            BillingAddress: {
                AddressID: 0,
                FirstName: "",
                LastName: "",
                CompanyName: "",
                EmailAddress: "",
                Address: "",
                Address2: "",
                City: "",
                State: "",
                Zip: "",
                Country: "",
                Phone: "",
                Mobile: "",
                Fax: "",
                Website: "",
                IsDefaultBilling: false,
                IsBillingAsShipping: false
            },
            ShippingAddress: {
                AddressID: 0,
                isDefaultShipping: false
            },
            Vars: {
                GatewayName: "",
                SpCost: 0
            },
            UserCart: {
                SelectedVals: "",
                hideTD: false,
                CartID: 0,
                itemID: 1,
                isActive: true,
                amount: 0,
                SC: 0,
                paymentMethodName: "",
                paymentMethodCode: "",
                ddlhide: [],
                lstItems: [],
                spMethodID: 0,
                IsFShipping: '<%=IsFShipping %>',
                TotalDiscount: '<%=Discount %>',
                CartDiscount: 0,
                Tax: 0,
                IsDownloadItemInCart: false,
                IsDownloadItemInCartFull: false,
                CountDownloadableItem: 0,
                CountAllItem: 0,
                Itemtype: "",
                couponCode: '<%=CouponCode %>'
            }, config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath(),
                method: "",
                url: "",
                ajaxCallMode: 0, ///0 for get categories and bind, 1 for notification,2 for versions bind
                error: 0,
                sessionValue: ""
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: CheckOut.config.type,
                    contentType: CheckOut.config.contentType,
                    cache: CheckOut.config.cache,
                    async: CheckOut.config.async,
                    url: CheckOut.config.url,
                    data: CheckOut.config.data,
                    dataType: CheckOut.config.dataType,
                    success: CheckOut.ajaxSuccess,
                    error: CheckOut.ajaxFailure
                });
            }
            , CheckDownloadableOnlyInCart: function() {
                if (CheckOut.UserCart.CountDownloadableItem != 0) {
                    if (CheckOut.UserCart.CountDownloadableItem == CheckOut.UserCart.CountAllItem) {
                        CheckOut.UserCart.IsDownloadItemInCartFull = true;
                        $('.sfGridWrapperContent h3').hide();
                    }
                }
                else {
                    CheckOut.UserCart.IsDownloadItemInCartFull = false;
                }

            },
            GetStateList: function(countryCode) {
                this.config.method = "AspxCommerceWebService.asmx/BindStateList";
                this.config.url = this.config.baseURL + this.config.method;
                //this.config.async = false;
                this.config.data = JSON2.stringify({ countryCode: countryCode });
                this.config.ajaxCallMode = 13;
                this.config.error = 13;
                this.ajaxCall(this.config);
            },
            GetDiscountCartPriceRule: function(SpCost) {
                CheckOut.Vars.SpCost = SpCost;
                this.config.method = "AspxCommerceWebService.asmx/getCartID";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), customerID: AspxCommerce.utils.GetCustomerID(), sessionCode: AspxCommerce.utils.GetSessionCode() });
                this.config.ajaxCallMode = 11;
                this.config.error = 11;
                this.config.async = false;
                this.ajaxCall(this.config);
            },
            AssignShippingCost: function() {
                $("select[class='cssClassShippingMethod']").bind("change", function() {
                    var spCost = $($(this)).find('option:selected').text().split(')');
                    var c = spCost[1].split('(');
                    $(this).closest('tr').find("input[class='cssClassShippingCost']").val('').val(c[1]);
                    $(this).closest('tr').find("input[class='cssClassShippingMethodForID']").val('').val($(this).val());
                });
            },
            billingChange: function() {
                $("a[name='billingChange']").bind("click", function() {
                    var address = $('#prBillingAddressInfo').text();
                    var addr = address.split(',');
                    var Name = addr[0].split(' ');
                    var span = '';
                    Array.prototype.clean = function(deleteValue) {
                        for (var i = 0; i < this.length; i++) {
                            if (this[i] == deleteValue) {
                                this.splice(i, 1);
                                i--;
                            }
                        }
                        return this;
                    };
                    Name.clean("");
                    CheckOut.ClearAll();
                    $("#hdnAddressID").val($('#prBillingAddressInfo').find('span').attr('id'));
                    $("#txtFirstName").val($.trim(Name[0]));
                    $("#txtLastName").val($.trim(Name[1]));
                    $("#txtCompanyName").val($.trim(addr[12]));
                    $("#txtEmailAddress").val($.trim(addr[6]));
                    $("#txtAddress1").val($.trim(addr[1]));
                    $("#txtAddress2").val($.trim(addr[11]));
                    $("#txtCity").val($.trim(addr[2]));
                    $("#txtState").val($.trim(addr[3]));
                    $("#txtZip").val($.trim(addr[5]));
                    $('#ddlCountry').val($.trim(addr[4]));
                    $("#txtPhone").val($.trim(addr[7]));
                    $("#txtMobile").val($.trim(addr[8]));
                    $("#txtFax").val($.trim(addr[9]));
                    $("#txtWebsite").val($.trim(addr[10]));

                    ShowPopup(this);
                });
            },
            shippingChange: function() {
                $("a[name='shippingChange']").bind("click", function() {
                    var address = $(this).closest('tr').find('span[class="cssClassShippingAddressInfo"]').text();
                    var addr = address.split(',');
                    var Name = addr[0].split(' ');

                    Array.prototype.clean = function(deleteValue) {
                        for (var i = 0; i < this.length; i++) {
                            if (this[i] == deleteValue) {
                                this.splice(i, 1);
                                i--;
                            }
                        }
                        return this;
                    };
                    Name.clean("");
                    CheckOut.ClearAll();
                    $("#hdnAddressID").val($(this).closest('tr').find('span[class="cssClassShippingAddressInfo"]').attr('title'));
                    $("#txtFirstName").val($.trim(Name[0]));
                    $("#txtLastName").val($.trim(Name[1]));
                    $("#txtCompanyName").val($.trim(addr[12]));
                    $("#txtEmailAddress").val($.trim(addr[6]));
                    $("#txtAddress1").val($.trim(addr[1]));
                    $("#txtAddress2").val($.trim(addr[11]));
                    $("#txtCity").val($.trim(addr[2]));
                    $("#txtState").val($.trim(addr[3]));
                    $("#txtZip").val($.trim(addr[5]));
                    $('#ddlCountry').val($.trim(addr[4]));
                    $("#txtPhone").val($.trim(addr[7]));
                    $("#txtMobile").val($.trim(addr[8]));
                    $("#txtFax").val($.trim(addr[9]));
                    $("#txtWebsite").val($.trim(addr[10]));
                    ShowPopup(this);
                });
            },
            BindCartItems: function() {
                this.config.method = "AspxCommerceWebService.asmx/GetShippingCostByItem";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.async = false;
                this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), customerID: AspxCommerce.utils.GetCustomerID(), sessionCode: AspxCommerce.utils.GetSessionCode(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName() });
                this.config.ajaxCallMode = 10;
                this.config.error = 10;
                this.ajaxCall(this.config);
            },
            QuantitityDiscountAmount: function() {
                this.config.method = "AspxCommerceWebService.asmx/GetDiscountQuantityAmount";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), userName: AspxCommerce.utils.GetUserName(), customerID: AspxCommerce.utils.GetCustomerID(), cultureName: AspxCommerce.utils.GetCultureName(), sessionCode: AspxCommerce.utils.GetSessionCode() });
                this.config.ajaxCallMode = 8;
                this.config.error = 8;
                this.ajaxCall(this.config);
            },
            BindCreateElements: function() {
                CheckOut.UserCart.SC = 0;
                var arr = new Array();
                var arrAddress = new Array();
                var arrAddressID = new Array();
                $('#dvColumnSet').html('');
                var ShippingMethodID = new Array();
                var select = $('.userAddress option:selected');
                $.each(select, function(index, item) {
                    if (jQuery.inArray($(this).val(), arr) == -1) {
                        arr.push($(this).val());
                        arrAddress.push($(this).text());
                    }
                    arrAddressID.push($(this).val());
                });
                Array.prototype.count = function(count) {
                    var cc = 0;
                    for (var i = 0; i < this.length; i++) {
                        if (this[i] != count) {
                            cc++;
                        }
                    }
                    return cc;
                };
                var countItem = 0;
                $.each(arr, function(index, item) {
                    var itemiD = 'tbl' + index + '';
                    var shippingID = 'dvSPM' + index + '';
                    var newItemNAddressRow = '';

                    if (item == 0) {
                        newItemNAddressRow += '<div class="columnSet"><h3>' + getLocale(AspxCheckoutWithMultipleAddresses, 'Your Digital Items') + '</h3><table width="100%" cellspacing="0" cellpadding="0" border="0" class="cssClassTBLBorderNone">';
                        newItemNAddressRow += '<tbody><tr><td width="31%" valign="top">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Ditital Items') + '</td>';
                        newItemNAddressRow += '<td width="2%" valign="top">&nbsp;</td>';
                        newItemNAddressRow += '<td width="69%" valign="top"><p>' + getLocale(AspxCheckoutWithMultipleAddresses, 'Items') + '<a class="cssClassChange" href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'My-Cart' + pageExtension + '">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Edit Items') + '</a></p>';
                        newItemNAddressRow += '<div class="cssClassEditItemBox">';

                        newItemNAddressRow += '<table id="' + itemiD + '" width="100%" cellspacing="0" cellpadding="0" border="0">';
                        newItemNAddressRow += '<thead class="cssClassHeaderTitle"><tr><td class="cssClassShippingHeading">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Item') + '</td><td class="cssClassShippingHeading">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Quantity') + '</td><td class="cssClassShippingHeading">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Unit Price') + '</td><td class="cssClassShippingHeading">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Sub Total') + '</td></tr></thead>';
                        newItemNAddressRow += '<tbody><tr class="cssClassHeaderTitle"></tr></tbody></table></div></td></tr>';
                        newItemNAddressRow += '</tbody></table></div>';

                    } else {
                        newItemNAddressRow += '<div class="columnSet"><h3>Address ' + eval(countItem + 1) + ' of ' + arr.count(0) + '</h3><table width="100%" cellspacing="0" cellpadding="0" border="0" class="cssClassTBLBorderNone">';
                        newItemNAddressRow += '<tbody><tr><td width="31%" valign="top"><p><span class="cssClassShippingHeading">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Shipping to') + '</span> <a class="cssClassChange" rel="popuprel" name="shippingChange" href="#">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Change') + '</a></p>';

                        newItemNAddressRow += '<span id="spn' + index + '" title="" class="cssClassShippingAddressInfo"></span><div class="cssClassShippingMethodBox">';
                        newItemNAddressRow += '<span class="cssClassShippingHeading">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Shipping Method') + '</span><br/><span id="' + shippingID + '">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Total Shipping Cost for these items : ') + '</span></td>';

                        newItemNAddressRow += '<td width="2%" valign="top">&nbsp;</td>';
                        newItemNAddressRow += '<td width="69%" valign="top"><p>' + getLocale(AspxCheckoutWithMultipleAddresses, 'Items') + '<a class="cssClassChange" href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'My-Cart' + pageExtension + '">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Edit Items') + '</a></p>';
                        newItemNAddressRow += '<div class="cssClassEditItemBox">';
                        newItemNAddressRow += '<table id="' + itemiD + '" width="100%" cellspacing="0" cellpadding="0" border="0">';
                        newItemNAddressRow += '<thead class="cssClassHeaderTitle"><tr><td class="cssClassShippingHeading">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Item') + '</td><td class="cssClassShippingHeading">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Quantity') + '</td><td class="cssClassShippingHeading">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Unit Price') + '</td><td class="cssClassShippingHeading">' + getLocale(AspxCheckoutWithMultipleAddresses, 'Sub Total') + '</td></tr></thead>';
                        newItemNAddressRow += '<tbody><tr class="cssClassHeaderTitle"></tr></tbody></table></div></td></tr>';
                        newItemNAddressRow += '</tbody></table></div>';
                        countItem++;
                    }
                    $("#dvColumnSet").append(newItemNAddressRow);
                    $("#spn" + index).html(arrAddress[index]);
                    $("#spn" + index).attr('title', arrAddressID[index]);

                    $spnBillingaddress = $("#spn" + index);

                    CheckOut.shippingChange();

                    var newItemRow = '';
                    var spTotalCost = 0;
                    var subTotalCost = 0;
                    var weight = 0;
                    var costvariants = '';

                    $.each(select, function(i, it) {
                        if ($(this).val() == item) {
                            if ($(this).attr("Downloadable") == 1) {
                                CheckOut.UserCart.spMethodID = 0;
                                CheckOut.UserCart.weight = 0;
                                CheckOut.UserCart.costvariants = 0;
                                CheckOut.UserCart.hideTD = true;
                            }
                            else {
                                if ($(this).closest('tr').find("input[class='cssClassShippingMethodForID']").val() > 0)
                                    CheckOut.UserCart.spMethodID = $(this).closest('tr').find("input[class='cssClassShippingMethodForID']").val();

                                if ($(this).closest('tr').find("input[class='cssClassItemWeight']").val() > 0)
                                    CheckOut.UserCart.weight = $(this).closest('tr').find("input[class='cssClassItemWeight']").val();

                                if ($(this).closest('tr').find("input[class='cssClassVariants']").val() != undefined) {
                                    CheckOut.UserCart.costvariants = $(this).closest('tr').find("input[class='cssClassVariants']").val();
                                } else { CheckOut.UserCart.costvariants = 0; }
                            }
                            newItemRow += '<tr><td><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + $(this).closest('tr').find('a').attr('sku') + pageExtension + '" class="cssClassItem">' + $(this).closest('tr').find('a').text() + '</a>';
                            newItemRow += '<input type="hidden" size="3" value=' + $(this).closest('tr').find("input[type='hidden']").val() + ' class="cssClassItmID"></td>';
                            newItemRow += '<td><span class="cssClassQty">' + $(this).closest('tr').find("input[type='text']").val() + '</span>';
                            newItemRow += '<td><span class="cssClassIC cssClassFormatCurrency">' + $(this).closest('tr').find("input[class='cssClassItemCost']").val() + '</span>';
                            newItemRow += '<td><span class="cssClassSTC cssClassFormatCurrency">' + $(this).closest('tr').find("input[class='cssClassSubTotalCost']").val() + '</span>';
                            newItemRow += '</td></tr>';
                            spTotalCost = parseFloat(spTotalCost) + parseFloat($(this).closest('tr').find("input[class='cssClassShippingCost']").val().replace(/[^0-9\.]+/g, ""));
                            subTotalCost = parseFloat(subTotalCost) + parseFloat($(this).closest('tr').find("input[class='cssClassSubTotalCost']").val().replace(/[^0-9\.]+/g, ""));


                            CheckOut.UserCart.Itemtype = $(this).closest('tr').find("input[type='hidden']").attr('itemtype');

                            if (parseInt(CheckOut.UserCart.Itemtype) == 2) {
                                CheckOut.UserCart.lstItems[i] = { "OrderID": 0, "ShippingAddressID": 0, "ShippingMethodID": 0, "ItemID": $(this).closest('tr').find("input[type='hidden']").val(), "Variants": costvariants, "Quantity": $(this).closest('tr').find("input[type='text']").val(), "Price": $(this).closest('tr').find("input[class='cssClassItemCost']").val(), "Weight": 0, "Remarks": "orderItemRemarks", "ShippingRate": 0, 'IsDownloadable': true };

                            } else {

                                CheckOut.UserCart.lstItems[i] = { "OrderID": 0, "ShippingAddressID": arrAddressID[i], "ShippingMethodID": CheckOut.UserCart.spMethodID, "ItemID": $(this).closest('tr').find("input[type='hidden']").val(), "Variants": costvariants, "Quantity": $(this).closest('tr').find("input[type='text']").val(), "Price": $(this).closest('tr').find("input[class='cssClassItemCost']").val(), "Weight": CheckOut.UserCart.weight, "Remarks": "orderItemRemarks", "ShippingRate": $(this).closest('tr').find("input[class='cssClassShippingCost']").val(), 'IsDownloadable': false };
                            }
                        }
                    });
                    if (CheckOut.UserCart.IsFShipping.toLowerCase() == 'true') {

                        $('.cssClassShippingMethodBox span:odd').html('').html(getLocale(AspxCheckoutWithMultipleAddresses, "Total Shipping Cost for these items : 0.00(freeShipping)"));
                        $('.cssSPCost strong').html('0');
                        spTotalCost = 0;
                    }
                    subTotalCost = parseFloat(spTotalCost) + parseFloat(subTotalCost);

                    newItemRow += '<tr><td></td><td></td><td><span class="cssClassShippingHeading" class="cssClasslabel">' + getLocale(AspxCheckoutWithMultipleAddresses, "Shipping Total:") + '</span></td><td><span class="cssSPCost cssClassFormatCurrency"><strong>' + spTotalCost.toFixed(2) + '</strong></span></td></tr>';

                    newItemRow += '<tr><td></td><td></td><td><span class="cssClassShippingHeading" class="cssClasslabel">' + getLocale(AspxCheckoutWithMultipleAddresses, "Grand SubTotal:") + '</span></td><td><span class="cssGrandSubTotal cssClassFormatCurrency"><strong>' + subTotalCost.toFixed(2) + '</strong></span></td></tr>';
                    $('#' + itemiD + '>tbody').html('');
                    $('#' + itemiD + '>tbody').append(newItemRow);
                    $('#' + shippingID + '').html('');
                    $('#' + shippingID + '').append(spTotalCost);

                    CheckOut.UserCart.SC += spTotalCost;
                    CheckOut.SetSessionValue("ShippingCostAll", CheckOut.UserCart.SC);
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                });
            },
            GetCartItemTotalCount: function() {
                this.config.url = this.config.baseURL + "AspxCommerceWebService.asmx/GetCartItemsCount";
                this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), customerID: AspxCommerce.utils.GetCustomerID(), sessionCode: AspxCommerce.utils.GetSessionCode(), userName: AspxCommerce.utils.GetUserName() });
                this.config.ajaxCallMode = 33;
                this.ajaxCall(this.config);
            }
            ,
            BindUserAddress: function() {
                this.config.method = "AspxCommerceWebService.asmx/GetAddressBookDetails";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), customerID: AspxCommerce.utils.GetCustomerID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName() });
                this.config.ajaxCallMode = 7;
                this.config.error = 7;
                this.ajaxCall(this.config);
            },
            BindBillingAddress: function() {
                this.config.method = "AspxCommerceWebService.asmx/GetAddressBookDetails";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), customerID: AspxCommerce.utils.GetCustomerID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName() });
                this.config.ajaxCallMode = 6;
                this.config.error = 6;
                this.ajaxCall(this.config);
            },

            GetAllCountry: function() {
                this.config.method = "AspxCommerceWebService.asmx/BindCountryList";
                this.config.url = this.config.baseURL + this.config.method;
                //this.config.async = false;
                //this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), customerID: AspxCommerce.utils.GetCustomerID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName(), sessionCode: AspxCommerce.utils.GetSessionCode() });
                this.config.ajaxCallMode = 5;
                this.config.error = 5;
                this.ajaxCall(this.config);
            },

            AddUpdateUserAddress: function() {
                var addressId = $("#hdnAddressID").val();
                var firstName = $("#txtFirstName").val();
                var lastName = $("#txtLastName").val();
                var email = $("#txtEmailAddress").val();
                var company = $("#txtCompanyName").val();
                var address1 = $("#txtAddress1").val();
                var address2 = $("#txtAddress2").val();
                var city = $("#txtCity").val();
                var state = ''; var state = '';
                if ($("#ddlCountry :selected").text() == 'United States') {
                    state = $("#ddlUSState :selected").text();
                }
                else {
                    state = $("#txtState").val();
                }
                var zip = $("#txtZip").val();
                var phone = $("#txtPhone").val();
                var mobile = $("#txtMobile").val();
                var fax = $("#txtFax").val();
                var webSite = $("#txtWebsite").val();
                var countryName = $("#ddlCountry :selected").text();
                var isDefaultShipping = $("#chkShippingAddress").attr("checked");
                var isDefaultBilling = $("#chkBillingAddress").attr("checked");
                this.config.method = "AspxCommerceWebService.asmx/AddUpdateUserAddress";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ addressID: addressId, customerID: AspxCommerce.utils.GetCustomerID(), firstName: firstName, lastName: lastName, email: email, company: company, address1: address1, address2: address2,
                    city: city, state: state, zip: zip, phone: phone, mobile: mobile, fax: fax, webSite: webSite, countryName: countryName, isDefaultShipping: isDefaultShipping, isDefaultBilling: isDefaultBilling, storeID: AspxCommerce.utils.GetStoreID(),
                    portalID: AspxCommerce.utils.GetPortalID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName()
                });
                this.config.ajaxCallMode = 4;
                this.config.error = 4;
                this.ajaxCall(this.config);


                //                $.ajax({
                //                    type: "POST",
                //                    url: aspxservicePath + "AspxCommerceWebService.asmx/AddUpdateUserAddress",
                //                    data: JSON2.stringify({ addressID: addressId, customerID: customerId, firstName: firstName, lastName: lastName, email: email, company: company, address1: address1, address2: address2,
                //                        city: city, state: state, zip: zip, phone: phone, mobile: mobile, fax: fax, webSite: webSite, countryName: countryName, isDefaultShipping: isDefaultShipping, isDefaultBilling: isDefaultBilling, storeID: storeId,
                //                        portalID: portalId, userName: userName, cultureName: cultureName
                //                    }),
                //                    contentType: "application/json;charset=utf-8",
                //                    dataType: "json",
                //                    success: function() {
                //                        BindUserAddress();
                //                        BindBillingAddress();
                //                        $('#fade, #popuprel').fadeOut();
                //                    }
                //                    //            ,
                //                    //            error: function() {
                //                    //                alert("update error");
                //                    //            }

                //                });
            },

            ClearAll: function() {
                $("#hdnAddressID").val(0);
                $("#txtFirstName").val('');
                $("#txtLastName").val('');
                $("#txtEmailAddress").val('');
                $("#txtCompanyName").val('');
                $("#txtAddress1").val('');
                $("#txtAddress2").val('');
                $("#txtCity").val('');
                $("#txtState").val('');
                $("#ddlcountry").val(1);
                $("#txtZip").val('');
                $("#txtPhone").val('');
                $("#txtMobile").val('');
                $("#txtFax").val('');
                $("#txtWebsite").val('');
                //$(".error").hide();
            },

            SetSessionValue: function(sessionKey, sessionValue) {
                this.config.method = "AspxCommerceWebService.asmx/SetSessionVariable";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ key: sessionKey, value: sessionValue });
                this.config.ajaxCallMode = 2;
                this.config.error = 2;
                this.ajaxCall(this.config);
            },
            LoadPGatewayList: function() {
                this.config.method = "AspxCommerceWebService.asmx/GetPGList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), cultureName: AspxCommerce.utils.GetCultureName() });
                this.config.ajaxCallMode = 3;
                this.config.error = 3;
                this.ajaxCall(this.config);
            },
            LoadControl: function(ControlName, Name) {
                CheckOut.Vars.GatewayName = Name;
                this.config.method = "LoadControlHandler.aspx/Result";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = "{ controlName:'" + AspxCommerce.utils.GetAspxRootPath() + ControlName + "'}";
                this.config.ajaxCallMode = 21;
                this.config.error = 21;
                this.ajaxCall(this.config);

            }, ajaxFailure: function() {
                switch (CheckOut.config.error) {
                }
            },
            ajaxSuccess: function(data) {
                switch (CheckOut.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 33:
                        cartItemCount = data.d;
                        break;
                    case 21: //load control                      
                        if (CheckOut.Vars.GatewayName.toLowerCase() == 'aimauthorize') {
                            $('#dvCheque').remove();
                            $('#creditCard').remove();
                            $('#AIMChild').remove();
                            $('#dvPGList input[type="button"]').remove();
                            $('#dvPGList ').append(data.d);
                            var button = $('#dvPGList').find("input[type=button]");
                            $('#dvPGList input[type="button"]').remove();

                            $('#placeOrder .sfButtonwrapper input[type="submit"]:last').remove();
                            $('#placeOrder .sfButtonwrapper ').find('input').not("#btnPlaceOrderBack").remove();
                            $('#placeOrder .sfButtonwrapper').append(button);
                            $('#placeOrder .sfButtonwrapper div ').remove();
                        } else {
                            $('#placeOrder .sfButtonwrapper input[type="submit"]:last').remove();
                            $('#placeOrder .sfButtonwrapper ').find('input').not("#btnPlaceOrderBack").remove();
                            $('#placeOrder .sfButtonwrapper').append(data.d);
                            $('#placeOrder .sfButtonwrapper div ').remove();
                        }
                        break;
                    case 2: //get session variable
                        CheckOut.config.sessionValue = parseFloat(data.d);
                        break;
                    case 3:
                        if (data.d.length > 0) {
                            $.each(data.d, function(index, item) {
                                //<input id="rdbCheck" type="radio" name="paymentType" class="cssClassRadioBtn" /><b><label> Check/MoneyOrder</label></b><br />
                                //_type":"PaymentGatewayListInfo:#AspxCommerce.Core","_ControlSource":"Modules\/AspxCommerce\/PayPal\/paypal.ascx","_PaymentGatewayTypeID":2,"_PaymentGatewayTypeName":"PayPal"}]}

                                $(' #dvPGList').append('<input id="rdb' + item.PaymentGatewayTypeName + '" name="PGLIST" type="radio" friendlyname="' + item.FriendlyName + '" source="' + item.ControlSource + '" value="' + item.PaymentGatewayTypeID + '" class="cssClassRadioBtn" /><b><label> ' + item.PaymentGatewayTypeName + '</label></b><br />');

                            });
                            $('#dvPGList input[name="PGLIST"]').bind("click", function() {
                                CheckOut.SetSessionValue("Gateway", $(this).attr('value'));
                                if ('aimauthorize' == $.trim($(this).attr('friendlyname').toLowerCase())) {

                                    CheckOut.LoadControl($(this).attr('source'), $.trim($(this).attr('friendlyname')));
                                }
                                else {
                                    CheckOut.LoadControl($(this).attr('source'), $(this).attr('friendlyname'));
                                    $('#dvCheque').hide();
                                    $('#creditCard').hide();
                                    $('#AIMChild').hide();
                                    $('#dvPGList input[type="button"]').remove();
                                }
                            });
                        }
                        break;
                    case 4: //add update address
                        RemovePopUp();
                        CheckOut.BindUserAddress();
                        CheckOut.BindBillingAddress();

                        break;
                    case 5: // bind country
                        var countryElements = '';
                        $.each(data.d, function(index, value) {
                            countryElements += '<option value=' + value.Value + '>' + value.Text + '</option>';
                        });
                        $("#ddlCountry").html('');
                        $("#ddlCountry").html(countryElements);
                        break;
                    case 6: //bind billing address
                        var span = '';
                        if (data.d.length > 0) {
                            $.each(data.d, function(index, item) {
                                if (item.DefaultBilling == 1) {
                                    // billingAddressID = item.AddressID;
                                    CheckOut.BillingAddress.addressID = item.AddressID;
                                    span += "<span id=" + item.AddressID + "> ";
                                    span += item.FirstName.replace(",", "-") + " " + item.LastName.replace(",", "-");
                                    if (item.Address1 != "")
                                        span += ", " + item.Address1.replace(",", "-");

                                    if (item.City != "")
                                        span += ", " + item.City.replace(",", "-");

                                    if (item.State != "")
                                        span += ", " + item.State.replace(",", "-");

                                    if (item.Country != "")
                                        span += ", " + item.Country.replace(",", "-");

                                    if (item.Zip != "")
                                        span += ", " + item.Zip.replace(",", "-");

                                    if (item.Email != "")
                                        span += ", " + item.Email.replace(",", "-");

                                    if (item.Phone != "")
                                        span += ", " + item.Phone.replace(",", "-");

                                    //  if (item.Mobile != "")
                                    span += ", " + item.Mobile.replace(",", "-");
                                    //  if (item.Fax != "")
                                    span += ", " + item.Fax.replace(",", "-");

                                    //  if (item.Website != "")
                                    span += ", " + item.Website;

                                    // if (item.Address2 != "")
                                    span += ", " + item.Address2.replace(",", "-");

                                    // if (item.Company != "")
                                    span += ", " + item.Company.replace(",", "-");

                                    CheckOut.BillingAddress.FirstName = item.FirstName.replace(",", "-");
                                    CheckOut.BillingAddress.LastName = item.LastName.replace(",", "-");
                                    CheckOut.BillingAddress.Address = item.Address1.replace(",", "-");
                                    CheckOut.BillingAddress.CompanyName = item.Company.replace(",", "-");
                                    CheckOut.BillingAddress.EmailAddress = item.Email.replace(",", "-");
                                    CheckOut.BillingAddress.City = item.City.replace(",", "-");
                                    CheckOut.BillingAddress.State = item.State.replace(",", "-");
                                    CheckOut.BillingAddress.zip = item.Zip.replace(",", "-");
                                    CheckOut.BillingAddress.Country = item.Country.replace(",", "-");
                                    CheckOut.BillingAddress.Phone = item.Phone.replace(",", "-");
                                    CheckOut.BillingAddress.Fax = item.Fax.replace(",", "-");

                                }
                            });
                            $("#prBillingAddressInfo").html(span);
                            $('#spnTotalTax').html('').append('Total Tax: <span class="cssClassFormatCurrency"> ' + (CheckOut.UserCart.Tax * rate).toFixed(2) + '</span>');
                            $('#tdBilling').html($('#prBillingAddressInfo').html());
                            $('#dvPlaceOrder').html($('#dvColumnSet').html());
                            CheckOut.shippingChange();
                        }
                        break;
                    case 7: //user address binding
                        var option = '';
                        if (data.d.length > 0) {
                            $.each(data.d, function(index, item) {
                                if (item.DefaultShipping == 1) {
                                    option += "<option value=" + item.AddressID + " selected='selected'> ";
                                    option += item.FirstName.replace(",", "-") + " " + item.LastName.replace(",", "-");
                                    if (item.Address1 != "")
                                        option += ", " + item.Address1.replace(",", "-");

                                    if (item.City != "")
                                        option += ", " + item.City.replace(",", "-");

                                    if (item.State != "")
                                        option += ", " + item.State.replace(",", "-");

                                    if (item.Country != "")
                                        option += ", " + item.Country.replace(",", "-");

                                    if (item.Zip != "")
                                        option += ", " + item.Zip.replace(",", "-");

                                    if (item.Email != "")
                                        option += ", " + item.Email.replace(",", "-");

                                    if (item.Phone != "")
                                        option += ", " + item.Phone.replace(",", "-");

                                    // if (item.Mobile != "")
                                    option += ", " + item.Mobile.replace(",", "-");

                                    //  if (item.Fax != "")
                                    option += ", " + item.Fax.replace(",", "-");

                                    //  if (item.Website != "")
                                    option += ", " + item.Website.replace(",", "-");

                                    //   if (item.Address2 != "")
                                    option += ", " + item.Address2.replace(",", "-");

                                    //   if (item.Company != "")
                                    option += ", " + item.Company.replace(",", "-");

                                }
                                else {
                                    option += "<option value=" + item.AddressID + "> ";
                                    option += item.FirstName.replace(",", "-") + " " + item.LastName.replace(",", "-");
                                    if (item.Address1 != "")
                                        option += ", " + item.Address1.replace(",", "-");


                                    if (item.City != "")
                                        option += ", " + item.City.replace(",", "-");

                                    if (item.State != "")
                                        option += ", " + item.State.replace(",", "-");

                                    if (item.Country != "")
                                        option += ", " + item.Country.replace(",", "-");

                                    if (item.Zip != "")
                                        option += ", " + item.Zip.replace(",", "-");

                                    if (item.Email != "")
                                        option += ", " + item.Email.replace(",", "-");

                                    if (item.Phone != "")
                                        option += ", " + item.Phone.replace(",", "-");

                                    //  if (item.Mobile != "")
                                    option += ", " + item.Mobile.replace(",", "-");

                                    //    if (item.Fax != "")
                                    option += ", " + item.Fax.replace(",", "-");

                                    // if (item.Website != "")
                                    option += ", " + item.Website.replace(",", "-");

                                    //  if (item.Address2 != "")
                                    option += ", " + item.Address2.replace(",", "-");

                                    //   if (item.Company != "")
                                    option += ", " + item.Company.replace(",", "-");

                                }
                            });
                            // $(".userAddress").html(option);
                            $('.userAddress').not('.userAddress[Downloadable=1]').html(option);

                            if (CheckOut.UserCart.SelectedVals != "") {
                                $(".userAddress").each(function(i) {
                                    $(this).val(CheckOut.UserCart.SelectedVals.split(',')[i]);
                                });
                            }
                            for (var x in CheckOut.UserCart.ddlhide) {
                                $('#' + CheckOut.UserCart.ddlhide[x]).hide();
                            }
                            CheckOut.BindCreateElements();
                        }
                        break;
                    case 8: //quantity discount
                        CheckOut.UserCart.TotalDiscount = parseFloat(data.d).toFixed(2);
                        break;
                    case 10: //cart details                       
                        $("#tblCartInfo>tbody").html('');
                        CheckOut.UserCart.ddlhide = [];
                        if (data.d.length > 0) {
                            var ddlShipping = '';
                            var itemID = '';
                            var oldID = '';
                            var i = 0;
                            var j = 1;
                            var x = 1;
                            var oldSp = '';
                            $.each(data.d, function(index, item) {
                                var newCartItemRow = '';
                                if (oldSp == item.ShippingMethodID) {
                                    j = 1;
                                    i = 0;
                                    oldID = item.ItemID;
                                }
                                else if (oldID == item.ItemID) {
                                    i = 1;
                                }
                                else { i = 0; j = 1; oldID = item.ItemID; oldSp = item.ShippingMethodID }

                                if (i == 0 && j != 0) {
                                    var cv = "";
                                    if (item.Variants != "") {
                                        cv = "(" + item.Variants + ")";
                                    }
                                    ddlShipping = 'dvSPM' + item.ItemID + x;
                                    newCartItemRow += '<tr><td><input type="hidden" size="3" value=' + item.ItemID + ' itemtype="' + item.ItemTypeID + '" class="cssClassItemID">';
                                    newCartItemRow += '<a href="' + aspxRedirectPath + 'item/' + item.Sku + pageExtension + '" class="cssClassLink" sku="' + item.Sku + '">' + item.ItemName + cv + '</a></td>';
                                    newCartItemRow += '<td><input type="text" size="3" class="cssClassQuantity" value="' + item.Quantity + '" readonly="readonly"> <input type="hidden" value=' + item.VariantIDs + ' class="cssClassVariants" /></td>';
                                    CheckOut.UserCart.CountAllItem++;
                                    if (item.ItemTypeID != 2) {
                                        CheckOut.UserCart.IsDownloadItemInCart = true;

                                        newCartItemRow += '<td><select class="userAddress" Downloadable="0"></select></td>';
                                        newCartItemRow += '<td><select id="' + ddlShipping + '" class="cssClassShippingMethod">';
                                        newCartItemRow += '<option value="' + item.ShippingMethodID + '">' + item.ShippingMethodName + " (" + item.ShippingCost + ')</option>';
                                        newCartItemRow += '</select>';
                                        newCartItemRow += '<input type="hidden" size="3" class="cssClassShippingMethodForID" value="' + item.ShippingMethodID + '">';
                                        newCartItemRow += '<input type="hidden" size="3" class="cssClassShippingCost" value="' + item.ShippingCost + '">';
                                        newCartItemRow += '<input type="hidden" size="3" class="cssClassItemCost" value="' + item.UnitPrice + '">';
                                        newCartItemRow += '<input type="hidden" size="3" class="cssClassItemWeight" value="' + item.Weight + '">';
                                        newCartItemRow += '<input type="hidden" size="3" class="cssClassSubTotalCost" value="' + item.SubTotalPrice + '"></td>';
                                    } else {
                                        CheckOut.UserCart.CountDownloadableItem++;
                                        newCartItemRow += '<td><strong>' + getLocale(AspxCheckoutWithMultipleAddresses, 'No Shipping Address Needed') + '</strong><select id="' + item.ItemName.split(' ').join('') + item.ItemID + '_' + item.ItemTypeID + '" class="userAddress" Downloadable="1"  ><option value="0" selected="selected">' + getLocale(AspxCheckoutWithMultipleAddresses, 'No Shipping Address Required') + '</option></select></td>';
                                        newCartItemRow += '<td><strong>' + getLocale(AspxCheckoutWithMultipleAddresses, 'No Shipping Cost Needed') + '</strong>';
                                        newCartItemRow += '<select  id="' + ddlShipping + '" class="cssClassShippingMethod">';
                                        newCartItemRow += '<option value="0" selected="selected" Downloadable="1"></option>';
                                        newCartItemRow += '</select>';

                                        newCartItemRow += '<input type="hidden" size="3" class="cssClassShippingMethodForID" value="0">';
                                        newCartItemRow += '<input type="hidden" size="3" class="cssClassShippingCost" value="0">';
                                        newCartItemRow += '<input type="hidden" size="3" class="cssClassItemCost" value="' + item.UnitPrice + '">';
                                        newCartItemRow += '<input type="hidden" size="3" class="cssClassItemWeight" value="0">';
                                        newCartItemRow += '<input type="hidden" size="3" class="cssClassSubTotalCost" value="' + item.SubTotalPrice + '"></td>';


                                        CheckOut.UserCart.ddlhide.push(item.ItemName.split(' ').join('') + item.ItemID + '_' + item.ItemTypeID);
                                        CheckOut.UserCart.ddlhide.push(ddlShipping);


                                    }


                                    newCartItemRow += '</tr>';
                                    $("#tblCartInfo>tbody").append(newCartItemRow);
                                    CheckOut.UserCart.Tax += item.Quantity * item.TaxRateValue;
                                    x++;
                                }
                                else {
                                    $('#' + ddlShipping + '').append('<option value="' + item.ShippingMethodID + '">' + item.ShippingMethodName + " (" + item.ShippingCost + ')</option>');
                                }

                            });
                        }
                        CheckOut.BindUserAddress();
                        CheckOut.AssignShippingCost();
                        if (cartItemCount == 0) {

                            $('.sfGridWrapperContent #tblCartInfo, .sfGridWrapperContent .sfButtonwrapper').remove();
                            $('.sfGridWrapperContent h3').html('').html(getLocale(AspxCheckoutWithMultipleAddresses, 'No Items found in your Cart'));
                            $('.sfGridWrapperContent').append("<div class='sfButtonwrapper'><button type='button' id='btnContinueInStore' class='sfBtn'><span>" + getLocale(AspxCheckoutWithMultipleAddresses, 'Continue to Shopping') + "</span></button></div><div class='cssClassClear'></div></div></div></div>");

                            $("#btnContinueInStore").click(function() {
                                if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + homeURL + pageExtension;
                                }
                                else {
                                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + homeURL;
                                }
                                return false;
                            });
                        }
                        CheckOut.CheckDownloadableOnlyInCart();
                        for (var x in CheckOut.UserCart.ddlhide) {
                            $('#' + CheckOut.UserCart.ddlhide[x]).hide();
                        }
                        //                if (IsDownloadItemInCartFull) {
                        //                    $('#tblCartInfo tr td:nth-child(4)').hide();
                        //                    $('#tblCartInfo tr td:nth-child(3)').hide();
                        //                    $('.userAddress').attr('disabled', 'disabled');
                        //                    //$('.cssClassTBLBorderNone tbody tr td:first').hide();
                        //                }
                        if ($(".cssClassItemID").length < cartItemCount) {
                            var si = parseInt($(".cssClassItemID").length);
                            var ci = cartItemCount;
                            var i = ci - si;
                            $('#btnShippingAddressContinue').hide();
                            $('.sfGridWrapperContent #tblCartInfo, .sfGridWrapperContent .sfButtonwrapper').remove();
                            $('.sfGridWrapperContent h3').html('').html('<span id="spnDeleteItem">' + getLocale(AspxCheckoutWithMultipleAddresses, 'you have') + '<strong>' + i + '</strong>' + getLocale(AspxCheckoutWithMultipleAddresses, 'item that does not meet shipping item weight criteria Or Shipping providers are unable to ship items.') + '<br />' + getLocale(AspxCheckoutWithMultipleAddresses, 'If you still want to checkout then you have to delete that items from your cart to procceed  further process.') + '<br /><strong>' + getLocale(AspxCheckoutWithMultipleAddresses, 'Thank you') + '<strong></span>');


                            //  $('#btnShippingAddressContinue').parent('.sfButtonwrapper').append('<span id="spnDeleteItem">you have <strong>' + i + '</strong> item that does not meet shipping item weight criteria.<br />If you still want to checkout then you have to delete that items from your shopping bag to procceed  further process.<br /><strong>Thank you<strong></span>');

                        }
                        else {
                            $('#btnShippingAddressContinue').show();
                            $('#spnDeleteItem').empty();
                        }
                        CheckOut.CheckDownloadableOnlyInCart();
                        break;
                    case 11: //discount from price cart rule
                        this.config.method = "AspxCommerceWebService.asmx/GetDiscountPriceRule";
                        this.config.url = this.config.baseURL + this.config.method;
                        this.config.data = JSON2.stringify({ cartID: data.d, storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName(), shippingCost: CheckOut.Vars.SpCost });
                        this.config.ajaxCallMode = 12;
                        this.config.error = 12;
                        this.config.async = false;
                        this.ajaxCall(this.config);
                        break;
                    case 12:
                        CheckOut.UserCart.CartDiscount = parseFloat(data.d).toFixed(2);
                        break;
                    case 13:
                        //                        $('#ddlUSState').show();
                        //                        $('#txtState').hide();
                        $("#ddlUSState").html('');
                        $.each(data.d, function(index, item) {
                            if (item.Text != "NotExists") {
                                $('#txtState').hide();
                                $("#ddlUSState").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                            } else {
                                $('#ddlUSState').hide();
                                $('#txtState').show();
                            }
                        });
                        break;
                }

            },
            Init: function() {
                if (AspxCommerce.utils.GetCustomerID() == 0) {
                    window.location.href = aspxRedirectPath + homeURL + pageExtension;

                }
                else {
                    CheckOut.GetCartItemTotalCount();
                    CheckOut.CheckDownloadableOnlyInCart();
                    var v = $("#form1").validate({
                        messages: {
                            cardCode: {
                                required: '*',
                                minlength: "* (at least 3 chars)"
                            },
                            FirstName: {
                                required: '*',
                                minlength: "* (at least 2 chars)", maxlength: "*"
                            },
                            LastName: {
                                required: '*',
                                minlength: "* (at least 2 chars)", maxlength: "*"
                            },
                            Email: {
                                required: '*',
                                email: '*'
                            },
                            Address1: {
                                required: '*',
                                minlength: "* (at least 2 chars)", maxlength: "*"
                            },
                            Address2: {
                                maxlength: "*"
                            },
                            Company: {
                                maxlength: "*"
                            }, Fax: {
                                maxlength: "*"
                            },
                            Phone: {
                                required: '*',
                                maxlength: "", minlength: "* (at least 7 digits)"
                            },
                            Mobile: {
                                maxlength: "*",
                                minlength: "* (at least 10 digits)"
                            },
                            stateprovince: {
                                required: '*',
                                minlength: "* (at least 2 chars)", maxlength: "*"
                            },
                            Zip: {
                                required: '*',
                                minlength: "* (at least 4 chars)", maxlength: "*"
                            },
                            City: {
                                required: '*',
                                minlength: "* (at least 2 chars)", maxlength: "*"
                            },
                            name: {
                                required: '*',
                                minlength: "* (at least 2 chars)", maxlength: "*"
                            }
                        },
                        rules:
                 {
                     creditCard: {
                         required: true,
                         creditcard: true
                     }
                 },
                        ignore: ":hidden",
                        submitHandler: function() {
                            if (v.form()) {

                            }
                        }
                    });

                    //  var $tabs = $('#dvMultipleAddress').tabs({ fx: [null, {event:false, height: 'show', opacity: 'show'}] });
                    var $tabs = $('#dvMultipleAddress').tabs({ event: false });
                    CheckOut.GetAllCountry();
                    CheckOut.BindCartItems();
                    CheckOut.LoadPGatewayList();

                    $('#lblAuthCode').hide();
                    $('#txtAuthCode').hide();
                    if (CheckOut.UserCart.TotalDiscount == 0) {
                        CheckOut.QuantitityDiscountAmount();
                    }

                    $('#ddlTransactionType').bind("change", function() {
                        if ($('#ddlTransactionType option:selected').text() == " CAPTURE_ONLY") {
                            $('#lblAuthCode').show();
                            $('#txtAuthCode').show();
                        }
                        else {
                            $('#lblAuthCode').hide();
                            $('#txtAuthCode').hide();
                        }
                    });

                    $('#btnShippingAddressContinue').bind("click", function() {
                        CheckOut.UserCart.SelectedVals = "";
                        $(".userAddress").each(function() {
                            CheckOut.UserCart.SelectedVals += $(this).val() + ",";
                        });

                        if ($('.cssClassQuantity:first').val() < 0) {
                        }
                        else if ($(".userAddress option").html() == '' || $(".userAddress option").html() == null) {
                            //alert('Your Address Details has not been created Yet!! \n Please Create it to continue..');
                            csscody.alert("<h2>" + getLocale(AspxCheckoutWithMultipleAddresses, 'Information Message') + '</h2><p>' + getLocale(AspxCheckoutWithMultipleAddresses, "Your Address Details has not been created Yet!! \n Please Create it to continue..") + "</p>");
                            CheckOut.ClearAll();
                            ShowPopupControl("popuprel");
                            $('#chkShippingAddress, #chkBillingAddress').attr('checked', true);
                            $('#chkBillingAddress, #chkShippingAddress').attr('disabled', 'disabled');
                        }
                        else {
                            CheckOut.BindCreateElements();
                            $.cookies.set('ShippingDetails', $(".userAddress option").html());
                            $tabs.tabs('option', 'active', 1);
                        }
                        if (CheckOut.UserCart.IsDownloadItemInCartFull) {
                            $('.cssClassTBLBorderNone tbody tr td:first,.cssClassTBLBorderNone tbody tr td:nth-child(2)').hide();
                        }
                    });
                    $('#btnShippingBack').bind("click", function() {
                        $tabs.tabs('option', 'active', 0);
                    });
                    $('#btnBillingBack').bind("click", function() {
                        $tabs.tabs('option', 'active', 1);
                    });
                    $('#btnPlaceOrderBack').bind("click", function() {
                        $tabs.tabs('option', 'active', 2);
                    });

                    $('#btnShippingContinue').bind("click", function() {
                        CheckOut.BindBillingAddress();
                        CheckOut.billingChange();
                        retriveForMulti();
                        CheckOut.GetDiscountCartPriceRule(CheckOut.UserCart.SC);
                        $tabs.tabs('option', 'active', 2);
                    });

                    function retriveForMulti() {
                        if ($('.cssClassShippingMethodBox span:last').html().replace(/[^0-9\.]+/g, "") == $('.cssSPCost').html().replace(/[^0-9\.]+/g, "")) {
                            CheckOut.SetSessionValue("ShippingCostAll", parseFloat($('.cssClassShippingMethodBox span:last').html().replace(/[^0-9\.]+/g, "")));
                        }
                        CheckOut.SetSessionValue("DiscountAll", eval(parseFloat(CheckOut.UserCart.TotalDiscount) + parseFloat(CheckOut.UserCart.CartDiscount)));
                        // $.cookie('ShippingCost', $("#txtShippingTotal").val());
                    }

                    $('#btnBillingContinue').bind("click", function() {
                        var GrandTotal = 0;
                        if (v.form()) {
                            if ($('#dvPGList input:radio:checked').attr('checked') == true) {
                                if ($.trim($('#dvPGList input:radio:checked').attr('friendlyname')) == 'AIMAuthorize') {
                                    if ($('#AIMChild').length > 0) {
                                        CheckOut.SetSessionValue("TaxAll", CheckOut.UserCart.Tax);
                                        $('#spnTotalTax').html('').append('Total Tax: <span class="cssClassFormatCurrency">' + (CheckOut.UserCart.Tax * rate).toFixed(2) + '</span>');
                                        $('#tdBilling').html($('#prBillingAddressInfo').html());
                                        $('#dvPlaceOrder').html($('#dvColumnSet').html());

                                        $('#dvPlaceOrder').find("span[class='cssGrandSubTotal cssClassFormatCurrency']").each(function() {
                                            GrandTotal = parseFloat(GrandTotal) + parseFloat($(this).html().replace(/[^0-9\.]+/g, ""));
                                        });

                                        $('#spnTotalDiscount').html('').append(getLocale(AspxCheckoutWithMultipleAddresses, 'Total Discount: ') + '<span class="cssClassFormatCurrency">' + (eval(parseFloat(eval(CheckOut.UserCart.TotalDiscount * rate)) + parseFloat(eval(CheckOut.UserCart.CartDiscount * rate)))).toFixed(2) + '</span>');
                                        CheckOut.SetSessionValue("DiscountAll", eval(parseFloat(CheckOut.UserCart.TotalDiscount) + parseFloat(CheckOut.UserCart.CartDiscount)));
                                        $('#spnGrandTotal').html('').append(getLocale(AspxCheckoutWithMultipleAddresses, 'Grand Total: ') + '<span class="cssClassFormatCurrency">' + (parseFloat(GrandTotal) + parseFloat(CheckOut.UserCart.Tax * rate) - parseFloat(CheckOut.UserCart.TotalDiscount * rate) - parseFloat(CheckOut.UserCart.CartDiscount * rate)) + '</span>');
                                        CheckOut.UserCart.amount = parseFloat(GrandTotal / rate) + parseFloat(CheckOut.UserCart.Tax) - parseFloat(CheckOut.UserCart.TotalDiscount) - parseFloat(CheckOut.UserCart.CartDiscount);
                                        CheckOut.SetSessionValue("GrandTotalAll", CheckOut.UserCart.amount);
                                        // $.cookie('ShipingCost', 20);
                                        //$.cookie('Total', 300);
                                        // $.cookie('GrandTotal', 320);
                                        CheckOut.shippingChange();
                                        $tabs.tabs('option', 'active', 3);
                                        if (CheckOut.UserCart.amount < 0) {
                                            csscody.alert("<h2>" + getLocale(AspxCheckoutWithMultipleAddresses, 'Information Alert') + '</h2><p>' + getLocale(AspxCheckoutWithMultipleAddresses, 'Your cart is not eligible to checkout due to a negatve total amount!') + "</p>");
                                            $("#dvPGList input:radio").attr("disabled", "disabled");
                                            $('#placeOrder .sfButtonwrapper ').find('input').not("#btnPlaceOrderBack").remove();

                                        } else {
                                            $("#dvPGList input:radio").attr("disabled", false);
                                        }
                                    }


                                } else {
                                    CheckOut.SetSessionValue("TaxAll", CheckOut.UserCart.Tax);
                                    $('#spnTotalTax').html('').append('Total Tax: <span class="cssClassFormatCurrency"> ' + (CheckOut.UserCart.Tax * rate).toFixed(2) + '</span>');
                                    $('#tdBilling').html($('#prBillingAddressInfo').html());
                                    $('#dvPlaceOrder').html($('#dvColumnSet').html());

                                    $('#dvPlaceOrder').find("span[class='cssGrandSubTotal cssClassFormatCurrency']").each(function() {


                                        GrandTotal = parseFloat(GrandTotal) + parseFloat($(this).html().replace(/[^0-9\.]+/g, ""));

                                    });

                                    $('#spnTotalDiscount').html('').append(getLocale(AspxCheckoutWithMultipleAddresses, 'Total Discount: ') + '<span class="cssClassFormatCurrency"> ' + (eval(parseFloat(eval(CheckOut.UserCart.TotalDiscount * rate)) + parseFloat(eval(CheckOut.UserCart.CartDiscount * rate)))).toFixed(2) + '</span>');
                                    CheckOut.SetSessionValue("DiscountAll", eval(parseFloat(CheckOut.UserCart.TotalDiscount) + parseFloat(CheckOut.UserCart.CartDiscount)));

                                    $('#spnGrandTotal').html('').append(getLocale(AspxCheckoutWithMultipleAddresses, 'Grand Total: ') + '<span class="cssClassFormatCurrency">' + (parseFloat(GrandTotal) + parseFloat(CheckOut.UserCart.Tax * rate) - parseFloat(CheckOut.UserCart.TotalDiscount * rate) - parseFloat(CheckOut.UserCart.CartDiscount * rate)) + '</span>');
                                    CheckOut.UserCart.amount = parseFloat(GrandTotal / rate) + parseFloat(CheckOut.UserCart.Tax) - parseFloat(CheckOut.UserCart.TotalDiscount) - parseFloat(CheckOut.UserCart.CartDiscount);
                                    CheckOut.SetSessionValue("GrandTotalAll", CheckOut.UserCart.amount);
                                    // $.cookie('ShipingCost', 20);
                                    //$.cookie('Total', 300);
                                    // $.cookie('GrandTotal', 320);
                                    CheckOut.shippingChange();
                                    $tabs.tabs('option', 'active', 3);
                                    if (CheckOut.UserCart.amount < 0) {
                                        csscody.alert("<h2>" + getLocale(AspxCheckoutWithMultipleAddresses, 'Information Alert') + '</h2><p>' + getLocale(AspxCheckoutWithMultipleAddresses, 'Your cart is not eligible to checkout due to a negatve total amount!') + "</p>");
                                        $("#dvPGList input:radio").attr("disabled", "disabled");
                                        $('#placeOrder .sfButtonwrapper ').find('input').not("#btnPlaceOrderBack").remove()

                                    } else {
                                        $("#dvPGList input:radio").attr("disabled", false);
                                    }
                                }
                            }
                            else {
                                // alert('Please Select your payment system');
                                csscody.alert("<h2>" + getLocale(AspxCheckoutWithMultipleAddresses, 'Information Message') + '</h2><p>' + getLocale(AspxCheckoutWithMultipleAddresses, 'Please Select your payment system.') + "</p>");
                            }
                        }
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    });

                    $('#btnSubmitAddress').bind("click", function() {
                        if (v.form()) {

                            CheckOut.AddUpdateUserAddress();
                        }
                        else {
                            return false;
                        }
                    });

                    $("#btnEnterNewAddress").bind("click", function() {
                        CheckOut.ClearAll();
                        ShowPopupControl("popuprel");
                        if ($('.userAddress').html() != '') {
                            $('#trShippingAddress , #trBillingAddress').hide();
                        }
                        else {
                            $('#trShippingAddress , #trBillingAddress').show();
                            $('#chkShippingAddress, #chkBillingAddress').attr('checked', true);
                            $('#chkBillingAddress, #chkShippingAddress').attr('disabled', 'disabled');
                        }
                    });

                    $(".cssClassClose").bind("click", function() {
                        RemovePopUp();
                    });
                    $('#ddlUSState').hide();
                    $("#ddlCountry ").bind("change", function() {
                        //if ($("#ddlCountry :selected").text() == 'United States') {
                        CheckOut.GetStateList($(this).val());
                        //                        }
                        //                        else {
                        //                            $('#ddlUSState').hide();
                        //                            $('#txtState').show();
                        //                        }
                    });
                }
            }
        }
        CheckOut.Init();
    });
    //]]>
</script>

<div class="cssClassTabPanelTable">
    <div id="dvMultipleAddress" class="cssClassMultipleCheckout">
        <ul>
            <li><a href="#selectAddress">
                <asp:Label ID="lblselectAddress"  runat="server" Text="Select Address" 
                    meta:resourcekey="lblselectAddressResource1"></asp:Label></a></li>
            <li><a href="#shippingAddress">
                <asp:Label ID="lblshippingAddress"  runat="server" Text="Shipping Address" 
                    meta:resourcekey="lblshippingAddressResource1"></asp:Label></a></li>
            <li><a href="#billingInformation">
                <asp:Label ID="lblbillingInformation" runat="server" Text="Billing Information" 
                    meta:resourcekey="lblbillingInformationResource1"></asp:Label></a></li>
            <li><a href="#placeOrder">
                <asp:Label ID="lblplaceOrder"  runat="server" Text="Place Order" 
                    meta:resourcekey="lblplaceOrderResource1"></asp:Label></a></li>
            <li><a href="#orderSuccess">
                <asp:Label ID="lblorderSuccess" runat="server" Text="Order Success" 
                    meta:resourcekey="lblorderSuccessResource1"></asp:Label></a></li>
        </ul>
        <div id="selectAddress">
            <div class="sfFormwrapper">
                <div id="div1">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblHeaderShippingMulti"  runat="server" 
                                    Text="Ship to Multiple Addresses" 
                                    meta:resourcekey="lblHeaderShippingMultiResource1"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                                <div class="sfButtonwrapper">
                                    <button type="button" id="btnEnterNewAddress" class="sfBtn">
                                        <span class="sfLocale">Enter a New Adddress</span></button>
                                </div>
                            </div>
                            <div class="cssClassClear">
                            </div>
                        </div>
                        <div class="sfGridwrapper">
                            <div class="sfGridWrapperContent">
                                <div class="log">
                                </div>
                                <h3 class="sfLocale">
                                    Please select shipping address for applicable items</h3>
                                <table id="tblCartInfo" width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <thead>
                                        <tr>
                                            <td class="cssClassShippingHeading sfLocale">
                                                Item
                                            </td>
                                            <td class="cssClassShippingHeading sfLocale">
                                                Quantity
                                            </td>
                                            <td class="cssClassShippingHeading sfLocale">
                                                Shipping Address
                                            </td>
                                            <td class="cssClassShippingHeading sfLocale">
                                                Shipping Method
                                            </td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr><td></td></tr>
                                    </tbody>
                                </table>
                                <div class="sfButtonwrapper cssClassRight">
                                    <button type="submit" id="btnShippingAddressContinue" class="sfBtn">
                                       <span class="sfLocale icon-arrow-slim-e">Continue</span></button>
                                </div>
                                <div class="cssClassClear">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="shippingAddress">
            <div class="sfFormwrapper">
                <div id="div3">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblSelectShippingMethod"  runat="server" 
                                    Text="Select Shipping Method" 
                                    meta:resourcekey="lblSelectShippingMethodResource1"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                            </div>
                            <div class="cssClassClear">
                            </div>
                        </div>
                        <div class="sfGridwrapper">
                            <div class="sfGridWrapperContent">
                                <div id="dvColumnSet">
                                </div>
                                <div class="sfButtonwrapper cssClassRight">
                                    <button type="button" id="btnShippingBack" class="sfBtn">
                                        <span class="sfLocale icon-arrow-slim-w">Back</span></button>
                                    <button type="submit" id="btnShippingContinue" class="sfBtn">
                                        <span class="sfLocale icon-arrow-slim-e">Continue</span></button>
                                </div>
                                <div class="cssClassClear">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="billingInformation">
            <div class="sfFormwrapper">
                <div id="div4">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblBillingInfo"  runat="server" Text="Billing Information" 
                                    meta:resourcekey="lblBillingInfoResource1"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                            </div>
                            <div class="cssClassClear">
                            </div>
                        </div>
                        <div class="sfGridwrapper">
                            <div class="sfGridWrapperContent">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td width="31%" valign="top" class="cssClassTBLBorderNone">
                                                <p>
                                                    <span class="sfLocale">Billing to</span> 
                                                    <a class="cssClassChange sfLocale" name="billingChange" rel="popuprel" href="#">Change</a></p>
                                                <p id="prBillingAddressInfo" class="cssClassBillingAddressInfo">
                                                </p>
                                            </td>
                                            <td width="2%" valign="top">&nbsp;
                                                
                                            </td>
                                            <td width="69%" valign="top">
                                                <div class="cssClassPaymentMethods">
                                                    <p class="cssClassPadding">
                                                        <span class="sfLocale">Payment Methods</span></p>
                                                    <div id="dvPGList">
                                                    </div>
                                                   </div> 
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div class="sfButtonwrapper cssClassRight">
                                    <button type="button" id="btnBillingBack" class="sfBtn">
                                        <span class="sfLocale icon-arrow-slim-w">Back</span></button>
                                    <button type="submit" id="btnBillingContinue" class="sfBtn">
                                        <span class="sfLocale icon-arrow-slim-e">Continue</span></button>
                                </div>
                                <div class="cssClassClear">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="placeOrder">
            <div class="sfFormwrapper">
                <div id="div5">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblBilling"  runat="server" Text="Billing Information" 
                                    meta:resourcekey="lblBillingResource1"></asp:Label>
                            </h2>
                            <div class="cssClassClear">
                            </div>
                        </div>
                        <div class="sfGridwrapper">
                            <div class="sfGridWrapperContent">
                                <div id="dvTemp">
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td width="31%" valign="top" class="cssClassTBLBorderNone">
                                                    <p>
                                                        <span class="cssClassShippingHeading sfLocale">Billing to</span> 
                                                        <a class="cssClassChange sfLocale" name="billingChange" rel="popuprel" href="#">Change</a></p>
                                                </td>
                                                    <td id="tdBilling">
                                                    </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblShippingInformation"  runat="server" 
                                    Text="Shipping Information" meta:resourcekey="lblShippingInformationResource1"></asp:Label>
                            </h2>
                            <div class="cssClassClear">
                            </div>
                        </div>
                        <div class="sfGridwrapper">
                        
                            <div class="sfGridWrapperContent">
                                <div id="dvPlaceOrder">
                                </div>
                                 <div class="sfGridWrapperContent"> 
                                 <table class="cssClassTBLBorderNone noborder" cellspacing="0" cellpadding="0" border="0" width="100%">
                         <tr><td width="31%" valign="top" >  <strong class="sfLocale">Additional Note:</strong></td> <td  width="2%" valign="top" ></td><td width="69%" valign="top">
                                <textarea id="txtAdditionalNote" class="cssClassTextarea" rows="3" cols="90" style="width: 610px; height: 45px;"></textarea></td></tr>
                        </table>
                         <div class="cssClassClear">
                                </div></div>
                                <div class="cssClassRight">                                    
                                    <strong><span id="spnTotalTax" class="cssClassShippingHeading sfLocale">Total Tax:</span>
                                    </strong>
                                    <br />
                                    <strong><span id="spnTotalDiscount" class="cssClassShippingHeading sfLocale">Total Discount: </span>
                                    </strong>
                                    <br />
                                    <strong><span id="spnGrandTotal" class="cssClassShippingHeading sfLocale">Grand Total: </span>
                                    </strong>                                   
                                </div>
                                  
                                <div class="sfButtonwrapper cssClassRight">
                                    <input id="btnPlaceOrderBack" type="button" value="Back " />
                                   
                                        <input id="btnPlaceOrderContinue" type="submit" value="Place Order " />
                                    
                                </div>
                                <div class="cssClassClear">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="orderSuccess">
            <span class="sfLocale">Your order has been received</span>
            <br />
            <br />
            <input type="hidden" id="hdnPO" runat="server" />
            <h3 class="sfLocale">
                Thank you for your purchase!</h3>
            <br />
            <br />
            <p class="sfLocale">
                You can check your oder in my oders.<span id="PurchaseOrderNumber"></span>
            </p>
            <br />
            <p class="sfLocale">
                You will receive an order confirmation email with details of your order and a link
                to track its progress.</p>
            <a href="#" class="sfLocale">Continue Shopping</a>
        </div>
    </div>
</div>
<div class="popupbox" id="popuprel">
<div class="cssPopUpBody">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span class="sfLocale">Close</span></button>
    </div>
    <h2>
        <asp:Label ID="lblAddressTitle" runat="server" Text="Address Details" 
            meta:resourcekey="lblAddressTitleResource1"></asp:Label>
    </h2>
    <div class="sfFormwrapper">
        <table id="tblNewAddress" width="100%" border="0" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td width="20%">
                        <asp:Label ID="lblFirstName" runat="server" Text="FirstName" 
                            CssClass="cssClassLabel " meta:resourcekey="lblFirstNameResource1"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td width="80%">
                        <input type="text" id="txtFirstName" name="FirstName" class="required" minlength="2" maxlength="40"/>
                    </td>
                    <td>
                        <asp:Label ID="lblLastName" runat="server" Text="LastName:" 
                            CssClass="cssClassLabel " meta:resourcekey="lblLastNameResource1"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtLastName" name="LastName" class="required" minlength="2" maxlength="40" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="cssClassLabel " 
                            meta:resourcekey="lblEmailResource1"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtEmailAddress" name="Email" class="required email" minlength="2" />
                    </td>
                     <td>
                        <asp:Label ID="lblCompany" Text="Company:" runat="server" 
                             CssClass="cssClassLabel " meta:resourcekey="lblCompanyResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtCompanyName" name="Company" maxlength="40"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAddress1" Text="Address1:" runat="server" 
                            CssClass="cssClassLabel " meta:resourcekey="lblAddress1Resource1"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtAddress1" name="Address1" class="required" minlength="2"maxlength="250"/>
                    </td>
                     <td>
                        <asp:Label ID="lblAddress2" Text="Address2:" runat="server" 
                             CssClass="cssClassLabel " meta:resourcekey="lblAddress2Resource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtAddress2" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCountry" Text="Country:" runat="server" 
                            CssClass="cssClassLabel " meta:resourcekey="lblCountryResource1"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <select id="ddlCountry" class="sfListmenu">
                            <option></option>
                        </select>
                    </td>
                   
                     <td>
                        <asp:Label ID="lblState" Text="State/Province:" runat="server" 
                             CssClass="cssClassLabel " meta:resourcekey="lblStateResource1"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtState" name="stateprovince" class="required" minlength="2" maxlength="250" />
                         <select id="ddlUSState" class="sfListmenu">
                             <option></option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblZip" Text="Zip/Postal Code:" runat="server" 
                            CssClass="cssClassLabel " meta:resourcekey="lblZipResource1"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtZip" name="Zip" class="required alphaNumberic" minlength="4" maxlength="10" />
                    </td>
                    <td>
                        <asp:Label ID="lblCity" Text="City:" runat="server" CssClass="cssClassLabel " 
                            meta:resourcekey="lblCityResource1"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtCity" name="City" class="required" minlength="2" maxlength="250" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPhone" Text="Phone:" runat="server" CssClass="cssClassLabel " 
                            meta:resourcekey="lblPhoneResource1"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtPhone" name="Phone" class="required number" minlength="7" maxlength="20" />
                    </td>
                    <td>
                        <asp:Label ID="lblMobile" Text="Mobile:" runat="server" 
                            CssClass="cssClassLabel  " meta:resourcekey="lblMobileResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtMobile" class="number" name="Mobile" maxlength="20" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFax" Text="Fax:" runat="server" CssClass="cssClassLabel " 
                            meta:resourcekey="lblFaxResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtFax" class="number"  name="Fax" maxlength="20"/>
                    </td>
                    <td>
                        <asp:Label ID="lblWebsite" Text="Website:" runat="server" 
                            CssClass="cssClassLabel " meta:resourcekey="lblWebsiteResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtWebsite"  class="url"  maxlength="50"/>
                    </td>
                </tr>
                <tr id="trShippingAddress">
                    <td>
                        <input type="checkbox" id="chkShippingAddress" />
                    </td>
                    <td>
                        <asp:Label ID="lblDefaultShipping" Text="Use as Default Shipping Address" runat="server"
                            CssClass="cssClassLabel sfLocale" 
                            meta:resourcekey="lblDefaultShippingResource1"></asp:Label>
                    </td>
                </tr>
                <tr id="trBillingAddress">
                    <td>
                        <input type="checkbox" id="chkBillingAddress" />
                    </td>
                    <td>
                        <asp:Label ID="lblDefaultBilling" Text="Use as Default Billing Address" runat="server"
                            CssClass="cssClassLabel sfLocale" 
                            meta:resourcekey="lblDefaultBillingResource1"></asp:Label>
                    </td>
                </tr>               
            </tbody>
        </table>
         <input type="hidden" id="hdnAddressID" />
        <div class="sfButtonwrapper">
            <button type="button" id="btnSubmitAddress" class="cssClassButtonSubmit sfBtn">
                <span class="sfLocale icon-save">Save</span></button>
        </div>
    </div>
</div>
</div>
