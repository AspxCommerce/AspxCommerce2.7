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
  public  class MyOrderListForReturnInfo
    {        
        private System.Nullable<int> _orderID;     
        private System.Nullable<int> _itemID;
        private System.Nullable<int> _itemTypeID;
        private string _itemName;
        private string _SKU;
        private string _costVaraitns;
        private string _variantID;
        private System.Nullable<int> _quantity;
        private System.Nullable<int> _addressID;
        private System.Nullable<int> _customerID;
        private System.Nullable<int> _orderDateCount;
        private System.Nullable<int> _isReturnApplied;
        
        public MyOrderListForReturnInfo()
        {
        }
        
        [DataMember]
        public System.Nullable<int> OrderID
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
        public System.Nullable<int> ItemID
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
        public System.Nullable<int> ItemTypeID
        {
            get
            {
                return this._itemTypeID;
            }
            set
            {
                if ((this._itemTypeID != value))
                {
                    this._itemTypeID = value;
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
        [DataMember]
         public string CostVariants
         {
             get
             {
                 return this._costVaraitns;
             }
             set
             {
                 if ((this._costVaraitns != value))
                 {
                     this._costVaraitns = value;
                 }
             }
         }
        [DataMember]
        public string VariantID
        {
            get
            {
                return this._variantID;
            }
            set
            {
                if ((this._variantID != value))
                {
                    this._variantID = value;
                }
            }
        }
        [DataMember]
         public System.Nullable<int> Quantity
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
        public System.Nullable<int> AddressID
        {
            get
            {
                return this._addressID;
            }
            set
            {
                if ((this._addressID != value))
                {
                    this._addressID = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> CustomerID
        {
            get
            {
                return this._customerID;
            }
            set
            {
                if ((this._customerID != value))
                {
                    this._customerID = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> OrderDateCount
        {
            get
            {
                return this._orderDateCount;
            }
            set
            {
                if ((this._orderDateCount != value))
                {
                    this._orderDateCount = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> IsReturnApplied
        {
            get
            {
                return this._isReturnApplied;
            }
            set
            {
                if ((this._isReturnApplied != value))
                {
                    this._isReturnApplied = value;
                }
            }
        }

    }
}
