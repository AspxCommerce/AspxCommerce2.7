using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.Core
{
    [Serializable]
    [DataContract]
    public class CommonRateList
    {
        private int _shippingMethodId;
        private string _shippingMethodName;
        private string _shippingMethodCode;
        private string _deliveryTime;
        private string _currencyCode;
        private decimal _totalCharges;
        private string _imagePath="";

        [DataMember]
        public int ShippingMethodId
        {
            get { return this._shippingMethodId; }
            set { this._shippingMethodId = value; }
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
                this._shippingMethodName = value;
            }
        }
        [DataMember]
        public string ShippingMethodCode
        {
            get
            {
                return this._shippingMethodCode;
            }
            set
            {
                this._shippingMethodCode = value;
            }
        }
        [DataMember]
        public string DeliveryTime
        {
            get
            {
                return this._deliveryTime;
            }
            set
            {
                this._deliveryTime = value;
            }
        }
        [DataMember]
        public decimal TotalCharges
        {
            get
            {
                return this._totalCharges;
            }
            set
            {
                this._totalCharges = value;
            }
        }

        [DataMember]
        public string CurrencyCode
        {
            get
            {
                return this._currencyCode;
            }
            set
            {
                this._currencyCode = value;
            }
        }
        [DataMember]
        public string ImagePath
        {
            get
            {
                return this._imagePath;
            }
            set
            {
                this._imagePath = value;
            }
        }

    }
}
