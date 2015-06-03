using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class NotifictionMail
    {
        public NotifictionMail()
        {
        }
        public int ItemID { get; set; }
        public string VariantID { get; set; }
        public string VariantValue { get; set; }
        public string Email { get; set; }

    }
    public class GetAllNotificationInfo
    {
        public string ItemSKU { get; set; }
        public string MailStatus { get; set; }
        public string ItemStatus { get; set; }
        public string Customer { get; set; }


    }

    public class InsertNotificationInfo : NotifictionMail
    {

        public string ItemSKU { get; set; }

    }

}
