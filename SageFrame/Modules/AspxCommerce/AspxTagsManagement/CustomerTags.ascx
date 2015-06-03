<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerTags.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxTagsManagement_CustomerTags" %>

<script type="text/javascript">
    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxTagsManagement
        });
    });
    var btnDataExcel = '<%= btnExportDataToExcel.ClientID %>';
    var btnExcel = '<%= btnExportToExcel.ClientID %>';
    var lblShowHeading = '<%=lblShowHeading.ClientID %>'; var umi = '<%=UserModuleID%>';
    //]]>
</script>

<div id="divCustomerTagDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTagHeading" runat="server" Text="Customers Tags" meta:resourcekey="lblTagHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <asp:Button ID="btnExportDataToExcel" runat="server" OnClick="Button1_Click" Text="Export to Excel"
                            CssClass="cssClassButtonSubmit" meta:resourcekey="btnExportDataToExcelResource1" />
                    </p>
                    <p>
                        <asp:Button ID="btnExport" runat="server" class="cssClassButtonSubmit" OnClick="ButtonCustomerTags_Click"
                            Text="Export to CSV" meta:resourcekey="btnExportResource1" />
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <%--<div class="cssClassSearchPanel sfFormwrapper">
          <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td><label class="cssClassLabel"> Cost Variant Name:</label>
                <input type="text" id="txtVariantName" class="sfTextBoxSmall" /></td>
              <td><div class="sfButtonwrapper cssClassPaddingNone">
                  <p>
                    <button type="button" onclick="SearchCostVariantName()"> <span><span>Search</span></span></button>
                  </p>
                </div></td>
            </tr>
          </table>
        </div>--%>
                <div class="loading">
                    <img id="ajaxCustomerTagsImage2" src="" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvCusomerTag" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="CustomerTagExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0"
                    style="display: none">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divShowCustomerTagList" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblShowHeading" runat="server" meta:resourcekey="lblShowHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnBack" class="sfBtn">
                            <span class="sfLocale icon-arrow-slim-w">Back</span>
                        </button>
                    </p>
                    <p>
                        <asp:Button ID="btnExportToExcel" runat="server" OnClick="Button2_Click" Text="Export to Excel"
                            CssClass="cssClassButtonSubmit" meta:resourcekey="btnExportToExcelResource1" />
                    </p>
                    <p>
                        <asp:Button ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit" OnClick="ButtonCustomerTagsDetail_Click"
                            Text="Export to CSV" meta:resourcekey="btnExportToCSVResource1" />
                    </p>
                    
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="loading">
                    <img id="ajaxCustomerImageLoad" src="" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="grdShowTagsList" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="ShowTagListExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0"
                    style="display: none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="HdnGridData" runat="server" />
<asp:HiddenField ID="_csvCustomerTagHdn" runat="server" />
<asp:HiddenField ID="_csvCustomerTagDetailHdn" runat="server" />
