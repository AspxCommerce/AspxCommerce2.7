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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame;
using SageFrame.Framework;
using System.Web.Services;
using System.IO;
using AspxCommerce.Core;
using System.Text;
using AspxCommerce.PayPal;
using SageFrame.Core;

public partial class Modules_AspxCommerce_ServiceItems_PayThroughPaypal : PageBase
{
    public string AspxPaymentModulePath;
    public int storeID;
    public int portalID;
    public int customerID;
    public string UserName;
    public string CultureName, MainCurrency;
    public string SessionCode = string.Empty;
    public int PayPal;
    public string Spath;
    public double Rate;
    public string SelectedCurrency = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["PaypalData"] != null)
            {
                string[] data = Session["PaypalData"].ToString().Split('#');
                storeID = int.Parse(data[0]);
                portalID = int.Parse(data[1]);
                UserName = data[2];
                customerID = int.Parse(data[3]);
                SessionCode = data[4];
                CultureName = data[5];
                Spath = ResolveUrl("~/Modules/AspxCommerce/AspxCommerceServices/");
                                              AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                aspxCommonObj.StoreID=storeID;
                aspxCommonObj.PortalID=portalID;
                var ssc = new StoreSettingConfig();
                MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, storeID, portalID, CultureName);
                if (PaypalSupportedCurrency.paypalSupportedCurrency.Split(',').Where(s => string.Compare(MainCurrency, s, true) == 0).Count() > 0)
                {
                    Rate = 1;
                    SelectedCurrency = MainCurrency;
                }
                else
                {
                    var aws = new AspxCoreController();
                    Rate = aws.GetCurrencyRateOnChange(aspxCommonObj,MainCurrency, "USD","en-US");
					MainCurrency="USD"; 
                    SelectedCurrency = MainCurrency;
                    /* Some time if selected currency does not exist in currency table then it returns 1, 
                       if we take 1 as rate then it will convert same as previous amount
                       So avoid Transaction by making it 0 */
                    if (Rate == 1)
                    {
                        Rate = 0;
                    }
                }
                if (Rate != 0)
                {
                                       Session["SelectedCurrency"] = SelectedCurrency;
                    LoadSetting();
                }
                else
                {
                    lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
                    clickhere.Visible = false;
                }
                 
            }
            else
            {
                lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
                clickhere.Visible = false;
            }
           
           

        }
        catch (Exception ex)
        {
            lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
            clickhere.Visible = false;
            ProcessException(ex);      
        }       

    } 

    [WebMethod]
    public static void SetSessionVariable(string key, string value)
    {
        HttpContext.Current.Session[key] = value;

    }
    public void LoadSetting()
    {
        var pw = new PayPalWCFService();
        var orderdata2 =  (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];

        try
        {
            List<PayPalSettingInfo> sf = pw.GetAllPayPalSetting(int.Parse(Session["GateWay"].ToString()), storeID,
                                                                portalID);

            string postUrl;
            if (bool.Parse(sf[0].IsTestPaypal))
            {
                postUrl = "https://www.sandbox.paypal.com/us/cgi-bin/webscr";
                HttpContext.Current.Session["IsTestPayPal"] = true;

            }
            else
            {
                postUrl = "https://www.paypal.com/us/cgi-bin/webscr";
                HttpContext.Current.Session["IsTestPayPal"] = false;

            }
            string ids = Session["OrderID"].ToString() + "#" + storeID + "#" + portalID + "#" + UserName + "#" +
                         customerID + "#" + SessionCode + "#" + Session["IsTestPayPal"].ToString() + "#" +
                         Session["GateWay"].ToString();


            var url = new StringBuilder();

            url.Append(postUrl + "?cmd=_cart&business=" +
                       HttpUtility.UrlEncode(sf[0].BusinessAccount.Trim()));
            string serviceType = string.Empty;
            if (Session["ServiceType"] != null)
            {
                serviceType = Session["ServiceType"].ToString();
            }
            if (serviceType.ToLower() == "true")
            {
                var appointmentInfo = new BookAppointmentInfo();
                if (HttpContext.Current.Session["AppointmentCollection"] != null)
                {
                    appointmentInfo = (BookAppointmentInfo) HttpContext.Current.Session["AppointmentCollection"];
                }
                int nCount = 1;
                double itemPrice = Convert.ToDouble(appointmentInfo.ServiceProductPrice)*Rate;
                url.AppendFormat("&item_name_" + nCount + "={0}",
                                 HttpUtility.UrlEncode(appointmentInfo.ServiceProductName));
                url.AppendFormat("&amount_" + nCount + "={0}",
                                 HttpUtility.UrlEncode(Math.Round(itemPrice, 2).ToString()));
               
                double discountAll = 0.00;
                double couponDiscount = 0.00;
                double taxAll = 0.00;
                double shippingCostAll = 0.00;

                url.AppendFormat("&num_cart_items={0}", HttpUtility.UrlEncode(nCount.ToString()));
                url.AppendFormat("&discount_amount_cart={0}",
                                 HttpUtility.UrlEncode(Math.Round((discountAll + couponDiscount), 2).ToString()));
                url.AppendFormat("&tax_cart={0}", HttpUtility.UrlEncode(Math.Round(taxAll, 2).ToString()));
                url.AppendFormat("&no_shipping={0}", HttpUtility.UrlEncode("1"));
                url.AppendFormat("&shipping_1={0}", HttpUtility.UrlEncode(Math.Round(shippingCostAll, 2).ToString()));
                url.AppendFormat("&currency_code={0}", HttpUtility.UrlEncode(MainCurrency));

                if (sf[0].ReturnUrl != null && sf[0].ReturnUrl.Trim() != "")
                {

                 
                                                          var serviceSuccessPage = "Appointment-Success.aspx";
                    var successPageURL = sf[0].ReturnUrl.Substring(0, sf[0].ReturnUrl.LastIndexOf("/")) + "/" +serviceSuccessPage;
                    url.AppendFormat("&return={0}", HttpUtility.UrlEncode(successPageURL.ToString()));
                }
                if (!string.IsNullOrEmpty(sf[0].VerificationUrl))
                    url.AppendFormat("&notify_url={0}", HttpUtility.UrlEncode(sf[0].VerificationUrl));
                if (!string.IsNullOrEmpty(sf[0].CancelUrl))
                    url.AppendFormat("&cancel_return={0}", HttpUtility.UrlEncode(sf[0].CancelUrl));

                url.AppendFormat("&upload={0}", HttpUtility.UrlEncode("1"));
                url.AppendFormat("&rm={0}", HttpUtility.UrlEncode("1"));

                url.AppendFormat("&custom={0}", HttpUtility.UrlEncode(ids));
                Response.Redirect(url.ToString(), false);
            }            
        }
        catch (Exception ex)
        {
            lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
            clickhere.Visible = false;
            ProcessException(ex);
        }

    }


    protected void clickhere_Click(object sender, EventArgs e)
    {
        LoadSetting();
    }
}
