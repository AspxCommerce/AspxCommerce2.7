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
    public class ReturnItemsInfo
    {
        private int _returnID;
        private string _returnFileDate;
        private int _orderID;
        private string _orderedDate;
        private string _dateModified;
        private string _returnStatus;
        private string _returnAction;
        private string _retrunProductStatus;
        private string _returnReason;
        private string _otherFaults;
        private string _itemName;
        private string _SKU;
        private int _quantity;
        private string _costVariants;
        private string _shippingAddress;
        private string _returnAddress;
        private string _shippingMethodName;
        private string _shippingProviderName;


        public ReturnItemsInfo()
        {
        }

        [DataMember]
        public int ReturnID
        {
            get
            {
                return this._returnID;
            }
            set
            {
                if ((this._returnID != value))
                {
                    this._returnID = value;
                }
            }
        }

        [DataMember]
        public string ReturnFileDate
        {
            get
            {
                return this._returnFileDate;
            }
            set
            {
                if ((this._returnFileDate != value))
                {
                    this._returnFileDate = value;
                }
            }
        }
        [DataMember]
        public string DateModified
        {
            get
            {
                return this._dateModified;
            }
            set
            {
                if ((this._dateModified != value))
                {
                    this._dateModified = value;
                }
            }
        }
        [DataMember]
        public int OrderID
        {
            get
            {
                return this._orderID;
            }
            set
            {
                if ((this._orderID != value))
                {
                    this._orderID = value;
                }
            }
        }

        [DataMember]
        public string OrderedDate
        {
            get
            {
                return this._orderedDate;
            }
            set
            {
                if ((this._orderedDate != value))
                {
                    this._orderedDate = value;
                }
            }
        }
        [DataMember]
        public string ReturnStatus
        {
            get
            {
                return this._returnStatus;
            }
            set
            {
                if ((this._returnStatus != value))
                {
                    this._returnStatus = value;
                }
            }
        }
        [DataMember]
        public string ReturnAction
        {
            get
            {
                return this._returnAction;
            }
            set
            {
                if ((this._returnAction != value))
                {
                    this._returnAction = value;
                }
            }
        }
        [DataMember]
        public string RetrunProductStatus
        {
            get
            {
                return this._retrunProductStatus;
            }
            set
            {
                if ((this._retrunProductStatus != value))
                {
                    this._retrunProductStatus = value;
                }
            }
        }

        [DataMember]
        public string ReturnReason
        {
            get
            {
                return this._returnReason;
            }
            set
            {
                if ((this._returnReason != value))
                {
                    this._returnReason = value;
                }
            }
        }
        [DataMember]
        public string OtherFaults
        {
            get
            {
                return this._otherFaults;
            }
            set
            {
                if ((this._otherFaults != value))
                {
                    this._otherFaults = value;
                }
            }
        }

        [DataMember]
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
        [DataMember]
        public string SKU
        {
            get
            {
                return this._SKU;
            }
            set
            {
                if ((this._SKU != value))
                {
                    this._SKU = value;
                }
            }
        }
        [DataMember]
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
        [DataMember]
        public string CostVariants
        {
            get
            {
                return this._costVariants;
            }
            set
            {
                if ((this._costVariants != value))
                {
                    this._costVariants = value;
                }
            }
        }
        [DataMember]
        public string ShippingAddress
        {
            get
            {
                return this._shippingAddress;
            }
            set
            {
                if ((this._shippingAddress != value))
                {
                    this._shippingAddress = value;
                }
            }
        }
        [DataMember]
        public string ReturnAddress
        {
            get
            {
                return this._returnAddress;
            }
            set
            {
                if ((this._returnAddress != value))
                {
                    this._returnAddress = value;
                }
            }
        }
        [DataMember]
        public string ShippingMethodName
        {
            get
            {
                return this._shippingMethodName;
            }
            set
            {
                if ((this._shippingMethodName != value))
                {
                    this._shippingMethodName = value;
                }
            }
        }
        [DataMember]
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






    }
    public class ReturnSaveUpdateInfo
    {
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public string CostVariantIDs { get; set; }
        public string ItemName { get; set; }
        public string CostVariants { get; set; }
        public int Quantity { get; set; }
        public int ProductStatusID { get; set; }
        public int ReturnReasonID { get; set; }
        public string OtherDetails { get; set; }
        public int ReturnShippingAddressID { get; set; }



    }

}
