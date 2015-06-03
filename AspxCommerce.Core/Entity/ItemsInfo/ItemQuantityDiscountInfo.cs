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
    public class ItemQuantityDiscountInfo
    {
        #region Constructor
        public ItemQuantityDiscountInfo()
        {
        } 
        #endregion

        #region Private Fields        
        private int _quantityDiscountID;        
        private System.Nullable<decimal> _quantity;       
        private System.Nullable<decimal> _price;
        private string _roleIDs; 
        #endregion
        [DataMember]
        public int QuantityDiscountID
        {
            get
            {
                return this._quantityDiscountID;
            }
            set
            {
                if ((this._quantityDiscountID != value))
                {
                    this._quantityDiscountID = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<decimal> Quantity
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
        [DataMember]
        public string RoleIDs
        {
            get
            {
                return this._roleIDs;
            }
            set
            {
                if ((this._roleIDs != value))
                {
                    this._roleIDs = value;
                }
            }
        }
    }
}
