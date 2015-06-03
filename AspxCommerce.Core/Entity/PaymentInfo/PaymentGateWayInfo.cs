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
   public class PaymentGateWayInfo
    {
       public PaymentGateWayInfo()
       {

       }
        [DataMember(Name = "_rowTotal", Order = 0)]
       private System.Nullable<int> _rowTotal;
		
        [DataMember(Name = "_paymentGatewayTypeID", Order = 1)]
		private int _paymentGatewayTypeID;
		
        [DataMember(Name = "_paymentGatewayTypeName", Order = 2)]
		private string _paymentGatewayTypeName;

		[DataMember(Name = "_isActive", Order = 3)]
		private System.Nullable<bool> _isActive;
        [DataMember(Name = "_isUse", Order = 4)]
        private System.Nullable<bool> _isUse;
        [DataMember(Name = "_edit", Order = 5)]
        private string _edit;

        [DataMember(Name = "_setting", Order = 6)]
        private string _setting;

        [DataMember(Name = "_hdnEdit", Order = 7)]
        private string _hdnEdit;

        [DataMember(Name = "_hdnSetting", Order = 8)]
        private string _hdnSetting;

        [DataMember(Name = "_hdnPaymentGatewayTypeID", Order = 9)]
        private int _hdnPaymentGatewayTypeID;

        [DataMember(Name = "_logoUrl", Order = 10)]
        private string _logoUrl;
        
    

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
		
		
		public System.Nullable<bool> IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if ((this._isActive != value))
				{
					this._isActive = value;
				}
			}
		}
        public System.Nullable<bool> IsUse
        {
            get
            {
                return this._isUse;
            }
            set
            {
                if ((this._isUse != value))
                {
                    this._isUse = value;
                }
            }
        }
        public string Edit
        {
            get
            {
                return this._edit;
            }
            set
            {
                if ((this._edit != value))
                {
                    this._edit = value;
                }
            }
        }

        public string Setting
        {
            get
            {
                return this._setting;
            }
            set
            {
                if ((this._setting != value))
                {
                    this._setting = value;
                }
            }
        }
        public string HdnEdit
        {
          get
            {
                return this._hdnEdit;
            }
            set
            {
                if ((this._hdnEdit != value))
                {
                    this._hdnEdit = value;
                }
            }
        }


        public string HdnSetting
        {
            get
            {
                return this._hdnSetting;
            }
            set
            {
                if ((this._hdnSetting != value))
                {
                    this._hdnSetting = value;
                }
            }
        }
        public int HdnPaymentGatewayTypeID
        {
            get
            {
                return this._hdnPaymentGatewayTypeID;
            }
            set
            {
                if ((this._hdnPaymentGatewayTypeID != value))
                {
                    this._hdnPaymentGatewayTypeID = value;
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
    public class PaymentGateWayBasicInfo
    {
        public int PaymentGateWayID { get; set; }
        public int OrderID { get; set; }
        public string PaymentGatewayName { get; set; }
        public string BillToName { get; set; }
        public string ShipToName { get; set; }
        public string OrderStatusName { get; set; }
        public bool IsUse { get; set; }
        public string LogoUrl { get; set; }
        public string DestinationUrl { get; set; }
        public string OldLogoUrl { get; set; }
        public System.Nullable<bool> IsActive { get; set; }

    }
 }

