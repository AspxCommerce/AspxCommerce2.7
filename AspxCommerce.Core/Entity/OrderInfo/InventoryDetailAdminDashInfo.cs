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
    public class InventoryDetailAdminDashInfo
    {
        public InventoryDetailAdminDashInfo()
        {
        }

        private System.Nullable<int> _totalItem;

        private System.Nullable<int> _active;

        private System.Nullable<int> _hidden;

        private System.Nullable<int> _dItemscountNo;

        private string _downlodableIDs;

        private System.Nullable<int> _sItemsCountNo;

        private string _specialItemsIDs;

        private System.Nullable<int> _lowStockItemCount;

        private System.Nullable<int> _groupItemCount;

        private System.Nullable<int> _kitItemCount;


        [DataMember]
        public System.Nullable<int> TotalItem
        {
            get
            {
                return this._totalItem;
            }
            set
            {
                if ((this._totalItem != value))
                {
                    this._totalItem = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> Active
        {
            get
            {
                return this._active;
            }
            set
            {
                if ((this._active != value))
                {
                    this._active = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> Hidden
        {
            get
            {
                return this._hidden;
            }
            set
            {
                if ((this._hidden != value))
                {
                    this._hidden = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> DItemscountNo
        {
            get
            {
                return this._dItemscountNo;
            }
            set
            {
                if ((this._dItemscountNo != value))
                {
                    this._dItemscountNo = value;
                }
            }
        }

        [DataMember]
        public string DownlodableIDs
        {
            get
            {
                return this._downlodableIDs;
            }
            set
            {
                if ((this._downlodableIDs != value))
                {
                    this._downlodableIDs = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> SItemsCountNo
        {
            get
            {
                return this._sItemsCountNo;
            }
            set
            {
                if ((this._sItemsCountNo != value))
                {
                    this._sItemsCountNo = value;
                }
            }
        }

        [DataMember]
        public string SpecialItemsIDs
        {
            get
            {
                return this._specialItemsIDs;
            }
            set
            {
                if ((this._specialItemsIDs != value))
                {
                    this._specialItemsIDs = value;
                }
            }
        }
        
        [DataMember]        
        public System.Nullable<int> LowStockItemCount
        {
            get
            {
                return this._lowStockItemCount;
            }
            set
            {
                if ((this._lowStockItemCount != value))
                {
                    this._lowStockItemCount = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> GroupItemCount
        {
            get
            {
                return this._groupItemCount;
            }
            set
            {
                if ((this._groupItemCount != value))
                {
                    this._groupItemCount = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<int> KitItemCount
        {
            get
            {
                return this._kitItemCount;
            }
            set
            {
                if ((this._kitItemCount != value))
                {
                    this._kitItemCount = value;
                }
            }
        }

    }
}
