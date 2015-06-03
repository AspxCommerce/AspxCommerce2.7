using System;
using System.Runtime.Serialization;

namespace AspxCommerce.Moneybookers
{
    [DataContract]
    [Serializable]   
    public class MoneybookersSettingInfo
    {
        private string _MoneybookersSuccessUrl;
        private string _MoneybookersMerchantAccount;		
        private string  _IsTestMoneybookers;
        private string _MoneybookersSecretWord;
        private string _MoneybookersCurrencyCode;
        private string _MoneybookersStatusUrl;
        private string _MoneybookersCancelUrl;
        private string _MoneybookersLogoUrl;
        
        
        public MoneybookersSettingInfo()
        {
        }
        [DataMember]
        public string MoneybookersSuccessUrl
		{
			get
			{
                return this._MoneybookersSuccessUrl;
			}
			set
			{
                if ((this._MoneybookersSuccessUrl != value))
				{
                    this._MoneybookersSuccessUrl = value;
				}
			}
		}

      
        [DataMember]
        public string MoneybookersMerchantAccount
		{
			get
			{
                return this._MoneybookersMerchantAccount;
			}
			set
			{
                if ((this._MoneybookersMerchantAccount != value))
				{
                    this._MoneybookersMerchantAccount = value;
				}
			}
		}

       
        [DataMember]
        public string IsTestMoneybookers
        {
            get
            {
                return this._IsTestMoneybookers;
            }
            set
            {
                if ((this._IsTestMoneybookers != value))
                {
                    this._IsTestMoneybookers = value;
                }
            }
        }

        [DataMember]
        public string MoneybookersSecretWord
        {
            get
            {
                return this._MoneybookersSecretWord;
            }
            set
            {
                if ((this._MoneybookersSecretWord != value))
                {
                    this._MoneybookersSecretWord = value;
                }
            }
        }

        [DataMember]
        public string MoneybookersCurrencyCode
        {
            get
            {
                return this._MoneybookersCurrencyCode;
            }
            set
            {
                if ((this._MoneybookersCurrencyCode != value))
                {
                    this._MoneybookersCurrencyCode = value;
                }
            }
        }

        [DataMember]
        public string MoneybookersStatusUrl
        {
            get
            {
                return this._MoneybookersStatusUrl;
            }
            set
            {
                if ((this._MoneybookersStatusUrl != value))
                {
                    this._MoneybookersStatusUrl = value;
                }
            }
        }

        [DataMember]
        public string MoneybookersCancelUrl
        {
            get
            {
                return this._MoneybookersCancelUrl;
            }
            set
            {
                if ((this._MoneybookersCancelUrl != value))
                {
                    this._MoneybookersCancelUrl = value;
                }
            }
        }

        [DataMember]
        public string MoneybookersLogoUrl
        {
            get
            {
                return this._MoneybookersLogoUrl;
            }
            set
            {
                if ((this._MoneybookersLogoUrl != value))
                {
                    this._MoneybookersLogoUrl = value;
                }
            }
        }
		
    }
}
