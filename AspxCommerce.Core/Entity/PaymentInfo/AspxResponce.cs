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
    public class AspxResponce
    {
        #region Private Fields

        private string _invoiceNumber;
        private string _customerID;
        private string _purchaseOrderNumber;
        private string _sequence;
        private int _stamp;
        private string _fingerPrint;

        private string _responceCode;
        private string _responceReasonCode;
        private string _transactionCode;
        private string _authorizationCode;
        private string _message;
        private Array _arrResponse; 
        #endregion

        #region Public Fields
        [DataMember]
        public string InvoiceNumber
        {
            get
            {
                return this._invoiceNumber;
            }
            set
            {
                if ((this._invoiceNumber != value))
                {
                    this._invoiceNumber = value;
                }
            }
        }
        [DataMember]
        public string CustomerID
        {
            get
            {
                return this._customerID;
            }
            set
            {
                if ((this._customerID != value))
                {
                    this._customerID = value;
                }
            }
        }

        [DataMember]
        public string PurchaseOrderNumber
        {
            get
            {
                return this._purchaseOrderNumber;
            }
            set
            {
                if ((this._purchaseOrderNumber != value))
                {
                    this._purchaseOrderNumber = value;
                }
            }
        }

        [DataMember]
        public string Sequence
        {
            get
            {
                return this._sequence;
            }
            set
            {
                if ((this._sequence != value))
                {
                    this._sequence = value;
                }
            }
        }
        [DataMember]
        public int Stamp
        {
            get
            {
                return this._stamp;
            }
            set
            {
                if ((this._stamp != value))
                {
                    this._stamp = value;
                }
            }
        }

        [DataMember]
        public string FingerPrint
        {
            get
            {
                return this._fingerPrint;
            }
            set
            {
                if ((this._fingerPrint != value))
                {
                    this._fingerPrint = value;
                }
            }
        }


        [DataMember]
        public string ResponceCode
        {
            get
            {
                return this._responceCode;
            }
            set
            {
                if ((this._responceCode != value))
                {
                    this._responceCode = value;
                }
            }
        }

        [DataMember]
        public string ResponceReasonCode
        {
            get
            {
                return this._responceReasonCode;
            }
            set
            {
                if ((this._responceReasonCode != value))
                {
                    this._responceReasonCode = value;
                }
            }
        }

        [DataMember]
        public string TransactionCode
        {
            get
            {
                return this._transactionCode;
            }
            set
            {
                if ((this._transactionCode != value))
                {
                    this._transactionCode = value;
                }
            }
        }

        [DataMember]
        public string AuthorizationCode
        {
            get
            {
                return this._authorizationCode;
            }
            set
            {
                if ((this._authorizationCode != value))
                {
                    this._authorizationCode = value;
                }
            }
        }

        [DataMember]
        public string Message
        {
            get
            {
                return this._message;
            }
            set
            {
                if ((this._message != value))
                {
                    this._message = value;
                }
            }
        }

        [DataMember]
        public Array arrResponse
        {
            get
            {
                return this._arrResponse;
            }
            set
            {
                if ((this._arrResponse != value))
                {
                    this._arrResponse = value;
                }
            }
        }


        #endregion
    }
}
