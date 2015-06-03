using System;
using System.Runtime.Serialization;
namespace AspxCommerce.RewardPoints
{
    [DataContract]
    [Serializable]
    public class MyRewardPointsInfo
    {

        [DataMember(Name = "_rowTotal", Order = 0)]
        private int _rowTotal;

        [DataMember(Name = "_rowNumber", Order = 1)]
        private int _rowNumber;

        [DataMember(Name = "_rewardPoints", Order = 2)]
        private decimal _rewardPoints;

        [DataMember(Name = "_rewardAmount", Order = 3)]
        private decimal _rewardAmount;

        [DataMember(Name = "_usedRewardPoints", Order = 4)]
        private decimal _usedRewardPoints;

        [DataMember(Name = "_balanaceRewardPoints", Order = 5)]
        private decimal _balanaceRewardPoints;

        [DataMember(Name = "_rewardReason", Order = 6)]
        private string _rewardReason;

        [DataMember(Name = "_addedOn", Order = 7)]
        private string _addedOn;

        [DataMember(Name = "_expiresOn", Order = 8)]
        private string _expiresOn;

        [DataMember(Name = "_rewardPointsExpiresInDays", Order = 9)]
        private string _rewardPointsExpiresInDays;

        [DataMember(Name = "_isActive", Order = 10)]
        private bool _isActive;


        public int RowNumber
        {
            get
            {
                return this._rowNumber;
            }
            set
            {
                if ((this._rowNumber != value))
                {
                    this._rowNumber = value;
                }
            }
        }

        public int RowTotal
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

        public decimal RewardPoints
        {
            get
            {
                return this._rewardPoints;
            }
            set
            {
                if ((this._rewardPoints != value))
                {
                    this._rewardPoints = value;
                }
            }
        }
        public decimal RewardAmount
        {
            get
            {
                return this._rewardAmount;
            }
            set
            {
                if ((this._rewardAmount != value))
                {
                    this._rewardAmount = value;
                }
            }
        }
        public decimal UsedRewardPoints
        {
            get
            {
                return this._usedRewardPoints;
            }
            set
            {
                if ((this._usedRewardPoints != value))
                {
                    this._usedRewardPoints = value;
                }
            }
        }
        public decimal BalanaceRewardPoints
        {
            get
            {
                return this._balanaceRewardPoints;
            }
            set
            {
                if ((this._balanaceRewardPoints != value))
                {
                    this._balanaceRewardPoints = value;
                }
            }
        }
        public string RewardReason
        {
            get
            {
                return this._rewardReason;
            }
            set
            {
                if ((this._rewardReason != value))
                {
                    this._rewardReason = value;
                }
            }
        }
        public string AddedOn
        {
            get
            {
                return this._addedOn;
            }
            set
            {
                if ((this._addedOn != value))
                {
                    this._addedOn = value;
                }
            }
        }
        public string ExpiresOn
        {
            get
            {
                return this._expiresOn;
            }
            set
            {
                if ((this._expiresOn != value))
                {
                    this._expiresOn = value;
                }
            }
        }
        public string RewardPointsExpiresInDays
        {
            get
            {
                return this._rewardPointsExpiresInDays;
            }
            set
            {
                if ((this._rewardPointsExpiresInDays != value))
                {
                    this._rewardPointsExpiresInDays = value;
                }
            }
        }
        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if ((this._isActive != value))
                {
                    this._isActive = value;
                }
            }
        }



    }
}
