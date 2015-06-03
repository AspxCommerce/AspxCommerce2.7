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
   public class OrderDetailsByOrderIDInfo
    {
        private System.Nullable<int> _orderID;
        private string _orderDate;
		private System.Nullable<int> _addressID;
		
		private string _billingName;
		
		private string _email;
		
		private string _company;
		
		private string _address1;
		
		private string _address2;
		
		private string _city;
		
		private string _state;
		
		private string _zip;
		
		private string _country;
		
		private string _phone;
		
		private string _mobile;
		
		private string _fax;
		
		private string _website;
		
		private string _storeName;
		
		private string _storeDescription;
		
		private string _shippingName;
		
		private string _shipEmail;
		
		private string _shipCompany;
		
		private string _shipAddress1;
		
		private string _shipAddress2;
		
		private string _shipCity;
		
		private string _shipState;
		
		private string _shipZip;
		
		private string _shipCountry;
		
		private string _shipPhone;
		
		private string _shipMobile;
		
		private string _shipFax;
		
		private string _shipWebsite;
		
		private string _shippingMethodName;
		
		private string _paymentMethodName;
		
		private string _orderStatusName;
        private string _itemName;

        private string _sku;

        private System.Nullable<decimal> _shippingCost;

		private System.Nullable<int> _itemId;
		
		private System.Nullable<int> _quantity;
		
		private System.Nullable<decimal> _price;
		
		private System.Nullable<decimal> _taxTotal;
        private System.Nullable<decimal> _couponAmount;
        private System.Nullable<decimal> _grandSubTotal;
        private System.Nullable<decimal> _totalCost;
        private System.Nullable<decimal> _subTotal;
       
        private string _invoiceNumber;
		public OrderDetailsByOrderIDInfo()
		{
		}
		
		[DataMember]
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

        [DataMember]
        public string OrderDate
        {
            get
            {
                return this._orderDate;
            }
            set
            {
                if ((this._orderDate != value))
                {
                    this._orderDate = value;
                }
            }
        }
		
        [DataMember]
        public System.Nullable<int> AddressID
		{
			get
			{
				return this._addressID;
			}
			set
			{
				if ((this._addressID != value))
				{
					this._addressID = value;
				}
			}
		}
		
		[DataMember]
		public string BillingName
		{
			get
			{
				return this._billingName;
			}
			set
			{
				if ((this._billingName != value))
				{
					this._billingName = value;
				}
			}
		}
		
		[DataMember]
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
		
		[DataMember]
		public string Company
		{
			get
			{
				return this._company;
			}
			set
			{
				if ((this._company != value))
				{
					this._company = value;
				}
			}
		}
		
		[DataMember]
		public string Address1
		{
			get
			{
				return this._address1;
			}
			set
			{
				if ((this._address1 != value))
				{
					this._address1 = value;
				}
			}
		}
		
		[DataMember]
		public string Address2
		{
			get
			{
				return this._address2;
			}
			set
			{
				if ((this._address2 != value))
				{
					this._address2 = value;
				}
			}
		}
		
		[DataMember]
		public string City
		{
			get
			{
				return this._city;
			}
			set
			{
				if ((this._city != value))
				{
					this._city = value;
				}
			}
		}
		
		[DataMember]
		public string State
		{
			get
			{
				return this._state;
			}
			set
			{
				if ((this._state != value))
				{
					this._state = value;
				}
			}
		}
		
		[DataMember]
		public string Zip
		{
			get
			{
				return this._zip;
			}
			set
			{
				if ((this._zip != value))
				{
					this._zip = value;
				}
			}
		}
		
		[DataMember]
		public string Country
		{
			get
			{
				return this._country;
			}
			set
			{
				if ((this._country != value))
				{
					this._country = value;
				}
			}
		}
		
		[DataMember]
		public string Phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				if ((this._phone != value))
				{
					this._phone = value;
				}
			}
		}
		
		[DataMember]
		public string Mobile
		{
			get
			{
				return this._mobile;
			}
			set
			{
				if ((this._mobile != value))
				{
					this._mobile = value;
				}
			}
		}
		
		[DataMember]
		public string Fax
		{
			get
			{
				return this._fax;
			}
			set
			{
				if ((this._fax != value))
				{
					this._fax = value;
				}
			}
		}
		
		[DataMember]
		public string Website
		{
			get
			{
				return this._website;
			}
			set
			{
				if ((this._website != value))
				{
					this._website = value;
				}
			}
		}
		
		[DataMember]
		public string StoreName
		{
			get
			{
				return this._storeName;
			}
			set
			{
				if ((this._storeName != value))
				{
					this._storeName = value;
				}
			}
		}
		
		[DataMember]
		public string StoreDescription
		{
			get
			{
				return this._storeDescription;
			}
			set
			{
				if ((this._storeDescription != value))
				{
					this._storeDescription = value;
				}
			}
		}
		
		[DataMember]
		public string ShippingName
		{
			get
			{
				return this._shippingName;
			}
			set
			{
				if ((this._shippingName != value))
				{
					this._shippingName = value;
				}
			}
		}
		
		[DataMember]
		public string ShipEmail
		{
			get
			{
				return this._shipEmail;
			}
			set
			{
				if ((this._shipEmail != value))
				{
					this._shipEmail = value;
				}
			}
		}
		
		[DataMember]
		public string ShipCompany
		{
			get
			{
				return this._shipCompany;
			}
			set
			{
				if ((this._shipCompany != value))
				{
					this._shipCompany = value;
				}
			}
		}
		
		[DataMember]
		public string ShipAddress1
		{
			get
			{
				return this._shipAddress1;
			}
			set
			{
				if ((this._shipAddress1 != value))
				{
					this._shipAddress1 = value;
				}
			}
		}
		
		[DataMember]
		public string ShipAddress2
		{
			get
			{
				return this._shipAddress2;
			}
			set
			{
				if ((this._shipAddress2 != value))
				{
					this._shipAddress2 = value;
				}
			}
		}
		
		[DataMember]
		public string ShipCity
		{
			get
			{
				return this._shipCity;
			}
			set
			{
				if ((this._shipCity != value))
				{
					this._shipCity = value;
				}
			}
		}
		
		[DataMember]
		public string ShipState
		{
			get
			{
				return this._shipState;
			}
			set
			{
				if ((this._shipState != value))
				{
					this._shipState = value;
				}
			}
		}
		
		[DataMember]
		public string ShipZip
		{
			get
			{
				return this._shipZip;
			}
			set
			{
				if ((this._shipZip != value))
				{
					this._shipZip = value;
				}
			}
		}
		
		[DataMember]
		public string ShipCountry
		{
			get
			{
				return this._shipCountry;
			}
			set
			{
				if ((this._shipCountry != value))
				{
					this._shipCountry = value;
				}
			}
		}
		
		[DataMember]
		public string ShipPhone
		{
			get
			{
				return this._shipPhone;
			}
			set
			{
				if ((this._shipPhone != value))
				{
					this._shipPhone = value;
				}
			}
		}
		
		[DataMember]
		public string ShipMobile
		{
			get
			{
				return this._shipMobile;
			}
			set
			{
				if ((this._shipMobile != value))
				{
					this._shipMobile = value;
				}
			}
		}
		
		[DataMember]
		public string ShipFax
		{
			get
			{
				return this._shipFax;
			}
			set
			{
				if ((this._shipFax != value))
				{
					this._shipFax = value;
				}
			}
		}
		
		[DataMember]
		public string ShipWebsite
		{
			get
			{
				return this._shipWebsite;
			}
			set
			{
				if ((this._shipWebsite != value))
				{
					this._shipWebsite = value;
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
		
		[DataMember]
		public string OrderStatusName
		{
			get
			{
				return this._orderStatusName;
			}
			set
			{
				if ((this._orderStatusName != value))
				{
					this._orderStatusName = value;
				}
			}
		}
		
		[DataMember]
		public System.Nullable<int> ItemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				if ((this._itemId != value))
				{
					this._itemId = value;
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
                return this._sku;
            }
            set
            {
                if ((this._sku != value))
                {
                    this._sku = value;
                }
            }
        }
		
		
		[DataMember]
		public System.Nullable<int> Quantity
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
		public System.Nullable<decimal> Price
		{
			get
			{
                return this._price;
			}
			set
			{
                if ((this._price != value))
				{
                    this._price = value;
				}
			}
		}
		
		[DataMember]
		public System.Nullable<decimal> TaxTotal
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
		
		[DataMember]
        public System.Nullable<decimal> CouponAmount
		{
			get
			{
				return this._couponAmount;
			}
			set
			{
				if ((this._couponAmount != value))
				{
					this._couponAmount = value;
				}
			}
		}
        [DataMember]
        public System.Nullable<decimal> GrandSubTotal
        {
            get
            {
                return this._grandSubTotal;
            }
            set
            {
                if ((this._grandSubTotal != value))
                {
                    this._grandSubTotal = value;
                }
            }
        }

        [DataMember]
        public System.Nullable<decimal> SubTotal
        {
            get
            {
                return this._subTotal;
            }
            set
            {
                if ((this._subTotal != value))
                {
                    this._subTotal = value;
                }
            }
        }


        [DataMember]
        public System.Nullable<decimal> ShippingCost
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

        [DataMember]
        public System.Nullable<decimal> TotalCost
        {
            get
            {
                return this._totalCost;
            }
            set
            {
                if ((this._totalCost != value))
                {
                    this._totalCost = value;
                }
            }
        }

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

	}  
}
