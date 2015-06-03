using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using System.Text;
using System.Collections;

public partial class Modules_AspxCommerce_AspxAdminDashBoard_StoreQuickStatics :BaseAdministrationUserControl
{
    public int StoreID;
    public int PortalID;
    public string UserName;
    public string CultureName;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            PortalID = GetPortalID;
            StoreID = GetStoreID;
            UserName = GetUsername;
            CultureName = GetCurrentCultureName;
            IncludeLanguageJS();
            GetStoreQuickStatics();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    Hashtable hst = null;
    public void GetStoreQuickStatics()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        hst = AppLocalized.getLocale(modulePath);
        StoreQuickStaticsInfo lstStockInfo = AspxAdminDashController.GetStoreQuickStatics(aspxCommonObj);
        if (lstStockInfo != null)
        {
            StringBuilder strStoreQuickStat = new StringBuilder();
			strStoreQuickStat.Append("<table width='100%' cellspacing='0' cellpadding='0' border='0'><tr>");
            strStoreQuickStat.Append("<td><div class='cssTotalSales'><i class='icon-totalsales'></i><p><span class='cssClassFormatCurrency'>");
            strStoreQuickStat.Append(lstStockInfo.TotalSales);
            strStoreQuickStat.Append("</span>");
            strStoreQuickStat.Append(getLocale("Total Sales"));
            strStoreQuickStat.Append("</p></div></td>");
            strStoreQuickStat.Append("<td><div class='cssTotalCustomerOrder'><i class='icon-totalcustomerorder'></i><p><span>");
            strStoreQuickStat.Append((Convert.ToDecimal(lstStockInfo.TotalCustomerOrdered)/Convert.ToDecimal(lstStockInfo.TotalCustomers)*100).ToString("N2"));
            strStoreQuickStat.Append("%</span>");
            strStoreQuickStat.Append(getLocale("Conversion Rate"));
            strStoreQuickStat.Append("</p></div></td>");
            strStoreQuickStat.Append("<td><div class='cssAverageOrder'><i class='icon-averageorder'></i><p><span>");
            strStoreQuickStat.Append(lstStockInfo.TotalSales/lstStockInfo.TotalCustomers);
            strStoreQuickStat.Append("</span>");
            strStoreQuickStat.Append(getLocale("Average Orders"));
            strStoreQuickStat.Append("</p></div></td>");
            strStoreQuickStat.Append("<td><div class='cssTotalCustomers'><i class='icon-totalcustomers'></i><p><span>");
            strStoreQuickStat.Append(lstStockInfo.TotalCustomers);
            strStoreQuickStat.Append("</span>");
            strStoreQuickStat.Append(getLocale("Total Customers"));
            strStoreQuickStat.Append("</p></div></td></tr></table>");
            ltrStoreQuickStatics.Text = strStoreQuickStat.ToString();            
        }
    }
    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }
}