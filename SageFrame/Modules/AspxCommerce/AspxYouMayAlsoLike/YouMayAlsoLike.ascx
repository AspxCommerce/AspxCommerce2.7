<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YouMayAlsoLike.ascx.cs"
    Inherits="Modules_AspxYouMayAlsoLike_YouMayAlsoLike" %>

<div id="divRelatedItems" class="cssClassProductDetailInformation cssClassYouMayAlsoLike"
    style="display: none">    
    <div class="cssClassYouMayAlsoLikeWrapper clearfix" id="divYouMayAlsoLike">
        <asp:Literal ID="ltrRelatedItemInCart" runat="server" EnableViewState="false"></asp:Literal>
    </div>
</div>

<script type="text/javascript">
    //<![CDATA[
    var YouMayAlsoLike = "";
    (function ($) {
        $.YouMayAlsoLikeView = function (p) {
            p = $.extend
        ({
            allowAddToCart: '',
            relatedItemCount: 0,
            enableYouMayAlsoLike: '',
            noOfYouMayAlsoLikeInARow: 0
        }, p);
            var storeId = AspxCommerce.utils.GetStoreID();
            var portalId = AspxCommerce.utils.GetPortalID();
            var userName = AspxCommerce.utils.GetUserName();
            var cultureName = AspxCommerce.utils.GetCultureName();
            var customerId = AspxCommerce.utils.GetCustomerID();
            var ip = AspxCommerce.utils.GetClientIP();
            var countryName = AspxCommerce.utils.GetAspxClientCoutry();
            var sessionCode = AspxCommerce.utils.GetSessionCode();
            var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
            var RelatedUpSellCrossSellItems = '';
            var Imagelist = '';
            var aspxCommonObj = {
                StoreID: storeId,
                PortalID: portalId,
                UserName: userName,
                CultureName: cultureName,
                CustomerID: customerId,
                SessionCode: sessionCode
            };
            YouMayAlsoLike = {
                config: {
                    isPostBack: false,
                    async: true,
                    cache: true,
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: '{}',
                    dataType: "json",
                    baseURL: p.YouMayAlsoLikeModulePath + "YouMayLikeHandler.ashx/",
                    url: "",
                    method: "",
                    ajaxCallMode: ""
                },

                ajaxCall: function (config) {
                    $.ajax({
                        type: YouMayAlsoLike.config.type,
                        contentType: YouMayAlsoLike.config.contentType,
                        cache: YouMayAlsoLike.config.cache,
                        async: YouMayAlsoLike.config.async,
                        data: YouMayAlsoLike.config.data,
                        dataType: YouMayAlsoLike.config.dataType,
                        url: YouMayAlsoLike.config.url,
                        success: YouMayAlsoLike.config.ajaxCallMode,
                        error: YouMayAlsoLike.ajaxFailure
                    });
                },

                init: function () {
                    $("#divRelatedItems").hide();
                    if (p.enableYouMayAlsoLike.toLowerCase() == 'true' && eval(p.relatedItemCount) > 0) {
                                                $("#divRelatedItems").show();
                        $('.cssClassYouMayAlsoLikeBox a img[title]').tipsy({ gravity: 'n' });
                    }
                },
                //Send the list of images to the ImageResizer
                ResizeImageDynamically: function (Imagelist) {
                    ImageType = {
                        "Large": "Large",
                        "Medium": "Medium",
                        "Small": "Small"
                    };
                    YouMayAlsoLike.config.method = "DynamicImageResizer";
                    YouMayAlsoLike.config.url = aspxservicePath + "AspxImageResizerHandler.ashx/" + this.config.method;
                    YouMayAlsoLike.config.data = JSON2.stringify({ imgCollection: Imagelist, type: ImageType.Medium, imageCatType: "Item", aspxCommonObj: aspxCommonObj });
                    YouMayAlsoLike.config.ajaxCallMode = YouMayAlsoLike.ResizeImageSuccess;
                    YouMayAlsoLike.ajaxCall(YouMayAlsoLike.config);

                },
                ResizeImageSuccess: function () {
                },

                GetItemRetatedUpSellAndCrossSellList: function () {
                    RelatedUpSellCrossSellItems = '';
                    var url = window.location.href;
                    var itemSku = null;
                    if (url.indexOf('item')) {
                        itemSku = url.substring(url.lastIndexOf('/'));
                        itemSku = itemSku.substring(1, (itemSku.lastIndexOf('.')));
                    }
                    this.config.url = this.config.baseURL + "GetYouMayAlsoLikeItems";
                    this.config.data = JSON2.stringify({ itemSKU: itemSku, aspxCommonObj: aspxCommonObj, count: p.relatedItemCount });
                    this.config.ajaxCallMode = YouMayAlsoLike.BindRelatedItemsByCartItems;
                    this.ajaxCall(this.config);
                },            

                BindRelatedItemsByCartItems: function (msg) {
                    $.each(msg.d, function (index, value) {
                        if (value.BaseImage != "") {
                            Imagelist += value.BaseImage+';';
                        }
                    });
                        //Resize Image Dynamically
                        if (Imagelist.length > 0) {
                            YouMayAlsoLike.ResizeImageDynamically(Imagelist);
                        }
                        var length = msg.d.length;
                        if (length > 0) {
                            var item;
                            for (var index = 0; index < length; index++) {
                                item = msg.d[index];
                            var imagePath = itemImagePath + item.BaseImage;
                            if (item.BaseImage == "") {
                                imagePath = '<%=NoImageYouMayAlsoLikePath %>';
                            }
                            if (item.AlternateText == "") {
                                item.AlternateText = item.Name;
                            }
                            if ((index + 1) % eval(p.noOfYouMayAlsoLikeInARow) == 0) {
                                RelatedUpSellCrossSellItems += "<div class=\"cssClassYouMayAlsoLikeBox cssClassYouMayAlsoLikeBoxFourth\">";
                            }
                            else {
                                RelatedUpSellCrossSellItems += "<div class=\"cssClassYouMayAlsoLikeBox\">";
                            }
                            RelatedUpSellCrossSellItems += '<p class="cssClassCartPicture"><a href="' + aspxRedirectPath + 'item/' + item.SKU + pageExtension + '"><img alt="' + item.AlternateText + '" title="' + item.Name + '" src="' + aspxRootPath + imagePath.replace('uploads', 'uploads/Medium') + '"></a></p>';
                            RelatedUpSellCrossSellItems += '<p class="cssClassProductRealPrice"><span class="cssClassFormatCurrency">Price : ' + (parseFloat(item.Price)).toFixed(2) + '</span></p>';
                            if (p.allowAddToCart.toLowerCase() == 'true') {
                                if ('<%=AllowOutStockPurchase %>'.toLowerCase() == 'false') {
                                    if (item.IsOutOfStock) {
                                        RelatedUpSellCrossSellItems += "<div class='sfButtonwrapper cssClassOutOfStock'><a href='#'><span>" + getLocale(AspxYouMayAlsoLike, "Out Of Stock") + "</span></a></div>";
                                    }
                                    else {
                                        RelatedUpSellCrossSellItems += "<div class='sfButtonwrapper'><a href='#' onclick='AspxCommerce.RootFunction.AddToCartFromJS(" + item.ItemID + "," + item.Price + "," + JSON2.stringify(item.SKU) + "," + 1 + "," + item.IsCostVariantItem + "," + "this);'><span>" + getLocale(AspxYouMayAlsoLike, "Add to Cart") + "</span></a></div>";
                                    }
                                }
                                else {

                                    RelatedUpSellCrossSellItems += "<div class='sfButtonwrapper'><a href='#' onclick='AspxCommerce.RootFunction.AddToCartFromJS(" + item.ItemID + "," + item.Price + "," + JSON2.stringify(item.SKU) + "," + 1 + "," + item.IsCostVariantItem + "," + "this);'><span>" + getLocale(AspxYouMayAlsoLike, "Add to Cart") + "</span></a></div>";
                                }
                            }
                            RelatedUpSellCrossSellItems += "</div>"
                        };
                        RelatedUpSellCrossSellItems += "<div class=\"cssClassClear\"></div>";
                        $("#divYouMayAlsoLike").html(RelatedUpSellCrossSellItems);
                        $('.cssClassYouMayAlsoLikeBox a img[title]').tipsy({ gravity: 'n' });
                                            }                   
                }

            };
            YouMayAlsoLike.init();
            return {
                GetItemRetatedUpSellAndCrossSellList: YouMayAlsoLike.GetItemRetatedUpSellAndCrossSellList
            }
        };
        $.fn.YouMayAlsoLike = function (p) {
            $.YouMayAlsoLikeView(p);
        };
    })(jQuery);
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxYouMayAlsoLike
        });
        $(this).YouMayAlsoLike({
            allowAddToCart: '<%=AllowAddToCart %>',
            relatedItemCount: '<%=NoOfYouMayAlsoLikeItems%>',
            enableYouMayAlsoLike: '<%=EnableYouMayAlsoLike %>',
            noOfYouMayAlsoLikeInARow: '<%=NoOfYouMayAlsoLikeInARow %>',
            YouMayAlsoLikeModulePath: '<%=YouMayAlsoLikeModulePath %>'
        });
    });
    //]]>
</script>