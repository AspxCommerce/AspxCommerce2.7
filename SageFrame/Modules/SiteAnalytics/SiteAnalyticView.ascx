<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteAnalyticView.ascx.cs"
    Inherits="Modules_SiteAnalytic_SiteAnalyticView" %>
<script language="javascript" type="text/javascript">
    //<![CDATA[    
    $(function () {

        var Flag = '<%=Flag %>';
        if (Flag == 1) {
            $(this).SiteAnalyticBuilder({
                CultureCode: 'en-US',
                baseURL: '',
                PortalID: '<%=PortalID %>',
                UserModuleID: '<%=ModuleID %>'
            });
        }
    });
    //]]>	
</script>

<h1> Site Analytics</h1>
<div id="divAnalytic" runat="server">

<div id="divChoose" class="sfChartSelection">
  <ul class="sfTab">
    <li id="Chart" class="sfDefault icon-chart">
      <label class="sfFormlabel"> Chart</label>
    </li>
    <li id="Data" class="icon-data">
      <label class="sfFormlabel"> Data</label>
    </li>
  </ul>
</div>
<div id="divRange" class="sfTableOption">
  <label class="sfFormlabel"> Start Date</label>
  <input type="text" id="txtStartDate" class="sfInputbox" />
  <label class="sfFormlabel"> End Date</label>
  <input type="text" id="txtEndDate" class="sfInputbox" />
  <label class="sfLocale icon-preview sfBtn"> View
    <input type="button" class="sfBtn" id="btnShowData" value="" />
  </label>
</div>
<div class="sfFormwrapper">
  <div id="divChart">
    <div class="clearfix">
      <div id="DailyVisit" class="code" style="height: 300px; width:900px; float:left; "> </div>
    </div>
    <div class="clearfix">
      <div id="MonthlyVisitMeterGaugeChart" style="top:0px; width:500px; height:300px; float: left; position: relative;"> </div>
      <div id="BrowserWiseVisit" style="margin-top: 20px; width: 480px; height: 300px; float: left"> </div>
    </div>
    <div style="float:left; width:auto;">
      <div id="CountryWiseVisit" style="margin-top: 20px; width: 500px;float: left; height: 300px;"> </div>
      <div style="clear:both;"> <span>You Clicked: </span><span id="info1">Nothing yet</span></div>
    </div>
    <div style="float:left; width:auto;">
      <div id="PageVisit" style="margin-top: 20px; width:500px; height: 300px; float: left"> </div>
      <div style="clear:both;"> <span>You Clicked: </span><span id="info2">Nothing yet</span></div>
    </div>
  </div>
  <div id="divData" style="display: none">
    <div class="sfGridwrapper" id="divVisitedCountryList">
      <label class="sfFormlable"> Country Wise Visit</label>
      <table id="tblVisitedCountryList" width="100%" cellpadding="0" cellspacing="0">
      </table>
      <div id="PagingVisitedCountryList" class="sfPagination"> </div>
    </div>
    <div id="divVisitedPageList" class="sfGridwrapper">
      <label class="sfFormlable"> Page Wise Visit</label>
      <select id="slPage" class="sfListmenu">
      </select>
      <table id="tblVisitedPageList" width="100%" cellpadding="0" cellspacing="0">
      </table>
      <div id="PagingVisitedPageList" class="sfPagination"> </div>
    </div>
    <div id="divBrowserList" class="sfGridwrapper">
      <label class="sfFormlable"> Browser Wise Visit</label>
      <table id="tblBrowserList" width="100%" cellpadding="0" cellspacing="0">
      </table>
      <div id="PagingBrowserList" class="sfPagination"> </div>
    </div>
    <div id="divRefSite" class="sfGridwrapper">
      <label class="sfFormlable"> Ref Sites</label>
      <table id="tblRefSite" width="100%" cellpadding="0" cellspacing="0">
      </table>
      <div id="PagingRefList" class="sfPagination"> </div>
    </div>
    <div id="divExport" style="clear: both;">
      <asp:Button ID="btnExportToExcel" runat="server" CssClass="sfBtn" Text="Export to Excel"
                OnClick="btnExportToExcel_Click" />
      <asp:Button ID="btnExportToPDF" runat="server" CssClass="sfBtn" Text="Export to PDF"
                OnClick="btnExportToPDF_Click" />
      <%--            <asp:PostBackTrigger ControlID="btnExportToExcel" />
            <asp:PostBackTrigger ControlID="btnExportToPDF" />--%>
    </div>
  </div>
  <div class="clear"> </div>
</div>
</div>
<div id="divMessage" runat="server" visible="false">
    <label id="lblMessage" runat="server"></label>
</div>