<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Aspx_Component.ascx.cs" Inherits="Modules_AspxCommerce_AspxKitManagement_Aspx_Component" %>

<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxKitManagement
        });
    });
    var umi = '<%=UserModuleID%>';
    //]]>

</script>


<div id="dvKits">

    <div class="cssClassHeader">
        <h1>
            <asp:Label ID="lblTitle" runat="server"  meta:resourcekey="lblTitleResource1"></asp:Label>
        </h1>
        <div class="cssClassHeaderRight">
            <div class="sfButtonwrapper">
                <p>
                    <button class="sfBtn" id="btnDeleteAllKit" type="button">
                        <span class="sfLocale icon-delete">Delete All Selected</span></button>
                </p>
                <p>
                    <button class="sfBtn" id="btnAddNewKit" type="button">
                        <span class="sfLocale icon-addnew">Add Kit</span></button>
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
                                    Kit Name:</label>
                                <input type="text" id="txtSrchKitName" class="sfTextBoxSmall" />
                            </td>
                            <td>   <button type="button" id="btnSearchKit" class="sfBtn">
                                            <span class="sfLocale icon-search">Search</span></button>
                                                              </td>

                        </tr>
                    </table>
                </div>
                <table id="gdvKitList" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    
</div>



<div id="dvEditKit" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblCouponManageTitle" Text="Save Kit" runat="server" meta:resourcekey="lblCouponManageTitleResource1"></asp:Label>
            </h1>
        </div>
        <div class="sfFormwrapper">
            <table border="0" width="100%" id="" class="cssClassPadding">
                <tr>
                    <td>
                        <asp:Label ID="lblKitName" Text="Kit Name:" runat="server" CssClass="cssClassLabel" meta:resourcekey="lblKitNameResource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtKitName" class="sfInputbox required" name="kitname" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" Text="Default Price:" runat="server" CssClass="cssClassLabel" meta:resourcekey="Label1Resource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtKitPrice" class="sfInputbox required decimal" name="price" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" Text="Default Quantity:" runat="server" CssClass="cssClassLabel" meta:resourcekey="Label2Resource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtKitQuantity" class="sfInputbox required integer" name="quantity" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" Text="Default Weight:" runat="server" CssClass="cssClassLabel" meta:resourcekey="Label3Resource1"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtKitWeight" class="sfInputbox required decimal" name="weight" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" Text="Choose KitComponent:" runat="server" CssClass="cssClassLabel" meta:resourcekey="Label4Resource1"></asp:Label>
                    </td>
                    <td>
                        <select id="ddlKitComponent" class="sfListmenu">
                        </select>
                        <button type="button" id="btnAddNewComponent" class="sfBtn">
                            <span class="sfLocale icon-addnew">Create New</span></button>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sfButtonwrapper">
            <p>
                <button type="button" id="btnCancel" class="sfBtn">
                    <span class="sfLocale icon-close">Cancel</span></button>
            </p>
            <p>
                <button type="button" id="btnSave" class="sfBtn">
                    <span class="sfLocale icon-search">Save</span></button>
            </p>
        </div>
    </div>

</div>

<div class="cssClassCommonBox Curve" style="display: none" id="dvNewComponent">
    <div class="cssClassHeader">
        <h3>
            <label>Add New Component</label></h3>

    </div>
    <div class="sfFormwrapper">
        <table width="100%" border="0" id="tblNewComponent" class="cssClassPadding">
            <tbody>
                <tr>
                    <td>Name</td>
                    <td>
                        <input type="text" id="txtNewComponent" class="sfInputbox required" name="component">
                    </td>
                </tr>
                <tr>
                    <td>Input Type</td>
                    <td>
                        <select id="ddlComponentType">
                            <option value="2">Radio</option>
                            <option value="3">CheckBox</option>
                        </select>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="sfButtonwrapper">
     <button class="sfBtn icon-close" id="btnComponentCancel" type="button">Cancel</button>
        <button class="sfBtn icon-save" id="btnComponentSave" type="button">Save</button>
       
    </div>
</div>



