<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryManage.ascx.cs"
    Inherits="Modules_AspxCategoryManagement_CategoryManage" %>

<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxCategoryManagement
        });
        $("#imgDelete").attr("src", aspxTemplateFolderPath + '/images/admin/commondeletebtn.png');
        $("#imgAdd").attr("src", aspxTemplateFolderPath + '/images/admin/imgadd.png');
    });


    var categoryLargeThumbImageHeight = '<%=CategoryLargeThumbImageHeight %>';
    var categoryLargeThumbImageWidth = '<%=CategoryLargeThumbImageWidth %>';
    var categoryMediumThumbImageHeight = '<%=CategoryMediumThumbImageHeight %>';
    var categoryMediumThumbImageWidth = '<%=CategoryMediumThumbImageWidth %>';
    var categorySmallThumbImageHeight = '<%=CategorySmallThumbImageHeight %>';
    var categorySmallThumbImageWidth = '<%=CategorySmallThumbImageWidth %>';
    var categoryTitleLabel = '<%=lblCategoryTitle.ClientID %>';
    var banerWidth = '<%=BannerWidth %>';
    var bannerHeight = '<%=BannerHeight %>';
    var umi = '<%=UserModuleID%>';
    //]]>
</script>
<style type="text/css">
    .ui-tabs .ui-tabs-nav li a {
        padding: 12px 10px;
    }
</style>
<div>
    <input type="hidden" id="hdnCatNameTxtBox" />
</div>
<div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="cssClassTableLeftCol" width="270px;" style="vertical-align: top;">
                <div class="cssClassSideBox">
                    <div class="cssClassSideBoxNavi Curve">
                        <h3 class="sfLocale">Available Categories</h3>
                        <br />


                        <div id="CategoryTree_Container">
                        </div>
                    </div>
                </div>
            </td>
            <td>
                <div>
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h1>
                                <asp:Label ID="lblCategoryTitle" runat="server" Text="Category (ID: " meta:resourcekey="lblCategoryTitleResource1"></asp:Label><span
                                    id="lblCategoryID">0</span>
                                <asp:Label ID="lblclosing" runat="server" Text=" )" meta:resourcekey="lblclosingResource1"></asp:Label>
                            </h1>
                            <div class="cssClassLanguageSettingWrapper">
                                 <span class="sfLocale">Select Langauge:</span>
                                        <asp:Literal ID="languageSetting" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                            <div class="cssClassHeaderRight">
                                <div class="sfButtonwrapper ">
                                    <p>
                                        <button type="button" class="cssClassAddCategory sfBtn" onclick="categoryMgmt.AddCategory()">
                                            <span class="sfLocale icon-addnew">Add Category</span></button>
                                    </p>
                                    <p>
                                        <button type="button" class="cssClassAddSubCategory sfBtn" onclick="categoryMgmt.AddSubCategory()">
                                            <span class="sfLocale icon-addnew">Add Sub Category</span></button>
                                    </p>
                                    <p>
                                        <button type="button" id="btnActivateCategory sfBtn" style="display: none;" class="cssClassActivateCategory"
                                            onclick="categoryMgmt.ActivateCategory()">
                                            <span id="spanActivate" class="sfLocale icon-complete">Activate</span></button>
                                    </p>
                                    <p>
                                        <button type="button" id="btnDeActivateCategory sfBtn" style="display: none;" class="cssClassDeActivateCategory"
                                            onclick="categoryMgmt.DeActivateCategory()">
                                            <span id="spanDeActivate" class="sfLocale icon-deactivate">Deactivate</span></button>
                                    </p>
                                    <div class="cssClassClear">
                                    </div>
                                </div>
                            </div>
                            <div class="cssClassClear">
                            </div>
                        </div>
                          
                        <div class="cssClassTabPanelTable">
                            <input type="hidden" id="CagetoryMgt_categoryID" value="0" />
                            <input type="hidden" id="CagetoryMgt_parentCagetoryID" value="0" />
                            <div id="dynForm">
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div class="cssClassClear">
    </div>
</div>
<div class="popupbox" id="popuprel6">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span class="sfLocale">Close</span></button>
    </div>
    <div class="sfGridWrapperContent">
        <div class="cssClassCommonBox " style="width: 400px;">
            <div class="cssClassHeader">
                <h2>
                    <asp:Label ID="lblAddProvider" runat="server" Text="Add Provider to Branch" meta:resourcekey="lblAddProviderResource1"></asp:Label>
                </h2>
            </div>
        </div>
        <div class="cssClassBranch">

            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblBranchHeader" runat="server" Text="Branch Name:" meta:resourcekey="lblBranchHeaderResource1"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td>
                            <select class="cssClassStoreCollection sfListmenu">
                                <option>--</option>
                            </select>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="sfClearlog">
        </div>
        <div id="divProviderTbl" class="sfGridwrapper" style="display: none">
            <div class="sfGridWrapperContent">
                <div class="cssProviderAddDelete">
                    <p class="cssDeleteSelectedProvider " style="cursor: pointer">
                        <img alt="Delete Selected" title="Delete Selected" id="imgDelete" class="sfLocale" />&nbsp;Delete
                        Selected
                    </p>
                    <p class="cssAddServiceProvider" style="cursor: pointer">
                        <img alt="Add Provider" title="Add Provider" id="imgAdd" class="sfLocale" />&nbsp;Add
                        Provider
                    </p>
                </div>
                <table id="tblBranchProviders" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
        <div class="cssServiceProviderTbl" style="display: none">
            <table cellspacing="0" cellpadding="o" border="0" width="100%">
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="lblServiceProviderName" runat="server" Text="Provider Name:" meta:resourcekey="lblServiceProviderNameResource1"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </label>
                    </td>
                    <td>
                        <input type="text" class="sfInputbox" id="txtServiceProviderName" />
                        <span class="providerNameError" style="color: red;">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <asp:Label ID="lblProviderNickName" runat="server" Text="Provider Nick Name:" meta:resourcekey="lblProviderNickNameResource1"></asp:Label>
                        </label>
                    </td>
                    <td>
                        <input type="text" class="sfInputbox" id="txtServiceProviderNickName" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <span class="providerBranchNameError" style="color: red;"></span>
                    </td>
                </tr>
                <tbody>
                </tbody>
            </table>
            <div class="sfButtonwrapper">
                <p>
                    <button type="button" id="btnSaveServiceProvider" value="0" class="sfBtn">
                        <span class="sfLocale">Save</span></button>
                </p>
                <p>
                    <button type="button" id="btnCancelProviderAdd" class="sfBtn">
                        <span class="sfLocale">Cancel</span></button>
                </p>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
        <div class="sfButtonwrapper">

            <p>
                <button type="button" id="btnCancelServiceProvider" style="display: none" class="sfBtn">
                    <span class="sfLocale icon-close">Cancel</span></button>
            </p>
            <div class="cssClassClear">
            </div>
        </div>
    </div>
</div>

<div class="popupbox cropBox" id="popuprel1">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span class="sfLocale">Close</span></button>
    </div>
    <div class="sfGridWrapperContent">
        <div class="cssClassCommonBox">
            <div class="cssClassHeader">
                	<h2>
                    	<asp:Label ID="lblBannerImage" runat="server" Text="Crop Banner Image" meta:resourcekey="lblBannerImageResource1"></asp:Label>
                	</h2>
            </div>
            <div class="jc-demo-box clearfix ">
                <div class="sfFloatLeft" style="width: 48%;">
                    <div id="dvCropImage">
                    </div>
                    <div id="divCropValue" style="display: none;" class="cssCropValueWrapper">
                        <label class="cssCropValueLabel">X1
                            <input type="text" name="X1" id="X1" size="4" style="width: 80px;" /></label>
                        <label class="cssCropValueLabel">Y1
                            <input type="text" name="X2" id="X2" size="4" style="width: 80px;" /></label><br />

                        <label class="cssCropValueLabel">X2
                            <input type="text" name="Y1" id="Y1" size="4" style="width: 80px;" /></label>
                        <label class="cssCropValueLabel">Y2
                            <input type="text" name="Y2" id="Y2" size="4" style="width: 80px;" /></label><br />

                        <label class="cssCropValueLabel">W
                            <input type="text" name="W" id="W" size="4" style="width: 80px;" /></label>
                        <label class="cssCropValueLabel">H
                            <input type="text" name="H" id="H" size="4" style="width: 80px;" /></label>
                    </div>
                </div>
                <div id="dvCropPreview" class="sfFloatRight" style="width: 48%;">
                    <div class="preview-container">
                    </div>
                </div>
            </div>
        </div>
        <div class="sfButtonwrapper clearfix">
            <p style="float: right;">
                <button id="btnCropForBanner" type="button" class="sfBtn">
                    <span class="sfLocale icon-crop">Crop For Banner</span></button>
            </p>
        </div>
    </div>

    <div class="clear">
    </div>
</div>

