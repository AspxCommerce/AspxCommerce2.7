<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderStatusManagement.ascx.cs"
    Inherits="Modules__ModulesInstalltions_AspxOrderStatusManagement_OrderStatusManagement" %>

<script type="text/javascript">

    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxOrderManagement
        });
    });
    var lblHeading = "<%=lblHeading.ClientID %>";
    var umi = '<%=UserModuleID%>';
    //]]>
</script>

<div id="divOrderStatusDetail">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblOrderStatusHeading" runat="server" Text="Manage Order Status"
                    meta:resourcekey="lblOrderStatusHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnAddNew" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Order Status</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelected" class="sfBtn">
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
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <%--<td>
                                <asp:Label ID="lblOrderID" runat="server" CssClass="cssClassLabel" Text="Order ID:"></asp:Label>
                                <input type="text" id="txtOrderID" class="sfTextBoxSmall" />
                            </td>--%>
                            <td>
                                <asp:Label ID="lblStatusName" runat="server" CssClass="cssClassLabel"
                                    Text="Status Name:" meta:resourcekey="lblStatusNameResource1"></asp:Label>
                                <input type="text" id="txtOrderStateName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" CssClass="cssClassLabel"
                                    Text="Active:" meta:resourcekey="Label1Resource1"></asp:Label>
                                <select id="ddlVisibitity" class="sfListmenu">
                                    <option value="" class="sfLocale">--All--</option>
                                    <option value="True" class="sfLocale">Yes</option>
                                    <option value="False" class="sfLocale">No</option>
                                </select>
                            </td>
                            <td>
                                <button type="button" onclick="OrderStatusMgmt.SearchOrderStatus()" class="sfBtn">
                                    <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxOrderStatusMgmtImage" src="" title="loading...." alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="tblOrderStatusDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<div id="divEditOrderStatus" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblHeading" runat="server" Text="Edit Order Status ID:"
                    meta:resourcekey="lblHeadingResource1"></asp:Label>
            </h1>
        </div>
        <div class="sfFormwrapper">
             <div class="cssClassLanguageSettingWrapper">
                                 <span class="sfLocale">Select Langauge:</span>
                                        <asp:Literal ID="languageSetting" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
            <table cellspacing="0" cellpadding="0" border="0" class="cssClassPadding">
                <tr>
                    <td>
                        <asp:Label ID="lblOrderStatusName" runat="server" Text="Order Status Name:"
                            CssClass="cssClassLabel" meta:resourcekey="lblOrderStatusNameResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtOrderStatusAliasName" name="StatusName" class="sfInputbox required" minlength="2" /><span id="osErrorLabel"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAliasToolTip" runat="server" Text="Alias Tool Tip:"
                            CssClass="cssClassLabel" meta:resourcekey="lblAliasToolTipResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtAliasToolTip" name="ToolTipName" class="sfInputbox required" minlength="2" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAliasHelp" runat="server" Text="Alias Help:"
                            CssClass="cssClassLabel" meta:resourcekey="lblAliasHelpResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtAliasHelp" class="sfInputbox" />
                    </td>
                </tr>
                <tr id="isActiveTR">
                    <td>
                        <asp:Label ID="lblOrderStatusIsActive" runat="server" Text="Active:"
                            CssClass="cssClassLabel" meta:resourcekey="lblOrderStatusIsActiveResource1"></asp:Label>
                    </td>
                    <td>
                        <div id="chkIsActiveOrderStatus" class="cssClassCheckBox">
                            <input id="chkIsActiveOrder" type="checkbox" name="chkIsActive" />
                        </div>
                    </td>
                </tr>
                <tr id="Tr1">
                    <td>
                        <asp:Label ID="lblReduceQuantiry" runat="server" Text="Reduce Item Quantity:"
                            CssClass="cssClassLabel" meta:resourcekey="lblReduceQuantiryResource1"></asp:Label>
                    </td>
                    <td>
                        <div id="Div1" class="cssClassCheckBox">
                            <input id="chkIsReduceQuantity" type="checkbox" name="chkIsReduceQuantity" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnBack" class="sfBtn">
                    <span class="sfLocale icon-arrow-slim-w">Back</span></button>
            </p>
            <p>
                <button type="reset" id="btnReset" class="sfBtn">
                    <span class="sfLocale icon-refresh">Reset</span></button>
            </p>
            <p>
                <button type="button" id="btnSaveOrderStatus" class="sfBtn">
                    <span class="sfLocale icon-save">Save Status</span></button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
</div>
<input id="hdnIsSystem" type="hidden" />