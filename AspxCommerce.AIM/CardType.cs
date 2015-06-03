using System;
using System.Runtime.Serialization;

namespace AuthorizeNetAIM 
{
    [Serializable]
    [DataContract]
    public class CardType
    {
        #region Privatre Members

        private int _cardTypeID;
        private string _cardTypeName;

        #endregion

        #region Public members

        [DataMember]
        public int cardTypeID
        {
            get
            {
                return this._cardTypeID;
            }
            set
            {
                if ((this._cardTypeID != value))
                {
                    this._cardTypeID = value;
                }
            }
        }
        [DataMember]
        public string cardTypeName
        {
            get
            {
                return this._cardTypeName;
            }
            set
            {
                if ((this._cardTypeName != value))
                {
                    this._cardTypeName = value;
                }
            }
        }

        #endregion

        #region Constructor
        public CardType()
        {

        }
        #endregion

    }
}
