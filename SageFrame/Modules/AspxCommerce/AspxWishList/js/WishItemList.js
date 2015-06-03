(function ($) {
    arrItemListType = new Array();
    var View = function (p) {
        p = $.extend({
            CountryName: '',
            UserEmailIDWishList: 0,
            ServerNameVariables: '',
            AllowAddToCart: '',
            AllowOutStockPurchaseSetting: '',
            ShowImageInWishlistSetting: '',
            NoImageWishListSetting: '',
            CurrentPage: 0,
            RowTotal: 0,
            ArrayLength: 0,
            ServicePath: aspxservicePath + "AspxCommerceWebService.asmx/",
            UserFullName: '',
            userModuleIDWishList: 0
        }, p);

        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName(),
            CustomerID: AspxCommerce.utils.GetCustomerID(),
            SessionCode: AspxCommerce.utils.GetSessionCode()
        };
        var countryName = p.CountryName;
        var userEmailWishList = p.UserEmailIDWishList;
        var serverLocation = p.ServerNameVariables;
        var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
        var Imagelist = '';
        var WishList = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: p.ServicePath + "Handler.ashx/",
                method: "",
                url: "",
                ajaxCallMode: "",
                error: ""
            },

            ajaxCall: function (config) {

                $.ajax({
                    type: WishList.config.type,
                    beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", p.userModuleIDWishList);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: WishList.config.contentType,
                    cache: WishList.config.cache,
                    async: WishList.config.async,
                    url: WishList.config.url,
                    data: WishList.config.data,
                    dataType: WishList.config.dataType,
                    success: function (data) {
                        WishList.config.ajaxCallMode(data);
                    },
                    error: function () {
                        WishList.config.error;
                    }
                });
            },

            BindWishItemOnDeletion: function () {
                $('#tblWishItemList tbody tr').each(function () {
                    $(this).find('td input:checkbox').removeAttr('checked');
                });
                WishList.GetWishItemList(1, $("#ddlWishListPageSize").val(), 0, $("#ddlWishListSortBy option:selected").val()); csscody.info("<h2>" + getLocale(AspxWishItems, "Successful Message") + "</h2><p>" + getLocale(AspxWishItems, "Wished item has been deleted successfully.") + "</p>");
            },
            //Send the list of images to the ImageResizer
            ResizeImageDynamically: function (Imagelist) {
                ImageType = {
                    "Large": "Large",
                    "Medium": "Medium",
                    "Small": "Small"
                };
                WishList.config.method = "DynamicImageResizer";
                WishList.config.url = aspxservicePath + "AspxImageResizerHandler.ashx/" + WishList.config.method;
                WishList.config.data = JSON2.stringify({ imgCollection: Imagelist, type: ImageType.Small, aspxCommonObj: aspxCommonObj });
                WishList.config.ajaxCallMode = WishList.ResizeImageSuccess;
                WishList.ajaxCall(WishList.config);
            },
            ResizeImageSuccess: function () {
            },
            BindWishItemList: function (msg) {
                $("#tblWishItemList>tbody").html('');
                $("#chkHeading").prop('checked', false);
                if (msg.d.length > 0) {
                    $("#divWishListSort").css('display', 'block');
                    arrItemListType.length = 0;
                    arrItemListType = [];
                    $.each(msg.d, function (index, item) {
                        if (item.ImagePath != "") {
                            Imagelist += item.ImagePath + ';';
                        }
                        WishList.BindWishListItems(item, index);
                    });
                    WishList.ResizeImageDynamically(Imagelist);
                    if (arrItemListType.length > 0) {
                        var items_per_page = $('#ddlWishListPageSize').val();
                        $('#Pagination').pagination(rowTotal, {
                            items_per_page: items_per_page,
                            current_page: currentPage,
                            callfunction: true,
                            function_name: { name: WishItem.Get, limit: $('#ddlWishListPageSize').val(), sortBy: $('#ddlWishListSortBy').val() },
                            prev_text: "" + getLocale(AspxWishItems, "Prev") + "",
                            next_text: "" + getLocale(AspxWishItems, "Next") + "",
                            prev_show_always: false,
                            next_show_always: false
                        });
                        $('#divWishListPageNumber').show();
                    }

                    $(".comment").each(function () {
                        if ($(this).val() == "") {
                            $(this).addClass("lightText").val(getLocale(AspxWishItems, "enter a comment.."));
                        }
                    });

                    $(".comment").bind("focus", function () {
                        if ($(this).val() == "enter a comment..") {
                            $(this).removeClass("lightText").val("");
                        }
                    });
                    $(".comment").bind("blur", function () {
                        if ($(this).val() == "") {
                            $(this).val("enter a comment..").addClass("lightText");
                        }
                    });
                    $("#tblWishItemList>thead").css("display", "");
                    $("#wishitemBottom").show();
                } else {
                    $("#divWishListSort").css('display', 'none');
                    $("#divWishListPageNumber").css('display', 'none');
                    $("#tblWishItemList>thead").hide();
                    $("#wishitemBottom").hide();
                    $("#tblWishItemList").html("<tr><td class=\"cssClassNotFound\">" + getLocale(AspxWishItems, "Your wishlist is empty!") + "</td></tr>");
                }
                $("#lnkMyWishlist").html("<i class=\"i-mywishlist\"></i>" + getLocale(AspxWishItems, "My Wishlist") + " <span class=\"cssClassTotalCount\">[" + msg.d.length + "]</span>");
            },
            BindWishItemSingleDelete: function () {
                WishList.GetWishItemList(1, $("#ddlWishListPageSize").val(), 0, $("#ddlWishListSortBy option:selected").val()); csscody.info("<h2>" + getLocale(AspxWishItems, "Successful Message") + "</h2><p>" + getLocale(AspxWishItems, "Wished item has been deleted successfully.") + "</p>");
            },
            GetUpdateWishListMsg: function () {
                csscody.info("<h2>" + getLocale(AspxWishItems, "Successful Message") + "</h2><p>" + getLocale(AspxWishItems, "Your wishlist has been updated successfully.") + "</p>");
            },
            BindWishListOnClear: function () {
                WishList.GetWishItemList(1, $("#ddlWishListPageSize").val(), 0, $("#ddlWishListSortBy option:selected").val()); csscody.info("<h2>" + getLocale(AspxWishItems, "Successful Message") + "</h2><p>" + getLocale(AspxWishItems, "Your wishlist has been cleared successfully.") + "</p>");
            },
            OnSharingWishList: function () {
                csscody.info("<h2>" + getLocale(AspxWishItems, "Successful Message") + "</h2><p>" + getLocale(AspxWishItems, "Email has been sent successfully.") + "</p>");
                $('#txtEmailID').val('');
                $('#txtEmailMessage').val('');
                $('#divWishListContent').show();
                $('#divShareWishList').hide();
                $('#fade, #popuprel5, .cssClassClose').fadeOut();
            },
            GetUpdateWishListErrorMsg: function () {
                csscody.error("<h2>" + getLocale(AspxWishItems, "Error Message") + "</h2><p>" + getLocale(AspxWishItems, "Failed to update wish list!") + "</p>");
            },
            GetWishListClearErrorMsg: function () {
                csscody.error("<h2>" + getLocale(AspxWishItems, "Error Message") + "</h2><p>" + getLocale(AspxWishItems, "Failed to clear wish list!") + "</p>");
            },
            GetSendEmailErrorMsg: function () {
                $('#fade, #popuprel5, .cssClassClose').fadeOut();
                csscody.error("<h2>" + getLocale(AspxWishItems, "Error Message") + "</h2><p>" + getLocale(AspxWishItems, "Failed to sending mail!") + "</p>");
            },
            trim: function (str, chars) {
                return WishList.ltrim(WishList.rtrim(str, chars), chars);
            },
            ltrim: function (str, chars) {
                chars = chars || "\\s";
                return str.replace(new RegExp("^[" + chars + "]+", "g"), "");
            },
            rtrim: function (str, chars) {
                chars = chars || "\\s";
                return str.replace(new RegExp("[" + chars + "]+$", "g"), "");
            },
            validateMultipleEmailsCommaSeparated: function (value) {
                var result = value.split(",");
                for (var i = 0; i < result.length; i++)
                    if (!WishList.validateEmail(result[i]))
                        return false;
                return true;
            },
            validateEmail: function (field) {
                var regex = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                return (regex.test(WishList.trim(field))) ? true : false;
            },
            ClearShareWishItemForm: function () {
                $('#txtEmailID').val('');
                $('#txtEmailMessage').val('');
                $('#tblWishItemList tbody tr').each(function () {
                    $(this).find('td input[type="checkbox"]').removeAttr('checked');
                    $(this).find('td .comment').val('');
                });
                $('#chkHeading').removeAttr('checked');
            },
            DeleteWishListItem: function (wishItemId) {
                var properties = {
                    onComplete: function (e) {
                        WishList.ConfirmDeleteWishItem(wishItemId, e);
                    }
                };
                csscody.confirm("<h2>" + getLocale(AspxWishItems, "Delete Confirmation") + "</h2><p>" + getLocale(AspxWishItems, "Are you sure you want to delete this wished item?") + "</p>", properties);
            },
            ConfirmDeleteWishItem: function (id, event) {
                if (event) {
                    this.config.method = "DeleteWishItem";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({ wishItemID: id, aspxCommonObj: aspxCommonObj });
                    this.config.ajaxCallMode = WishList.BindWishItemOnDeletion;
                    this.ajaxCall(this.config);
                }
            },
            GetWishItemList: function (offset, limit, _currentPage, sortBy) {
                var count = 10;
                var isAll = 1;
                currentPage = _currentPage;
                WishList.config.method = "GetWishItemList";
                WishList.config.url = WishList.config.baseURL + WishList.config.method;
                WishList.config.data = JSON2.stringify({ offset: offset, limit: limit, aspxCommonObj: aspxCommonObj, flagShowAll: isAll, count: count, sortBy: sortBy });
                WishList.config.ajaxCallMode = WishList.BindWishItemList;
                WishList.ajaxCall(WishList.config);
            },
            BindWishListItems: function (response, index) {
                rowTotal = response.RowTotal;
                arrItemListType.push(response.ItemID);
                var imagePath = itemImagePath + response.ImagePath;
                if (response.ImagePath == "") {
                    imagePath = p.NoImageWishListSetting;
                } else if (response.AlternateText == "") {
                    response.AlternateText = response.ItemName;
                }
                var WishDate = WishList.DateDeserialize(response.WishDate, "M/d/yyyy");
                var itemSKU = JSON2.stringify(response.SKU);
                var cosVaraint = JSON2.stringify(response.CostVariantValueIDs);
                var href = '';
                var cartUrl = '';
                var dataContent = '';
                dataContent += 'data-class="addtoCart" data-type="button" data-ItemTypeID="' + response.ItemTypeID + '" data-ItemID="' + response.ItemID + '" data-addtocart="';
                dataContent += 'addtocart' + response.ItemID + '"data-title="' + response.ItemName + '"';
                dataContent += "data-onclick='AspxCommerce.RootFunction.AddToCartFromJS(" + response.ItemID + "," + parseFloat(response.Price).toFixed(2) + "," + JSON2.stringify(response.SKU) + "," + 1 + "," + "\"" + response.ItemCostVariantValue + "\"" + "," + "this);'";
                if (response.CostVariantValueIDs == "") {
                    cartUrl = '#';
                    href = AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + pageExtension;
                } else {
                    cartUrl = AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + pageExtension + '?varId=' + response.CostVariantValueIDs + '';
                    href = AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + pageExtension + '?varId=' + response.CostVariantValueIDs + '';
                }
                if (index % 2 == 0) {
                    Items = '<tr class="sfEven" id="tr_' + response.ItemID + '"><td class="cssClassWishItemChkbox"><input type="checkbox" id="' + response.WishItemID + '" class="cssClassWishItem" /></td><td class="cssClassWishItemImg"><div class="cssClassImage"><img src="' + aspxRootPath + imagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div></td><td class="cssClassWishItemDetails">';
                }
                else {
                    Items = '<tr class="sfOdd" id="tr_' + response.ItemID + '"><td class="cssClassWishItemChkbox"><input type="checkbox" id="' + response.WishItemID + '" class="cssClassWishItem" /></td><td class="cssClassWishItemImg"><div class="cssClassImage"><img src="' + aspxRootPath + imagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div></td><td class="cssClassWishItemDetails">';
                }
                Items += '<a href="' + href + '">' + response.ItemName + '</a>';
                Items += '<div class="cssClassWishDate"><i class="i-calender"></i>' + WishDate + '</div><div class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.WishItemID + '" class="comment">' + response.Comment + '</textarea></div></td><td><input type="hidden" name="hdnCostVariandValueIDS" value=' + cosVaraint + ' /><span>' + response.ItemCostVariantValue + '</span></td>';
                if (p.AllowAddToCart.toLowerCase() == 'true') {
                    if (p.AllowOutStockPurchaseSetting.toLowerCase() == 'false') {
                        if (response.IsOutOfStock) {
                            if (response.ItemTypeID == 5) {
                                Items += "<td class='cssClassWishToCart'><p class='cssClassGroupPriceWrapper'>" + getLocale(AspxWishItems, "Starting At ") + "<span class='cssClassPrice cssClassFormatCurrency'>" + parseFloat(response.Price).toFixed(2) + "</span><div " + dataContent + " class='sfButtonwrapper cssClassOutOfStock'><a href=\"#\"><span>" + getLocale(AspxWishItems, "Out Of Stock") + "</span></p></a></div></td>";
                            }
                            else {
                                Items += "<td class='cssClassWishToCart'><span class='cssClassPrice cssClassFormatCurrency'>" + parseFloat(response.Price).toFixed(2) + "</span><div " + dataContent + " class='sfButtonwrapper cssClassOutOfStock'><a href=\"#\"><span>" + getLocale(AspxWishItems, "Out Of Stock") + "</span></a></div></td>";
                            }
                        }
                        else {
                            if (response.ItemTypeID == 5) {
                                Items += "<td class='cssClassWishToCart'><p class='cssClassGroupPriceWrapper'>" + getLocale(AspxWishItems, "Starting At ") + "<span class='cssClassPrice cssClassFormatCurrency'>" + parseFloat(response.Price).toFixed(2) + "</span></p><div " + dataContent + " class='sfButtonwrapper'><label class='icon-cart cssClassCartLabel cssClassGreenBtn'><button type=\"button\" class=\"addtoCart\" addtocart=\"addtocart" + response.ItemID + "\" onclick='AspxCommerce.RootFunction.AddToCartFromJS(" + response.ItemID + "," + parseFloat(response.Price).toFixed(2) + "," + JSON2.stringify(response.SKU) + "," + 1 + "," + "\"" + response.ItemCostVariantValue + "\"" + "," + "this);'><span>" + getLocale(AspxWishItems, "Cart +") + "</span></button></label></div></td>";
                            }
                            else {
                                Items += "<td class='cssClassWishToCart'><span class='cssClassPrice cssClassFormatCurrency'>" + parseFloat(response.Price).toFixed(2) + "</span><div " + dataContent + " class='sfButtonwrapper'><label class='icon-cart cssClassCartLabel cssClassGreenBtn'><button type=\"button\" class=\"addtoCart\" addtocart=\"addtocart" + response.ItemID + "\" onclick='AspxCommerce.RootFunction.AddToCartFromJS(" + response.ItemID + "," + parseFloat(response.Price).toFixed(2) + "," + JSON2.stringify(response.SKU) + "," + 1 + "," + "\"" + response.ItemCostVariantValue + "\"" + "," + "this);'><span>" + getLocale(AspxWishItems, "Cart +") + "</span></button></label></div></td>";
                            }
                        }
                    }
                    else {
                        if (response.ItemTypeID == 5) {
                            Items += "<td class='cssClassWishToCart'><p class='cssClassGroupPriceWrapper'>" + getLocale(AspxWishItems, "Starting At ") + "<span class='cssClassPrice cssClassFormatCurrency'>" + parseFloat(response.Price).toFixed(2) + "</span></p><div " + dataContent + " class='sfButtonwrapper'><label class='icon-cart cssClassCartLabel cssClassGreenBtn'><button type=\"button\" class=\"addtoCart\" addtocart=\"addtocart" + response.ItemID + "\" onclick='AspxCommerce.RootFunction.AddToCartFromJS(" + response.ItemID + "," + parseFloat(response.Price).toFixed(2) + "," + JSON2.stringify(response.SKU) + "," + 1 + "," + "\"" + response.ItemCostVariantValue + "\"" + "," + "this);'><span>" + getLocale(AspxWishItems, "Cart +") + "</span></button></label></div></td>";
                        }
                        else {
                            Items += "<td class='cssClassWishToCart'><span class='cssClassPrice cssClassFormatCurrency'>" + parseFloat(response.Price).toFixed(2) + "</span><div " + dataContent + " class='sfButtonwrapper'><label class='icon-cart cssClassCartLabel cssClassGreenBtn'><button type=\"button\" class=\"addtoCart\" addtocart=\"addtocart" + response.ItemID + "\" onclick='AspxCommerce.RootFunction.AddToCartFromJS(" + response.ItemID + "," + parseFloat(response.Price).toFixed(2) + "," + JSON2.stringify(response.SKU) + "," + 1 + "," + "\"" + response.ItemCostVariantValue + "\"" + "," + "this);'><span>" + getLocale(AspxWishItems, "Cart +") + "</span></button></label></div></td>";
                        }

                    }
                }
                Items += "<td class='cssClassDelete'><a onclick='WishItem.Delete(" + response.WishItemID + ")' title=\"" + getLocale(AspxWishItems, "Delete") + "\"><i class='i-delete' original-title=\"" + getLocale(AspxWishItems, "Delete") + "\"></i></a></td></tr>";

                $("#tblWishItemList>tbody").append(Items);
                var cookieCurrency = Currency.cookie.read();
                Currency.currentCurrency = BaseCurrency;
                Currency.format = 'money_format';
                Currency.convertAll(Currency.currentCurrency, cookieCurrency);
                $(".tipsy").remove();
                $('.cssClassImage img[title]').tipsy({ gravity: 'n' });
                //$(".i-delete").tipsy({ gravity: 'n' });
                if (p.ShowImageInWishlistSetting.toLowerCase() == 'false') {
                    $('.cssClassWishItemDetails div').hide();
                }
                $(".comment").keypress(function (e) {
                    if (e.which == 35) {
                        return false;
                    }
                });
                $("#lnkMyWishlist").html("<i class=\"i-mywishlist\"></i>" + getLocale(AspxWishItems, "My Wishlist") + " <span class=\"cssClassTotalCount\">[" + rowTotal + "]</span>");

                var removeItem = "Modules/AspxCommerce/AspxWishList/WishItemList.ascx";
                if (typeof(loadedControls) != 'undefined') {
                    loadedControls = jQuery.grep(loadedControls, function (value) {
                        return value != removeItem;
                    });
                }
            },
            ismaxlength: function (obj) {
                var mlength = obj.getAttribute ? parseInt(obj.getAttribute("maxlength")) : "";
                if (obj.getAttribute && obj.value.length > mlength)
                    obj.value = obj.value.substring(0, mlength);
            },
            AddToCartToJS: function (itemId, itemPrice, itemSKU, itemQuantity) {
                AspxCommerce.RootFunction.AddToCartFromJS(itemId, itemPrice, itemSKU, itemQuantity);
            },
            ConfirmSingleDelete: function (id, event) {
                if (event) {
                    this.config.method = "DeleteWishItem";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({ wishItemID: id, aspxCommonObj: aspxCommonObj });
                    this.config.ajaxCallMode = WishList.BindWishItemSingleDelete;
                    this.ajaxCall(this.config);
                }
            },
            DeleteWishItem: function (itemId) {
                var properties = {
                    onComplete: function (e) {
                        WishList.ConfirmSingleDelete(itemId, e);
                    }
                };
                csscody.confirm("<h2>" + getLocale(AspxWishItems, "Delete Confirmation") + "</h2><p>" + getLocale(AspxWishItems, "Are you sure you want to delete wished item?") + "</p>", properties);
            },
            DeleteMultipleWishList: function (ids, event) {
                WishList.ConfirmDeleteWishItem(ids, event);
            },
            UpdateWishList: function () {
                var comment = '';
                var wishItemId = '';
                $(".cssClassWishItemChkbox").find('input:checkbox').each(function (i) {
                    if ($(this).prop('id') != 'chkHeading') {
                        if ($(this).prop("checked")) {
                            wishItemId += parseInt($(this).prop("id").replace(/[^0-9]/gi, '')) + '#';
                            comment += $(this).parents('tr:eq(0)').find('.comment').val() + '#';
                        }
                    }
                });
                if (wishItemId == "") {

                    csscody.alert('<h2>' + getLocale(AspxWishItems, "Information Alert") + '</h2><p>' + getLocale(AspxWishItems, "Please select at least one wish item to update.") + '</p>');
                    return false;
                }
                comment = comment.substring(0, comment.length - 1);
                wishItemId = wishItemId.substring(0, wishItemId.length - 1);
                WishList.config.method = "UpdateWishList";
                WishList.config.url = WishList.config.baseURL + WishList.config.method;
                WishList.config.data = JSON2.stringify({ wishItemID: wishItemId, comment: comment, aspxCommonObj: aspxCommonObj });
                WishList.config.ajaxCallMode = WishList.GetUpdateWishListMsg;
                WishList.config.error = WishList.GetUpdateWishListErrorMsg;
                WishList.ajaxCall(WishList.config);

                var removeItem = "Modules/AspxCommerce/AspxWishList/WishItemList.ascx";
                if (typeof (loadedControls) != 'undefined') {
                    loadedControls = jQuery.grep(loadedControls, function (value) {
                        return value != removeItem;
                    });
                }
            },
            DeleteAllWishList: function () {
                this.config.method = "ClearWishList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = WishList.BindWishListOnClear;
                this.config.error = WishList.GetWishListClearErrorMsg;
                this.ajaxCall(this.config);
            },
            ClearWishList: function () {
                var properties = {
                    onComplete: function (e) {
                        if (e) {
                            WishList.DeleteAllWishList(e);
                        }
                    }
                };
                csscody.confirm("<h2>" + getLocale(AspxWishItems, "Delete Confirmation") + "</h2><p>" + getLocale(AspxWishItems, "Are you sure you want to clear wish list items?") + "</p>", properties);
            },
            DateDeserialize: function (content, format) {
                content = eval('new ' + content.replace(/[/]/gi, ''));
                return formatDate(content, format);
            },
            SendShareItemEmail: function () {
                var emailID = '';
                var message = '';
                var itemId = '';
                var arr = new Array;
                var elems = '';
                $('#tblWishItemList tbody tr').find('td input[type="checkbox"]').each(function () {
                    if ($(this).prop('checked')) {
                        itemId += $(this).attr("id").replace(/[^0-9]/gi, '') + ',';
                    }
                });
                itemId = itemId.substring(0, itemId.length - 1);
                emailID = $('#txtEmailID').val();
                message = Encoder.htmlEncode($('#txtEmailMessage').val());
                var senderName = p.UserFullName;
                var senderEmail = userEmailWishList;
                var receiverEmailID = emailID;
                var subject = "Take A Look At " + senderName + "'s " + " WishList";
                var msgbodyhtml = '';
                var msgCommenthtml = '';
                var serverHostLoc = 'http://' + serverLocation;
                var fullDate = new Date();
                var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : (fullDate.getMonth() + 1);
                if (twoDigitMonth.length == 2) {
                } else if (twoDigitMonth.length == 1) {
                    twoDigitMonth = '0' + twoDigitMonth;
                }
                var currentDate = fullDate.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();
                var dateyear = fullDate.getFullYear();
                var trLength = $('#tblWishItemList tbody tr').length;
                var tdWishItemList = new Array();
                var wishlistItemInfo = {
                    itemInfo: {
                        src: '',
                        alt: '',
                        title: '',
                        price: '',
                        href: '',
                        hrefHtml: '',
                        htmlComment: ''
                    }
                };
                $('#tblWishItemList tbody tr').each(function () {
                    if ($(this).find('td input[type="checkbox"]').prop('checked')) {
                        wishlistItemInfo.itemInfo.src = $(this).find('td div.cssClassImage img').prop('src');
                        wishlistItemInfo.itemInfo.alt = $(this).find('td div.cssClassImage img').prop('alt');
                        wishlistItemInfo.itemInfo.title = $(this).find('td div.cssClassImage img').prop('alt');
                        wishlistItemInfo.itemInfo.price = $(this).find('td.cssClassWishItemDetails span').html();
                        wishlistItemInfo.itemInfo.href = $(this).find('td.cssClassWishItemDetails a').prop('href');
                        wishlistItemInfo.itemInfo.hrefHtml = $(this).find('td.cssClassWishItemDetails a').html();
                        wishlistItemInfo.itemInfo.htmlComment = $(this).find('td.cssClassWishComment textarea').val();
                        tdWishItemList.push(JSON2.stringify(wishlistItemInfo.itemInfo));
                    } else {
                        return true;
                    }
                });

                var data = "[" + tdWishItemList + "]";
                var wishlistObj = {
                    ItemID: itemId,
                    SenderName: senderName,
                    SenderEmail: senderEmail,
                    ReceiverEmail: receiverEmailID,
                    Subject: subject,
                    Message: message,
                    MessageBody: data
                };
                this.config.method = "SendShareWishItemEmail";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, wishlistObj: wishlistObj });
                this.config.ajaxCallMode = WishList.OnSharingWishList;
                this.config.error = WishList.GetSendEmailErrorMsg;
                this.ajaxCall(this.config);
            },
            HideMessage: function () {
                $('.errorMessage').hide();
            },
            Init: function () {
                rowTotal = p.RowTotal;
                arrayLength = p.ArrayLength;
                currentPage = 0;
                    $("#ddlWishListPageSize").html('');
                    var pagesize = '';
                    pagesize += "<option data-html-text='5' value='5'>" + 5 + "</option>";
                    pagesize += "<option data-html-text='10' value='10'>" + 10 + "</option>";
                    pagesize += "<option data-html-text='15' value='15'>" + 15 + "</option>";
                    pagesize += "<option data-html-text='20' value='20'>" + 20 + "</option>";
                    pagesize += "<option data-html-text='25' value='25'>" + 25 + "</option>";
                    pagesize += "<option data-html-text='40' value='40'>" + 40 + "</option>";
                    $("#ddlWishListPageSize").html(pagesize);
                    $("#divWishItemsList").show();
                    $('#divWishListSort').show();
                    $('#wishitemBottom').show();
                    if (rowTotal > 0) {
                        if (parseInt(arrayLength) > 0) {
                            var items_per_page = 5;
                            $('#Pagination').pagination(arrayLength, {
                                items_per_page: items_per_page,
                                current_page: currentPage,
                                callfunction: true,
                                function_name: { name: WishList.GetWishItemList, limit: 5, sortBy: 1 },
                                prev_text: "" + getLocale(AspxWishItems, "Prev") + "",
                                next_text: "" + getLocale(AspxWishItems, "Next") + "",
                                prev_show_always: false,
                                next_show_always: false
                            });
                            $('#divWishListPageNumber').show();
                        }
                    }
                    $("#ddlWishListPageSize").change(function () {
                        WishItem.Get(1, $("#ddlWishListPageSize").val(), 0, $("#ddlWishListSortBy option:selected").val());
                    });
                    $("#ddlWishListSortBy").change(function () {
                        var items_per_page = $('#ddlWishListPageSize').val();
                        var offset = 1;
                        WishItem.Get(offset, items_per_page, 0, $(this).val());
                    });
                    $("#divWishListContent").show();
                    $('.errorMessage').hide();
                    $('#divShareWishList').hide();
                    if (userFriendlyURL) {
                        $("#lnkContinueShopping").prop("href", '' + aspxRedirectPath + homeURL + pageExtension);
                    } else {
                        $("#lnkContinueShopping").prop("href", '' + aspxRedirectPath + homeURL);
                    }
                    $("#continueInStore").bind("click", function () {
                        if (userFriendlyURL) {
                            window.location.href = aspxRedirectPath + homeURL + pageExtension;
                        } else {
                            window.location.href = aspxRedirectPath + homeURL;
                        }
                        return false;
                    });
                    $('#shareWishList').bind("click", function () {

                        $('#divShareWishList').show();
                        WishList.HideMessage();
                        var wishChecked = false;
                        $('#tblWishItemList tbody tr').each(function () {
                            if ($(this).find('td input[type="checkbox"]').prop('checked')) {
                                wishChecked = true;
                                return true;
                            }
                        });
                        if (wishChecked == true) {
                            var removeItem = "Modules/AspxCommerce/AspxWishList/WishItemList.ascx";
                            if (typeof(loadedControls) != 'undefined') {
                                loadedControls = jQuery.grep(loadedControls, function (value) {
                                    return value != removeItem;
                                });
                            }
                            ShowPopupControl('popuprel5');
                        } else {
                            csscody.alert("<h2>" + getLocale(AspxWishItems, "Information Alert") + "</h2><p>" + getLocale(AspxWishItems, "Please select at least one item.") + "</p>");
                        }
                    });

                    $(".cssClassClose").bind("click", function () {
                        $('#fade, #popuprel5').fadeOut();
                    });

                    $('#btnShareWishItem').bind("click", function () {
                        var emailIDsColln = $('#txtEmailID').val();
                        if (WishList.validateMultipleEmailsCommaSeparated(emailIDsColln)) {
                            WishList.SendShareItemEmail();
                        } else {
                            $('.errorMessage').show();
                        }
                        RemovePopUp();
                    });
                    $("#btnDeletedMultiple").click(function () {
                        var wishItemIds = '';
                        $(".cssClassWishItemChkbox").find('input:checkbox').each(function (i) {

                            if ($(this).prop('id') != 'chkHeading') {
                                if ($(this).prop("checked")) {
                                    wishItemIds += $(this).prop('id') + ',';
                                }
                            }
                        });
                        if (wishItemIds == "") {
                            csscody.alert('<h2>' + getLocale(AspxWishItems, "Information Alert") + '</h2><p>' + getLocale(AspxWishItems, "Please select at least one wish item to delete.") + '</p>');
                            return false;
                        }
                        var properties = {
                            onComplete: function (e) {
                                WishList.DeleteMultipleWishList(wishItemIds, e);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxWishItems, "Delete Confirmation") + "</h2><p>" + getLocale(AspxWishItems, "Are you sure you want to delete selected wished items?") + "</p>", properties);
                    });
                    $("#chkHeading").on('change', function () {
                        if ($(this).prop("checked")) {
                            $('#tblWishItemList tbody tr').each(function () {
                                $(this).find('td input[type="checkbox"]').prop('checked', 'checked');
                            });
                        } else {
                            $('#tblWishItemList tbody tr').each(function () {
                                $(this).find('td input[type="checkbox"]').removeAttr('checked');
                            });
                        }
                    });

                    $('#tblWishItemList tbody tr').find(".cssClassWishItemChkbox").on('change', function () {
                        var totalitems = $('#tblWishItemList tbody tr').find('td input[type="checkbox"]').length;
                        var matchedcount = 0;
                        $('#tblWishItemList tbody tr').find('td input[type="checkbox"]').each(function () {

                            if ($(this).prop('checked'))
                                matchedcount++;

                        });
                        if (matchedcount == totalitems) {
                            $("#chkHeading").prop('checked', 'checked');
                        } else {
                            $("#chkHeading").removeAttr('checked');
                        }
                    });

                    $(".comment").each(function () {
                        if ($(this).val() == "") {
                            $(this).addClass("lightText").val(getLocale(AspxWishItems, "enter a comment.."));
                        }
                    });

                    $(".comment").bind("focus", function () {
                        if ($(this).val() == "enter a comment..") {
                            $(this).removeClass("lightText").val("");
                        }
                    });
                    $(".comment").bind("blur", function () {
                        if ($(this).val() == "") {
                            $(this).val("enter a comment..").addClass("lightText");
                        }
                    });
            }
        };
     

        WishItem = function () {
          
            return {
                Get: WishList.GetWishItemList,
                Clear: WishList.ClearWishList,
                Delete: WishList.DeleteWishItem,
                AddToCart: WishList.AddToCartToJS,
                Update: WishList.UpdateWishList,
                ismaxlength: WishList.ismaxlength,
                HideMessage: WishList.HideMessage
            };
        }();

        WishList.Init();
    };
    $.fn.WishItemList = function (p) {
        View(p);
    };
})(jQuery);