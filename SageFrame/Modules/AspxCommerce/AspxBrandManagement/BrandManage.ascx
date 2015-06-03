<%@ Control Language="C#" AutoEventWireup="true"  CodeFile="BrandManage.ascx.cs" Inherits="Modules_AspxCommerce_AspxBrandManagement_BrandManage" %>

<script type="text/javascript">
    var maxFileSize ='<%=maxFileSize%>';
    $(function()  {
        $(".sfLocale").localize({
            moduleKey: Brand
        });
    });
    var umi = '<%=UserModuleId%>';
</script>
<div id="divBrandManage">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblBrandManageHeading" runat="server" Text="Manage Brands" 
                    meta:resourcekey="lblBrandManageHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnShow" class="sfBtn">
                           <span class="sfLocale icon-showall">Show All</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddNewBrand" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Brand</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelected" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span></button>
                    </p>
                </div>
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="sfFormwrapper sfTableOption">
                    <table border="0" cellspacing="0" cellpadding="0" >
                        <tr>
                           <td>
                                <label class="cssClassLabel sfLocale">
                                    Brand Name:</label>
                                <input type="text" id="txtSearchBrandName" class="sfTextBoxSmall" />
                            </td>
                            <td>
                                       <button type="button" id="btnSearch" class="sfBtn">
                                           <span class="sfLocale icon-search">Search</span></button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxImageLoad" />
                </div>
                <div class="log">
                </div>
                <table id="tblBrandManage" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<br />
<div id="divBrandProviderForm" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <span id="_lblEditBrandName" class="cssClassLabel"></span>
            </h2>
        </div>
         <div class="cssClassLanguageSettingWrapper">
                                 <span class="sfLocale">Select Langauge:</span>
                                        <asp:Literal ID="languageSetting" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
        <div class="sfFormwrapper">
            <table id="tblEditBrandManage" border="0" class="cssClassPadding tdpadding" width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblBrandName" runat="server" CssClass="cssClassLabel" 
                            Text="Brand Name:" meta:resourcekey="lblBrandNameResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input id="txtBrandName" class="sfInputbox required" name="brandName"
                            type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblBrandDescription" runat="server" CssClass="cssClassLabel" 
                            Text="Brand Description" meta:resourcekey="lblBrandDescriptionResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <textarea id="editor1" class="ckeditor cssClassTextArea dynFormItem"
                            name="editor1"></textarea>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="lblIsShowInSlider" runat="server" Text="Slider View:"
                            CssClass="cssClassLabel" meta:resourcekey="lblIsShowInSliderResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlIsShowInSlider" class="sfListmenu">
                            <option value="0" class="sfLocale">NO</option>
                            <option value="1" class="sfLocale">YES</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIsFeatured" runat="server" Text="Featured:" 
                            CssClass="cssClassLabel" meta:resourcekey="lblIsFeaturedResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlIsFeatured" class="sfListmenu">
                            <option value="0" class="sfLocale">NO</option>
                            <option value="1" class="sfLocale">YES</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFeaturedFrom" runat="server" Text="Featured From:" 
                            CssClass="cssClassLabel" meta:resourcekey="lblFeaturedFromResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input id="txtFeatureFrom" class="sfInputbox " name="featureFrom" type="text" disabled="disabled" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFeaturedTo" runat="server" Text="Featured To:" 
                            CssClass="cssClassLabel" meta:resourcekey="lblFeaturedToResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input id="txtFeatureTo" class="sfInputbox " name="featureTo" type="text" disabled="disabled" />
                    </td>
                </tr>
                <tr id="image">
                    <td>
                        <asp:Label ID="lblBrandImgUrl" runat="server" CssClass="cssClassLabel" 
                            Text="Upload Brand Image" meta:resourcekey="lblBrandImgUrlResource1"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input id="txtBrandImageUrl" class="cssClassBrowse" type="file" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divBrandImage">
                        </div>
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
                    <span class="sfLocale icon-save">Save</span></button>
            </p>
        </div>
    </div>
</div>