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
    public class ShippingProviderNameListInfo
    {
        public ShippingProviderNameListInfo()
        {
        }

        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "ShippingProviderID", Order = 1)]
        private int _shippingProviderID;

        [DataMember(Name = "ShippingProviderServiceCode", Order = 2)]
        private string _shippingProviderServiceCode;

        [DataMember(Name = "ShippingProviderName", Order = 3)]
        private string _shippingProviderName;
       

        [DataMember(Name = "ShippingProviderAliasHelp", Order = 4)]
        private string _shippingProviderAliasHelp;

        [DataMember(Name = "ShippingProviderSetting", Order = 5)]
        private string _shippingProviderSetting;
        [DataMember(Name = "HiddenSetting", Order = 6)]
        private string _hiddenSetting;

        [DataMember(Name = "HiddenProviderId", Order = 7)]
        private int _hiddenProviderId;
       
        [DataMember(Name = "IsActive", Order = 8)]
        private System.Nullable<bool> _isActive;

        public System.Nullable<int> RowTotal
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

        public int ShippingProviderID
        {
            get
            {
                return this._shippingProviderID;
            }
            set
            {
                if ((this._shippingProviderID != value))
                {
                    this._shippingProviderID = value;
                }
            }
        }

        public string ShippingProviderServiceCode
        {
            get
            {
                return this._shippingProviderServiceCode;
            }
            set
            {
                if ((this._shippingProviderServiceCode != value))
                {
                    this._shippingProviderServiceCode = value;
                }
            }
        }


        public string ShippingProviderName
        {
            get
            {
                return this._shippingProviderName;
            }
            set
            {
                if ((this._shippingProviderName != value))
                {
                    this._shippingProviderName = value;
                }
            }
        }

        public string ShippingProviderAliasHelp
        {
            get
            {
                return this._shippingProviderAliasHelp;
            }
            set
            {
                if ((this._shippingProviderAliasHelp != value))
                {
                    this._shippingProviderAliasHelp = value;
                }
            }
        }
        public string ShippingProviderSetting
        {
            get
            {
                return this._shippingProviderSetting;
            }
            set
            {
                if ((this._shippingProviderSetting != value))
                {
                    this._shippingProviderSetting = value;
                }
            }
        }
        public string HiddenSetting
        {
            get
            {
                return this._hiddenSetting;
            }
            set
            {
                if ((this._hiddenSetting != value))
                {
                    this._hiddenSetting = value;
                }
            }
        }
        public int HiddenProviderId
        {
            get { return this._hiddenProviderId; }
            set
            {
                if ((this._hiddenProviderId != value))
                {
                    this._hiddenProviderId = value;
                }
            }
        }

        public System.Nullable<bool> IsActive
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
