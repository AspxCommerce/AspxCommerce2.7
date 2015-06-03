using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxInvoiceMgntProvider
    {
        public AspxInvoiceMgntProvider()
        {
        }

        public static List<InvoiceDetailsInfo> GetInvoiceDetailsList(int offset, System.Nullable<int> limit, InvoiceBasicInfo invoiceObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@InvoiceNumber", invoiceObj.InvoiceNumber));
                parameter.Add(new KeyValuePair<string, object>("@BillToName", invoiceObj.BillToName));
                parameter.Add(new KeyValuePair<string, object>("@OrderStatusID", invoiceObj.OrderStatusName));               
                SQLHandler sqlH = new SQLHandler();
                List<InvoiceDetailsInfo> lstInvoiceDetail = sqlH.ExecuteAsList<InvoiceDetailsInfo>("usp_Aspx_GetInvoiceDetails", parameter);
                return lstInvoiceDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<InvoiceDetailByorderIDInfo> GetInvoiceDetailsByOrderID(int orderID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                SQLHandler sqlh = new SQLHandler();
                List<InvoiceDetailByorderIDInfo> info = sqlh.ExecuteAsList<InvoiceDetailByorderIDInfo>("usp_Aspx_GetInvoiceDetailsByOrderID", parameter);
                return info;
            }

            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
