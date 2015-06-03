/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using AspxCommerce.Core;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using System.Data;

public partial class Modules_AspxCommerce_AspxReturnAndPolicy_ReturnsReports : BaseAdministrationUserControl
{
    public int StoreID;
    public int PortalID, UserModuleIDReturn;
    public string Username;
    public string CultureName;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("StoreTaxes", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/PopUp/style.css");
                IncludeJs("StoreTaxes","/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js",
                     "/js/GridView/jquery.dateFormat.js", "/js/DateTime/date.js", "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", "/js/PopUp/custom.js", "/js/ExportToCSV/table2CSV.js"
                     , "/Modules/AspxCommerce/AspxReturnAndPolicy/js/ReturnReport.js"
                     , "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js",
                          "/js/CurrencyFormat/jquery.formatCurrency.all.js");

                StoreID = GetStoreID;
                PortalID = GetPortalID;
                Username = GetUsername;
                CultureName = GetCurrentCultureName;
                UserModuleIDReturn = int.Parse(SageUserModuleID);
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
        IncludeLanguageJS();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalServicePath", " var aspxservicePath='" + ResolveUrl("~/") + "Modules/AspxCommerce/AspxCommerceServices/" + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalRootPath", " var aspxRootPath='" + ResolveUrl("~/") + "';", true);

            InitializeJs();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void InitializeJs()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable resultsData = new DataTable();
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            StoreSettingConfig ssc = new StoreSettingConfig();
            string CurrencyCode = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, GetStoreID, GetPortalID, CultureName);
            string CurrencySymbol = StoreSetting.GetSymbolFromCurrencyCode(CurrencyCode, GetStoreID, GetPortalID);
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("@CurrencySymbol", CurrencySymbol));
            string filename = "MyReport_ReturnReport" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls";
            string filePath = HttpContext.Current.Server.MapPath(ResolveUrl(this.AppRelativeTemplateSourceDirectory)) + filename;
            ExportLargeData excelLdata = new ExportLargeData();
            string showReport = HdnValue.Value;
            if (showReport == "1")
                excelLdata.ExportTOExcel(filePath, "[dbo].[usp_Aspx_GetReturnReportForExport]", parameter, resultsData);
            else if (showReport == "2")
                excelLdata.ExportTOExcel(filePath, "[dbo].[usp_Aspx_GetReturnReportByCurrentMonthForExport]", parameter, resultsData);
            else
                excelLdata.ExportTOExcel(filePath, "[dbo].[usp_Aspx_GetReturnReportBy24hoursForExport]", parameter, resultsData);

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void ButtonReturn_Click(object sender, System.EventArgs e)
    {
        try
        {
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            StoreSettingConfig ssc = new StoreSettingConfig();
            string CurrencyCode = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, GetStoreID, GetPortalID, CultureName);
            string CurrencySymbol = StoreSetting.GetSymbolFromCurrencyCode(CurrencyCode, GetStoreID, GetPortalID);
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("@CurrencySymbol", CurrencySymbol));
            string filename = "MyReport_ReturnReport" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".csv";
            string filePath = HttpContext.Current.Server.MapPath(ResolveUrl(this.AppRelativeTemplateSourceDirectory)) + filename;
            ExportLargeData csvLdata = new ExportLargeData();
            string showReport = HdnValue.Value;
            if (showReport == "1")
                csvLdata.ExportToCSV(true, ",", "[dbo].[usp_Aspx_GetReturnReportForExport]", parameter, filePath);
            else if (showReport == "2")
                csvLdata.ExportToCSV(true, ",", "[dbo].[usp_Aspx_GetReturnReportByCurrentMonthForExport]", parameter, filePath);
            else
                csvLdata.ExportToCSV(true, ",", "[dbo].[usp_Aspx_GetReturnReportBy24hoursForExport]", parameter, filePath);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}

