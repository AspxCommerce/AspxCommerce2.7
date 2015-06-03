using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class CategoryQuantityStatics
    {
       public string CategoryName { get; set; }
       public int TotalItemQuantity{get;set;}

    }

    public class CategoryRevenueStatics
    {
        public string CategoryName { get; set; }
        public decimal TotalAmount { get; set; }
    }


    public class VisitorOrderStatics
    {
        public int TotalVisit { get; set; }
        public int TotalOrder { get; set; }
    }

    public class VisitorNewAccountStatics
    {
        public int TotalVisit { get; set; }
        public int TotalCustomer { get; set; }
    }

    public class VisitorNewOrderStatics
    {
       public int TotalCustomer{get;set;}
       public int TotalOrder { get; set; }
    }

    public class RefundStatics
    {
        public decimal TotalRefundAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal OtherPostalCharges { get; set; }
    }
    public class  RefundReasonStatics
    {
        public string ReturnReasonAliasName { get; set; }
        public int ReturnReasonID { get; set; }
        public int TotalReason { get; set; }
    }

}
