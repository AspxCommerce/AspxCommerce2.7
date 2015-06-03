using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace AspxCommerce.Core
{
    public class AppointmentIDInfo
    {
        public int AppointmentID { get; set; }
    }

    public class ServiceItemInfo
    {
      // public List<ServiceDateInfo> AvailableDate { get; set; }
       public List<ServiceEmployeeInfo> EmployeeData { get; set; }
       public int BranchId { get; set; }
       public string BranchName { get; set; }
       public int Position { get; set; }
       public int? ServiceId { get; set; }
      
    }

    public class ServiceEmployeeInfo
    {
        public int? ServiceEmployeeId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public List<ServiceDateInfo> AvailableDate { get; set; }
    }

    public class ServiceDateInfo
    {
        public int? ServiceDateId { get; set; }
        public DateTime? ServiceDateFrom { get; set; }
        public DateTime? ServiceDateTo { get; set; }
        public List<ServiceTimeInfo> ServiceTime { get; set; }
    }

    public class ServiceTimeInfo
    {
        public int? ServiceTimeId { get; set; }
       // public int? ServiceQuantity { get; set; }
        public string ServiceTimeFrom { get; set; }
        public string ServiceTimeTo { get; set; }
      
    }

    public class ServiceBookedTime:AppointmentIDInfo
    {
        public int ServiceTimeId { get; set; }
        public string ServiceTimeFrom { get; set; }
        public string ServiceTimeTo { get; set; }
        public string ServiceBookedInterval { get; set; }
    }

    public class GetServiceBookedTimeInfo
    {
        public int ServiceCategoryID { get; set; }
        public int BranchID { get; set; }
        public int EmployeeID { get; set; }
        public string ServiceDateID { get; set; }
        public string ServiceDate { get; set; }
        public int ServiceTimeID { get; set; }
        public int ItemID { get; set; }
    }
}

