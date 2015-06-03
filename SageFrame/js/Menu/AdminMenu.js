(function($) {
    $.createSageAdminMenu = function(p) {
        p = $.extend
        ({
            baseURL: '/Modules/SageMenu/MenuWebService.asmx/',
            UserModuleID: 1,
            PortalID: 1,
            CultureCode: 'en-US',
            UserName: 'username',
            UserMode: 0,
            PagePath: "Home.aspx",
            PortalSEOName: "default"
        }, p);

        var SageMenuAdmin = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: { data: '' },
                dataType: 'json',
                baseURL: p.baseURL,
                method: "",
                url: "",
                ajaxCallMode: 0,
                arr: [],
                arrPages: [],
                UserModuleID: p.UserModuleID,
                PortalID: p.PortalID,
                CultureCode: p.CultureCode,
                UserName: p.UserName,
                UserMode: p.UserMode


            },
            init: function() {
                this.LoadAdminMenu();
            },
            HighlightSelected: function() {
                var menu = $("#mnuAdminSageFrame ul li");
                $.each(menu, function(index, item) {
                    var hreflink = $(this).find("a").attr("href");
                    if (location.href.toLowerCase().indexOf(hreflink.toLowerCase()) > -1) {
                        $(this).addClass('cssClassActive');
                    }
                });
            },
            ajaxSuccess: function(data) {
                switch (SageMenuAdmin.config.ajaxCallMode) {
                    case 0:
                        SageMenuAdmin.BindAdminMenu(data);
                        break;
                }
            },
            ajaxFailure: function() {
                return false;
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: SageMenuAdmin.config.type,
                    contentType: SageMenuAdmin.config.contentType,
                    cache: SageMenuAdmin.config.cache,
                    url: SageMenuAdmin.config.url,
                    data: SageMenuAdmin.config.data,
                    dataType: SageMenuAdmin.config.dataType,
                    success: SageMenuAdmin.ajaxSuccess,
                    error: SageMenuAdmin.ajaxFailure
                });

            },
            LoadAdminMenu: function() {
                this.config.method = "GetBackEndMenu";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ PortalID: parseInt(SageMenuAdmin.config.PortalID), UserName: SageMenuAdmin.config.UserName, CultureCode: SageMenuAdmin.config.CultureCode, UserMode: parseInt(SageMenuAdmin.config.UserMode), PortalSEOName: p.PortalSEOName });
                this.config.ajaxCallMode = 0;
                this.ajaxCall(this.config);
            },
            BindAdminMenu: function(data) {
                var pages = data.d;
                var PageID = "";
                var parentID = "";
                var PageLevel = 0;
                var itemPath = '';
                var html = "";
                html += '<ul class="sagemenu-admin">';
                $.each(pages, function(index, item) {
                    PageID = item.PageID;
                    parentID = item.ParentID;
                    PageLevel = item.Level;
                    if (item.Level == 0) {
                        var PageLink = p.PagePath + item.TabPath;
                        html += '<li class="menu-header"><a href=' + PageLink + '>';
                        html += item.PageName;
                        html += "</a>";
                        if (item.ChildCount > 0) {
                            html += "<ul>";
                            itemPath += item.PageName;
                            html += SageMenuAdmin.BindFooterChildren(pages, PageID);
                            html += "</ul>";
                        }
                        html += "</li>";
                    }
                    itemPath = '';
                });
                html += '</ul>';
                $('#mnuAdminSageFrame').append(html);
                jQuery('ul.sagemenu-admin').superfish();
                SageMenuAdmin.HighlightSelected();
            },
            BindFooterChildren: function(response, PageID) {
                var strListmaker = '';
                var childNodes = '';
                var path = '';
                var itemPath = "";
                $.each(response, function(index, item) {
                    if (item.Level > 0) {
                        if (item.ParentID == PageID) {
                            itemPath += item.PageName;
                            var PageLink = p.PagePath + item.TabPath;
                            strListmaker += '<li class="child"><a  href=' + PageLink + '>' + item.PageName + '</a>';
                            childNodes = SageMenuAdmin.BindFooterChildren(response, item.PageID);
                            if (childNodes != '') {
                                strListmaker += "<ul>" + childNodes + "</ul>";
                            }
                            strListmaker += "</li>";
                        }
                    }
                });
                return strListmaker;
            }
        };
        SageMenuAdmin.init();
    };

    $.fn.SageAdminMenuBuilder = function(p) {
        $.createSageAdminMenu(p);
    };
})(jQuery);

   
   