using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspxCommerce.Core;
using AspxCommerce.Core.Mobile;
using SageFrame.Web;

/// <summary>
/// Summary description for AspxMobileCheckOutControl
/// </summary>
public class AspxMobileCheckOutControl : BaseAdministrationUserControl
{
    public AspxMobileCheckOutControl()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public OrderInfo OrderDetail
    {
        get
        {
            if (Session["mb_OrderDetail"] != null)
                return (OrderInfo)Session["mb_OrderDetail"];
            else
                return null;

        }
        set { Session["mb_OrderDetail"] = value; }
    }
    public UserAddressInfo ShippingAddress
    {
        get
        {
            if (Session["mb_ShippingAddress"] != null)
                return (UserAddressInfo)Session["mb_ShippingAddress"];
            else
                return null;

        }
        set { Session["mb_ShippingAddress"] = value; }
    }

    public UserAddressInfo BillingAddress
    {
        get
        {
            if (Session["mb_BillingAddress"] != null)
                return (UserAddressInfo)Session["mb_BillingAddress"];
            else
                return null;

        }
        set { Session["mb_BillingAddress"] = value; }
    }

    public List<GiftCardUsage> GiftCardDetail
    {
        get
        {
            if (Session["mb_GiftCardUsage"] != null)
                return (List<GiftCardUsage>)Session["mb_GiftCardUsage"];
            else
                return null;

        }
        set { Session["mb_GiftCardUsage"] = value; }
    }

    public List<OrderItem> ItemDetails
    {
        get
        {
            if (Session["mb_ItemDetails"] != null)
                return (List<OrderItem>) Session["mb_ItemDetails"];
            else
                return null;

        }
        set { Session["mb_ItemDetails"] = value; }

    }

    public string CouponCode
    {
        get
        {
            if (Session["mb_CouponCode"] != null)
                return Session["mb_CouponCode"].ToString();
            else
                return "";

        }
        set { Session["mb_CouponCode"] = value; }

    }

    public int CouponCodeApplied
    {
        get
        {
            if (Session["mb_CouponCodeApplied"] != null)
                return (int)Session["mb_CouponCodeApplied"];
            else
                return 0;

        }
        set { Session["mb_CouponCodeApplied"] = value; }

    }

    public bool IsCheckoutFromMobile
    {
        get
        {
            if (Session["mb_IsCheckoutFromMobile"] != null)
                return (bool)Session["mb_IsCheckoutFromMobile"];
            else
                return false;

        }
        set { Session["mb_IsCheckoutFromMobile"] = value; }
    }
    public decimal DiscountTotal
    {
        get
        {
            if (Session["mb_DiscountTotal"] != null)
                return (decimal)Session["mb_DiscountTotal"];
            else
                return 0;

        }
        set { Session["mb_DiscountTotal"] = value; }
    }

    public decimal CouponTotal{get; set; }

    public decimal TaxTotal { get; set; }
    public decimal ShippingCostTotal { get; set; }
    public double Rate { get; set; }
    public string Currency { get; set; }

}
