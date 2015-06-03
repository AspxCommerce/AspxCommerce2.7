<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvanceSearchSetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxAdvanceSearch_AdvanceSearchSetting" %>
<div>
    <h3 class="sfLocale">
       Advance Search Module Setting
    </h3>
    <table>
     <tr>
            <td>
                <span class="sfLocale">Enable Advance Search:</span>
            </td>
            <td>
                <input type="checkbox" id="chkEnableAdvanceSearch" />
            </td>
        </tr>
         <tr>
            <td>
                <span class="sfLocale">Enable Brand Search:</span>
            </td>
            <td>
                <input type="checkbox" id="chkEnableBrandSearch" />
            </td>
        </tr>
          <tr>
            <td>
                <span class="sfLocale">No Of Items in a Row:</span>
            </td>
            <td>
                <input type="text" id="txtNoOfItemsInARow"/>
            </td>
        </tr> 
        <tr>
            <td>
                <span class="sfLocale">Advance Search Page Name:</span>
            </td>
            <td>
                <input type="text" id="txtAdvanceSearchPage" disabled="disabled"/>
            </td>
        </tr> 
          <tr>
            <td>
                <input type="button" class="sfLocale" id="btnAdvanceSearchSettingSave" value="Save" />
            </td>
        </tr>     
    </table>
</div>
<script type="text/javascript">
  //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AdvanceSearchLang
        });
        $(this).AdvanceSearchSetting({
            AdvanceSearchModulePath: "<%=AdvanceSearchModulePath %>"
        });
    });
  //]]>
</script>