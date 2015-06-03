using System;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
     [DataContract]
     [Serializable]
    
    public class OrderItemsTaxInfo
    {
        private int _orderID;
        private int _taxManageRuleID;
        private string _taxManageRuleName;
        private decimal _taxSubTotal;        

        [DataMember]
		public int OrderID
		{
			get
			{
				return this._orderID;
			}
			set
			{
				if ((this._orderID != value))
				{
					this._orderID = value;
				}
			}
		}
        [DataMember]
        public int TaxManageRuleID
        {
            get
            {
                return this._taxManageRuleID;
            }
            set
            {
                if ((this._taxManageRuleID != value))
                {
                    this._taxManageRuleID = value;
                }
            }
        }

        [DataMember]
        public string TaxManageRuleName
        {
            get
            {
                return this._taxManageRuleName;
            }
            set
            {
                if ((this._taxManageRuleName != value))
                {
                    this._taxManageRuleName = value;
                }
            }
        }

        [DataMember]
        public decimal TaxSubTotal
        {
            get
            {
                return this._taxSubTotal;
            }
            set
            {
                if ((this._taxSubTotal != value))
                {
                    this._taxSubTotal = value;
                }
            }
        }


    }

   
}
