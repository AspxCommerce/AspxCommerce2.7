<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BrandSettings.ascx.cs" Inherits="Modules_AspxCommerce_AspxBrandView_BrandSettings" %>

<div class="cssBrandSettings">
    <table>
        <thead>
            <tr><td class="sfLocale">
                Brand Settings</td></tr>
        </thead>
         <tr>
            <td>
                <asp:Label ID="lblEnableBrand" runat="server" Text="Enable Brand Slider" 
                    meta:resourcekey="lblEnableBrandResource1"></asp:Label>
            </td>
            <td>
                <input type="checkbox" id="chkEnableBrand" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblBrandCount" runat="server" 
                    Text="Enter the Number of Brand Slide" 
                    meta:resourcekey="lblBrandCountResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtBrandCount" name="BrandCount" class="required number" />
            </td>
        </tr>        
         <tr>
            <td>
                <asp:Label ID="lblBrandAllPage" runat="server" Text="Show All Brand Page:" 
                    meta:resourcekey="lblBrandAllPageResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtAllBrandPage" disabled="disabled" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblEnableBrandRss" runat="server" Text="Enable Brand Rss" 
                    meta:resourcekey="lblEnableBrandRssResource1"></asp:Label>
            </td>
            <td>
                <input type="checkbox" id="chkEnableBrandRss" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblBrandRssCount" runat="server" 
                    Text="Number Of Brand To Show In Rss" 
                    meta:resourcekey="lblBrandRssCountResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtBrandRssCount" name="BrandRssCount" class="required number" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblBrandRssPage" runat="server" Text="Brand Rss Page" 
                    meta:resourcekey="lblBrandRssPageResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtBrandRssPage" disabled="disabled" />
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnBrandSettingsSave" class="sfLocale sfBtn" value="Save" />
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript">
(function($) {
    $.BrandSettingView = function(param) {
        param = $.extend
        ({
            Settings: ''
        }, param);
        var p = $.parseJSON(param.Settings);
        function aspxCommonObj() {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                CultureName: AspxCommerce.utils.GetCultureName()
            };
            return aspxCommonInfo;
        }

        var BrandSettings = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: p.BrandModulePath + "Services/AspxBrandViewServices.asmx/",
                method: "",
                url: "",
                ajaxCallMode: ""
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: BrandSettings.config.type,
                    contentType: BrandSettings.config.contentType,
                    cache: BrandSettings.config.cache,
                    async: BrandSettings.config.async,
                    url: BrandSettings.config.url,
                    data: BrandSettings.config.data,
                    dataType: BrandSettings.config.dataType,
                    success: BrandSettings.config.ajaxCallMode,
                    error: BrandSettings.config.ajaxFailure
                });
            },
            BindBrandSettings: function() {
                $("#chkEnableBrand").prop("checked", p.EnableBrand);
                $("#txtBrandCount").val(p.BrandCount);
                $("#txtAllBrandPage").val(p.BrandAllPage);
                $("#chkEnableBrandRss").prop("checked", p.EnableBrandRss);
                $("#txtBrandRssCount").val(p.BrandRssCount);
                $("#txtBrandRssPage").val(p.BrandRssPage);
            },
            BrandSettingsUpdate: function() {
                var isEnableBrand = $("#chkEnableBrand").prop("checked");
                var brandCount = $("#txtBrandCount").val();
                var brandAllPage = $("#txtAllBrandPage").val();
                var isEnableBrandRss = $("#chkEnableBrandRss").prop("checked");
                var brandRssCount = $("#txtBrandRssCount").val();
                var brandRssPage = $("#txtBrandRssPage").val();
                var settingKeys = "IsEnableBrand*BrandCount*BrandAllPage*IsEnableBrandRss*BrandRssCount*BrandRssPage";
                var settingValues = isEnableBrand + " * " + brandCount + " * " + brandAllPage + " * " + isEnableBrandRss + " * " + brandRssCount + " * " + brandRssPage;
                var param = JSON2.stringify({ SettingValues: settingValues, SettingKeys: settingKeys, aspxCommonObj: aspxCommonObj() });
                this.config.method = "BrandSettingsUpdate";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = BrandSettings.BrandSettingsSuccess;
                this.ajaxCall(this.config);
            },
            BrandSettingsSuccess: function(msg) {
                SageFrame.messaging.show(getLocale(AspxBrandView, "Setting Saved Successfully"), "Success");
            },
            init: function() {
                BrandSettings.BindBrandSettings();
                $("#btnBrandSettingsSave").click(function() {                    
                    if (v.form()) {
                        BrandSettings.BrandSettingsUpdate();
                    }
                    else {
                        SageFrame.messaging.show(getLocale(AspxBrandView, "Please fill all the required form."), "Alert");
                    }
                });
                var v = $("#form1").validate({
                    messages: {
                        BrandCount: {
                            required: '*'
                        },
                        BrandRssCount: {
                            required: '*'

                        }
                    },
                    rules:
                        {
                            BrandCount: {
                                minlength: 1,
                                maxlength: 2,
                                digits: true
                            },
                            BrandRssCount: {
                                minlength: 1,
                                maxlength: 2,
                                digits: true
                            }
                        }
                });
            }
        };
        BrandSettings.init();
    };
    $.fn.BrandSetting = function(p) {
        $.BrandSettingView(p);
    };
})(jQuery);
$(function() {
    $(".sfLocale").localize({
        moduleKey: AspxBrandView
    });
    $(this).BrandSetting({
        Settings: '<%=Settings %>'
    });
});
</script>