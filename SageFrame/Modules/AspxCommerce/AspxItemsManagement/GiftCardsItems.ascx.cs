using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AspxCommerce.Core;
using SageFrame.Web;
using SageFrame.Web.Utilities;

public partial class Modules_AspxCommerce_AspxItemsManagement_GiftCardsItems : BaseAdministrationUserControl
{
    public int StoreID;
    public int PortalID;
    public string Username;
    public string CultureName, UserModuleId;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                IncludeCss("GiftCardItems", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/PopUp/style.css", "/Templates/" + TemplateName + "/css/JQueryUI/jquery.ui.all.css");
                IncludeJs("GiftCardItemsjs", "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js",
                           "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js", "/js/DateTime/date.js", "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js",
                           "/js/PopUp/custom.js", "/js/ExportToCSV/table2CSV.js", "/Modules/AspxCommerce/AspxItemsManagement/js/GiftCardItems.js", "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js", "/js/CurrencyFormat/jquery.formatCurrency.all.js");

                StoreID = GetStoreID;
                PortalID = GetPortalID;
                Username = GetUsername;
                CultureName = GetCurrentCultureName;
                UserModuleId = SageUserModuleID;
            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            InitializeJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void InitializeJS()
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
            aspxCommonObj.CultureName = GetCurrentCultureName;
            StoreSettingConfig ssc = new StoreSettingConfig();
            string CurrencyCode = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, GetStoreID, GetPortalID, CultureName);
            string CurrencySymbol = StoreSetting.GetSymbolFromCurrencyCode(CurrencyCode, GetStoreID, GetPortalID);
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("@CurrencySymbol", CurrencySymbol));
            parameter.Add(new KeyValuePair<string, object>("@GiftCardType", _csvGiftCardHiddenCsv.Value));
            string filename = "MyReport_GiftCardItems" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls";
            string filePath = HttpContext.Current.Server.MapPath(ResolveUrl(this.AppRelativeTemplateSourceDirectory)) + filename;
            ExportLargeData excelLdata = new ExportLargeData();
            excelLdata.ExportTOExcel(filePath, "[dbo].[usp_Aspx_GetGiftCardReportsForExport]", parameter, resultsData);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void ButtonLowStock_Click(object sender, EventArgs e)
    {
        
        try
        {
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            aspxCommonObj.CultureName = GetCurrentCultureName;
            StoreSettingConfig ssc = new StoreSettingConfig();
            string CurrencyCode = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, GetStoreID, GetPortalID, CultureName);
            string CurrencySymbol = StoreSetting.GetSymbolFromCurrencyCode(CurrencyCode, GetStoreID, GetPortalID);
            List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
            parameter.Add(new KeyValuePair<string, object>("@CurrencySymbol", CurrencySymbol));
            parameter.Add(new KeyValuePair<string, object>("@GiftCardType", _csvGiftCardHiddenCsv.Value));
            string filename = "MyReport_GiftCardItems" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".csv";
            string filePath = HttpContext.Current.Server.MapPath(ResolveUrl(this.AppRelativeTemplateSourceDirectory)) + filename;
            ExportLargeData csvLdata = new ExportLargeData();
            csvLdata.ExportToCSV(true, ",", "[dbo].[usp_Aspx_GetGiftCardReportsForExport]", parameter, filePath);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
