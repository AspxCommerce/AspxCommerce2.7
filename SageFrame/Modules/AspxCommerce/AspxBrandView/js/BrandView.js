(function($) {
    $.BrandItemView = function(p) {
        p = $.extend
        ({
            brandModulePath: '',
            enableBrandRss: '',            
            brandRssPage: ''
        }, p);

        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),           
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        var Imagelist = '';
        var BrandView = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: p.brandModulePath + "Services/AspxBrandViewServices.asmx/",
                method: "",
                url: "",
                ajaxCallMode: "",
                error: ""
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: BrandView.config.type,
                    contentType: BrandView.config.contentType,
                    cache: BrandView.config.cache,
                    async: BrandView.config.async,
                    url: BrandView.config.url,
                    data: BrandView.config.data,
                    dataType: BrandView.config.dataType,
                    success: BrandView.config.ajaxCallMode,
                    error: BrandView.config.error
                });
            },

            GetAllBrandForSlider: function() {
                this.config.url = this.config.baseURL + "GetAllBrandForItem";
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                this.config.async = false;
                this.config.ajaxCallMode = BrandView.GetAllBrandForSliderSucess;
                this.ajaxCall(this.config);
            },
            GetAllFeaturedBrand: function() {
                var count = 8;
                this.config.url = this.config.baseURL + "GetAllFeaturedBrand";
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, Count: count });
                this.config.async = false;
                this.config.ajaxCallMode = BrandView.GetAllFeaturedBrandSucess;
                this.ajaxCall(this.config);
            },
            //Send the list of images to the ImageResizer
            ResizeImageDynamically: function (Imagelist) {
                ImageType = {
                    "Large": "Large",
                    "Medium": "Medium",
                    "Small": "Small"
                };
                BrandView.config.method = "DynamicImageResizer";
                BrandView.config.url = aspxservicePath + "AspxImageResizerHandler.ashx/" + this.config.method;
                BrandView.config.data = JSON2.stringify({ imgCollection: Imagelist, type: ImageType.Small,imageCatType: "Brand",aspxCommonObj: aspxCommonObj });
                BrandView.config.ajaxCallMode = BrandView.ResizeImageSuccess;
                BrandView.ajaxCall(BrandView.config);

            },
            ResizeImageSuccess: function () {
            },
            GetAllFeaturedBrandSucess: function (msg) {
                var element = '';
                element += "<ul>";
                var length = msg.d.length;
                if (length > 0) {
                    var value;
                    for (var index = 0; index < length; index++) {
                        value = msg.d[index];
                        if (value.BrandImageUrl != "") {
                            var imgUrlSegments = value.BrandImageUrl.split('/');
                            Imagelist += imgUrlSegments[imgUrlSegments.length - 1] + ';';
                        }
                    };
                    BrandView.ResizeImageDynamically(Imagelist);
                    Imagelist = '';
                    $.each(msg.d, function (index, value) {
                        var imagepath = aspxRootPath + value.BrandImageUrl;
                        element += "<li><a href='" + aspxRedirectPath + "brand/" + fixedEncodeURIComponent(value.BrandName) + pageExtension + "'><img brandId='" + value.BrandID + "' src='" + imagepath.replace('uploads', 'uploads/Small') + "'   alt='" + value.BrandName + "' title='" + value.BrandName + "'  /></a></li>";
                    });
                    element += "</ul>";
                    $(".sfFeaturedBrands").append(element);
                } else {
                    $(".sfFeaturedBrands").append("<span class='cssClassNotFound'>" + getLocale(AspxBrandView, "The store has no featured brand!") + "</span>");
                }
            },
            ClearList: function() {
                $("#a-d").html('');
                $("#e-l").html('');
                $("#m-p").html('');
                $("#q-z").html('');
            },
            GetAllBrandForSliderSucess: function(msg) {
                var elementad = '';
                var elementel = '';
                var elementmp = '';
                var elementqz = '';
                BrandView.ClearList();
                var includedItem=function(arr, obj) {
                    for(var i=0; i<arr.length; i++) {
                        if (arr[i] == obj) return true;
                    }
                }
                var length = msg.d.length;
                if (length > 0) {
                    var value;
                    for (var index = 0; index < length; index++) {
                        value = msg.d[index];
                        var imagepath = aspxRootPath + value.BrandImageUrl;
                        if (value.BrandImageUrl != "") {
                            var imgUrlSegments = value.BrandImageUrl.split('/');
                            var imgToBeAdded = imgUrlSegments[imgUrlSegments.length - 1] + ';';
                            Imagelist += imgToBeAdded;

                        } 
                    };
                    //Resize Image Dynamically
                    if (Imagelist.length > 0) {
                        BrandView.ResizeImageDynamically(Imagelist);
                    }
                   
                    var value;
                    var length = msg.d.length;
                    for (var index = 0; index < length; index++) {
                        value = msg.d[index];
                        var imagepath = aspxRootPath + value.BrandImageUrl;
                        if ((value.BrandName.trim().toUpperCase().charCodeAt(0) >= 65 && value.BrandName.trim().toUpperCase().charCodeAt(0) <= 68) || (value.BrandName.trim().toUpperCase().charCodeAt(0) >= 48 && value.BrandName.trim().toUpperCase().charCodeAt(0) <= 57)) {
                            elementad += "<li><a href='" + aspxRedirectPath + "brand/" + fixedEncodeURIComponent(value.BrandName) + pageExtension + "'><img brandId='" + value.BrandID + "' src='" + imagepath.replace('uploads', 'uploads/Small') + "'   alt='" + value.BrandName + "' title='" + value.BrandName + "'  /><h3 align='center'>" + value.BrandName + "</h3></a></li>";
                        } else if (value.BrandName.trim().toUpperCase().charCodeAt(0) >= 69 && value.BrandName.trim().toUpperCase().charCodeAt(0) <= 76) {
                            elementel += "<li><a href='" + aspxRedirectPath + "brand/" + fixedEncodeURIComponent(value.BrandName) + pageExtension + "'><img brandId='" + value.BrandID + "' src='" + imagepath.replace('uploads', 'uploads/Small') + "'   alt='" + value.BrandName + "' title='" + value.BrandName + "'   /><h3 align='center'>" + value.BrandName + "</h3></a></li>";
                        } else if (value.BrandName.trim().toUpperCase().charCodeAt(0) >= 77 && value.BrandName.trim().toUpperCase().charCodeAt(0) <= 80) {
                            elementmp += "<li><a href='" + aspxRedirectPath + "brand/" + fixedEncodeURIComponent(value.BrandName) + pageExtension + "'><img brandId='" + value.BrandID + "' src='" + imagepath.replace('uploads', 'uploads/Small') + "'   alt='" + value.BrandName + "' title='" + value.BrandName + "'  /><h3 align='center'>" + value.BrandName + "</h3></a></li>";
                        } else if (value.BrandName.trim().toUpperCase().charCodeAt(0) >= 81 && value.BrandName.trim().toUpperCase().charCodeAt(0) <= 90) {
                            elementqz += "<li><a href='" + aspxRedirectPath + "brand/" + fixedEncodeURIComponent(value.BrandName) + pageExtension + "'><img brandId='" + value.BrandID + "' src='" + imagepath.replace('uploads', 'uploads/Small') + "'   alt='" + value.BrandName + "' title='" + value.BrandName + "'   /><h3 align='center'>" + value.BrandName + "</h3></a></li>";
                        }
                    };
                    if (elementad != '') {
                        $("#a-d").prev('h1').show();
                        $("#a-d").show();
                        $("#a-d").append(elementad);
                    }
                    if (elementel != '') {
                        $("#e-l").prev('h1').show();
                        $("#e-l").show();
                        $("#e-l").append(elementel);
                    }
                    if (elementmp != '') {
                        $("#m-p").prev('h1').show();
                        $("#m-p").show();
                        $("#m-p").append(elementmp);
                    }
                    if (elementqz != '') {
                        $("#q-z").prev('h1').show();
                        $("#q-z").show();
                        $("#q-z").append(elementqz);
                    }
                } else {
                    $("#divSlideWrapper").append("<span class='cssClassNotFound'>" + getLocale(AspxBrandView, "The store has no brand!") + "</span>");
                }
                
            },
            LoadViewAllBrandRssImage: function() {
                var pageurl = aspxRedirectPath + p.brandRssPage + pageExtension;
                $('#featureBrandRssImage,#allBrandRssImg').parent('a').show();
                $('#featureBrandRssImage').parent('a').removeAttr('href').attr('href', pageurl + '?type=fbrands');
                $('#featureBrandRssImage').removeAttr('src').attr('src', aspxTemplateFolderPath + '/images/rss-icon.png');
                $('#featureBrandRssImage').removeAttr('title').attr('title', getLocale(AspxBrandView, "Featured Brands Rss Feed Title"))
                $('#featureBrandRssImage').removeAttr('alt').attr('alt', getLocale(AspxBrandView, 'Featured Brands Rss Feed Alt'));

                $('#allBrandRssImg').parent('a').removeAttr('href').attr('href', pageurl + '?type=abrands');
                $('#allBrandRssImg').removeAttr('src').attr('src', aspxTemplateFolderPath + '/images/rss-icon.png');
                $('#allBrandRssImg').removeAttr('title').attr('title', getLocale(AspxBrandView,'All Brands Rss Feed Title'));
                $('#allBrandRssImg').removeAttr('alt').attr('alt', getLocale(AspxBrandView, "All Brands Rss Feed Alt"));
            },
            Init: function() {
                BrandView.GetAllFeaturedBrand();
                BrandView.GetAllBrandForSlider();
                if (p.enableBrandRss.toLowerCase() == "true") {
                    BrandView.LoadViewAllBrandRssImage();
                }
            }
        };
        BrandView.Init();
    };
    $.fn.BrandView = function(p) {
        $.BrandItemView(p);
    };
})(jQuery);