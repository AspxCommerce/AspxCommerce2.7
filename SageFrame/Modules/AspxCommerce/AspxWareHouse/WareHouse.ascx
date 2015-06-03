<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WareHouse.ascx.cs" Inherits="Modules_AspxCommerce_AspxWareHouse_WareHouse" %>

<script type="text/javascript">
    //<![CDATA[
    var UserDownloadable = "";
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxWareHouse
        });
    });
    //]]>
</script>

<input id="hdnWHouseID" type="hidden" value="0" />
<div id="dvGridForm">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblHeadingWareHouse" runat="server" CssClass="cssClassLabel" Text="Store WareHouse"
                    meta:resourcekey="lblHeadingWareHouseResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                   <p>
                        <button type="button" id="btnAddWareHouse" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Ware House</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelectedWareHouse" class="sfBtn" >
                            <span class="sfLocale icon-delete">Delete All Selected</span>
                        </button>
                    </p>
                    
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="loading">
                    <img id="ajaxStoreAccessImage3" src="" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvWareHouse" cellspacing="0" cellpadding="0" border="0" width="100%"">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="popuprel3" class="popupbox adminpopup" style="display: none">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span class="sfLocale">Close</span></button>
    </div>
    <div class="sfFormwrapper">
        <div class="cssClassHeader">
            <h2 class="sfLocale">
                Add WareHouse</h2>
        </div>
        <div class="cssClassCommonCenterBoxTable">
            <table cellpadding="0" cellspacing="0" border="0">
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text="Name" CssClass="sfFormlabel" meta:resourcekey="lblNameResource1"></asp:Label>
                        </td>
                        <td>
                            <input type="text" id="txtName" class="required sfInputbox sfLocale" name="storeName" />
                        </td>
                        <td>
                            <asp:Label ID="lblCity" runat="server" Text="City:" CssClass="sfFormlabel" meta:resourcekey="lblCityResource1"></asp:Label>
                        </td>
                        <td>
                            <input type="text" id="txtCity" class="required sfInputbox" name="city" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStreetAddress" runat="server" Text="Street Address 1" CssClass="sfFormlabel" meta:resourcekey="lblStreetAddressResource2"></asp:Label>
                        </td>
                        <td>
                            <input type="text" id="txtStreetAddress1" class="required sfInputbox " name="streetAddress" />
                        </td>
                        <td>
                            <asp:Label ID="lblStreetAddress2" runat="server" Text="Street Address 2" CssClass="sfFormlabel" meta:resourcekey="lblStreetAddress2Resource2"></asp:Label>
                        </td>
                        <td>
                            <input type="text" id="txtStreetAddress2" class="sfInputbox " name="streetAddress2" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCountry" runat="server" Text="Country:" CssClass="sfFormlabel"
                                meta:resourcekey="lblCountryResource1"></asp:Label>
                        </td>
                        <td>
                            <select id="ddlCountry" class="sfListmenu ">
                            </select>
                        </td>
                        <td>
                            <asp:Label ID="lblPostalCode" runat="server" Text="Zip /PostalCode:" CssClass="sfFormlabel"
                                meta:resourcekey="lblPostalCodeResource1"></asp:Label>
                        </td>
                        <td>
                            <input type="text" id="txtPostalCode" class="required sfInputbox sfLocale" name="postalCode" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblState" runat="server" Text="State/Province" CssClass="sfFormlabel"
                                meta:resourcekey="lblStateResource1"></asp:Label>
                        </td>
                        <td>
                            <select id="ddlState" class="sfListmenu">
                                <option value="AL" class="sfLocale">Choose One</option>
                            </select>
                            <input type="text" id="txtState" name="State" class="required sfInputbox" name="state"
                                style="display: none;" />
                        </td>
                        <td>
                            <asp:Label ID="lblPhone" runat="server" Text="Phone:" CssClass="sfFormlabel" meta:resourcekey="lblPhoneResource1"></asp:Label>
                        </td>
                        <td>
                            <input type="text" id="txtPhone" maxlength="30" class="sfInputbox" name="phone" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFax" runat="server" Text="Fax:" CssClass="sfFormlabel" meta:resourcekey="lblFaxResource1"></asp:Label>
                        </td>
                        <td>
                            <input type="text" id="txtFax" maxlength="30" class="sfInputbox" name="fax" />
                        </td>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="sfFormlabel" meta:resourcekey="lblEmailResource1"></asp:Label>
                        </td>
                        <td>
                            <input type="text" id="txtEmail" maxlength="100" class="email sfInputbox" name="email" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIsPrimary" runat="server" Text="Default:" CssClass="sfFormlabel"
                                meta:resourcekey="lblIsPrimaryResource1"></asp:Label>
                        </td>
                        <td>
                            <input type="checkbox" id="chkIsPrimary" class="sfCheckBox" name="check" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <button type="button" id="btnCancel" class="sfBtn">
                <span class="sfLocale icon-close">Cancel</span>
            </button>
            <button type="button" id="btnSave" class="sfBtn">
                <span class="sfLocale icon-save">Save</span>
            </button>
        </div>
    </div>
</div>

