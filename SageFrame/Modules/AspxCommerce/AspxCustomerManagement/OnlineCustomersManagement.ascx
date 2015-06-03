<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OnlineCustomersManagement.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxCustomerManagement_OnlineCustomersManagement" %>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxCustomerManagement
        });
    });


    //]]>
</script>

<div id="divAttrForm">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblAttrFormHeading" runat="server" Text="Online Customers"
                    meta:resourcekey="lblAttrFormHeadingResource1"></asp:Label>
            </h1>
        </div>
        <div class="cssClassTabPanelTable">
            <div id="container-7">
                <ul>
                    <li><a href="#fragment-1">
                        <asp:Label ID="lblTabTitle1" runat="server" Text="Registered Customers"
                            meta:resourcekey="lblTabTitle1Resource1"></asp:Label>
                    </a></li>
                    <li><a href="#fragment-2">
                        <asp:Label ID="lblTabTitle2" runat="server" Text="Anonymous Users"
                            meta:resourcekey="lblTabTitle2Resource1"></asp:Label>
                    </a></li>
                </ul>
                <div id="fragment-1">
                    <div>
                        <div id="divRegisteredUsers">
                            <div class="cssClassCommonBox Curve">
                                <div class="sfGridwrapper">
                                    <div class="sfGridWrapperContent">
                                        <div class="sfFormwrapper sfTableOption">
                                            <table border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <label class="cssClassLabel sfLocale">
                                                            User Name:</label>
                                                        <input type="text" id="txtSearchUserName1" class="sfTextBoxSmall" />
                                                    </td>
                                                    <td>
                                                        <label class="cssClassLabel sfLocale">
                                                            Host Address:</label>
                                                        <input type="text" id="txtSearchHostAddress1" class="sfTextBoxSmall" />
                                                    </td>
                                                    <td>
                                                        <label class="cssClassLabel sfLocale">
                                                            Browser Name:</label>
                                                        <input type="text" id="txtBrowserName1" class="sfTextBoxSmall" />
                                                    </td>
                                                    <td>
                                                        <button type="button" id="btnSearchRegisteredUser" class="sfBtn">
                                                            <span class="sfLocale icon-search">Search</span></button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="loading">
                                            <img id="ajaxCustomerOnline" src="" alt="loading...." />
                                        </div>
                                        <div class="log">
                                        </div>
                                        <table id="gdvOnlineRegisteredUser" cellspacing="0" cellpadding="0" border="0" width="100%">
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="fragment-2">
                    <div>
                        <div id="divAnonymousUser">
                            <div class="cssClassCommonBox Curve">
                                <div class="sfGridwrapper">
                                    <div class="sfGridWrapperContent">
                                        <div class="sfFormwrapper sfTableOption">
                                            <table border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <label class="cssClassLabel sfLocale">
                                                            Host Address:</label>
                                                        <input type="text" id="txtSearchHostAddress0" class="sfTextBoxSmall" />
                                                    </td>
                                                    <td>
                                                        <label class="cssClassLabel sfLocale">
                                                            Browser Name:</label>
                                                        <input type="text" id="txtBrowserName0" class="sfTextBoxSmall" />
                                                    </td>
                                                    <td>
                                                        <button type="button" id="btnSearchAnonymousUser" class="sfBtn">
                                                            <span class="sfLocale icon-search">Search</span></button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="loading">
                                            <img id="ajaxCustomerOnlie2" src="" alt="loading...." />
                                        </div>
                                        <div class="log">
                                        </div>
                                        <table id="gdvOnlineAnonymousUser" cellspacing="0" cellpadding="0" border="0" width="100%">
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
