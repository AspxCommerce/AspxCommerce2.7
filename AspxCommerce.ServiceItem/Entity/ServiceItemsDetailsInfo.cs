using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.ServiceItem
{

    [Serializable]
    [DataContract]
    public class ServiceItemsDetailsInfo
    {
        [DataMember]
        private int _itemID;
        [DataMember]
        private string _itemName;
        [DataMember]
        private decimal _price;
        [DataMember]
        private string _shortdescription;
        [DataMember]
        private string _description;
        [DataMember]
        private string _imagePath;
        [DataMember]
        private int _categoryID;
        [DataMember]
        private int _serviceDuration;
       

        public int ItemID
        {
            get { return this._itemID; }
            set
            {
                if (this._itemID != value)
                {
                    this._itemID = value;
                }
            }
        }

        public string ItemName
        {
            get { return this._itemName; }
            set
            {
                if (this._itemName != value)
                {
                    this._itemName = value;
                }
            }
        }

        public decimal Price
        {
            get { return this._price; }
            set
            {
                if (this._price != value)
                {
                    this._price = value;
                }
            }
        }

        public string ShortDescription
        {
            get { return this._shortdescription; }
            set
            {
                if (this._shortdescription != value)
                {
                    this._shortdescription = value;
                }
            }
        }  

        public string Description
        {
            get { return this._description; }
            set
            {
                if (this._description != value)
                {
                    this._description = value;
                }
            }
        }

        public string ImagePath
        {
            get { return this._imagePath; }
            set
            {
                if (this._imagePath != value)
                {
                    this._imagePath = value;
                }
            }
        }

        public int CategoryID
        {
            get { return this._categoryID; }
            set
            {
                if (this._categoryID != value)
                {
                    this._categoryID = value;
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
    }
}
