using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class RssFeedInfo
    {
        public string ShortDescription { get; set; }
        public string AddedOn { get; set; }
        public string ImagePath { get; set; }
    }

    public class RssFeedItemInfo : ItemCommonInfo
    {
        public string ShortDescription { get; set; }
        public string AddedOn { get; set; }
        public string AlternateText { get; set; }
    }

    public class RssFeedServiceType:RssFeedInfo
    {
        public int ServiceCategoryID { get; set; }
        public string ServiceName { get; set; }
    }

    public class  RssFeedCategory:RssFeedInfo
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    public class RssFeedPopularTag
    {
        public string TagName { get; set; }
        public string TagIDs { get; set; }
        public List<ItemCommonInfo> TagItemInfo { get; set; }
    }

    public class RssFeedNewOrders
    {
        public string OrderID { get; set; }
        public string AddedOn { get; set; }
        public string StoreName { get; set; }
        public string GrandTotal { get; set; }
        public string OrderStatus { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMethodName { get; set; }
        public List<ItemCommonInfo> OrderItemInfo { get; set; }
    }

    public class RssFeedNewCustomer
    {
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public string AddedOn { get; set; }
        public string Email { get; set; }
    }

    public class RssFeedNewItemReview:ItemCommonInfo
    {
        public string Status { get; set; }
        public string AddedOn { get; set; }
        public string UserName { get; set; }
        public int TotalRatingAverage { get; set; }
        public string ReviewSummary { get; set; }
    }

    public class RssFeedNewTag
    {
        public string TagName { get; set; }
        public string TagStatus { get; set; }
        public string AddedOn { get; set; }
        public string TagIDs { get; set; }
        public List<ItemCommonInfo> TagItemInfo { get; set; }
    }

    public class RssFeedLowStock:ItemCommonInfo
    {
        public int Quantity { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
    }

    public class RssFeedBrand:ItemCommonInfo
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public string BrandImageUrl { get; set; }
        public string AddedOn { get; set; }
    }
}
