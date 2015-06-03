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
   public class PaymentGatewayListInfo
    {
        [DataMember]
        private int _paymentGatewayTypeID;
        [DataMember]
        private string _paymentGatewayTypeName;
        [DataMember]
        private string _controlSource;
        [DataMember]
        private string _friendlyName;
        [DataMember]
        private string _logoUrl;


        public PaymentGatewayListInfo()
        {
        }

     
        public int PaymentGatewayTypeID
        {
            get
            {
                return this._paymentGatewayTypeID;
            }
            set
            {
                if ((this._paymentGatewayTypeID != value))
                {
                    this._paymentGatewayTypeID = value;
                }
            }
        }

       
        public string PaymentGatewayTypeName
        {
            get
            {
                return this._paymentGatewayTypeName;
            }
            set
            {
                if ((this._paymentGatewayTypeName != value))
                {
                    this._paymentGatewayTypeName = value;
                }
            }
        }
        public string FriendlyName
        {
            get
            {
                return this._friendlyName;
            }
            set
            {
                if ((this._friendlyName != value))
                {
                    this._friendlyName = value;
                }
            }
        }
       
        public string ControlSource
        {
            get
            {
                return this._controlSource;
            }
            set
            {
                if ((this._controlSource != value))
                {
                    this._controlSource = value;
                }
            }
        }
        public string LogoUrl
        {
            get
            {
                return this._logoUrl;
            }
            set
            {
                if ((this._logoUrl != value))
                {
                    this._logoUrl = value;
                }
            }
        }
    }
   
}
