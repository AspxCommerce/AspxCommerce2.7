<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LatestFiveOrderItems.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxAdminDashBoard_LatestFiveOrderItems" %>

<script type="text/javascript">
    //<![CDATA[

    //]]>
    $(document).ready(function() {
        $(".sfLocale").localize({
            moduleKey: AspxAdminDashBoard
        });
    });
</script>

<div id="divLatestOrderStaticsByCustomer" class="cssClssRoundedBoxTable">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader sfPriMrg-t">
            <h2>
                <asp:Label ID="lblInventoryDetail" CssClass="cssClassLabel" runat="server" 
                    Text="List of Latest Orders" meta:resourcekey="lblInventoryDetailResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfGridwrapper">
            <div id="divLatestOrderStatics">
            </div>
        </div>
    </div>
</div>
