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
using System.IO;
using SageFrame.Framework;
using AspxCommerce.Core;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using SageFrame.Web;
using System.Collections;
using SageFrame.Message;
using SageFrame.Web.Utilities;

public partial class GiftCardSuccess : BaseAdministrationUserControl
{
    public string SendEmailFrom, SendOrderNotice,PageExtension,_sageRedirectPath, _addressPath = string.Empty,MainCurrency = string.Empty, Region, SelectedCurrency;
    bool _isUseFriendlyUrls = true;   
    decimal currencyRate = 1;
    public int orderID;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            aspxCommonObj.UserName = GetUsername;
            aspxCommonObj.CultureName = GetCurrentCultureName;            
            if (!IsPostBack)
            {
                if (Session["OrderID"] != null)
                {
                    orderID = int.Parse(Session["OrderID"].ToString());
                }

                StoreSettingConfig ssc = new StoreSettingConfig();               
                AspxCoreController acc = new AspxCoreController();
                               MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, GetPortalID, GetPortalID, GetCurrentCultureName);

                if (Session["CurrencyCode"] != null && Session["CurrencyCode"].ToString() != "")
                {
                    SelectedCurrency = Session["CurrencyCode"].ToString();
                }
                else
                {
                    SelectedCurrency = MainCurrency;
                }
                if (Session["Region"] != null && Session["Region"].ToString() != "")
                {
                    Region = Session["Region"].ToString();
                }
                else
                {
                    Region = StoreSetting.GetRegionFromCurrencyCode(SelectedCurrency, GetStoreID, GetPortalID);
                }
               
                               currencyRate = Convert.ToDecimal(acc.GetCurrencyRateOnChange(aspxCommonObj, MainCurrency, SelectedCurrency, Region));
          


                var sfConfig = new SageFrameConfig();
                _isUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
                PageExtension = SageFrameSettingKeys.PageExtension;
                if (_isUseFriendlyUrls)
                {
                    if (!IsParent)
                    {
                        _sageRedirectPath = ResolveUrl(GetParentURL + "/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + PageExtension);
                        _addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/portal/" + GetPortalSEOName + "/";
             
                    }
                    else
                    {
                        _sageRedirectPath = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + PageExtension);
                        _addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
             
                    }
                }
                else
                {
                    _sageRedirectPath = ResolveUrl("{~/Default" + PageExtension + "?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
                }

                var imgProgress = (Image)UpdateProgress1.FindControl("imgPrgress");
                if (imgProgress != null)
                {
                    imgProgress.ImageUrl = GetTemplateImageUrl("ajax-loader.gif", true);
                }
                hlnkHomePage.NavigateUrl = _sageRedirectPath;               
                SendEmailFrom = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, GetStoreID, GetPortalID, GetCurrentCultureName);
                SendOrderNotice = ssc.GetStoreSettingsByKey(StoreSetting.SendOrderNotification, GetStoreID, GetPortalID, GetCurrentCultureName);

                SendConfrimMessage();
            }
            IncludeLanguageJS();
        }
        catch(Exception ex)
        {
            ProcessException(ex);
        }
        
    }

    protected void SendConfrimMessage()
    {
        try
        {
            if (Session["OrderID"] != null)
            {
                const int responseCode = 1;                const string responsereasontext = "Transaction occured successfully";
                const int responsereasonCode = 1;
                string paymentmethod = string.Empty;
                var orderdata2 = new OrderDetailsCollection();
                if (HttpContext.Current.Session["OrderCollection"] != null)
                {
                  
                    orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                   
                }
                string invoice = orderdata2.ObjOrderDetails.InvoiceNumber;
                var random = new Random();
                string purchaseorderNo = (random.Next(0, 1000)).ToString();
                string timeStamp = ((int)(DateTime.UtcNow - new DateTime(2011, 1, 1)).TotalSeconds).ToString();
                string transID = (random.Next(9999999, 111111111)).ToString();
                lblTransaction.Text = transID;
                lblInvoice.Text = invoice;
                lblPaymentMethod.Text = "Gift Card";
                lblPaymentStatus.Text = "Successfull";
                lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy ");
                lblOrderNo.Text = "#" + Session["OrderID"].ToString();
                AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                aspxCommonObj.StoreID = GetStoreID;
                aspxCommonObj.PortalID = GetPortalID;
                aspxCommonObj.UserName = GetUsername;
                aspxCommonObj.CustomerID = GetCustomerID;
                aspxCommonObj.SessionCode = HttpContext.Current.Session.SessionID;
                aspxCommonObj.CultureName = GetCurrentCultureName;
                string result = AspxGiftCardController.Parse(orderdata2.ObjOrderDetails.OrderID,transID, invoice, purchaseorderNo, responseCode,
                                                         responsereasonCode, responsereasontext, aspxCommonObj);

                AspxGiftCardController.IssueGiftCard(orderdata2.LstOrderItemsInfo, orderdata2.ObjOrderDetails.OrderID, true, aspxCommonObj);
                      
                if (orderdata2.GiftCardDetail != null && CheckOutSessions.Get<List<GiftCardUsage>>("UsedGiftCard").Count > 0)
                {
                    AspxGiftCardController.UpdateGiftCardUsage(orderdata2.GiftCardDetail, orderdata2.ObjCommonInfo.StoreID,
                                         orderdata2.ObjCommonInfo.PortalID, orderdata2.ObjOrderDetails.OrderID, orderdata2.ObjCommonInfo.AddedBy,
                                         orderdata2.ObjCommonInfo.CultureName);
                   
                }
                lblerror.Text = result;
                lblerror.Text = GetSageMessage("Payment", "PaymentProcessed"); 
                var tinfo = new TransactionLogInfo();
                var tlog = new TransactionLog();

                tinfo.TransactionID = transID;
                tinfo.AuthCode = "";
                               tinfo.TotalAmount = orderdata2.ObjOrderDetails.GrandTotal* currencyRate;
                tinfo.ResponseCode = responseCode.ToString();
                tinfo.ResponseReasonText = responsereasontext;
                tinfo.OrderID = orderdata2.ObjOrderDetails.OrderID;
                tinfo.StoreID =  orderdata2.ObjCommonInfo.StoreID;
                tinfo.PortalID = orderdata2.ObjCommonInfo.PortalID;
                tinfo.AddedBy = orderdata2.ObjCommonInfo.AddedBy;
                tinfo.CustomerID = orderdata2.ObjOrderDetails.CustomerID;
                tinfo.SessionCode = orderdata2.ObjOrderDetails.SessionCode;
                tinfo.PaymentGatewayID = orderdata2.ObjOrderDetails.PaymentGatewayTypeID;
                tinfo.PaymentStatus = "Processed";
                tinfo.CreditCard = "";
                tinfo.CurrencyCode = SelectedCurrency;
                tlog.SaveTransactionLog(tinfo);

                CheckOutHelper cHelper = new CheckOutHelper();
                cHelper.ClearSessions();
               
               
                if (Session["OrderCollection"] != null)
                {
                   
                  var orderdata = (OrderDetailsCollection)Session["OrderCollection"];
                    try
                    {
                        orderdata.ObjOrderDetails.OrderStatus = "Processed";
                        EmailTemplate.SendEmailForOrder(GetPortalID, orderdata, _addressPath, TemplateName, transID);
                    }
                    catch (Exception ex)
                    {
                        lblerror.Text = "";
                        lblerror.Text= GetSageMessage("Payment", "EmailSendOrderProblem");
                        ProcessException(ex);
                    }
                    Session.Remove("OrderCollection");
                }
            }
            else
            {
                Response.Redirect(_sageRedirectPath, false);
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}

