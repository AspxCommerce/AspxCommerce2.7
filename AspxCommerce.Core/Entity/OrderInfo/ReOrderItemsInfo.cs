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
    public class ReOrderItemsInfo
    {

        private int _orderID;

        private int _itemID;
       
        private bool _isPercentage;    

        private string _itemName;

        private string _sku;

        private decimal _price;

        private int _quantity;

        private int _weight;

        private string _costVariants;

        private string _itemCostVariantIDs;
      
        public ReOrderItemsInfo()
        {
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

        
        [DataMember]
        public bool IsPercentage
        {
            get
            {
                return this._isPercentage;
            }
            set
            {
                if ((this._isPercentage != value))
                {
                    this._isPercentage = value;
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
        public string ItemCostVariantValueIDs
        {
            get
            {
                return this._itemCostVariantIDs;
            }
            set
            {
                if ((this._itemCostVariantIDs != value))
                {
                    this._itemCostVariantIDs = value;
                }
            }
        }

        
    }

}
