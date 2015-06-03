<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsLetterSetting.ascx.cs"
    Inherits="Modules_NewsLetter_NewsLetterSetting" %>
    <script type="text/javascript">
        var UserModuleID = '<%=UserModuleID %>';
        var PortalID = '<%=PortalID %>';
        var NewsLetterPath = '<%=ModulePath %>';
        var UserName = '<%=UserName %>';
        
    </script>
<div id="divSetting" class="sfFormwrapper">
    <table>
        <tr>
            <td>
                <label class="sfFormlabel">
                    Module Header</label>
            </td>
            <td>
                :
            </td>
            <td>
                <input id="txtNewsLetterHeader" name="Header" type="text" class="sfInputbox" />
            </td>
        </tr>
        <tr>
            <td>
                <label class="sfFormlabel">
                    Module Description</label>
            </td>
            <td>
                :
            </td>
            <td>
                <input type="text" name="Description" id="txtNLDescription" class="sfInputbox" />
            </td>
        </tr>
        <tr>
        <td><label class="sfFormlabel">UnSubscribe Page Name</label></td>
        <td>:</td>
        <td><input type="text" name="pagename" id="txtPageName" class="sfInputbox" /></td>
        </tr>
        <tr style="display:none">
            <td>
                <label class="sfFormlabel">
                    Subscription by mobile also</label>
            </td>
            <td>
                :
            </td>
            <td>
                <input value="ByMobile" name="ByMobile" type="checkbox" id="chkByMobile" />
            </td>
        </tr>
    </table>
    <div class="sfButtonwrapper sftype1">
        <label id="btnSaveNLSetting" class="sfSave">
            Save</label>
    </div>
</div>
