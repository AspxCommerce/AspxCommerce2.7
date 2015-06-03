var StoreSettings = "";
$(function () {
    var progressTime = null;
    var progress = 0;
    var pcount = 0;
    var percentageInterval = [10, 20, 30, 40, 60, 80, 100];
    var timeInterval = [1, 2, 4, 2, 1, 5, 1];
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };

    Array.prototype.clean = function (deleteValue) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == deleteValue) {
                this.splice(i, 1);
                i--;
            }
        }
        return this;
    };

    StoreSettings = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: aspxservicePath,
            method: "",
            url: ""
        },
        init: function () {
            StoreSettings.HideForLaterUseDivs();
            StoreSettings.InitializeTabs();
            StoreSettings.GetAllCountry();
            StoreSettings.GetAllCurrency();
            StoreSettings.BindItemsViewAsDropDown();
            StoreSettings.BindItemsSortByDropDown();
            StoreSettings.GetAllStoreSettings();
            $("input[DataType='Integer']").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 31 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $('#txtMaximumImageSize,#txtItemLargeThumbnailImageHeight,#txtItemLargeThumbnailImageWidth,#txtItemMediumThumbnailImageHeight,#txtItemMediumThumbnailImageWidth,#txtItemSmallThumbnailImageHeight,#txtItemSmallThumbnailImageWidth,#txtCategoryLargeThumbnailImageHeight,#txtCategoryLargeThumbnailImageWidth,#txtCategoryMediumThumbnailImageHeight,#txtCategoryMediumThumbnailImageWidth,#txtCategorySmallThumbnailImageHeight,#txtCategorySmallThumbnailImageWidth').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                }
            });
            $("#chkResizeProportionally").change(function () {
                if ($(this).prop('checked') == true) {
                    $("#txtItemLargeThumbnailImageHeight").prop('disabled', true);
                    $("#txtItemMediumThumbnailImageHeight").prop('disabled', true);
                    $("#txtItemSmallThumbnailImageHeight").prop('disabled', true);
                }
                else {
                    $("#txtItemLargeThumbnailImageHeight").prop('disabled', false);
                    $("#txtItemMediumThumbnailImageHeight").prop('disabled', false);
                    $("#txtItemSmallThumbnailImageHeight").prop('disabled', false);
                }
            });
            $("#ddlCurrency").prop("disabled", "disabled");
            $("#txtWeightUnit").prop("disabled", "disabled");
            $("#form1").validate({
                messages: {
                    DefaultImageProductURL: {
                        required: '*'
                    },
                    MyAccountURL: {
                        required: '*'
                    },
                    ShoppingCartURL: {
                        required: '*'
                    },
                    MainCurrency: {
                        required: '*'
                    },
                    Weight: {
                        required: '*'
                    },
                    Dimension: {
                        required: '*'
                    },
                    StoreName: {
                        required: '*'
                    },


                    TimeTodeleteAbandCart: {
                        required: '*',
                        number: true
                    },
                    CartAbandonTime: {
                        required: '*',
                        number: true
                    },
                    LowStockQuantity: {
                        required: '*'
                    },
                    OutOfStockQuantity: {
                        required: '*'
                    },
                    ShoppingOptionRange: {
                        required: '*'
                    },
                    EmailFrom: {
                        required: '*'
                    },
                    DefaultTitle: {
                        required: '*'
                    },
                    MaximumImageSize: {
                        required: '*'
                    },
                    MaximumDownloadSize: {
                        required: '*'
                    },
                    ItemLargeThumbnailImageHeight: {
                        required: '*'
                    },
                    ItemLargeThumbnailImageWidth: {
                        required: '*'
                    },
                    ItemMediumThumbnailImageHeight: {
                        required: '*'
                    },
                    ItemMediumThumbnailImageWidth: {
                        required: '*'
                    },
                    ItemSmallThumbnailImageHeight: {
                        required: '*'
                    },
                    ItemSmallThumbnailImageWidth: {
                        required: '*'
                    },
                    CategoryLargeThumbnailImageHeight: {
                        required: '*'
                    },
                    CategoryLargeThumbnailImageWidth: {
                        required: '*'
                    },
                    CategoryMediumThumbnailImageHeight: {
                        required: '*'
                    },
                    CategoryMediumThumbnailImageWidth: {
                        required: '*'
                    },
                    CategorySmallThumbnailImageHeight: {
                        required: '*'
                    },
                    CategorySmallThumbnailImageWidth: {
                        required: '*'
                    },
                    DefaultTimeZone: {
                        required: '*'
                    },
                    MinimumCartSubTotalAmount: {
                        required: '*'
                    },
                    NoOfDisplayItems: {
                        required: '*',
                        maxlength: getLocale(AspxStoreSettingsManagement, "* (no more than 2 digits)")
                    },
                    AdditionalCVR: {

                    },
                    MinCartQuantity: {
                        required: '*'
                    },
                    MaxCartQuantity: {
                        required: '*'
                    },
                    NewCategoryRssCount: { required: '*' },
                    NewOrderRssCount: { required: '*' },
                    NewCustomerRssCount: { required: '*' },
                    NewItemTagRssCount: { required: '*' },
                    NewItemReviewRssCount: { required: '*' },
                    LowStockItemRssCount: { required: '*' }
                },
                submitHandler: function () {
                    AspxCommerce.CheckSessionActive(aspxCommonObj);
                    if (AspxCommerce.vars.IsAlive) {

                        StoreSettings.UpdateStoreSettings();
                    } else {
                        window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + LoginURL + pageExtension;
                    }
                }
            });
            StoreSettings.ImageUploader("fupDefaultImageURL");
            StoreSettings.ImageUploader("fupStoreLogo");

            $("#cbSelectAllBillingCountry,#cbSelectAllShippingCountry").click(function () {
                StoreSettings.SelectAllCountry(this.id);
            });
        },

        ajaxCall: function (config) {
            $.ajax({
                type: StoreSettings.config.type, beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: StoreSettings.config.contentType,
                cache: StoreSettings.config.cache,
                async: StoreSettings.config.async,
                url: StoreSettings.config.url,
                data: StoreSettings.config.data,
                dataType: StoreSettings.config.dataType,
                success: StoreSettings.ajaxSuccess,
                error: StoreSettings.ajaxFailure
            });
        },
        HideForLaterUseDivs: function () {
            $("#storefragment-4").hide();
            $("#divForLaterUseEmail").hide();
            $("#divForLaterUseGS").hide();
            $("#divForLaterUseCPS").hide();
            $("#divForLaterUseOS").hide();
        },

        InitializeTabs: function () {
            var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
            $tabs.tabs('option', 'active', 0);
        },

        GetAllStoreSettings: function () {
            this.config.method = "AspxCoreHandler.ashx/GetAllStoreSettings";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },

        GetAllCountry: function () {
            this.config.method = "AspxCoreHandler.ashx/BindCountryList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = '{}';
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },

        GetAllCurrency: function () {
            this.config.method = "AspxCoreHandler.ashx/BindCurrencyList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
        },
        BindItemsViewAsDropDown: function () {
            this.config.method = "AspxCoreHandler.ashx/BindItemsViewAsList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = "{}";
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
        },
        BindItemsSortByDropDown: function () {
            this.config.method = "AspxCommonHandler.ashx/BindItemsSortByList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = "{}";
            this.config.ajaxCallMode = 6;
            this.ajaxCall(this.config);
        },
        DeleteAllResizedImages: function () {
            this.config.method = "DeleteAllResizedImages";
            this.config.url = aspxservicePath + "AspxImageResizerHandler.ashx/" + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 7;
            this.ajaxCall(this.config);
        },

        BindAllValue: function (obj) {
            $("#hdnPrevFilePath").val(obj.DefaultProductImageURL);
            $("#defaultProductImage").html('<img src="' + aspxRootPath + obj.DefaultProductImageURL + '" class="uploadImage" height="90px" width="100px"/>');
            $("#" + ddlMyAccountURL).val(obj.MyAccountURL);
            $("#" + ddlShoppingCartURL).val(obj.ShoppingCartURL);
            $("#" + ddlDetailsPageURL).val(obj.DetailPageURL);
            $("#" + ddlItemDetailURL).val(obj.ItemDetailURL);
            $("#" + ddlCategoryDetailURL).val(obj.CategoryDetailURL);
            $("#" + ddlSingleCheckOutURL).val(obj.SingleCheckOutURL);
            $("#" + ddlMultiCheckOutURL).val(obj.MultiCheckOutURL);
            $("#" + ddlStoreLocatorURL).val(obj.StoreLocatorURL);
            $("#" + ddlTrackPackageUrl).val(obj.TrackPackageUrl);
            $("#" + ddlShippingDetailPageURL).val(obj.ShipDetailPageURL);
            $("#" + ddlItemMgntPageURL).val(obj.ItemMgntPageURL);
            $("#" + ddlCatMgntPageURL).val(obj.CategoryMgntPageURL);

            $("#hdnPrevStoreLogoPath").val(obj.StoreLogoURL);
            $("#divStoreLogo").html('<img src="' + aspxRootPath + obj.StoreLogoURL + '" class="uploadImage" height="90px" width="100px"/>');
            $("#txtStoreName").val(obj.StoreName);
            $("#ddlCurrency").val(obj.MainCurrency);
            $("#chkRealTimeNotifications").prop("checked", $.parseJSON(obj.AllowRealTimeNotifications.toLowerCase()));
            $("#txtWeightUnit").val(obj.WeightUnit);
            $("#txtDimensionUnit").val(obj.DimensionUnit);
            $("#txtCartAbandonTime").val(obj.CartAbandonedTime);
            $("#txtTimeToDeleteAbandCart").val(obj.TimeToDeleteAbandonedCart);
            $("#txtLowStockQuantity").val(obj.LowStockQuantity);
            $("#txtOutOfStockQuantity").val(obj.OutOfStockQuantity);
            $("#txtShoppingOptionRange").val(obj.ShoppingOptionRange);
            $("#chkAllowAnonymousCheckout").prop("checked", $.parseJSON(obj.AllowAnonymousCheckOut.toLowerCase()));
            $("#chkAllowMultipleShippingAddress").prop("checked", $.parseJSON(obj.AllowMultipleShippingAddress.toLowerCase()));
            $("#chkAllowOutStockPurchase").prop("checked", $.parseJSON(obj.AllowOutStockPurchase.toLowerCase()));
            $("#chkAskCustomerToSubscribe").prop("checked", $.parseJSON(obj.AskCustomerToSubscribe.toLowerCase()))
            $("#chkEstimateShippingCostInCartPage").prop("checked", $.parseJSON(obj.EstimateShippingCostInCartPage.toLowerCase()))


            $("#txtSendEmailsFrom").val(obj.SendEcommerceEmailsFrom);
            $("#chkSendOrderNotification").prop("checked", $.parseJSON(obj.SendOrderNotification.toLowerCase()));

            $("#chkShowAddToCartButton").prop("checked", $.parseJSON(obj.ShowAddToCartButton.toLowerCase()));
            $("#txtAddToCartButtonText").val(obj.AddToCartButtonText);
            var ArrFinalViewAs = [];
            if (obj.ViewAsOptions != null) {
                var arryViewAsOpt = (obj.ViewAsOptions).split(',');
                $.each(arryViewAsOpt, function (index, value) {
                    var OptId = value.split('#');
                    ArrFinalViewAs.push(OptId[0]);
                });
                for (var i = 0; i < ArrFinalViewAs.length; i++) {
                    $("#ddlViewAsOptions [value=" + $.trim(ArrFinalViewAs[i]) + "]").prop('selected', "selected");
                }
            }
            $("#ddlViewAsOptionsDefault").empty().append($("#ddlViewAsOptions option:selected").clone());
            $("#ddlViewAsOptionsDefault").val(obj.ViewAsOptionsDefault)
            var ArrFinalSortBy = [];
            if (obj.SortByOptions != null) {
                var ArrSortByOptions = (obj.SortByOptions).split(',');
                $.each(ArrSortByOptions, function (index, value) {
                    var SortById = value.split('#');
                    ArrFinalSortBy.push(SortById[0]);
                });
                for (var i = 0; i < ArrFinalSortBy.length; i++) {
                    $("#ddlSortByOptions [value=" + $.trim(ArrFinalSortBy[i]) + "]").prop('selected', "selected");
                }
            }
            $("#ddlSortByOptionsDefault").empty().append($("#ddlSortByOptions option:selected").clone());
            $("#ddlSortByOptionsDefault").val(obj.SortByOptionsDefault)
            $('#ddlViewAsOptions').click(function () {
                $('#ddlViewAsOptionsDefault').empty();
                $('#ddlViewAsOptionsDefault').append($('#ddlViewAsOptions option:selected').clone());
            });
            $('#ddlSortByOptions').click(function () {
                $('#ddlSortByOptionsDefault').empty();
                $('#ddlSortByOptionsDefault').append($('#ddlSortByOptions option:selected').clone());
            });

            $("#txtMaximumImageSize").val(obj.MaximumImageSize);
            $("#txtMaxDownloadFileSize").val(obj.MaxDownloadFileSize);
            $("#chkResizeProportionally").prop("checked", $.parseJSON(obj.ResizeImagesProportionally.toLowerCase()))
            $("#txtItemLargeThumbnailImageHeight").val(obj.ItemLargeThumbnailImageHeight);
            $("#txtItemLargeThumbnailImageWidth").val(obj.ItemLargeThumbnailImageWidth);
            $("#txtItemMediumThumbnailImageHeight").val(obj.ItemMediumThumbnailImageHeight);
            $("#txtItemMediumThumbnailImageWidth").val(obj.ItemMediumThumbnailImageWidth);
            $("#txtItemSmallThumbnailImageHeight").val(obj.ItemSmallThumbnailImageHeight);
            $("#txtItemSmallThumbnailImageWidth").val(obj.ItemSmallThumbnailImageWidth);
            $("#txtCategoryLargeThumbnailImageHeight").val(obj.CategoryLargeThumbnailImageHeight);
            $("#txtCategoryLargeThumbnailImageWidth").val(obj.CategoryLargeThumbnailImageWidth);
            $("#txtCategoryMediumThumbnailImageHeight").val(obj.CategoryMediumThumbnailImageHeight);
            $("#txtCategoryMediumThumbnailImageWidth").val(obj.CategoryMediumThumbnailImageWidth);
            $("#txtCategorySmallThumbnailImageHeight").val(obj.CategorySmallThumbnailImageHeight);
            $("#txtCategorySmallThumbnailImageWidth").val(obj.CategorySmallThumbnailImageWidth);
            $("#chkShowItemImagesInCart").prop("checked", $.parseJSON(obj.ShowItemImagesInCart.toLowerCase()));
            $("#chkShowItemImagesInWishList").prop("checked", $.parseJSON(obj.ShowItemImagesInWishList.toLowerCase()));
            $("#txtItemImageMaxWidth").val(obj.ItemImageMaxWidth);
            $("#txtItemImageMaxHeight").val(obj.ItemImageMaxHeight);
            $("#txtCategoryBannerImageWidth").val(obj.CategoryBannerImageWidth);
            $("#txtCategoryBannerImageHeight").val(obj.CategoryBannerImageHeight);
            $('#txtWaterMark').val(obj.WaterMarkText);
            $('input[name=watermarkposition][value=' + obj.WaterMarkTextPosition.toUpperCase() + ']').prop('checked', 'checked');
            $('#txtWaterMarkRotationAngle').val(obj.WaterMarkTextRotation);
            $('input[name=showWaterMarkImage]').prop("checked", $.parseJSON(obj.ShowWaterMarkImage.toLowerCase()));
            $('input[name=watermarkImageposition][value=' + obj.WaterMarkImagePosition.toUpperCase() + ']').prop('checked', 'checked');
            $('#txtWaterMarkImageRotation').val(obj.WaterMarkImageRotation);
            $("#chkAllowMultipleAddress").prop("checked", $.parseJSON(obj.AllowUsersToCreateMultipleAddress.toLowerCase()));
            $("#chkAllowShippingEstimate").prop("checked", $.parseJSON(obj.AllowShippingRateEstimate.toLowerCase()));
            $("#chkAllowCouponDiscount").prop("checked", $.parseJSON(obj.AllowCouponDiscount.toLowerCase()));
            $("#txtMinimumCartSubTotalAmount").val(obj.MinimumCartSubTotalAmount);
            $("#txtAdditionalCVR").val(obj.AdditionalCVR);
            $("#txtMinCartQuantity").val(obj.MinCartQuantity);
            $("#txtMaxCartQuantity").val(obj.MaxCartQuantity);
            $("#chkEmailAFriend").prop("checked", $.parseJSON(obj.EnableEmailAFriend.toLowerCase()));
            $("#chkShowMiniShoppingCart").prop("checked", $.parseJSON(obj.ShowMiniShoppingCart.toLowerCase()));
            $("#chkMultipleReviewsPerUser").prop("checked", $.parseJSON(obj.AllowMultipleReviewsPerUser.toLowerCase()));
            $("#chkMultipleReviewsPerIP").prop("checked", $.parseJSON(obj.AllowMultipleReviewsPerIP.toLowerCase()));
            $("#chkAllowAnonymousUserToWriteReviews").prop("checked", $.parseJSON(obj.AllowAnonymousUserToWriteItemRatingAndReviews.toLowerCase()));
            $("#txtNoOfDisplayItems").val(obj.NoOfDisplayItems);
            $("#ddlItemDisplayMode").val(obj.ItemDisplayMode);
            $("#chkModuleCollapsible").prop("checked", $.parseJSON(obj.ModuleCollapsible.toLowerCase()));

            //Disable the heights setting of item images if resizing proportionally is selected
            if ($.parseJSON(obj.ResizeImagesProportionally.toLowerCase())) {
                $("#txtItemLargeThumbnailImageHeight").prop('disabled', true);
                $("#txtItemMediumThumbnailImageHeight").prop('disabled', true);
                $("#txtItemSmallThumbnailImageHeight").prop('disabled', true);
            }
            if ($.trim(obj.AllowedShippingCountry) == "ALL") {
                $('#cbSelectAllShippingCountry').prop('checked', 'checked');
                $("#lbCountryShipping").find('option').prop('selected', 'selected');

            } else {
                var valz = obj.AllowedShippingCountry.split(',');
                for (var v = 0; v < valz.length; v++) {
                    if (valz[v] != '0')
                        $('#lbCountryShipping').find('option[value=' + valz[v] + ']').prop('selected', 'selected');
                }
            }
            if ($.trim(obj.AllowedBillingCountry) == "ALL") {
                $('#cbSelectAllBillingCountry').prop('checked', 'checked');
                $("#lbCountryBilling").find('option').prop('selected', 'selected');
            } else {
                var valx = obj.AllowedBillingCountry.split(',');
                for (var s = 0; s < valx.length; s++) {
                    if (valx[s] != '0')
                        $('#lbCountryBilling').find('option[value=' + valx[s] + ']').prop('selected', 'selected');
                }

            }
            $("#" + ddlRssFeedURL).val(obj.RssFeedURL);
            $('#categoryChkBox').prop("checked", $.parseJSON(obj.NewCategoryRss.toLowerCase()));
            $('#txtCategoryRssCount').val(obj.NewCategoryRssCount);
            $('#newOrderChkBox').prop("checked", $.parseJSON(obj.NewOrderRss.toLowerCase()));
            $('#txtNewOrderRssCount').val(obj.NewOrderRssCount);
            $('#newCustomerChkBox').prop("checked", $.parseJSON(obj.NewCustomerRss.toLowerCase()));
            $('#txtNewCustomerRssCount').val(obj.NewCustomerRssCount);
            $('#newItemTagChkBox').prop("checked", $.parseJSON(obj.NewItemTagRss.toLowerCase()));
            $('#txtNewItemTagRssCount').val(obj.NewItemTagRssCount);
            $('#newItemReviewChkBox').prop("checked", $.parseJSON(obj.NewItemReviewRss.toLowerCase()));
            $('#txtNewItemReviewRssCount').val(obj.NewItemReviewRssCount);
            $('#lowStockChkBox').prop("checked", $.parseJSON(obj.LowStockItemRss.toLowerCase()));
            $('#txtLowStockRssCount').val(obj.LowStockItemRssCount);

            StoreSettings.ListNameSelected();
        },
        BindChangeFxToCountry: function () {
            $("#lbCountryShipping,#lbCountryBilling").bind("change", function () {
                StoreSettings.ListNameSelected();
                if ($("#lbCountryShipping").val() != null) {
                    if ($("#lbCountryShipping").val().length == $("#lbCountryShipping").find('option').length) {
                        $("#cbSelectAllShippingCountry").prop('checked', 'checked');
                    } else {
                        $("#cbSelectAllShippingCountry").removeAttr('checked');
                    }
                }
                if ($("#lbCountryBilling").val() != null) {
                    if ($("#lbCountryBilling").val().length == $("#lbCountryBilling").find('option').length) {
                        $("#cbSelectAllBillingCountry").prop('checked', 'checked');
                    } else {
                        $("#cbSelectAllBillingCountry").removeAttr('checked');
                    }
                }

            });

        },

        UpdateStoreSettings: function () {
            if (aspxRootPath != "/") {
                var defaultImageProductURL = $("#defaultProductImage>img").attr("src").replace(aspxRootPath, "");
            }
            else {
                var defaultImageProductURL = $("#defaultProductImage>img").attr("src").replace(aspxRootPath, "");
            }
            var prevFilePath = $("#hdnPrevFilePath").val();
            var myAccountURL = $("#" + ddlMyAccountURL).val();
            var shoppingCartURL = $("#" + ddlShoppingCartURL).val();
            var detailsPageURL = $("#" + ddlDetailsPageURL).val();
            var itemDetailURL = $("#" + ddlItemDetailURL).val();
            var categoryDetailURL = $("#" + ddlCategoryDetailURL).val();
            var singleCheckOutURL = $("#" + ddlSingleCheckOutURL).val();
            var multiCheckOutURL = $("#" + ddlMultiCheckOutURL).val();
            var storeLocatorURL = $("#" + ddlStoreLocatorURL).val();
            var trackPackageUrl = $("#" + ddlTrackPackageUrl).val();
            var shipDetailPageURL = $("#" + ddlShippingDetailPageURL).val();
            var itemMgntPageURL = $("#" + ddlItemMgntPageURL).val();
            var catMgntPageURL = $("#" + ddlCatMgntPageURL).val();

            var currency = $("#ddlCurrency option:selected").val();
            var realTimeNotifications = $("#chkRealTimeNotifications").prop("checked");
            var weightUnit = $("#txtWeightUnit").val();
            var dimensionUnit = $("#txtDimensionUnit").val();
            var storeName = $("#txtStoreName").val();
            if (aspxRootPath != "/") {
                var storeLogoURL = $("#divStoreLogo>img").attr("src").replace(aspxRootPath, "");
            }
            else {
                var storeLogoURL = $("#divStoreLogo>img").attr("src").replace(aspxRootPath, "");
            }
            var prevStoreLogoPath = $("#hdnPrevStoreLogoPath").val();
            var cartAbandonedTime = $("#txtCartAbandonTime").val();
            var timeToDeleteAbandonedCart = $("#txtTimeToDeleteAbandCart").val();
            var lowStockQuantity = $("#txtLowStockQuantity").val();
            var outOfStockQuantity = $("#txtOutOfStockQuantity").val();
            var shoppingOptionRange = $("#txtShoppingOptionRange").val();
            var allowAnonymousCheckout = $("#chkAllowAnonymousCheckout").prop("checked");
            var allowMultipleShippingAddress = $("#chkAllowMultipleShippingAddress").prop("checked");
            var allowOutStockPurchase = $("#chkAllowOutStockPurchase").prop("checked");
            var AskCustomerToSubscribe = $("#chkAskCustomerToSubscribe").prop("checked");
            var EstimateShippingCostInCartPage = $("#chkEstimateShippingCostInCartPage").prop("checked");

            var emailFrom = $("#txtSendEmailsFrom").val();
            var SendOrderNotification = $("#chkSendOrderNotification").prop("checked");

            var ShowAddToCartButton = $("#chkShowAddToCartButton").prop("checked");
            var AddToCartButtonText = $("#txtAddToCartButtonText").val();
            var ViewAsOptions = "";
            $("#ddlViewAsOptions option:selected").each(function () {
                ViewAsOptions += $(this).val() + "#" + $(this).text() + ",";
            });
            var ViewAsOptionsDefault = $("#ddlViewAsOptionsDefault").val();
            var SortByOptions = "";
            $("#ddlSortByOptions option:selected").each(function () {
                SortByOptions += $(this).val() + "#" + $(this).text() + ",";
            });
            var SortByOptionsDefault = $("#ddlSortByOptionsDefault").val();

            var maximumImageSize = $("#txtMaximumImageSize").val();
            var maximumDownloadSize = $("#txtMaxDownloadFileSize").val();
            var ResizeImageProportionally = $("#chkResizeProportionally").prop("checked");
            var ItemLargeThumbnailImageHeight = $("#txtItemLargeThumbnailImageHeight").val();
            var ItemLargeThumbnailImageWidth = $("#txtItemLargeThumbnailImageWidth").val();
            var ItemMediumThumbnailImageHeight = $("#txtItemMediumThumbnailImageHeight").val();
            var ItemMediumThumbnailImageWidth = $("#txtItemMediumThumbnailImageWidth").val();
            var ItemSmallThumbnailImageHeight = $("#txtItemSmallThumbnailImageHeight").val();
            var ItemSmallThumbnailImageWidth = $("#txtItemSmallThumbnailImageWidth").val();
            var CategoryLargeThumbnailImageHeight = $("#txtCategoryLargeThumbnailImageHeight").val();
            var CategoryLargeThumbnailImageWidth = $("#txtCategoryLargeThumbnailImageWidth").val();
            var CategoryMediumThumbnailImageHeight = $("#txtCategoryMediumThumbnailImageHeight").val();
            var CategoryMediumThumbnailImageWidth = $("#txtCategoryMediumThumbnailImageWidth").val();
            var CategorySmallThumbnailImageHeight = $("#txtCategorySmallThumbnailImageHeight").val();
            var CategorySmallThumbnailImageWidth = $("#txtCategorySmallThumbnailImageWidth").val();
            var showItemImagesInCart = $("#chkShowItemImagesInCart").prop("checked");
            var showItemImagesInWishList = $("#chkShowItemImagesInWishList").prop("checked");
            var ItemImageMaxWidth = $("#txtItemImageMaxWidth").val();
            var ItemImageMaxHeight = $("#txtItemImageMaxHeight").val();
            var CategoryBannerImageWidth = $("#txtCategoryBannerImageWidth").val();
            var CategoryBannerImageHeight = $("#txtCategoryBannerImageHeight").val();
            var allowMultipleAddress = $("#chkAllowMultipleAddress").prop("checked");
            var allowShippingEstimate = $("#chkAllowShippingEstimate").prop("checked");
            var allowCouponDiscount = $("#chkAllowCouponDiscount").prop("checked");
            var minimumOrderAmount = $("#txtMinimumCartSubTotalAmount").val();
            var additionalCCVR = 0;
            if ($("#txtAdditionalCVR").val() != "") {
                additionalCCVR = $("#txtAdditionalCVR").val();
            }
            var MinCartQuantity = $("#txtMinCartQuantity").val();
            var MaxCartQuantity = $("#txtMaxCartQuantity").val();

            var enableEmailAFriend = $("#chkEmailAFriend").prop("checked");
            var showMiniShoppingCart = $("#chkShowMiniShoppingCart").prop("checked");
            var allowMultipleReviewsPerUser = $("#chkMultipleReviewsPerUser").prop("checked");
            var allowMultipleReviewsPerIP = $("#chkMultipleReviewsPerIP").prop("checked");
            var allowAnonymousUserToWriteReviews = $("#chkAllowAnonymousUserToWriteReviews").prop("checked");
            var noOfDisplayItems = $("#txtNoOfDisplayItems").val();
            var itemDisplayMode = $("#ddlItemDisplayMode").val();
            var moduleCollapsible = $("#chkModuleCollapsible").prop("checked");
            if ($("#lbCountryShipping").val().length != null && $("#lbCountryShipping").val().clean('0').length > 0)
                var shippingCountry = $('#cbSelectAllShippingCountry').is(':checked') == true ? 'ALL' : $("#lbCountryShipping").val().clean('0').join();
            else {
                csscody.error('<h2>' + getLocale(AspxStoreSettingsManagement, "Error Message") + "</h2><p>" + getLocale(AspxStoreSettingsManagement, "Please select Country for Shipping.") + '</p>');
                return false;
            }
            if ($("#lbCountryBilling").val().length != null && $("#lbCountryBilling").val().clean('0').length > 0)
                var billingCountry = $('#cbSelectAllBillingCountry').is(':checked') == true ? 'ALL' : $("#lbCountryBilling").val().clean('0').join();
            else {
                csscody.error('<h2>' + getLocale(AspxStoreSettingsManagement, "Error Message") + "</h2><p>" + getLocale(AspxStoreSettingsManagement, "Please select Country for Billing.") + '</p>');
                return false;
            }
            var rssFeedURL = $("#" + ddlRssFeedURL).val();
            var newCategoryRss = $('#categoryChkBox').prop('checked');
            var newCategoryRssCount = $.trim($('#txtCategoryRssCount').val());
            var newOrderRss = $('#newOrderChkBox').prop('checked');
            var newOrderRssCount = $.trim($('#txtNewOrderRssCount').val());
            var newCustomerRss = $('#newCustomerChkBox').prop('checked');
            var newCustomerRssCount = $.trim($('#txtNewCustomerRssCount').val());
            var newItemTagRss = $('#newItemTagChkBox').prop('checked');
            var newItemTagRssCount = $.trim($('#txtNewItemTagRssCount').val());
            var newItemReviewRss = $('#newItemReviewChkBox').prop('checked');
            var newItemReviewRssCount = $.trim($('#txtNewItemReviewRssCount').val());
            var lowStockItemRss = $('#lowStockChkBox').prop('checked');
            var lowStockItemRssCount = $.trim($('#txtLowStockRssCount').val());

            var wMText = $.trim($('#txtWaterMark').val());
            var wmTextPos = $.trim($('input[name=watermarkposition]:checked').val());
            var wmTextRotation = $.trim($('#txtWaterMarkRotationAngle').val());
            var wmImagePos = $.trim($('input[name=watermarkImageposition]:checked').val());
            var wmImageRotation = $.trim($('#txtWaterMarkImageRotation').val());
            var showWaterMarkImage = $("input[name=showWaterMarkImage]").is(":checked");
            var settingValues = '';
            settingValues += myAccountURL + '*' + shoppingCartURL + '*' + detailsPageURL + '*' + itemDetailURL + '*' + categoryDetailURL + '*' + singleCheckOutURL + '*' + multiCheckOutURL + '*' + storeLocatorURL
                    + '*' + trackPackageUrl + '*' + shipDetailPageURL + '*' + itemMgntPageURL + '*' + catMgntPageURL + '*';
            settingValues += currency + '*' + realTimeNotifications + '*' + weightUnit + '*' + dimensionUnit + '*' + storeName + '*' + cartAbandonedTime + '*' + timeToDeleteAbandonedCart + '*' + lowStockQuantity + '*' + outOfStockQuantity
                    + '*' + shoppingOptionRange + '*' + allowAnonymousCheckout + '*' + allowMultipleShippingAddress + '*' + allowOutStockPurchase + '*' + AskCustomerToSubscribe + '*' + EstimateShippingCostInCartPage + '*';
            settingValues += emailFrom + '*' + SendOrderNotification + '*';
            settingValues += ShowAddToCartButton + '*' + AddToCartButtonText + '*' + ViewAsOptions + '*' + ViewAsOptionsDefault + '*' + SortByOptions + '*' + SortByOptionsDefault + '*';
            settingValues += maximumImageSize + '*' + maximumDownloadSize + '*' + ResizeImageProportionally + '*' + ItemLargeThumbnailImageHeight + '*' + ItemLargeThumbnailImageWidth + '*' + ItemMediumThumbnailImageHeight
                    + '*' + ItemMediumThumbnailImageWidth + '*' + ItemSmallThumbnailImageHeight + '*' + ItemSmallThumbnailImageWidth + '*' + CategoryLargeThumbnailImageHeight + '*' + CategoryLargeThumbnailImageWidth
                    + '*' + CategoryMediumThumbnailImageHeight + '*' + CategoryMediumThumbnailImageWidth + '*' + CategorySmallThumbnailImageHeight + '*' + CategorySmallThumbnailImageWidth + '*' + showItemImagesInCart
                    + '*' + showItemImagesInWishList + '*' + ItemImageMaxWidth + '*' + ItemImageMaxHeight + '*' + CategoryBannerImageWidth + '*' + CategoryBannerImageHeight + '*';
            settingValues += allowMultipleAddress + '*' + allowShippingEstimate + '*' + allowCouponDiscount + '*' + minimumOrderAmount + '*' + additionalCCVR + '*' + MinCartQuantity + '*' + MaxCartQuantity + '*';
            settingValues += enableEmailAFriend + '*' + showMiniShoppingCart + '*' + allowMultipleReviewsPerUser + '*' + allowMultipleReviewsPerIP
                         + '*' + allowAnonymousUserToWriteReviews + '*' + noOfDisplayItems + '*' + itemDisplayMode + '*' + moduleCollapsible + '*' + shippingCountry + '*' + billingCountry;
            settingValues += '*' + rssFeedURL + '*' + newCategoryRss + '*' + newCategoryRssCount + '*' + newOrderRss + '*' + newOrderRssCount + '*' + newCustomerRss + '*' + newCustomerRssCount + '*' +
                                newItemTagRss + '*' + newItemTagRssCount + '*' + newItemReviewRss + '*' + newItemReviewRssCount + '*' + lowStockItemRss + '*' +
                                    lowStockItemRssCount + '*' + wMText + '*' + wmTextPos + '*' + wmTextRotation + '*' + wmImagePos + '*' + wmImageRotation + '*' + showWaterMarkImage;
            var settingKeys = '';
            settingKeys += 'MyAccountURL' + '*' + 'ShoppingCartURL' + '*' + 'DetailPageURL' + '*' + 'ItemDetailURL' + '*' + 'CategoryDetailURL' + '*' + 'SingleCheckOutURL'
                              + '*' + 'MultiCheckOutURL' + '*' + 'StoreLocatorURL' + '*' + 'TrackPackageUrl' + '*' + 'ShipDetailPageURL' + '*' + 'ItemMgntPageURL' + '*' + 'CategoryMgntPageURL' + '*';
            settingKeys += 'MainCurrency' + '*' + 'AllowRealTimeNotifications' + '*' + 'WeightUnit' + '*' + 'DimensionUnit' + '*' + 'StoreName' + '*' + 'CartAbandonedTime' + '*' + 'TimeToDeleteAbandonedCart'
                              + '*' + 'LowStockQuantity' + '*' + 'OutOfStockQuantity' + '*' + 'ShoppingOptionRange' + '*' + 'AllowAnonymousCheckOut' + '*' + 'AllowMultipleShippingAddress' + '*' + 'AllowOutStockPurchase'
                               + '*' + 'AskCustomerToSubscribe' + '*' + 'EstimateShippingCostInCartPage' + '*';
            settingKeys += 'SendEcommerceEmailsFrom' + '*' + 'SendOrderNotification' + '*';
            settingKeys += 'Show.AddToCartButton' + '*' + 'AddToCartButtonText' + '*' + 'ViewAsOptions' + '*' + 'ViewAsOptionsDefault' + '*' + 'SortByOptions' + '*' + 'SortByOptionsDefault' + '*';
            settingKeys += 'MaximumImageSize' + '*' + 'MaxDownloadFileSize' + '*' + 'ResizeImagesProportionally' + '*' + 'ItemLargeThumbnailImageHeight' + '*' + 'ItemLargeThumbnailImageWidth' + '*' + 'ItemMediumThumbnailImageHeight'
                              + '*' + 'ItemMediumThumbnailImageWidth' + '*' + 'ItemSmallThumbnailImageHeight' + '*' + 'ItemSmallThumbnailImageWidth' + '*' + 'CategoryLargeThumbnailImageHeight'
                              + '*' + 'CategoryLargeThumbnailImageWidth' + '*' + 'CategoryMediumThumbnailImageHeight' + '*' + 'CategoryMediumThumbnailImageWidth' + '*' + 'CategorySmallThumbnailImageHeight'
                              + '*' + 'CategorySmallThumbnailImageWidth' + '*' + 'ShowItemImagesInCart' + '*' +
                               'ShowItemImagesInWishList' + '*' + 'ItemImageMaxWidth' + '*' + 'ItemImageMaxHeight' + '*' + 'CategoryBannerImageWidth' + '*' + 'CategoryBannerImageHeight' + '*';
            settingKeys += 'AllowUsersToCreateMultipleAddress' + '*' + 'AllowShippingRateEstimate' + '*' + 'AllowCouponDiscount' + '*' + 'MinimumCartSubTotalAmount' + '*' + 'AdditionalCVR' + '*' + 'MinCartQuantity' + '*' + 'MaxCartQuantity' + '*';
            settingKeys += 'Enable.EmailAFriend' + '*' + 'Show.MiniShoppingCart' + '*' + 'AllowMultipleReviewsPerUser' + '*' + 'AllowMultipleReviewsPerIP'
                     + '*' + 'AllowAnonymousUserToWriteItemRatingAndReviews' + '*' + 'NoOfDisplayItems' + '*' + 'ItemDisplayMode'
                  + '*' + 'ModuleCollapsible' + '*' + 'AllowedShippingCountry' + '*' + 'AllowedBillingCountry';
            settingKeys += '*' + 'RssFeedURL' + '*' + 'NewCategoryRss' + '*' + 'NewCategoryRssCount' + '*' + 'NewOrderRss' + '*' + 'NewOrderRssCount' + '*' + 'NewCustomerRss' + '*' + 'NewCustomerRssCount' + '*' +
                                'NewItemTagRss' + '*' + 'NewItemTagRssCount' + '*' + 'NewItemReviewRss' + '*' + 'NewItemReviewRssCount' + '*' + 'LowStockItemRss' + '*' +
                                    'LowStockItemRssCount' + '*' + "WaterMarkText" + '*' + "WaterMarkTextPosition" + '*' + "WaterMarkTextRotation" + '*' + "WaterMarkImagePosition" + '*' +
                                                            "WaterMarkImageRotation" + '*' + 'ShowWaterMarkImage';
            this.config.method = "AspxCoreHandler.ashx/UpdateStoreSettings";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ settingKeys: settingKeys, settingValues: settingValues, prevFilePath: prevFilePath, newFilePath: defaultImageProductURL, prevStoreLogoPath: prevStoreLogoPath, newStoreLogoPath: storeLogoURL, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },

        ImageUploader: function (obj) {
            maxFileSize = maxFilesize;
            var upload = new AjaxUpload($('#' + obj), {
                action: aspxStoreSetModulePath + "MultipleFileUploadHandler.aspx",
                name: 'myfile[]',
                multiple: false,
                data: {},
                autoSubmit: true,
                responseType: 'json',
                onChange: function (file, ext) {
                },
                onSubmit: function (file, ext) {
                    pcount = 0;
                    var percentage = $('.progress').find('.percentage');
                    var progressBar = $('.progress').find('.progressBar');
                    $('.progress').show();
                    StoreSettings.dummyProgress(progressBar, percentage);
                    if (ext != "exe") {
                        if (ext && /^(jpg|jpeg|jpe|gif|bmp|png|ico)$/i.test(ext)) {
                            this.setData({
                                'MaxFileSize': maxFileSize
                            });
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxStoreSettingsManagement, "Alert Message") + "</h2><p>" + getLocale(AspxStoreSettingsManagement, "Not a valid image!") + '</p>');
                            return false;
                        }
                    }
                    else {
                        csscody.alert('<h2>' + getLocale(AspxStoreSettingsManagement, "Alert Message") + "</h2><p>" + getLocale(AspxStoreSettingsManagement, "Not a valid image!") + '</p>');
                        return false;
                    }
                },
                onComplete: function (file, response) {
                    var res = eval(response);
                    if (res.Message != null && res.Status > 0) {
                        StoreSettings.AddNewImages(res, obj);
                        return false;
                    }
                    else {
                        csscody.error('<h2>' + getLocale(AspxStoreSettingsManagement, "Error Message") + "</h2><p>" + getLocale(AspxStoreSettingsManagement, "Sorry! image can not be uploaded.") + '</p>');
                        return false;
                    }
                }
            });
        },
        dummyProgress: function (progressBar, percentage) {
            if (percentageInterval[pcount]) {
                progress = percentageInterval[pcount] + Math.floor(Math.random() * 10 + 1);
                percentage.text(progress.toString() + '%');
                progressBar.progressbar({
                    value: progress
                });
                var percent = percentage.text();
                percent = percent.replace('%', '');
                percent = percent.replace('%', '');
                if (percent == 100100 || percent > 100100) {
                    percentage.text('100%');
                    $('.progress').hide();
                }
            }

            if (timeInterval[pcount]) {
                progressTime = setTimeout(function () {
                    StoreSettings.dummyProgress(progressBar, percentage)
                }, timeInterval[pcount] * 10);
            }
            pcount++;
        },

        AddNewImages: function (response, obj) {
            if (obj == "fupDefaultImageURL") {
                $("#defaultProductImage").html('<img src="' + aspxRootPath + response.Message + '" class="uploadImage" height="90px" width="100px"/>');
            } else {
                $("#divStoreLogo").html('<img src="' + aspxRootPath + response.Message + '" class="uploadImage" height="90px" width="100px"/>');
            }
        },
        SelectAllCountry: function (type) {
            if (type == 'cbSelectAllShippingCountry') {
                if ($("#cbSelectAllShippingCountry").is(":checked") == true || $("#cbSelectAllShippingCountry").is(":checked") == "true") {
                    $("#lbCountryShipping").find('option').prop('selected', 'selected');
                } else {
                    $("#lbCountryShipping").find('option').removeAttr('selected');
                    $("#lbCountryShipping").find('option:first').prop('selected', 'selected');
                }
            }
            if (type == 'cbSelectAllBillingCountry') {
                if ($("#cbSelectAllBillingCountry").is(":checked") == true || $("#cbSelectAllBillingCountry").is(":checked") == "true") {
                    $("#lbCountryBilling").find('option').prop('selected', 'selected');
                } else {
                    $("#lbCountryBilling").find('option').removeAttr('selected');
                    $("#lbCountryBilling").find('option:first').prop('selected', 'selected');
                }
            }
            StoreSettings.ListNameSelected();
        },
        ListNameSelected: function () {
            var s = "", b = "";
            if ($("#lbCountryShipping").val() != null) {
                var sl = $("#lbCountryShipping").val().length;

                $("#lbCountryShipping").find('option:selected').each(function (index) {
                    if (sl - 1 > index) {
                        if (this.value != '0')
                            s += $(this).text() + ", ";
                    }
                    else
                        s += $(this).text();
                });
            }
            if ($("#lbCountryBilling").val() != null) {
                var bl = $("#lbCountryBilling").val().length;
                $("#lbCountryBilling").find('option:selected').each(function (index) {
                    if (bl - 1 > index) {
                        if (this.value != '0')
                            b += $(this).text() + ", ";
                    }
                    else
                        b += $(this).text();
                });
            }
            if (s != "") {
                $("#lblShippingCountry").html(getLocale(AspxStoreSettingsManagement, "Selected Country :") + s);
            } else {
                $("#lblShippingCountry").html('');
            }
            if (b != "") {
                $("#lblBillingCountry").html(getLocale(AspxStoreSettingsManagement, "Selected Country :") + b);
            } else {
                $("#lblBillingCountry").html('');
            }


        },

        ajaxSuccess: function (data) {
            switch (StoreSettings.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    var value = data.d;
                    if (value != null) {
                        StoreSettings.BindAllValue(value);
                    }
                    StoreSettings.InitializeTabs();
                    break;
                case 2:
                    var countryElements = '';
                    var selectOne = '<option value="0">' + getLocale(AspxStoreSettingsManagement, 'Select One') + '</option>';
                    $.each(data.d, function (index, value) {
                        countryElements += '<option value=' + value.Value + '>' + value.Text + '</option>';
                    });
                    $("#lbCountryShipping").html(selectOne + countryElements);
                    $("#lbCountryBilling").html(selectOne + countryElements);
                    StoreSettings.BindChangeFxToCountry();
                    break;
                case 3:
                    var currencyElements = '';
                    $.each(data.d, function (index, value) {
                        currencyElements += '<option value=' + value.CurrencyCode + '>' + value.CurrencyName + '</option>';
                    });
                    $("#ddlCurrency").html(currencyElements);
                    break;
                case 4:
                    StoreSettings.DeleteAllResizedImages();
                    StoreSettings.GetAllStoreSettings();
                    csscody.info('<h2>' + getLocale(AspxStoreSettingsManagement, "Information Message") + "</h2><p>" + getLocale(AspxStoreSettingsManagement, "Store Settings  has been updated successfully.") + '</p>');
                    break;
                case 5:
                    if (data.d.length > 0) {
                        $.each(data.d, function (index, item) {
                            var displayOptions = "<option value=" + item.DisplayItemID + ">" + item.OptionType + "</option>";
                            $("#ddlViewAsOptions").append(displayOptions);
                        });
                    }
                    break;
                case 6:
                    if (data.d.length > 0) {
                        $.each(data.d, function (index, item) {
                            var displayOptions = "<option value=" + item.SortOptionTypeID + ">" + item.OptionType + "</option>"
                            $("#ddlSortByOptions").append(displayOptions);
                        });
                    }
                    break;
                case 7:
                    break;
            }
        }
    }
    StoreSettings.init();
});