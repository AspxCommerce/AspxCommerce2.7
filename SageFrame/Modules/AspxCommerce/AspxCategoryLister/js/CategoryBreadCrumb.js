var Breadcrum = "";
$.extend($.expr[':'], {
    containsExact: function (a, i, m) {
        var ihtml = $('<div />').html(a.innerHTML).text();
        return $.trim(ihtml.toLowerCase()) === m[3].toLowerCase();
    }
});
Array.prototype.clean = function (deleteValue) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == deleteValue) {
            this.splice(i, 1);
            i--;
        }
    }
    return this;
};
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
    Breadcrum = {
        vars: {
            itemCat: "",
            itmName: "",
            current: ""
        },
        config: {
            isPostBack: false,
            async: true,
            cache: true,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath(),
            method: "",
            url: "",
            ajaxCallMode: ""
        },
        ajaxCall: function (config) {
            $.ajax({
                type: Breadcrum.config.type,
                contentType: Breadcrum.config.contentType,
                cache: Breadcrum.config.cache,
                async: Breadcrum.config.async,
                url: Breadcrum.config.url,
                data: Breadcrum.config.data,
                dataType: Breadcrum.config.dataType,
                success: Breadcrum.config.ajaxCallMode,
                error: Breadcrum.config.ajaxFailure
            });
        },
        
        getCategoryForItemPortal: function (itmName) {
            itmName = decodeURIComponent(itmName.replace('ampersand', '&').replace(new RegExp("-", "g"), ' ').replace('_', '-'));
            Breadcrum.config.method = "AspxCommonHandler.ashx/GetCategoryForItem";
            Breadcrum.config.url = Breadcrum.config.baseURL + Breadcrum.config.method;
            Breadcrum.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), itemSku: itmName, cultureName: AspxCommerce.utils.GetCultureName() });
            Breadcrum.config.ajaxCallMode = Breadcrum.BindCategoryForItemPortal;
            Breadcrum.ajaxCall(Breadcrum.config);
        },
        BindCategoryForItemPortal: function (msg) {

            if (msg.d != null) {
                if (msg.d != "") {
                    var dx = jQuery.parseJSON(msg.d);
                    Breadcrum.vars.itemCat = dx.ItemCategory;
                    var tag = new Array();
                    var hrefarr = new Array();
                    tag = [];
                    hrefarr = [];
                    $('#breadcrumb ul').html('');
                    Breadcrum.vars.current = dx.ItemName;
                    var parents = dx.ParentCategories;
                    parents = parents.split(',');
                    var plength = parents.length;
                    for (var counter = 0; counter < plength; counter++) {
                        tag.push(parents[counter]);
                        var hrf = aspxRedirectPath + "category/" + fixedEncodeURIComponent($.trim(parents[counter])) + pageExtension;
                        hrefarr.push(hrf);
                    }
                    hrefarr.reverse();
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + pageExtension + ' class="i-home">' + getLocale(AspxCategoryLister, 'home') + '</a></li>');
                    var tlength = tag.length;
                    if (tlength > 0) {
                        tag.reverse();
                        for (var x = 0; x < tlength; x++) {
                            $('#breadcrumb ul').append('<li ><a href="' + $.trim(hrefarr[x]) + '">' + tag[x] + '</a></li>');
                        }
                    }

                    $('#breadcrumb ul').append('<li class="last">' + fixedDecodeURIComponent(Breadcrum.vars.current) + ' </li>');
                    tag = [];
                    hrefarr = [];


                } else {
                    $('#breadcrumb li:last').remove();
                }
            }

        },
        getCategoryForItem: function (itmName) {
            itmName = decodeURIComponent(itmName.replace('ampersand', '&').replace(new RegExp("-", "g"), ' ').replace('_', '-'));
            Breadcrum.config.method = "AspxCommonHandler.ashx/GetCategoryForItem";
            Breadcrum.config.url = Breadcrum.config.baseURL + Breadcrum.config.method;
            Breadcrum.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), itemSku: itmName, cultureName: AspxCommerce.utils.GetCultureName() });
            Breadcrum.config.ajaxCallMode = Breadcrum.BindCategoryForItem;
            Breadcrum.ajaxCall(Breadcrum.config);
        },
        BindCategoryForItem: function (msg) {

            if (msg.d != null) {
                if (msg.d != "") {
                    var dx = jQuery.parseJSON(msg.d);
                    Breadcrum.vars.itemCat = dx.ItemCategory;
                    var tag = new Array();
                    var hrefarr = new Array();

                    Breadcrum.vars.current = dx.ItemName;
                    var parents = dx.ParentCategories;
                    parents = parents.split(',');
                    var plength = parents.length;
                    for (var counter = 0; counter < plength; counter++) {
                        tag.push(parents[counter]);
                        var hrf = aspxRedirectPath + "category/" + fixedEncodeURIComponent($.trim(parents[counter])) + pageExtension;
                        hrefarr.push(hrf);
                    }
                    hrefarr.reverse();
                    $('#breadcrumb ul').html('');
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + pageExtension + ' class="i-home">' + getLocale(AspxCategoryLister, 'home') + '</a></li>');
                    $('#breadcrumb ul li:gt(0)').remove();
                    var tlength = tag.length;
                    if (tlength > 0) {
                        tag.reverse();
                        for (var x = 0; x < tlength; x++) {
                            $('#breadcrumb ul').append('<li ><a href="' + $.trim(hrefarr[x]) + '">' + tag[x] + '</a></li>');
                        }
                    }
                    $('#breadcrumb ul').append('<li class="last">' + fixedDecodeURIComponent(Breadcrum.vars.current) + ' </li>');
                    tag = [];
                    hrefarr = [];

                    $('#breadcrumb ul li').not('.last').click(function () {
                        if ($(this).attr('class') == 'first') {
                        } else {
                            var current = $(this).children().html();
                            $(this).nextAll().remove();
                            $('#breadcrumb li:last').remove();
                            $('#breadcrumb ul').append('<li class="last">' + fixedDecodeURIComponent(current) + '</li>');
                        }
                    });
                } else {
                    $('#breadcrumb li:last').remove();
                }
            }
        },
        Init: function () {
        }
    };
    Breadcrum.Init();
});