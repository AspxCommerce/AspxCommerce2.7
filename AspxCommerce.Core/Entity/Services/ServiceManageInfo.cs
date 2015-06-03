using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class ServiceManageInfo
    {
        public int RowTotal { get; set; }
        public int ServiceID { get; set; }
        public string ItemName { get; set; }
        public int ItemID { get; set; }
        public DateTime ServiceDateFrom { get; set; }
        public DateTime ServiceDateTo { get; set; }
        public string ServiceTimeFrom { get; set; }
        public string ServiceTimeTo { get; set; }
        public decimal Price { get; set; }
    }

    public class ServiceListInfo
    {
        public int RowTotal { get; set; }
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string StoreBranchName { get; set; }
    }
    public class ServiceGridListInfo
    {
        public int RowTotal { get; set; }
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string StoreBranchName { get; set; }
        public int StoreBranchID { get; set; }
    }

    public class ServiceEmpInfo
    {
        public int RowTotal { get; set; }
        public int ServiceID { get; set; }
        public int ServiceEmployeeID { get; set;}
        public string ServiceEmployeeName { get; set; }
        public int EmployeeID { get; set; }
    }
    public class BookingDetailsInfo
    {
        public int RowTotal { get; set; }
        public int AppointmentID { get; set; }
        public string AppointmentStatusAliasName { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PreferredDate { get; set; }
        public string PreferredTime { get; set; }
        public string ItemName { get; set; }
    }

    public class ServiceCoreAvailableDate
    {
        public int ServiceID { get; set; }
        public int ServiceDateID { get; set; }
        public DateTime? ServiceDateFrom { get; set; }
        public DateTime? ServiceDateTo { get; set; }
    }
  

    public class GetCoreServiceDate
    {
        public int ServiceID { get; set; }
        public int BranchID { get; set; }
        public int EmployeeID { get; set; }
    }

    public class BookAnAppointmentCoreInfo
    {
        public int RowTotal { get; set; }
        public int AppointmentID { get; set; }
        public int OrderID { get; set; }
        public int AppointmentStatusID { get; set; }
        public string AppointmentStatusName { get; set; }
        public bool IsSystemUsed { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public DateTime PreferredDate { get; set; }
        public string PreferredTime { get; set; }
        public string TypeOfTreatment { get; set; }
        public string StoreLocation { get; set; }
        public string TypeOfCustomer { get; set; }
        public string MembershipElite { get; set; }
        public int ServiceCategoryID { get; set; }
        public string ServiceCategoryName { get; set; }
        public int ServiceProductID { get; set; }
        public string ServiceProductName { get; set; }
        public string ServiceProductPrice { get; set; }
        public int PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public string StoreLocationName { get; set; }
        public int ServiceDateId { get; set; }
        public string PreferredTimeInterval { get; set; }
        public int PreferredTimeId { get; set; }
        public string ServiceDuration { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string AddedOn { get; set; }
    }

    public class ServiceCoreAvailableTime
    {
        public int ServiceID { get; set; }
        public int ServiceDateID { get; set; }
        public int ServiceTimeID { get; set; }
        public string ServiceTimeFrom { get; set; }
        public string ServiceTimeTo { get; set; }
        //   public int ServiceQuantity { get; set; }
    }
    public class GetServiceCoreAvailableTime
    {
        public int CategoryID { get; set; }
        public int BranchID { get; set; }
        public int EmployeeID { get; set; }
        public string ServiceDateID { get; set; }
        public string ServiceDate { get; set; }
        public int ItemID { get; set; }
    }
}
