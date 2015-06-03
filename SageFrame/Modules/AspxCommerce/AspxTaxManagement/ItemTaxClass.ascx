<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemTaxClass.ascx.cs"
    Inherits="Modules_AspxTaxManagement_ItemTaxClass" %>

<script type="text/javascript">
    //<![CDATA[  
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxTaxManagement
        });
    });
    var lblItemTaxClassHeading = '<%=lblItemTaxClassHeading.ClientID %>';
    var umi = '<%=UserModuleID%>';
    //]]>
</script>

<div id="divTaxItemClassGrid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text="Manage Item Tax Class" 
                    meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnAddNewTaxItemClass" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Item Tax Class</span>
                        </button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelected" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span>
                        </button>
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
                                <label class="cssClassLabel sflocale">
                                    Item Tax Class Name:</label>
                                <input type="text" id="txtItemClassName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                
                                        <button type="button" onclick="ItemTaxClass.SearchItemClassName()" class="sfBtn">
                                            <span class="sflocale icon-search">Search</span></button>
                                  
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxItemTaxClassImage" src=""  alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvTaxItemClassDetails" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divProductTaxClass" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblItemTaxClassHeading" runat="server" 
                    Text="Item Tax Class Information" 
                    meta:resourcekey="lblItemTaxClassHeadingResource1"></asp:Label>
            </h2>
        </div>
        <div class="sfFormwrapper">
             <div class="cssClassLanguageSettingWrapper">
                                 <span class="sfLocale">Select Langauge:</span>
                                        <asp:Literal ID="languageSetting" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
            <table cellspacing="0" cellpadding="0" border="0" class="cssClassPadding tdpadding">
                <tr>
                    <td>
                        <asp:Label ID="lblTaxItemClassName" runat="server" Text="Item Tax Class Name:" 
                            CssClass="cssClassLabel" meta:resourcekey="lblTaxItemClassNameResource1"></asp:Label>
    
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtTaxItemClassName" class="sfInputbox required"  /><span class="cssClassRequired">*</span><span id="spanError"></span>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnCancel" class="sfBtn">
                    <span class="sfLocale icon-close">Cancel</span>
                </button>
            </p>
            <p>
                <button type="button" id="btnSaveTaxItemClass" class="sfBtn">
                    <span class="sfLocale icon-save">Save</span>
                </button>
            </p>
        </div>
    </div>
</div>
<input type="hidden" id="hdnTaxItemClassID" />
