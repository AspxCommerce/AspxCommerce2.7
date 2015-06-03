<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GiftCardsAll.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxGiftCard_GiftCardsAll" %>

<script type="text/javascript">
    //<![CDATA[    
    var GiftCardsAll = "";
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxGiftCard
        });
        var noOfItemsInARow = '<%=NoOfItemsInARow%>';
        var allowAddToCart = '<%=AllowAddToCart %>';


        var allowOutStockPurchase = '<%=AllowOutStockPurchase %>';
        var defaultImagePath = '<%=DefaultImagePath %>';
        var rowTotal = '<%=rowTotal %>';
        var currentPage = 0;
        var ip = AspxCommerce.utils.GetClientIP();
        var countryName = AspxCommerce.utils.GetAspxClientCoutry();
        var aspxCommonObj = function () {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                UserName: AspxCommerce.utils.GetUserName(),
                CultureName: AspxCommerce.utils.GetCultureName(),
                CustomerID: AspxCommerce.utils.GetCustomerID()
            };
            return aspxCommonInfo;
        };
        GiftCardsAll = {
            config: {
                isPostBack: false,
                async: true,
                cache: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: aspxservicePath,
                method: "",
                url: "",
                ajaxCallMode: "",
                itemid: 0
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: GiftCardsAll.config.type,
                    contentType: GiftCardsAll.config.contentType,
                    cache: GiftCardsAll.config.cache,
                    async: GiftCardsAll.config.async,
                    url: GiftCardsAll.config.url,
                    data: GiftCardsAll.config.data,
                    dataType: GiftCardsAll.config.dataType,
                    success: GiftCardsAll.config.ajaxCallMode,
                    error: GiftCardsAll.ajaxFailure
                });
            },
            GetAllGiftCards: function (offset, limit, currentPage) {
                GiftCardsAll.config.method = "AspxCoreHandler.ashx/GetAllGiftCards";
                GiftCardsAll.config.url = GiftCardsAll.config.baseURL + GiftCardsAll.config.method;
                GiftCardsAll.config.data = JSON2.stringify({ offset: offset, limit: limit, rowTotal: rowTotal, aspxCommonObj: aspxCommonObj() });
                GiftCardsAll.config.ajaxCallMode = GiftCardsAll.BindAllGiftCards;
                GiftCardsAll.ajaxCall(this.config);
            },
            BindAllGiftCards: function (msg) {
                var html = '';
                var length = msg.d.length;
                if (length > 0) {
                    var item;
                    for (var index = 0; index < length; index++) {
                        item = msg.d[index];
                        var imagePath = itemImagePath + item.ImagePath;
                        var altImagePath = itemImagePath + item.AlternateImagePath;
                        if (item.ImagePath == "") {
                            imagePath = defaultImagePath;
                        }
                        if (item.AlternateText == "") {
                            item.AlternateText = item.Name;
                        }
                        if (item.AlternateImagePath == "") {
                            altImagePath = item.ImagePath;
                        }
                        if ((index + 1) % eval(noOfItemsInARow) == 0) {
                            html += "<div class=\"cssClassProductsBox cssClassProductsBoxNoMargin\">";
                        } else {
                            html += "<div class=\"cssClassProductsBox\">";
                        }
                        var hrefItem = aspxRedirectPath + "item/" + fixedEncodeURIComponent(item.SKU) + pageExtension;
                        var name = '';
                        if (item.Name.length > 50) {
                            name = item.Name.substring(0, 50);
                            var i = 0;
                            i = name.lastIndexOf(' ');
                            name = name.substring(0, i);
                            name = name + "...";
                        } else {
                            name = item.Name;
                        }
                        html += '<div id="productImageWrapID_' + item.ItemID + '" class="cssClassProductsBoxInfo" costvariantItem=' + item.IsCostVariantItem + '  itemid="' + item.ItemID + '" title="' + item.Name + '"><h2>' + name + '</h2><h3>' + item.SKU + '</h3><div id="divitemImage" class="cssClassProductPicture"><a href="' + hrefItem + '" ><img id="' + item.ItemID + '"  alt="' + item.AlternateText + '"  title="' + item.AlternateText + '" data-original="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/loader_100x12.gif" src="' + aspxRootPath + imagePath.replace('uploads', 'uploads/Medium') + '"></a></div>';

                        if (!item.HidePrice) {
                            if (item.ListPrice != null) {
                                html += "<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\"><p class=\"cssClassProductOffPrice\"><span class=\"cssClassFormatCurrency\" value=" + (item.ListPrice).toFixed(2) + ">" + (item.ListPrice * rate).toFixed(2) + "</span></p><p class=\"cssClassProductRealPrice \" ><span class=\"cssClassFormatCurrency\" value=" + (item.Price).toFixed(2) + ">" + (item.Price * rate).toFixed(2) + "</span></p></div></div>";
                            } else {
                                html += "<div class=\"cssClassProductPriceBox\"><div class=\"cssClassProductPrice\"><p class=\"cssClassProductRealPrice \" ><span class=\"cssClassFormatCurrency\" value=" + (item.Price).toFixed(2) + ">" + (item.Price * rate).toFixed(2) + "</span></p></div></div>";
                            }
                        } else {
                            html += "<div class=\"cssClassProductPriceBox\"></div>";
                        }
                        html += '<div class="cssClassProductDetail"><p><a href="' + aspxRedirectPath + 'item/' + item.SKU + pageExtension + '">' + getLocale(AspxGiftCard, "Details") + '</a></p></div>';
                        var itemSKU = JSON2.stringify(item.SKU);
                        var itemName = JSON2.stringify(item.Name);
                        html += "<div class=\"sfButtonwrapper\">";
                        html += "<div class=\"cssClassWishListButton\"><input type=\"hidden\" name=\"itemwish\"  value='" + item.ItemID + ',' + JSON2.stringify(item.SKU) + ",this' /></div>";
                        html += "<div class=\"cssClassCompareButton\"><input type=\"hidden\" name=\"itemcompare\"  value='" + item.ItemID + ',' + JSON2.stringify(item.SKU) + ",this' /></div>";
                        html += "</div>";
                        html += "<div class=\"cssClassclear\"></div></div>";
                        if (allowAddToCart.toLowerCase() == 'true') {
                            if (allowOutStockPurchase.toLowerCase() == 'false') {
                                if (item.IsOutOfStock) {
                                    html += "<div class=\"cssClassAddtoCard\"><div class=\"sfButtonwrapper cssClassOutOfStock\"><button type=\"button\"><span>" + getLocale(AspxGiftCard, "Out Of Stock") + "</span></button></div></div>";
                                } else {
                                    html += "<div class=\"cssClassAddToCart\"><div class=\"sfButtonwrapper \"><button type=\"button\" class=\"addtoCart sfBtn\" data-addtocart=\"addtocart" + item.ItemID + "\" onclick='AspxCommerce.RootFunction.AddToCartFromJS(" + item.ItemID + "," + (item.Price).toFixed(2) + "," + JSON2.stringify(item.SKU) + "," + 1 + "," + "\"" + item.IsCostVariantItem + "\"" + "," + "this);'><span>" + getLocale(AspxGiftCard, "Add to cart") + "</span></button></div></div>";
                                }

                            } else {
                                html += "<div class=\"cssClassAddToCart\"><div class=\"sfButtonwrapper \"><button type=\"button\" class=\"addtoCart sfBtn\" data-addtocart=\"addtocart" + item.ItemID + "\" onclick='AspxCommerce.RootFunction.AddToCartFromJS(" + item.ItemID + "," + (item.Price).toFixed(2) + "," + JSON2.stringify(item.SKU) + "," + 1 + "," + "\"" + item.IsCostVariantItem + "\"" + "," + "this);'><span>" + getLocale(AspxGiftCard, "Add to cart") + "</span></button></div></div>";
                            }
                        }

                        html += "</div>";
                    };

                } else {
                    html += "<span class=\"cssClassNotFound\">" + getLocale(AspxGiftCard, "This store has no items listed yet!") + "</span>";
                }
                $("#" + '<%=divGiftCards.ClientID %>').html(html);
                $('#divitemImage a img[title]').tipsy({ gravity: 'n' });
            },
            init: function () {
                if (rowTotal > 0) { $("#divPageNumber").show(); }
                $('#divitemImage a img[title]').tipsy({ gravity: 'n' });
                var items_per_page = $('#ddlPageSize').val();;
                $('#Pagination').pagination(rowTotal, {
                    items_per_page: items_per_page,
                    current_page: currentPage,
                    callfunction: true,
                    function_name: { name: GiftCardsAll.GetAllGiftCards, limit: $('#ddlPageSize').val() },
                    prev_text: getLocale(AspxGiftCard, "Prev"),
                    next_text: getLocale(AspxGiftCard, "Next"),
                    prev_show_always: false,
                    next_show_always: false
                });
                $("#ddlPageSize").bind("change", function () {
                    var items_per_page = $(this).val();
                    var offset = 1;
                    GiftCardsAll.GetAllGiftCards(offset, items_per_page, 0);
                    $('#Pagination').pagination(rowTotal, {
                        items_per_page: items_per_page,
                        current_page: currentPage,
                        callfunction: true,
                        function_name: { name: GiftCardsAll.GetAllGiftCards, limit: $('#ddlPageSize').val() },
                        prev_text: getLocale(AspxGiftCard, "Prev"),
                        next_text: getLocale(AspxGiftCard, "Next"),
                        prev_show_always: false,
                        next_show_always: false
                    });
                });
            }
        };
        GiftCardsAll.init();
    });
    //]]>  
</script>

<div class="cssGiftCardContainer">
    <h2 class="cssClassMiddleHeader">
        <span class="sfLocale">Gift Cards</span></h2>
    <div id="divGiftCards" runat="server" enableviewstate="false">
    </div>
    <div class="cssClassClear">
    </div>
    <div class="cssClassPageNumber" id="divPageNumber" style="display: none;">
        <div class="cssClassPageNumberLeftBg">
            <div class="cssClassPageNumberRightBg">
                <div class="cssClassPageNumberMidBg">
                    <div id="Pagination">
                    </div>
                    <div class="cssClassViewPerPage">
                        <span class="sfLocale">View Per Page</span>
                        <div class="cssClassDrop">
                            <select id="ddlPageSize" class="cssClassDropDown">
                                <option value="8">8</option>
                                <option value="16">16</option>
                                <option value="40">40</option>
                                <option value="80">80</option>
                                <option value="120">120</option>
                            </select>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
