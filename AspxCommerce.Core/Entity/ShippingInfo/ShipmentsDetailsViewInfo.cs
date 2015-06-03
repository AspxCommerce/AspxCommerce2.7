/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class ShipmentsDetailsViewInfo
    {
        private int _shippingMethodID;
        private int _orderID;
        private string _shippingMethod;
        private string _itemName;
        private string _sku;
        private decimal _price;
        private int _quantity;
        private int _weight;
        private decimal _discountAmount;
        private decimal _couponAmount;
        private decimal _rewardDiscountAmount;
        private decimal _taxTotal;
        private decimal _shippingRate;
        private decimal _grandSubTotal;
        private decimal _grandTotal;
        private string _shippingAddress;
        private string _shipmentDate;
        private decimal _totalCoupon;
        private decimal _totalShippingRate;
        private string _costVariants;
        private string _remarks;
        private decimal _totalShippingCost;
		 private string _giftCard;
        public ShipmentsDetailsViewInfo()
        {
        }
        [DataMember]
        public int ShippingMethodID
        {
            get
            {
                return this._shippingMethodID;
            }
            set
            {
                if ((this._shippingMethodID != value))
                {
                    this._shippingMethodID = value;
                }
            }
        }
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
        public string CostVariants
        {
            get
            {
                return this._costVariants;
            }
            set
            {
                if ((this._costVariants != value))
                {
                    this._costVariants = value;
                }
            }
        }

        [DataMember]
        public string ShippingMethod
        {
            get
            {
                return this._shippingMethod;
            }
            set
            {
                if ((this._shippingMethod != value))
                {
                    this._shippingMethod = value;
                }
            }
        }
        [DataMember]
        public string ItemName
        {
            get
            {
                return this._itemName;
            }
            set
            {
                if ((this._itemName != value))
                {
                    this._itemName = value;
                }
            }
        }

        [DataMember]
        public string SKU
        {
            get
            {
                return this._sku;
            }
            set
            {
                if ((this._sku != value))
                {
                    this._sku = value;
                }
            }
        }

        [DataMember]
        public decimal Price
        {
            get
            {
                return this._price;
            }
            set
            {
                if ((this._price != value))
                {
                    this._price = value;
                }
            }
        }

        [DataMember]
        public int Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                if ((this._quantity != value))
                {
                    this._quantity = value;
                }
            }
        }

        [DataMember]
        public int Weight
        {
            get
            {
                return this._weight;
            }
            set
            {
                if ((this._weight != value))
                {
                    this._weight = value;
                }
            }
        }
        [DataMember]
        public decimal DiscountAmount
        {
            get
            {
                return this._discountAmount;
            }
            set
            {
                if ((this._discountAmount != value))
                {
                    this._discountAmount = value;
                }
            }
        }
        [DataMember]
        public decimal CouponAmount
        {
            get
            {
                return this._couponAmount;
            }
            set
            {
                if ((this._couponAmount != value))
                {
                    this._couponAmount = value;
                }
            }
        }
        [DataMember]
        public decimal RewardDiscountAmount
        {
            get
            {
                return this._rewardDiscountAmount;
            }
            set
            {
                if ((this._rewardDiscountAmount != value))
                {
                    this._rewardDiscountAmount = value;
                }
            }
        }
        [DataMember]
        public decimal TaxTotal
        {
            get
            {
                return this._taxTotal;
            }
            set
            {
                if ((this._taxTotal != value))
                {
                    this._taxTotal = value;
                }
            }
        }

        [DataMember]
        public decimal GrandTotal
        {
            get
            {
                return this._grandTotal;
            }
            set
            {
                if ((this._grandTotal != value))
                {
                    this._grandTotal = value;
                }
            }
        }

        [DataMember]
        public decimal GrandSubTotal
        {
            get
            {
                return this._grandSubTotal;
            }
            set
            {
                if ((this._grandSubTotal != value))
                {
                    this._grandSubTotal = value;
                }
            }
        }
        [DataMember]
        public decimal ShippingRate
        {
            get
            {
                return this._shippingRate;
            }
            set
            {
                if ((this._shippingRate != value))
                {
                    this._shippingRate = value;
                }
            }
        }

        [DataMember]
        public string ShippingAddress
        {
            get
            {
                return this._shippingAddress;
            }
            set
            {
                if ((this._shippingAddress != value))
                {
                    this._shippingAddress = value;
                }
            }
        }
        [DataMember]
        public string ShipmentDate
        {
            get
            {
                return this._shipmentDate;
            }
            set
            {
                if((this._shipmentDate!=value))
                {
                    this._shipmentDate=value;
                }
            }
        }

        [DataMember]
        public decimal TotalCoupon
        {
            get
            {
                return this._totalCoupon;
            }
            set
            {
                if ((this._totalCoupon != value))
                {
                    this._totalCoupon = value;
                }
            }
        }
        [DataMember]
        public decimal TotalShippingRate
        {
            get
            {
                return this._totalShippingRate;
            }
            set
            {
                if ((this._totalShippingRate != value))
                {
                    this._totalShippingRate = value;
                }
            }
        }
        [DataMember]
        public string Remarks
        {
            get
            {
                return this._remarks;
            }
            set
            {
                if ((this._remarks != value))
                {
                    this._remarks = value;
                }
            }
        }
        [DataMember]
        public decimal TotalShippingCost
        {
            get
            {
                return this._totalShippingCost;
            }
            set
            {
                if ((this._totalShippingCost != value))
                {
                    this._totalShippingCost = value;
                }
            }
        }
		  [DataMember]
        public string GiftCard
        {
            get
            {
                return this._giftCard;
            }
            set
            {
                if ((this._giftCard != value))
                {
                    this._giftCard = value;
                }
            }
        }
    }
}
