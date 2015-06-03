using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class OrderTaxInfo
    {
       public int TaxManageRuleID { get; set; }
       public int ItemID { get; set; }
       public decimal TaxSubTotal { get; set; }
       public int StoreID { get; set; }
       public int PortalID { get; set; }
       public string AddedBy { get; set; }
       public string CostVariantsValueIDs { get; set; }
       
    }
}
