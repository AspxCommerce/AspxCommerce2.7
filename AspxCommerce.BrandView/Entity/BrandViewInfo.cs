using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AspxCommerce.BrandView
{
    public class BrandViewInfo
    {
        public BrandViewInfo()
        { }
        [DataMember]
        private int _RowTotal;

        [DataMember]
        private int _brandID;
        [DataMember]
        private string _brandName;
        [DataMember]
        private string _brandDescription;
        [DataMember]
        private string _brandImageUrl;
        [DataMember]
        private bool _isShowInSlider;
        [DataMember]
        private bool _isFeatured;
        [DataMember]
        private System.Nullable<System.DateTime> _featuredFrom;
        [DataMember]
        private System.Nullable<System.DateTime> _featuredTo;
        [DataMember]
        private bool _isActive;

        public int RowTotal
        {
            get
            {
                return this._RowTotal;
            }
            set
            {
                if ((this._RowTotal != value))
                {
                    this._RowTotal = value;
                }
            }
        }


        public int BrandID
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
        public bool IsShowInSlider
        {
            get
            {
                return this._isShowInSlider;
            }
            set
            {
                if ((this._isShowInSlider != value))
                {
                    this._isShowInSlider = value;
                }
            }
        }
        public bool IsFeatured
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
        public System.Nullable<System.DateTime> FeaturedFrom
        {
            get
            {
                return this._featuredFrom;
            }
            set
            {
                if ((this._featuredFrom != value))
                {
                    this._featuredFrom = value;
                }
            }
        }
        public System.Nullable<System.DateTime> FeaturedTo
        {
            get
            {
                return this._featuredTo;
            }
            set
            {
                if ((this._featuredTo != value))
                {
                    this._featuredTo = value;
                }
            }
        }

        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if ((this._isActive != value))
                {
                    this._isActive = value;
                }
            }
        }
    }
}
