using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using System.Text;


public partial class ShippingRateEstimate : BaseAdministrationUserControl
{

    public string DimentionalUnit = string.Empty, WeightUnit = string.Empty;
    public string ShowRateEstimate = string.Empty;
    public int Count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            var ssc = new StoreSettingConfig();
            ssc.GetStoreSettingParamThree(StoreSetting.DimensionUnit, 
                                            StoreSetting.WeightUnit, 
                                            StoreSetting.EstimateShippingCostInCartPage,
                                            out DimentionalUnit,
                                            out WeightUnit,
                                            out ShowRateEstimate,
                                            GetStoreID, 
                                            GetPortalID,
                                            GetCurrentCultureName);

            IncludeJs("ValidationRate", "/js/FormValidation/jquery.validate.js", "/Modules/AspxCommerce/AspxShippingRateEstimate/js/ShippingRateEstimate.js");
            GetItemCount();
        }
        IncludeLanguageJS();
        LoadCountry();
    }

    private void LoadCountry()
    {
        StringBuilder ddlCountries = new StringBuilder();        
        
        List<CountryList> countries = AspxShipRateController.LoadCountry();
        foreach (CountryList country in countries)
	    {
		    ddlCountries.Append("<option value=");
            ddlCountries.Append(country.CountryCode);
            ddlCountries.Append(">");
            ddlCountries.Append(country.Country);
            ddlCountries.Append(" </option>");
	    }
        ddlCountry.Text = ddlCountries.ToString();
    }
    private void GetItemCount()
    {
        try
        {
            AspxCommonInfo commonInfo = new AspxCommonInfo(GetStoreID, GetPortalID, GetUsername, GetCurrentCulture(), GetCustomerID, Session.SessionID);
            Count = AspxHeaderController.GetCartItemsCount(commonInfo);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}




  
