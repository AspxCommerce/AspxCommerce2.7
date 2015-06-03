<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerTaxClass.ascx.cs"
    Inherits="Modules_AspxTaxManagement_CustomerTaxClass" %>

<script type="text/javascript">
    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxTaxManagement
        });
    });
    var lblCustomerTaxClassHeading='<%=lblCustomerTaxClassHeading.ClientID %>';
    var umi = '<%=UserModuleID%>';
    //]]>
</script>

<div id="divTaxCustomerClassGrid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text="Manage Customer Tax Class" 
                    meta:resourcekey="lblTitleResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnAddNewTaxCustomerClass" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Customer Tax Class</span>
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
                <div class="cssClassSearchPanel sfFormwrapper">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Customer Tax Class Name:</label>
                                <input type="text" id="txtCustomerClassName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <div class="sfButtonwrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="CustomerTaxClass.SearchCustomerClassName()" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCustomerTaxClassImage" src=""  alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvTaxCustomerClassDetails" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<div id="divCustomerTaxClass" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblCustomerTaxClassHeading" runat="server" 
                    meta:resourcekey="lblCustomerTaxClassHeadingResource1" ></asp:Label>
            </h2>
        </div>
        <div class="sfFormwrapper">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding tdpadding">
                <tr>
                    <td>
                        <asp:Label ID="lblTaxCustomerClassName" runat="server" Text="Customer Tax Class Name:"
                            CssClass="cssClassLabel" 
                            meta:resourcekey="lblTaxCustomerClassNameResource1"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtTaxCustomerClassName" class="sfInputbox"/>
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
                <button type="button" id="btnSaveTaxCustomerClass" class="sfBtn">
                    <span class="sfLocale icon-save">Save</span>
                </button>
            </p>
        </div>
    </div>
</div>
<input type="hidden" id="hdnTaxCustomerClass" />
