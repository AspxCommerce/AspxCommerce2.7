var ItemImageGalleryApi = "";
Variable = function (height, width, thumbWidth, thumbHeight) {
    this.height = height;
    this.width = width;
    this.thumbHeight = thumbHeight;
    this.thumbWidth = thumbWidth;
};
var newObject = new Variable(255, 320, 87, 75);
(function ($) {
    $.ItemImageGalleryView = function (p) {
        p = $.extend
    ({
        ItemImageGalleryModulePath: '',
        referImagePath: '',
        ImageCount: 0
    }, p);
        var aspxCommonObj = function () {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName: AspxCommerce.utils.GetCultureName(),
                CustomerID: AspxCommerce.utils.GetCustomerID(),
                SessionCode: AspxCommerce.utils.GetSessionCode()
            }
        };
        var Imagelist = '';
        ItemImageGallery = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: "json",
                baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCoreHandler.ashx/",
                url: "",
                method: "",
                ajaxCallMode: ""
            },
            vars:
                {
                  aspxCommonInfo : {
                        StoreID: AspxCommerce.utils.GetStoreID(),
                        PortalID: AspxCommerce.utils.GetPortalID(),
                        UserName: AspxCommerce.utils.GetUserName(),
                        CultureName: AspxCommerce.utils.GetCultureName(),
                        CustomerID: AspxCommerce.utils.GetCustomerID(),
                        SessionCode: AspxCommerce.utils.GetSessionCode()
                    }

                },

            ajaxCall: function (config) {
                $.ajax({
                    type: ItemImageGallery.config.type,
                    contentType: ItemImageGallery.config.contentType,
                    cache: ItemImageGallery.config.cache,
                    async: ItemImageGallery.config.async,
                    data: ItemImageGallery.config.data,
                    dataType: ItemImageGallery.config.dataType,
                    url: ItemImageGallery.config.url,
                    success: ItemImageGallery.config.ajaxCallMode,
                    error: ItemImageGallery.ajaxFailure
                });
            },

            init: function () {
                $('.cssClassProductBigPicture').attr('imagepath', p.referImagePath);
                var windowsWidth = $(window).width();               
                if (windowsWidth > 800) {
                    $('.cloud-zoom, .cloud-zoom-gallery').CloudZoom({
                        zoomWidth: 'auto',
                        zoomHeight: 'auto',
                        position: 'inside',
                        tint: false,
                        tintOpacity: 0.5,
                        lensOpacity: 0.5,
                        softFocus: false,
                        smoothMove: 3
                    });
                }               
                $(".cssClassProductBigPicture").show();
                $('#divBindThumbs').jcarousel({
                    vertical: false,
                    scroll: 1,
                    itemFallbackDimension: 300
                });
            },

            GetImageLists: function (itemId, itemTypeId, sku, combinationId, aspxCommonObj) {               
                if (itemTypeId == 3) {
                    ItemImageGallery.GetGiftCardThemes(itemId, aspxCommonObj);
                } else {
                    this.config.method = "GetItemsImageGalleryInfoBySKU";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({ itemSKU: sku, aspxCommonObj: aspxCommonObj, combinationId: combinationId });
                    this.config.ajaxCallMode = ItemImageGallery.BindItemsImageGallery;
                    this.ajaxCall(this.config);
                }
            },

            BindItemsImageGallery: function (msg) {                
                $("#divMainImage").html('');
                $("#divBindThumbs").html('');
                var windowsWidth = $(window).width();
                var length = msg.d.length;
                if (msg.d.length > 0) {
                    var bindImage = '';
                    var bindImageThumb = '';
                    var item;
                    for (var index = 0; index < length; index++) {
                        item = msg.d[index];
                        var imagePath = itemImagePath + item.ImagePath;
                        if (item.ImagePath == "") {
                            imagePath = noItemDetailImagePath;
                        }
                        else {
                            Imagelist += item.ImagePath + ';';
                        }

                        if (index == 0) {
                            $('.cssClassProductBigPicture').attr('imagepath', imagePath);
                            var rel = "useZoom: 'zoom1', smallImage: '" + aspxRootPath + imagePath.replace('uploads', 'uploads/Large') + "' ";
                            if (index == 0) {
                                bindImage = "<a rel='' href='" + aspxRootPath + imagePath + "' id='zoom1' class='cloud-zoom'  title='" + item.AlternateText + "'><img title='" + item.AlternateText + "' src='" + aspxRootPath + imagePath.replace('uploads', 'uploads/Large') + "'></a></div>";
                                bindImageThumb += '<li><a rel="' + rel + '" href="' + aspxRootPath + imagePath + '" class="cloud-zoom-gallery"><img title="' + item.AlternateText + '" src="' + aspxRootPath + imagePath.replace('uploads', 'uploads/Small') + '" class="zoom-tiny-image"></a></li>';
                            }
                            var href = aspxRootPath + imagePath.replace('uploads', 'uploads/Medium')
                            $(".st_facebook_hcount").attr("st_image", href)
                        }
                        else {
                            bindImageThumb += '<li><a rel="' + rel + '" href="' + aspxRootPath + imagePath + '" class="cloud-zoom-gallery"><img title="' + item.AlternateText + '" src="' + aspxRootPath + imagePath.replace('uploads', 'uploads/Small') + '" class="zoom-tiny-image"></a></li>';
                        }
                    };
                    ImageType = {
                        "Large": "Large",
                        "Medium": "Medium",
                        "Small": "Small"
                    };
                    ImageTypes = ImageType.Large + ';' + ImageType.Small;
                    ItemImageGallery.ResizeImageDynamically(Imagelist, ImageTypes);
                    $("#divMainImage").append(bindImage);                    
                    if (windowsWidth > 800) {
                        $("#divBindThumbs").append("<ul id='thumblist'>" + bindImageThumb + "</ul>");
                    }                   
                } else {
                    var bindImage = '';
                    var bindImageThumb = '';
                    bindImage = "<a rel='' href='" + aspxRootPath + item.noItemDetailImagePath + "' id='zoom1' class='cloud-zoom'  title='" + item.AlternateText + "'><img title='" + item.AlternateText + "' src='" + aspxRootPath + item.noItemDetailImagePath.replace('uploads', 'uploads/Large') + "'></a></div>";
                    bindImageThumb += '<li><a rel="' + rel + '" href="' + aspxRootPath + item.noItemDetailImagePath + '" class="cloud-zoom-gallery"><img title="' + item.AlternateText + '" src="' + aspxRootPath + item.noItemDetailImagePath.replace('uploads', 'uploads/Small') + '" class="zoom-tiny-image"></a></li>';
                    
                    $("#divMainImage").append(bindImage);
                    if (windowsWidth > 800) {
                        $("#divBindThumbs").append("<ul id='thumblist'>" + bindImageThumb + "</ul>");
                    }
                }
                $('#divBindThumbs').jcarousel({
                    vertical: false,
                    scroll: 1,
                    itemFallbackDimension: 300
                });
                if (windowsWidth > 800) {                   
                    $('.cloud-zoom, .cloud-zoom-gallery').CloudZoom({
                        zoomWidth: 'auto',
                        zoomHeight: 'auto',
                        position: 'inner',
                        tint: false,
                        tintOpacity: 0.5,
                        lensOpacity: 0.5,
                        softFocus: false,
                        smoothMove: 3
                    });
                }
            },
            GetGiftCardThemes: function () {
                var param = JSON2.stringify({ itemId: itemId, aspxCommonObj: aspxCommonObj() });
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    url: aspxservicePath + 'AspxCoreHandler.ashx/GetGiftCardThemeImagesByItem',
                    data: param,
                    dataType: "json",
                    success: function (data) {
                        $("#divMainImage").html('');
                        $("#divBindThumbs").html('');
                        if (data.d.length > 0) {
                            var bindImage = '';
                            var bindImageThumb = ''; 
                            $.each(data.d, function (index, item) {
                                if (item.GraphicImage != "" && item.GraphicImage != null) {
                                    var ImageArr = item.GraphicImage.split('/');
                                    Imagelist += ImageArr[ImageArr.length - 1] + ';';
                                }
                                if (index == 0) {
                                    $('.cssClassProductBigPicture').attr('imagepath', aspxRootPath + item.GraphicImage);
                                    var rel = "useZoom: 'zoom1', smallImage: '" + aspxRootPath + item.GraphicImage + "' ";
                                    if (index == 0) {
                                        bindImage = "<a class='selected' rel='' href='" + aspxRootPath + item.GraphicImage + "' id='zoom1' class='cloud-zoom'  title='" + item.GraphicName + "'><img title='" + item.GraphicName + "' src='" + aspxRootPath + item.GraphicImage + "'></a></div>";
                                        bindImageThumb += '<li><a rel="' + rel + '" href="' + aspxRootPath + item.GraphicImage + '" class="cloud-zoom-gallery"><img title="' + item.GraphicName + '" src="' + aspxRootPath + item.GraphicImage + '" class="zoom-tiny-image"></a></li>';
                                    }
                                    var href = aspxRootPath + imagePath;
                                    $(".st_facebook_hcount").attr("st_image", href)
                                }
                                else {
                                    bindImageThumb += '<li><a rel="' + rel + '" href="' + aspxRootPath + item.GraphicImage + '" class="cloud-zoom-gallery"><img title="' + item.GraphicName + '" src="' + aspxRootPath + item.GraphicImage + '" class="zoom-tiny-image"></a></li>';
                                }
                            });
                            ImageType = {
                                "Large": "Large",
                                "Medium": "Medium",
                                "Small": "Small"
                            };
                            ImageTypes = ImageType.Large + ';' + ImageType.Small;
                            ItemImageGallery.ResizeImageDynamically(Imagelist, ImageTypes);
                            $("#divMainImage").append(bindImage);
                            $("#divBindThumbs").append("<ul id='thumblist'>" + bindImageThumb + "</ul>");
                            var windowsWidth = $(window).width();
                            if (windowsWidth >= 800) {
                                $('.cloud-zoom, .cloud-zoom-gallery').CloudZoom({
                                    zoomWidth: 'auto',
                                    zoomHeight: 'auto',
                                    position: 'inside',
                                    tint: false,
                                    tintOpacity: 0.5,
                                    lensOpacity: 0.5,
                                    softFocus: false,
                                    smoothMove: 3
                                });
                            }
                            $('#divBindThumbs').jcarousel({
                                vertical: false,
                                scroll: 1,
                                itemFallbackDimension: 300
                            });
                        }
                    }
                });
            },
            ResizeImageDynamically: function (Imagelist, ImageTypes) {
                ItemDetail.config.method = "MultipleImageResizer";
                ItemDetail.config.url = aspxservicePath + "AspxImageResizerHandler.ashx/" + ItemDetail.config.method;
                ItemDetail.config.data = JSON2.stringify({ imgCollection: Imagelist, types: ImageTypes, imageCatType: "Item" ,aspxCommonObj: ItemImageGallery.vars.aspxCommonInfo});
                ItemDetail.config.ajaxCallMode = ItemDetail.ResizeImageSuccess;
                ItemDetail.ajaxCall(ItemDetail.config);
            },
            ResizeImageSuccess: function () {
            }

        };
        ItemImageGallery.init();       
        ItemImageGalleryApi = function () {
            return {
                ReloadItemImageGallery: function (itemId,itemTypeId, sku, combinationId, aspxCommonObj) {
                    ItemImageGallery.GetImageLists(itemId,itemTypeId, sku, combinationId, aspxCommonObj)
                }
            };
        }();       
    };
    $.fn.ItemImageGallery = function (p) {
        $.ItemImageGalleryView(p);
    };
})(jQuery);