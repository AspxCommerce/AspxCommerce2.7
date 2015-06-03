using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class BrandItemsInfo
    {
        [DataMember]
        private int _brandID;

        [DataMember]
        private int _itemID;

        [DataMember]
        private string _brandName;

        [DataMember]
        private int _itemCount;

        public int BrandID
        {
            get
            {
                return this._brandID;
            }
            set
            {
                if ((this._brandID != value))
                {
                    this._brandID = value;
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
        public string BrandName
        {
            get
            {
                return this._brandName;
            }
            set
            {
                if ((this._brandName != value))
                {
                    this._brandName = value;
                }
            }
        }
        public int ItemCount
        {
            get
            {
                return this._itemCount;
            }
            set
            {
                if ((this._itemCount != value))
                {
                    this._itemCount = value;
                }
            }
        }
    }
}
