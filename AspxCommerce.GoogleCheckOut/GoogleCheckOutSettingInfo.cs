using System;
using System.Runtime.Serialization;

namespace AspxCommerce.GoogleCheckOut
{
    [DataContract]
    [Serializable]   
    public class GoogleCheckOutSettingInfo
    {
        private string _GoogleMerchantID;

        private string _GoogleMerchantKey;

        private string _GoogleEnvironmentType;

        private string _GoogleAPICallbackUrl;

        private string _GoogleCurrencyType;
       
        
        public GoogleCheckOutSettingInfo()
        {
        }
        [DataMember]
        public string GoogleMerchantID
		{
			get
			{
                return this._GoogleMerchantID;
			}
			set
			{
                if ((this._GoogleMerchantID != value))
				{
                    this._GoogleMerchantID = value;
				}
			}
		}

      
        [DataMember]
        public string GoogleMerchantKey
		{
			get
			{
                return this._GoogleMerchantKey;
			}
			set
			{
                if ((this._GoogleMerchantKey != value))
				{
                    this._GoogleMerchantKey = value;
				}
			}
		}

       
        [DataMember]
        public string GoogleEnvironmentType
        {
            get
            {
                return this._GoogleEnvironmentType;
            }
            set
            {
                if ((this._GoogleEnvironmentType != value))
                {
                    this._GoogleEnvironmentType = value;
                }
            }
        }

        [DataMember]
        public string GoogleAPICallbackUrl
        {
            get
            {
                return this._GoogleAPICallbackUrl;
            }
            set
            {
                if ((this._GoogleAPICallbackUrl != value))
                {
                    this._GoogleAPICallbackUrl = value;
                }
            }
        }

        [DataMember]
        public string GoogleCurrencyType
        {
            get
            {
                return this._GoogleCurrencyType;
            }
            set
            {
                if ((this._GoogleCurrencyType != value))
                {
                    this._GoogleCurrencyType = value;
                }
            }
        }

        
        
		
    }
}
