using System;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class TransactionLogInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;
        [DataMember(Name = "_transactionLogID", Order = 1)]
        private int _transactionLogID;

        [DataMember(Name = "_paymentGatewayTypeName", Order = 2)]
        private string _paymentGatewayTypeName;

        [DataMember(Name = "_transactionID", Order = 3)]
        private string _transactionID;

        [DataMember(Name = "_orderID", Order = 4)]
        private int _orderID;

        [DataMember(Name = "_paymentStatus", Order =5)]
        private string _paymentStatus = "";

        [DataMember(Name = "_totalAmount", Order = 6)]
        private decimal _totalAmount;

        [DataMember(Name = "_payerEmail", Order = 7)]
        private string _payerEmail = "";

        [DataMember(Name = "_creditCard", Order = 8)]
        private string _creditCard = "";

        [DataMember(Name = "_responseCode", Order = 9)]
        private string _responseCode = "";
    

        [DataMember(Name = "_addedBy", Order = 10)]
        private string _addedBy;

        [DataMember(Name = "_customerID", Order = 11)]
        private int _customerID;

        [DataMember(Name = "_paymentGatewayID", Order = 12)]
        private int _paymentGatewayID;

        [DataMember(Name = "_sessionCode", Order = 13)]
        private string _sessionCode = "";

        [DataMember(Name = "_responseReasonText", Order = 14)]
        private string _responseReasonText = "";

        [DataMember(Name = "_authCode", Order = 15)]
        private string _authCode = "";
        private string _recieverEmail = "";
        private int _storeID;
        private int _portalID;

        private string _currencyCode = "";

        private System.Nullable<bool> _isActive;

        private System.Nullable<bool> _isDeleted;

        private System.Nullable<bool> _isModified;

        private System.Nullable<System.DateTime> _addedOn;

        private System.Nullable<System.DateTime> _updatedOn;

        private System.Nullable<System.DateTime> _deletedOn;


        private string _updatedBy;

        private string _deletedBy;

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
        public int TransactionLogID
        {
            get
            {
                return this._transactionLogID;
            }
            set
            {
                this._transactionLogID = value;

            }
        }
        public int PaymentGatewayID
        {
            get
            {
                return this._paymentGatewayID;
            }
            set
            {
                this._paymentGatewayID = value;

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
                this._paymentGatewayTypeName = value;

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
                this._transactionID = value;

            }
        }

        public string PayerEmail
        {
            get
            {
                return this._payerEmail;
            }
            set
            {
                this._payerEmail = value;

            }
        }

        public decimal TotalAmount
        {
            get
            {
                return this._totalAmount;
            }
            set
            {
                this._totalAmount = value;

            }
        }

        public string RecieverEmail
        {
            get
            {
                return this._recieverEmail;
            }
            set
            {
                this._recieverEmail = value;

            }
        }

        public string PaymentStatus
        {
            get
            {
                return this._paymentStatus;
            }
            set
            {
                this._paymentStatus = value;

            }
        }

        public int OrderID
        {
            get
            {
                return this._orderID;
            }
            set
            {
                this._orderID = value;

            }
        }
        public string CreditCard
        {
            get
            {
                return this._creditCard;
            }
            set
            {
                this._creditCard = value;

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
                this._customerID = value;

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
                this._sessionCode = value;

            }
        }

        public string ResponseCode
        {
            get
            {
                return this._responseCode;
            }
            set
            {
                this._responseCode = value;

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
                this._responseReasonText = value;

            }
        }

        public string AuthCode
        {
            get
            {
                return this._authCode;
            }
            set
            {
                this._authCode = value;

            }
        }
        public int StoreID
        {
            get
            {
                return this._storeID;
            }
            set
            {
                this._storeID = value;

            }
        }

        public int PortalID
        {
            get
            {
                return this._portalID;
            }
            set
            {
                this._portalID = value;

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
                this._isActive = value;
            }
        }


        public System.Nullable<bool> IsDeleted
        {
            get
            {
                return this._isDeleted;
            }
            set
            {

                this._isDeleted = value;


            }
        }


        public System.Nullable<bool> IsModified
        {
            get
            {
                return this._isModified;
            }
            set
            {
                this._isModified = value;
            }
        }



        public System.Nullable<System.DateTime> AddedOn
        {
            get
            {
                return this._addedOn;
            }
            set
            {

                this._addedOn = value;

            }
        }


        public System.Nullable<System.DateTime> UpdatedOn
        {
            get
            {
                return this._updatedOn;
            }
            set
            {
                this._updatedOn = value;

            }
        }

        public System.Nullable<System.DateTime> DeletedOn
        {
            get
            {
                return this._deletedOn;
            }
            set
            {
                this._deletedOn = value;

            }
        }



        public string AddedBy
        {
            get
            {
                return this._addedBy;
            }
            set
            {
                this._addedBy = value;

            }
        }


        public string UpdatedBy
        {
            get
            {
                return this._updatedBy;
            }
            set
            {
                this._updatedBy = value;

            }
        }


        public string DeletedBy
        {
            get
            {
                return this._deletedBy;
            }
            set
            {
                this._deletedBy = value;

            }
        }

        public string CurrencyCode
        {
            get {
                return this._currencyCode;
            }
            set { this._currencyCode = value; }
        }
    }
}
