<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InventoryDetails.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxAdminDashBoard_InventoryDetails" %>

<script type="text/javascript">
    //<![CDATA[
    var lowStock = '<%=LowStockQuantity%>';
    //]]>
</script>

<div id="divInventoryDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader" style="margin-top:15px;">
            <h2>
                <asp:Label ID="lblInventoryDetail" runat="server" Text="Inventory " 
                    meta:resourcekey="lblInventoryDetailResource1"></asp:Label>
            </h2>
        </div>
       
            <div class="cssInventory clearfix">
                <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblInventoryDetail">
                           <tr>
                           <td> 
                           <i class="icon-total-items"></i>
                           <label id="lblItemtotal"></label>
                           <asp:Label ID="lblTotalItem" runat="server" CssClass="cssClassLabel" 
                                Text="Total Items" meta:resourcekey="lblTotalItemResource1"></asp:Label></td>
                       
                            <td>
                            <i class="icon-active-items"></i><label
                                id="lblAvtive"></label><asp:Label ID="lblActiveItem" runat="server" CssClass="cssClassLabel" 
                                Text="Active Items" meta:resourcekey="lblActiveItemResource1"></asp:Label></td>
                       
                            <td><i class="icon-hidden-items"></i><label
                                id="lblHidden"></label><asp:Label ID="lblHiddenItem" runat="server" CssClass="cssClassLabel" 
                                Text="Hidden Items" meta:resourcekey="lblHiddenItemResource1"></asp:Label></td>
                       
                            <td><i class="icon-downloadable-items"></i><label
                                id="lblDownloadable"></label><asp:Label ID="lblDownloadableItems" runat="server" CssClass="cssClassLabel" 
                                Text="Downloadable Items" meta:resourcekey="lblDownloadableItemsResource1"></asp:Label></td>
                       
                            <td><i class="icon-special-items"></i><label
                                id="lblSpecial"></label><asp:Label ID="lblSpecialItems" runat="server" CssClass="cssClassLabel" 
                                Text="Special Items" meta:resourcekey="lblSpecialItemsResource1"></asp:Label></td>
                        
                            <td><i class="icon-lowstock-items"></i><label
                                id="lblLowstock"></label><asp:Label ID="lblLowStockItem" runat="server" CssClass="cssClassLabel" 
                                Text="Low Stock Items" meta:resourcekey="lblLowStockItemResource1"></asp:Label></td>
                             <td><i class="icon-group-items"></i><label
                                id="lblGroupItem"></label><asp:Label ID="Label2" runat="server" CssClass="cssClassLabel" 
                                Text="Group Items" meta:resourcekey="lblGroupItemResource1"></asp:Label></td>
                             <td><i class="icon-kit-items"></i><label
                                id="lblKitItem"></label><asp:Label ID="Label3" runat="server" CssClass="cssClassLabel" 
                                Text="Kit Items" meta:resourcekey="lblKitItemResource1"></asp:Label></td>
                       <tr>
                       </table>
            </div>
       
    </div>
</div>

