using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class AspxInvoiceMgntController
    {
        public AspxInvoiceMgntController()
        {
        }

        public static List<InvoiceDetailsInfo> GetInvoiceDetailsList(int offset, System.Nullable<int> limit, InvoiceBasicInfo invoiceObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<InvoiceDetailsInfo> lstInvoiceDetail = AspxInvoiceMgntProvider.GetInvoiceDetailsList(offset, limit, invoiceObj, aspxCommonObj);
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
                List<InvoiceDetailByorderIDInfo> info = AspxInvoiceMgntProvider.GetInvoiceDetailsByOrderID(orderID, aspxCommonObj);
                return info;
            }

            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
