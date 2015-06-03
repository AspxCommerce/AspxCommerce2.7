using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.MegaCategory
{
    [DataContract]
    [Serializable]
    public class MegaCategoryViewInfo
    {
        private Int32 _categoryID;
        private string _categoryName;
        private string _levelCategoryName;
        private Int32 _parentID;
        private System.Nullable<Int32> _categoryLevel;
        private string _path;
        private string _attributeValue;
        private string _catImagePath;
        private int _productCount;
        private Int32 _childCount;

        [DataMember]
        public Int32 CategoryID
        {
            get
            {
                return this._categoryID;
            }
            set
            {
                this._categoryID = value;
            }
        }

        [DataMember]
        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                _categoryName = value;
            }
        }

        [DataMember]
        public string LevelCategoryName
        {
            get
            {
                return _levelCategoryName;
            }
            set
            {
                _levelCategoryName = value;
            }
        }


        [DataMember]
        public Int32 ParentID
        {
            get
            {
                return this._parentID;
            }
            set
            {
                this._parentID = value;
            }
        }
        [DataMember]
        public System.Nullable<Int32> CategoryLevel
        {
            get
            {
                return this._categoryLevel;
            }
            set
            {
                this._categoryLevel = value;
            }
        }

        [DataMember]
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        [DataMember]
        public string CategoryImagePath
        {
            get
            {
                return this._catImagePath;
            }
            set
            {
                this._catImagePath = value;
            }
        }

        [DataMember]
        public string AttributeValue
        {
            get
            {
                return this._attributeValue;
            }
            set
            {
                this._attributeValue = value;
            }
        }
        [DataMember]
        public int ProductCount
        {
            get
            {
                return this._productCount;
            }
            set
            {
                this._productCount = value;
            }
        }


        [DataMember]
        public Int32 ChildCount
        {
            get
            {
                return this._childCount;
            }
            set
            {
                this._childCount = value;
            }
        }
    }
}
