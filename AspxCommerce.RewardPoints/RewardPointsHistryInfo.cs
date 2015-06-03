using System;
using System.Runtime.Serialization;
namespace AspxCommerce.RewardPoints
{
    [DataContract]
    [Serializable]
    public class RewardPointsHistryInfo
    {
        private int _rowNumber;
        private int _rowTotal;
        private int _customerID;
        private string _customerName;
        private string _email;
        private decimal _totalPoints;
        private decimal _usedPoints;
        private decimal _netPoints;
        private decimal _netAmount;
        private string _addedBy;

        [DataMember]
        public int RowNumber
        {
            get
            {
                return this._rowNumber;
            }
            set
            {
                if (this._rowNumber != value)
                {
                    this._rowNumber = value;
                }
            }
        }
        [DataMember]
        public int RowTotal
        {
            get
            {
                return this._rowTotal;
            }
            set
            {
                if (this._rowTotal != value)
                {
                    this._rowTotal = value;
                }
            }
        }
        [DataMember]
        public int CustomerID
        {
            get
            {
                return this._customerID;
            }
            set
            {
                if (this._customerID != value)
                {
                    this._customerID = value;
                }
            }
        }
        [DataMember]
        public string CustomerName
        {
            get
            {
                return this._customerName;
            }
            set
            {
                if (this._customerName != value)
                {
                    this._customerName = value;
                }
            }
        }
        [DataMember]
        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                if (this._email != value)
                {
                    this._email = value;
                }
            }
        }
        [DataMember]
        public decimal TotalPoints
        {
            get
            {
                return this._totalPoints;
            }
            set
            {
                if (this._totalPoints != value)
                {
                    this._totalPoints = value;
                }
            }
        }
        [DataMember]
        public decimal UsedPoints
        {
            get
            {
                return this._usedPoints;
            }
            set
            {
                if (this._usedPoints != value)
                {
                    this._usedPoints = value;
                }
            }
        }
        [DataMember]
        public decimal NetPoints
        {
            get
            {
                return this._netPoints;
            }
            set
            {
                if (this._netPoints != value)
                {
                    this._netPoints = value;
                }
            }
        }
        [DataMember]
        public decimal NetAmount
        {
            get
            {
                return this._netAmount;
            }
            set
            {
                if (this._netAmount != value)
                {
                    this._netAmount = value;
                }
            }
        }
        [DataMember]
        public string AddedBy
        {
            get
            {
                return this._addedBy;
            }
            set
            {
                if (this._addedBy != value)
                {
                    this._addedBy = value;
                }
            }
        }



    }
}
