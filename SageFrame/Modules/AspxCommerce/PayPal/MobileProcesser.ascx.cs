using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using AspxCommerce.PayPal;

public partial class Modules_Mobile_Processer : AspxMobileCheckOutControl
{
    public string SelectedCurrency = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        StartProccess();
        IncludeLanguageJS();
    }

   
    private void LoadSetting()
    {
        var pw = new PayPalWCFService();
        var orderdata = OrderDetail;

        try
        {
            List<PayPalSettingInfo> sf = pw.GetAllPayPalSetting(orderdata.PaymentGatewayTypeId, orderdata.StoreId, orderdata.PortalId);

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
            string ids = orderdata.OrderId + "#" + orderdata.StoreId + "#" + orderdata.PortalId + "#" + orderdata.AddedBy + "#" + orderdata.CustomerId + "#" + orderdata.SessionCode + "#" + Session["IsTestPayPal"].ToString() + "#" + orderdata.PaymentGatewayTypeId;


            var url = new StringBuilder();

            url.Append(postUrl + "?cmd=_cart&business=" +
                HttpUtility.UrlEncode(sf[0].BusinessAccount.Trim()));

            List<CartInfoforPaypal> cd = pw.GetCartDetails(orderdata.StoreId, orderdata.PortalId, orderdata.CustomerId, orderdata.AddedBy, orderdata.CultureName, orderdata.SessionCode);
            int nCount = 1;
            if (ItemDetails.Count() == cd.Count())
            {


                if (cd.Count == 0)
                {
                    throw new Exception("Your cart does't have any items!Please re-check and checkout again!");
                }
                foreach (CartInfoforPaypal oItem in cd)
                {
                    double itemPrice = Convert.ToDouble(oItem.Price)*Rate;
                    url.AppendFormat("&item_name_" + nCount + "={0}", HttpUtility.UrlEncode(oItem.ItemName));
                    url.AppendFormat("&amount_" + nCount + "={0}",
                                     HttpUtility.UrlEncode(Math.Round(itemPrice, 2).ToString()));
                    url.AppendFormat("&quantity_" + nCount + "={0}", HttpUtility.UrlEncode(oItem.Quantity.ToString()));
                    nCount++;
                }
            }else
            {
                throw new Exception("Your ordered items and current cart items does not matched!");
            }

            nCount--;
            double discountAll = Convert.ToDouble(DiscountTotal) * Rate;
            double couponDiscount = (double)CouponTotal * Rate;
            double taxAll = Convert.ToDouble(TaxTotal) * Rate;
            double shippingCostAll = Convert.ToDouble(ShippingCostTotal) * Rate;
            url.AppendFormat("&num_cart_items={0}", HttpUtility.UrlEncode(nCount.ToString()));
            url.AppendFormat("&discount_amount_cart={0}", HttpUtility.UrlEncode(Math.Round((discountAll + couponDiscount), 2).ToString()));
            url.AppendFormat("&tax_cart={0}", HttpUtility.UrlEncode(Math.Round(taxAll, 2).ToString()));
            url.AppendFormat("&no_shipping={0}", HttpUtility.UrlEncode("1"));
            url.AppendFormat("&shipping_1={0}", HttpUtility.UrlEncode(Math.Round(shippingCostAll, 2).ToString()));
            url.AppendFormat("&currency_code={0}", HttpUtility.UrlEncode(Currency));

            if (sf[0].ReturnUrl != null && sf[0].ReturnUrl.Trim() != "")
                url.AppendFormat("&return={0}", HttpUtility.UrlEncode(sf[0].ReturnUrl.ToString()));
            if (!string.IsNullOrEmpty(sf[0].VerificationUrl))
                url.AppendFormat("&notify_url={0}", HttpUtility.UrlEncode(sf[0].VerificationUrl));
            if (!string.IsNullOrEmpty(sf[0].CancelUrl))
                url.AppendFormat("&cancel_return={0}", HttpUtility.UrlEncode(sf[0].CancelUrl));

            url.AppendFormat("&upload={0}", HttpUtility.UrlEncode("1"));
            url.AppendFormat("&rm={0}", HttpUtility.UrlEncode("1"));

            url.AppendFormat("&custom={0}", HttpUtility.UrlEncode(ids));

            Response.Redirect(url.ToString(), false);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void StartProccess()
    {
        try
        {

            var ssc = new StoreSettingConfig();
            Currency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, OrderDetail.StoreId, OrderDetail.PortalId, OrderDetail.CultureName);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.StoreID = OrderDetail.StoreId;
            aspxCommonObj.PortalID = OrderDetail.PortalId;
            if (PaypalSupportedCurrency.paypalSupportedCurrency.Split(',').Where(s => string.Compare(Currency, s, true) == 0).Count() > 0)
            {
                Rate = 1;
                SelectedCurrency = Currency;
            }
            else
            {                
                AspxCoreController acc = new AspxCoreController();
                Rate = acc.GetCurrencyRateOnChange(aspxCommonObj,Currency, "USD", "en-US");
                Currency = "USD";
                SelectedCurrency = Currency;

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

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
