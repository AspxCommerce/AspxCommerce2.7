<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddressBook.ascx.cs" Inherits="Modules_AspxUserDashBoard_AddressBook" %>

<script type="text/javascript">
    //<![CDATA[
    var defaultShippingExist = '<%=defaultShippingExist %>';
    var defaultBillingExist = '<%=defaultBillingExist %>';
    var addressId = '<%=addressId%>';
    var addressBook = "";
    aspxCommonObj.UserName = AspxCommerce.utils.GetUserName();
    $.expr[':'].exactcontains = function (a, i, m) {
        return $(a).text().match("^" + m[3] + "$");
    };

    $.validator.addMethod("alpha_dash", function (value, element) {
        return this.optional(element) || /^[a-z0-9_ \-]+$/i.test(value);
    });
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxUserDashBoard
        });
        addressBook = {
            variables: {
                checkIfExist: '',
                addNewAddress: 0,
                notExists: ''
            },
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: moduleRootPath, method: "",
                url: "",
                ajaxCallMode: "",
                error: ""
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: addressBook.config.type,
                    beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", userModuleIDUD);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: addressBook.config.contentType,
                    cache: addressBook.config.cache,
                    async: addressBook.config.async,
                    url: addressBook.config.url,
                    data: addressBook.config.data,
                    dataType: addressBook.config.dataType,
                    success: addressBook.config.ajaxCallMode,
                    error: addressBook.config.error
                });
            },
            ClearAll: function () {
                $("#hdnAddressID").val(0);
                $("#txtFirstName").val('');
                $("#txtLastName").val('');
                $("#txtEmailAddress").val('');
                $("#txtCompanyName").val('');
                $("#txtAddress1").val('');
                $("#txtAddress2").val('');
                $("#txtCity").val('');
                $("#txtState").val('');
                $("#txtZip").val('');
                $("#ddlUSState").val(1);
                $("#txtPhone").val('');
                $("#txtMobile").val('');
                $("#txtFax").val('');
                $("#txtWebsite").val('');
                $("#chkShippingAddress").removeAttr("checked");
                $("#chkBillingAddress").removeAttr("checked");
                $("#chkShippingAddress").removeAttr("disabled");
                $("#chkBillingAddress").removeAttr("disabled");
            },
            BindAddressDetails: function (msg) {
                var defaultBillingAddressElements = '';
                var defaultShippingAddressElements = '';
                var addressElements = '';
                var addressId = 0;
                var additionalAddrCount = 0;
                var defaultShippingExist = false;
                var defaultBillingExist = false;
                var value;
                for (var index = 0; index < msg.d.length; index++) {
                    value = msg.d[index];
                    if (value.DefaultBilling && value.DefaultShipping) {
                        addressId = value.AddressID;
                    }
                    if (!defaultShippingExist) {
                        if (value.DefaultShipping) {
                            defaultShippingExist = true;
                        } else {
                            defaultShippingExist = false;
                        }
                    }
                    if (!defaultBillingExist) {
                        if (value.DefaultBilling) {
                            defaultBillingExist = true;
                        } else {
                            defaultBillingExist = false;
                        }
                    }
                    if (value.DefaultBilling || value.DefaultShipping) {
                        if (value.DefaultShipping) {
                            defaultShippingAddressElements += '<h3>' + getLocale(AspxUserDashBoard, 'Default Shipping Address') + '</h3>';
                            defaultShippingAddressElements += '<div><span name="FirstName">' + value.FirstName + '</span>' + " " + '<span name="LastName">' + value.LastName + '</span></div>';
                            defaultShippingAddressElements += '<div><span name="Email">' + value.Email + '</span></div>';
                            if (value.Company != "") {
                                defaultShippingAddressElements += '<div><span name="Company">' + value.Company + '</span></div>';
                            }
                            defaultShippingAddressElements += '<div><span name="Address1">' + value.Address1 + '</span></div>';
                            if (value.Address2 != "") {
                                defaultShippingAddressElements += '<div><span name="Address2">' + value.Address2 + '</span></div>';
                            }
                            defaultShippingAddressElements += '<div><span name="City">' + value.City + '</span>, ';
                            defaultShippingAddressElements += '<span name="State">' + value.State + '</span>, ';
                            defaultShippingAddressElements += '<span name="Country">' + value.Country + '</span></div>';
                            defaultShippingAddressElements += '<div>Zip: <span name="Zip">' + value.Zip + '</span></div>';
                            defaultShippingAddressElements += '<div><i class="i-phone"></i><span name="Phone">' + value.Phone + '</span></div>';
                            if (value.Mobile != "") {
                                defaultShippingAddressElements += '<div><i class="i-mobile"></i><span name="Mobile">' + value.Mobile + '</span></div>';
                            }
                            if (value.Fax != "") {
                                defaultShippingAddressElements += '<div><i class="i-fax"></i><span name="Fax">' + value.Fax + '</span></div>';
                            }
                            if (value.Website != "") {
                                defaultShippingAddressElements += '<div><span name="Website">' + value.Website + '</span></div>';
                            }
                            defaultShippingAddressElements += '</div>';
                            $("#liDefaultShippingAddress").html(defaultShippingAddressElements);
                        }
                        if (value.DefaultBilling) {
                            defaultBillingAddressElements += '<h3>' + getLocale(AspxUserDashBoard, 'Default Billing Address') + '</h3>';
                            defaultBillingAddressElements += '<div><span name="FirstName">' + value.FirstName + '</span>' + " " + '<span name="LastName">' + value.LastName + '</span></div>';
                            defaultBillingAddressElements += '<div><span name="Email">' + value.Email + '</span></div>';
                            if (value.Company != "") {
                                defaultBillingAddressElements += '<div><span name="Company">' + value.Company + '</span></div>';
                            }
                            defaultBillingAddressElements += '<div><span name="Address1">' + value.Address1 + '</span></div>';
                            if (value.Address2 != "") {
                                defaultBillingAddressElements += '<div><span name="Address2">' + value.Address2 + '</span></div>';
                            }
                            defaultBillingAddressElements += '<div><span name="City">' + value.City + '</span>, ';
                            defaultBillingAddressElements += '<span name="State">' + value.State + '</span>, ';
                            defaultBillingAddressElements += '<span name="Country">' + value.Country + '</span></div>';
                            defaultBillingAddressElements += '<div>Zip: <span name="Zip">' + value.Zip + '</span></div>';
                            defaultBillingAddressElements += '<div><i class="i-phone"></i><span name="Phone">' + value.Phone + '</span></div>';
                            if (value.Mobile != "") {
                                defaultBillingAddressElements += '<div><i class="i-mobile"></i><span name="Mobile">' + value.Mobile + '</span></div>';
                            }
                            if (value.Fax != "") {
                                defaultBillingAddressElements += '<div><i class="i-fax"></i><span name="Fax">' + value.Fax + '</span></div>';
                            }
                            if (value.Website != "") {
                                defaultBillingAddressElements += '<div><span name="Website">' + value.Website + '</span></div>';
                            }
                            defaultBillingAddressElements += '</div>';
                            $("#liDefaultBillingAddress").html(defaultBillingAddressElements);
                        }
                    } else {
                        additionalAddrCount = additionalAddrCount + 1;
                        addressElements += '<div class="cssClassAddWrapper">';
                        addressElements += '<div class="cssClassAddAddress">';
                        addressElements += '<div><span name="FirstName">' + value.FirstName + '</span>' + " " + '<span name="LastName">' + value.LastName + '</span></div>';
                        addressElements += '<div><span name="Email">' + value.Email + '</span></div>';
                        if (value.Company != "") {
                            addressElements += '<div><span name="Company">' + value.Company + '</span></div>';
                        }
                        addressElements += '<div><span name="Address1">' + value.Address1 + '</span></div>';
                        if (value.Address2 != "") {
                            addressElements += '<div><span name="Address2">' + value.Address2 + '</span></div>';
                        }
                        addressElements += '<div><span name="City">' + value.City + '</span>, ';
                        addressElements += '<span name="State">' + value.State + '</span>, ';
                        addressElements += '<span name="Country">' + value.Country + '</span></div>';
                        addressElements += '<div>Zip: <span name="Zip">' + value.Zip + '</span></div>';
                        addressElements += '<div><i class="i-phone"></i><span name="Phone">' + value.Phone + '</span></div>';
                        if (value.Mobile != "") {
                            addressElements += '<div><i class="i-mobile"></i><span name="Mobile">' + value.Mobile + '</span></div>';
                        }
                        if (value.Fax != "") {
                            addressElements += '<div><i class="i-fax"></i><span name="Fax">' + value.Fax + '</span></div>';
                        }
                        if (value.Website != "") {
                            addressElements += '<div><span name="Website">' + value.Website + '</span></div>';
                        }
                        addressElements += '</div>';
                        addressElements += ' <div class="cssClassChange"><a href="#" rel="popuprel" name="EditAddress" value="' + value.AddressID + '" Flag="0"><i class="icon-edit"></i></a> <a href="#" name="DeleteAddress" class="cssDeleteBtn" value="' + value.AddressID + '"><i class="icon-delete"></i></a></div></div></div>';
                    }
                };
                if (defaultShippingExist) {
                    $("#hdnDefaultShippingExist").val('1');
                } else {
                    $("#hdnDefaultShippingExist").val('0');
                    $("#liDefaultShippingAddress").html("<span class=\"cssClassNotFound\">" + getLocale(AspxUserDashBoard, 'You have not set default shipping adresss yet!') + "</span>");
                }
                if (defaultBillingExist) {
                    $("#hdnDefaultBillingExist").val('1');
                } else {
                    $("#hdnDefaultBillingExist").val('0');
                    $("#liDefaultBillingAddress").html("<span class=\"cssClassNotFound\">" + getLocale(AspxUserDashBoard, "You have not set default billing adresss yet!") + "</span>");
                }
                if (additionalAddrCount == 0) {
                    addressElements += "<span class=\"cssClassNotFound\">";
                    addressElements += getLocale(AspxUserDashBoard, "There is no additional address entries!");
                    addressElements += "</span>";
                }
                $("#olAddtionalEntries").html('');
                $("#olAddtionalEntries").html(addressElements);
                $("a[name='EditAddress']").bind("click", function () {
                    addressBook.ClearAll();
                    $("#hdnAddressID").val($(this).attr("value"));
                    $("#txtFirstName").val($(this).parent('div').prev('div').find('span[name="FirstName"]').text());
                    $("#txtLastName").val($(this).parent('div').prev('div').find('span[name="LastName"]').text());
                    $("#txtEmailAddress").val($(this).parent('div').prev('div').find('span[name="Email"]').text());
                    $("#txtCompanyName").val($(this).parent('div').prev('div').find('span[name="Company"]').text());
                    $("#txtAddress1").val($(this).parent('div').prev('div').find('span[name="Address1"]').text());
                    $("#txtAddress2").val($(this).parent('div').prev('div').find('span[name="Address2"]').text());
                    $("#txtCity").val($(this).parent('div').prev('div').find('span[name="City"]').text());
                    $("#txtState").val($(this).parent('div').prev('div').find('span[name="State"]').text());
                    $("#txtZip").val($(this).parent('div').prev('div').find('span[name="Zip"]').text());
                    var countryName = $(this).parent('div').prev('div').find('span[name="Country"]').text();
                    $('#ddlCountry').val($('#ddlCountry option:exactcontains(' + $.trim(countryName) + ')').attr('value'));
                    var countryCode = $('#ddlCountry').val();
                    var txtState = $(this).parent('div').prev('div').find('span[name="State"]').text();
                    $("#ddlUSState").html('');
                    $.ajax({
                        type: "POST",
                        url: aspxservicePath + "AspxCoreHandler.ashx/BindStateList",
                        data: JSON2.stringify({ countryCode: countryCode }),
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $.each(msg.d, function (index, item) {
                                if (item.Text != 'NotExists') {
                                    $('#ddlUSState').show();
                                    $('#txtState').hide();
                                    if (txtState != '' || txtState != null) {
                                        $("#ddlUSState").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                                    }
                                } else {
                                    addressBook.variables.notExists = item.Text;
                                    $('#ddlUSState').hide();
                                    $('#txtState').show();
                                    $("#txtState").val(txtState);
                                }
                            });
                            $('#ddlUSState option').filter(function () { return ($(this).text() == $.trim(txtState)); }).attr('selected', 'selected');
                        }
                    });
                    $("#txtPhone").val($(this).parent('div').prev('div').find('span[name="Phone"]').text());
                    $("#txtMobile").val($(this).parent('div').prev('div').find('span[name="Mobile"]').text());
                    $("#txtFax").val($(this).parent('div').prev('div').find('span[name="Fax"]').text());
                    $("#txtWebsite").val($(this).parent('div').prev('div').find('span[name="Website"]').text());

                    if ($(this).attr("Flag") == 1) {
                        $("#chkShippingAddress").prop('checked', 'checked');
                        $("#chkBillingAddress").prop('checked', 'checked');
                    } else {
                        $("#chkShippingAddress").removeAttr("checked");
                        $("#chkBillingAddress").removeAttr("checked");
                    }
                    if ($(this).attr("value") == addressId) {
                        $("#chkBillingAddress").attr("disabled", "disabled");
                        $("#chkShippingAddress").attr("disabled", "disabled");
                    } else if ($(this).attr("Flag") == 1) {

                        if ($(this).attr("Element") == "Billing") {

                            $("#chkBillingAddress").attr("disabled", "disabled");
                            $("#chkShippingAddress").removeAttr("disabled");
                        } else {
                            $("#chkShippingAddress").attr("disabled", "disabled");
                            $("#chkBillingAddress").removeAttr("disabled");
                        }
                    } else {
                        $("#chkShippingAddress").removeAttr("disabled");
                        $("#chkBillingAddress").removeAttr("disabled");
                    }

                    ShowPopup(this);
                    return false;
                });
                $("div>a[name='DeleteAddress']").bind("click", function () {
                    var addressId = $(this).attr("value");
                    var properties = {
                        onComplete: function (e) {
                            addressBook.ConfirmAddressDelete(addressId, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxUserDashBoard, "Delete Confirmation") + "</h2><p>" + getLocale(AspxUserDashBoard, "Are you sure you want to delete this address?") + "</p>", properties);
                    return false;
                });

            },

            GetAddressBookDetails: function () {
                this.config.method = "AspxCoreHandler.ashx/GetAddressBookDetails";
                this.config.url = aspxservicePath + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = addressBook.BindUserAddressDetails;
                this.ajaxCall(this.config);
            },

            GetAllCountry: function () {
                this.config.method = "AspxCoreHandler.ashx/BindCountryList";
                this.config.url = aspxservicePath + this.config.method;
                this.config.data = "{}";
                this.config.ajaxCallMode = addressBook.BindCountryList;
                this.ajaxCall(this.config);
            },

            GetStateList: function (CountryCode) {
                this.config.method = "AspxCoreHandler.ashx/BindStateList";
                this.config.url = aspxservicePath + this.config.method;
                this.config.data = JSON2.stringify({ countryCode: CountryCode });
                this.config.ajaxCallMode = addressBook.BindStateList;
                this.config.error = addressBook.GetStateListErrorMsg;
                this.ajaxCall(this.config);
            },

            AddUpdateUserAddress: function (state) {
                var addressId = $("#hdnAddressID").val();
                var firstName = Encoder.htmlEncode($("#txtFirstName").val());
                var lastName = Encoder.htmlEncode($("#txtLastName").val());
                var email = $("#txtEmailAddress").val();
                var company = Encoder.htmlEncode($("#txtCompanyName").val());
                var address1 = Encoder.htmlEncode($("#txtAddress1").val());
                var address2 = Encoder.htmlEncode($("#txtAddress2").val());
                var city = $("#txtCity").val();
                var zip = $("#txtZip").val();
                var phone = $("#txtPhone").val();
                var mobile = $("#txtMobile").val();
                var fax = $("#txtFax").val();
                var webSite = $("#txtWebsite").val();
                var countryName = $("#ddlCountry :selected").text();
                var isDefaultShipping = $("#chkShippingAddress").prop("checked");
                var isDefaultBilling = $("#chkBillingAddress").prop("checked");
                var addressObj = {
                    AddressID: addressId,
                    FirstName: firstName,
                    LastName: lastName,
                    Email: email,
                    Company: company,
                    Address1: address1,
                    Address2: address2,
                    City: city,
                    State: state,
                    Zip: zip,
                    Phone: phone,
                    Mobile: mobile,
                    Fax: fax,
                    Country: countryName,
                    WebSite: webSite,
                    DefaultShipping: isDefaultShipping,
                    DefaultBilling: isDefaultBilling
                };
                this.config.url = aspxservicePath + "AspxCoreHandler.ashx/AddUpdateUserAddress";
                this.config.data = JSON2.stringify({
                    addressObj: addressObj,
                    aspxCommonObj: aspxCommonObj
                });
                this.config.ajaxCallMode = addressBook.BindAddressOnAddUpdate;
                this.config.error = addressBook.GetAddressUpdateErrorMsg;
                this.ajaxCall(this.config);
                return false;
            },

            DeleteAddressBook: function (id) {
                this.config.method = "AspxCoreHandler.ashx/DeleteAddressBook";
                this.config.url = aspxservicePath + this.config.method;
                this.config.data = JSON2.stringify({ addressID: id, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = addressBook.BindAddressOnDelete;
                this.config.error = addressBook.GetAddressDeleteErrorMsg;
                this.ajaxCall(this.config);
            },

            CheckAddressAlreadyExist: function () {
                this.config.async = false;
                this.config.method = "AspxCoreHandler.ashx/CheckAddressAlreadyExist";
                this.config.url = aspxservicePath + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = addressBook.SetIfAddressAlreadyExist;
                this.ajaxCall(this.config);
                return addressBook.variables.checkIfExist;
            },

            ConfirmAddressDelete: function (id, event) {
                if (event) {
                    addressBook.DeleteAddressBook(id);
                }
            },

            BindUserAddressDetails: function (msg) {
                addressBook.BindAddressDetails(msg);
            },

            BindCountryList: function (msg) {
                var countryElements = '';
                var value;
                var length = msg.d.length;
                for (var index = 0; index < length; index++) {
                    value = msg.d[index];
                    countryElements += '<option value=' + value.Value + '>' + value.Text + '</option>';
                };
                $("#ddlCountry").html(countryElements);
            },

            BindStateList: function (msg) {
                $('#ddlUSState').show();
                $('#txtState').hide();
                $("#ddlUSState").html('');
                $.each(msg.d, function (index, item) {
                    if (item.Text != 'NotExists') {
                        $("#ddlUSState").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                    } else {
                        $('#ddlUSState').hide();
                        $('#txtState').show();
                    }
                });
            },

            BindAddressOnAddUpdate: function () {
                addressBook.GetAddressBookDetails();
                RemovePopUp();
            },

            BindAddressOnDelete: function () {
                csscody.info("<h2>" + getLocale(AspxUserDashBoard, "Successful Message") + "</h2><p>" + getLocale(AspxUserDashBoard, "Address has been deleted successfully.") + "</p>");
                addressBook.GetAddressBookDetails();
            },

            SetIfAddressAlreadyExist: function (msg) {
                addressBook.variables.checkIfExist = msg.d;
            },

            GetStateListErrorMsg: function () {
                csscody.error('<h2>' + getLocale(AspxUserDashBoard, 'Error Message') + '</h2><p>' + getLocale(AspxUserDashBoard, 'Failed to load state list!') + '</p>');
            },
            GetAddressUpdateErrorMsg: function () {
                addressBook.variables.addNewAddress = -1;
                csscody.error('<h2>' + getLocale(AspxUserDashBoard, 'Error Message') + '</h2><p>' + getLocale(AspxUserDashBoard, 'Failed to update address book!') + '</p>');
            },
            GetAddressDeleteErrorMsg: function () {
                csscody.error('<h2>' + getLocale(AspxUserDashBoard, 'Error Message') + '</h2><p>' + getLocale(AspxUserDashBoard, 'Failed to delete!') + '</p>');
            },

            init: function (config) {
                if (defaultShippingExist.toLowerCase() == 'true') {
                    $("#hdnDefaultShippingExist").val('1');
                } else {
                    $("#hdnDefaultShippingExist").val('0');
                    $("#liDefaultShippingAddress").html("<span class=\"cssClassNotFound\">" + getLocale(AspxUserDashBoard, 'You have not set default shipping adresss yet!') + "</span>");
                }
                if (defaultBillingExist.toLowerCase() == 'true') {
                    $("#hdnDefaultBillingExist").val('1');
                } else {
                    $("#hdnDefaultBillingExist").val('0');
                    $("#liDefaultBillingAddress").html("<span class=\"cssClassNotFound\">" + getLocale(AspxUserDashBoard, "You have not set default billing adresss yet!") + "</span>");
                }
                $("a[name='EditAddress']").bind("click", function () {
                    addressBook.ClearAll();
                    $("#hdnAddressID").val($(this).attr("value"));
                    $("#txtFirstName").val($(this).parent('div').prev('div').find('span[name="FirstName"]').text());
                    $("#txtLastName").val($(this).parent('div').prev('div').find('span[name="LastName"]').text());
                    $("#txtEmailAddress").val($(this).parent('div').prev('div').find('span[name="Email"]').text());
                    $("#txtCompanyName").val($(this).parent('div').prev('div').find('span[name="Company"]').text());
                    $("#txtAddress1").val($(this).parent('div').prev('div').find('span[name="Address1"]').text());
                    $("#txtAddress2").val($(this).parent('div').prev('div').find('span[name="Address2"]').text());
                    $("#txtCity").val($(this).parent('div').prev('div').find('span[name="City"]').text());
                    $("#txtState").val($(this).parent('div').prev('div').find('span[name="State"]').text());
                    $("#txtZip").val($(this).parent('div').prev('div').find('span[name="Zip"]').text());
                    var countryName = $(this).parent('div').prev('div').find('span[name="Country"]').text();
                    $('#ddlCountry').val($('#ddlCountry option:exactcontains(' + $.trim(countryName) + ')').attr('value'));
                    var countryCode = $('#ddlCountry').val();
                    var txtState = $(this).parent('div').prev('div').find('span[name="State"]').text();
                    $("#ddlUSState").html('');
                    $.ajax({
                        type: "POST",
                        url: aspxservicePath + "AspxCoreHandler.ashx/BindStateList",
                        data: JSON2.stringify({ countryCode: countryCode }),
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $.each(msg.d, function (index, item) {
                                if (item.Text != 'NotExists') {
                                    $('#ddlUSState').show();
                                    $('#txtState').hide();
                                    if (txtState != '' || txtState != null) {
                                        $("#ddlUSState").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                                    }
                                } else {
                                    addressBook.variables.notExists = item.Text;
                                    $('#ddlUSState').hide();
                                    $('#txtState').show();
                                    $("#txtState").val(txtState);
                                }
                            });
                            $('#ddlUSState option').filter(function () { return ($(this).text() == $.trim(txtState)); }).attr('selected', 'selected');
                        }
                    });
                    $("#txtPhone").val($(this).parent('div').prev('div').find('span[name="Phone"]').text());
                    $("#txtMobile").val($(this).parent('div').prev('div').find('span[name="Mobile"]').text());
                    $("#txtFax").val($(this).parent('div').prev('div').find('span[name="Fax"]').text());
                    $("#txtWebsite").val($(this).parent('div').prev('div').find('span[name="Website"]').text());

                    if ($(this).attr("Flag") == 1) {
                        $("#chkShippingAddress").prop('checked', 'checked');
                        $("#chkBillingAddress").prop('checked', 'checked');
                    } else {
                        $("#chkShippingAddress").removeAttr("checked");
                        $("#chkBillingAddress").removeAttr("checked");
                    }
                    if ($(this).attr("value") == addressId) {
                        $("#chkBillingAddress").attr("disabled", "disabled");
                        $("#chkShippingAddress").attr("disabled", "disabled");
                    } else if ($(this).attr("Flag") == 1) {

                        if ($(this).attr("Element") == "Billing") {

                            $("#chkBillingAddress").attr("disabled", "disabled");
                            $("#chkShippingAddress").removeAttr("disabled");
                        } else {
                            $("#chkShippingAddress").attr("disabled", "disabled");
                            $("#chkBillingAddress").removeAttr("disabled");
                        }
                    } else {
                        $("#chkShippingAddress").removeAttr("disabled");
                        $("#chkBillingAddress").removeAttr("disabled");
                    }

                    ShowPopup(this);
                    return false;
                });
                $("div>a[name='DeleteAddress']").bind("click", function () {
                    if (typeof (loadedControls) != 'undefined') {
                        var removeItem = "Modules/AspxCommerce/AspxUserDashBoard/AddressBook.ascx";
                        loadedControls = jQuery.grep(loadedControls, function (value) {
                            return value != removeItem;
                        });
                    }
                    var addressId = $(this).attr("value");
                    var properties = {
                        onComplete: function (e) {
                            addressBook.ConfirmAddressDelete(addressId, e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxUserDashBoard, "Delete Confirmation") + "</h2><p>" + getLocale(AspxUserDashBoard, "Are you sure you want to delete this address?") + "</p>", properties);
                    return false;
                });

                $('#ddlUSState').hide();
                $("#lnkNewAddress").bind("click", function () {

                    addressBook.variables.addNewAddress = 1;
                    if (allowMultipleAddress.toLowerCase() == 'false') {
                        var checkExist = addressBook.CheckAddressAlreadyExist();
                        if (checkExist) {
                            csscody.alert('<h2>' + getLocale(AspxUserDashBoard, 'Alert Message') + '</h2><p>' + getLocale(AspxUserDashBoard, 'Multiple address book is disabled.') + '</p>');
                            return false;
                        } else {
                            addressBook.ClearAll();
                            if ($("#hdnDefaultShippingExist").val() == "0") {
                                $("#chkShippingAddress").prop("checked", "checked");
                                $("#chkShippingAddress").prop("disabled", "disabled");
                            }
                            if ($("#hdnDefaultBillingExist").val() == "0") {
                                $("#chkBillingAddress").prop("checked", "checked");
                                $("#chkBillingAddress").prop("disabled", "disabled");
                            }
                            ShowPopup(this);
                        }
                    } else {
                        addressBook.ClearAll();
                        if ($("#hdnDefaultShippingExist").val() == "0") {
                            $("#chkShippingAddress").prop("checked", "checked");
                            $("#chkShippingAddress").prop("disabled", "disabled");
                        }
                        if ($("#hdnDefaultBillingExist").val() == "0") {
                            $("#chkBillingAddress").prop("checked", "checked");
                            $("#chkBillingAddress").prop("disabled", "disabled");
                        }
                        ShowPopup(this);
                    }
                    return false;
                });
                $(".cssClassClose").bind("click", function () {
                    RemovePopUp();
                    return false;
                });
                $("#btnCancelAddNewAddress").bind("click", function () {
                    RemovePopUp();
                    return false;
                });
                $("#btnAddNewAddress").bind("click", function () {
                    RemovePopUp();
                    return false;
                });

                var v = $("#form1").validate({
                    rules: {
                        Phone: {
                            required: true,
                            digits: true
                        },
                        Mobile: {
                            digits: true
                        },
                        Fax: {
                            digits: true
                        }, Zip: { "alpha_dash": true, "required": true }
                    },
                    messages: {
                        FirstName: {
                            required: '*',
                            minlength: "*" + getLocale(AspxUserDashBoard, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        LastName: {
                            required: '*',
                            minlength: "*" + getLocale(AspxUserDashBoard, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        Email: {
                            required: '*',
                            email: getLocale(AspxUserDashBoard, "Please enter valid email id")
                        },
                        Wedsite: {
                            url: '*'
                        },
                        Address1: {
                            required: '*',
                            minlength: "* " + getLocale(AspxUserDashBoard, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        Address2: {
                            maxlength: "*"
                        },
                        Phone: {
                            required: '*',
                            minlength: "*" + getLocale(AspxUserDashBoard, "(at least 7 digits)") + "",
                            maxlength: "*",

                        },
                        Mobile: {
                            minlength: "* " + getLocale(AspxUserDashBoard, "(at least 10 digits)") + "",
                            maxlength: "*"
                        },
                        Fax: {
                            minlength: "*" + getLocale(AspxUserDashBoard, "(at least 7 digits)") + "",
                            maxlength: "*",
                        },
                        Zip: {
                            required: '*',
                            minlength: "* " + getLocale(AspxUserDashBoard, "(at least 2 chars)") + "",
                            maxlength: "*",
                            alpha_dash: "* " + getLocale(AspxUserDashBoard, "(no special character allowed)") + ""
                        },
                        State: {
                            required: '*',
                            minlength: "* " + getLocale(AspxUserDashBoard, "(at least 2 chars)") + "",
                            maxlength: "*"
                        },
                        Zip: {
                            required: '*',
                            minlength: "* " + getLocale(AspxUserDashBoard, "(at least 4 chars)") + "",
                            maxlength: "*"
                        },
                        City: {
                            required: '*',
                            minlength: "* " + getLocale(AspxUserDashBoard, "(at least 2 chars)") + "",
                            maxlength: "*"
                        }
                    },
                    ignore: ":hidden"
                });

                $('#btnSubmitAddress').bind("click", function () {
                    if (typeof (loadedControls) != 'undefined') {
                        var removeItem = "Modules/AspxCommerce/AspxUserDashBoard/AddressBook.ascx";
                        loadedControls = jQuery.grep(loadedControls, function (value) {
                            return value != removeItem;
                        });
                    }
                    if (v.form()) {
                        $.ajax({
                            type: "POST",
                            url: aspxservicePath + "AspxCoreHandler.ashx/BindStateList",
                            data: JSON2.stringify({ countryCode: $("#ddlCountry :selected").val() }),
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            async: false,
                            success: function (msg) {
                                var state = '';
                                if (msg.d.length > 2) {
                                    state = $("#ddlUSState :selected").text();
                                } else {
                                    state = $("#txtState").val();
                                }
                                addressBook.AddUpdateUserAddress(state);
                            }
                        });
                        if (addressBook.variables.addNewAddress > 0) {
                            csscody.info("<h2>" + getLocale(AspxUserDashBoard, 'Successful Message') + '</h2><p>' + getLocale(AspxUserDashBoard, 'Address has been saved successfully.') + "</p>");
                        } else if (addressBook.variables.addNewAddress < 0) {
                            return false;
                        } else {
                            csscody.info("<h2>" + getLocale(AspxUserDashBoard, "Successful Message") + "</h2><p>" + getLocale(AspxUserDashBoard, "Address has been updated successfully.") + "</p>");
                        }
                        addressBook.variables.addNewAddress = 0;
                        return false;
                    } else {
                        return false;
                    }
                    return false;
                });

                $("#ddlCountry ").bind("change", function () {
                    addressBook.GetStateList($(this).val());
                    $("#txtState").val('');
                });
            }
        };
        addressBook.init();
    });

    //]]>
</script>

<div class="cssClasMyAddressInformation">
    <div class="cssClassHeader">
        <h2>
            <span class="sfLocale">Address Book</span></h2>

    </div>
    <div class="cssClassCommonWrapper">
        <div class="cssClassCol1 clearfix">
            <div class="cssClassAddressBook sfCol_48">
                <asp:Literal ID="ltrShipAddress" runat="server" EnableViewState="false"></asp:Literal>
            </div>
            <div class="cssClassAddressBook1 sfCol_48">
                <asp:Literal ID="ltrBillingAddress" runat="server" EnableViewState="false"></asp:Literal>
            </div>
        </div>
        <div class="cssClassCol3">
            <div class="cssClassAddressBook clearfix">
                <h3 class="sfLocale">Additional Addresses Entries</h3>
                <asp:Literal ID="ltrAdditionalEntries" runat="server" EnableViewState="false"></asp:Literal>
            </div>

        </div>
    </div>
    <div class="sfButtonwrapper">
        <a href="#" id="lnkNewAddress" rel="popuprel" class="cssClassGreenBtn"><span class="sfLocale">New Address +</span>
        </a>
    </div>
</div>
<div class="popupbox" id="popuprel">
    <div class="cssPopUpBody">
        <div class="cssClassCloseIcon">
            <button type="button" class="cssClassClose">
                <span class="sfLocale"><i class="i-close"></i>Close</span></button>
        </div>
        <h2>
            <asp:Label ID="lblAddressTitle" runat="server" Text="Address Details"
                meta:resourcekey="lblAddressTitleResource1"></asp:Label>
        </h2>
        <div class="sfFormwrapper cssClassTMar10">
            <div id="tblNewAddress">
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name:" CssClass="cssClassLabel" meta:resourcekey="lblFirstNameResource1"></asp:Label><span class="cssClassRequired">*</span>
                        </div>

                        <input type="text" id="txtFirstName" name="FirstName" class="required" minlength="2" maxlength="40" />
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name:"
                                CssClass="cssClassLabel" meta:resourcekey="lblLastNameResource1"></asp:Label><span
                                    class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtLastName" name="LastName" class="required" minlength="2" maxlength="40" />
                    </li>
                </ul>
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="cssClassLabel"
                                meta:resourcekey="lblEmailResource1"></asp:Label><span
                                    class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtEmailAddress" name="Email" class="required email" minlength="2" />
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblCompany" Text="Company:" runat="server"
                                CssClass="cssClassLabel" meta:resourcekey="lblCompanyResource1"></asp:Label>
                        </div>
                        <input type="text" id="txtCompanyName" name="Company" maxlength="40" />
                    </li>
                </ul>
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblAddress1" Text="Address 1:" runat="server"
                                CssClass="cssClassLabel" meta:resourcekey="lblAddress1Resource1"></asp:Label><span
                                    class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtAddress1" name="Address1" class="required" minlength="2" maxlength="250" />
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblAddress2" Text="Address 2:" runat="server"
                                CssClass="cssClassLabel" meta:resourcekey="lblAddress2Resource1"></asp:Label>
                        </div>
                        <input type="text" id="txtAddress2" name="Address2" maxlength="250" />
                    </li>
                </ul>
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblCountry" Text="Country:" runat="server"
                                CssClass="cssClassLabel" meta:resourcekey="lblCountryResource1"></asp:Label><span
                                    class="cssClassRequired">*</span>
                        </div>
                        <asp:Literal ID="ltrCountry" runat="server" EnableViewState="false"></asp:Literal>
                    </li>

                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblState" Text="State/Province:" runat="server"
                                CssClass="cssClassLabel" meta:resourcekey="lblStateResource1"></asp:Label><span
                                    class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtState" name="State" class="required" minlength="2" maxlength="250" />
                        <select id="ddlUSState" class="sfListmenu">
                        </select>
                    </li>
                </ul>
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblZip" Text="Zip/Postal Code:" runat="server"
                                CssClass="cssClassLabel" meta:resourcekey="lblZipResource1"></asp:Label><span
                                    class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtZip" name="Zip" class="required alpha_dash" minlength="4" maxlength="10" />
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblCity" Text="City:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblCityResource1"></asp:Label><span
                                    class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtCity" name="City" class="required" minlength="2" maxlength="250" />
                    </li>
                </ul>
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblPhone" Text="Phone:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblPhoneResource1"></asp:Label><span
                                    class="cssClassRequired">*</span>
                        </div>
                        <input type="text" id="txtPhone" name="Phone" class="required number" minlength="7" maxlength="15" />
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblMobile" Text="Mobile:" runat="server"
                                CssClass="cssClassLabel" meta:resourcekey="lblMobileResource1"></asp:Label>
                        </div>
                        <input type="text" id="txtMobile" name="Mobile" class="number" minlength="10" maxlength="15" />
                    </li>
                </ul>
                <ul class="clearfix">
                    <li class="cssTextLi">
                        <div>
                            <asp:Label ID="lblFax" Text="Fax:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblFaxResource1"></asp:Label>
                        </div>
                        <input type="text" id="txtFax" name="Fax" class="number" minlength="7" maxlength="20" />
                    </li>
                    <li class="cssTextLi NoRmargin">
                        <div>
                            <asp:Label ID="lblWebsite" Text="Website:" runat="server"
                                CssClass="cssClassLabel" meta:resourcekey="lblWebsiteResource1"></asp:Label>
                        </div>
                        <input type="text" id="txtWebsite" name="Wedsite" class="url" maxlength="50" />
                    </li>
                </ul>
                <ul id="trShippingAddress" class="clearfix">
                    <li class="cssBillingChk">
                        <input type="checkbox" id="chkShippingAddress" class="sfLocale" />
                        <asp:Label ID="lblDefaultShipping" Text=" Use as Default Shipping Address" runat="server"
                            CssClass="cssClassLabel" meta:resourcekey="lblDefaultShippingResource1"></asp:Label>
                    </li>
                </ul>
                <ul id="trBillingAddress" class="clearfix">
                    <li class="cssBillingChk">
                        <input type="checkbox" id="chkBillingAddress" class="sfLocale" />
                        <asp:Label ID="lblDefaultBilling" Text="Use as Default Billing Address" runat="server"
                            CssClass="cssClassLabel" meta:resourcekey="lblDefaultBillingResource1"></asp:Label>
                    </li>
                </ul>
                <div class="sfButtonwrapper">
                    <label class="cssClassGreenBtn icon-save">
                        <button type="submit" id="btnSubmitAddress" class="cssClassButtonSubmit">
                            <span class="sfLocale">Save</span></button></label>
                </div>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hdnAddressID" />
<input type="hidden" id="hdnDefaultShippingExist" />
<input type="hidden" id="hdnDefaultBillingExist" />
