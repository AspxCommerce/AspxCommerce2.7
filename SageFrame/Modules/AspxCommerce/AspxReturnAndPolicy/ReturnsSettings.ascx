<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReturnsSettings.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxReturnAndPolicy_ReturnsSettings" %>

<script type="text/javascript">
    var serverLocation = '<%=Request.ServerVariables["SERVER_NAME"]%>';
    var serverHostLoc = "http://" + serverLocation;
    var path = '<%=path%>';
    var umi = '<%=UserModuleID%>';
    $(function() {       
        $(".sfLocale").localize({
            moduleKey: AspxReturnAndPolicy
        });
    });   

</script>

<div id="divReturnManagement">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblReturnsManagementHeading" runat="server" 
                    meta:resourcekey="lblReturnsManagementHeadingResource1"></asp:Label>
            </h1>
        </div>
        
           
                <div class="cssClassSearchPanel sfFormwrapper">
                    <table breturn="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel sfLocale">
                                    Order For Return Expires in Day(s) :</label>
                            </td>
                            <td>
                                <input type="text" id="txtExpiresInDays" name="ExpiresInDays" class="sfInputbox required number"
                                    datatype="Integer" maxlength="5" />
                            </td>
                        </tr>
                       
                    </table>
                     
                </div>
                <div class="sfButtonwrapper cssClassPaddingNone">                                   
                                    <button type="button" id="btnSave" class="sfBtn icon-save">
                                        <span class="sfLocale">Save</span></button>
                                </div>
                <div class="log">
                </div>
                <div class="cssClassClear">
                </div>
            
        
    </div>
</div>
