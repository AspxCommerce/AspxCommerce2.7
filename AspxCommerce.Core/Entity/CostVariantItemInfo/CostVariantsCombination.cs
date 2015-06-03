using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class CostVariantsCombination
    {
     
      //  public string CultureName { get; set; }
        public int ItemId{ get; set; }
      //  public int PortalId { get; set; }
     //   public int StoreId { get; set; }
     //   public string UserName { get; set; }
        public bool IsNewFlag { get; set; }
        public List<VariantCombination> VariantOptions { get; set; }

     
    }
}
