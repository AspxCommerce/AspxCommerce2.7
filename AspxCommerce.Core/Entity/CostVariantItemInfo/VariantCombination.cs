using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class VariantCombination
    {
        public int CombinationID { get; set; }
        public int DisplayOrder { get; set; }
        public string CombinationType { get; set; }
        public string CombinationValues { get; set; }
        public string CombinationValuesName { get; set; }
        public string CombinationPriceModifier { get; set; }
        public bool CombinationPriceModifierType { get; set; }
        public string CombinationWeightModifier { get; set; }
        public bool CombinationWeightModifierType { get; set; }
        public int CombinationQuantity { get; set; }
        public bool CombinationIsActive { get; set; }
        public string ImageFile { get; set; }
    }
}
