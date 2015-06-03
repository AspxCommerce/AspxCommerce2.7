<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AspxIndexManagement.ascx.cs" Inherits="Modules_AspxCommerce_AspxIndexManagement_AspxIndexManagement" %>
<script type="text/javascript">
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxIndexManagementLocal
        });
    });
    var umi = '<%=UserModuleID%>';
</script>
<style type="text/css">
    #divRight {
        width: 100%;
    }
</style>

<div id="divIndexManagement">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="Label1" runat="server" Text="Index Management" meta:resourcekey="lblIndexMgmtResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnReIndexAll" class="sfBtn">
                            <span class="icon-index sfLocale">Re-Index All Tables</span>
                        </button>
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <div class="loading">
                    <img id="ajaxCutomerMgmtImageLoad" src="" alt="loading...." title="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvIndexedTables" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>

