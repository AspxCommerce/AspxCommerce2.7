using System;
using System.Runtime.Serialization;

namespace AspxCommerce.RewardPoints
{
    [DataContract]
    [Serializable]
    public class GeneralSettingInfo
    {
        private int _generalSettingsID;
        private decimal _rewardPoints;
        private decimal _rewardExchangeRate;
        private decimal _amountSpent;
        private decimal _rewardPointsEarned;
        private string _addOrderStatusID;
        private string _subOrderStatusID;
        private int _rewardPointsExpiresInDays;
        private decimal _minRedeemBalance;
        private decimal _balanceCapped;
        private bool _isActive;
        private decimal _totalRewardPoints;
        private decimal _totalRewardAmount;
        private string _orderStatus;
        private decimal _refAmountSpent;
        private decimal _refPointsEarned;
        private decimal _refAmountSpentNext;
        private decimal _refPointsEarnedNext;
        private bool _isPurchaseActive;

        [DataMember]
        public int GeneralSettingsID
        {
            get
            {
                return this._generalSettingsID;
            }
            set
            {
                if (this._generalSettingsID != value)
                {
                    this._generalSettingsID = value;
                }
            }
        }
        [DataMember]
        public decimal RewardPoints
        {
            get
            {
                return this._rewardPoints;
            }
            set
            {
                if (this._rewardPoints != value)
                {
                    this._rewardPoints = value;
                }
            }
        }
        [DataMember]
        public decimal RewardExchangeRate
        {
            get
            {
                return this._rewardExchangeRate;
            }
            set
            {
                if (this._rewardExchangeRate != value)
                {
                    this._rewardExchangeRate = value;
                }
            }
        }
        [DataMember]
        public decimal AmountSpent
        {
            get
            {
                return this._amountSpent;
            }
            set
            {
                if (this._amountSpent != value)
                {
                    this._amountSpent = value;
                }
            }
        }
        [DataMember]
        public decimal RewardPointsEarned
        {
            get
            {
                return this._rewardPointsEarned;
            }
            set
            {
                if (this._rewardPointsEarned != value)
                {
                    this._rewardPointsEarned = value;
                }
            }
        }
        [DataMember]
        public string AddOrderStatusID
        {
            get
            {
                return this._addOrderStatusID;
            }
            set
            {
                if (this._addOrderStatusID != value)
                {
                    this._addOrderStatusID = value;
                }
            }
        }
        [DataMember]
        public string SubOrderStatusID
        {
            get
            {
                return this._subOrderStatusID;
            }
            set
            {
                if (this._subOrderStatusID != value)
                {
                    this._subOrderStatusID = value;
                }
            }
        }
        [DataMember]
        public int RewardPointsExpiresInDays
        {
            get
            {
                return this._rewardPointsExpiresInDays;
            }
            set
            {
                if (this._rewardPointsExpiresInDays != value)
                {
                    this._rewardPointsExpiresInDays = value;
                }
            }
        }
        [DataMember]
        public decimal MinRedeemBalance
        {
            get
            {
                return this._minRedeemBalance;
            }
            set
            {
                if (this._minRedeemBalance != value)
                {
                    this._minRedeemBalance = value;
                }
            }
        }
        [DataMember]
        public decimal BalanceCapped
        {
            get
            {
                return this._balanceCapped;
            }
            set
            {
                if (this._balanceCapped != value)
                {
                    this._balanceCapped = value;
                }
            }
        }
        [DataMember]
        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if (this._isActive != value)
                {
                    this._isActive = value;
                }
            }
        }
        [DataMember]
        public decimal TotalRewardPoints
        {
            get
            {
                return this._totalRewardPoints;
            }
            set
            {
                if (this._totalRewardPoints != value)
                {
                    this._totalRewardPoints = value;
                }
            }
        }
        [DataMember]
        public decimal TotalRewardAmount
        {
            get
            {
                return this._totalRewardAmount;
            }
            set
            {
                if (this._totalRewardAmount != value)
                {
                    this._totalRewardAmount = value;
                }
            }
        }
        [DataMember]
        public string OrderStatus
        {
            get
            {
                return this._orderStatus;
            }
            set
            {
                if (this._orderStatus != value)
                {
                    this._orderStatus = value;
                }
            }
        }

        [DataMember]
        public decimal RefAmountSpent
        {
            get
            {
                return this._refAmountSpent;
            }
            set
            {
                if (this._refAmountSpent != value)
                {
                    this._refAmountSpent = value;
                }
            }
        }
        [DataMember]
        public decimal RefPointsEarned
        {
            get
            {
                return this._refPointsEarned;
            }
            set
            {
                if (this._refPointsEarned != value)
                {
                    this._refPointsEarned = value;
                }
            }
        }

        [DataMember]
        public decimal RefAmountSpentNext
        {
            get
            {
                return this._refAmountSpentNext;
            }
            set
            {
                if (this._refAmountSpentNext != value)
                {
                    this._refAmountSpentNext = value;
                }
            }
        }
        [DataMember]
        public decimal RefPointsEarnedNext
        {
            get
            {
                return this._refPointsEarnedNext;
            }
            set
            {
                if (this._refPointsEarnedNext != value)
                {
                    this._refPointsEarnedNext = value;
                }
            }
        }

        [DataMember]
        public bool IsPurchaseActive
        {
            get
            {
                return this._isPurchaseActive;
            }
            set
            {
                if (this._isPurchaseActive != value)
                {
                    this._isPurchaseActive = value;
                }
            }
        }


    }
}
