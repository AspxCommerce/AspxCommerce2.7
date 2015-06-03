<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServiceItemDetails.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxServiceItems_ServiceItemDetails" %>

<script type="text/javascript">
    var ServiceItemDetailsApi = "";
    ; (function ($) {
        $.ServiceItemDetailsView = function (p) {
            p = $.extend
            ({
                noImageServiceItemPath: '<%=NoImageServiceItemPath %>',
            serviceModulePath: '<%=serviceModulePath %>'
        }, p);

        $(".sfLocale").localize({
            moduleKey: AspxServiceLocale
        });

        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        var ServiceItemDetails = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: serviceModulePath + "ServiceHandler.ashx/",
                method: "",
                url: "",
                ajaxCallMode: ""
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: ServiceItemDetails.config.type,
                    contentType: ServiceItemDetails.config.contentType,
                    cache: ServiceItemDetails.config.cache,
                    async: ServiceItemDetails.config.async,
                    url: ServiceItemDetails.config.url,
                    data: ServiceItemDetails.config.data,
                    dataType: ServiceItemDetails.config.dataType,
                    success: ServiceItemDetails.config.ajaxCallMode,
                    error: ServiceItemDetails.ajaxFailure
                });
            },

            GetServiceItemDetails: function () {
                var itemID = ServiceItemDetails.GetParameterByName("id");
                this.config.method = "GetServiceItemDetails";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ itemID: itemID, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = ServiceItemDetails.BindServiceItemDetails;
                this.ajaxCall(this.config);
            },
            BindServiceItemDetails: function (msg) {
                var serviceItem = '';
                var length = msg.d.length;
                if (length > 0) {
                    var item;
                    for (var index = 0; index < length; index++) {
                        item = msg.d[index];
                        serviceItem = "<div class='cssItemName'><h2><span>" + item.ItemName + "</span></h2></div>";
                        var imagePath = itemImagePath + item.ImagePath;
                        if (item.ImagePath == "") {
                            imagePath = noImageServiceItemPath;
                        }
                        serviceItem += "<div class='cssItemImage'><a href='" + aspxRootPath + imagePath + "'><img  title=\"Click To View Large Image\" alt='" + item.ItemName + "' src=" + aspxRootPath + imagePath.replace('uploads', 'uploads/Large') + "></a></div>";
                        serviceItem += "<div class='cssDesc'><p>" + Encoder.htmlDecode(item.Description) + "</p></div>";
                        serviceItem += "<span class=\"cssClassServiceDuration\" value=" + (item.ServiceDuration) + ">" + '(' + (item.ServiceDuration) + ' ' + getLocale(AspxServiceLocale, "minutes") + ')' + "</span>&nbsp;";
                        serviceItem += "<span class=\"cssClassFormatCurrency\" value=" + (item.Price) + ">" + (item.Price * rate) + "</span>";
                        serviceItem += '<div class="sfButtonwrapper"><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'Book-An-Appointment' + pageExtension + '?cid=' + item.CategoryID + '&pid=' + item.ItemID + '">' + getLocale(AspxServiceLocale, "Book Now") + '</a></div></div>';

                    };
                    $("#divServiceItemDetails").html('').append(serviceItem);
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    $(".cssItemImage a").lightBox();
                } else {
                    $("#divServiceItemDetails").html('').html("<div class='cssClassNotFound'><p>" + getLocale(AspxServiceLocale, "There is no service description available") + "</p></div>");
                }
            },
            GetParameterByName: function (name) {
                name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
                var regexS = "[\\?&]" + name + "=([^&#]*)";
                var regex = new RegExp(regexS);
                var results = regex.exec(window.location.search);
                var x = results[1].split('&');
                if (x == null)
                    return "";
                else
                    return decodeURIComponent(x[0].replace(/\+/g, " "));
            },
            Init: function () {
                $(document).ready(function () {
                    $(".cssItemImage a").lightBox();
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                });
            }
        };
        ServiceItemDetails.Init();
    };
    $.fn.ServiceItemDetails = function (p) {
        $.ServiceItemDetailsView(p);
    };
})(jQuery);
</script>

<div class="cssServiceItemWrapper">
    <asp:Literal ID="ltrServiceItemDetail" runat="server" EnableViewState="False"
        meta:resourcekey="ltrServiceItemDetailResource1"></asp:Literal>
</div>
