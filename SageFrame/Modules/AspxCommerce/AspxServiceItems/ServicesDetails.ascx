<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServicesDetails.ascx.cs" Inherits="Modules_AspxCommerce_AspxServiceItems_ServicesDetails" %>

<script type="text/javascript">
    //<![CDATA[
    var ServiceDetails = "";
    $(function () {
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        var servicekey = "<%=Servicekey%>";
        var noImageCategoryDetailPath = '<%=NoImageCategoryDetailPath %>';
        var noOfItemInRow = '<%=NoOfItemInRow %>';
        ServiceDetails = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: aspxservicePath + "AspxServiceItemsHandler.ashx/",
                method: "",
                url: "",
                ajaxCallMode: ""
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: ServiceDetails.config.type,
                    contentType: ServiceDetails.config.contentType,
                    cache: ServiceDetails.config.cache,
                    async: ServiceDetails.config.async,
                    url: ServiceDetails.config.url,
                    data: ServiceDetails.config.data,
                    dataType: ServiceDetails.config.dataType,
                    success: ServiceDetails.config.ajaxCallMode,
                    error: ServiceDetails.ajaxFailure
                });
            },

            GetServiceDetails: function () {
                this.config.method = "GetServiceDetails";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ servicekey: servicekey, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = ServiceDetails.BindServiceDetails;
                this.ajaxCall(this.config);
            },

            BindServiceDetails: function (msg) {
                var bindServiceDetails = '';
                var categoryImage = '';
                var categoryDetails = '';
                var itemId = '';
                var length = msg.d.length;
                if (length > 0) {
                    var item;
                    for (var index = 0; index < length; index++) {
                        item = msg.d[index];
                        itemId = item.ItemID;
                        categoryImage = "/Modules/AspxCommerce/AspxCategoryManagement/uploads/" + item.CategoryImagePath;
                        categoryDetails = Encoder.htmlDecode(item.CategoryDetails);
                        if ((index + 1) % eval(noOfItemInRow) == 0) {
                            bindServiceDetails += "<div class='cssClassItems cssClassProductsBoxNoMargin clearfix'>";
                        } else {
                            bindServiceDetails += "<div class='cssClassItems clearfix'>";
                        }
                        bindServiceDetails += "<h3><a href='" + aspxRootPath + "Service-Item-Details" + pageExtension + "?id=" + item.ItemID + "'><span>" + item.ItemName + "</span></a></h3>";
                        bindServiceDetails += "<p>" + Encoder.htmlDecode(item.ShortDescription) + "</p>";
                        bindServiceDetails += "<span class=\"cssClassServiceDuration\" value=" + (item.ServiceDuration) + ">" + '(' + (item.ServiceDuration) + ' ' + getLocale(DetailsBrowse, "minutes") + ')' + "</span>&nbsp;";
                        bindServiceDetails += "<span class=\"cssClassFormatCurrency\" value=" + (item.Price) + ">" + (item.Price * rate) + "</span><div class=\"cssClassClear\"></div>";
                        bindServiceDetails += '<div class="sfButtonwrapper"><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'Book-An-Appointment' + pageExtension + '?cid=' + item.CategoryID + '&pid=' + item.ItemID + '">' + getLocale(DetailsBrowse, "Book Now") + '</a></div><div class="cssClassClear"></div></div>';

                    };
                    $("#divServiceName").html('').append("<h2><span>" + servicekey + "</span></h2><div class='cssImageWrapper'><img src='" + aspxRootPath + categoryImage.replace('uploads/Small', 'uploads/Medium') + "'/></div><div class='cssServiceDesc'><p>" + categoryDetails + "</p></div>");
                    $(".cssServiceItemWrapper").html('').append(bindServiceDetails);
                }
                else {
                    $("#divServiceName").html('').append("<h2><span>" + servicekey + "</span></h2>");
                    $(".cssServiceItemWrapper").html('').append("<span class=\"cssClassNotFound\">" + getLocale(DetailsBrowse, "No Service's Products are Available!") + "</span>");
                }
            },

            fixedEncodeURIComponent: function (str) {
                return encodeURIComponent(str).replace(/!/g, '%21').replace(/'/g, '%27').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/-/g, '_').replace(/\*/g, '%2A').replace(/%26/g, 'ampersand').replace(/%20/g, '-');
            },

            init: function () {
                $(document).ready(function () {
                });
            }
        };
        ServiceDetails.init();
    });
    //]]>
</script>

<%--<div id="divServiceDetails" class="cssServiceDetail">
<div id="divServiceName" class="cssServiceName">
    <h2></h2>
</div>
<div class="cssServiceItemWrapper"></div>
</div>--%>
<asp:Literal ID="ltrServiceDetails" runat="server" EnableViewState="False"
    meta:resourcekey="ltrServiceDetailsResource1"></asp:Literal>