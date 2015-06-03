<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LogoSetting.ascx.cs" Inherits="Modules_Logo_LogoSetting" %>

<script type="text/javascript">
    //<![CDATA[    
    var moduleID = '<%=moduleID%>';
    var portalID = '<%=portalID%>';
    var resolvedUrl = '<%=resolvedUrl%>';
    var currentDirectory = '<%=currentDirectory%>';
    var culture = '<%=culture %>';
    //]]>	
</script>

<div class="sfFormwrapper sfPadding">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="15%">
                <label class="sfFormlabel">
                    Logo</label>
            </td>
            <td>
                <input type="file" id="fluLogo" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <div id="divLogoIcon" style="max-height: 150px">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblLogoText" CssClass="sfFormlabel" runat="server" Text="Logo Text"></asp:Label>
            </td>
            <td>
                <textarea id="txtLogoText" class="sfTextarea"></textarea>
            </td>
        </tr>
        <tr>
            <td>
                <label class="sfFormlabel">
                    Navigate URL</label>
            </td>
            <td>
                <input type="text" id="txtUrl" class="sfInputbox" value="http://" />
            </td>
        </tr>
        <tr>
            <td>
                <label class="sfFormlabel">
                    Slogan</label>
            </td>
            <td>
                <textarea id="txtSlogan" class="sfTextarea"></textarea>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <div class="sftype1">
                    <label id="btnSaveLogo" class="sfLocale icon-save sfBtn">
                        Save</label>
                </div>
            </td>
        </tr>
    </table>
</div>
