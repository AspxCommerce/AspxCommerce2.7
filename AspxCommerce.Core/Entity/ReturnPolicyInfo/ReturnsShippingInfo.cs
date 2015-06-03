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
    public class ReturnsShippingInfo
    {
        private int _shippingMethodID;
        private string _shippingMethodName;
        private string _deliveryTime;
        private string _alternateText;
        private string _shippingCost;

        [DataMember]
        public int ShippingMethodID
        {
            get
            {
                return this._shippingMethodID;
            }
            set
            {
                if ((this._shippingMethodID != value))
                {
                    this._shippingMethodID = value;
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
        public string DeliveryTime
        {
            get
            {
                return this._deliveryTime;
            }
            set
            {
                if ((this._deliveryTime != value))
                {
                    this._deliveryTime = value;
                }
            }
        }
        [DataMember]
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
        [DataMember]
        public string ShippingCost
        {
            get
            {
                return this._shippingCost;
            }
            set
            {
                if ((this._shippingCost != value))
                {
                    this._shippingCost = value;
                }
            }
        }



    }
}
