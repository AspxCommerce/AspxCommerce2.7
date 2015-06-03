using System;
using System.Runtime.Serialization;

namespace AspxCommerce.PayPal
{
    [Serializable]
    [DataContract]
    public class CartInfoforPaypal
    {
        [DataMember]
        private int _CartID;

        [DataMember]
        private string _SKU;

        [DataMember]
        private System.Nullable<int> _ItemID;

        [DataMember]
        private string _ItemName;

        [DataMember]
        private string _ImagePath;

        [DataMember]
        private string _AlternateText;

        [DataMember]
        private System.Nullable<decimal> _Price;

        [DataMember]
        private System.Nullable<int> _Quantity;

        [DataMember]
        private System.Nullable<decimal> _Weight;

        [DataMember]
        private System.Nullable<decimal> _TaxRateValue;

        [DataMember]
        private string _ShortDescription;

        [DataMember]
        private System.Nullable<decimal> _TotalItemCost;

        [DataMember]
        private string _Remarks;

        [DataMember]
        private string _SessionCode;

        [DataMember]
        private string _CostVariantsValueIDs;

        [DataMember]
        private string _UserName;

        [DataMember]
        private System.Nullable<int> _StoreID;

        [DataMember]
        private System.Nullable<int> _PortalID;

        public CartInfoforPaypal()
        {
        }

        public int CartID
        {
            get
            {
                return this._CartID;
            }
            set
            {
                if ((this._CartID != value))
                {
                    this._CartID = value;
                }
            }
        }

        public string SKU
        {
            get
            {
                return this._SKU;
            }
            set
            {
                if ((this._SKU != value))
                {
                    this._SKU = value;
                }
            }
        }

        public System.Nullable<int> ItemID
        {
            get
            {
                return this._ItemID;
            }
            set
            {
                if ((this._ItemID != value))
                {
                    this._ItemID = value;
                }
            }
        }

        public string ItemName
        {
            get
            {
                return this._ItemName;
            }
            set
            {
                if ((this._ItemName != value))
                {
                    this._ItemName = value;
                }
            }
        }

        public string ImagePath
        {
            get
            {
                return this._ImagePath;
            }
            set
            {
                if ((this._ImagePath != value))
                {
                    this._ImagePath = value;
                }
            }
        }

        public string AlternateText
        {
            get
            {
                return this._AlternateText;
            }
            set
            {
                if ((this._AlternateText != value))
                {
                    this._AlternateText = value;
                }
            }
        }

        public System.Nullable<decimal> Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                if ((this._Price != value))
                {
                    this._Price = value;
                }
            }
        }
        public System.Nullable<int> Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                if ((this._Quantity != value))
                {
                    this._Quantity = value;
                }
            }
        }
        public System.Nullable<decimal> Weight
        {
            get
            {
                return this._Weight;
            }
            set
            {
                if ((this._Weight != value))
                {
                    this._Weight = value;
                }
            }
        }

        public System.Nullable<decimal> TaxRateValue
        {
            get
            {
                return this._TaxRateValue;
            }
            set
            {
                if ((this._TaxRateValue != value))
                {
                    this._TaxRateValue = value;
                }
            }
        }

        public string ShortDescription
        {
            get
            {
                return this._ShortDescription;
            }
            set
            {
                if ((this._ShortDescription != value))
                {
                    this._ShortDescription = value;
                }
            }
        }

        public System.Nullable<decimal> TotalItemCost
        {
            get
            {
                return this._TotalItemCost;
            }
            set
            {
                if ((this._TotalItemCost != value))
                {
                    this._TotalItemCost = value;
                }
            }
        }
        public string Remarks
        {
            get
            {
                return this._Remarks;
            }
            set
            {
                if ((this._Remarks != value))
                {
                    this._Remarks = value;
                }
            }
        }

        public string SessionCode
        {
            get
            {
                return this._SessionCode;
            }
            set
            {
                if ((this._SessionCode != value))
                {
                    this._SessionCode = value;
                }
            }
        }

        public string CostVariantsValueIDs
        {
            get
            {
                return this._CostVariantsValueIDs;
            }
            set
            {
                if ((this._CostVariantsValueIDs != value))
                {
                    this._CostVariantsValueIDs = value;
                }
            }
        }

        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this._UserName = value;
                }
            }
        }

        public System.Nullable<int> StoreID
        {
            get
            {
                return this._StoreID;
            }
            set
            {
                if ((this._StoreID != value))
                {
                    this._StoreID = value;
                }
            }
        }

        public System.Nullable<int> PortalID
        {
            get
            {
                return this._PortalID;
            }
            set
            {
                if ((this._PortalID != value))
                {
                    this._PortalID = value;
                }
            }
        }
    }
}

