<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsTags.ascx.cs" Inherits="Modules_AspxCommerce_AspxTagsManagement_AspxItemsTags" %>

<script type="text/javascript">
    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxTagsManagement
        });
    });
    lblShowItemTagHeading = '<%=lblShowItemTagHeading.ClientID %>';
    var umi = '<%=UserModuleID%>';
    //]]>
</script>

<div id="divItemTagDetails">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTagHeading" runat="server" Text="Items Tags" meta:resourcekey="lblTagHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <asp:Button ID="btnExportDataToExcelItemTag" runat="server" OnClick="Button1_Click"
                            Text="Export to Excel" CssClass="cssClassButtonSubmit" meta:resourcekey="btnExportDataToExcelItemTagResource1" />
                    </p>
                    <p>
                        <asp:Button ID="btnExportItemTag" runat="server" class="cssClassButtonSubmit" OnClick="ButtonItemTag_Click"
                            Text="Export to CSV" meta:resourcekey="btnExportItemTagResource1" />
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="loading">
                    <img id="ajaxItemTagsImage2" src="" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvItemTag" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="ItemTagsExportDataTbl" width="100%" border="0" cellpadding="0" cellspacing="0"
                    style="display: none">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divShowItemsTagsList" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblShowItemTagHeading" runat="server" meta:resourcekey="lblShowItemTagHeadingResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnBack" class="sfBtn">
                            <span class="sfLocale icon-arrow-slim-w">Back</span>
                        </button>
                    </p>
                    <p>
                        <asp:Button ID="btnExportToExcelItemTag" runat="server" OnClick="Button2_Click" Text="Export to Excel"
                            CssClass="cssClassButtonSubmit" meta:resourcekey="btnExportToExcelItemTagResource1" />
                    </p>
                    <p>
                        <asp:Button ID="btnExportToCSV" runat="server" class="cssClassButtonSubmit" OnClick="ButtonItemTagDetail_Click"
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
                    <img id="ajaxItemTagsImage" src="" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvShowItemTagsList" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
                <table id="ShowItemTagListExportDataTbl" width="100%" border="0" cellpadding="0"
                    cellspacing="0" style="display: none">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<asp:HiddenField ID="HdnGridData" runat="server" />
<asp:HiddenField ID="_csvItemTagHdnValue" runat="server" />
<asp:HiddenField ID="_csvItemTagDetailHdnValue" runat="server" />
