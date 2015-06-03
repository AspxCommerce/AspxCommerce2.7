(function($) {
    $.createCacheMaintenance = function(p) {
        p = $.extend
                ({
                    PortalID: '',
                    UserModuleID: '',
                    UserName: ''
                }, p);

        var CacheMaintenanceControl = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: SageFrameAppPath + "/Modules/Admin/CacheMaintenance/WebService.asmx/",
                method: "",
                url: "",
                ajaxCallMode: 0,
                PortalID: p.PortalID,
                UserModuleID: p.UserModuleID,
                UserName: p.UserName
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: CacheMaintenanceControl.config.type,
                    contentType: CacheMaintenanceControl.config.contentType,
                    cache: CacheMaintenanceControl.config.cache,
                    async: CacheMaintenanceControl.config.async,
                    url: CacheMaintenanceControl.config.url,
                    data: CacheMaintenanceControl.config.data,
                    dataType: CacheMaintenanceControl.config.dataType,
                    success: CacheMaintenanceControl.ajaxSuccess,
                    error: CacheMaintenanceControl.ajaxFailure,
                    complete: CacheMaintenanceControl.ajaxComplete
                });
            },

            ResetCache: function(CashKey) {
                this.config.method = "ResetCache";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    CashKey: CashKey,
                    PortalID: CacheMaintenanceControl.config.PortalID,
                    userModuleId: CacheMaintenanceControl.config.UserModuleID,
                    UserName: CacheMaintenanceControl.config.UserName,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 0;
                this.ajaxCall(this.config);
            },
            EnableHeavyCache: function(EnableHeavyCacheKey, DisableHeavyCacheKey) {
                this.config.method = "EnableHeavyCache";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    EnableHeavyCacheKey: EnableHeavyCacheKey,
                    DisableHeavyCacheKey: DisableHeavyCacheKey,
                    PortalID: p.PortalID,
                    userModuleId: CacheMaintenanceControl.config.UserModuleID,
                    UserName: CacheMaintenanceControl.config.UserName,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            },

            GetCacheKeys: function() {
                this.config.method = "GetCacheKeys";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = '{}';
                this.config.data = JSON2.stringify({
                    PortalID: p.PortalID,
                    userModuleId: CacheMaintenanceControl.config.UserModuleID,
                    UserName: CacheMaintenanceControl.config.UserName,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            GetHeavyCacheKey: function() {
                this.config.method = "GetHeavyCacheKey";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    PortalID: p.PortalID,
                    userModuleId: CacheMaintenanceControl.config.UserModuleID,
                    UserName: CacheMaintenanceControl.config.UserName,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            },
            BindHeavyCacheKey: function(msg) {
                var HeavyCachKey = msg.d;
                var length = msg.d.length;
                if (length > 0) {

                    $.each(HeavyCachKey, function(index, item) {
                        switch (item.HeavyCacheKey) {
                            case 1:
                                $('#chkFrontMenu').attr('checked', 'checked');
                                break;
                            case 2:
                                $('#chkSideMenu').attr('checked', 'checked');

                                break;
                            case 3:
                                $('#chkFooterMenu').attr('checked', 'checked');
                                break;
                        }

                    });
                }
            },
            BindCachKey: function(msg) {
                var CachKey = msg.d;
                var html = "";
                var length = msg.d.length;
                if (length > 0) {
                    html += '<table width="100%" id="tblCache">';
                    html += '<tr>'
                    html += '<th>Check All</th>'
                    html += '<th><input type="checkbox" id="chkCheckAll" class="sfCheckbox"  /></th>';
                    html += '</tr>';
                    var item;
                    for (var index = 0; index < length; index++) {
                        item = msg.d[index];
                        if (item != "") {
                            html += '<tr>';
                            html += '<td><label id="lbl_" ' + item.CacheKey + ' class="sfFormlabel">' + item.CacheKey + '</label></td>';
                            html += '<td><input type="checkbox" id="chk_" ' + item.CacheKey + ' class="sfCheckbox"  name="clearCache" value="' + item.CacheKey + '" /></td>';
                            html += '</tr>';

                        }
                    };
                }
                else {
                    html += '<label id="lblmsg" class="sfFormlabel"> Cache is clear.</label>';
                }
                html += '</table>';
                $('#divBindCacheKey').html(html);
                CacheMaintenanceControl.CheckAll();
            },
            CheckAll: function() {
                $('#chkCheckAll').on("change", function() {
                    if ($(this).is(':checked')) {
                        $("input[name='clearCache']").each(function() {
                            $(this).prop("checked", true);
                        });

                    } else {
                        $("input[name='clearCache']").each(function() {
                            $(this).prop("checked", false);
                        });
                    }
                });
            },
            BindControl: function() {

                $('#btnSave').on("click", function() {
                    if ($("input[name='clearCache']:checked").length > 0) {
                        var CashKey = "";
                        $("input[name='clearCache']:checked").each(function() {
                            CashKey += $(this).val() + ",";
                        });
                        CacheMaintenanceControl.ResetCache(CashKey);
                        CacheMaintenanceControl.GetCacheKeys();
                    }
                    else {
                        SageFrame.messaging.show("Select at least one check box to clear cache", "Error");
                    }
                });

                CacheMaintenanceControl.CheckAll();
                $('#chkCheckAllHeavyCache').on("change", function() {
                    if ($(this).is(':checked')) {
                        $("input[name='chkHeavyCache']").each(function() {
                            $(this).prop("checked", true);
                        });

                    } else {
                        $("input[name='chkHeavyCache']").each(function() {
                            $(this).prop("checked", false);
                        });
                    }
                });

                $('#tblCache tr').each(function(index, value) {
                    var styleClass = index % 2 == 0 ? "sfOdd" : "sfEven";
                    $(this).addClass(styleClass);
                });
                $('#btnEnableCache').on("click", function() {
                    //if ($("input[name='chkHeavyCache']:checked").length > 0) {
                    var EnableHeavyCacheKey = "";
                    var DisableHeavyCacheKey = "";
                    $("input[name='chkHeavyCache']:checked").each(function() {
                        EnableHeavyCacheKey += $(this).val() + ",";
                    });
                    $("input[name='chkHeavyCache']:not(:checked)").each(function() {
                        DisableHeavyCacheKey += $(this).val() + ",";
                    });
                    CacheMaintenanceControl.EnableHeavyCache(EnableHeavyCacheKey, DisableHeavyCacheKey);
                    //                    }
                    //                    else {
                    //                        SageFrame.messaging.show("Select At least a cache to enable", "error");
                    //                    }
                });
            },
            ajaxComplete: function() {

            },
            ajaxSuccess: function(msg) {
                switch (CacheMaintenanceControl.config.ajaxCallMode) {
                    case 0:
                        SageFrame.messaging.show("Cache clear successfully", "success");
                        break;
                    case 1:
                        CacheMaintenanceControl.BindCachKey(msg);
                        break;
                    case 2:
                        SageFrame.messaging.show("Cache Settings saved successfully.", "success");
                        break;
                    case 3:
                        CacheMaintenanceControl.BindHeavyCacheKey(msg);
                        break;
                }
            },
            ajaxFailure: function(msg) {
                return false;
            },
            init: function(config) {

                CacheMaintenanceControl.BindControl();
                CacheMaintenanceControl.GetCacheKeys();
                CacheMaintenanceControl.GetHeavyCacheKey();
            }
        };
        CacheMaintenanceControl.init();
    };
    $.fn.CacheMaintenance = function(p) {
        $.createCacheMaintenance(p);
    };
})(jQuery);
