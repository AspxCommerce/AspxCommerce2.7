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
    public class ShoppingCartInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_cartID", Order = 1)]
        private int _cartID;

        [DataMember(Name = "_itemID", Order = 2)]
		private System.Nullable<int> _itemID;

        [DataMember(Name = "_userName", Order = 3)]
        private string _userName;

        [DataMember(Name = "_itemName", Order = 4)]
		private string _itemName;

        [DataMember(Name = "_quantity", Order = 5)]
		private System.Nullable<int> _quantity;

        [DataMember(Name = "_price", Order = 6)]
		private System.Nullable<decimal> _price;

        [DataMember(Name = "_weight", Order = 7)]
		private System.Nullable<decimal> _weight;

        [DataMember(Name = "_sku", Order = 8)]
        private string _sku;


        public ShoppingCartInfo()
		{
		}
		
		public System.Nullable<int> RowTotal
		{
			get
			{
				return this._rowTotal;
			}
			set
			{
				if ((this._rowTotal != value))
				{
					this._rowTotal = value;
				}
			}
		}
		
		public int CartID
		{
			get
			{
				return this._cartID;
			}
			set
			{
				if ((this._cartID != value))
				{
					this._cartID = value;
				}
			}
		}
		
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

        public string UserName
        {
            get
            {
                return this._userName;
            }
            set
            {
                if ((this._userName != value))
                {
                    this._userName = value;
                }
            }
        }
		
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
		
		public System.Nullable<decimal> Price
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
		
		public System.Nullable<decimal> Weight
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
    }
}
