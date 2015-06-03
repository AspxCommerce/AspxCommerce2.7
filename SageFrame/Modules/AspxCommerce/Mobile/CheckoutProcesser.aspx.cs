using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using AspxCommerce.Core.Mobile;
using SageFrame.Web.Utilities;
using System.Text.RegularExpressions;

public partial class Modules_moblie_CheckoutProcesser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Process();
    }

    private OrderInfo _orderDetail;
    private List<GiftCardUsage> _giftCardUsages;
    private decimal _giftCardTotal = 0;
    private UserAddressInfo _userBillingAddresInfo;
    private UserAddressInfo _userShippingAddresInfo;
    private List<OrderItem> _orderItem;
    private void Process()
    {
        try
        {
            int orderId = 0, couponCodeApplied = 0;

            string couponCode = string.Empty,
                   giftCardCode = string.Empty,
                   digest = string.Empty,
                   itemsInfo = string.Empty;
            #region Required Fields
            if (Request.QueryString["oid"] != null)
                orderId = int.Parse(Request.QueryString["oid"]);                                  
            if (Request.QueryString["iinfo"] != null)
                itemsInfo = Request.QueryString["iinfo"];           
            #endregion
            if (Request.QueryString["cc"] != null)
                couponCode = Request.QueryString["cc"];            if (Request.QueryString["cca"] != null)
                couponCodeApplied = int.Parse(Request.QueryString["cca"]);            if (Request.QueryString["gcc"] != null)
                giftCardCode = Request.QueryString["gcc"];            if (Request.QueryString["md5"] != null)
                digest = Request.QueryString["md5"].ToUpper();
            if (orderId > 0 && !string.IsNullOrEmpty(itemsInfo))
            {
                var values = string.Format("{0}{1}{2}{3}{4}", orderId, itemsInfo, couponCode, couponCodeApplied,
                                           giftCardCode);

                if (VerifyMd5(values, digest))
                {
                    const string gateWayControl = "MobileProcesser.ascx";
                    GetOrderDetails(orderId);
                    SetOrderItems(itemsInfo);
                    SetGiftCardInfo(giftCardCode);
                    string folderPath = "Modules/" + GetPaymentGateWaySourceLocation(_orderDetail.PaymentGatewayTypeId);

                    var controlPath = Path.Combine(folderPath, gateWayControl);
                    controlPath = ResolveUrl(@"~/" + controlPath);

                    var userControl = (AspxMobileCheckOutControl)Page.LoadControl(controlPath);

                    userControl.OrderDetail = _orderDetail;
                    userControl.GiftCardDetail = _giftCardUsages;
                    userControl.BillingAddress = _userBillingAddresInfo;
                    if (_userShippingAddresInfo!=null)
                    userControl.ShippingAddress = _userShippingAddresInfo;
                    userControl.IsCheckoutFromMobile = true;
                    userControl.ItemDetails = _orderItem;
                    userControl.CouponCode = couponCode;
                    userControl.CouponCodeApplied = couponCodeApplied;
                    userControl.DiscountTotal = CalculateDiscountTotal();
                    userControl.ShippingCostTotal = _orderDetail.ShippingRate;


                                       userControl.TaxTotal = _orderDetail.TaxTotal;
                                                                             Page.Controls.Add(userControl);
                }
                else
                {
                    throw new Exception("Please submit valid data!");
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private string SerializeObjectForQueryString(object obj)
    {
        var properties = from p in obj.GetType().GetProperties()
                         where p.GetValue(obj, null) != null
                         select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

               string queryString = String.Join("&", properties.ToArray());
        return queryString;
    }

    private bool VerifyMd5(string values, string querymd5)
    {
               byte[] byteValue = (new System.Text.ASCIIEncoding()).GetBytes(values);
        MD5 md5 = MD5.Create();
        byte[] hashedBytes = md5.ComputeHash(byteValue);

               string fingerprint = string.Empty;
        for (int i = 0; i < hashedBytes.Length; i++)
            fingerprint += hashedBytes[i].ToString("x").ToUpper().PadLeft(2, '0');

       
        if (querymd5.ToUpper() == fingerprint.ToUpper())
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private decimal CalculateDiscountTotal()
    {
        var totalDiscout = _orderDetail.DiscountAmount + _orderDetail.CouponDiscountAmount + _giftCardTotal;
        if (totalDiscout > 0)
            return totalDiscout;
        else
            return 0;

    }

    private void GetOrderDetails(int orderId)
    {
        try
        {
            var parameter = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@OrderId", orderId) };
            var sqlH = new SQLHandler();
            _orderDetail = sqlH.ExecuteAsObject<OrderInfo>("usp_Aspx_mb_GetOrderDetailByOrderId", parameter);
            GetUserBillingAddress(_orderDetail.UserBillingAddressId);
            if (_orderDetail.UserShippingAddressId != 0 )
            GetUserShippingAddress(_orderDetail.UserShippingAddressId);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string GetPaymentGateWaySourceLocation(int paymentGatewayId)
    {
        try
        {
            var parameter = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@PaymentGateWayId", paymentGatewayId) };
            var sqlH = new SQLHandler();
            return sqlH.ExecuteAsScalar<string>("usp_Aspx_mb_GetPaymentGatewayLocation", parameter);

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void GetUserBillingAddress(int billingAddressId)
    {
        try
        {
            var parameter = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@BillingAddressId", billingAddressId) };
            var sqlH = new SQLHandler();
            _userBillingAddresInfo = sqlH.ExecuteAsObject<UserAddressInfo>("usp_Aspx_mb_GetBillingAddressByAddressId",
                                                                           parameter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void GetUserShippingAddress(int shippingAddressId)
    {
        try
        {
            var parameter = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@ShippingAddressId", shippingAddressId) };
            var sqlH = new SQLHandler();
            _userShippingAddresInfo = sqlH.ExecuteAsObject<UserAddressInfo>("usp_Aspx_mb_GetShippingAddressByAddressId",
                                                                           parameter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void SetOrderItems(string itemsInfo)
    {
        itemsInfo = itemsInfo.Replace("'", "\"");
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<OrderItem> orderitems = serializer.Deserialize<List<OrderItem>>(itemsInfo);

        if (orderitems.Any())
        {
            _orderItem = orderitems;
        }
    }

    private void SetGiftCardInfo(string codes)
    {

        if (!string.IsNullOrEmpty(codes))
        {
            codes = codes.Replace("'", "\"");
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var giftCardUsages  = serializer.Deserialize<List<GiftCardUsage>>(codes);  
            if (giftCardUsages.Any())
            {
                _giftCardUsages = giftCardUsages;
            }
            foreach (var giftCardUsage in _giftCardUsages)
            {
                _giftCardTotal += giftCardUsage.ReducedAmount;
            }
                                                                                                                                                                                                      

        }

    }



}
