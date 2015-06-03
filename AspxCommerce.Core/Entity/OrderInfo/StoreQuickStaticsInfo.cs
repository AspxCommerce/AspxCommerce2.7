using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class StoreQuickStaticsInfo
    {
        public StoreQuickStaticsInfo() { }

        public int TotalSales { get; set; }
        public int TotalCustomerOrdered { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalOrders { get; set; }
    }   
}
