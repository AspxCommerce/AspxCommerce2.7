<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemRatingCriteriaManage.ascx.cs"
    Inherits="Modules_AspxItemRatingManagement_ItemRatingCriteriaManage" %>

<script type="text/javascript">

    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxItemRatingManagement
        });
    });
    var lblItemRatingFormTitle = '<%=lblItemRatingFormTitle.ClientID %>';
    var umi = '<%=UserModuleID%>';
    //]]>
</script>

<div id="divShowItemCriteriaDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text="Manage Item Rating Criteria" 
                    meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnAddNewCriteria" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Criteria</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelectedCriteria" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span></button>
                    </p>
                    <%--<p> <input type="button" class="" id="btnDeactivateSelected" value="Deactivate All Selected" /> </p>--%>
                    
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
                                    Rating Criteria:</label>
                                <input type="text" id="txtSearchCriteria" class="sfTextBoxSmall" />
                            </td>
                          <td> 
                                <label class="cssClassLabel sfLocale">
                                    Active:</label>
                                <select id="ddlIsActive" class="sfListmenu">
                                    <option value="" class="sfLocale">--All--</option>
                                    <option value="True" class="sfLocale">Yes</option>
                                    <option value="False" class="sfLocale">No</option>
                                </select>
                            </td>
                            <td>
                                 <button type="button" id="btnCriteriaSearch" class="sfBtn" >
                                            <span class="sfLocale icon-search">Search</span></button>
                                
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxItemRatingCriteriaImage" src=""  alt="loading...." class="sfLocale"/>
                </div>
                <div class="log">
                </div>
                <table id="gdvItemRatingCriteria" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divItemCriteriaForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblItemRatingFormTitle" runat="server" 
                    meta:resourcekey="lblItemRatingFormTitleResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfFormwrapper">
            <table border="0" width="100%" id="tblEditReviewForm" class="cssClassPadding tdpadding">
                <tr>
                    <td width="10%">
                        <asp:Label ID="lblCriteria" runat="server" Text="Criteria:" 
                            CssClass="cssClassLabel" meta:resourcekey="lblCriteriaResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtNewCriteria" name="CriteriaTypeName" class="sfInputbox required" />
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
                <button type="button" id="btnCancelCriteriaUpdate" class="sfBtn">
                    <span class="sfLocale icon-close">Cancel</span></button>
            </p>
            <p>
                <button type="button" id="btnSubmitCriteria" class="sfBtn">
                <span class="sfLocale icon-save">Save</span></button>
            </p>
        </div>
        <input type="hidden" id="hdnItemCriteriaID" />
    </div>
</div>
