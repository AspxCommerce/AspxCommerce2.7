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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.CompareItem
{
    [DataContract]
    [Serializable]
    public class ItemsCompareInfo
    {
        #region "Private Members"
        int _compareItemID;
        int _itemID;
        string _costVariantValueID;
        string _itemCostVariantValue;
        string _itemName;
        string _sku;
        string _imagePath;
        #endregion
        #region "Constructors"
        public ItemsCompareInfo()
        {
        }
        public ItemsCompareInfo(int compareItemID, int itemID, string costVariantValueID, string itemCostVariantValue, string itemName, string sku)
        {
            this.CompareItemID = compareItemID;
            this.ItemID = itemID;
            this.CostVariantValueID = costVariantValueID;
            this.ItemCostVariantValue = itemCostVariantValue;
            this.ItemName = itemName;
            this.SKU = sku;
        }
        #endregion
        #region "Public Members"
        [DataMember]
        public int CompareItemID
        {
            get { return _compareItemID; }
            set { _compareItemID = value; }
        }
        [DataMember]
        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }
        [DataMember]
        public string CostVariantValueID
        {
            get { return _costVariantValueID; }
            set { _costVariantValueID = value; }
        }
        [DataMember]
        public string ItemCostVariantValue
        {
            get { return _itemCostVariantValue; }
            set { _itemCostVariantValue = value; }
        }
        [DataMember]
        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
        [DataMember]
        public string SKU
        {
            get { return _sku; }
            set { _sku = value; }
        }
        [DataMember]
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        #endregion
    }
}
