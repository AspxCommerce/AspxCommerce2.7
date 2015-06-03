using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class GiftCardUsage
    {
      
        public string GiftCardCode { get; set; }
        public decimal Price { get; set; }
        public decimal Balance { get; set; }
        public int GiftCardId { get; set; }
        public decimal ReducedAmount { get; set; }
        
      
    }
}
