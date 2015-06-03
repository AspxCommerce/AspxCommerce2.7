using System;
using System.Runtime.Serialization;


namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class YouJustExploredSettingInfo
    {
        [DataMember]
        private string _enableModule;
        [DataMember]
        private int _noOfItemShown;
        [DataMember]
        private string _headerText;
        [DataMember]
        private int _storeId;
        [DataMember]
        private int _portalId;
        [DataMember]
        private string _cultureName;

        public string EnableModule
        {
            get { return this._enableModule; }
            set
            {
                if (this._enableModule != value)
                {
                    this._enableModule = value;
                }
            }
        }

        public int NoOfItemShown
        {
            get { return this._noOfItemShown; }
            set
            {
                if (this._noOfItemShown != value)
                {
                    this._noOfItemShown = value;
                }
            }
        }
        public string HeaderText
        {
            get { return this._headerText; }
            set
            {
                if (this._headerText != value)
                {
                    this._headerText = value;
                }
            }
        }
        public int StoreId
        {
            get { return this._storeId; }
            set
            {
                if (this._storeId != value)
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
                if (this._portalId != value)
                {
                    this._portalId = value;
                }
            }
        }
        public string CultureName
        {
            get
            {
                return this._cultureName;
            }
            set
            {
                if (this._cultureName != value)
                {
                    this._cultureName = value;
                }
            }
        }
    }
}