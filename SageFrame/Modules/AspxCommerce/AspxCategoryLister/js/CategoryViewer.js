var Category = "";

$(function() {
    var aspxCommonObj = function() {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        return aspxCommonInfo;
    };

    Category = {
        vars: {
            itemPath: ""
        },
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath(),
            method: "",
            url: "",
            ajaxCallMode: ""
        },
        ajaxCall: function(config) {
            $.ajax({
                type: Category.config.type,
                contentType: Category.config.contentType,
                cache: Category.config.cache,
                async: Category.config.async,
                url: Category.config.url,
                data: Category.config.data,
                dataType: Category.config.dataType,
                success: Category.config.ajaxCallMode,
                error: Category.ajaxFailure
            });
        },

        BindCategoryMenuList: function (msg) {
            var length = msg.d.length;
            if (length > 0) {
                var categoryID = '';
                var parentID = '';
                var categoryLevel = '';
                var attributeValue;
                var catListmaker = '';
                var catList = '';
                catListmaker += "<div class=\"cssClassCategoryNavHor\"><ul class='sf-menu'>";
                var eachCat = '';
                for (var index = 0; index < length; index++) {
                    eachCat = msg.d[index];
                    var totalCategory = eachCat.length;
                    var css = '';
                    if (eachCat.ChildCount > 0) {
                        css = "class=cssClassCategoryParent";
                    } else {
                        css = "";
                    }
                    categoryID = eachCat.CategoryID;
                    parentID = eachCat.ParentID;
                    categoryLevel = eachCat.CategoryLevel;
                    attributeValue = eachCat.AttributeValue;
                    if (eachCat.CategoryLevel == 0) {
                        var hrefParentCategory = aspxRedirectPath + "category/" + fixedEncodeURIComponent(eachCat.AttributeValue) + pageExtension;
                        catListmaker += "<li " + css + "><a href=" + hrefParentCategory + ">";
                        catListmaker += eachCat.AttributeValue;
                        catListmaker += "</a>";

                        if (eachCat.ChildCount > 0) {
                            catListmaker += "<ul>";
                            Category.vars.itemPath += eachCat.AttributeValue;
                            catListmaker += Category.BindChildCategory(msg.d, categoryID);
                            catListmaker += "</ul>";
                        }
                        catListmaker += "</li>";
                    }
                    Category.vars.itemPath = '';
                };
                catListmaker += "<div class=\"cssClassclear\"></div></ul></div>";
                $("#divCategoryLister").html(catListmaker);
            } else {
                $("#divCategoryLister").html("<span class=\"cssClassNotFound\">" + getLocale(AspxCategoryLister, "This store has no category found!") + "</span>");
            }
            if ($('.cssClassCategoryNav>ul>li').length > 4) {
                $('.cssClassCategoryNav>ul>li:last').addClass('cssClassLastMenu');
            }
        },

        BindCategory: function() {
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.UserName = null;
            this.config.method = "AspxCommerceWebService.asmx/GetCategoryMenuList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = Category.BindCategoryMenuList;
            this.ajaxCall(this.config);
        },

        BindChildCategory: function(response, categoryID) {
            var strListmaker = '';
            var childNodes = '';
            var path = '';
            Category.vars.itemPath += "/";
            $.each(response, function(index, eachCat) {
                if (eachCat.CategoryLevel > 0) {
                    if (eachCat.ParentID == categoryID) {
                        var css = '';
                        if (eachCat.ChildCount > 0) {
                            css = "class=cssClassCategoryParent";
                        } else {
                            css = "";
                        }
                        var hrefCategory = aspxRedirectPath + "category/" + fixedEncodeURIComponent(eachCat.AttributeValue) + pageExtension;
                        Category.vars.itemPath += eachCat.AttributeValue;
                        strListmaker += "<li " + css + "><a href=" + hrefCategory + ">" + eachCat.AttributeValue + "</a>";
                        childNodes = Category.BindChildCategory(response, eachCat.CategoryID);
                        Category.vars.itemPath = Category.vars.itemPath.replace(Category.vars.itemPath.lastIndexOf(eachCat.AttributeValue), '');
                        if (childNodes != '') {
                            strListmaker += "<ul>" + childNodes + "</ul>";
                        }
                        strListmaker += "</li>";
                    }
                }
            });
            return strListmaker;
        },
        LoadCategoryRssImage: function() {
            var pageurl = aspxRedirectPath + rssFeedUrl + pageExtension;
            $('#categoryRssImage').parent('a').show();
            $('#categoryRssImage').parent('a').removeAttr('href').attr('href', pageurl + '?type=rss&action=category');
            $('#categoryRssImage').removeAttr('src').attr('src', aspxTemplateFolderPath + '/images/rss-icon.png');
            $('#categoryRssImage').removeAttr('title').attr('title', getLocale(AspxRssFeedLocale, "Category Rss Feed Title"));
            $('#categoryRssImage').removeAttr('alt').attr('alt', getLocale(AspxRssFeedLocale, "Category Rss Feed Alt"));
        },
        Init: function() {
            if (categoryRss.toLowerCase() == 'true') {
                LoadCategoryRssImage();
            }
                                            
        }
    };
    Category.Init();
});