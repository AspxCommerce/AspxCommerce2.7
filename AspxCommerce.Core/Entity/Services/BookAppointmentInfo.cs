using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.Core
{
    public class BookAppointmentInfo
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

    public class BookAppointmentGridInfo
    {
        public int RowTotal { get; set; }
        public int AppointmentID { get; set; }
        public int OrderId { get; set; }
        public string AppointmentStatusName { get; set; }
        public string UserName { get; set; }
        public string StoreLocationName { get; set; }
        public string EmployeeName { get; set; }
        public string PreferredTimeInterval { get; set; }
        public string ServiceProductName { get; set; }
        public string ServiceProductPrice { get; set; }
        public DateTime AddedOn { get; set; }
    }

    public class OrderServiceDetailInfo
    {
        public string ServiceCategoryName { get; set; }
        public string ServiceProductName { get; set; }
        public string ServiceDuration { get; set; }
        public string EmployeeName { get; set; }
        public string StoreLocationName { get; set; }
        public DateTime PreferredDate { get; set; }
        public string PreferredTime { get; set; }
        public string PreferredTimeInterval { get; set; }
    }
}
