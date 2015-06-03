using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class CouponPerSalesViewDetailInfo
    {
        public int RowTotal { get; set; }
        public string CouponCode { get; set; }
        public int? OrderID { get; set; }
        public int CustomerID { get; set; }
        public string UserName { get; set; }
        public int CouponID { get; set; }
        public decimal CouponDiscountAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public int NoOfUse { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class CouponPerSalesGetInfo
    {
        public string CouponCode { get; set; }
        public int? OrderID { get; set; }
        public decimal? CouponAmountFrom { get; set; }
        public decimal? CouponAmountTo { get; set; }
        public decimal? GrandTotalAmountFrom { get; set; }
        public decimal? GrandTotalAmountTo { get; set; }
    }
}
