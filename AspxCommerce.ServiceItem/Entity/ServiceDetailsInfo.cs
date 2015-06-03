using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.ServiceItem
{
    [Serializable]
    [DataContract]
    public class ServiceDetailsInfo
    {
        [DataMember]
        private int _rowTotal;
        [DataMember]
        private int _categoryID;
        [DataMember]
        private int _itemID;
        [DataMember]
        private string _itemName;
        [DataMember]
        private decimal _price;
        [DataMember]
        private string _shortdescription;
        [DataMember]
        private string _itemImagePath;
        [DataMember]
        private string _categoryImagePath;
        [DataMember]
        private string _categoryDetails;
        [DataMember]
        private int _serviceDuration;
        [DataMember]
        private string _categoryName;

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

        public int CategoryID
        {
            get
            {
                return this._categoryID;
            }
            set
            {
                if ((this._categoryID != value))
                {
                    this._categoryID = value;
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


        public string ShortDescription
        {
            get
            {
                return this._shortdescription;
            }
            set
            {
                if ((this._shortdescription != value))
                {
                    this._shortdescription = value;
                }
            }
        }
        public string ItemImagePath
        {
            get
            {
                return this._itemImagePath;
            }
            set
            {
                if ((this._itemImagePath != value))
                {
                    this._itemImagePath = value;
                }
            }
        }
        public string CategoryImagePath
        {
            get
            {
                return this._categoryImagePath;
            }
            set
            {
                if ((this._categoryImagePath != value))
                {
                    this._categoryImagePath = value;
                }
            }
        }

        public string CategoryDetails
        {
            get
            {
                return this._categoryDetails;
            }
            set
            {
                if ((this._categoryDetails != value))
                {
                    this._categoryDetails = value;
                }
            }
        }

        public int ServiceDuration
        {
            get { return this._serviceDuration; }
            set
            {
                if ((this._serviceDuration != value))
                {
                    this._serviceDuration = value;
                }
            }
        }

        public string CategoryName
        {
            get
            {
                return this._categoryName;
            }
            set
            {
                if ((this._categoryName != value))
                {
                    this._categoryName = value;
                }
            }
        }
    }
}
