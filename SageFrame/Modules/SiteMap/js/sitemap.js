$(function () {
    var SiteMap = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: { data: '' },
            dataType: 'json',
            baseURL: SiteMapPath + 'Services/SiteMapWebService.asmx/',
            method: "",
            ModulePath: '',
            PortalID: PortalID,
            UserModuleID: UserModuleID,
            UserName: UserName,
            ajaxCallMode: 0,
            url: "",
            method: "",
            CultureName: CultureName,
            PageExtension: PageExtension
        },
        ajaxSuccess: function (data) {
            switch (SiteMap.config.ajaxCallMode) {
                case 1:
                    SiteMap.BindPages(data);
                    break;
            }
        },
        ajaxFailure: function () {
            alert("you got error");
        },
        ajaxCall: function (config) {
            $.ajax({
                type: this.config.type,
                async: this.config.async,
                contentType: this.config.contentType,
                cache: this.config.cache,
                url: this.config.url,
                data: this.config.data,
                dataType: this.config.dataType,
                success: this.ajaxSuccess,
                error: this.ajaxFailure
            });
        },
        init: function () {
            SiteMap.GetPagesForSiteMap();
        },
        GetPagesForSiteMap: function () {
            SiteMap.config.method = "GetSitemap";
            SiteMap.config.url = SiteMap.config.baseURL + SiteMap.config.method;
            SiteMap.config.data = JSON2.stringify({
                PortalID: SiteMap.config.PortalID,
                UserName: SiteMap.config.UserName,
                CultureCode: SiteMap.config.CultureName,
                UserModuleID: SiteMap.config.UserModuleID,
                secureToken: SageFrameSecureToken
            });
            SiteMap.config.ajaxCallMode = 1;
            SiteMap.ajaxCall(SiteMap.config);
        },
        BindPages: function (data) {
            var pages = data.d;
            var PageID = "";
            var parentID = "";
            var PageLevel = 0;
            var itemPath = "";
            var html = "";
            var rootItemCount = 0;
            $.each(pages, function (index, item) {
                if (item.MenuLevel == 0) {
                    rootItemCount = index;
                }
            });
            $.each(pages, function (index, item) {
                PageID = item.MenuItemID;
                parentID = item.ParentID;
                categoryLevel = item.MenuLevel;
                if (item.MenuLevel == 0) {
                    var PageLink = item.LinkType == 0 ? SageFrameHostURL + item.URL + PageExtension : item.LinkURL;

                    if (item.ChildCount > 0) {
                        html += '<li>' + SiteMap.BuildMenuItem(item, PageLink);
                    }
                    else {
                        html += '<li>' + SiteMap.BuildMenuItem(item, PageLink);
                    }
                    if (parseInt(item.LinkType) == 1) {
                        html += '<ul class="megamenu"><li style="><div class="megawrapper">' + item.HtmlContent + '</div></li></ul>';
                    }
                    else {
                        if (item.ChildCount > 0) {
                            html += "<ul>";
                            itemPath += item.Title;
                            html += SiteMap.BindChildCategory(pages, PageID);
                            html += "</ul>";
                        }
                    }
                    html += "</li>";
                }
                itemPath = '';
            });
            html += '</ul>';
            $("#primaryNav").html(html);
        },
        BindChildCategory: function (response, PageID) {
            var strListmaker = '';
            var childNodes = '';
            var path = '';
            var itemPath = "";
            itemPath += "";
            var html = '';
            $.each(response, function (index, item) {
                if (item.MenuLevel > 0) {
                    if (item.ParentID == PageID) {
                        itemPath += item.Title;
                        var PageLink = item.LinkType == 0 ? SageFrameHostURL + item.URL + PageExtension : item.LinkURL; 
                        var styleClass = item.ChildCount > 0 ? 'class="sfParent"' : '';
                        strListmaker += '<li>' + SiteMap.BuildMenuItem(item, PageLink, 0);
                        childNodes = SiteMap.BindChildCategory(response, item.MenuItemID);
                        if (childNodes != '') {
                            strListmaker += "<ul>" + childNodes + "</ul>";
                        }
                        if (item.HtmlContent != '') {
                            strListmaker += '<ul class="megamenu"><li style="><div class="megawrapper">' + item.HtmlContent + '</div></li></ul>';
                        }
                        strListmaker += "</li>";
                    }
                }
            });
            return strListmaker;
        },
        BuildMenuItem: function (item, PageLink) {
            var html = '';
            if (!item.IsActive) { PageLink = '#'; }
            var title = item.LinkType == 0 ? item.PageName : item.Title;
            PageLink = item.LinkType == 1 ? '#' : PageLink;
            var arrowStyle = item.ChildCount > 0 ? '<span class="sf-sub-indicator"> »</span>' : '';
            html = '<a  href=' + PageLink + '>' + title + '</a>';
            return html;
        }
    };
    SiteMap.init();
});