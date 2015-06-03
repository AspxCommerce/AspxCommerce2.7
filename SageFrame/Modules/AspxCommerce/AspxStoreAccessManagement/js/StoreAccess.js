var StoreAccessmanage = "";
$(function () {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var Edit = 0;
    var isActive = false;
    var StoreAccessKeyID = 0;
    var StoreAccessData = '';
    var isIP = false;
    var auto = false;
    var storeAccessVar = '';
    var dletemsgonce = false;
    var validIp = /^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$/;
    $.validator.addMethod('IP4Checker', function (value) {
        var ipAddress = /^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$/;
        return value.match(ipAddress);
    }, getLocale(AspxStoreAccessManagement, 'Invalid IP address'));

    $.validator.addMethod('IP4CheckerTo', function (value) {
        var ipAddress = /^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$/;
        if (value == "") {
            return true;
        }
        return value.match(ipAddress);
    }, getLocale(AspxStoreAccessManagement, 'Invalid IP address'));
    $.validator.addMethod('EmailChecker', function (value) {
        var email_regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        return value.match(email_regex);
    }, getLocale(AspxStoreAccessManagement, 'Invalid Email'));

    $.validator.addMethod('CardChecker', function (value) {
        var creditCardRegex = /^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$/;

        return value.match(creditCardRegex);
    }, getLocale(AspxStoreAccessManagement, 'Invalid CreditCard'));
    $.validator.addMethod('DomainChecker', function (value) {
        var urlRegex = /[^w{3}\.]([a-zA-Z0-9]([a-zA-Z0-9\-]{0,65}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}/igm;
        return value.match(urlRegex);
    }, getLocale(AspxStoreAccessManagement, 'Invalid Domain'));

    StoreAccessmanage = {
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
            url: ""
        },

        init: function () {
            var v = $("#form1").validate({
                messages: {
                    txtNameValidate: {
                        required: '*'
                    },
                    msg: {
                        required: '*'
                    },
                    status: {
                        required: '*'
                    }
                },
                rules: {
                    ipAddress1: {
                        required: true,
                        IP4Checker: true
                    },
                    ipAddress: {
                        IP4CheckerTo: true
                    },
                    Email2: {
                        EmailChecker: true
                    },
                    Credit: {
                        required: true,
                        CardChecker: true
                    },
                    website: {
                        required: true,
                        DomainChecker: true
                    }
                }
            });

            var $tabs = $('#dvTabPanel').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
            $tabs.tabs('option', 'active', 0);
            StoreAccessmanage.SelectFirstTab();
            StoreAccessmanage.LoadStoreAccessMgmtStaticImage();
            StoreAccessmanage.bindAll();
            $("#txtsrchIPDate,#txtIPEndDate,#txtsrchDomainDate,#txtDomainEndDate,#txtsrchEmailDate,#txtEmailEndDate,#txtsrchCreditCardDate,#txtCardEndDate,#txtsrchCustomerDate,#txtCustomerDate").datepicker({ dateFormat: 'yy/mm/dd' });

            $(".cssClassClose").click(function () {
                RemovePopUp();
                StoreAccessmanage.ClearEditAddForm();
                $('#IPTO').remove();
                auto = false;
                Edit = 0;
                v.resetForm();
                StoreAccessmanage.ClearPopUpError();
            });

            $("#btnCancelSaveUpdate").click(function () {
                RemovePopUp();
                StoreAccessmanage.ClearEditAddForm();
                $('#IPTO').remove();
                auto = false;
                Edit = 0;
                v.resetForm();
                StoreAccessmanage.ClearPopUpError();
               
            });
            $('#txtsrchIP,#txtsrchIPDate,#txtIPEndDate,#SelectStatusIP').keyup(function (event) {
                if (event.keyCode == 13) {
                    StoreAccessmanage.searchStoreAccess('#btnSrchIP');
                }
            });
            $('#txtsrchDomain,#txtsrchDomainDate,#txtDomainEndDate,#SelectStatusDomain').keyup(function (event) {
                if (event.keyCode == 13) {
                    StoreAccessmanage.searchStoreAccess('#btnSrchDomain');
                }
            });
            $('#txtsrchEmail,#txtsrchEmailDate,#txtEmailEndDate,#SelectStatusEmail').keyup(function (event) {
                if (event.keyCode == 13) {
                    StoreAccessmanage.searchStoreAccess('#btnSrchEmail');
                }
            });
            $('#txtsrchCreditCard,#txtsrchCreditCardDate,#txtCardEndDate,#SelectStatusCreditCard').keyup(function (event) {
                if (event.keyCode == 13) {
                    StoreAccessmanage.searchStoreAccess('#btnSrchCreditCard');
                }
            });
            $('#txtsrchCustomer,#txtsrchCustomerDate,#txtCustomerDate,#SelectStatusCustomer').keyup(function (event) {
                if (event.keyCode == 13) {
                    StoreAccessmanage.searchStoreAccess('#btnSrchCustomer');
                }
            });

            $('#btnDeleteSelectedIP,#btnDeleteSelectedDomain,#btnDeleteSelectedEmail,#btnDeleteSelectedCreditCard,#btnDeleteSelectedCustomer').click(function () {
                dletemsgonce = false;
                var attribute_ids = new Array;
                attribute_ids = [];
                switch ($(this).prop('id')) {
                    case "btnDeleteSelectedIP":
                        attribute_ids = SageData.Get("gdvIP").Arr;
                        if (attribute_ids.length > 0) {
                            var properties = {
                                onComplete: function (e) {
                                    StoreAccessmanage.ConfirmDeleteMultiple(attribute_ids, e);
                                }
                            };
                            csscody.confirm("<h2>" + getLocale(AspxStoreAccessManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "Are you sure you want to delete the selected IP?") + "</p>", properties);
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxStoreAccessManagement, "Information Alert") + '</h2><p>' + getLocale(AspxStoreAccessManagement, "Please select at least one IP before delete.") + '</p>');
                        }
                        break;
                    case "btnDeleteSelectedDomain":
                        attribute_ids = SageData.Get("gdvDomain").Arr;
                        if (attribute_ids.length > 0) {
                            var properties = {
                                onComplete: function (e) {
                                    StoreAccessmanage.ConfirmDeleteMultiple(attribute_ids, e);
                                }
                            };
                            csscody.confirm("<h2>" + getLocale(AspxStoreAccessManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "Are you sure you want to delete the selected domain?") + "<p>", properties);
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxStoreAccessManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "Please select at least one domain before delete.") + '</p>');
                        }
                        break;
                    case "btnDeleteSelectedEmail":
                        attribute_ids = SageData.Get("gdvEmail").Arr;
                        if (attribute_ids.length > 0) {
                            var properties = {
                                onComplete: function (e) {
                                    StoreAccessmanage.ConfirmDeleteMultiple(attribute_ids, e);
                                }
                            };
                            csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete the selected email?</p>", properties);
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxStoreAccessManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "Please select at least one email before delete.") + '</p>');
                        }
                        break;
                    case "btnDeleteSelectedCreditCard":
                        attribute_ids = SageData.Get("gdvCreditCard").Arr;
                        if (attribute_ids.length > 0) {
                            var properties = {
                                onComplete: function (e) {
                                    StoreAccessmanage.ConfirmDeleteMultiple(attribute_ids, e);
                                }
                            };
                            csscody.confirm("<h2>" + getLocale(AspxStoreAccessManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "Are you sure you want to delete the selected credit card?") + "</p>", properties);
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxStoreAccessManagement, "Information Alert") + '</h2><p>' + getLocale(AspxStoreAccessManagement, "Please select at least one credit card before delete.") + '</p>');
                        }
                        break;
                    case "btnDeleteSelectedCustomer":
                        attribute_ids = SageData.Get("gdvCustomer").Arr;
                        if (attribute_ids.length > 0) {
                            var properties = {
                                onComplete: function (e) {
                                    StoreAccessmanage.ConfirmDeleteMultiple(attribute_ids, e);
                                }
                            };
                            csscody.confirm("<h2>" + getLocale(AspxStoreAccessManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "Are you sure you want to delete the selected customer?") + "</p>", properties);
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxStoreAccessManagement, "Information Alert") + '</h2><p>' + getLocale(AspxStoreAccessManagement, "Please select at least one customer before delete.") + '</p>');
                        }
                        break;
                    default:
                        break;
                }
            });
            $("#txtStoreAccessValue").bind("focusout", function () {
                switch (StoreAccessKeyID) {
                    case "3":
                        StoreAccessmanage.CheckEmail();
                        break;
                }
            });

            $('#btnAddIP,#btnAddDomain,#btnAddEmail,#btnAddCustomer,#btnAddCreditCard').click(function () {
                StoreAccessmanage.ClearPopUpError();
                $('#txtStoreAccessValue').removeAttr("name");
                $('#IPTO').remove();
                Edit = 0;
                var btn = $(this).prop('id');
                switch (btn) {
                    case "btnAddIP":
                        isIP = true;
                        $('#txtStoreAccessValue').prop("name", "ipAddress1");
                        storeAccessVar = "IP address";
                        StoreAccessKeyID = $('input[name="IP"]').val();
                        $('<tr id="IPTO"><td><asp:Label ID="lblIpRangeTo" Text="To:" runat="server" CssClass="cssClassLabel"><span class="cssClassLabel">' + getLocale(AspxStoreAccessManagement, "To:") + '</span></asp:Label> </td><td class="cssClassTableRightCol"><input type="text" id="txtStoreAccessValueTo" class="sfInputbox"  name="ipAddress" /></td></tr>').insertAfter('#forIPonly');
                        $('#' + lblStoreAccessValueID).html(getLocale(AspxStoreAccessManagement, "Add new IP Range:"));
                        $('#' + lblAddEditStoreAccessTitleID).html(getLocale(AspxStoreAccessManagement, "Add new IP Range:"));
                        StoreAccessmanage.ClearEditAddForm();
                        $('#chkStatusActive').prop('checked', true);
                        ShowPopupControl("popuprel");
                        break;
                    case "btnAddDomain":
                        isIP = false;
                        $('#txtStoreAccessValue').prop("name", "website");
                        storeAccessVar = "Domain";
                        StoreAccessKeyID = $('input[ name="Domain"]').val();
                        $('#' + lblStoreAccessValueID).html(getLocale(AspxStoreAccessManagement, "Add new Domain:"));
                        $('#' + lblAddEditStoreAccessTitleID).html(getLocale(AspxStoreAccessManagement, "Add new Domain:"));
                        StoreAccessmanage.ClearEditAddForm();
                        $('#chkStatusActive').prop('checked', true);
                        ShowPopupControl("popuprel");
                        break;
                    case "btnAddEmail":
                        isIP = false;
                        $('#txtStoreAccessValue').prop("name", "Email2");
                        storeAccessVar = "Email";
                        StoreAccessKeyID = $('input[ name="Email"]').val();
                        $('#' + lblStoreAccessValueID).html(getLocale(AspxStoreAccessManagement, "Add new Email:"));
                        $('#' + lblAddEditStoreAccessTitleID).html(getLocale(AspxStoreAccessManagement, "Add new Email:"));
                        StoreAccessmanage.ClearEditAddForm();
                        $('#chkStatusActive').prop('checked', true);
                        ShowPopupControl("popuprel"); $('#txtStoreAccessValue').autocomplete({
                            open: function () {
                                setTimeout(function () {
                                    $('.ui-autocomplete').css('z-index', 9999);
                                }, 0);
                            },
                            source: function (req, res) {
                                $.ajax({
                                    url: aspxservicePath + "AspxCoreHandler.ashx/GetAspxUserEmail", beforeSend: function (request) {
                                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                                        request.setRequestHeader("UMID", umi);
                                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                                        request.setRequestHeader("PType", "v");
                                        request.setRequestHeader('Escape', '0');
                                    },
                                    data: JSON2.stringify({ email: $('#txtStoreAccessValue').val(), aspxCommonObj: aspxCommonObj }),
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    dataFilter: function (da) { return da; },
                                    success: function (da) {

                                        res($.map(da.d, function (item) {
                                            return {
                                                value: item.Email
                                            };
                                        }));

                                    },
                                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                                        alert(textStatus);
                                    }
                                });
                            },
                            minLength: 1
                        });
                        $('#txtStoreAccessValue').autocomplete("enable");
                        break;
                    case "btnAddCreditCard":
                        isIP = false;
                        storeAccessVar = "Credit Card";
                        $('#txtStoreAccessValue').prop("name", "Credit");
                        StoreAccessKeyID = $('input[ name="CreditCard"]').val();
                        $('#' + lblStoreAccessValueID).html(getLocale(AspxStoreAccessManagement, "Add new Credit Card:"));
                        $('#' + lblAddEditStoreAccessTitleID).html(getLocale(AspxStoreAccessManagement, "Add new Credit Card:"));
                        StoreAccessmanage.ClearEditAddForm();
                        $('#chkStatusActive').prop('checked', true);
                        ShowPopupControl("popuprel");
                        break;
                    case "btnAddCustomer":
                        isIP = false;
                        auto = true;
                        storeAccessVar = "Customer";
                        StoreAccessKeyID = $('input[ name="Customer"]').val();
                        $('#' + lblStoreAccessValueID).html(getLocale(AspxStoreAccessManagement, "Add new Customer:"));
                        $('#' + lblAddEditStoreAccessTitleID).html(getLocale(AspxStoreAccessManagement, "Add new Customer:"));
                        StoreAccessmanage.ClearEditAddForm();
                        $('#chkStatusActive').prop('checked', true);
                        ShowPopupControl("popuprel");
                        $('#txtStoreAccessValue').autocomplete({
                            open: function () {
                                setTimeout(function () {
                                    $('.ui-autocomplete').css('z-index', 9999);
                                }, 0);
                            },
                            source: function (request, response) {
                                $.ajax({
                                    url: aspxservicePath + "AspxCoreHandler.ashx/GetAspxUser", beforeSend: function (request) {
                                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                                        request.setRequestHeader("UMID", umi);
                                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                                        request.setRequestHeader("PType", "v");
                                        request.setRequestHeader('Escape', '0');
                                    },
                                    data: JSON2.stringify({ userName: $('#txtStoreAccessValue').val(), aspxCommonObj: aspxCommonObj }),
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    dataFilter: function (data) { return data; },
                                    success: function (data) {
                                        if (auto) {
                                            response($.map(data.d, function (item) {
                                                return {
                                                    value: item.UserName
                                                };
                                            }));
                                        }
                                    },
                                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                                        alert(textStatus);
                                    }
                                });
                            },
                            minLength: 2
                        });
                        $('#txtStoreAccessValue').autocomplete("enable");

                        break;
                    default:
                        break;
                }
            });

            $('#btnSubmit').click(function () {               
                if (v.form()) {
                    RemovePopUp();
                    StoreAccessmanage.SaveUpdate();
                    StoreAccessmanage.ClearEditAddForm();
                    $('#IPTO').remove();
                    auto = false;
                } else {
                    return false;
                }

            });
        },
        ajaxCall: function (config) {
            $.ajax({
                type: StoreAccessmanage.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: StoreAccessmanage.config.contentType,
                cache: StoreAccessmanage.config.cache,
                async: StoreAccessmanage.config.async,
                url: StoreAccessmanage.config.url,
                data: StoreAccessmanage.config.data,
                dataType: StoreAccessmanage.config.dataType,
                success: StoreAccessmanage.ajaxSuccess,
                error: StoreAccessmanage.ajaxFailure
            });
        },

        ClearPopUpError: function () {
            $('#txtStoreAccessValue').removeClass('error');
            $('#txtStoreAccessValue').parents('td').find('label').remove();
            $('#txtReason').removeClass('error');
            $('#txtReason').parents('td').find('label').remove();
        },

        ConfirmDeleteMultiple: function (attribute_ids, event) {
            if (event) {
                for (var id in attribute_ids) {
                    StoreAccessmanage.DeleteStoreAccessByID(attribute_ids[id], event);
                }
                attribute_ids = [];
                // StoreAccessmanage.bindAll();
            }
        },

        LoadStoreAccessMgmtStaticImage: function () {
            $('#ajaxStoreAccessImage1').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxStoreAccessImage2').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxStoreAccessImage3').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxStoreAccessImage4').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            $('#ajaxStoreAccessImage5').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        SelectFirstTab: function () {
            var $tabs = $('#dvMultipleAddress').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
            $tabs.tabs('option', 'active', 0);
        },

        bindAll: function () {
            StoreAccessmanage.LoadStoreAccessIPs(null, null, null, null);
            StoreAccessmanage.LoadStoreAccessDomains(null, null, null, null);
            StoreAccessmanage.LoadStoreAccessEmails(null, null, null, null);
            StoreAccessmanage.LoadStoreAccessCreditCards(null, null, null, null);
            StoreAccessmanage.LoadStoreAccessCustomers(null, null, null, null);
            StoreAccessmanage.loadkey();
        },

        ClearEditAddForm: function () {
            $('#txtStoreAccessValue').val('');
            $('#txtReason').val('');
            $('#editAdd input[type="radio"]').prop('checked', false);
        },
        CheckEmail: function () {
            switch (StoreAccessKeyID) {
                case "3":
                    var isAdmin = false;
                    var email = $.trim($('#txtStoreAccessValue').val());
                    $.ajax({
                        url: aspxservicePath + "AspxCoreHandler.ashx/CheckEmailAddress", beforeSend: function (request) {
                            request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                            request.setRequestHeader("UMID", umi);
                            request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                            request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                            request.setRequestHeader("PType", "v");
                            request.setRequestHeader('Escape', '0');
                        },
                        data: JSON2.stringify({ email: email, aspxCommonObj: aspxCommonObj }),
                        dataType: "json",
                        type: "POST",
                        async: false,
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            isAdmin = data.d;
                        },
                        error: function () {
                        }
                    });
                    if (isAdmin) {
                        csscody.alert('<h2>' + getLocale(AspxStoreAccessManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "You are not allowed to add admin email address") + ' </p>');
                        $("#txtStoreAccessValue").val('');
                        return false;
                    }

                    break;
            }

        },

        SaveUpdate: function (e) {
            if (isIP) {

                if ($.trim($('#txtStoreAccessValueTo').val()) == "") {
                    StoreAccessData = $('#txtStoreAccessValue').val();
                    if (validIp.test(StoreAccessData)) {
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxStoreAccessManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "Invalid Ip Address [From]") + '</p>');
                        return false;
                    }
                } else {
                    StoreAccessData = $('#txtStoreAccessValue').val() + '-' + $('#txtStoreAccessValueTo').val();
                    if (validIp.test($('#txtStoreAccessValue').val())) {
                        if (validIp.test($('#txtStoreAccessValueTo').val())) {
                            if ($('#txtStoreAccessValue').val() > $('#txtStoreAccessValueTo').val()) {
                                csscody.alert('<h2>' + getLocale(AspxStoreAccessManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "Invalid Ip Range") + ' </p>');
                                return false;
                            } else {

                            }
                        } else {
                            $('#txtStoreAccessValueTo').addClass("required error");
                        }
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxStoreAccessManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "Invalid Ip Address [From]") + '</p>');
                        return false;
                    }
                }

            } else {
                StoreAccessData = $('#txtStoreAccessValue').val();
            }

            if ($('#chkStatusActive').prop('checked')) {
                isActive = true;
            } else {
                isActive = false;
            }
            switch (StoreAccessKeyID) {
                case "5":
                    if ($.trim($('#txtStoreAccessValue').val().toLowerCase()) == 'superuser') {
                        csscody.alert('<h2>' + getLocale(AspxStoreAccessManagement, "Information Alert") + '</h2><p>' + getLocale(AspxStoreAccessManagement, "You are not allowed to add superuser") + '</p>');
                        return false;
                    }

                    break;
                case "3":
                    StoreAccessmanage.CheckEmail();
                    break;
            }

            var storeData = isIP == false ? $.trim($("#txtStoreAccessValue").val()) : ($.trim($("#txtStoreAccessValue").val()) + '-' + $.trim($("#txtStoreAccessValueTo").val()));
            if (Edit > 0) {
                StoreAccessmanage.config.url = StoreAccessmanage.config.baseURL + "SaveUpdateStoreAccess";
                StoreAccessmanage.config.data = JSON2.stringify({ edit: Edit, storeAccessKeyID: StoreAccessKeyID, storeAccessData: StoreAccessData, reason: $('#txtReason').val(), isActive: isActive, aspxCommonObj: aspxCommonObj });
                StoreAccessmanage.config.ajaxCallMode = 1;
                StoreAccessmanage.ajaxCall(StoreAccessmanage.config);
            }
            else {
                $.ajax({
                    url: this.config.baseURL + "CheckExisting", beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    data: JSON2.stringify({ aspxCommonObj: aspxCommonObj, storeAccesskeyId: StoreAccessKeyID, accessData: storeData }),
                    dataType: "json",
                    type: "POST",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d == 'true' || data.d == true) {
                            StoreAccessmanage.config.url = StoreAccessmanage.config.baseURL + "SaveUpdateStoreAccess";
                            StoreAccessmanage.config.data = JSON2.stringify({ edit: Edit, storeAccessKeyID: StoreAccessKeyID, storeAccessData: StoreAccessData, reason: $('#txtReason').val(), isActive: isActive, aspxCommonObj: aspxCommonObj });
                            StoreAccessmanage.config.ajaxCallMode = 1;
                            StoreAccessmanage.ajaxCall(StoreAccessmanage.config);
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxStoreAccessManagement, "Alert Message") + '</h2><p>' + getLocale(AspxStoreAccessManagement, "Inputed Data is Already Exist!.") + ' </p>');
                        }
                    },
                    error: function () {
                    }
                });
            }


        },

        loadkey: function () {
            this.config.url = this.config.baseURL + "GetStoreKeyID";
            this.config.data = '{}';
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        CheckExisting: function () {
            this.config.url = this.config.baseURL + "CheckExisting";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, storeAccesskeyId: StoreAccessKeyID, accessData: storeData })
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },

        searchStoreAccess: function (Id) {
            switch ($(Id).prop('id')) {
                case "btnSrchIP":
                    var Search = $.trim($('#txtsrchIP').val());
                    var startDate = $('#txtsrchIPDate').val();
                    var endDate = $("#txtIPEndDate").val();
                    var Status = $.trim($('#SelectStatusIP').val()) == "" ? null : ($.trim($('#SelectStatusIP').val()) == "True" ? true : false);
                    if (Search.length < 1) {
                        Search = null;
                    }

                    if (startDate.length < 1) {
                        startDate = null;
                    }
                    if (endDate.length < 1) {
                        endDate = null;
                    }
                    if ((startDate <= endDate) || (startDate == null && endDate != '') || (startDate != "" && endDate == null)) {
                        var ipAddress = /^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$/;
                        if (Search != null && Search != "") {
                            if (Search.match(ipAddress)) {
                                StoreAccessmanage.LoadStoreAccessIPs(Search, startDate, endDate, Status);
                            } else {
                                csscody.alert('<h2>' + getLocale(AspxStoreAccessManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "Please enter valid ip address!") + '</p>');
                            }
                        } else {
                            StoreAccessmanage.LoadStoreAccessIPs(Search, startDate, endDate, Status);
                        }


                    } else {
                        csscody.alert("<h2>" + getLocale(AspxStoreAccessManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "From date must be less than To date!") + "</p>");
                    }
                    break;
                case "btnSrchDomain":
                    var Search = $.trim($('#txtsrchDomain').val());
                    var startDate = $.trim($('#txtsrchDomainDate').val());
                    var endDate = $("#txtDomainEndDate").val();
                    var Status = $.trim($('#SelectStatusDomain').val()) == "" ? null : ($.trim($('#SelectStatusDomain').val()) == "True" ? true : false);

                    if (Search.length < 1) {
                        Search = null;
                    }
                    if (startDate.length < 1) {
                        startDate = null;
                    }
                    if (endDate.length < 1) {
                        endDate = null;
                    }
                    if ((startDate <= endDate) || (startDate == null && endDate != '') || (startDate != "" && endDate == null)) {
                        StoreAccessmanage.LoadStoreAccessDomains(Search, startDate, endDate, Status);
                    } else {
                        csscody.alert("<h2>" + getLocale(AspxStoreAccessManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "From date must be less than To date!") + "</p>");
                    }
                    break;
                case "btnSrchEmail":
                    var Search = $.trim($('#txtsrchEmail').val());
                    var startDate = $.trim($('#txtsrchEmailDate').val());
                    var endDate = $("#txtEmailEndDate").val();
                    var Status = $.trim($('#SelectStatusEmail').val()) == "" ? null : ($.trim($('#SelectStatusEmail').val()) == "True" ? true : false);

                    if (Search.length < 1) {
                        Search = null;
                    }
                    if (startDate.length < 1) {
                        startDate = null;
                    }
                    if (endDate.length < 1) {
                        endDate = null;
                    }
                    if ((startDate <= endDate) || (startDate == null && endDate != '') || (startDate != "" && endDate == null)) {
                        StoreAccessmanage.LoadStoreAccessEmails(Search, startDate, endDate, Status);
                    } else {
                        csscody.alert("<h2>" + getLocale(AspxStoreAccessManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "From date must be less than To date!") + "</p>");
                    }
                    break;
                case "btnSrchCreditCard":
                    var Search = $.trim($('#txtsrchCreditCard').val());
                    var startDate = $.trim($('#txtsrchCreditCardDate').val());
                    var endDate = $("#txtCardEndDate").val();
                    var Status = $.trim($('#SelectStatusCreditCard').val()) == "" ? null : ($.trim($('#SelectStatusCreditCard').val()) == "True" ? true : false);

                    if (Search.length < 1) {
                        Search = null;
                    }
                    if (startDate.length < 1) {
                        startDate = null;
                    }
                    if (endDate.length < 1) {
                        endDate = null;
                    }
                    if ((startDate <= endDate) || (startDate == null && endDate != '') || (startDate != "" && endDate == null)) {
                        StoreAccessmanage.LoadStoreAccessCreditCards(Search, startDate, endDate, Status);
                    } else {
                        csscody.alert("<h2>" + getLocale(AspxStoreAccessManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "From date must be less than To date!") + "</p>");
                    }
                    break;
                case "btnSrchCustomer":
                    var Search = $.trim($('#txtsrchCustomer').val());
                    var startDate = $.trim($('#txtsrchCustomerDate').val());
                    var endDate = $("#txtCustomerDate").val();
                    var Status = $.trim($('#SelectStatusCustomer').val()) == "" ? null : ($.trim($('#SelectStatusCustomer').val()) == "True" ? true : false);

                    if (Search.length < 1) {
                        Search = null;
                    }
                    if (startDate.length < 1) {
                        startDate = null;
                    }
                    if (endDate.length < 1) {
                        endDate = null;
                    }
                    if ((startDate <= endDate) || (startDate == null && endDate != '') || (startDate != "" && endDate == null)) {
                        StoreAccessmanage.LoadStoreAccessCustomers(Search, startDate, endDate, Status);
                    } else {
                        csscody.alert("<h2>" + historyLocale(AspxStoreAccessManagement, " Alert") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "From date must be less than To date!") + "</p>");
                    }
                    break;
                default:
                    break;
            }
        },

        LoadStoreAccessIPs: function (search, startdate, enddate, status) {
            this.config.method = "LoadStoreAccessIPs";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("gdvIP_pagesize").length > 0) ? $("#gdvIP_pagesize :selected").text() : 10;

            $("#gdvIP").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxStoreAccessManagement, 'Ids'), name: 'Ids', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxStoreAccessManagement, 'IP'), name: 'IPRange', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Reason'), name: 'Reason', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Added On'), name: 'Added On', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Status'), name: 'Status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Active/Inactive' },
                    { display: getLocale(AspxStoreAccessManagement, 'KeyType'), name: 'KeyType', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxStoreAccessManagement, 'KeyID'), name: 'KeyID', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxStoreAccessManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxStoreAccessManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'StoreAccessmanage.EditCoupons', arguments: '1,2,3,4,5,6,7' },
                    { display: getLocale(AspxStoreAccessManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'StoreAccessmanage.DeleteCoupons', arguments: '1' }
                ],

                rp: perpage,
                nomsg: getLocale(AspxStoreAccessManagement, "No Records Found!"),
                param: { search: search, startDate: startdate, endDate: enddate, status: status, aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 7: { sorter: false } }
            });
        },

        LoadStoreAccessDomains: function (search, startdate, enddate, status) {
            this.config.method = "LoadStoreAccessDomains";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("gdvDomain_pagesize").length > 0) ? $("#gdvDomain_pagesize :selected").text() : 10;

            $("#gdvDomain").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxStoreAccessManagement, 'Ids'), name: 'Ids', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxStoreAccessManagement, 'Domain'), name: 'Domain', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Reason'), name: 'Reason', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Added On'), name: 'Added On', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Status'), name: 'Status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Active/Inactive' },
                    { display: getLocale(AspxStoreAccessManagement, 'KeyType'), name: 'KeyType', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxStoreAccessManagement, 'KeyID'), name: 'KeyID', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxStoreAccessManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxStoreAccessManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'StoreAccessmanage.EditCoupons', arguments: '1,2,3,4,5,6,7' },
                    { display: getLocale(AspxStoreAccessManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'StoreAccessmanage.DeleteCoupons', arguments: '1' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxStoreAccessManagement, "No Records Found!"),
                param: { search: search, startDate: startdate, endDate: enddate, status: status, aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 7: { sorter: false } }
            });
        },

        LoadStoreAccessEmails: function (search, startdate, enddate, status) {
            this.config.method = "LoadStoreAccessEmails";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("gdvEmail_pagesize").length > 0) ? $("#gdvEmail_pagesize :selected").text() : 10;

            $("#gdvEmail").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxStoreAccessManagement, 'Ids'), name: 'Ids', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxStoreAccessManagement, 'Email'), name: 'Email', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Reason'), name: 'Reason', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Added On'), name: 'Added On', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Status'), name: 'Status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Active/Inactive' },
                    { display: getLocale(AspxStoreAccessManagement, 'KeyType'), name: 'KeyType', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxStoreAccessManagement, 'KeyID'), name: 'KeyID', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxStoreAccessManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxStoreAccessManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'StoreAccessmanage.EditCoupons', arguments: '1,2,3,4,5,6,7' },
                    { display: getLocale(AspxStoreAccessManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'StoreAccessmanage.DeleteCoupons', arguments: '' }
                ],

                rp: perpage,
                nomsg: getLocale(AspxStoreAccessManagement, "No Records Found!"),
                param: { search: search, startDate: startdate, endDate: enddate, status: status, aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 7: { sorter: false } }
            });
        },

        EditCoupons: function (tblID, argus) {
            $('#IPTO').remove();
            $('#txtStoreAccessValue').removeAttr("name");
            Edit = 0;
            switch (tblID) {
                case "gdvIP":
                    isIP = true;
                    storeAccessVar = "IP address";
                    $('#txtStoreAccessValue').prop("name", "ipAddress1");
                    $('#' + lblStoreAccessValueID).html(getLocale(AspxStoreAccessManagement, "Edit IP Range From:"));
                    $('#' + lblAddEditStoreAccessTitleID).html(getLocale(AspxStoreAccessManagement, "Edit IP Range"));
                    $('<tr id="IPTO"><td><asp:Label ID="lblIpRangeTo" Text="To:"runat="server" CssClass="cssClassLabel"><span class="cssClassLabel">' + getLocale(AspxStoreAccessManagement, "To:") + '</span></asp:Label> </td><td class="cssClassTableRightCol"><input type="text" id="txtStoreAccessValueTo" class="sfInputbox"  name="ipAddress" /></td></tr>').insertAfter('#forIPonly');

                    Edit = argus[0];
                    StoreAccessKeyID = argus[8];
                    $('#txtStoreAccessValue').val(argus[3].split('-')[0]);
                    $('#txtStoreAccessValueTo').val(argus[3].split('-')[1]);
                    $('#txtReason').val(argus[4]);
                    if (argus[6].toLowerCase() == 'true' || argus[6].toLowerCase() == "yes" || argus[6].toLowerCase() == 'active') {
                        $('#chkStatusActive').prop('checked', true);
                    } else if (argus[6].toLowerCase() == 'false' || argus[6].toLowerCase() == "no" || argus[6].toLowerCase() == 'inactive') {
                        $('#chkStarusDisActive').prop('checked', true);
                    }
                    ShowPopupControl("popuprel");
                    break;
                case "gdvEmail":
                    isIP = false;
                    storeAccessVar = "Email";
                    $('#txtStoreAccessValue').prop("name", "Email2");
                    $('#' + lblStoreAccessValueID).html("Edit Email:");
                    $('#' + lblAddEditStoreAccessTitleID).html("Edit Email");
                    Edit = argus[0];
                    StoreAccessKeyID = argus[8];
                    $('#txtStoreAccessValue').val(argus[3]);
                    $('#txtReason').val(argus[4]);

                    if (argus[6].toLowerCase() == 'true' || argus[6].toLowerCase() == "yes" || argus[6].toLowerCase() == 'active') {
                        $('#chkStatusActive').prop('checked', true);
                    } else if (argus[6].toLowerCase() == 'false' || argus[6].toLowerCase() == "no" || argus[6].toLowerCase() == 'inactive') {
                        $('#chkStarusDisActive').prop('checked', true);
                    }
                    ShowPopupControl("popuprel");

                    break;
                case "gdvDomain":
                    isIP = false;
                    $('#txtStoreAccessValue').prop("name", "website");
                    storeAccessVar = "Domain";
                    $('#' + lblStoreAccessValueID).html(getLocale(AspxStoreAccessManagement, "Edit Domain Name: "));
                    $('#' + lblAddEditStoreAccessTitleID).html(getLocale(AspxStoreAccessManagement, "Edit Domain"));
                    Edit = argus[0];
                    StoreAccessKeyID = argus[8];
                    $('#txtStoreAccessValue').val(argus[3]);
                    $('#txtReason').val(argus[4]);
                    if (argus[6].toLowerCase() == 'true' || argus[6].toLowerCase() == "yes" || argus[6].toLowerCase() == 'active') {
                        $('#chkStatusActive').prop('checked', true);
                    } else if (argus[6].toLowerCase() == 'false' || argus[6].toLowerCase() == "no" || argus[6].toLowerCase() == 'inactive') {
                        $('#chkStarusDisActive').prop('checked', true);
                    }
                    ShowPopupControl("popuprel");

                    break;
                case "gdvCreditCard":
                    isIP = false;
                    storeAccessVar = "Credit card";
                    $('#txtStoreAccessValue').prop("name", "Credit");
                    $('#' + lblStoreAccessValueID).html(getLocale(AspxStoreAccessManagement, "Edit Credit Card No: "));
                    $('#' + lblAddEditStoreAccessTitleID).html(getLocale(AspxStoreAccessManagement, "Edit Credit Card"));
                    Edit = argus[0];
                    StoreAccessKeyID = argus[8];
                    $('#txtStoreAccessValue').val(argus[3]);
                    $('#txtReason').val(argus[4]);
                    if (argus[6].toLowerCase() == 'true' || argus[6].toLowerCase() == "yes" || argus[6].toLowerCase() == 'active') {
                        $('#chkStatusActive').prop('checked', true);
                    } else if (argus[6].toLowerCase() == 'false' || argus[6].toLowerCase() == "no" || argus[6].toLowerCase() == 'inactive') {
                        $('#chkStarusDisActive').prop('checked', true);
                    }
                    ShowPopupControl("popuprel");
                    break;
                case "gdvCustomer":
                    isIP = false;
                    storeAccessVar = "Customer";
                    $('#' + lblStoreAccessValueID).html(getLocale(AspxStoreAccessManagement, "Edit Customer Name: "));
                    $('#' + lblAddEditStoreAccessTitleID).html(getLocale(AspxStoreAccessManagement, "Edit Customer"));
                    Edit = argus[0];
                    StoreAccessKeyID = argus[8];
                    $('#txtStoreAccessValue').val(argus[3]);
                    $('#txtReason').val(argus[4]);

                    if (argus[6].toLowerCase() == 'true' || argus[6].toLowerCase() == "yes" || argus[6].toLowerCase() == 'active') {
                        $('#chkStatusActive').prop('checked', true);
                    } else if (argus[6].toLowerCase() == 'false' || argus[6].toLowerCase() == "no" || argus[6].toLowerCase() == 'inactive') {
                        $('#chkStarusDisActive').prop('checked', true);
                    }
                    ShowPopupControl("popuprel");
                    $('#txtStoreAccessValue').autocomplete("enable");
                    $('#txtStoreAccessValue').autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: aspxservicePath + "AspxCoreHandler.ashx/GetAspxUser", beforeSend: function (request) {
                                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                                    request.setRequestHeader("UMID", umi);
                                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                                    request.setRequestHeader("PType", "v");
                                    request.setRequestHeader('Escape', '0');
                                },
                                data: JSON2.stringify({ userName: $('#txtStoreAccessValue').val(), aspxCommonObj: aspxCommonObj }),
                                dataType: "json",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                dataFilter: function (data) { return data; },
                                success: function (data) {
                                    if (auto) {
                                        response($.map(data.d, function (item) {
                                            return {
                                                value: item.UserName
                                            };
                                        }));
                                    }
                                },
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    csscody.error('<h2>' + getLocale(AspxStoreAccessManagement, "Error Message") + '</h2><p>' + textStatus + '</p>');
                                }
                            });
                        },
                        minLength: 2
                    });
                    break;
                default:
                    break;
            }
        },

        DeleteCoupons: function (tblID, argus) {
            var properties = {
                onComplete: function (e) {
                    StoreAccessmanage.DeleteStoreAccessByID(argus[0], e);
                }
            };
            csscody.confirm("<h2>" + getLocale(AspxStoreAccessManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxStoreAccessManagement, "Are you sure you want to delete?") + "</p>", properties);
        },

        DeleteStoreAccessByID: function (Ids, event) {
            if (event) {
                this.config.url = this.config.baseURL + "DeletStoreAccess";
                this.config.data = JSON2.stringify({ storeAccessID: Ids, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            }
        },

        LoadStoreAccessCustomers: function (search, startdate, enddate, status) {
            this.config.method = "LoadStoreAccessCustomer";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("gdvCustomer_pagesize").length > 0) ? $("#gdvCustomer_pagesize :selected").text() : 10;

            $("#gdvCustomer").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxStoreAccessManagement, 'Ids'), name: 'Ids', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxStoreAccessManagement, 'Customer Name'), name: 'Customer', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Reason'), name: 'Reason', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Added On'), name: 'Added On', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Status'), name: 'Status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Active/Inactive' },
                    { display: getLocale(AspxStoreAccessManagement, 'KeyType'), name: 'KeyType', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxStoreAccessManagement, 'KeyID'), name: 'KeyID', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxStoreAccessManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxStoreAccessManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'StoreAccessmanage.EditCoupons', arguments: '1,2,3,4,5,6,7' },
                    { display: getLocale(AspxStoreAccessManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'StoreAccessmanage.DeleteCoupons', arguments: '1' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxStoreAccessManagement, "No Records Found!"),
                param: { search: search, startDate: startdate, endDate: enddate, status: status, aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 7: { sorter: false } }
            });
        },

        LoadStoreAccessCreditCards: function (search, startdate, enddate, status) {

            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("gdvCreditCard_pagesize").length > 0) ? $("#gdvCreditCard_pagesize :selected").text() : 10;

            $("#gdvCreditCard").sagegrid({
                url: aspxservicePath + "AspxCoreHandler.ashx/",
                functionMethod: 'LoadStoreAccessCreditCards',
                colModel: [
                    { display: getLocale(AspxStoreAccessManagement, 'Ids'), name: 'Ids', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: getLocale(AspxStoreAccessManagement, 'Credit Card'), name: 'Credit Card', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Reason'), name: 'Reason', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Added On'), name: 'Added On', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxStoreAccessManagement, 'Status'), name: 'Status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Active/Inactive' },
                    { display: getLocale(AspxStoreAccessManagement, 'KeyType'), name: 'KeyType', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxStoreAccessManagement, 'KeyID'), name: 'KeyID', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxStoreAccessManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxStoreAccessManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'StoreAccessmanage.EditCoupons', arguments: '1,2,3,4,5,6,7' },
                    { display: getLocale(AspxStoreAccessManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'StoreAccessmanage.DeleteCoupons', arguments: '1' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxStoreAccessManagement, "No Records Found!"),
                param: { search: search, startDate: startdate, endDate: enddate, status: status, aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 7: { sorter: false } }
            });
        },
        ajaxSuccess: function (data) {
            switch (StoreAccessmanage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    StoreAccessmanage.bindAll();
                    if (Edit > 0) {
                        csscody.info('<h2>' + getLocale(AspxStoreAccessManagement, "Successful Message") + '</h2><p>' + storeAccessVar + getLocale(AspxStoreAccessManagement, ' has been updated successfully.') + ' </p>');
                    } else {
                        csscody.info('<h2>' + getLocale(AspxStoreAccessManagement, "Successful Message") + '</h2><p>' + storeAccessVar + getLocale(AspxStoreAccessManagement, ' has been saved successfully.') + ' </p>');
                    }
                    Edit = 0;
                    StoreAccessData = '';
                    break;
                case 2:
                    if (data.d.length > 0) {
                        $.each(data.d, function (index, item) {
                            $('#hdnField').append('<input type="hidden" name=' + item.StoreAccessKeyValue + ' value=' + item.StoreAccessKeyID + ' />');
                        });
                    }
                    break;
                case 3:
                    if (!dletemsgonce) {
                        setTimeout(function () {
                            StoreAccessmanage.bindAll();
                        }, 1000);
                        dletemsgonce = true;
                        csscody.info('<h2>' + getLocale(AspxStoreAccessManagement, "Successful Message") + "</h2><p>" + getLocale(AspxStoreAccessManagement, " Deleted successful. ") + '</p>');

                    }
                    break;

            }
        }
    };
    StoreAccessmanage.init();
});