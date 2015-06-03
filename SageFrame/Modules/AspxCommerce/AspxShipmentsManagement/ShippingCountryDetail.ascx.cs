using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using System.Text;

public partial class Modules_AspxCommerce_AspxShipmentsManagement_ShippingCountryDetail :BaseUserControl
{
    public string BillingCountry, ShippingDetail;
    public int StoreID, PortalID, CustomerID;
    public string UserName, CultureName;
    protected void Page_Load(object sender, EventArgs e)
    {
        StoreID = GetStoreID;
        PortalID = GetPortalID;
        CustomerID = GetCustomerID;
        UserName = GetUsername;
        CultureName = GetCurrentCultureName;
        StoreSettingConfig ssc = new StoreSettingConfig();
        BillingCountry = ssc.GetStoreSettingsByKey(StoreSetting.AllowedBillingCountry, StoreID, PortalID, CultureName);
        ShippingDetail = ssc.GetStoreSettingsByKey(StoreSetting.AllowedShippingCountry, StoreID, PortalID, CultureName);
        BindCountryList();
    }

    private void BindCountryList()
    {
        StringBuilder blCountry = new StringBuilder();
        StringBuilder spCountry = new StringBuilder();
        StringBuilder lstBLCountry = new StringBuilder();
        StringBuilder lstSPCountry = new StringBuilder();
        List<CountryInfo> lstCountry = AspxCommonController.BindCountryList();
        Array bArray = BillingCountry.Split(',');
        Array sArray = ShippingDetail.Split(',');
        blCountry.Append("<div id=\"ddlBLCountry\"><h2>List Of Billing Available Country</h2><ul>");
        spCountry.Append("<div id=\"ddlSPCountry\"><h2>List Of Shipping Available Country</h2></ul>");
        foreach (var countryInfo in lstCountry)
        {
            if (BillingCountry.ToLower() == "all")
            {
                lstBLCountry.Append("<li class=\"cssCountryList\"> " +
                                                   countryInfo.Text + "</li>");
            }
            else
            {
                if (Array.IndexOf(bArray, countryInfo.Value) >= 0)
                {
                    lstBLCountry.Append("<li class=\"cssCountryList\"> " +
                                         countryInfo.Text + "</li>");
                }
            }

        }
        foreach (var countryInfo in lstCountry)
        {
            if (ShippingDetail.ToLower() == "all")
            {
                lstSPCountry.Append("<li class=\"cssCountryList\"> " +
                                         countryInfo.Text + "</li>");
            }
            else
            {
                if (Array.IndexOf(sArray, countryInfo.Value) >= 0)
                {
                    lstSPCountry.Append("<li class=\"cssCountryList\"> " +
                                         countryInfo.Text + "</li>");
                }
            }

        }
        blCountry.Append(lstBLCountry);
        spCountry.Append(lstSPCountry);
        blCountry.Append("</ul></div>");
        spCountry.Append("</ul></div>");
        ltrShippingDetails.Text = blCountry.ToString() + spCountry.ToString();
    }
}