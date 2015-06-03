<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopCutomersByOrder.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxAdminDashBoard_TopCutomersByOrder" %>

<script type="text/javascript">
    //<![CDATA[

    //]]>
</script>

<div id="divTopCustoerByOrder" class="cssClssRoundedBoxTable sfPriMrg-t">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblInventoryDetail" CssClass="cssClassLabel" runat="server" 
                    Text="Top Customer By Order " meta:resourcekey="lblInventoryDetailResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfGridwrapper">
            <div id="divTopCustomerOrderAdmindash">
            </div>
        </div>
    </div>
</div>
