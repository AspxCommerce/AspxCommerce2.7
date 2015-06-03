<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsCompare.ascx.cs"
    Inherits="Modules_AspxCompareItems_ItemsCompare" %>
<div class="fixed" style="display: none;">
    <div class="cssCompareBtnWrapper" style="display: none;"><a class="cssCompareBtnShow" href="#"><span class="sfLocale">Show Compare Items</span></a></div>
    <div id="compareProductsContainer" class="cssClassCompareContainer" style="display: none;">
        <div class="compareProductsBoxTitle">
            <div>
                <h2><span class="sfLocale">Compare Items</span></h2>
                <div class="cssClassClose" id="compareCloseBtn">
                </div>
            </div>
        </div>
        <div class="cssCompareProductsBox">
            <div id="compareError" style="display: none">
                <asp:Literal ID="ltrError" runat="server" EnableViewState="False"></asp:Literal>
            </div>
            <div id="compareProductsBox">
                <asp:Literal ID="ltrCompareItem" runat="server" EnableViewState="False"></asp:Literal>
            </div>
            <div id="compareButtonWrapper">
                <div class="sfButtonwrapper">
                    <input type="button" id="btnCompare" value="Compare" class="sfBtn sfLocale" />
                </div>
                <div class="sfButtonwrapperclear" align="center">
                    <input type="button" id="btnCompareClearAll" value="Clear All" class="sfBtn sfLocale" />
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(this).ItemCompare({
            CompareItemListURL: '<%=CompareItemListURL %>',
            MaxCompareItemCount: '<%=MaxCompareItemCount%>',
            CompareLen: '<%=compareLen %>',
            DefaultImagePath: '<%=DefaultImagePath %>',
            ServicePath: '<%=ServicePath%>',
            ButtonTemplate: "<label><input type='checkbox' onclick='ItemsCompareAPI.Add(${Params});' id='${ID}'><span>" + getLocale(AspxItemsCompare, "Compare") + "</span></label>",
            ItemDetailButtonTemplate: '<label class="i-compare cssCompareLabel cssClassGreyBtn"><button type="button" id={ID} onclick="ItemsCompareAPI.AddToCompareFromDetails(${Params})" >' + getLocale(AspxItemsCompare, "Compare +") + '</button></label>'
        });
    });
    //]]>
</script>