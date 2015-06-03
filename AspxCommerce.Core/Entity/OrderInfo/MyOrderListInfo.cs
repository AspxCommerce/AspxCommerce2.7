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
    public class MyOrderListInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_orderID", Order = 1)]
        private System.Nullable<int> _orderID;

        [DataMember(Name = "_inVoiceNumber", Order = 2)]
        private string _inVoiceNumber;

        [DataMember(Name = "_customerID", Order = 3)]
        private System.Nullable<int> _customerID;

        [DataMember(Name = "_userName", Order = 4)]
        private string _userName;        

        [DataMember(Name = "_email", Order = 5)]
        private string _email; 
        [DataMember(Name = "_orderStatus", Order = 6)]
        private string _orderStatus;

        [DataMember(Name = "_grandTotal", Order = 7)]
        private System.Nullable<decimal> _grandTotal;

        [DataMember(Name = "_paymentGatewayTypeName", Order = 8)]
        private string _paymentGatewayTypeName;

        [DataMember(Name = "_paymentMethodName", Order = 9)]
        private string _paymentMethodName;
		
		[DataMember(Name = "_orderedDate", Order = 10)]
        private string _orderedDate;

        public MyOrderListInfo()
        {
        }

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
        public System.Nullable<int> OrderID
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


        public string InVoiceNumber
        {
            get
            {
                return this._inVoiceNumber;
            }
            set
            {
                if ((this._inVoiceNumber != value))
                {
                    this._inVoiceNumber = value;
                }
            }
        }

         public System.Nullable<int> CustomerID
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

         public string UserName
         {
             get
             {
                 return this._userName;
             }
             set
             {
                 if ((this._userName != value))
                 {
                     this._userName = value;
                 }
             }
         }

         public string Email
         {
             get
             {
                 return this._email;
             }
             set
             {
                 if ((this._email != value))
                 {
                     this._email = value;
                 }
             }
         }

        public string OrderStatus
        {
            get
            {
                return this._orderStatus;
            }
            set
            {
                if ((this._orderStatus != value))
                {
                    this._orderStatus = value;
                }
            }
        }


        public System.Nullable<decimal> GrandTotal
        {
            get
            {
                return this._grandTotal;
            }
            set
            {
                if ((this._grandTotal != value))
                {
                    this._grandTotal = value;
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


        public string PaymentMethodName
        {
            get
            {
                return this._paymentMethodName;
            }
            set
            {
                if ((this._paymentMethodName != value))
                {
                    this._paymentMethodName = value;
                }
            }
        }
		
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
    }
}
