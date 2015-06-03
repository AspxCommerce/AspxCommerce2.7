using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core.Mobile
{
    public class OrderInfo
    {
        #region Private Members

        private int _orderId;
        private string _invoiceNumber;
        private string _transactionId;
        private decimal _grandTotal;
        private decimal _discountAmount;
        private decimal _couponDiscountAmount;
        private string _couponCode;
        private string _purchaseOrderNumber;
        private int _paymentGatewayTypeId;
        private string _clientIPAddress;
        private int _userBillingAddressId;
        private int _userShippingAddressId;
        private int _shippingMethodId;
        private int _paymentMethodId;
        private decimal _taxTotal;
        private string _currencyCode;

        private int _customerId;
        private string _sessionCode;
        private string _addedBy;
        private int _storeId;
        private int _portalId;
        private string _cultureName;

        private bool _isGuestUser;
        private decimal _shippingRate;
        private string _remarks;
        private bool _isDownloadable;
        private bool _isMultipleCheckOut;
        private string _paymentMethodName;

        private string _itemIds;
        private string _itemQuantities;
        private int _orderStatusID;
        private DateTime _addedOn;
       



        #endregion

        #region public Members

        public DateTime AddedOn
        {
            get { return this._addedOn; }
            set
            {
                if ((this._addedOn != value))
                {
                    this._addedOn = value;
                }
            }
        }

        public int OrderStatusID
        {
            get { return this._orderStatusID; }
            set
            {
                if ((this._orderStatusID != value))
                {
                    this._orderStatusID = value;
                }
            }
        }

        public int OrderId
        {
            get { return this._orderId; }
            set
            {
                if ((this._orderId != value))
                {
                    this._orderId = value;
                }
            }
        }

        public string SessionCode
        {
            get { return this._sessionCode; }
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
            get { return this._invoiceNumber; }
            set
            {
                if ((this._invoiceNumber != value))
                {
                    this._invoiceNumber = value;
                }
            }
        }

        public string TransactionId
        {
            get { return this._transactionId; }
            set
            {
                if ((this._transactionId != value))
                {
                    this._transactionId = value;
                }
            }
        }

        public decimal GrandTotal
        {
            get { return this._grandTotal; }
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
            get { return this._discountAmount; }
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
            get { return this._couponDiscountAmount; }
            set
            {
                if ((this._couponDiscountAmount != value))
                {
                    this._couponDiscountAmount = value;
                }
            }
        }

        public decimal ShippingRate
        {
            get { return this._shippingRate; }
            set
            {
                if ((this._shippingRate != value))
                {
                    this._shippingRate = value;
                }
            }
        }
        public string CouponCode
        {
            get { return this._couponCode; }
            set
            {
                if ((this._couponCode != value))
                {
                    this._couponCode = value;
                }
            }
        }

        public string PurchaseOrderNumber
        {
            get { return this._purchaseOrderNumber; }
            set
            {
                if ((this._purchaseOrderNumber != value))
                {
                    this._purchaseOrderNumber = value;
                }
            }
        }

        public int PaymentGatewayTypeId
        {
            get { return this._paymentGatewayTypeId; }
            set
            {
                if ((this._paymentGatewayTypeId != value))
                {
                    this._paymentGatewayTypeId = value;
                }
            }
        }



        public string ClientIPAddress
        {
            get { return this._clientIPAddress; }
            set
            {
                if ((this._clientIPAddress != value))
                {
                    this._clientIPAddress = value;
                }
            }
        }

        public int UserBillingAddressId
        {
            get { return this._userBillingAddressId; }
            set
            {
                if ((this._userBillingAddressId != value))
                {
                    this._userBillingAddressId = value;
                }
            }
        }
        public int UserShippingAddressId
        {
            get { return this._userShippingAddressId; }
            set
            {
                if ((this._userShippingAddressId != value))
                {
                    this._userShippingAddressId = value;
                }
            }
        }

        public int ShippingMethodId
        {
            get { return this._shippingMethodId; }
            set
            {
                if ((this._shippingMethodId != value))
                {
                    this._shippingMethodId = value;
                }
            }
        }

        public int PaymentMethodId
        {
            get { return this._paymentMethodId; }
            set
            {
                if ((this._paymentMethodId != value))
                {
                    this._paymentMethodId = value;
                }
            }
        }

        public decimal TaxTotal
        {
            get { return this._taxTotal; }
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
            get { return this._currencyCode; }
            set
            {
                if ((this._currencyCode != value))
                {
                    this._currencyCode = value;
                }
            }
        }



        public int CustomerId
        {
            get { return this._customerId; }
            set
            {
                if ((this._customerId != value))
                {
                    this._customerId = value;
                }
            }
        }


        public bool IsGuestUser
        {
            get { return this._isGuestUser; }
            set
            {
                if ((this._isGuestUser != value))
                {
                    this._isGuestUser = value;
                }
            }
        }

        public bool IsDownloadable
        {
            get { return this._isDownloadable; }
            set
            {
                if ((this._isDownloadable != value))
                {
                    this._isDownloadable = value;
                }
            }
        }

        public int StoreId
        {
            get { return this._storeId; }
            set
            {
                if ((this._storeId != value))
                {
                    this._storeId = value;
                }
            }
        }

        public int PortalId
        {
            get { return this._portalId; }
            set
            {
                if ((this._portalId != value))
                {
                    this._portalId = value;
                }
            }
        }

        public string AddedBy
        {
            get { return this._addedBy; }
            set
            {
                if ((this._addedBy != value))
                {
                    this._addedBy = value;
                }
            }
        }

        public string CultureName
        {
            get { return this._cultureName; }
            set
            {
                if ((this._cultureName != value))
                {
                    this._cultureName = value;
                }
            }
        }

        public bool IsMultipleCheckOut
        {
            get { return this._isMultipleCheckOut; }
            set
            {
                if ((this._isMultipleCheckOut != value))
                {
                    this._isMultipleCheckOut = value;
                }
            }
        }

        public string Remarks
        {
            get { return this._remarks; }
            set
            {
                if ((this._remarks != value))
                {
                    this._remarks = value;
                }
            }
        }
        public string PaymentMethodName
        {
            get { return this._paymentMethodName; }
            set
            {
                if ((this._paymentMethodName != value))
                {
                    this._paymentMethodName = value;
                }
            }

        }
        public string ItemIds
        {
            get { return this._itemIds; }
            set
            {
                if ((this._itemIds != value))
                {
                    this._itemIds = value;
                }
            }
        }

        public string ItemQuantities
        {
            get { return this._itemQuantities; }
            set
            {
                if ((this._itemQuantities != value))
                {
                    this._itemQuantities = value;
                }
            }
        }


        #endregion
    }

}
