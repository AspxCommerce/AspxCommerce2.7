<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvanceSearch.ascx.cs" Inherits="Modules_AspxCommerce_AspxAdvanceSearch_AdvanceSearch" %>

<div class="cssClassAdvenceSearch">
    <div class="cssClassHeader">
        <h1>
            <asp:Label ID="lblTitle" runat="server" Text="Advanced Search:" meta:resourcekey="lblTitleResource1"></asp:Label>
        </h1>
    </div>
    <div class="sfFormwrapper sfAdvanceSearch cssClassTMar30">
       <div class="clearfix">
                    <div class="sfCol_50">
                    <div><asp:Label ID="lblSearchFor" runat="server" Text="Search For:" CssClass="cssClassLabel cssClasssearchFor"
                        meta:resourcekey="lblSearchForResource1"></asp:Label></div>
                    <input type="text" id="txtSearchFor" class="sfInputbox searchForTextBox" />
            </div>
                    <div class="sfCol_25">
                    <div><asp:Label ID="lblCategory" runat="server" Text="In Categories:" CssClass="cssClassLabel"
                        meta:resourcekey="lblCategoryResource1"></asp:Label></div>
                    <asp:Literal ID="ltrCategories" runat="server" EnableViewState="False" 
                            meta:resourcekey="ltrCategoriesResource1"></asp:Literal></div>
            
           <div class="sfCol_25" runat="server" id="trBrand" visible="false"> 
              
                    <div><asp:Label ID="lblSearchBrand" runat="server" Text="In Brands:" CssClass="cssClassLabel"
                        meta:resourcekey="lblBrandsResource1"></asp:Label></div>
                    <asp:Literal ID="ltrBrands" runat="server" EnableViewState="False" 
                        meta:resourcekey="ltrBrandsResource1"></asp:Literal></div>
                    </div>
             
                    <div class="pricebox clearfix cssClassTMar10">                        
                        <div class="sfCol_10"><strong><asp:Label ID="lblPriceIn" runat="server" Text="Price:" CssClass="cssClassLabel"
                            meta:resourcekey="lblPriceInResource1"></asp:Label></strong></div>
                        <div id="divPriceFrom" class="sfCol_25">
                            <span class="sfLocale">From:</span><input class="sfInputbox" id="txtPriceFrom" type="text" />
                            <span id="errmsgPriceFrom"></span>
                        </div>
                        <div id="divPriceTo" class="sfCol_25">
                            <span class="sfLocale">To:</span><input class="sfInputbox" id="txtPriceTo" type="text" />
                            <span id="errmsgPriceTo"></span>
                        </div>
                            
                    </div>
                    <div class="cssDynAttrWrapper clearfix cssClassTMar10" style="display:none;"></div>
                          
                        <div class="sfButtonwrapper cssClassTMar10 cssClassLMar20">
       
            <label class="cssClassOrangeBtn i-search"><button type="button" id="btnAdvanceSearch">
                <span class="sfLocale">Search</span></button></label>
        
    </div>
                    
            
    </div>
    
</div>
<asp:Literal ID="ltrSortView" runat="server" EnableViewState="false"></asp:Literal>
<div id="divShowAdvanceSearchResult" class="cssClassDisplayResult">
</div>
<!-- TODO:: paging Here -->
<asp:Literal ID="ltrPagination" runat="server" EnableViewState="false"></asp:Literal>

<script type="text/javascript">
    //<![CDATA[
    $(function () {    
        $(".sfLocale").localize({
            moduleKey: AdvanceSearchLang
        });
        $(this).AdvanceSearchInit({
            NoImageAdSearchPathSetting: '<%=NoImageAdSearchPath %>',
            AllowAddToCart: '<%=AllowAddToCart %>',
            AllowOutStockPurchaseSetting: '<%=AllowOutStockPurchase %>',            
            NoOfItemsInRow: '<%=NoOfItemsInARow %>',
            Displaymode: '<%=ItemDisplayMode %>'.toLowerCase(),
            AdvanceSearchModulePath: "<%=AdvanceSearchModulePath %>"
        });
    });   
    //]]>                              
</script>