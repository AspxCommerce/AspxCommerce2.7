using System;
using System.Runtime.Serialization;

namespace AuthorizeNetAIM
{
    [DataContract]
    [Serializable]
    public class AIMAuthorizeSettingInfo
    {
        [DataMember]
        private string _Version;
        [DataMember]
		private string _DelimData;
        [DataMember]
		private string _APILoginID;
        [DataMember]
		private string _TransactionKey;
        [DataMember]
		private string _RelayResponse;
        [DataMember]
		private string _DelimChar;
        [DataMember]
		private string _EncapChar;
        [DataMember]
        private string _XEmailCustomerAIM;
        [DataMember]
		private string _XMerchantEmailAIM;
        [DataMember]
		private string _XFooterEmailReceiptAIM;
        [DataMember]
		private string _XHeaderEmailReceiptAIM;
        [DataMember]
		private string _XHeaderHtmlReceiptAIM;
        [DataMember]
        private string _XMerchantDescriptorAIM;
        [DataMember]
        private string _IsTestAIM;

        [DataMember]
		private System.Nullable<int> _UserModuleID;

        public AIMAuthorizeSettingInfo()
        {
        }
        
		public string Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				if ((this._Version != value))
				{
					this._Version = value;
				}
			}
		}
		public string DelimData
		{
			get
			{
				return this._DelimData;
			}
			set
			{
				if ((this._DelimData != value))
				{
					this._DelimData = value;
				}
			}
		}
		public string APILoginID
		{
			get
			{
				return this._APILoginID;
			}
			set
			{
				if ((this._APILoginID != value))
				{
					this._APILoginID = value;
				}
			}
		}
        public string IsTestAIM
        {
            get
            {
                return this._IsTestAIM;
            }
            set
            {
                if ((this._IsTestAIM != value))
                {
                    this._IsTestAIM = value;
                }
            }
        }
		public string TransactionKey
		{
			get
			{
				return this._TransactionKey;
			}
			set
			{
				if ((this._TransactionKey != value))
				{
					this._TransactionKey = value;
				}
			}
		}
		public string RelayResponse
		{
			get
			{
				return this._RelayResponse;
			}
			set
			{
				if ((this._RelayResponse != value))
				{
					this._RelayResponse = value;
				}
			}
		}
		public string DelimChar
		{
			get
			{
				return this._DelimChar;
			}
			set
			{
				if ((this._DelimChar != value))
				{
					this._DelimChar = value;
				}
			}
		}
		public string EncapChar
		{
			get
			{
				return this._EncapChar;
			}
			set
			{
				if ((this._EncapChar != value))
				{
					this._EncapChar = value;
				}
			}
		}     
        public string XEmailCustomerAIM
        {
            get
            {
                return this._XEmailCustomerAIM;
            }
            set
            {
                if ((this._XEmailCustomerAIM != value))
                {
                    this._XEmailCustomerAIM = value;
                }
            }
        }
        public string XMerchantEmailAIM
        {
            get
            {
                return this._XMerchantEmailAIM;
            }
            set
            {
                if ((this._XMerchantEmailAIM != value))
                {
                    this._XMerchantEmailAIM = value;
                }
            }
        }
        public string XFooterEmailReceiptAIM
        {
            get
            {
                return this._XFooterEmailReceiptAIM;
            }
            set
            {
                if ((this._XFooterEmailReceiptAIM != value))
                {
                    this._XFooterEmailReceiptAIM = value;
                }
            }
        }
        public string XHeaderEmailReceiptAIM
        {
            get
            {
                return this._XHeaderEmailReceiptAIM;
            }
            set
            {
                if ((this._XHeaderEmailReceiptAIM != value))
                {
                    this._XHeaderEmailReceiptAIM = value;
                }
            }
        }
        public string XHeaderHtmlReceiptAIM
        {
            get
            {
                return this._XHeaderHtmlReceiptAIM;
            }
            set
            {
                if ((this._XHeaderHtmlReceiptAIM != value))
                {
                    this._XHeaderHtmlReceiptAIM = value;
                }
            }
        }
        public string XMerchantDescriptorAIM
        {
            get
            {
                return this._XMerchantDescriptorAIM;
            }
            set
            {
                if ((this._XMerchantDescriptorAIM != value))
                {
                    this._XMerchantDescriptorAIM = value;
                }
            }
        }
		public System.Nullable<int> UserModuleID
		{
			get
			{
				return this._UserModuleID;
			}
			set
			{
				if ((this._UserModuleID != value))
				{
					this._UserModuleID = value;
				}
			}
		}
    }
}
