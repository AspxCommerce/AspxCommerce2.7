using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   
    public class OrderLabel 
    {        

        public int OrderId { get; set; }
        public int UserBillingAddressId { get; set; }
        public int UserShippingAddressId { get; set; }
        public int ShippingMethodId { get; set; }
        public int ShippingProviderId { get; set; }
        public string ShippingMethodName { get; set; }
        public string ItemNames { get; set; }
        public string Weights { get; set; }
        public string ItemQuantities { get; set; }
        public string Prices { get; set; }
        public int StoreId { get; set; }
        public int PortalId { get; set; }
        public int ProviderId { get; set; }
        public string CultureName { get; set; }

    }
}
