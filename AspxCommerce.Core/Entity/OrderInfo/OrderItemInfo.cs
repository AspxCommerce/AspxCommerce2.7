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




namespace AspxCommerce.Core
{
     public class OrderItemInfo
    {
        #region private members

        private int _orderID;
        private int _shippingAddressID;
        private int _shippingMethodID;
        private int _itemID;
        private int _quantity;
        private decimal _price;
        private decimal _weight;
        private string _remarks;
        private decimal _shippingRate;
        private string _variants;
        private string _variantIDs;
        private bool _isDownloadable;
        private bool _isGiftCard;
         private int _cartItemId;
         private decimal _rewardedPoints;

        #endregion

        #region public members

        public int ShippingAddressID
        {
            get
            {
                return this._shippingAddressID;
            }
            set
            {
                if ((this._shippingAddressID != value))
                {
                    this._shippingAddressID = value;
                }
            }
        }
        public string Variants
        {
            get
            {
                return this._variants;
            }
            set
            {
                if ((this._variants != value))
                {
                    this._variants = value;
                }
            }
        }
        public string VariantIDs
        {
            get
            {
                return this._variantIDs;
            }
            set
            {
                if ((this._variantIDs != value))
                {
                    this._variantIDs = value;
                }
            }
        }
         

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

        public int ItemID
        {
            get
            {
                return this._itemID;
            }
            set
            {
                if ((this._itemID != value))
                {
                    this._itemID = value;
                }
            }
        }

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

        public decimal Weight
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
        public bool IsDownloadable
        {
            get
            {
                return this._isDownloadable;
            }
            set
            {
                if ((this._isDownloadable != value))
                {
                    this._isDownloadable = value;
                }
            }
        }

         public bool IsGiftCard
         {
             get { return this._isGiftCard; }
             set
             {
                 if ((this._isGiftCard != value))
                 {
                     this._isGiftCard = value;
                 }
             }
         }

         public int CartItemId
         {
             get { return this._cartItemId; }
             set
             {
                 if ((this._cartItemId != value))
                 {
                     this._cartItemId = value;
                 }
             }
         }

         public decimal RewardedPoints
         {
             get { return this._rewardedPoints; }
             set
             {
                 if ((this._rewardedPoints != value))
                 {
                     this._rewardedPoints = value;
                 }
             }
         }

         public string KitDescription { get; set; }
         public string KitData { get; set; }


         #endregion 

    }
}
