using System;
using System.Runtime.Serialization;

namespace AspxCommerce.RewardPoints
{
    [DataContract]
    [Serializable]
    public class MyRewardPointsHistoryInfo
    {
        private int _rowNumber;
        private int _rowTotal;
        private int _rewardPointsID;
        private int _rewardRuleID;
        private int _exchangeRewardPoints;
        private decimal _exchangeRewardAmount;
        private int _rewardPoints;
        private int _usedRewardPoints;
        private int _balanaceRewardPoints;
        private int _totalRewardPoints;
        private decimal _totalRewardAmount;
        private string _rewardReason;
        private int _addOrderStatusID;
        private int _subOrderStatusID;
        private int _itemID;
        private string _costVariantValueID;
        private int _orderID;
        private bool _isActive;
        private string _addedOn;
        private int _totalDaysAddedOn;
        private string _expiresOn;
        private int _rewardPointsExpiresInDays;

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
        public int RewardPointsID
        {
            get
            {
                return this._rewardPointsID;
            }
            set
            {
                if (this._rewardPointsID != value)
                {
                    this._rewardPointsID = value;
                }
            }
        }
        [DataMember]
        public int RewardRuleID
        {
            get
            {
                return this._rewardRuleID;
            }
            set
            {
                if (this._rewardRuleID != value)
                {
                    this._rewardRuleID = value;
                }
            }
        }
        [DataMember]
        public int ExchangeRewardPoints
        {
            get
            {
                return this._exchangeRewardPoints;
            }
            set
            {
                if (this._exchangeRewardPoints != value)
                {
                    this._exchangeRewardPoints = value;
                }
            }
        }
        [DataMember]
        public decimal ExchangeRewardAmount
        {
            get
            {
                return this._exchangeRewardAmount;
            }
            set
            {
                if (this._exchangeRewardAmount != value)
                {
                    this._exchangeRewardAmount = value;
                }
            }
        }
        [DataMember]
        public int RewardPoints
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
        public int UsedRewardPoints
        {
            get
            {
                return this._usedRewardPoints;
            }
            set
            {
                if (this._usedRewardPoints != value)
                {
                    this._usedRewardPoints = value;
                }
            }
        }
        [DataMember]
        public int BalanaceRewardPoints
        {
            get
            {
                return this._balanaceRewardPoints;
            }
            set
            {
                if (this._balanaceRewardPoints != value)
                {
                    this._balanaceRewardPoints = value;
                }
            }
        }
        [DataMember]
        public int TotalRewardPoints
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
        public string RewardReason
        {
            get
            {
                return this._rewardReason;
            }
            set
            {
                if (this._rewardReason != value)
                {
                    this._rewardReason = value;
                }
            }
        }
        [DataMember]
        public int AddOrderStatusID
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
        public int SubOrderStatusID
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
        public int ItemID
        {
            get
            {
                return this._itemID;
            }
            set
            {
                if (this._itemID != value)
                {
                    this._itemID = value;
                }
            }
        }
        [DataMember]
        public string CostVariantValueID
        {
            get
            {
                return this._costVariantValueID;
            }
            set
            {
                if (this._costVariantValueID != value)
                {
                    this._costVariantValueID = value;
                }
            }
        }
        [DataMember]
        public int OrderID
        {
            get
            {
                return this._orderID;
            }
            set
            {
                if (this._orderID != value)
                {
                    this._orderID = value;
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
        public string AddedOn
        {
            get
            {
                return this._addedOn;
            }
            set
            {
                if (this._addedOn != value)
                {
                    this._addedOn = value;
                }
            }
        }
        [DataMember]
        public int TotalDaysAddedOn
        {
            get
            {
                return this._totalDaysAddedOn;
            }
            set
            {
                if (this._totalDaysAddedOn != value)
                {
                    this._totalDaysAddedOn = value;
                }
            }
        }
        [DataMember]
        public string ExpiresOn
        {
            get
            {
                return this._expiresOn;
            }
            set
            {
                if (this._expiresOn != value)
                {
                    this._expiresOn = value;
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









    }
}
