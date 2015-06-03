<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManageProviderShippingMethod.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxShippingManagement_ManageProviderShippingMethod" %>

<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxShippingManagement
        });
    });
    var maxFileSize = '<%=MaxFileSize%>';
    var umi = '<%=UserModuleID%>';
 //]]>
</script>

<!-- Grid -->
<div id="divShowShippingMethodGrid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitleShippingMethods" runat="server" Text="Shipping Methods" meta:resourcekey="lblTitleShippingMethodsResource1"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnDeleteSelected" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span></button>
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="loading">
                    <img id="ajaxShippingMgmtImage1" src="" alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvShippingMethod" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<!-- End of Grid -->
<input type="hidden" id="hdnShippingMethodID" />
<input type="hidden" id="hdnPrevFilePath" />
<div id="dvProviderService">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblAddshippingAddress" runat="server" Text="Add Shipping Address:"
                    meta:resourcekey="lblAddshippingAddressResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfFormwrapper">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding">
                <tr>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</div>
