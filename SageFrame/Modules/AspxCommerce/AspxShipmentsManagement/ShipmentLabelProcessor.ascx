<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShipmentLabelProcessor.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxShipmentsManagement_ShipmentLabelProcessor" %>
<script type="text/javascript">
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxShipmentsManagement
        });
        $("#btnShippingBack").click(function () {
            var href = SageFrameHostURL + "/Admin/AspxCommerce/Sales/Orders-Overview/Orders" + pageExtension;            
            window.open(href, '_self');
        });

        var oid = getParameterByName('oid');
        var breadcrumb = "<ul><li class='sfFirst'><a href='" + SageFrameHostURL + "/Admin/Admin.aspx'><i class='icon-home'></i></a></li><li><span>AspxCommerce</span></li><li><span>Sales</span></li><li><span>Orders Overview</span></li><li><a href='" + SageFrameHostURL + "/Admin/AspxCommerce/Sales/Orders-Overview/Orders.aspx'>Orders</a></li><li><a href='" + SageFrameHostURL + "/Admin/Shipping-Label.aspx?oid=" + oid + "'>Shipping Label</a></li></ul>";

        $("div.sfBreadcrumb").html(breadcrumb);
    });

    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }
</script>
<div class="sfFormwrapper">
    <div id="dvholder">
        <asp:PlaceHolder runat="server" ID="phShippingLabelHolder"></asp:PlaceHolder>
    </div>
    <div>
        <asp:Literal runat="server" ID="ltLabelPreview" 
            meta:resourcekey="ltLabelPreviewResource1"></asp:Literal>
    </div>
    <div id="dvCustomLabelCreater" runat="server" visible="False">
        <div class="sfFormwrapper">
            <div id="dvShipmentForm" runat="server">
                <div class="sfGridwrapper">
                    <div class="sfGridwrapperContent">
                    <div class="clearfix">
                        <div class="cssClassBillingAddress">
                            <h2>
                                <asp:Label runat="server" ID="lblBillingAddress" Text="Billing Address" 
                                    meta:resourcekey="lblBillingAddressResource1"></asp:Label></h2>
                            <asp:Literal runat="server" ID="ltBillingAddress" 
                                meta:resourcekey="ltBillingAddressResource1"></asp:Literal>
                        </div>
                        <div class="cssClassWareAddress">
                            <h2>
                                <asp:Label runat="server" ID="lblWareHouseAddress" Text="WareHouse Address" 
                                    meta:resourcekey="lblWareHouseAddressResource1"></asp:Label></h2>
                            <asp:Literal runat="server" ID="ltWareHouse" 
                                meta:resourcekey="ltWareHouseResource1"></asp:Literal>
                        </div>
                        </div>
                        <div class="cssClassToAddress">
                            <h2>
                                <asp:Label runat="server" ID="lblToAddress" Text="To Address" 
                                    meta:resourcekey="lblToAddressResource1"></asp:Label></h2>
                            <asp:Literal runat="server" ID="ltShippingAddress" 
                                meta:resourcekey="ltShippingAddressResource1"></asp:Literal>
                        </div>
                    </div>
                    <div class="cssClassPackage">
                        <h2 class="sfLocale">
                            Package Details
                        </h2>
                        <asp:Literal runat="server" ID="ltPackageDetail" 
                            meta:resourcekey="ltPackageDetailResource1"></asp:Literal>
                        <div class="clear">
                        </div>
                        <div>
                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Total Weight:" ID="lblTotalWeight" 
                                            meta:resourcekey="lblTotalWeightResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPackageTotalWeight" 
                                            meta:resourcekey="lblPackageTotalWeightResource1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Weight Unit:" ID="lblWeightUnit" 
                                            meta:resourcekey="lblWeightUnitResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblStoreWeightUnit" 
                                            meta:resourcekey="lblStoreWeightUnitResource1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Service Type:" ID="lblServiceType" 
                                            meta:resourcekey="lblServiceTypeResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblUserShippingMethod" 
                                            meta:resourcekey="lblUserShippingMethodResource1"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Package Dimension (L*W*H):" ID="lbldimension" 
                                            meta:resourcekey="lbldimensionResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPackageLength" CssClass="sfTextBoxSmall" 
                                            meta:resourcekey="txtPackageLengthResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtPackageLength" ValidationGroup="label" 
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPackageLength"
                                            ErrorMessage="*" ValidationExpression="^\d+$" ValidationGroup="label" 
                                            meta:resourcekey="RegularExpressionValidator1Resource1" ForeColor="Red"></asp:RegularExpressionValidator>
                                        <asp:TextBox runat="server" ID="txtPackageWidth" CssClass="sfTextBoxSmall" 
                                            meta:resourcekey="txtPackageWidthResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtPackageWidth" ValidationGroup="label" 
                                            meta:resourcekey="RequiredFieldValidator2Resource1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPackageWidth"
                                            ErrorMessage="*" ValidationExpression="^\d+$" ValidationGroup="label" 
                                            meta:resourcekey="RegularExpressionValidator2Resource1" ForeColor="Red"></asp:RegularExpressionValidator>
                                        <asp:TextBox runat="server" ID="txtPackageHeight" CssClass="sfTextBoxSmall" 
                                            meta:resourcekey="txtPackageHeightResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtPackageHeight" ValidationGroup="label" 
                                            meta:resourcekey="RequiredFieldValidator3Resource1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPackageHeight"
                                            ErrorMessage="*" ValidationExpression="^\d+$" ValidationGroup="label" 
                                            meta:resourcekey="RegularExpressionValidator3Resource1" ForeColor="Red"></asp:RegularExpressionValidator>
                                        <asp:Label runat="server" ID="lblStoreDimensionUnit" 
                                            meta:resourcekey="lblStoreDimensionUnitResource1"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="sfButtonWrapper">
                        <p>
                            <asp:Button runat="server" ID="btnCreateLabel" Text="Create Label" CssClass="sfBtn"
                                OnClick="btnCreateLabel_Click" ValidationGroup="label" 
                                meta:resourcekey="btnCreateLabelResource1" /></p>
                    </div>
                </div>
            </div>
            <div id="dvLabelPreview" runat="server">
                <asp:Literal runat="server" ID="ltPreviewLabel" 
                    meta:resourcekey="ltPreviewLabelResource1"></asp:Literal>
            </div>
        </div>
    </div>
    <div class="sfError">
        <asp:Label runat="server" ID="lblErrorMessage" ForeColor="Red" 
            meta:resourcekey="lblErrorMessageResource1"></asp:Label>
    </div>    
    <br />
<button class="sfBtn" id="btnShippingBack" type="button">
                <span class="sfLocale icon-arrow-slim-w">Back</span></button>
</div>
