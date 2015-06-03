using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using AspxCommerce.PayPal;
using SageFrame.Web;
using SageFrame.Web.Utilities;

public partial class Modules_AspxCommerce_AspxServiceItems_AppointmentSuccess : BaseAdministrationUserControl
{
    public string SendEmailFrom, SendOrderNotice;
    private bool _isUseFriendlyUrls = true;
    private string _sageRedirectPath, _addressPath = string.Empty;
    public string StoreLogoUrl = string.Empty;
    private string _authToken, _txToken, _query;
    private string _strResponse;
    private string _transID;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("messagecss", "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                    "/Modules/AspxCommerce/AspxServiceItems/css/ServiceItems.css");
                IncludeJs("messagejs", "/js/MessageBox/alertbox.js");
                var sfConfig = new SageFrameConfig();
                _isUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);

                if (_isUseFriendlyUrls)
                {
                    if (GetPortalID > 1)
                    {
                        _sageRedirectPath =
                            ResolveUrl("~/portal/" + GetPortalSEOName + "/" +sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + SageFrameSettingKeys.PageExtension);
                        _addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/portal/" +GetPortalSEOName + "/";

                    }
                    else
                    {
                        _sageRedirectPath =
                            ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) +
                                       SageFrameSettingKeys.PageExtension);
                        _addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";

                    }
                }
                else
                {
                    _sageRedirectPath =
                        ResolveUrl("{~/Default" + SageFrameSettingKeys.PageExtension + "?ptlid=" + GetPortalID +
                                   "&ptSEO=" + GetPortalSEOName + "&pgnm=" +
                                   sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
                }

                var imgProgress = (Image) UpdateProgress1.FindControl("imgPrgress");
                if (imgProgress != null)
                {
                    imgProgress.ImageUrl = GetTemplateImageUrl("ajax-loader.gif", true);
                }
                hlnkHomePage.NavigateUrl = _sageRedirectPath;

                StoreSettingConfig ssc = new StoreSettingConfig();
                                            
                StoreLogoUrl = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, GetStoreID, GetPortalID, GetCurrentCultureName);
                SendConfrimMessage();
            }
            IncludeLanguageJS();
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    protected void SendConfrimMessage()
    {

        AspxCommerce.ServiceItem.BookAnAppointmentInfo appointmentInfo = new AspxCommerce.ServiceItem.BookAnAppointmentInfo();
        if (Session["OrderID"] != null)
        {
            int storeID = GetStoreID;
            int portalID = GetPortalID;
            string userName = GetUsername;
            int customerID = GetCustomerID;
            var orderdata = new OrderDetailsCollection();
            if (HttpContext.Current.Session["OrderCollection"] != null)
            {
                orderdata = (OrderDetailsCollection) HttpContext.Current.Session["OrderCollection"];
            }
            string invoice = orderdata.ObjOrderDetails.InvoiceNumber;
            lblInvoice.Text = invoice;

            var random = new Random();
            string transID = (random.Next(99999, 111111)).ToString();
            lblTransaction.Text = transID.Trim();

            if (Session["PaymentMethodName"] != null)
            {
                if (HttpContext.Current.Session["PaymentMethodName"].ToString() == "paypal")
                {
                    var pw = new PayPalWCFService();
                    int j = orderdata.ObjOrderDetails.PaymentGatewayTypeID;
                    List<PayPalSettingInfo> setting = pw.GetAllPayPalSetting(j, storeID, portalID);
                    _authToken = setting[0].AuthToken;

                    _txToken = Request.QueryString.Get("tx");
                    _query = string.Format("cmd=_notify-synch&tx={0}&at={1}", _txToken, _authToken);
                                                          const string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
                    const string strLive = "https://www.paypal.com/cgi-bin/webscr";
                    string test = string.Empty;

                    if (Session["IsTestPayPal"] != null)
                    {

                        test = bool.Parse(Session["IsTestPayPal"].ToString()) ? strSandbox : strLive;
                    }
                    var req = (HttpWebRequest) WebRequest.Create(test);

                                       req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.ContentLength = _query.Length;

                                       var stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                    stOut.Write(_query);
                    stOut.Close();

                                       var stIn = new StreamReader(req.GetResponse().GetResponseStream());
                    _strResponse = stIn.ReadToEnd();
                    stIn.Close();

                                       if (_strResponse.StartsWith("SUCCESS"))
                    {
                        string sessionCode = HttpContext.Current.Session.SessionID;
                                                                                             try
                        {
                            PayPalHandler pdtt = ParseAfterIPN(_strResponse, storeID, portalID, userName, customerID, sessionCode, TemplateName, _addressPath);

                        }
                        catch (Exception)
                        {
                            lblerror.Text = GetSageMessage("Payment", "PaymentParsingIPNError");
                        } 

                        String[] stringArray = _strResponse.Split('\n');
                        int i;
                        string status = string.Empty;
                        for (i = 1; i < stringArray.Length - 1; i++)
                        {
                            String[] stringArray1 = stringArray[i].Split('=');

                            String sKey = stringArray1[0];
                            String sValue = HttpUtility.UrlDecode(stringArray1[1]);

                                                       switch (sKey)
                            {
                                case "txn_id":
                                    _transID = Convert.ToString(sValue);
                                    break;
                                case "payment_status":
                                    status = Convert.ToString(sValue);
                                    break;
                            }
                        }
                        lblTransaction.Text = _transID.Trim();
                        //lblInvoice.Text = _invoice;
                        lblPaymentMethod.Text = "Paypal";
                        if (status.ToLower().Trim() == "completed")
                        {
                            lblerror.Text = GetSageMessage("Payment", "PaymentProcessed");
                        }
                        else if (status.ToLower().Trim() == "pending")
                        {
                            lblerror.Text = GetSageMessage("Payment", "PaymentPending");
                        }
                    }
                    else
                    {
                        lblerror.Text = GetSageMessage("Payment", "PaymentError");
                    }
                }
                else if (HttpContext.Current.Session["PaymentMethodName"].ToString().ToLower() == "cashondelivery")
                {
                    const int responseCode = 1;                    const string responsereasontext = "Transaction occured successfully";
                    const int responsereasonCode = 1;
                   
                    string purchaseorderNo = (random.Next(0, 1000)).ToString();
                                                string sessionCode = HttpContext.Current.Session.SessionID;
                    string result = Parse(transID, invoice, purchaseorderNo, responseCode, responsereasonCode, responsereasontext, storeID, portalID, userName, customerID, sessionCode);
                    lblerror.Text = result;
                    lblerror.Text = GetSageMessage("Payment", "PaymentProcessed");
                    var tinfo = new TransactionLogInfo();
                    var tlog = new TransactionLog();  
                }
            }
            else
            {
                Response.Redirect(_sageRedirectPath, false);
            }
            if (HttpContext.Current.Session["AppointmentCollection"] != null)
            {

                appointmentInfo = (AspxCommerce.ServiceItem.BookAnAppointmentInfo)HttpContext.Current.Session["AppointmentCollection"];

            }

            lblServiceName.Text = appointmentInfo.ServiceCategoryName.Trim();
            lblServiceProduct.Text = appointmentInfo.ServiceProductName;
            lblServiceDuration.Text = appointmentInfo.ServiceDuration;
            lblStoreLocation.Text = appointmentInfo.StoreLocationName;
            lblServiceProviderName.Text = appointmentInfo.EmployeeName;
            lblProductPrice.Text = appointmentInfo.ServiceProductPrice.Trim();

            lblPaymentMethod.Text = appointmentInfo.PaymentMethodName;
            lblDate.Text =  appointmentInfo.PreferredDate.ToString("MM/dd/yyyy");
            lblTime.Text = appointmentInfo.PreferredTimeInterval.Trim();
        }
    }

    public static string Parse(string transId, string invoice, string POrderno, int responseCode, int responsereasonCode, string responsetext, int storeID, int portalID, string userName, int customerID, string sessionCode)
    {
        try
        {
            OrderDetailsCollection ot = new OrderDetailsCollection();
            OrderDetailsInfo odinfo = new OrderDetailsInfo();
            CartManageSQLProvider cms = new CartManageSQLProvider();
            CommonInfo cf = new CommonInfo();
            cf.StoreID = storeID;
            cf.PortalID = portalID;
            cf.AddedBy = userName;
                       AspxOrderDetails objad = new AspxOrderDetails();
            SQLHandler sqlH = new SQLHandler();
            odinfo.OrderID = int.Parse(HttpContext.Current.Session["OrderID"].ToString());
            odinfo.TransactionID = odinfo.ResponseCode.ToString(transId);
            odinfo.InvoiceNumber = Convert.ToString(invoice);
            odinfo.PurchaseOrderNumber = Convert.ToString(POrderno);
            odinfo.ResponseCode = Convert.ToInt32(responseCode);
            odinfo.ResponseReasonCode = Convert.ToInt32(responsereasonCode);
            odinfo.ResponseReasonText = Convert.ToString(responsetext);
            ot.ObjOrderDetails = odinfo;
            ot.ObjCommonInfo = cf;
            odinfo.OrderStatusID = 8;
            objad.UpdateOrderDetails(ot);
            if (HttpContext.Current.Session["OrderCollection"] != null)
            {
                OrderDetailsCollection orderdata2 = new OrderDetailsCollection();
                orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                objad.UpdateItemQuantity(orderdata2);
            }
            HttpContext.Current.Session.Remove("OrderID");
                      return "This transaction has been approved";
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public static PayPalHandler ParseAfterIPN(string postData, int storeID, int portalID, string userName, int customerID, string sessionCode, string TemplateName, string addressPath)
    {
        String sKey, sValue;
        PayPalHandler ph = new PayPalHandler();
        string transID = string.Empty;
        try
        {
                       String[] StringArray = postData.Split('\n');

                       /*
            * loop is set to start at 1 rather than 0 because first
            string in array will be single word SUCCESS or FAIL
            Only used to verify post data
            */
            OrderDetailsCollection ot = new OrderDetailsCollection();
            OrderDetailsInfo odinfo = new OrderDetailsInfo();
            CartManageSQLProvider cms = new CartManageSQLProvider();
            CommonInfo cf = new CommonInfo();
            cf.StoreID = storeID;
            cf.PortalID = portalID;
            cf.AddedBy = userName;
                       AspxOrderDetails objad = new AspxOrderDetails();
            SQLHandler sqlH = new SQLHandler();
                       odinfo.OrderID = int.Parse(HttpContext.Current.Session["OrderID"].ToString());
            int i;
            for (i = 1; i < StringArray.Length - 1; i++)
            {
                String[] StringArray1 = StringArray[i].Split('=');

                sKey = StringArray1[0];
                sValue = HttpUtility.UrlDecode(StringArray1[1]);

                               switch (sKey)
                {
                    case "payment_status":
                        odinfo.ResponseReasonText = Convert.ToString(sValue);
                        break;

                    case "mc_fee":
                                               break;

                    case "payer_email":
                                               break;

                    case "Tx Token":
                                               break;

                    case "txn_id":
                        odinfo.TransactionID = Convert.ToString(sValue);
                        transID = Convert.ToString(sValue);
                        break;

                }
            }

            ot.ObjOrderDetails = odinfo;
            ot.ObjCommonInfo = cf;
            HttpContext.Current.Session.Remove("OrderID");
            HttpContext.Current.Session.Remove("OrderCollection");

            return ph;
        }
        catch (Exception ex)
        {
            
            throw ex;
        }

    }
}
