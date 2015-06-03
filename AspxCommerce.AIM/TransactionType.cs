using System;
using System.Runtime.Serialization;

namespace AuthorizeNetAIM 
{
    [Serializable]
    [DataContract]
   public class TransactionType
    {
        #region Privatre Members

        private int _transactionTypeID;
        private string _transactionTypeName;

        #endregion

        #region Public members

        [DataMember]
        public int transactionTypeID
        {
            get
            {
                return this._transactionTypeID;
            }
            set
            {
                if ((this._transactionTypeID != value))
                {
                    this._transactionTypeID = value;
                }
            }
        }
        [DataMember]
        public string transactionTypeName
        {
            get
            {
                return this._transactionTypeName;
            }
            set
            {
                if ((this._transactionTypeName != value))
                {
                    this._transactionTypeName = value;
                }
            }
        }

        #endregion

        #region Constructor
        public TransactionType()
        {

        }
        #endregion


    }
}
