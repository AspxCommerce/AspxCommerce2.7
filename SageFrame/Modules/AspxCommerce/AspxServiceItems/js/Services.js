var ServiceViewAllApi = "";
(function ($) {
    $.ServiceView = function (p) {
        p = $.extend
        ({
            IsEnableService: '',
            ServiceCategoryInARow: 0,
            ServiceCategoryCount: 0,
            IsEnableServiceRss: '',
            ServiceRssCount: 0,
            ServiceDetailsPage: '',
            NoImageService: '',
            ServicePath: aspxservicePath + "AspxServiceItemsHandler.ashx/",
            ServiceModuelPath: ''
        }, p);

        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        ServiceViewAll = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: p.ServiceModuelPath + "ServiceHandler.ashx/",
                method: "",
                url: "",
                ajaxCallMode: ""
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: ServiceViewAll.config.type,
                    contentType: ServiceViewAll.config.contentType,
                    cache: ServiceViewAll.config.cache,
                    async: ServiceViewAll.config.async,
                    url: ServiceViewAll.config.url,
                    data: ServiceViewAll.config.data,
                    dataType: ServiceViewAll.config.dataType,
                    success: ServiceViewAll.config.ajaxCallMode,
                    error: ServiceViewAll.ajaxFailure
                });
            },

            GetAllServices: function () {
                this.config.method = "GetAllServices";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = ServiceViewAll.BindAllServices;
                this.ajaxCall(this.config);
            },

            BindAllServices: function (msg) {
                var serviceImagePath = "Modules/AspxCommerce/AspxCategoryManagement/uploads/";
                var bindServices = '';
                var length = msg.d.length;
                if (msg.d.length > 0) {
                    var item;
                    for (var index = 0; index < length; index++) {
                        item = msg.d[index];
                        var serviceName = item.ServiceName;
                        var servicePath = serviceImagePath + item.ServiceImagePath;
                        if (item.ServiceImagePath == '') {
                            servicePath = defaultImagePath;
                        }
                        var hrefServices = aspxRedirectPath + "service/" + ServiceViewAll.fixedEncodeURIComponent(serviceName) + pageExtension;
                        bindServices += "<li><h3><a href='" + hrefServices + "'><div class='cssClassImgWrapper'><img src='" + aspxRootPath + servicePath.replace('uploads', 'uploads/Medium') + "'/></div>" + serviceName + "</a></h3></li>";
                    };
                    $("#divBindAllServices").append(bindServices);
                }
                else {
                    $("#divBindAllServices").html('<span class=\"cssClassNotFound\">' + getLocale(AspxServiceLocale, "There are no services available!") + '</span>');
                }
            },
            LoadServiceTypeItemRssImage: function () {
            var pageurl = aspxRedirectPath + p.ServiceRssPage + pageExtension;
                $('#serviceItemRssImage').parent('a').show();
                $('#serviceItemRssImage').parent('a').removeAttr('href').attr('href', pageurl);
                $('#serviceItemRssImage').removeAttr('src').attr('src', aspxTemplateFolderPath + '/images/rss-icon.png');
                $('#serviceItemRssImage').removeAttr('title').attr('title', getLocale(AspxRssFeedLocale, "Services Rss Feed Title"));
                $('#serviceItemRssImage').removeAttr('alt').attr('alt', getLocale(AspxRssFeedLocale, "Services Rss Feed Alt"));
            },

            fixedEncodeURIComponent: function (str) {
                return encodeURIComponent(str).replace(/!/g, '%21').replace(/'/g, '%27').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/-/g, '_').replace(/\*/g, '%2A').replace(/%26/g, 'ampersand').replace(/%20/g, '-');
            },

            init: function () {
                                                     if (p.IsEnableServiceRss.toLowerCase() == 'true') {
                        ServiceViewAll.LoadServiceTypeItemRssImage();
                    }
                           }
        };
        ServiceViewAll.init();
    };
    $.fn.ServiceViewAll = function (p) {
        $.ServiceView(p);
    };
})(jQuery);