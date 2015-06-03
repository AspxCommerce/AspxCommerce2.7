(function($) {
    $.createBreadCrumb = function(p) {
        p = $.extend({
            baseURL: '/Modules/BreadCrumb/BreadCrumbWebService.asmx/',
            PagePath: "Home",
            PortalID: 1,
            PageName: 'Home',
            Container: "div.sfBreadcrumb",
            MenuId: "1",
            CultureCode: ""
        }, p);

        var BreadCrum = {
            config:
        {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: { data: '' },
            dataType: 'json',
            baseURL: p.baseURL,
            PagePath: p.PagePath,
            method: "",
            url: "",
            ajaxCallMode: 0,
            PortalID: p.PortalID,
            PageName: p.PageName,
            MenuId: p.MenuId,
            arr: [],
            arrPages: [],
            CultureCode: p.CultureCode
        },

            init: function() {
                $(".sfLocalee").Localize({
                    moduleKey: BreadCrumLanguage
                });
                this.LoadBreadCrum()
            },

            LoadBreadCrum: function() {
                this.config.method = "GetBreadCrumb";
                this.config.url = BreadCrum.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ PortalID: BreadCrum.config.PortalID, PageName: BreadCrum.config.PageName, MenuId: BreadCrum.config.MenuId, CultureCode: BreadCrum.config.CultureCode });
                this.config.ajaxCallMode = 0;
                this.ajaxCall(this.config)
            },

            BindBreadCrum: function(data) {
                var elem = '';
                var breadcrum = data.d;
                var html = '';
                var childPages = '';
                var isadmin = false;
                var isDuplicate = false;
                var isDistinct = false;
                $.each(breadcrum, function(index, item) {

                    if (elem.indexOf($.trim(item.TabPath)) != -1) {
                        isDuplicate = true;
                    }
                    if (isDuplicate) {
                        isDistinct = true;
                    }

                    elem = elem + item.TabPath;
                });
                $.each(breadcrum, function(index, item) {
                    if (item != "") {
                        childPages += item.TabPath + "/"; childPages = childPages.substring(0, childPages.length - 1);
                        var pageLink = BreadCrum.config.PagePath + "/" + childPages + Extension;
                        if (item === "Admin") {
                            pageLink = pageLink;
                            isadmin = true;
                        }
                        else if (item === "Super-User") {
                            pageLink = pageLink;
                            isadmin = true;
                        }
                        childPages += "/";
                        if (item.LocalPage != '') {
                            html += '<li><a href=' + item.TabPath + Extension + '>' + item.LocalPage.replace(new RegExp("-", "g"), ' ') + '</a></li>';
                        }
                        else {
                            if (isDistinct && index == 0) {
                                //do nothing
                            }
                            else {
                                html += '<li><a href=' + item.TabPath + Extension + '>' + item.TabPath.replace(new RegExp("-", "g"), ' ') + '</a></li>';
                            }
                        }
                    }
                });
                $(p.Container).append(html);
                var lastLink = $(p.Container + ' li:last a').html();
                $(p.Container + ' li:last').addClass("sfLast").html(lastLink);
            },

            ajaxSuccess: function(data) {
                switch (BreadCrum.config.ajaxCallMode) {
                    case 0: BreadCrum.BindBreadCrum(data);
                        break;
                }
            },

            ajaxFailure: function() {
                return false;
            },

            ajaxCall: function(config) {
                $.ajax({
                    type: BreadCrum.config.type,
                    contentType: BreadCrum.config.contentType,
                    cache: BreadCrum.config.cache,
                    async: BreadCrum.config.async,
                    url: BreadCrum.config.url,
                    data: BreadCrum.config.data,
                    dataType: BreadCrum.config.dataType,
                    success: BreadCrum.ajaxSuccess,
                    error: BreadCrum.ajaxFailure
                })
            }
        };
        BreadCrum.init()
    };
    $.fn.BreadCrumbBuilder = function(p) {
        $.createBreadCrumb(p)
    }
})(jQuery);