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
    public class TaxRateInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_taxRateID", Order = 1)]
		private int _taxRateID;

        [DataMember(Name = "_taxRateTitle", Order = 2)]
		private string _taxRateTitle;

        [DataMember(Name = "_country", Order = 3)]
		private string _country;

        [DataMember(Name = "_state", Order = 4)]
		private string _state;

        [DataMember(Name = "_zipPostCode", Order = 5)]
        private string _zipPostCode;

        [DataMember(Name = "_isZipPostRange", Order = 6)]
        private string _isZipPostRange;

        [DataMember(Name = "_taxRateValue", Order = 7)]
		private System.Nullable<decimal> _taxRateValue;

        [DataMember(Name = "_isPercent", Order = 8)]
        private string _isPercent;


        public TaxRateInfo()
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
		
		public int TaxRateID
		{
			get
			{
				return this._taxRateID;
			}
			set
			{
				if ((this._taxRateID != value))
				{
					this._taxRateID = value;
				}
			}
		}
		
		public string TaxRateTitle
		{
			get
			{
				return this._taxRateTitle;
			}
			set
			{
				if ((this._taxRateTitle != value))
				{
					this._taxRateTitle = value;
				}
			}
		}
		
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

        public string ZipPostCode
		{
			get
			{
                return this._zipPostCode;
			}
			set
			{
                if ((this._zipPostCode != value))
				{
                    this._zipPostCode = value;
				}
			}
		}

        public string IsZipPostRange
		{
			get
			{
                return this._isZipPostRange;
			}
			set
			{
                if ((this._isZipPostRange != value))
				{
                    this._isZipPostRange = value;
				}
			}
		}
		
		
		public System.Nullable<decimal> TaxRateValue
		{
			get
			{
				return this._taxRateValue;
			}
			set
			{
				if ((this._taxRateValue != value))
				{
					this._taxRateValue = value;
				}
			}
		}

        public string IsPercent
        {
            get
            {
                return this._isPercent;
            }
            set
            {
                if ((this._isPercent != value))
                {
                    this._isPercent = value;
                }
            }
        }

       
    }
    public class TaxRateDataTnfo {       
        public string TaxName { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int TaxRateID { get; set; }
        public string TaxRateTitle { get; set; }
        public string IsZipPostRange { get; set; }
        public string TaxRateValue { get; set; }
        public bool RateType { get; set; }

    }
}
