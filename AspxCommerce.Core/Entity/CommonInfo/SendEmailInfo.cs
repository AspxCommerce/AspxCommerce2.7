using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class SendEmailInfo
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string MessageBody { get; set; }
    }

    public class CouponEmailInfo : SendEmailInfo
    {
        public List<string> MessageBodyTemplate { get; set; }
    }
}
