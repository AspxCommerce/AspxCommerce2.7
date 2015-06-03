<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MostViewedItems.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxAdminDashBoard_MostViewedItems" %>

<script type="text/javascript">
    //<![CDATA[

    //]]>
</script>

<div id="divMostViewedItem" class="cssClssRoundedBoxTable">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader sfPriMrg-t">
            <h2>
                <asp:Label ID="lblInventoryDetail" CssClass="cssClassLabel" runat="server" 
                    Text="Most Viewed Item " meta:resourcekey="lblInventoryDetailResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfGridwrapper">
            <div id="divMostViewedItemAdmindash">
            </div>
        </div>
    </div>
</div>
