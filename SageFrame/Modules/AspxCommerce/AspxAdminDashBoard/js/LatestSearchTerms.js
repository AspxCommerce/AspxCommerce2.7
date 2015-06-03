$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var latestSearchTermCount = 5;
    var latestSearchTerms = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCoreHandler.ashx/",
            method: "",
            url: "",
            ajaxCallMode: 0
        },
        ajaxCall: function(config) {
            $.ajax({
                type: latestSearchTerms.config.type,
                contentType: latestSearchTerms.config.contentType,
                cache: latestSearchTerms.config.cache,
                async: latestSearchTerms.config.async,
                url: latestSearchTerms.config.url,
                data: latestSearchTerms.config.data,
                dataType: latestSearchTerms.config.dataType,
                success: latestSearchTerms.ajaxSuccess,
                error: latestSearchTerms.ajaxFailure
            });
        },
        GetLatestSearchTerms: function() {
            this.config.url = this.config.baseURL + "GetSearchStatistics";
            this.config.data = JSON2.stringify({ count: latestSearchTermCount, commandName: commandName, aspxCommonObj:aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (latestSearchTerms.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    var length = msg.d.length;
                    if (length > 0) {
                        var bodyElements = '';
                        var headELements = '';
                        headELements += '<table class="classTableWrapper"  width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>';
                        headELements += '<tr class="cssClassHeading"><td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard, "SearchTerm") + '</td>';
                        headELements += '<td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard,"No Of Use")+'</td>';
                        headELements += '</tr></tbody></table>';

                        $("#divLatestSearchTerms").html(headELements);
                        var value;
                        for (var index = 0; index < length; index++) {
                            value = msg.d[index];
                            if (value !== null) {
                                bodyElements += '<tr><td><label class="cssClassLabel">' + (value.SearchTerm != null ? value.SearchTerm : '')+ '</label></td>';
                                bodyElements += '<td><label class="cssClassLabel">' + (value.Count != null ? value.Count : 0) + '</label>';
                                bodyElements += '</tr>';
                            }
                        };
                        $("#divLatestSearchTerms").find('table>tbody').append(bodyElements);

                        $(".classTableWrapper > tbody tr:even").addClass("sfEven");
                        $(".classTableWrapper > tbody tr:odd").addClass("sfOdd");
                    }
                    else {
                        $("#divLatestSearchTerms").html('<span class=\"cssClassNotFound\"> ' + getLocale(AspxAdminDashBoard,"Nothing is searched recently!!")+'</span>');
                    }

                    break;
            }
        },
        ajaxFailure: function(msg) {
            switch (latestSearchTerms.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>' + getLocale(AspxAdminDashBoard,"Error Message")+'</h1><p>' + getLocale(AspxAdminDashBoard,"Failed to load Latest Ordered Items.")+'</p>');
                    break;
            }
        },
        init: function(config) {
            latestSearchTerms.GetLatestSearchTerms();
        }
    }
    latestSearchTerms.init();
});