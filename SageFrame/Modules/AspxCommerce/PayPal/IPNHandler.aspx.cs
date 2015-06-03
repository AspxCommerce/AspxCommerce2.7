using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using AspxCommerce.Core;
using AspxCommerce.PayPal;
using SageFrame.Framework;

public partial class Modules_AspxCommerce_PayPal_IPNHandler : PageBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string selectedCurrency = string.Empty;
        string MainCurrency = string.Empty;
        try
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
            MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, GetStoreID, GetPortalID, GetCurrentCultureName);
            if (Session["SelectedCurrency"] != null)
            {
                if (Session["SelectedCurrency"].ToString() != "")
                {
                    selectedCurrency = Session["SelectedCurrency"].ToString();
                }
            }
            else
            {
                selectedCurrency = MainCurrency;
            } 

            string islive = Request.Form["custom"];
            string test = string.Empty;
            const string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            const string strLive = "https://www.paypal.com/cgi-bin/webscr";
            test = bool.Parse(islive.Split('#')[6]) ? strSandbox : strLive;

            var req = (HttpWebRequest)WebRequest.Create(test);
                       req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);
            strRequest += "&cmd=_notify-validate";
            req.ContentLength = strRequest.Length;
                                                        var streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();
            var streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();
                                                                                                    if (strResponse == "VERIFIED")
            {
                string payerEmail = Request.Form["payer_email"];
                string paymentStatus = Request.Form["payment_status"];
                string receiverEmail = Request.Form["receiver_email"];
                string amount = Request.Form["mc_gross"];
                string invoice = Request.Form["invoice"];
                string addressName = Request.Form["address_name"];
                string addressStreet = Request.Form["address_street"];
                string addressCity = Request.Form["address_city"];
                string addressZip = Request.Form["address_zip"];
                string addressCountry = Request.Form["address_country"];
                string transID = Request.Form["txn_id"];
                string custom = Request.Form["custom"];
             
                string[] ids = custom.Split('#');
                int orderID = int.Parse(ids[0]);
                int storeID = int.Parse(ids[1]);
                int portalID = int.Parse(ids[2]);
                string userName = ids[3];
                int customerID = int.Parse(ids[4]);
                string sessionCode = ids[5];
                string pgid = ids[7];

                var tinfo = new TransactionLogInfo();
                var tlog = new TransactionLog();

                tinfo.TransactionID = transID;
                tinfo.AuthCode = "";
                tinfo.TotalAmount = decimal.Parse(amount);
                tinfo.ResponseCode = "1";
                tinfo.ResponseReasonText = "";
                tinfo.OrderID = orderID;
                tinfo.StoreID = storeID;
                tinfo.PortalID = portalID;
                tinfo.AddedBy = userName;
                tinfo.CustomerID = customerID;
                tinfo.SessionCode = sessionCode;
                tinfo.PaymentGatewayID = int.Parse(pgid);
                tinfo.PaymentStatus = paymentStatus;
                tinfo.PayerEmail = payerEmail;
                tinfo.CreditCard = "";
                tinfo.RecieverEmail = receiverEmail;
                tinfo.CurrencyCode = selectedCurrency;
                tlog.SaveTransactionLog(tinfo);
              

                if (paymentStatus.Equals("Completed"))
                {
                                                                                                                                                                                               var paypalobj = new PayPalHandler();
                    paypalobj.ParseIPN(orderID, transID, paymentStatus, storeID, portalID, userName, customerID, sessionCode);
                                   }
              

            }
            else if (strResponse == "INVALID")
            {
                           }
            else
            {
                           }
                   }
        catch (Exception ex)
        {
            ProcessException(ex);
                 }
    }


}
