<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MegaCategorySetting.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxMegaCategory_MegaCategorySetting" %>

<div class="cssMegaCategorySetting">
    <table>
        <thead>
            <tr>
                <th>
                   <h2 class="sfLocale">Mega Category Setting</h2>
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <asp:Label runat="server" EnableViewState="False" ID="lblModeOfView" 
                        Text="Mode Of Category" meta:resourcekey="lblModeOfViewResource1"></asp:Label>
                </td>
                <td>
                    <select id="slcMode" class="cssClassMode">
                        <option value="horizontal" class="sfLocale">Horizontal</option>
                        <option value="vertical" class="sfLocale">Vertical</option>
                        <option value="collapseable" class="sfLocale">Collapseable</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" EnableViewState="False" ID="lblNoOfColumn" 
                        Text="Number Of Column" meta:resourcekey="lblNoOfColumnResource1"></asp:Label>
                </td>
                <td>
                    <input type="text" id="txtNoOfColumn" name="NoOfColumn" class="required number" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" EnableViewState="False" Text="Show Category Image" 
                        meta:resourcekey="LabelResource1"></asp:Label>
                </td>
                <td>
                    <input type="checkbox" id="chkShowImage" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" EnableViewState="False" 
                        Text="Show Sub Category Image" meta:resourcekey="LabelResource2"></asp:Label>
                </td>
                <td>
                    <input type="checkbox" id="chkShowSubCatImage" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" EnableViewState="False" ID="lblEffect" Text="Effect" 
                        meta:resourcekey="lblEffectResource1"></asp:Label>
                </td>
                <td>
                    <select id="slcEffect">
                        <option value="show" class="sfLocale">Show</option>
                        <option value="slide" class="sfLocale">Slide</option>
                        <option value="fade" class="sfLocale">Fade</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" EnableViewState="False" ID="lblSpeed" Text="Speed" 
                        meta:resourcekey="lblSpeedResource1"></asp:Label>
                </td>
                <td>
                    <select id="slcSpeed" class="cssClassMode">
                        <option value="fast" class="sfLocale">fast</option>
                        <option value="slow" class="sfLocale">slow</option>                        
                    </select>                    
                </td>
            </tr>
            <tr id="trDirection" style="display: none;">
                <td>
                    <asp:Label runat="server" EnableViewState="False" ID="lblDirection" 
                        Text="Direction" meta:resourcekey="lblDirectionResource1"></asp:Label>
                </td>
                <td>
                    <select id="slcDirection">
                        <option value="right" class="sfLocale">Right</option>
                        <option value="left" class="sfLocale">Left</option>
                    </select>
                </td>
            </tr>
            <tr id="trEvent" style="display: none;">
                <td>
                    <asp:Label runat="server" EnableViewState="False" ID="lblEvent" Text="Event" 
                        meta:resourcekey="lblEventResource1"></asp:Label>
                </td>
                <td>
                    <select id="slcEvent">
                        <option value="click" class="sfLocale">Click</option>
                        <option value="hover" class="sfLocale">Hover</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" id="btnMenuCatSave" class="sfLocale sfBtn" value="Save" />
                </td>
            </tr>
        </tbody>
    </table>
</div>
<script type="text/javascript">
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxMegaCategory
        });
        var umi = '<%=UserModuleID%>';
        $(this).MegaCategorySetting({
            Settings: '<%=Settings %>', umi: umi
        });
    });
</script>
