using System;
using System.Runtime.Serialization;

namespace AspxCommerce.PayPal
{
    [DataContract]
    [Serializable]   
    public class PayPalSettingInfo
    {
         private string _returnUrl;
 
		private string _cancelUrl;
    
		private string _businessAccount;
 
		private string _verificationUrl;
        private string  _isTestPaypal;
        private string _authToken;
        public PayPalSettingInfo()
        {
        }
        [DataMember]
		public string ReturnUrl
		{
			get
			{
				return this._returnUrl;
			}
			set
			{
				if ((this._returnUrl != value))
				{
					this._returnUrl = value;
				}
			}
		}

        [DataMember]
		public string CancelUrl
		{
			get
			{
				return this._cancelUrl;
			}
			set
			{
				if ((this._cancelUrl != value))
				{
					this._cancelUrl = value;
				}
			}
		}

        [DataMember]
		public string BusinessAccount
		{
			get
			{
				return this._businessAccount;
			}
			set
			{
				if ((this._businessAccount != value))
				{
					this._businessAccount = value;
				}
			}
		}

        [DataMember]
		public string VerificationUrl
		{
			get
			{
				return this._verificationUrl;
			}
			set
			{
				if ((this._verificationUrl != value))
				{
					this._verificationUrl = value;
				}
			}
		}
        [DataMember]
        public string IsTestPaypal
        {
            get
            {
                return this._isTestPaypal;
            }
            set
            {
                if ((this._isTestPaypal != value))
                {
                    this._isTestPaypal = value;
                }
            }
        }
        [DataMember]
        public string AuthToken
        {
            get
            {
                return this._authToken;
            }
            set
            {
                if ((this._authToken != value))
                {
                    this._authToken = value;
                }
            }
        }
		
    }
}
