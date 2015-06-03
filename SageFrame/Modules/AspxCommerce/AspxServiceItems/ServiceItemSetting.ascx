<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServiceItemSetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxServiceItems_ServiceItemSetting" %>

<div class="cssServiceSetting">
    <table>
        <thead>
            <tr><td >
                <h2 class="sfLocale">Service Settings</h2></td></tr>
        </thead>
         <tr>
            <td>
                <asp:Label ID="lblEnableService" runat="server" Text="Enable Service Item"></asp:Label>
            </td>
            <td>
                <input type="checkbox" id="chkEnableService" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblServiceCount" runat="server" Text="Enter the Number of Products Displayed"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtServiceCount" name="ServiceCount" class="required number" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblServiceInARow" runat="server" Text="Enter the Number of Products Dispalyed In Row"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtServiceInARow" name="ServiceInARow" class="required number" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblShowServiceRss" runat="server" Text="Enable Rss"></asp:Label>
            </td>
            <td>
                <input type="checkbox" id="chkEnableServiceRss" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblServiceRssCount" runat="server" Text="Number of Rss To Show"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtServiceRssCount" name="ServiceRssCount" class="required number" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblServiceRssPage" runat="server" Text="Service Rss Page"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtServiceRssPage" disabled="disabled" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblServiceDeatailPage" runat="server" Text="Service Detail Page:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtServiceDetailPage" disabled="disabled" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblServiceItemDetailsPage" runat="server" Text="Service Item Details Page:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtServiceItemDetailsPage" disabled="disabled"/>
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblBookAnAppointmentPage" runat="server" Text="Book An Appointment Page:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtBookAnAppointmentPage" disabled="disabled" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblAppointmentSuccessPage" runat="server" Text="Appointment Success Page:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtAppointmentSuccessPage" disabled="disabled"/>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnServiceSettingSave" class="sfLocale sfBtn" value="Save" />
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
   
(function($) {
    $.ServiceSettingView = function (param) {
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
        };

        var ServiceSetting = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: aspxRootPath + "Modules/AspxCommerce/AspxServiceItems/ServiceHandler.ashx/",
                method: "",
                url: "",
                ajaxCallMode: ""
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: ServiceSetting.config.type,
                    contentType: ServiceSetting.config.contentType,
                    cache: ServiceSetting.config.cache,
                    async: ServiceSetting.config.async,
                    url: ServiceSetting.config.url,
                    data: ServiceSetting.config.data,
                    dataType: ServiceSetting.config.dataType,
                    success: ServiceSetting.config.ajaxCallMode,
                    error: ServiceSetting.config.ajaxFailure
                });
            },

            BindServiceSetting: function(data) {
                $("#chkEnableService").prop("checked", p.IsEnableService);
                $("#txtServiceCount").val(p.ServiceCategoryCount);
                $("#txtServiceInARow").val(p.ServiceCategoryInARow);
                $("#chkEnableServiceRss").prop("checked", p.IsEnableServiceRss);
                $("#txtServiceRssCount").val(p.ServiceRssCount);
                $("#txtServiceDetailPage").val(p.ServiceDetailsPage)
                $("#txtServiceItemDetailsPage").val(p.ServiceItemDetailPage);
                $("#txtBookAnAppointmentPage").val(p.BookAnAppointmentPage);
                $("#txtAppointmentSuccessPage").val(p.AppointmentSuccessPage);
                $("#txtServiceRssPage").val(p.ServiceRssPage);
            },
            ServiceSettingUpdate: function() {
                var isEnableService = $("#chkEnableService").prop("checked");
                var serviceCategoryCount = $("#txtServiceCount").val();
                var serviceCategoryInARow = $("#txtServiceInARow").val();
                var isEnableServiceRss = $("#chkEnableServiceRss").prop("checked");
                var serviceRssCount = $("#txtServiceRssCount").val();
                var serviceDetailsPage = $("#txtServiceDetailPage").val();
                var serviceItemDetailsPage = $("#txtServiceItemDetailsPage").val();
                var bookAnAppointmentPage = $("#txtBookAnAppointmentPage").val();
                var appointmentSuccessPage = $("#txtAppointmentSuccessPage").val();
                var serviceRssPage = $("#txtServiceRssPage").val();
                var settingKeys = "IsEnableService*ServiceCategoryCount*ServiceCategoryInARow*IsEnableServiceRss*ServiceRssCount*ServiceDetailsPage*ServiceItemDetailsPage*BookAnAppointmentPage*AppointmentSuccessPage*ServiceRssPage";
                var settingValues = isEnableService + " * " + serviceCategoryCount + " * " + serviceCategoryInARow + " * " + isEnableServiceRss + " * " + serviceRssCount + " * " + serviceDetailsPage + " * " + serviceItemDetailsPage + " * " + bookAnAppointmentPage + " * " + appointmentSuccessPage + " * " + serviceRssPage;
                var param = JSON2.stringify({ SettingValues: settingValues, SettingKeys: settingKeys, aspxCommonObj: aspxCommonObj() });
                this.config.method = "ServiceItemSettingUpdate";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = ServiceSetting.ServiceSettingSuccess;
                this.ajaxCall(this.config);
            },
            ServiceSettingSuccess: function(data) {
                SageFrame.messaging.show(getLocale(AspxServiceLocale, "Setting Saved Successfully"), "Success");
            },
            init: function() {
                ServiceSetting.BindServiceSetting();
                $("#btnServiceSettingSave").click(function() {                    
                    if (v.form()) {
                        ServiceSetting.ServiceSettingUpdate();
                    }
                    else {
                        SageFrame.messaging.show(getLocale(AspxServiceLocale, "Please fill all the required form."), "Alert");
                    }
                });
                var v = $("#form1").validate({
                    messages: {
                        ServiceCount: {
                            required: '*'
                        },
                        ServiceInARow: {
                            required: '*'

                        },
                        ServiceRssCount: {
                            required: '*'
                        }
                    },
                    rules:
                        {
                            ServiceCount: {
                                minlength: 1,
                                maxlength:2,
                                digits: true
                            },
                            ServiceInARow: {
                                minlength: 1,
                                maxlength: 2,
                                digits: true
                            },
                            ServiceRssCount: {
                                minlength: 1,
                                maxlength: 2,
                                digits: true
                            }
                        }
                });
            }
        };
        ServiceSetting.init();
    };
    $.fn.ServiceSetting = function(p) {
    $.ServiceSettingView(p);
    };
})(jQuery);
$(function () {
    $(".sfLocale").localize({
        moduleKey: AspxServiceLocale
    });
    $(this).ServiceSetting({
        Settings: '<%=Settings %>'
    });
});
</script>

