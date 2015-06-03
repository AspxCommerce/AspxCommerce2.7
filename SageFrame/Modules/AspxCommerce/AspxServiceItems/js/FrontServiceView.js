var FrontServicesAPI = "";
;(function($) {
    $.FrontServiceView = function(p) {
        p = $.extend
        ({
            IsEnableService: '',
            ServiceCategoryInARow: 0,
            ServiceCategoryCount: 0,
            IsEnableServiceRss: '',
            ServiceRssCount: 0,
            ServiceDetailsPage: '',
            NoImageService: '',
            ServicePath: '',
            ServiceModuelPath: ''
        }, p);

        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        var FrontServices = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: p.ServiceModulePath + "ServiceHandler.ashx/",
                method: "",
                url: "",
                ajaxCallMode: ""
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: FrontServices.config.type,
                    contentType: FrontServices.config.contentType,
                    cache: FrontServices.config.cache,
                    async: FrontServices.config.async,
                    url: FrontServices.config.url,
                    data: FrontServices.config.data,
                    dataType: FrontServices.config.dataType,
                    success: FrontServices.config.ajaxCallMode,
                    error: FrontServices.ajaxFailure
                });
            },
            GetServiceSetting: function() {
                var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
                this.config.method = "GetServiceSetting";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = FrontServices.BindServiceSetting;
                this.ajaxCall(this.config);
            },
            BindServiceSetting: function (msg) {
                var length = msg.d.length;
                if (length > 0) {
                    var item;
                    for (var index = 0; index < length; index++) {
                        item = msg.d[index];
                        p.IsEnableService = item.IsEnableService;
                        p.ServiceCount = item.ServiceCategoryCount;
                        p.ServiceInARow = item.ServiceCategoryInARow;
                        p.IsEnableServiceRss = item.IsEnableServiceRss;
                        p.ServiceRssCount = item.ServiceRssCount;
                        p.ServiceDetailsPage = item.ServiceDetailsPage;
                    };
                }
            },
            GetFrontServices: function() {
                this.config.method = "GetFrontServices";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, count: p.ServiceCategoryCount });
                this.config.ajaxCallMode = FrontServices.BindAllFrontServices;
                this.ajaxCall(this.config);
            },

            BindAllFrontServices: function(msg) {
                $("#divFrontService").html("");
                var serviceImagePath = "/Modules/AspxCommerce/AspxCategoryManagement/uploads/";
                var bindFrontServices = '';
                var serviceDesc = '';
                var index = '';
                var serviceDetails = '';
                var rowTotal = '';
                var length = msg.d.length;
                if (length > 0) {
                    rowTotal = msg.d.length;
                    var item;
                    for (var i = 0; i < length; i++) {
                        item = msg.d[i];
                                               if (item.ServiceDetails !== '' || item.ServiceDetails !== 'null') {
                            serviceDesc = item.ServiceDetail;
                            if (serviceDesc.indexOf(' ') > 1) {
                                index = serviceDesc.substring(0, 60).lastIndexOf(' ');
                                serviceDetails = serviceDesc.substring(0, index);
                                serviceDetails = serviceDetails + ' ...';
                            } else {
                                serviceDetails = serviceDesc;
                            }
                        } else {
                            serviceDetails = '';
                        }
                        var servicePath = serviceImagePath + item.ServiceImagePath;
                        if (item.ServiceImagePath == '') {
                            servicePath = p.NoImageService;
                        }
                        var hrefServices = aspxRedirectPath + "service/" + FrontServices.fixedEncodeURIComponent(item.ServiceName) + pageExtension;

                        bindFrontServices += "<li><h3><a href='" + hrefServices + "'><span>" + item.ServiceName;
                        bindFrontServices += "<div class='cssClassImgWrapper'><span><img title='" + item.ServiceName + "' alt='" + item.ServiceName + "' src='" + aspxRootPath + servicePath.replace('uploads', 'uploads/Medium') + "'/></div></span></a></h3><p>" + serviceDetails + "</p></li>";
                    });
                    $("#divFrontService").append(bindFrontServices);
                    if (parseInt(rowTotal) > count) {
                        $("#divViewMoreServices").append("<a href='" + aspxRootPath + p.ServiceDetailsPage + pageExtension + "'>" + getLocale(AspxShipmentsManagement, "View More") + "</a>");
                    }
                }
                else {
                    $("#divFrontService").html("<span class=\"cssClassNotFound\">" + getLocale(AspxServiceLocale, "There are no services available!") + "</span>");
                }
            },

            fixedEncodeURIComponent: function(str) {
                return encodeURIComponent(str).replace(/!/g, '%21').replace(/'/g, '%27').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/-/g, '_').replace(/\*/g, '%2A').replace(/%26/g, 'ampersand').replace(/%20/g, '-');
            },
            LoadServiceTypeItemRssImage: function() {
                var pageurl = aspxRedirectPath + p.ServiceRssPage + pageExtension;
                $('#serviceItemRssImage').parent('a').show();
                $('#serviceItemRssImage').parent('a').removeAttr('href').attr('href', pageurl);
                $('#serviceItemRssImage').removeAttr('src').attr('src', aspxTemplateFolderPath + '/images/rss-icon.png');
                $('#serviceItemRssImage').removeAttr('title').attr('title', getLocale(AspxRssFeedLocale, "Services Rss Feed Title"));
                $('#serviceItemRssImage').removeAttr('alt').attr('alt', getLocale(AspxRssFeedLocale, "Services Rss Feed Alt"));
            },
            init: function() {
                $(document).ready(function() {
                                       $('#divFrontService a img[title]').tipsy({ gravity: 'n' });
                    if (p.IsEnableServiceRss.toLowerCase() == 'true') {
                        FrontServices.LoadServiceTypeItemRssImage();
                    }
                });
            }
        };
        FrontServices.init();
    };
    $.fn.FrontServices = function(p) {
        $.FrontServiceView(p);
    };
})(jQuery);