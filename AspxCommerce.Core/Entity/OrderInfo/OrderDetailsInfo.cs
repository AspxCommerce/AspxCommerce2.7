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

namespace AspxCommerce.Core
{
    public class OrderDetailsInfo
    {
        #region Private Members

        private int _orderID;
        private bool _isShippingAddressRequired;
        private string _invoiceNumber;
        private string _orderStatus;
        private string _sessionCode;
        private string _transactionID;
        private decimal _grandTotal;
        private decimal _discountAmount;
        private decimal _couponDiscountAmount;
        private string _couponCode;       
        private decimal _rewardDiscountAmount;    
        private decimal _usedRewardPoints;
        private string _purchaseOrderNumber;
        private int _paymentGatewayTypeID;
        private int _paymentGatewaySubTypeID;
        private string _clientIPAddress;
        private int _userBillingAddressID;
        private int _shippingMethodID;
        private int _paymentMethodID;
        private decimal _taxTotal;
        private string _currencyCode;
        private DateTime _date;
        private int _customerID;
        private bool _isGuestUser;
        private int _responseCode;
        private int _responseReasonCode;
        private string _responseReasonText;
        private string _remarks;
        private bool _isMultipleCheckOut;
        private int _orderStatusID;

        private string _version;
        private string _delimData;
        private string _apiLogin;
        private string _transactionKey;
        private string _relayResponse;
        private string _delimChar;
        private string _encapeChar;
        private string _isTest;
        private string _isEmailCustomer;
        private bool _isDownloadable;

        #endregion

        #region public Members
      
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
        public bool IsDownloadable
        {
            get
            {
                return this._isDownloadable;
            }
            set
            {
                if ((this._isDownloadable != value))
                {
                    this._isDownloadable = value;
                }
            }
        }

        public bool IsShippingAddressRequired
        {
            get
            {
                return this._isShippingAddressRequired;
            }
            set
            {
                if ((this._isShippingAddressRequired != value))
                {
                    this._isShippingAddressRequired = value;
                }
            }


        }

        public int OrderStatusID
        {
            get
            {
                return this._orderStatusID;
            }
            set
            {
                if ((this._orderStatusID != value))
                {
                    this._orderStatusID = value;
                }
            }
        }


        public string SessionCode
        {
            get
            {
                return this._sessionCode;
            }
            set
            {
                if ((this._sessionCode != value))
                {
                    this._sessionCode = value;
                }
            }
        }

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


        public string TransactionID
        {
            get
            {
                return this._transactionID;
            }
            set
            {
                if ((this._transactionID != value))
                {
                    this._transactionID = value;
                }
            }
        }

        public decimal GrandTotal
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

        public decimal DiscountAmount
        {
            get
            {
                return this._discountAmount;
            }
            set
            {
                if ((this._discountAmount != value))
                {
                    this._discountAmount = value;
                }
            }
        }

        public decimal CouponDiscountAmount
        {
            get
            {
                return this._couponDiscountAmount;
            }
            set
            {
                if ((this._couponDiscountAmount != value))
                {
                    this._couponDiscountAmount = value;
                }
            }
        }

        public string CouponCode
        {
            get
            {
                return this._couponCode;
            }
            set
            {
                if ((this._couponCode != value))
                {
                    this._couponCode = value;
                }
            }
        }
    
        public decimal RewardDiscountAmount
        {
            get
            {
                return this._rewardDiscountAmount;
            }
            set
            {
                if ((this._rewardDiscountAmount != value))
                {
                    this._rewardDiscountAmount = value;
                }
            }
        }
        
        public decimal UsedRewardPoints
        {
            get
            {
                return this._usedRewardPoints;
            }
            set
            {
                if ((this._usedRewardPoints != value))
                {
                    this._usedRewardPoints = value;
                }
            }
        }

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

        public int PaymentGatewaySubTypeID
        {
            get
            {
                return this._paymentGatewaySubTypeID;
            }
            set
            {
                if ((this._paymentGatewaySubTypeID != value))
                {
                    this._paymentGatewaySubTypeID = value;
                }
            }
        }

        public string ClientIPAddress
        {
            get
            {
                return this._clientIPAddress;
            }
            set
            {
                if ((this._clientIPAddress != value))
                {
                    this._clientIPAddress = value;
                }
            }
        }

        public int UserBillingAddressID
        {
            get
            {
                return this._userBillingAddressID;
            }
            set
            {
                if ((this._userBillingAddressID != value))
                {
                    this._userBillingAddressID = value;
                }
            }
        }

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

        public int PaymentMethodID
        {
            get
            {
                return this._paymentMethodID;
            }
            set
            {
                if ((this._paymentMethodID != value))
                {
                    this._paymentMethodID = value;
                }
            }
        }

        public decimal TaxTotal
        {
            get
            {
                return this._taxTotal;
            }
            set
            {
                if ((this._taxTotal != value))
                {
                    this._taxTotal = value;
                }
            }
        }

        public string CurrencyCode
        {
            get
            {
                return this._currencyCode;
            }
            set
            {
                if ((this._currencyCode != value))
                {
                    this._currencyCode = value;
                }
            }
        }

        public DateTime Date
        {
            get
            {
                return this._date;
            }
            set
            {
                if ((this._date != value))
                {
                    this._date = value;
                }
            }
        }

        public int CustomerID
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
            
        public int ResponseCode
        {
            get
            {
                return this._responseCode;
            }
            set
            {
                if ((this._responseCode != value))
                {
                    this._responseCode = value;
                }
            }
        }

        public int ResponseReasonCode
        {
            get
            {
                return this._responseReasonCode;
            }
            set
            {
                if ((this._responseReasonCode != value))
                {
                    this._responseReasonCode = value;
                }
            }
        }

        public string ResponseReasonText
        {
            get
            {
                return this._responseReasonText;
            }
            set
            {
                if ((this._responseReasonText != value))
                {
                    this._responseReasonText = value;
                }
            }
        }

        public string Remarks
        {
            get
            {
                return this._remarks;
            }
            set
            {
                if ((this._remarks != value))
                {
                    this._remarks = value;
                }
            }
        }

        public bool IsGuestUser
        {
            get
            {
                return this._isGuestUser;
            }
            set
            {
                if ((this._isGuestUser != value))
                {
                    this._isGuestUser = value;
                }
            }
        }
        public bool IsMultipleCheckOut
        {
            get
            {
                return this._isMultipleCheckOut;
            }
            set
            {
                if ((this._isMultipleCheckOut != value))
                {
                    this._isMultipleCheckOut = value;
                }
            }
        }
        
        public string Version
        {
            get
            {
                return this._version;
            }
            set
            {
                if ((this._version != value))
                {
                    this._version = value;
                }
            }
        }

        public string DelimData
        {
            get
            {
                return this._delimData;
            }
            set
            {
                if ((this._delimData != value))
                {
                    this._delimData = value;
                }
            }
        }

        public string APILogin
        {
            get
            {
                return this._apiLogin;
            }
            set
            {
                if ((this._apiLogin != value))
                {
                    this._apiLogin = value;
                }
            }
        }

        public string TransactionKey
        {
            get
            {
                return this._transactionKey;
            }
            set
            {
                if ((this._transactionKey != value))
                {
                    this._transactionKey = value;
                }
            }
        }

        public string RelayResponse
        {
            get
            {
                return this._relayResponse;
            }
            set
            {
                if ((this._relayResponse != value))
                {
                    this._relayResponse = value;
                }
            }
        }

        public string DelimChar
        {
            get
            {
                return this._delimChar;
            }
            set
            {
                if ((this._delimChar != value))
                {
                    this._delimChar = value;
                }
            }
        }

        public string EncapeChar
        {
            get
            {
                return this._encapeChar;
            }
            set
            {
                if ((this._encapeChar != value))
                {
                    this._encapeChar = value;
                }
            }
        }

        public string IsTest
        {
            get
            {
                return this._isTest;
            }
            set
            {
                if ((this._isTest != value))
                {
                    this._isTest = value;
                }
            }
        }

        public string IsEmailCustomer
        {
            get
            {
                return this._isEmailCustomer;
            }
            set
            {
                if ((this._isEmailCustomer != value))
                {
                    this._isEmailCustomer = value;
                }
            }
        }

        #endregion
    }

}
