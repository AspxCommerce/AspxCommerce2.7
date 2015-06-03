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
    public class CategoryItemInfo
    {
        [DataMember(Name = "_RowTotal", Order = 0)]
        private int _rowTotal;

        [DataMember(Name = "ID", Order = 1)]
        private Int32 _ID;

        [DataMember(Name = "ItemID", Order = 2)]
        private Int32 _itemID;

        [DataMember(Name = "SKU", Order = 3)]
        private string _sku;

        [DataMember(Name = "Name", Order = 4)]
        private string _name;

        [DataMember(Name = "Price", Order = 5)]
        private decimal _price;
		
        [DataMember(Name = "_isChecked", Order = 6)]
        private System.Nullable<bool> _isChecked;

        public int RowTotal
        {
            get { return _rowTotal; }
            set { _rowTotal = value; }
        }

        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Int32 ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        public string SKU
        {
            get { return _sku; }
            set { _sku = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
		
        public System.Nullable<bool> IsCheckedID
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }
    }

    public class GetCategoryItemInfo
    {
        public int CategoryID { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}
