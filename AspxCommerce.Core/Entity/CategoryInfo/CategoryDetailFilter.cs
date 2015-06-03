using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.Core
{
    public class CategoryDetailFilter
    {
        [DataMember(Name = "_categoryID", Order = 0)] 
        private int _categoryID;

        [DataMember(Name = "_categoryName", Order = 1)]
        private string _categoryName;

        [DataMember(Name = "_categoryImage", Order = 2)]
        private string _categoryImage;

        [DataMember(Name = "_itemCount", Order = 3)]
        private int _itemCount;

        [DataMember(Name = "_categoryShortDesc", Order = 4)]
        private string _categoryShortDesc;



        public int CategoryID
        {
            get
            {
                return this._categoryID;

            }
            set
            {
                if (this._categoryID != value)
                {
                    _categoryID = value;
                }
            }

        }
        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                if (this._categoryName != value)
                {
                    _categoryName = value;
                }
            }

        }

        public string CategoryImage
        {
            get
            {
                return _categoryImage;
            }
            set
            {
                if (this._categoryImage != value)
                {
                    _categoryImage = value;
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
                if (this._itemCount != value)
                {
                    _itemCount = value;
                }
            }

        }
        public string CategoryShortDesc
        {
            get
            {
                return _categoryShortDesc;
            }
            set
            {
                if (this._categoryShortDesc != value)
                {
                    _categoryShortDesc = value;
                }
            }

        }

    }
}
