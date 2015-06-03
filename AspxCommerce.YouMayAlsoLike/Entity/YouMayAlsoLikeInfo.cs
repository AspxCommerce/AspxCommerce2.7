using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AspxCommerce.YouMayAlsoLike
{
    public class YouMayAlsoLikeInfo
    {
         #region Constructor
        public YouMayAlsoLikeInfo()
        {
        }
        #endregion

        #region Private Fields
        [DataMember]
        private int _rowTotal;

        [DataMember]
        private bool _isCostVariantItem;

        [DataMember]
        private int _itemID;

        [DataMember]
        private int _itemTypeID;

        [DataMember]
        private string _costVariantItemID;
        [DataMember]

        private System.Nullable<System.DateTime> _dateFrom;

        [DataMember]
        private System.Nullable<System.DateTime> _dateTo;

        [DataMember]
        private string _isFeatured;

        [DataMember]
        private string _sku;

        [DataMember]
        private string _name;

        [DataMember]
        private string _description;

        [DataMember]
        private string _shortDescription;

        [DataMember]
        private int _quantity;

        [DataMember]
        private string _price;

        [DataMember]
        private string _weight;

        [DataMember]
        private string _listPrice;

        [DataMember]
        private System.Nullable<bool> _hidePrice;

        [DataMember]
        private System.Nullable<bool> _hideInRSSFeed;

        [DataMember]
        private System.Nullable<bool> _hideToAnonymous;

        [DataMember]
        private System.Nullable<bool> _isOutOfStock;

        [DataMember]
        private System.Nullable<System.DateTime> _addedOn;

        [DataMember]
        private string _baseImage;

        [DataMember]
        private string _alternateText;

        [DataMember]
        private int _taxRuleID;

        [DataMember]
        private string _taxRateValue;

        [DataMember]
        private string _sampleLink;

        [DataMember]
        private string _sampleFile;

        [DataMember]
        private string _discountPrice;

        [DataMember]
        private string _itemVideoIDs;

        [DataMember]
        private System.Nullable<int> _brandID;

        [DataMember]
        private string _brandName;

        [DataMember]
        private string _brandDescription;

        [DataMember]
        private string _brandImageUrl;

        [DataMember]
        private int? _itemViewCount;

        [DataMember] private string _status;
		
        [DataMember] private string _itemTagIDs;
		
        [DataMember] private int _statusID;
		
		[DataMember]
        private string _itemCategories;
		
        #endregion

        #region Public Fields

        public int RowTotal
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

        public bool IsCostVariantItem
        {
            get
            {
                return this._isCostVariantItem;
            }
            set
            {
                if ((this._isCostVariantItem != value))
                {
                    this._isCostVariantItem = value;
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
          public int ItemTypeID
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
        public string CostVariantItemID
        {
            get
            {
                return this._costVariantItemID;
            }
            set
            {
                if ((this._costVariantItemID != value))
                {
                    this._costVariantItemID = value;
                }
            }
        }
        public System.Nullable<System.DateTime> DateFrom
        {
            get
            {
                return this._dateFrom;
            }
            set
            {
                if ((this._dateFrom != value))
                {
                    this._dateFrom = value;
                }
            }
        }

        public System.Nullable<System.DateTime> DateTo
        {
            get
            {
                return this._dateTo;
            }
            set
            {
                if ((this._dateTo != value))
                {
                    this._dateTo = value;
                }
            }
        }

        public string IsFeatured
        {
            get
            {
                return this._isFeatured;
            }
            set
            {
                if ((this._isFeatured != value))
                {
                    this._isFeatured = value;
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

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if ((this._name != value))
                {
                    this._name = value;
                }
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                if ((this._description != value))
                {
                    this._description = value;
                }
            }
        }

        public string ShortDescription
        {
            get
            {
                return this._shortDescription;
            }
            set
            {
                if ((this._shortDescription != value))
                {
                    this._shortDescription = value;
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

        public string Price
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

        public string Weight
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

        public string ListPrice
        {
            get
            {
                return this._listPrice;
            }
            set
            {
                if ((this._listPrice != value))
                {
                    this._listPrice = value;
                }
            }
        }

        public System.Nullable<bool> HidePrice
        {
            get
            {
                return this._hidePrice;
            }
            set
            {
                if ((this._hidePrice != value))
                {
                    this._hidePrice = value;
                }
            }
        }

        public System.Nullable<bool> HideInRSSFeed
        {
            get
            {
                return this._hideInRSSFeed;
            }
            set
            {
                if ((this._hideInRSSFeed != value))
                {
                    this._hideInRSSFeed = value;
                }
            }
        }

        public System.Nullable<bool> HideToAnonymous
        {
            get
            {
                return this._hideToAnonymous;
            }
            set
            {
                if ((this._hideToAnonymous != value))
                {
                    this._hideToAnonymous = value;
                }
            }
        }

        public System.Nullable<bool> IsOutOfStock
        {
            get
            {
                return this._isOutOfStock;
            }
            set
            {
                if ((this._isOutOfStock != value))
                {
                    this._isOutOfStock = value;
                }
            }
        }

        public System.Nullable<System.DateTime> AddedOn
        {
            get
            {
                return this._addedOn;
            }
            set
            {
                if ((this._addedOn != value))
                {
                    this._addedOn = value;
                }
            }
        }

        public string BaseImage
        {
            get
            {
                return this._baseImage;
            }
            set
            {
                if ((this._baseImage != value))
                {
                    this._baseImage = value;
                }
            }
        }

        public string AlternateText
        {
            get
            {
                return this._alternateText;
            }
            set
            {
                if ((this._alternateText != value))
                {
                    this._alternateText = value;
                }
            }
        }

        public int TaxRuleID
        {
            get
            {
                return this._taxRuleID;
            }
            set
            {
                if ((this._taxRuleID != value))
                {
                    this._taxRuleID = value;
                }
            }
        }

        public string TaxRateValue
        {
            get
            {
                return this._taxRateValue;
            }
            set
            {
                if ((this._taxRateValue != value))
                {
                    this._taxRateValue = value;
                }
            }
        }

        public string SampleLink
        {
            get
            {
                return this._sampleLink;
            }
            set
            {
                if ((this._sampleLink != value))
                {
                    this._sampleLink = value;
                }
            }
        }

        public string SampleFile
        {
            get
            {
                return this._sampleFile;
            }
            set
            {
                if ((this._sampleFile != value))
                {
                    this._sampleFile = value;
                }
            }
        }

        public string DiscountPrice
        {
            get
            {
                return this._discountPrice;
            }
            set
            {
                if ((this._discountPrice != value))
                {
                    this._discountPrice = value;
                }
            }
        }
        public string ItemVideoIDs
        {

            get
            {
                return this._itemVideoIDs;
            }
            set
            {
                if ((this._itemVideoIDs != value))
                {
                    this._itemVideoIDs = value;
                }
            }

        }
        public System.Nullable<int> BrandID
        {
            get
            {
                return this._brandID;

            }
            set
            {
                if (this._brandID != value)
                {
                    _brandID = value;
                }

            }


        }
        public string BrandName
        {
            get
            {
                return _brandName;
            }
            set
            {
                if (this._brandName != value)
                {
                    _brandName = value;
                }
            }
        }
        public string BrandDescription
        {
            get
            {
                return _brandDescription;
            }
            set
            {
                if (this._brandDescription != value)
                {
                    _brandDescription = value;
                }
            }

        }

        public string BrandImageUrl
        {


            get
            {
                return _brandImageUrl;
            }
            set
            {
                if (this._brandImageUrl != value)
                {
                    _brandImageUrl = value;
                }
            }
        }
        public int? ItemViewCount
        {
            get
            {
                return this._itemViewCount;
            }
            set
            {
                if ((this._itemViewCount != value))
                {
                    this._itemViewCount = value;
                }
            }
        }

        public string Status
        {
            get { return this._status; }
            set
            {
                if ((this._status != value))
                {
                    this._status = value;
                }
            }
        }

        public string ItemTagIDs
        {
            get
            {
                return this._itemTagIDs;
            }
            set
            {
                if ((this._itemTagIDs != value))
                {
                    this._itemTagIDs = value;
                }
            }
        }
        public int StatusID
        {
            get
            {
                return this._statusID;
            }
            set
            {
                if ((this._statusID != value))
                {
                    this._statusID = value;
                }
            }
        }
		
		 public string ItemCategories
        {
            get
            {
                return _itemCategories;
            }
            set
            {
                if (this._itemCategories != value)
                {
                    _itemCategories = value;
                }
            }
        }

        #endregion
    }
}
