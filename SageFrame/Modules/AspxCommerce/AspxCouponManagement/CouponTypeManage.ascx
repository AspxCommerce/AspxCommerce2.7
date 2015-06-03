<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponTypeManage.ascx.cs"
    Inherits="Modules_AspxCouponManagement_CouponTypeManage" %>

<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxCouponManagement
        });
    });
   
    var lblCouponTypeFormTitle = "<%=lblCouponTypeFormTitle.ClientID %>";
    var umi = '<%=UserModuleId%>';
    //]]>
</script>

<div id="divShowCouponTypeDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblCouponTypeGridTitle" runat="server" 
                    Text="Manage Coupon Types" meta:resourcekey="lblCouponTypeGridTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                     <p>
                        <button type="button" id="btnAddNewCouponType" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Coupon Type</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelectedCouponType" class="sfBtn">
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
                <div class="sfFormwrapper sfTableOption">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Coupon Type:</label>
                                <input type="text" id="txtSearchCouponType" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                         <button type="button" onclick="couponTypeMgmt.SearchCouponType()"  class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCouponTypeImageLoad" src="" class="sfLocale" alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvCouponType" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divCouponTypeProviderForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblCouponTypeFormTitle" runat="server" 
                    meta:resourcekey="lblCouponTypeFormTitleResource1"></asp:Label>
            </h1>
        </div>
        <div class="sfFormwrapper">
             <div class="cssClassLanguageSettingWrapper">
                                 <span class="sfLocale">Select Langauge:</span>
                                        <asp:Literal ID="languageSetting" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
            <table border="0" width="100%" id="tblEditCouponForm" class="cssClassPadding tdpadding">
                <tr>
                    <td width="10%">
                        <asp:Label ID="lblCouponType" Text="Coupon Type:" runat="server" 
                            CssClass="cssClassLabel" meta:resourcekey="lblCouponTypeResource1"></asp:Label>
                       
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtNewCouponType" name="CouponTypeName" class="sfInputbox required" minlength="2" /><span id="ctErrorLabel"></span> <span class="cssClassRequired">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIsActive" runat="server" Text="Active:" 
                            CssClass="cssClassLabel" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="checkbox" id="chkIsActive" class="cssClassCheckBox" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnCancelCouponTypeUpdate" class="sfBtn">
                    <span class="sfLocale icon-close">Cancel</span></button>
            </p>
            <p>
                <button type="button" id="btnSubmitCouponType" class="sfBtn">
                    <span class="sfLocale icon-save">Save</span></button>
            </p>
        </div>
    </div>
</div>
