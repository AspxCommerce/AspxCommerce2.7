<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreAccessControl.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxStoreAccessManagement_StoreAccessControl" %>

<script type="text/javascript">
    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxStoreAccessManagement
        });
    });
       var lblStoreAccessValueID = '<%=LblStoreAccessValueID %>';
    var lblAddEditStoreAccessTitleID = '<%=LblAddEditStoreAccessTitleID %>';
    var umi = '<%=UserModuleID%>';
    //]]>
</script>

<script type="text/javascript">
    $(function() {
        $('#txtsrchEmail').autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: aspxservicePath + "AspxCoreHandler.ashx/SearchStoreAccess",
                    beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", umi);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    data: JSON2.stringify({ text: request.term, keyID: $('input[ name="Email"]').val() }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function(data) { return data; },
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            return {
                                value: item.StoreAccessData
                            };
                        }));
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                    }
                });
            },
            minLength: 2
        });
       // $('#txtStoreAccessValue').autocomplete("disable");

    });
</script>

<div class="cssClassTabPanelTable">
    <div id="dvTabPanel">
        <ul>
            <li><a href="#dvIP">
                <asp:Label ID="lblIP" runat="server" Text="IP" 
                    meta:resourcekey="lblIPResource1"></asp:Label></a></li>
            <li><a href="#dvDomain">
                <asp:Label ID="lblDomain" runat="server" Text="Domain" 
                    meta:resourcekey="lblDomainResource1"></asp:Label></a></li>
            <li><a href="#dvEmail">
                <asp:Label ID="lblEmail" runat="server" Text="Email" 
                    meta:resourcekey="lblEmailResource1"></asp:Label></a></li>
            <li><a href="#dvCreditCard">
                <asp:Label ID="lblCreditCard" runat="server" Text="Credit Card" 
                    meta:resourcekey="lblCreditCardResource1"></asp:Label></a></li>
            <li><a href="#dvCustomer">
                <asp:Label ID="lblCustomer" runat="server" Text="Customer" 
                    meta:resourcekey="lblCustomerResource1"></asp:Label></a></li>
        </ul>
        <div id="dvIP">
            <div class="cssClassIp">
                <div id="div1">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblHeaderIP" runat="server" CssClass="cssClassLabel" 
                                    Text=" List of IP's Blocked" meta:resourcekey="lblHeaderIPResource1"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                                <div class="sfButtonwrapper">
                                    <p>
                                        <button type="button" id="btnAddIP" class="sfBtn">
                                           <span class="sfLocale icon-addnew">Add New IP</span></button>
                                    </p>
                                    <p>
                                        <button type="button" id="btnDeleteSelectedIP" class="sfBtn">
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
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <label class="cssClassLabel sfLocale">
                                                    IP:</label>
                                                <input type="text" id="txtsrchIP" class="sfTextBoxSmall" />
                                            </td>
                                            <td width="365px">
                                                <label class="cssClassLabelsfLocale"">
                                                    AddedOn:</label><br />

                                                <span class="label sfLocale">From :</span>
                                                <input type="text" id="txtsrchIPDate" class="sfInputbox" />
                                                <span class="label sfLocale">To :</span>
                                                <input type="text" id="txtIPEndDate" class="sfInputbox" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel sfLocale">
                                                    Status:
                                                </label>
                                                <select id="SelectStatusIP" class="sfSelect">
                                                    <option value="" class="sfLocale">-- All -- </option>
                                                    <option value="True" class="sfLocale">Active </option>
                                                    <option value="False" class="sfLocale">Inactive </option>
                                                </select>
                                            </td>
                                            <td><br />

                                                        <button type="button" onclick="StoreAccessmanage.searchStoreAccess(this)" id="btnSrchIP" class="sfBtn">
                                                            <span class="sfLocale icon-search">Search</span></button>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="loading">
                                    <img id="ajaxStoreAccessImage1" src="" alt="loading...." title="loading...." />
                                </div>
                                <div class="log">
                                </div>
                                <table id="gdvIP" cellspacing="0" cellpadding="0" border="0" width="100%">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvDomain" style="display: none">
            <div class="cssClassDomain">
                <div id="div2">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblHeadingDomain" runat="server" CssClass="cssClassLabel" 
                                    Text=" List of Domains Blocked" meta:resourcekey="lblHeadingDomainResource1"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                                <div class="sfButtonwrapper">
                                    <p>
                                        <button type="button" id="btnAddDomain" class="sfBtn">
                                            <span class="sfLocale icon-addnew">Add New Domain</span></button>
                                    </p>
                                    <p>
                                        <button type="button" id="btnDeleteSelectedDomain" class="sfBtn">
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
                                                <label class="cssClassLabel sfLocale">
                                                    Domain:</label>
                                                <input type="text" id="txtsrchDomain" class="sfTextBoxSmall" />
                                            </td>
                                            <td width="365px">
                                                <label class="cssClassLabel sfLocale">
                                                    AddedOn:</label><br />
                                                <span class="label sfLocale">From :</span>
                                                <input type="text" id="txtsrchDomainDate" class="sfInputbox" />
                                                <span class="label sfLocale">To :</span>
                                                <input type="text" id="txtDomainEndDate" class="sfInputbox" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel sfLocale">
                                                    Status:
                                                </label>
                                                <select id="SelectStatusDomain" class="sfSelect">
                                                    <option value="" class="sfLocale">-- All -- </option>
                                                    <option value="True" class="sfLocale">Active </option>
                                                    <option value="False" class="sfLocale">Inactive </option>
                                                </select>
                                            </td>
                                            <td>
                                                        <button type="button" onclick="StoreAccessmanage.searchStoreAccess(this)" id="btnSrchDomain" class="sfBtn">
                                                            <span class="sfLocale icon-search">Search</span></button>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="loading">
                                    <img id="ajaxStoreAccessImage5" src="" alt="loading...." title="loading...." />
                                </div>
                                <div class="log">
                                </div>
                                <table id="gdvDomain" cellspacing="0" cellpadding="0" border="0" width="100%">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvEmail" style="display: none">
            <div class="cssClassEmail">
                <div id="div3">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblHeadingEmail" runat="server" CssClass="cssClassLabel" 
                                    Text="List of Email IDs Blocked" meta:resourcekey="lblHeadingEmailResource1"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                                <div class="sfButtonwrapper">
                                     <p>
                                        <button type="button" id="btnAddEmail" class="sfBtn">
                                            <span class="sfLocale icon-addnew">Add New Email</span></button>
                                    </p>
                                    <p>
                                        <button type="button" id="btnDeleteSelectedEmail" class="sfBtn">
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
                                            <td >
                                                <label class="cssClassLabel sfLocale">
                                                    Email:</label>
                                                <input type="text" id="txtsrchEmail" class="sfTextBoxSmall" />
                                            </td>
                                            <td width="365px">
                                                <label class="cssClassLabel sfLocale">
                                                    AddedOn:</label><br />
                                                <span class="label sfLocale">From :</span>
                                                <input type="text" id="txtsrchEmailDate" class="sfInputbox" />
                                                <span class="label sfLocale">To :</span>
                                                <input type="text" id="txtEmailEndDate" class="sfInputbox" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel sfLocale">
                                                    Status:
                                                </label>
                                                <select id="SelectStatusEmail" class="sfSelect">
                                                    <option value="" class="sfLocale">-- All -- </option>
                                                    <option value="True" class="sfLocale">Active </option>
                                                    <option value="False" class="sfLocale">Inactive </option>
                                                </select>
                                            </td>
                                            <td>
                                                     <br />   <button type="button" onclick="StoreAccessmanage.searchStoreAccess(this)" id="btnSrchEmail"  class="sfBtn">
                                                            <span class="sfLocale icon-search">Search</span></button>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="loading">
                                    <img id="ajaxStoreAccessImage4" src="" alt="loading...." title="loading...." />
                                </div>
                                <div class="log">
                                </div>
                                <table id="gdvEmail" cellspacing="0" cellpadding="0" border="0" width="100%">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvCreditCard" style="display: none">
            <div class="cssClassCreditCard">
                <div id="div4">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblHeadingCreditCard" runat="server" CssClass="cssClassLabel" 
                                    Text="List of Credit Cards Blocked" 
                                    meta:resourcekey="lblHeadingCreditCardResource1"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                                <div class="sfButtonwrapper">
                                    <p>
                                        <button type="button" id="btnAddCreditCard" class="sfBtn">
                                            <span class="sfLocale icon-addnew">Add New Credit Card</span></button>
                                    </p>
                                    <p>
                                        <button type="button" id="btnDeleteSelectedCreditCard" class="sfBtn">
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
                                                <label class="cssClassLabel sfLocale">
                                                    Credit Card No:</label>
                                                <input type="text" id="txtsrchCreditCard" class="sfTextBoxSmall" />
                                            </td>
                                            <td width="365px">
                                                <label class="cssClassLabel sfLocale">
                                                    AddedOn:</label><br />
                                                <span class="label sfLocale">From :</span>
                                                <input type="text" id="txtsrchCreditCardDate" class="sfInputbox" />
                                                <span class="label sfLocale">To :</span>
                                                <input type="text" id="txtCardEndDate" class="sfInputbox" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel sfLocale">
                                                    Status:</label> 
                                                <select id="SelectStatusCreditCard" class="sfSelect">
                                                    <option value="" class="sfLocale">-- All -- </option>
                                                    <option value="True" class="sfLocale">Active </option>
                                                    <option value="False" class="sfLocale">Inactive </option>
                                                </select>
                                            </td>
                                            <td><br />
                                                        <button type="button" onclick="StoreAccessmanage.searchStoreAccess(this)" id="btnSrchCreditCard" class="sfBtn">
                                                            <span class="sfLocale icon-search">Search</span></button>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="loading">
                                    <img id="ajaxStoreAccessImage3" src="" alt="loading...." title="loading...." />
                                </div>
                                <div class="log">
                                </div>
                                <table id="gdvCreditCard" cellspacing="0" cellpadding="0" border="0" width="100%">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvCustomer" style="display: none">
            <div class="cssClassCustomer">
                <div id="div6">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblHeadingCustomer" runat="server" CssClass="cssClassLabel" 
                                    Text="List of Customers Blocked" meta:resourcekey="lblHeadingCustomerResource1"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                                <div class="sfButtonwrapper">
                                    <p>
                                        <button type="button" id="btnAddCustomer" class="sfBtn">
                                            <span class="sfLocale icon-addnew">Add New Customer</span></button>
                                    </p>
                                    <p>
                                        <button type="button" id="btnDeleteSelectedCustomer" class="sfBtn">
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
                                                <label class="cssClassLabel sfLocale"">
                                                    Customer Name:</label>
                                                <input type="text" id="txtsrchCustomer" class="sfTextBoxSmall" />
                                            </td>
                                            <td width="365px">
                                                <label class="cssClassLabel sfLocale">
                                                    AddedOn:</label><br />
                                                <span class="sfLocale">From :</span>
                                                <input type="text" id="txtsrchCustomerDate" class="sfInputbox" />
                                                <span class="sfLocale">To :</span>
                                                <input type="text" id="txtCustomerDate" class="sfInputbox" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel sfLocale">
                                                    Status:</label>  
                                                <select id="SelectStatusCustomer" class="sfSelect">
                                                    <option value="" class="sfLocale">-- All -- </option>
                                                    <option value="True" class="sfLocale">Active </option>
                                                    <option value="False" class="sfLocale">Inactive </option>
                                                </select>
                                            </td>
                                            <td><br />
                                                        <button type="button" onclick="StoreAccessmanage.searchStoreAccess(this)" id="btnSrchCustomer" class="sfBtn">
                                                            <span class="sfLocale icon-search">Search</span></button>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="loading">
                                    <img id="ajaxStoreAccessImage2" src="" alt="loading...." title="loading...." />
                                </div>
                                <div class="log">
                                </div>
                                <table id="gdvCustomer" cellspacing="0" cellpadding="0" border="0" width="100%">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="popupbox" id="popuprel">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span class="sfLocale">Close</span></button>
    </div>
    <div id="editAdd">
        <div class="cssClassCommonBox Curve">
            <div class="cssClassHeader">
                <h3>
                    <asp:Label ID="lblAddEditStoreAccessTitle" runat="server" 
                        meta:resourcekey="lblAddEditStoreAccessTitleResource1"></asp:Label>
                </h3>
            </div>
            <div class="sfFormwrapper">
                <table border="0" width="100%" id="tblAddEditStoreAccessForm" class="cssClassPadding">
                    <tr id="forIPonly">
                        <td>
                            <asp:Label ID="lblStoreAccessValue" runat="server" CssClass="cssClassLabel" 
                                meta:resourcekey="lblStoreAccessValueResource1"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type="text" id="txtStoreAccessValue" name="txtNameValidate" class="sfInputbox required" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblReason" Text="Reason:" runat="server" 
                                CssClass="cssClassLabel" meta:resourcekey="lblReasonResource1"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <textarea id="txtReason" cols="30" rows="6" name="msg" class="cssClassTextarea required"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStatus" Text="Status:" runat="server" 
                                CssClass="cssClassLabel" meta:resourcekey="lblStatusResource1"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type="radio" id="chkStatusActive" class="cssClassRadioBtn" name="status" checked="checked" />
                            <span>Active</span>
                            <input type="radio" id="chkStarusDisActive" class="cssClassRadioBtn" name="status" />
                            <span>Inactive</span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="sfButtonwrapper">
            <p>
                    <button type="button" id="btnCancelSaveUpdate" class="sfBtn">
                        <span class="sfLocale icon-close">Cancel</span></button>
                </p>
                <p>
                    <button type="button" id="btnSubmit" class="sfBtn">
                        <span class="sfLocale icon-save">Save</span></button>
                </p>
                
            </div>
        </div>
    </div>
</div>
<div id="hdnField">
</div>
