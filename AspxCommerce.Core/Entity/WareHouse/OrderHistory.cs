using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public  class OrderHistory
    {

        private System.Nullable<int> _UnPaidOrder;

        private System.Nullable<int> _PaidOrder;

        private System.Nullable<decimal> _GrandTotal;

        private System.Nullable<decimal> _TotalDiscount;

        private System.Nullable<decimal> _TotalTax;

        private decimal _TotalCouponAmount;

        private System.Nullable<decimal> _TotalShippingCost;

        private int _OrderId;

        private int _ItemId;

        private string _ItemName;

        private string _Date;

        private decimal _Price;

        private int _Quantity;

        private System.Nullable<decimal> _Total;

        public OrderHistory()
        {
        }

        [DataMember(Name = "UnPaidOrder", Order = 1)]
        public System.Nullable<int> UnPaidOrder
        {
            get
            {
                return this._UnPaidOrder;
            }
            set
            {
                if ((this._UnPaidOrder != value))
                {
                    this._UnPaidOrder = value;
                }
            }
        }

        [DataMember(Name = "PaidOrder", Order = 2)]
        public System.Nullable<int> PaidOrder
        {
            get
            {
                return this._PaidOrder;
            }
            set
            {
                if ((this._PaidOrder != value))
                {
                    this._PaidOrder = value;
                }
            }
        }

        [DataMember(Name = "GrandTotal", Order = 3)]
        public System.Nullable<decimal> GrandTotal
        {
            get
            {
                return this._GrandTotal;
            }
            set
            {
                if ((this._GrandTotal != value))
                {
                    this._GrandTotal = value;
                }
            }
        }

        [DataMember(Name = "TotalDiscount", Order = 4)]
        public System.Nullable<decimal> TotalDiscount
        {
            get
            {
                return this._TotalDiscount;
            }
            set
            {
                if ((this._TotalDiscount != value))
                {
                    this._TotalDiscount = value;
                }
            }
        }

        [DataMember(Name = "TotalTax", Order = 5)]
        public System.Nullable<decimal> TotalTax
        {
            get
            {
                return this._TotalTax;
            }
            set
            {
                if ((this._TotalTax != value))
                {
                    this._TotalTax = value;
                }
            }
        }

        [DataMember(Name = "TotalCouponAmount", Order = 6)]
        public decimal TotalCouponAmount
        {
            get
            {
                return this._TotalCouponAmount;
            }
            set
            {
                if ((this._TotalCouponAmount != value))
                {
                    this._TotalCouponAmount = value;
                }
            }
        }

        [DataMember(Name = "TotalShippingCost", Order = 7)]
        public System.Nullable<decimal> TotalShippingCost
        {
            get
            {
                return this._TotalShippingCost;
            }
            set
            {
                if ((this._TotalShippingCost != value))
                {
                    this._TotalShippingCost = value;
                }
            }
        }

        [DataMember(Name = "OrderId", Order = 8)]
        public int OrderId
        {
            get
            {
                return this._OrderId;
            }
            set
            {
                if ((this._OrderId != value))
                {
                    this._OrderId = value;
                }
            }
        }

        [DataMember(Name = "ItemId", Order = 9)]
        public int ItemId
        {
            get
            {
                return this._ItemId;
            }
            set
            {
                if ((this._ItemId != value))
                {
                    this._ItemId = value;
                }
            }
        }

        [DataMember(Name = "ItemName", Order = 10)]
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

        [DataMember(Name = "Date", Order = 11)]
        public string Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                if ((this._Date != value))
                {
                    this._Date = value;
                }
            }
        }

        [DataMember(Name = "Price", Order = 12)]
        public decimal Price
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

        [DataMember(Name = "Quantity", Order = 13)]
        public int Quantity
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

        [DataMember(Name = "Total", Order = 14)]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }
    }
}
