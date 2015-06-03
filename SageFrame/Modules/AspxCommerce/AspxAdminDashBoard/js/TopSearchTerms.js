
$(function() {
    var aspxCommonObj = {
        StoreID: AspxCommerce.utils.GetStoreID(),
        PortalID: AspxCommerce.utils.GetPortalID(),
        UserName: AspxCommerce.utils.GetUserName(),
        CultureName: AspxCommerce.utils.GetCultureName()
    };
    var topSearchTermcount = 5;
    var latestSearchTermCount = 5;
    var topSearchTerms = {
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
                type: topSearchTerms.config.type,
                contentType: topSearchTerms.config.contentType,
                cache: topSearchTerms.config.cache,
                async: topSearchTerms.config.async,
                url: topSearchTerms.config.url,
                data: topSearchTerms.config.data,
                dataType: topSearchTerms.config.dataType,
                success: topSearchTerms.ajaxSuccess,
                error: topSearchTerms.ajaxFailure
            });
        },
        SetFirstTabActive: function() {
            var $tabs = $('#container-8').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });           
            $tabs.tabs("option", "active", 0);
        },
        GetTopSearchItems: function() {
            var commandName = "TOP";
            this.config.url = this.config.baseURL + "GetSearchStatistics";
            this.config.data = JSON2.stringify({ count: topSearchTermcount, commandName: commandName, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        GetLatestSearchTerms: function() {
            var commandName = "LATEST";
            this.config.url = this.config.baseURL + "GetSearchStatistics";
            this.config.data = JSON2.stringify({ count: latestSearchTermCount, commandName: commandName, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = 2;
            this.ajaxCall(this.config);
        },
        ajaxSuccess: function(msg) {
            switch (topSearchTerms.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                var length = msg.d.length;
                if (length > 0) {
                    var bodyElements = '';
                    var headELements = '';
                    headELements += '<table class="classTableWrapper cssTopSearch"  width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>';
                    headELements += '<tr class="cssClassHeading"><td class="cssClassNormalHeading" >' + getLocale(AspxAdminDashBoard, "Top Search Term") + ' </td>';
                    headELements += '<td  class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard, "No Of Use") + '</td>';
                    headELements += '</tr></tbody></table>';
                    $("#divTopSearchTerms").html(headELements);
                    var value;
                    for (var index = 0; index < length; index++) {
                        value = msg.d[index];
                        if (value !== null) {
                            bodyElements += '<tr><td><label class="cssClassLabel">' + (value.SearchTerm != null ? value.SearchTerm : '') + '</label></td>';
                            bodyElements += '<td><label class="cssClassLabel">' + (value.Count != null ? value.Count : 0) + '</label>';
                            bodyElements += '</tr>';
                        }
                    };
                    $("#divTopSearchTerms").find('table>tbody').append(bodyElements);
                    $(".cssTopSearch > tbody tr:even").addClass("sfOdd");
                    $(".cssTopSearch > tbody tr:odd").addClass("sfEven");

                } else {
                    $("#divTopSearchTerms").html('<span class=\"cssClassNotFound\">' + getLocale(AspxAdminDashBoard, "Nothing is searched recently!!") + '</span>');
                }
                break;
            case 2:
                var length = msg.d.length;
                if (length > 0) {
                    var bodyElements = '';
                    var headELements = '';
                    headELements += '<table class="classTableWrapper cssLatestSearch"  width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>';
                    headELements += '<tr class="cssClassHeading"><td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard, "Latest Search Term") + '</td>';
                    headELements += '<td class="cssClassNormalHeading">' + getLocale(AspxAdminDashBoard, "No Of Use") + '</td>';
                    headELements += '</tr></tbody></table>';

                    $("#divLatestSearchTerms").html(headELements);
                    var value;
                    for (var index = 0; index < length; index++) {
                        value = msg.d[index];
                        if (value !== null) {
                            bodyElements += '<tr><td><label class="cssClassLabel">' + (value.SearchTerm != null ? value.SearchTerm : '') + '</label></td>';
                            bodyElements += '<td><label class="cssClassLabel">' + (value.Count != null ? value.Count : 0) + '</label>';
                            bodyElements += '</tr>';
                        }
                    };
                    $("#divLatestSearchTerms").find('table>tbody').append(bodyElements);

                    $(".cssLatestSearch > tbody tr:even").addClass("sfOdd");
                    $(".cssLatestSearch > tbody tr:odd").addClass("sfEven");
                } else {
                    $("#divLatestSearchTerms").html('<span class=\"cssClassNotFound\">' + getLocale(AspxAdminDashBoard, "Nothing is searched recently!!") + '</span>');
                }

                break;
            }
        },
        ajaxFailure: function(msg) {
            switch (topSearchTerms.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                csscody.error('<h1>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h1><p>' + getLocale(AspxAdminDashBoard, "Failed to load Top Search Terms.") + '</p>');
                break;
            case 2:
                csscody.error('<h1>' + getLocale(AspxAdminDashBoard, "Error Message") + '</h1><p>' + getLocale(AspxAdminDashBoard, "Failed to load Latest search terms.") + '</p>');
                break;
            }
        },
        init: function(config) {
            topSearchTerms.SetFirstTabActive();
            topSearchTerms.GetLatestSearchTerms();
            topSearchTerms.GetTopSearchItems();
        }
    };
    topSearchTerms.init();
});