<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecialItemSetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxSpecialsItems_SpecialItemSetting" %>

<script type="text/javascript">
 //<![CDATA[
    (function ($) {
        $.SpecialSettingView = function (param) {
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

            var SpecialItemSetting = {
                config: {
                    isPostBack: false,
                    async: false,
                    cache: false,
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    data: '{}',
                    dataType: 'json',
                    baseURL: aspxRootPath + "Modules/AspxCommerce/AspxSpecialsItems/SpecialItemsHandler.ashx/",
                    method: "",
                    url: "",
                    ajaxCallMode: ""
                },
                ajaxCall: function (config) {
                    $.ajax({
                        type: SpecialItemSetting.config.type,
                        contentType: SpecialItemSetting.config.contentType,
                        cache: SpecialItemSetting.config.cache,
                        async: SpecialItemSetting.config.async,
                        url: SpecialItemSetting.config.url,
                        data: SpecialItemSetting.config.data,
                        dataType: SpecialItemSetting.config.dataType,
                        success: SpecialItemSetting.config.ajaxCallMode,
                        error: SpecialItemSetting.config.ajaxFailure
                    });
                },
                BindSpecialItemSetting: function () {
                    $("#chkEnableSpecialItem").prop("checked", p.IsEnableSpecialItems)
                    $("#txtSpecialItemCount").val(p.NoOfItemShown);
                    $("#txtSpecialItemInARow").val(p.NoOfItemInRow);
                    $("#chkEnableSpecialRss").prop("checked", p.IsEnableSpecialItemsRss);
                    $("#txtSpecialRssCount").val(p.SpecialItemsRssCount);
                    $("#txtSpecialRssPage").val(p.SpecialItemsRssPageName)
                    $("#txtSpecialDetailPage").val(p.SpecialItemsDetailPageName)
                },
                SpecialItemSettingUpdate: function () {

                    var isEnableSpecialItems = $("#chkEnableSpecialItem").prop("checked");
                    var noOfItemShown = $("#txtSpecialItemCount").val();
                    var noOfItemInRow = $("#txtSpecialItemInARow").val();
                    var isEnableSpecialItemsRss = $("#chkEnableSpecialRss").prop("checked");
                    var specialItemsRssCount = $("#txtSpecialRssCount").val();
                    var specialItemsRssPageName = $("#txtSpecialRssPage").val();
                    var specialItemsDetailPageName = $("#txtSpecialDetailPage").val();

                    var settingKeys = "IsEnableSpecialItems*NoOfItemShown*NoOfItemInRow*IsEnableSpecialItemsRss*SpecialItemsRssCount*SpecialItemsRssPageName*SpecialItemsDetailPageName";
                    var settingValues = isEnableSpecialItems + "*" + noOfItemShown + "*" + noOfItemInRow + "*" + isEnableSpecialItemsRss + "*" + specialItemsRssCount + "*" + specialItemsRssPageName + "*" + specialItemsDetailPageName;
                    var specialItemsSettingKeyValuePairObj = {
                        SettingKey: settingKeys,
                        SettingValue: settingValues
                    };

                    var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj(), specialObj: specialItemsSettingKeyValuePairObj });
                    this.config.method = "SaveAndUpdateSpecialSetting";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = param;
                    this.config.ajaxCallMode = SpecialItemSetting.SpecialItemSettingSuccess;
                    this.ajaxCall(this.config);
                },
                SpecialItemSettingSuccess: function (data) {                 
                    SageFrame.messaging.show(getLocale(AspxSpecials, "Setting Saved Successfully"), "Success");
                },
                init: function () {
                    SpecialItemSetting.BindSpecialItemSetting();
                    $("#btnSpecialItemSettingSave").click(function () {
                        if (v.form()) {
                            SpecialItemSetting.SpecialItemSettingUpdate();
                        }
                        else {
                            SageFrame.messaging.show(getLocale(AspxSpecials, "Please fill all the required form."), "Alert");
                        }
                        
                    });
                    var v = $("#form1").validate({
                        messages: {
                            SpecialItemCount: {
                                required: '*'
                            },
                            SpecialItemInARow: {
                                required: '*'

                            },
                            SpecialRssCount: {
                                required: '*'
                            }
                        },
                        rules:
                            {
                                SpecialItemCount: {
                                    minlength: 1,
                                    maxlength: 2,
                                    digits: true
                                },
                                SpecialItemInARow: {
                                    minlength: 1,
                                    maxlength: 2,
                                    digits: true
                                },
                                SpecialRssCount: {
                                    minlength: 1,
                                    maxlength: 2,
                                    digits: true
                                }
                            }
                    });
                }
            };
            SpecialItemSetting.init();
        };
        $.fn.SpecialSetting = function (p) {
            $.SpecialSettingView(p);
        };
    })(jQuery);
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxSpecials
        });
        $(this).SpecialSetting({
            Settings: '<%=Settings %>'
        });
    });
 //]]> 
</script>
<div class="cssSpecialItemSetting">
<h3 class="sfLocale">
         Special Item Settings
    </h3>
    <table>       
         <tr>
            <td>
                <asp:Label ID="lblEnableSpecialItem" runat="server" Text="Enable Special Item:" 
                    meta:resourcekey="lblEnableSpecialItemResource1"></asp:Label>
            </td>
            <td>
                <input type="checkbox" id="chkEnableSpecialItem" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSpecialItemCount" runat="server" 
                    Text="Enter the Number of Items Displayed:" 
                    meta:resourcekey="lblSpecialItemCountResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtSpecialItemCount" name="SpecialItemCount" class="required number" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSpecialItemInARow" runat="server" 
                    Text="Enter the Number of Items Displayed In Row:" 
                    meta:resourcekey="lblSpecialItemInARowResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtSpecialItemInARow" name="SpecialItemInARow" class="required number" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblShowSpecialRss" runat="server" Text="Enable Rss:" 
                    meta:resourcekey="lblShowSpecialRssResource1"></asp:Label>
            </td>
            <td>
                <input type="checkbox" id="chkEnableSpecialRss" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSpecialRssCount" runat="server" Text="Number of Rss To Show:" 
                    meta:resourcekey="lblSpecialRssCountResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtSpecialRssCount" name="SpecialRssCount" class="required number" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblSpecialRssPage" runat="server" 
                    Text="Special Item RssFeed Page:" meta:resourcekey="lblSpecialRssPageResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtSpecialRssPage"  disabled="disabled"/>
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblSpecialDeatailPage" runat="server" 
                    Text="Special Item Detail Page:" 
                    meta:resourcekey="lblSpecialDeatailPageResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtSpecialDetailPage" disabled="disabled" />
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnSpecialItemSettingSave" class="sfLocale sfBtn" value="Save" />
            </td>
        </tr>
    </table>
</div>
