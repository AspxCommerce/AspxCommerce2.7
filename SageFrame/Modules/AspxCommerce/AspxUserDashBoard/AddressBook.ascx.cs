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
using SageFrame.Web;
using AspxCommerce.Core;
using System.Collections.Generic;
using System.Text;
using System.Collections;

public partial class Modules_AspxUserDashBoard_AddressBook : BaseAdministrationUserControl
{
    public bool defaultShippingExist = false;
    public bool defaultBillingExist = false;
    public int addressId = 0;

    public int storeID, portalID, customerID;

    public string sessionCode = string.Empty;

    public string cultureName, userName;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            GetPortalCommonInfo(out storeID, out portalID, out customerID, out userName, out cultureName, out sessionCode);
            aspxCommonObj = new AspxCommonInfo(storeID, portalID, userName, cultureName, customerID, sessionCode);

            GetAddressBookDetails(aspxCommonObj);
            GetAllCountry(aspxCommonObj);
            IncludeJs("addressbook", "/js/encoder.js");
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    Hashtable hst = null;
    public void GetAddressBookDetails(AspxCommonInfo aspxCommonObj)
    {
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        int additionAddrCount = 0;
        List<AddressInfo> lstAddress = AspxUserDashController.GetUserAddressDetails(aspxCommonObj);
        StringBuilder defaultBillingAddressElements = new StringBuilder();
        StringBuilder defaultShippingAddressElements = new StringBuilder();
        StringBuilder addressElements = new StringBuilder();

        if (lstAddress.Count > 0)
        {
            addressElements.Append("<div id=\"olAddtionalEntries\">");
            foreach (AddressInfo value in lstAddress)
            {
                if ((bool.Parse(value.DefaultBilling.ToString())) &&
                    (bool.Parse(value.DefaultShipping.ToString())))
                {
                    addressId = value.AddressID;
                }

                if (!defaultShippingExist)
                {
                    if ((bool.Parse(value.DefaultShipping.ToString())))
                    {
                        defaultShippingExist = true;
                    }
                    else
                    {
                        defaultShippingExist = false;
                    }
                }

                if (!defaultBillingExist)
                {
                    if (bool.Parse(value.DefaultBilling.ToString()))
                    {
                        defaultBillingExist = true;
                    }
                    else
                    {
                        defaultBillingExist = false;
                    }
                }

                if ((bool.Parse(value.DefaultBilling.ToString())) ||
                    ((bool.Parse(value.DefaultShipping.ToString()))))
                {
                    if (bool.Parse(value.DefaultShipping.ToString()))
                    {

                        defaultShippingAddressElements.Append("<div id=\"liDefaultShippingAddress\">");
                        defaultShippingAddressElements.Append("<h3>");
                        defaultShippingAddressElements.Append(getLocale("Default Shipping Address"));
                        defaultShippingAddressElements.Append("</h3>");
                        defaultShippingAddressElements.Append("<div><span name=\"FirstName\">");
                        defaultShippingAddressElements.Append(value.FirstName);
                        defaultShippingAddressElements.Append("</span>");
                        defaultShippingAddressElements.Append(" ");
                        defaultShippingAddressElements.Append("<span name=\"LastName\">");
                        defaultShippingAddressElements.Append(value.LastName);
                        defaultShippingAddressElements.Append("</span></div>");
                        defaultShippingAddressElements.Append("<div><span name=\"Email\">");
                        defaultShippingAddressElements.Append(value.Email);
                        defaultShippingAddressElements.Append("</span></div>");
                        if (value.Company != "")
                        {
                            defaultShippingAddressElements.Append("<div><span name=\"Company\">");
                            defaultShippingAddressElements.Append(value.Company);
                            defaultShippingAddressElements.Append("</span></div>");
                        }
                        defaultShippingAddressElements.Append("<div><span name=\"Address1\">");
                        defaultShippingAddressElements.Append(value.Address1);
                        defaultShippingAddressElements.Append("</span></div>");
                        if (value.Address2 != "")
                        {
                            defaultShippingAddressElements.Append("<div><span name=\"Address2\">");
                            defaultShippingAddressElements.Append(value.Address2);
                            defaultShippingAddressElements.Append("</span></div>");
                        }
                        defaultShippingAddressElements.Append("<div><span name=\"City\">");
                        defaultShippingAddressElements.Append(value.City);
                        defaultShippingAddressElements.Append("</span>, ");
                        defaultShippingAddressElements.Append("<span name=\"State\">");
                        defaultShippingAddressElements.Append(value.State);
                        defaultShippingAddressElements.Append("</span>, ");
                        defaultShippingAddressElements.Append("<span name=\"Country\">");
                        defaultShippingAddressElements.Append(value.Country);
                        defaultShippingAddressElements.Append("</span></div><div>");
                        defaultShippingAddressElements.Append("Zip: <span name=\"Zip\">");
                        defaultShippingAddressElements.Append(value.Zip);
                        defaultShippingAddressElements.Append("</span></div><div>");
                        defaultShippingAddressElements.Append("<i class=\"i-phone\"></i>");
                        defaultShippingAddressElements.Append("<span name=\"Phone\">");
                        defaultShippingAddressElements.Append(value.Phone);
                        defaultShippingAddressElements.Append("</span></div>");
                        if (value.Mobile != "")
                        {
                            defaultShippingAddressElements.Append("<div>");
                            defaultShippingAddressElements.Append("<i class=\"i-mobile\"></i>");
                            defaultShippingAddressElements.Append("<span name=\"Mobile\">");
                            defaultShippingAddressElements.Append(value.Mobile);
                            defaultShippingAddressElements.Append("</span></div>");
                        }
                        if (value.Fax != "")
                        {
                            defaultShippingAddressElements.Append("<div>");
                            defaultShippingAddressElements.Append("<i class=\"i-fax\"></i>");
                            defaultShippingAddressElements.Append("<span name=\"Fax\">");
                            defaultShippingAddressElements.Append(value.Fax);
                            defaultShippingAddressElements.Append("</span></div>");
                        }
                        if (value.Website != "")
                        {
                            defaultShippingAddressElements.Append("<div>");
                            defaultShippingAddressElements.Append("<span name=\"Website\">");
                            defaultShippingAddressElements.Append(value.Website);
                            defaultShippingAddressElements.Append("</span></div>");
                        }
                        defaultShippingAddressElements.Append("</div>");
                        defaultShippingAddressElements.Append(
                            "<div class=\"cssClassChange\"><a href=\"#\" rel=\"popuprel\" name=\"EditAddress\" Flag=\"1\" value=\"");
                        defaultShippingAddressElements.Append(value.AddressID);
                        defaultShippingAddressElements.Append("\" Element=\"Shipping\">");
                        defaultShippingAddressElements.Append("<i class=\"icon-edit\"></i>");
                        defaultShippingAddressElements.Append("</a></div>");
                        ltrShipAddress.Text = defaultShippingAddressElements.ToString();
                    }

                    if (bool.Parse(value.DefaultBilling.ToString()))
                    {
                        defaultBillingAddressElements.Append("<div id=\"liDefaultBillingAddress\">");
                        defaultBillingAddressElements.Append("<h3>");
                        defaultBillingAddressElements.Append(getLocale("Default Billing Address"));
                        defaultBillingAddressElements.Append("</h3>");
                        defaultBillingAddressElements.Append("<div><span name=\"FirstName\">");
                        defaultBillingAddressElements.Append(value.FirstName);
                        defaultBillingAddressElements.Append("</span>");
                        defaultBillingAddressElements.Append(" ");
                        defaultBillingAddressElements.Append("<span name=\"LastName\">");
                        defaultBillingAddressElements.Append(value.LastName);
                        defaultBillingAddressElements.Append("</span></div>");
                        defaultBillingAddressElements.Append("<div><span name=\"Email\">");
                        defaultBillingAddressElements.Append(value.Email);
                        defaultBillingAddressElements.Append("</span></div>");
                        if (value.Company != "")
                        {
                            defaultBillingAddressElements.Append("<div><span name=\"Company\">");
                            defaultBillingAddressElements.Append(value.Company);
                            defaultBillingAddressElements.Append("</span></div>");
                        }
                        defaultBillingAddressElements.Append("<div><span name=\"Address1\">");
                        defaultBillingAddressElements.Append(value.Address1);
                        defaultBillingAddressElements.Append("</span></div>");
                        if (value.Address2 != "")
                        {
                            defaultBillingAddressElements.Append("<div><span name=\"Address2\">");
                            defaultBillingAddressElements.Append(value.Address2);
                            defaultBillingAddressElements.Append("</span></div>");
                        }
                        defaultBillingAddressElements.Append("<div><span name=\"City\">");
                        defaultBillingAddressElements.Append(value.City);
                        defaultBillingAddressElements.Append("</span>, ");
                        defaultBillingAddressElements.Append("<span name=\"State\">");
                        defaultBillingAddressElements.Append(value.State);
                        defaultBillingAddressElements.Append("</span>, ");
                        defaultBillingAddressElements.Append("<span name=\"Country\">");
                        defaultBillingAddressElements.Append(value.Country);
                        defaultBillingAddressElements.Append("</span></div><div>");
                        defaultBillingAddressElements.Append("Zip: <span name=\"Zip\">");
                        defaultBillingAddressElements.Append(value.Zip);
                        defaultBillingAddressElements.Append("</span></div><div>");
                        defaultBillingAddressElements.Append("<i class=\"i-phone\"></i>");
                        defaultBillingAddressElements.Append("<span name=\"Phone\">");
                        defaultBillingAddressElements.Append(value.Phone);
                        defaultBillingAddressElements.Append("</span></div>");
                        if (value.Mobile != "")
                        {
                            defaultBillingAddressElements.Append("<div>");
                            defaultBillingAddressElements.Append("<i class=\"i-mobile\"></i>");
                            defaultBillingAddressElements.Append("<span name=\"Mobile\">");
                            defaultBillingAddressElements.Append(value.Mobile);
                            defaultBillingAddressElements.Append("</span></div>");
                        }
                        if (value.Fax != "")
                        {
                            defaultBillingAddressElements.Append("<div>");
                            defaultBillingAddressElements.Append("<i class=\"i-fax\"></i>");
                            defaultBillingAddressElements.Append("<span name=\"Fax\">");
                            defaultBillingAddressElements.Append(value.Fax);
                            defaultBillingAddressElements.Append("</span></div>");
                        }
                        if (value.Website != "")
                        {
                            defaultBillingAddressElements.Append("<div>");
                            defaultBillingAddressElements.Append("<span name=\"Website\">");
                            defaultBillingAddressElements.Append(value.Website);
                            defaultBillingAddressElements.Append("</span></div>");
                        }
                        defaultBillingAddressElements.Append("</div>");
                        defaultBillingAddressElements.Append(
                            "<div class=\"cssClassChange\"><a href=\"#\" rel=\"popuprel\" name=\"EditAddress\" Flag=\"1\" value=\"");
                        defaultBillingAddressElements.Append(value.AddressID);
                        defaultBillingAddressElements.Append("\" Element=\"Billing\">");
                        defaultBillingAddressElements.Append("<i class=\"icon-edit\"></i>");
                        defaultBillingAddressElements.Append("</a></div>");
                        ltrBillingAddress.Text = defaultBillingAddressElements.ToString();
                    }
                }
                else
                {
                    additionAddrCount = additionAddrCount + 1;
                    addressElements.Append("<div class=\"cssClassAddWrapper\">");
                    addressElements.Append("<div class=\"cssClassAddAddress\">");
                    addressElements.Append("<div><span name=\"FirstName\">");
                    addressElements.Append(value.FirstName);
                    addressElements.Append("</span>");
                    addressElements.Append(" ");
                    addressElements.Append("<span name=\"LastName\">");
                    addressElements.Append(value.LastName);
                    addressElements.Append("</span></div>");
                    addressElements.Append("<div><span name=\"Email\">");
                    addressElements.Append(value.Email);
                    addressElements.Append("</span></div>");
                    if (value.Company != "")
                    {
                        addressElements.Append("<div><span name=\"Company\">");
                        addressElements.Append(value.Company);
                        addressElements.Append("</span></div>");
                    }
                    addressElements.Append("<div><span name=\"Address1\">");
                    addressElements.Append(value.Address1);
                    addressElements.Append("</span></div>");
                    if (value.Address2 != "")
                    {
                        addressElements.Append("<div><span name=\"Address2\">");
                        addressElements.Append(value.Address2);
                        addressElements.Append("</span></div>");
                    }
                    addressElements.Append("<div><span name=\"City\">");
                    addressElements.Append(value.City);
                    addressElements.Append("</span>, ");
                    addressElements.Append("<span name=\"State\">");
                    addressElements.Append(value.State);
                    addressElements.Append("</span>, ");
                    addressElements.Append("<span name=\"Country\">");
                    addressElements.Append(value.Country);
                    addressElements.Append("</span></div><div>");
                    addressElements.Append("Zip: <span name=\"Zip\">");
                    addressElements.Append(value.Zip);
                    addressElements.Append("</span></div><div>");
                    addressElements.Append("<i class=\"i-phone\"></i>");
                    addressElements.Append("<span name=\"Phone\">");
                    addressElements.Append(value.Phone);
                    addressElements.Append("</span></div>");
                    if (value.Mobile != "")
                    {
                        addressElements.Append("<div>");
                        addressElements.Append("<i class=\"i-mobile\"></i>");
                        addressElements.Append("<span name=\"Mobile\">");
                        addressElements.Append(value.Mobile);
                        addressElements.Append("</span></div>");
                    }
                    if (value.Fax != "")
                    {
                        addressElements.Append("<div>");
                        addressElements.Append("<i class=\"i-fax\"></i>");
                        addressElements.Append("<span name=\"Fax\">");
                        addressElements.Append(value.Fax);
                        addressElements.Append("</span></div>");
                    }
                    if (value.Website != "")
                    {
                        addressElements.Append("<div>");
                        addressElements.Append("<span name=\"Website\">");
                        addressElements.Append(value.Website);
                        addressElements.Append("</span></div>");
                    }
                    addressElements.Append("</div>");
                    addressElements.Append(
                        " <div class=\"cssClassChange\"><a href=\"#\" rel=\"popuprel\" name=\"EditAddress\" value=\"");
                    addressElements.Append(value.AddressID);
                    addressElements.Append("\" Flag=\"0\"><i class=\"icon-edit\"></i></a> <a href=\"#\" name=\"DeleteAddress\" class=\"cssDeleteBtn\" value=\"");
                    addressElements.Append(value.AddressID);
                    addressElements.Append("\"><i class=\"icon-delete\"></i></a></div>");
                    addressElements.Append("</div>");
                }

            }
            if (!defaultShippingExist)
            {
                defaultShippingAddressElements.Append("<span class=\"cssClassNotFound\">You have not set Default Shipping Adresss Yet!</span>");
                ltrShipAddress.Text = defaultShippingAddressElements.ToString();
            }
            if (!defaultBillingExist)
            {
                defaultBillingAddressElements.Append("<span class=\"cssClassNotFound\">You have not set Default Billing Adresss Yet!</span>");
                ltrBillingAddress.Text = defaultBillingAddressElements.ToString();
            }
            if (additionAddrCount == 0)
            {
                addressElements.Append("<div id=\"olAddtionalEntries\">");
                addressElements.Append("<span class=\"cssClassNotFound\">");
                addressElements.Append(getLocale("There is no additional address entries!"));
                addressElements.Append("</span>");
                addressElements.Append("</div>");
            }

        }
        else
        {
            defaultShippingAddressElements.Append("<div id=\"liDefaultShippingAddress\">");
            defaultShippingAddressElements.Append("<h3>");
            defaultShippingAddressElements.Append(getLocale("Default Shipping Address"));
            defaultShippingAddressElements.Append("</h3>");
            defaultShippingAddressElements.Append("<span class=\"cssClassNotFound\">You have not set Default Shipping Adresss Yet!</span></div>");
            defaultBillingAddressElements.Append("<div id=\"liDefaultBillingAddress\">");
            defaultBillingAddressElements.Append("<h3>");
            defaultBillingAddressElements.Append(getLocale("Default Billing Address"));
            defaultBillingAddressElements.Append("</h3>");
            defaultBillingAddressElements.Append("<span class=\"cssClassNotFound\">You have not set Default Billing Adresss Yet!</span></div>");
            ltrBillingAddress.Text = defaultBillingAddressElements.ToString();
            ltrShipAddress.Text = defaultShippingAddressElements.ToString();
            addressElements.Append("<div id=\"olAddtionalEntries\">");
            addressElements.Append("<span class=\"cssClassNotFound\">");
            addressElements.Append(getLocale("There is no additional address entries!"));
            addressElements.Append("</span>");
            addressElements.Append("</div>");
        }
        ltrAdditionalEntries.Text = addressElements.ToString();
    }

    public void GetAllCountry(AspxCommonInfo aspxCommonObj)
    {
        string pageExtension = SageFrameSettingKeys.PageExtension;
        List<CountryInfo> lstCountry = AspxCommonController.BindCountryList();
        if (lstCountry != null && lstCountry.Count > 0)
        {
            StringBuilder countryElements = new StringBuilder();
            countryElements.Append("<select id=\"ddlCountry\" class=\"sfListmenu\">");
            foreach (CountryInfo value in lstCountry)
            {
                countryElements.Append("<option value=");
                countryElements.Append(value.Value);
                countryElements.Append(">");
                countryElements.Append(value.Text);
                countryElements.Append("</option>");
            }
            countryElements.Append("</select>");
            ltrCountry.Text = countryElements.ToString();

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
