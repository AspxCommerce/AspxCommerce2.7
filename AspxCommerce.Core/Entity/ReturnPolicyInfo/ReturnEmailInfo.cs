
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class ReturnEmailInfo
    {

            public string returnID { get; set; }
        public string orderID { get; set; } 
        public string itemName { get; set; }
        public string variants { get; set; }
        public string Qty { get; set; }
        public string returnStatus { get; set; }
        public string returnAction { get; set; }
    }
}
