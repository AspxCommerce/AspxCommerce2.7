using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.AdminNotification
{
   public  class NotificationItemList
    {
       public List<OutOfStockInfo> OutOfStockDetails { get; set; }
       public List<ItemLowStockInfo> ItemLowStockDetails {get; set;}

    }
    [Serializable]
    public class OutOfStockInfo
    {

        private int _rowNumber;
        private int _itemId;
        private string _sku;
        private int _itemTypeId;
        private int _attributeSetId;
        private string _currencyCode;
        private int _itemCostvariantId;
        private string  _addedOn;
        private bool _isChecked;
        private bool _isCheckedFull;
        private string _itemName;
        

  

    public int RowNumber
       {
            get { return this._rowNumber; }
            set
            {
                if (_rowNumber != value)
                {
                    _rowNumber = value;
                }
            }
        }


        public int ItemID
       {
            get { return this._itemId; }
            set
            {
                if (_itemId != value)
                {
                    _itemId = value;
                }
            }
        }

        public string  SKU
        {
            get { return this._sku; }
            set
            {
                if (_sku != value)
                {
                    _sku = value;
                }
            }
        }
        public int ItemTypeID
        {
            get { return this._itemTypeId; }
            set
            {
                if (_itemTypeId != value)
                {
                    _itemTypeId = value;
                }
            }
        }
        public int AttributeSetID
        {
            get { return this._attributeSetId; }
            set
            {
                if (_attributeSetId != value)
                {
                    _attributeSetId = value;
                }
            }
        }

        public string CurrencyCode
        {
            get { return this._currencyCode; }
            set
            {
                if (_currencyCode != value)
                {
                    _currencyCode = value;
                }
            }
        }


         public int ItemCostvariantId
       {
            get { return this._itemCostvariantId; }
            set
            {
                if (_itemCostvariantId != value)
                {
                    _itemCostvariantId = value;
                }
            }
        }


          public string  AddedOn
       {
            get { return this._addedOn; }
            set
            {
                if (_addedOn != value)
                {
                    _addedOn = value;
                }
            }
        }

           public bool  IsCheked
       {
            get { return this._isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                }
            }
        }


         public bool  IsChekedFull
       {
            get { return this._isCheckedFull; }
            set
            {
                if (_isCheckedFull != value)
                {
                    _isCheckedFull = value;
                }
            }
        }
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


    }
        
    

        public class ItemLowStockInfo
        {
             private int _rowNumber;
        private int _itemId;
        private string _sku;
        private int _itemTypeId;
        private int _attributeSetId;
        private string _currencyCode;
        private int _itemCostvariantId;
        private string  _addedOn;
        private bool _isChecked;
        private bool _isCheckedFull;
        private string _itemName;


        public int RowNumber
        {
            get { return this._rowNumber; }
            set
            {
                if (_rowNumber != value)
                {
                    _rowNumber = value;
                }
            }
        }


        public int ItemID
        {
            get { return this._itemId; }
            set
            {
                if (_itemId != value)
                {
                    _itemId = value;
                }
            }
        }
        public string SKU
        {
            get { return this._sku; }
            set
            {
                if (_sku != value)
                {
                    _sku = value;
                }
            }
        }
        public int ItemTypeID
        {
            get { return this._itemTypeId; }
            set
            {
                if (_itemTypeId != value)
                {
                    _itemTypeId = value;
                }
            }
        }
        public int AttributeSetID
        {
            get { return this._attributeSetId; }
            set
            {
                if (_attributeSetId != value)
                {
                    _attributeSetId = value;
                }
            }
        }

        public string CurrencyCode
        {
            get { return this._currencyCode; }
            set
            {
                if (_currencyCode != value)
                {
                    _currencyCode = value;
                }
            }
        }



        public int ItemCostvariantId
        {
            get { return this._itemCostvariantId; }
            set
            {
                if (_itemCostvariantId != value)
                {
                    _itemCostvariantId = value;
                }
            }
        }


        public string AddedOn
        {
            get { return this._addedOn; }
            set
            {
                if (_addedOn != value)
                {
                    _addedOn = value;
                }
            }
        }

        public bool IsCheked
        {
            get { return this._isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                }
            }
        }


        public bool IsChekedFull
        {
            get { return this._isCheckedFull; }
            set
            {
                if (_isCheckedFull != value)
                {
                    _isCheckedFull = value;
                }
            }
        }
        public string ItemName
        {
            get { return this._itemName; }
            set
            {
                if (_itemName != value)
                {
                    _itemName = value;
                }
            }
        }






        }

}
