using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aspxcommerce.RealTimeCartManagement
{
    public class RealTimeNotificationInfo
    {       
            public string ConnectionId { get; set; }
            public string SessionId { get; set; }
    }

    public class OnlineUsersInfo
    {   
        public string UserName{get;set;}
        public string ConnectionId { get; set; }
        public DateTime ConnectionDate{get;set;}
        public string SessionCode { get; set; }
        public bool Status { get; set; }

    }

}
