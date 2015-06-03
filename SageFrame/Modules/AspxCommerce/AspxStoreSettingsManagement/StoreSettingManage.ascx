<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreSettingManage.ascx.cs"
    Inherits="Modules_AspxStoreSettings_StoreSettingManage" %>

<script type="text/javascript">
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxStoreSettingsManagement
        });
    });
    //<![CDATA[
    var ddlMyAccountURL = "<%=ddlMyAccountURL.ClientID %>";
    var ddlCatMgntPageURL = "<%=ddlCatMgntPageURL %>";
    var ddlItemMgntPageURL = "<%=ddlItemMgntPageURL %>";
    var ddlShoppingCartURL = "<%=ddlShoppingCartURL.ClientID%>";    
    var maxFilesize = '<%=MaxFileSize %>';
    var ddlDetailsPageURL = "<%=ddlDetailsPageURL.ClientID%>";
    var ddlItemDetailURL = "<%=ddlItemDetailURL.ClientID%>";
    var ddlCategoryDetailURL = "<%=ddlCategoryDetailURL.ClientID%>";   
    var ddlSingleCheckOutURL = "<%=ddlSingleCheckOutURL.ClientID %>";
    var ddlMultiCheckOutURL = "<%=ddlMultiCheckOutURL.ClientID %>";   
    var ddlStoreLocatorURL = "<%=ddlStoreLocatorURL.ClientID %>";    
    var ddlRssFeedURL = "<%=ddlRssFeedURL.ClientID %>"; 
    var ddlTrackPackageUrl = "<%=ddlTrackPackageUrl.ClientID %>";
    var ddlShippingDetailPageURL = "<%=ddlShippingDetailPageURL.ClientID %>";
    var ddlItemMgntPageURL = "<%=ddlItemMgntPageURL.ClientID %>";
    var ddlCatMgntPageURL = "<%=ddlCatMgntPageURL.ClientID %>";   
    var umi = '<%=UserModuleID%>';
    //]]>
</script>

<div id="divStoreSettings">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblHeading" runat="server" Text="Store Settings" meta:resourcekey="lblHeadingResource1"></asp:Label>
            </h1>
        </div>
        <div class="cssClassTabPanelTable">
            <div id="container-7">
                <ul>
                    <li><a href="#storefragment-1">
                        <asp:Label ID="lblTabTitle1" runat="server" Text="Standard" meta:resourcekey="lblTabTitle1Resource1"></asp:Label>
                    </a></li>
                    <li><a href="#storefragment-2">
                        <asp:Label ID="lblTabTitle2" runat="server" Text="General" meta:resourcekey="lblTabTitle2Resource1"></asp:Label>
                    </a></li>
                    <li><a href="#storefragment-3">
                        <asp:Label ID="lbTabTitle3" runat="server" Text="Email" meta:resourcekey="lbTabTitle3Resource1"></asp:Label>
                    </a></li>
                    <li><a href="#storefragment-4">
                        <asp:Label ID="lbTabTitle4" runat="server" Text="Display" meta:resourcekey="lbTabTitle4Resource1"></asp:Label>
                    </a></li>
                    <li><a href="#storefragment-5">
                        <asp:Label ID="lbTabTitle5" runat="server" Text="Media" meta:resourcekey="lbTabTitle5Resource1"></asp:Label>
                    </a></li>
                    <li><a href="#storefragment-8">
                        <asp:Label ID="lbTabTitle9" runat="server" Text="Shipping" meta:resourcekey="lbTabTitle9Resource2"></asp:Label>
                    </a></li>
                    <li><a href="#storefragment-usercart">
                        <asp:Label ID="Label7" runat="server" Text="Users/Cart" meta:resourcekey="Label7Resource1"></asp:Label>
                    </a></li>
                    <li><a href="#storefragment-RssFeed">
                        <asp:Label ID="lblRssFeedTab" runat="server" Text="Rss Feed" meta:resourcekey="lblRssFeedTabResource1"></asp:Label>
                    </a></li>
                    <li><a href="#storefragment-7">
                        <asp:Label ID="lbTabTitle8" runat="server" Text="Other" meta:resourcekey="lbTabTitle8Resource1"></asp:Label>
                    </a></li>
                </ul>
                <div id="storefragment-1">
                    <div class="sfFormwrapper">
                        <h2>
                            <asp:Label ID="lblTab1Info" runat="server" Text="Standard Settings" meta:resourcekey="lblTab1InfoResource1"></asp:Label>
                        </h2>
                        <table border="0" id="tblStandardSettingsForm">
                            <tr>
                                <td>
                                    <asp:Label ID="lblDefaultImageProductURL" Text="Default Image Product URL:" runat="server"
                                        CssClass="sfFormlabel" meta:resourcekey="lblDefaultImageProductURLResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input id="fupDefaultImageURL" type="file" class="cssClassBrowse" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div class="progress ui-helper-clearfix">
                                        <div class="progressBar" id="progressBar">
                                        </div>
                                        <div class="percentage">
                                        </div>
                                    </div>
                                    <div id="defaultProductImage">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMyAccountURL" runat="server" Text="My Account URL:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblMyAccountURLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMyAccountURL" runat="server" class="sfListmenu" meta:resourcekey="ddlMyAccountURLResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td>
                                    <asp:Label ID="lblCategoryMgntPageURL" runat="server" Text="My Category Management Page URL:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblCategoryMgntPageURLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCatMgntPageURL" class="sfListmenu" runat="server" 
                                        meta:resourcekey="ddlCatMgntPageURLResource1" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td>
                                    <asp:Label ID="lblItemMgntPageURL" runat="server" Text="My Item Management Page URL:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblItemMgntPageURLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlItemMgntPageURL" class="sfListmenu" runat="server" 
                                        meta:resourcekey="ddlItemMgntPageURLResource1" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShoppingCartURL" runat="server" Text="Shopping Cart URL:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblShoppingCartURLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlShoppingCartURL" class="sfListmenu" runat="server" meta:resourcekey="ddlShoppingCartURLResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>                           
                            <tr>
                                <td>
                                    <asp:Label ID="lblDetailPageURL" runat="server" Text="Details Page URL:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblDetailPageURLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDetailsPageURL" class="sfListmenu" runat="server" meta:resourcekey="ddlDetailsPageURLResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemDetailURL" runat="server" Text="Item Details Page URL:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblItemDetailURLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlItemDetailURL" class="sfListmenu" runat="server" meta:resourcekey="ddlItemDetailURLResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCategoryDetailURL" runat="server" Text="Category Details Page URL:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblCategoryDetailURLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCategoryDetailURL" class="sfListmenu" runat="server" meta:resourcekey="ddlCategoryDetailURLResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <asp:Label ID="lblSingleCheckOutURL" runat="server" Text="Single Address CheckOut URL:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblSingleCheckOutURLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSingleCheckOutURL" class="sfListmenu" runat="server" meta:resourcekey="ddlSingleCheckOutURLResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMultiCheckOutURL" runat="server" Text="Multiple Address CheckOut URL:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblMultiCheckOutURLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMultiCheckOutURL" runat="server" class="sfListmenu" meta:resourcekey="ddlMultiCheckOutURLResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <asp:Label ID="lblStoreLocatorURL" runat="server" Text="Store Locator URL:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblStoreLocatorURLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStoreLocatorURL" class="sfListmenu" runat="server" meta:resourcekey="ddlStoreLocatorURLResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>           
                            <tr>
                                <td>
                                    <asp:Label ID="lblTrackPackageUrl" runat="server" Text="Trace Package Page URL:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblTrackPackageUrlResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTrackPackageUrl" class="sfListmenu" runat="server" meta:resourcekey="ddlTrackPackageUrlResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShippingDetailPageURL" runat="server" Text="Shipping Detail Page URL:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblShippingDetailPageURLResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlShippingDetailPageURL" class="sfListmenu" runat="server"
                                        meta:resourcekey="ddlShippingDetailPageURLResource1">
                                    </asp:DropDownList>
                                </td>
                            </tr>                           
                        </table>
                    </div>
                </div>
                <div id="storefragment-2">
                    <div class="sfFormwrapper">
                        <h2>
                            <asp:Label ID="lblTab2Info" runat="server" Text="General Settings" meta:resourcekey="lblTab2InfoResource1"></asp:Label>
                        </h2>
                        <table id="tblGeneralSettingForm">
                            <tr>
                                <td>
                                    <asp:Label ID="lblStoreLogo" Text="Store Logo:" runat="server" CssClass="sfFormlabel"
                                        meta:resourcekey="lblStoreLogoResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input id="fupStoreLogo" type="file" class="cssClassBrowse" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div class="progress ui-helper-clearfix">
                                        <div class="progressBar">
                                        </div>
                                        <div class="percentage">
                                        </div>
                                    </div>
                                    <div id="divStoreLogo">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblStoreName" runat="server" Text="Store Name: " CssClass="sfFormlabel"
                                        meta:resourcekey="lblStoreNameResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtStoreName" name="StoreName" class="sfInputbox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMainCurrency" runat="server" Text="Main Currency:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblMainCurrencyResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <select id="ddlCurrency" class="sfListmenu">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblRealTimeManagement" runat="server" Text="Allow Real Time Notifications:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblRealTimeNotificationsResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkRealTimeNotifications" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblWeightUnit" runat="server" Text="Weight Unit:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblWeightUnitResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtWeightUnit" name="Weight" class="sfInputbox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDimensionUnit" runat="server" Text="Dimension Unit:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblDimensionUnitResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtDimensionUnit" name="Dimension" class="sfInputbox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblLowStockQuantity" runat="server" Text="Low Stock Quantity:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblLowStockQuantityResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtLowStockQuantity" name="LowStockQuantity" datatype="Integer"
                                        class="sfInputbox required" />
                                </td>
                            </tr>
                              <tr>
                                <td>
                                    <asp:Label ID="lblOutOfStockQuantity" runat="server" 
                                        Text="Out Of Stock Quantity:" CssClass="sfFormlabel" 
                                        meta:resourcekey="lblOutOfStockQuantityResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtOutOfStockQuantity" name="OutOfStockQuantity" datatype="Integer"
                                        class="sfInputbox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShoppingOptionRange" runat="server" Text="Shopping Option Range:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblShoppingOptionRangeResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtShoppingOptionRange" name="ShoppingOptionRange" datatype="Integer"
                                        class="sfInputbox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEstimateShippingCostInCartPage" runat="server" Text="Estimate Shipping Cost In Cart Page:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblEstimateShippingCostInCartPageResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkEstimateShippingCostInCartPage" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCartAbandonTime" Text="Cart Abandon Time(In Hours):" runat="server"
                                        CssClass="sfFormlabel" meta:resourcekey="lblCartAbandonTimeResource1"></asp:Label>
                                </td>
                                <td>
                                    <input type="text" id="txtCartAbandonTime" name="CartAbandonTime" datatype="Integer"
                                        class="sfInputbox required number">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTimeToDeleteAbandCart" Text="Abandoned Carts Deletion Time(In Hours):"
                                        runat="server" CssClass="sfFormlabel" meta:resourcekey="lblTimeToDeleteAbandCartResource1"></asp:Label>
                                </td>
                                <td>
                                    <input type="text" id="txtTimeToDeleteAbandCart" name="TimeTodeleteAbandCart" datatype="Integer"
                                        class="sfInputbox required number">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowAnonymousCheckOut" Text="Allow Anonymous Checkout:" runat="server"
                                        CssClass="sfFormlabel" meta:resourcekey="lblAllowAnonymousCheckOutResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAllowAnonymousCheckout" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowOutStockPurchase" runat="server" Text="Allow purchases when out of stock:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblAllowOutStockPurchaseResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAllowOutStockPurchase" disabled="disabled" readonly="readonly" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAskCustomerToSubscribe" runat="server" Text="Ask Customer To Subscribe:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblAskCustomerToSubscribeResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAskCustomerToSubscribe" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowMultipleShippingAddress" runat="server" Text="Allow Multiple Shipping Address:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblAllowMultipleShippingAddressResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAllowMultipleShippingAddress" disabled="disabled" readonly="readonly" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="storefragment-3">
                    <div class="sfFormwrapper">
                        <h2>
                            <asp:Label ID="lblEmailSettingForm" runat="server" Text="Email Settings" meta:resourcekey="lblEmailSettingFormResource1"></asp:Label>
                        </h2>
                        <table id="tblEmailSettingForm">
                            <tr>
                                <td>
                                    <asp:Label ID="lblSendEmailsFrom" runat="server" Text="Send E-Commerce Emails From:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblSendEmailsFromResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtSendEmailsFrom" name="EmailFrom" class="sfInputbox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSendOrderNotification" runat="server" Text="Send Order Notification:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblSendOrderNotificationResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkSendOrderNotification" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="storefragment-4">
                    <div class="sfFormwrapper">
                        <h2>
                            <asp:Label ID="lblSEODisplay" runat="server" Text="Display Settings" meta:resourcekey="lblSEODisplayResource1"></asp:Label>
                        </h2>
                        <table id="tblSEODisplayForm">
                            <tr>
                                <td>
                                    <asp:Label ID="lblModuleCollapsible" runat="server" Text="Module Collapsible:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblModuleCollapsibleResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkModuleCollapsible" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShowAddToCartButton" runat="server" Text="Show Add To Cart Button:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblShowAddToCartButtonResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkShowAddToCartButton" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAddToCartButtonText" runat="server" Text="Add To Cart Button Text:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblAddToCartButtonTextResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtAddToCartButtonText" name="AddToCartButtonText" class="sfInputbox required"
                                        disabled="disabled" readonly="readonly" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemDisplayMode" runat="server" Text="Item Display Mode:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblItemDisplayModeResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <select id="ddlItemDisplayMode" class="sfListmenu">
                                        <option value="Dropdown" class="sfLocale">Dropdown</option>
                                        <option value="Icon" class="sfLocale">Icon</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblViewAsOptions" runat="server" Text="View As Options:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblViewAsOptionsResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <select id="ddlViewAsOptions" multiple="multiple" class="sfListmenu">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblViewAsOptionsDefault" runat="server" Text="View As Options Default:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblViewAsOptionsDefaultResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <select id="ddlViewAsOptionsDefault" class="sfListmenu">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSortByOptions" runat="server" Text="Sort By Options:" CssClass="sfFormlabel"
                                        meta:resourcekey="lblSortByOptionsResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <select id="ddlSortByOptions" multiple="multiple" class="sfListmenu">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSortByOptionsDefault" runat="server" Text="Sort By Options Default:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblSortByOptionsDefaultResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <select id="ddlSortByOptionsDefault" class="sfListmenu">
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="storefragment-5">
                    <div class="sfFormwrapper">
                        <h2>
                            <asp:Label ID="lblMediaSetting" runat="server" Text="Media Settings" meta:resourcekey="lblMediaSettingResource1"></asp:Label>
                        </h2>
                        <table id="tblMediaSettingForm">

                            <tr>
                                <td>
                                    <asp:Label ID="lblMaximumImageSize" Text="Maximum Uploaded Image/File Size:" runat="server"
                                        CssClass="sfFormlabel" meta:resourcekey="lblMaximumImageSizeResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtMaximumImageSize" datatype="Integer" name="MaximumImageSize"
                                        class="sfInputbox required" />
                                    <b>KB</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMaxDownloadFileSize" Text="Maximum Download File Size:" runat="server"
                                        CssClass="sfFormlabel" meta:resourcekey="lblMaxDownloadFileSizeResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtMaxDownloadFileSize" datatype="Integer" name="MaximumDownloadSize"
                                        class="sfInputbox required" />
                                    <b>KB</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblImageResizeProportionally" runat="server" Text="Resize Images Proportionally:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblResizeImagesProportionallyResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkResizeProportionally" /><asp:label ID="lblImageResizeHelp" runat="server" Text="(Note: Item Images are resized to their width upon checked)"
                                         CssClass="sfFormlabel" meta:resourcekey="lblImageResizeHelpResource1"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblItemLargeThumbImageHeight" Text="Item Large Thumbnail Image Height:"
                                        runat="server" CssClass="sfFormlabel" 
                                        meta:resourcekey="lblItemLargeThumbImageHeightResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtItemLargeThumbnailImageHeight" name="ItemLargeThumbnailImageHeight"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemLargeThumbImageWidth" Text="Item Large Thumbnail Image Width:"
                                        runat="server" CssClass="sfFormlabel" 
                                        meta:resourcekey="lblItemLargeThumbImageWidthResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtItemLargeThumbnailImageWidth" name="ItemLargeThumbnailImageWidth"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemMediumThumbnailImageHeight" Text="Item Medium Thumbnail Image Height:"
                                        runat="server" CssClass="sfFormlabel" 
                                        meta:resourcekey="lblItemMediumThumbnailImageHeightResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtItemMediumThumbnailImageHeight" name="ItemMediumThumbnailImageHeight"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemMediumThumbnailImageWidth" Text="Item Medium Thumbnail Image Width:"
                                        runat="server" CssClass="sfFormlabel" 
                                        meta:resourcekey="lblItemMediumThumbnailImageWidthResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtItemMediumThumbnailImageWidth" name="ItemMediumThumbnailImageWidth"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemSmallThumbnailImageHeight" runat="server" Text="Item Small Thumbnail Image Height:"
                                        CssClass="sfFormlabel" 
                                        meta:resourcekey="lblItemSmallThumbnailImageHeightResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtItemSmallThumbnailImageHeight" name="ItemSmallThumbnailImageHeight"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemSmallThumbnailImageWidth" runat="server" Text="Item Small Thumbnail Image Width:"
                                        CssClass="sfFormlabel" 
                                        meta:resourcekey="lblItemSmallThumbnailImageWidthResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtItemSmallThumbnailImageWidth" name="ItemSmallThumbnailImageWidth"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemImageMaxWidth" runat="server" Text="Maximum Width Of Item Image:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblIItemImageMaxWidthResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtItemImageMaxWidth" name="ItemImageMaxWidth" datatype="Integer"
                                        class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemImageMaxHeight" runat="server" Text="Maximum Height Of Item Image:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblItemImageMaxHeightResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtItemImageMaxHeight" name="ItemImageMaxHeight" datatype="Integer"
                                        class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCategoryLargeThumbnailImageHeight" runat="server" Text="Category Large Thumbnail Image Height:"
                                        CssClass="sfFormlabel" 
                                        meta:resourcekey="lblCategoryLargeThumbnailImageHeightResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtCategoryLargeThumbnailImageHeight" name="CategoryLargeThumbnailImageHEight"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblCategoryLargeThumbnailImageWidth" runat="server" Text="Category Large Thumbnail Image Width:"
                                        CssClass="sfFormlabel" 
                                        meta:resourcekey="lblCategoryLargeThumbnailImageWidthResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtCategoryLargeThumbnailImageWidth" name="CategoryLargeThumbnailImageWidth"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCategoryMediumThumbnailImageHeight" runat="server" Text="Category Medium Thumbnail Image Height:"
                                        CssClass="sfFormlabel" 
                                        meta:resourcekey="lblCategoryMediumThumbnailImageHeightResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtCategoryMediumThumbnailImageHeight" name="CategoryMediumThumbnailImageHeight"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCategoryMediumThumbnailImageWidth" runat="server" Text="Category Medium Thumbnail Image Width:"
                                        CssClass="sfFormlabel" 
                                        meta:resourcekey="lblCategoryMediumThumbnailImageWidthResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtCategoryMediumThumbnailImageWidth" name="CategoryMediumThumbnailImageWidth"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCategorySmallThumbnailImageHeight" runat="server" Text="Category Small Thumbnail Image Height:"
                                        CssClass="sfFormlabel" 
                                        meta:resourcekey="lblCategorySmallThumbnailImageHeightResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtCategorySmallThumbnailImageHeight" name="CategorySmallThumbnailImageHeight"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCategorySmallThumbnailImageWidth" runat="server" Text="Category Small Thumbnail Image Width:"
                                        CssClass="sfFormlabel" 
                                        meta:resourcekey="lblCategorySmallThumbnailImageWidthResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtCategorySmallThumbnailImageWidth" name="CategorySmallThumbnailImageWidth"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCategoryBannerImageWidth" runat="server" Text="Category Banner Image Width:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblCategoryBannerImageWidthResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtCategoryBannerImageWidth" name="CategoryBannerImageWidth"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCategoryBannerImageHeight" runat="server" Text="Category Banner Image Height:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblCategoryBannerImageHeightResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtCategoryBannerImageHeight" name="CategoryBannerImageHeight"
                                        datatype="Integer" class="sfInputbox required" />
                                    <b>px</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Watermark Text:" CssClass="sfFormlabel"
                                        meta:resourcekey="Label4Resource1"></asp:Label>
                                </td>
                                <td>
                                    <input type="text" id="txtWaterMark" class="sfInputbox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Watermark Position:" CssClass="sfFormlabel"
                                        meta:resourcekey="Label5Resource1"></asp:Label>
                                </td>
                                <td>
                                    <div>
                                        <label>
                                            <input type="radio" name="watermarkposition" value="TOP_CENTER" />top center</label>
                                        <label>
                                            <input type="radio" name="watermarkposition" value="MID_CENTER" />mid center</label>
                                        <label>
                                            <input type="radio" name="watermarkposition" value="BOTTOM_CENTER" />bottom center</label>
                                        <label>
                                            <input type="radio" name="watermarkposition" value="TOP_LEFT" />top left</label>
                                        <label>
                                            <input type="radio" name="watermarkposition" value="MID_LEFT" />mid left</label>
                                        <label>
                                            <input type="radio" name="watermarkposition" value="BOTTOM_LEFT" />bottom left</label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Watermark Text Rotate:" CssClass="sfFormlabel"
                                        meta:resourcekey="Label6Resource1"></asp:Label>
                                </td>
                                <td>
                                    <input type="text" id="txtWaterMarkRotationAngle" value="0.00" class="sfInputbox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Watermark Image Position:" CssClass="sfFormlabel"
                                        meta:resourcekey="Label8Resource1"></asp:Label>
                                </td>
                                <td>
                                    <div>
                                        <label>
                                            <input type="radio" name="watermarkImageposition" value="TOP_LEFT" />top left</label>
                                        <label>
                                            <input type="radio" name="watermarkImageposition" value="TOP_RIGHT" />top right</label>
                                        <label>
                                            <input type="radio" name="watermarkImageposition" value="CENTER" />mid center</label>
                                        <label>
                                            <input type="radio" name="watermarkImageposition" value="BOTTOM_LEFT" />bottom left</label>
                                        <label>
                                            <input type="radio" name="watermarkImageposition" value="BOTTOM_RIGHT" />bottom
                                            right</label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Watermark Image Rotate:" CssClass="sfFormlabel"
                                        meta:resourcekey="Label9Resource1"></asp:Label>
                                </td>
                                <td>
                                    <input type="text" id="txtWaterMarkImageRotation" value="0.00" class="sfInputbox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Apply Watermark Image:" CssClass="sfFormlabel"
                                        meta:resourcekey="Label3Resource1"></asp:Label>
                                </td>
                                <td>
                                    <input type="checkbox" name="showWaterMarkImage" class="sfCheckbox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShowItemImagesInCart" runat="server" Text="Show Item Images in Cart:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblShowItemImagesInCartResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkShowItemImagesInCart" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShowItemImagesInWishList" runat="server" Text="Show Item Images in WishList:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblShowItemImagesInWishListResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkShowItemImagesInWishList" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="storefragment-usercart">
                    <div class="sfFormwrapper">
                        <h2>
                            <asp:Label ID="Label10" runat="server" Text="User/Cart Settings" meta:resourcekey="lblTitleUserCartsResource1"></asp:Label>
                        </h2>
                        <table id="Table3" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowMultipleAddress" runat="server" Text="Allow Users To Create Multiple Addresses:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblAllowMultipleAddressResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAllowMultipleAddress" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowShippingEstimate" runat="server" Text="Show Shipping Rate Estimate in Cart Page:"
                                        CssClass="sfFormlabel" 
                                        meta:resourcekey="lblAllowShippingEstimateResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAllowShippingEstimate" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowCouponDiscount" runat="server" Text="Show Coupon Discount in Cart Page:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblAllowCouponDiscountResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAllowCouponDiscount" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMinimumCartSubTotal" runat="server" Text="Minimum Cart Sub Total Amount:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblMinimumCartSubTotalResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtMinimumCartSubTotalAmount" name="MinimumOrderAmount" datatype="Integer"
                                        class="sfInputbox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAdditionalCVR" runat="server" Text="Currency Conversion Surcharges:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblAdditionalCVRResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtAdditionalCVR" name="AdditionalCVR" datatype="Integer"
                                        class="sfInputbox DigitDecimalAndNegative" />
                                    <b>%</b>
                                </td>
                            </tr>
                              <tr>
                                <td>
                                    <asp:Label ID="lblMinCartQuantity" runat="server" Text="Minimum Cart Quantity:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblMinCartQuantityResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtMinCartQuantity" name="MinCartQuantity" datatype="Integer"
                                        class="sfInputbox required" />
                                </td>
                            </tr>
                              <tr>
                                <td>
                                    <asp:Label ID="lblMaxCartQuantity" runat="server" Text="Maximum Cart Quantity:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblMaxCartQuantityResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtMaxCartQuantity" name="MaxCartQuantity" datatype="Integer"
                                        class="sfInputbox required" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="storefragment-7">
                    <div class="sfFormwrapper">
                        <h2>
                            <asp:Label ID="lblOtherSettings" runat="server" Text="Other Settings" meta:resourcekey="lblOtherSettingsResource1"></asp:Label>
                        </h2>
                        <table id="tblOtherSettings">  
                            <tr>
                                <td>
                                    <asp:Label ID="lblEnableEmailAFriend" runat="server" Text="Enable 'Email a Friend' :"
                                        CssClass="sfFormlabel" meta:resourcekey="lblEnableEmailAFriendResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkEmailAFriend" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblShowMiniShoppingCart" runat="server" Text="Show 'Mini Shopping Cart':"
                                        CssClass="sfFormlabel" meta:resourcekey="lblShowMiniShoppingCartResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkShowMiniShoppingCart" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowAnonymousUserToWriteReviews" runat="server" CssClass="sfFormlabel"
                                        Text="Allow Anonymous User to Write Reviews and Ratings:" meta:resourcekey="lblAllowAnonymousUserToWriteReviewsResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkAllowAnonymousUserToWriteReviews" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMultipleReviewsPerUser" runat="server" CssClass="sfFormlabel" Text="Allow Users To Write Multiple Reviews:"
                                        meta:resourcekey="lblMultipleReviewsPerUserResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkMultipleReviewsPerUser" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMultipleReviewsPerIP" runat="server" CssClass="sfFormlabel" Text="Allow Users To Write Multiple Reviews From Same IP:"
                                        meta:resourcekey="lblMultipleReviewsPerIPResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="checkbox" id="chkMultipleReviewsPerIP" />
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoOfDisplayItems" runat="server" Text="Number Of Items To display in a Row:"
                                        CssClass="sfFormlabel" meta:resourcekey="lblNoOfDisplayItemsResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtNoOfDisplayItems" name="NoOfDisplayItems" datatype="Integer"
                                        class="sfInputbox required" maxlength="2" disabled="disabled" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="storefragment-8">
                    <div class="sfFormwrapper">
                        <h2>
                            <asp:Label ID="Label1" runat="server" Text="Shipping Setting" meta:resourcekey="Label1Resource1"></asp:Label>
                        </h2>
                        <table border="0" id="Table1">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowBillingCountry" Text="List of allowed Billing Countries:"
                                        runat="server" CssClass="sfFormlabel" meta:resourcekey="lblAllowBillingCountryResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <select id="lbCountryBilling" multiple="multiple">
                                    </select>
                                    <input type="checkbox" id="cbSelectAllBillingCountry" />
                                </td>
                                <td class="cssClassGridRightCol">
                                    <label id="lblBillingCountry">
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAllowShippingCountry" Text="List of allowed Shipping Countries:"
                                        runat="server" CssClass="sfFormlabel" meta:resourcekey="lblAllowShippingCountryResource1"></asp:Label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <select id="lbCountryShipping" multiple="multiple">
                                    </select>
                                    <input type="checkbox" id="cbSelectAllShippingCountry" />
                                </td>
                                <td class="cssClassGridRightCol">
                                    <label id="lblShippingCountry">
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="storefragment-RssFeed">
                    <div class="sfFormwrapper">
                        <h2>
                            <asp:Label ID="lblRssFeedHeading" runat="server" Text="RssFeed Setting" meta:resourcekey="lblRssFeedHeadingResource1"></asp:Label>
                        </h2>
                        <table id="rssFeedTbl" cellpadding="0" cellspacing="0" border="0">
                            <%--<thead>
                                <tr>
                                    <th>
                                        <asp:Label ID="lblRssFeedTitle" runat="server" Text="Rss Feed For:" CssClass="sfFormlabel"
                                            meta:resourcekey="lblRssFeedTitleResource1"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label ID="lblRssFeedCount" runat="server" Text="Rss Feed Count" CssClass="sfFormlabel"
                                            meta:resourcekey="lblRssFeedCountResource1"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label ID="lblRssFeedStatus" runat="server" Text="Rss Feed Status" CssClass="sfFormlabel"
                                            meta:resourcekey="lblRssFeedStatusResource1"></asp:Label>
                                    </th>
                                </tr>
                            </thead>--%>
                            <tbody>
                                <tr>
                                    <td colspan="3">
                                        <h3>
                                            <asp:Label ID="Label2" runat="server" Text="For Front End:" meta:resourcekey="Label2Resource1"></asp:Label></h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRssFeedURL" runat="server" Text="RssFeed URL:" CssClass="sfFormlabel"
                                            meta:resourcekey="lblRssFeedURLResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRssFeedURL" class="sfListmenu" runat="server" meta:resourcekey="ddlRssFeedURLResource1">
                                        </asp:DropDownList>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Text="New Category:" CssClass="sfFormlabel"
                                            meta:resourcekey="lblCategoryResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <input type="text" id="txtCategoryRssCount" name="NewCategoryRssCount" class="sfInputbox required" />
                                    </td>
                                    <td>
                                        <input type="checkbox" id="categoryChkBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <h3>
                                            <asp:Label runat="server" Text="For Back End:" meta:resourcekey="LabelResource1"></asp:Label></h3>
                                    </td>
                                </tr>
                                <%--  For admin --%>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNewOrderRss" runat="server" Text="New Order:" CssClass="sfFormlabel"
                                            meta:resourcekey="lblNewOrderRssResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <input type="text" id="txtNewOrderRssCount" name="NewOrderRssCount" class="sfInputbox required" />
                                    </td>
                                    <td>
                                        <input type="checkbox" id="newOrderChkBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNewCustomerRss" runat="server" Text="New Customer:" CssClass="sfFormlabel"
                                            meta:resourcekey="lblNewCustomerRssResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <input type="text" id="txtNewCustomerRssCount" name="NewCustomerRssCount" class="sfInputbox required" />
                                    </td>
                                    <td>
                                        <input type="checkbox" id="newCustomerChkBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNewTag" runat="server" Text="New Item Tag:" CssClass="sfFormlabel"
                                            meta:resourcekey="lblNewTagResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <input type="text" id="txtNewItemTagRssCount" name="NewItemTagRssCount" class="sfInputbox required" />
                                    </td>
                                    <td>
                                        <input type="checkbox" id="newItemTagChkBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNewItemReview" runat="server" Text="New Item Review:" CssClass="sfFormlabel"
                                            meta:resourcekey="lblNewItemReviewResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <input type="text" id="txtNewItemReviewRssCount" name="NewItemReviewRssCount" class="sfInputbox required" />
                                    </td>
                                    <td>
                                        <input type="checkbox" id="newItemReviewChkBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLowStockItemRss" runat="server" Text="Low Stock Item:" CssClass="sfFormlabel"
                                            meta:resourcekey="lblLowStockItemRssResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <input type="text" id="txtLowStockRssCount" name="LowStockItemRssCount" class="sfInputbox required" />
                                    </td>
                                    <td>
                                        <input type="checkbox" id="lowStockChkBox" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="submit" id="btnSaveStoreSettings" class="cssClassButtonSubmit sfBtn">
                   <span class="sfLocale icon-save">Save Settings</span></button>
            </p>
        </div>
    </div>
</div>
<input type="hidden" id="hdnPrevFilePath" />
<input type="hidden" id="hdnPrevStoreLogoPath" />
