using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.ServiceItem
{
    public class ServiceCategoryInfo
    {
        public int ServiceCategoryID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDetail { get; set; }
        public int Count { get; set; }
        public string ServiceImagePath { get; set; }
    }

    public class ServiceItemProductInfo
    {
        public int ItemID { get; set; }
        public string SKU { get; set; }
        public string ItemName { get; set; }
        public string Price { get; set; }
        public int ServiceDuration { get; set; }
    }

    public class ServiceAvailableDate
    {
        public int ServiceID { get; set; }
        public int ServiceDateID { get; set; }
        public DateTime? ServiceDateFrom { get; set; }
        public DateTime? ServiceDateTo { get; set; }
    }

    public class ServiceAvailableTime
    {
        public int ServiceID { get; set; }
        public int ServiceDateID { get; set; }
        public int ServiceTimeID { get; set; }
        public string ServiceTimeFrom { get; set; }
        public string ServiceTimeTo { get; set; }
        //   public int ServiceQuantity { get; set; }
    }

    public class GetServiceDate
    {
        public int ServiceID { get; set; }
        public int BranchID { get; set; }
        public int EmployeeID { get; set; }
    }

    public class GetServiceAvailableTime
    {
        public int CategoryID { get; set; }
        public int BranchID { get; set; }
        public int EmployeeID { get; set; }
        public string ServiceDateID { get; set; }
        public string ServiceDate { get; set; }
        public int ItemID { get; set; }
    }


    public class FrontServiceCategoryView : ServiceCategoryInfo
    {
        public int RowTotal { get; set; }
    }
}
