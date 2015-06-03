using System;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    
    [DataContract]
    [Serializable]
    public class CartTaxforOrderInfo
    {
        public string TaxManageRuleName { get; set; }

        public int TaxManageRuleID { get; set; }

        public decimal TaxRateValue { get; set; }

        public string ItemID { get; set; }

        public string CostVariantsValueIDs { get; set; }

        public decimal Country { get; set; }

        public decimal State { get; set; }

        public decimal Zip { get; set; }

        public int AddressID { get; set; }

    }
    public class CartDataInfo
    {
        public string TaxManageRuleName { get; set; }

        public int TaxManageRuleID { get; set; }

        public decimal TaxRateValue { get; set; }

        public string ItemID { get; set; }

        public string CostVariantsValueIDs { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public int AddressID { get; set; }

    }
}
