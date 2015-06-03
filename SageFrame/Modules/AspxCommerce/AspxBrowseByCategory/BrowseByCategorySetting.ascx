<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BrowseByCategorySetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxBrowseByCategory_BrowseByCategorySetting" %>


<script type="text/javascript">
   $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxBrowseByCategory
        });
    }); 
    var browseByCategorySetting = '';
    $(function() {
        function aspxCommonObj() {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                CultureName: AspxCommerce.utils.GetCultureName()
            };
            return aspxCommonInfo;
        }

        browseByCategorySetting = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: aspxservicePath + "AspxCommerceWebService.asmx/",
                method: "",
                url: ""
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: browseByCategorySetting.config.type,
                    contentType: browseByCategorySetting.config.contentType,
                    cache: browseByCategorySetting.config.cache,
                    async: browseByCategorySetting.config.async,
                    url: browseByCategorySetting.config.url,
                    data: browseByCategorySetting.config.data,
                    dataType: browseByCategorySetting.config.dataType,
                    success: browseByCategorySetting.config.ajaxCallMode,
                    error: browseByCategorySetting.ajaxFailure
                });
            },
            init: function() {
                browseByCategorySetting.GetBrowseByCategorySetting();
                $("#btnBroweBySave").click(function() {                    
                    if (v.form()) {
                        browseByCategorySetting.UpdateBrowseByCategorySetting();
                    }
                    else {
                        SageFrame.messaging.show(getLocale(AspxBrowseByCategory, "Please fill all the required form."), "Alert");
                    }
                });
                var v = $("#form1").validate({
                    messages: {
                        BrowseByCount: {
                            required: '*'
                        },
                        BrowseByLevel: {
                            required: '*'

                        }
                    },
                    rules:
                        {
                            BrowseByCount: {
                                minlength: 1,
                                maxlength: 2,
                                digits: true
                            },
                            BrowseByLevel: {
                                minlength: 1,
                                maxlength: 2,
                                digits: true
                            }
                        }
                });
            },

            UpdateBrowseByCategorySetting: function() {
                var settingKey = "BrowseByCount*CategoryLevel";
                var count = $("#txtBrowseByCount").val();
                var level = $("#txtBrowseByLevel").val();
                var settingValue = count + "*" + level;
                var param = JSON2.stringify({ settingKeys: settingKey, settingValues: settingValue, aspxCommonObj: aspxCommonObj() });
                this.config.method = "UpdateBrowseByCategorySetting";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = browseByCategorySetting.UpdateSuccess;
                this.ajaxCall(this.config);
            },
            UpdateSuccess: function() {
            SageFrame.messaging.show(getLocale(AspxBrowseByCategory, "Setting Saved Successfully"), "Success");
            },
            GetBrowseByCategorySetting: function() {
                var param = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
                this.config.method = "GetBrowseByCategorySetting";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = browseByCategorySetting.BindBrowseByCategorySetting;
                this.ajaxCall(this.config);
            },
            BindBrowseByCategorySetting: function(data) {
                if (data.d.length > 0) {
                    $.each(data.d, function(index, item) {
                        $("#txtBrowseByCount").val(item.ItemCount);
                        $("#txtBrowseByLevel").val(item.CategoryLevel);
                    });
                }
            }
        };
        browseByCategorySetting.init();
    });
</script>


<div class="cssClassBrowseBySetting">
    <table>
        <tr>
            <td>
                <asp:Label runat="server" EnableViewState="False" ID="lblBrowseByCount" 
                    Text="Enter number of Category to Show" 
                    meta:resourcekey="lblBrowseByCountResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtBrowseByCount" name="BrowseByCount" class="required number" />
            </td>
        </tr>
        <tr>
            <td>
            <asp:Label runat="server" EnableViewState="False" ID="lblBrowseByLevel" 
                    Text="Enter the level" meta:resourcekey="lblBrowseByLevelResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtBrowseByLevel" name="BrowseByLevel" class="required number"/>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnBroweBySave" class="sfLocale sfBtn" value="Save"/>
            </td>
        </tr>
    </table>
</div>
