
namespace AspxCommerce.RewardPoints
{

    public class RewardPointsCommonInfo
    {
        public System.Nullable<int> RewardPointSettingsID { get; set; }
        public string RewardRuleName { get; set; }
        public System.Nullable<int> RewardRuleID { get; set; }
        public string RewardRuleType { get; set; }
        public System.Nullable<decimal> RewardPoints { get; set; }
        public System.Nullable<decimal> PurchaseAmount { get; set; }      
        public System.Nullable<bool> IsActive { get; set; }
    }
    public class GeneralSettingsCommonInfo
    {
        public System.Nullable<int> GeneralSettingsID { get; set; }
        public System.Nullable<decimal> RewardPoints { get; set; }
        public System.Nullable<decimal> RewardExchangeRate { get; set; }
        public System.Nullable<decimal> AmountSpent { get; set; }
        public System.Nullable<decimal> RewardPointsEarned { get; set; }
        public string AddOrderStatusID { get; set; }
        public string SubOrderStatusID { get; set; }
        public System.Nullable<int> RewardPointsExpiresInDays { get; set; }
        public System.Nullable<decimal> MinRedeemBalance { get; set; }
        public System.Nullable<decimal> BalanceCapped { get; set; }
        public System.Nullable<bool> IsActive { get; set; }
    }
    public class RewardPointsHistoryCommonInfo
    {
        public string CustomerID { get; set; }    
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }    

    }
    public class RewardPointsNLCommonInfo
    {          
        public string Email { get; set; }
        public System.Nullable<int> RewardRuleID { get; set; }

    }
    public class RewardPointsPollCommonInfo
    {

        public System.Nullable<int> PollID { get; set; }
        public System.Nullable<int> UserModuleID { get; set; }
        public System.Nullable<int> RewardRuleID { get; set; }

    }
}