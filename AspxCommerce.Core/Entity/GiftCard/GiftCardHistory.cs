using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class GiftCardHistory
    {
        public int GiftCardId { get; set; }
        public int OrderId { get; set; }
        public decimal UsedAmount { get; set; }
        public decimal Balance { get; set; }
        public string GiftCardCode { get; set; }
        public DateTime AddedOn { get; set; }
       
        public string Note { get; set; }
      
    }
}
