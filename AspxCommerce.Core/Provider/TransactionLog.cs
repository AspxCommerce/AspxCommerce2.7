using System;
using System.Collections.Generic;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class TransactionLog
    {
        public void SaveTransactionLog(TransactionLogInfo tinfo)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@TransactionID", tinfo.TransactionID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGateWayID", tinfo.PaymentGatewayID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PayerEmail", tinfo.PayerEmail));
                parameterCollection.Add(new KeyValuePair<string, object>("@RecieverEmail", tinfo.RecieverEmail));
                parameterCollection.Add(new KeyValuePair<string, object>("@TotalAmount", tinfo.TotalAmount));
                parameterCollection.Add(new KeyValuePair<string, object>("@CurrencyCode", tinfo.CurrencyCode));
                parameterCollection.Add(new KeyValuePair<string, object>("@PaymentStatus", tinfo.PaymentStatus));
                parameterCollection.Add(new KeyValuePair<string, object>("@OrderID", tinfo.OrderID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CustomerID", tinfo.CustomerID));
                parameterCollection.Add(new KeyValuePair<string, object>("@SessionCode", tinfo.SessionCode));
                parameterCollection.Add(new KeyValuePair<string, object>("@ResponseCode", tinfo.ResponseCode));
                parameterCollection.Add(new KeyValuePair<string, object>("@ResponseReasonText", tinfo.ResponseReasonText));
                parameterCollection.Add(new KeyValuePair<string, object>("@AuthCode", tinfo.AuthCode));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", tinfo.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", tinfo.PortalID));               
                parameterCollection.Add(new KeyValuePair<string, object>("@AddedBy", tinfo.AddedBy));

                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveTransactionLog", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
