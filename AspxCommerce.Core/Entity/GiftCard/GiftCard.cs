using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class GiftCard
    {
        public int? GiftCardId { get; set; }
        public int? GiftCardTypeId { get; set; }
        public int? GraphicThemeId { get; set; }
        public string GiftCardCode { get; set; }
        public string GiftCardPinCode { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
        public string Messege { get; set; }
        public decimal? Price { get; set; }
        public decimal? Balance { get; set; }
        public string GraphicThemeUrl { get; set; }
        public int? GiftCardCategoryId { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool? IsRecipientNotified { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsVerified { get; set; }
       
    }
    public class BalanceInquiry
    {
        public decimal? Price { get; set; }
        public decimal? UsedAmount { get; set; }
        public decimal? Balance { get; set; }
        public decimal? Deposit { get; set; }
        public string Note { get; set; }
        public DateTime? AddedOn { get; set; }
    }

    public class Vefification
    {
        public int GiftCardId { get; set; }
        public string GiftCardCode { get; set; }
        public decimal Price { get; set; }
        public decimal Balance { get; set; }
        public bool IsVerified { get; set; }
    }

    public class GiftCardGrid
    {
        public int RowTotal { get; set; }
        public int GiftCardId { get; set; }
        public string GiftCardCode { get; set; }
        public decimal Balance { get; set; }
        public bool IsRecipientNotified { get; set; }
        public DateTime AddedOn { get; set; }
        public bool IsActive { get; set; }
      
    }

    public class GiftCardType
    {
        public string Type { get; set; }
        public int TypeId { get; set; }
    }

    public class GiftCardDataInfo
    {
        public int? OrderID { get; set; }
        public decimal? Balance { get; set; }
        public decimal? BalanceTo { get; set; }
        public string GiftcardCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Status { get; set; }
    }
}
