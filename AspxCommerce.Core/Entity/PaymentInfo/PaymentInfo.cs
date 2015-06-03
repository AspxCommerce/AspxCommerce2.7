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




namespace AspxCommerce.Core
{
    public class PaymentInfo
    {
         #region private members

        private string _paymentMethodName;
        private string _paymentMethodCode;
        private string _cardNumber;
        private string _transactionType;
        private string _cardType;
        private string _cardCode;
        private string _cardHolder;
        private string _expiryMonth;
        private string _expiryYear;
        private string _expireDate;
        private string _accountNumber;
        private string _routingNumber;
        private string _accountType;
        private string _bankName;
        private string _accountHolderName;
        private string _chequeType;
        private string _chequeNumber;
        private bool _recurringBillingStatus;

        #endregion

        #region PUBLIC MEMBERS
       
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
        
        public string PaymentMethodCode
        {
            get
            {
                return this._paymentMethodCode;
            }
            set
            {
                if ((this._paymentMethodCode != value))
                {
                    this._paymentMethodCode = value;
                }
            }
        }
        
        public string CardNumber
        {
            get
            {
                return this._cardNumber;
            }
            set
            {
                if ((this._cardNumber != value))
                {
                    this._cardNumber = value;
                }
            }
        }
        
        public string TransactionType
        {
            get
            {
                return this._transactionType;
            }
            set
            {
                if ((this._transactionType != value))
                {
                    this._transactionType = value;
                }
            }
        }
       
        public string CardType
        {
            get
            {
                return this._cardType;
            }
            set
            {
                if ((this._cardType != value))
                {
                    this._cardType = value;
                }
            }
        }
        public string CardHolder
        {
            get
            {
                return this._cardHolder;
            }
            set
            {
                if ((this._cardHolder != value))
                {
                    this._cardHolder = value;
                }
            }
        }

        public string CardCode
        {
            get
            {
                return this._cardCode;
            }
            set
            {
                if ((this._cardCode != value))
                {
                    this._cardCode = value;
                }
            }
        }
        public string ExpireYear
        {
            get
            {
                return this._expiryYear;
            }
            set
            {
                if ((this._expiryYear != value))
                {
                    this._expiryYear = value;
                }
            }
        }

        public string ExpireMonth
        {
            get
            {
                return this._expiryMonth;
            }
            set
            {
                if ((this._expiryMonth != value))
                {
                    this._expiryMonth = value;
                }
            }
        }

        
        public string ExpireDate
        {
            get
            {
                return this._expireDate;
            }
            set
            {
                if ((this._expireDate != value))
                {
                    this._expireDate = value;
                }
            }
        }
       
        public string AccountNumber
        {
            get
            {
                return this._accountNumber;
            }
            set
            {
                if ((this._accountNumber != value))
                {
                    this._accountNumber = value;
                }
            }
        }
       
        public string RoutingNumber
        {
            get
            {
                return this._routingNumber;
            }
            set
            {
                if ((this._routingNumber != value))
                {
                    this._routingNumber = value;
                }
            }
        }
       
        public string AccountType
        {
            get
            {
                return this._accountType;
            }
            set
            {
                if ((this._accountType != value))
                {
                    this._accountType = value;
                }
            }
        }
        
        public string BankName
        {
            get
            {
                return this._bankName;
            }
            set
            {
                if ((this._bankName != value))
                {
                    this._bankName = value;
                }
            }
        }

        public string AccountHolderName
        {
            get
            {
                return this._accountHolderName;
            }
            set
            {
                if ((this._accountHolderName != value))
                {
                    this._accountHolderName = value;
                }
            }
        }

        public string ChequeType
        {
            get
            {
                return this._chequeType;
            }
            set
            {
                if ((this._chequeType != value))
                {
                    this._chequeType = value;
                }
            }
        }

        public string ChequeNumber
        {
            get
            {
                return this._chequeNumber;
            }
            set
            {
                if ((this._chequeNumber != value))
                {
                    this._chequeNumber = value;
                }
            }
        }

        public bool RecurringBillingStatus
        {
            get
            {
                return this._recurringBillingStatus;
            }
            set
            {
                if ((this._recurringBillingStatus != value))
                {
                    this._recurringBillingStatus = value;
                }
            }
        }

        public PaymentInfo()
        {
        }

        #endregion
    }
}
