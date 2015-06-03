using System;
using System.Runtime.Serialization;

namespace AspxCommerce.RewardPoints
{
    [DataContract]
    [Serializable]
    public class RewardPointsSettingInfo
    {
        private int _rowTotal;
        private int _rowNumber;
        private int _rewardPointSettingsID;
        private string _rewardRuleName;
        private int _rewardRuleID;
        private string _rewardRuleType;
        private decimal _rewardPoints;
        private decimal _purchaseAmount;
        private bool _isActive;

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
        public int RewardPointSettingsID
        {
            get
            {
                return this._rewardPointSettingsID;
            }
            set
            {
                if (this._rewardPointSettingsID != value)
                {
                    this._rewardPointSettingsID = value;
                }
            }
        }
        [DataMember]
        public string RewardRuleName
        {
            get
            {
                return this._rewardRuleName;
            }
            set
            {
                if (this._rewardRuleName != value)
                {
                    this._rewardRuleName = value;
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
        public string RewardRuleType
        {
            get
            {
                return this._rewardRuleType;
            }
            set
            {
                if (this._rewardRuleType != value)
                {
                    this._rewardRuleType = value;
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
        public decimal PurchaseAmount
        {
            get
            {
                return this._purchaseAmount;
            }
            set
            {
                if (this._purchaseAmount != value)
                {
                    this._purchaseAmount = value;
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


    }

}

