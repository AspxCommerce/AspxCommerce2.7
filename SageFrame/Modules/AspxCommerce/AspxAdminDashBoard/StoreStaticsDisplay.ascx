<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreStaticsDisplay.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxAdminDashBoard_StoreStaticsDisplay" %>

<script type="text/javascript">
    //<![CDATA[

    //]]>
     $(document).ready(function() {
        $(".sfLocale").localize({
            moduleKey: AspxAdminDashBoard
        });
    });
</script>

<div class="cssClassSelectChart">
    <label>
        <b class="sfLocale">Select Range:</b></label>
    <select id="ddlRange" class="sfListmenu reportTrigger">
        <option value="1" class="sfLocale">Last 24 Hours</option>
        <option value="7" class="sfLocale">Last 7 Days</option>
        <option value="30" class="sfLocale">Last 30 Days</option>
        <option value="365" class="sfLocale">Last 365 Days</option>
    </select><br />
    <br />
<div id="divChartType">
    <label id="lbla">
        <b class="sfLocale">Select Chart Type:</b></label>
    <select id="ddlChartType" class="sfListmenu">
        <option value="1" class="sfLocale">Bar</option>
        <option value="2" class="sfLocale">Pie</option>
        <option value="3" class="sfLocale">Line</option>
    </select></div></div>
<div id="div24hours" style="display:none">
</div>
<div id="divLW" style="display:none">
</div>
<div id="divCM" style="display:none">
</div>
<div id="divYear" style="display:none">
</div>
