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
    public class OrderItemsInfo
    {

        private int _orderID;

        private int _orderType;

        private string _inVoiceNumber;

        private string _orderedDate;

        private string _orderStatus;

        private decimal _couponAmount;

        private decimal _rewardDiscountAmount;

        private bool _isPercentage;

        private decimal _taxTotal;

        private decimal _totalShippingCost;

        private decimal _discountAmount;

        private decimal _grandTotal;

        private decimal _grandSubTotal;

        private string _paymentGatewayTypeName;

        private string _paymentMethodName;

        private string _billingAddress;

        private string _itemName;

        private string _sku;

        private decimal _price;

        private int _quantity;

        private int _weight;

        private string _costVariants;

        private System.Nullable<decimal> _shippingRate;

        private string _shippingAddress;

        private string _shippingMethod;

        private string _remarks;

        private string _storeName;

        private string _storeDescription;

        private string _giftCard;

        public OrderItemsInfo()
        {
        }

        [DataMember]
        public int OrderID
        {
            get { return this._orderID; }
            set
            {
                if ((this._orderID != value))
                {
                    this._orderID = value;
                }
            }
        }

        [DataMember]
        public int OrderType
        {
            get { return this._orderType; }
            set
            {
                if ((this._orderType != value))
                {
                    this._orderType = value;
                }
            }
        }

        [DataMember]
        public string InVoiceNumber
        {
            get { return this._inVoiceNumber; }
            set
            {
                if ((this._inVoiceNumber != value))
                {
                    this._inVoiceNumber = value;
                }
            }
        }

        [DataMember]
        public string OrderedDate
        {
            get { return this._orderedDate; }
            set
            {
                if ((this._orderedDate != value))
                {
                    this._orderedDate = value;
                }
            }
        }

        [DataMember]
        public string OrderStatus
        {
            get { return this._orderStatus; }
            set
            {
                if ((this._orderStatus != value))
                {
                    this._orderStatus = value;
                }
            }
        }

        [DataMember]
        public decimal CouponAmount
        {
            get { return this._couponAmount; }
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
            get { return this._rewardDiscountAmount; }
            set
            {
                if ((this._rewardDiscountAmount != value))
                {
                    this._rewardDiscountAmount = value;
                }
            }
        }

        [DataMember]
        public bool IsPercentage
        {
            get { return this._isPercentage; }
            set
            {
                if ((this._isPercentage != value))
                {
                    this._isPercentage = value;
                }
            }
        }

        [DataMember]
        public decimal TaxTotal
        {
            get { return this._taxTotal; }
            set
            {
                if ((this._taxTotal != value))
                {
                    this._taxTotal = value;
                }
            }
        }

        [DataMember]
        public decimal TotalShippingCost
        {
            get { return this._totalShippingCost; }
            set
            {
                if ((this._totalShippingCost != value))
                {
                    this._totalShippingCost = value;
                }
            }
        }

        [DataMember]
        public decimal DiscountAmount
        {
            get { return this._discountAmount; }
            set
            {
                if ((this._discountAmount != value))
                {
                    this._discountAmount = value;
                }
            }
        }

        [DataMember]
        public decimal GrandTotal
        {
            get { return this._grandTotal; }
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
            get { return this._grandSubTotal; }
            set
            {
                if ((this._grandSubTotal != value))
                {
                    this._grandSubTotal = value;
                }
            }
        }

        [DataMember]
        public string PaymentGatewayTypeName
        {
            get { return this._paymentGatewayTypeName; }
            set
            {
                if ((this._paymentGatewayTypeName != value))
                {
                    this._paymentGatewayTypeName = value;
                }
            }
        }

        [DataMember]
        public string PaymentMethodName
        {
            get { return this._paymentMethodName; }
            set
            {
                if ((this._paymentMethodName != value))
                {
                    this._paymentMethodName = value;
                }
            }
        }

        [DataMember]
        public string BillingAddress
        {
            get { return this._billingAddress; }
            set
            {
                if ((this._billingAddress != value))
                {
                    this._billingAddress = value;
                }
            }
        }

        [DataMember]
        public string ItemName
        {
            get { return this._itemName; }
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
            get { return this._sku; }
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
            get { return this._price; }
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
            get { return this._quantity; }
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
            get { return this._weight; }
            set
            {
                if ((this._weight != value))
                {
                    this._weight = value;
                }
            }
        }

        [DataMember]
        public string CostVariants
        {
            get { return this._costVariants; }
            set
            {
                if ((this._costVariants != value))
                {
                    this._costVariants = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<decimal> ShippingRate
        {
            get { return this._shippingRate; }
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
            get { return this._shippingAddress; }
            set
            {
                if ((this._shippingAddress != value))
                {
                    this._shippingAddress = value;
                }
            }
        }

        [DataMember]
        public string ShippingMethod
        {
            get { return this._shippingMethod; }
            set
            {
                if ((this._shippingMethod != value))
                {
                    this._shippingMethod = value;
                }
            }
        }

        [DataMember]
        public string Remarks
        {
            get { return this._remarks; }
            set
            {
                if ((this._remarks != value))
                {
                    this._remarks = value;
                }
            }
        }

        [DataMember]
        public string StoreName
        {
            get { return this._storeName; }
            set
            {
                if ((this._storeName != value))
                {
                    this._storeName = value;
                }
            }
        }

        [DataMember]
        public string StoreDescription
        {
            get { return this._storeDescription; }
            set
            {
                if ((this._storeDescription != value))
                {
                    this._storeDescription = value;
                }
            }
        }

        [DataMember]
        public string GiftCard
        {
            get { return this._giftCard; }
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
