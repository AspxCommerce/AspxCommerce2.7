using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class UrlInfo
    {
        private string baseUrl;
        private Home home;
        private MyAccount myAccount;
        private Item item;
        private string login;
        private string storeSettings;
        private string advanceSearch;
        private string registerUser;

        public UrlInfo()
        {
        }
        public string BaseUrl
        {
            get { return baseUrl; }
            set { baseUrl = value; }
        }

        public string Login
        {
           get { return login; }
           set { login = value; }
        }

        public string RegisterUser
        {
            get { return registerUser; }
            set { registerUser = value; }
        }

        public string StoreSettings
        {
            get { return storeSettings; }
            set { storeSettings = value; }
        }

        public string AdvanceSearch
        {
            get { return advanceSearch; }
            set { advanceSearch = value; }
        }

        public Home Home
        {
            get { return home; }
            set { home = value; }
        }

        public MyAccount MyAccount
        {
            get { return myAccount; }
            set { myAccount = value; }
        }

        public Item Item
        {
            get { return item; }
            set { item = value; }
        }

    }

    public class Home
    {
        public string RecentlyAddedItems { get; set; }
        public string SpecialItems { get; set; }
        public string BestSellerItems { get; set; }
        public string FeaturedItems { get; set; }
        public string PopularTags { get; set; }
        public string ShoppingOption { get; set; }
        public string CompareItems { get; set; }
    }

    public class MyAccount
    {
        public string AccountDashboard { get; set; }
        public string AccountInformation { get; set; }
        public string ChangePassword { get; set; }
        public string AddressBook { get; set; }
        public string MyOrders { get; set; }
        public string MyWishList { get; set; }
        public string SharedWishList { get; set; }
        public string MyDigitalItems { get; set; }
        public string ReferredFriends { get; set; }
        public string RecentHistory { get; set; }
    }
    public class Item
    {
        public BestSellerItems BestSellerItems { get; set; }
        public RecentlyAddedItems RecentlyAddedItems { get; set; }
        public SpecialItems SpecialItems { get; set; }
        public PopularTags PopularTags { get; set; }
        public ShoppingOption ShoppingOption { get; set; }
        public FeaturedItem FeaturedItem { get; set; }
        public CompareItems CompareItems { get; set; }
    }
	
    public class BestSellerItems
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Count { get; set; }
        public string State { get; set; }
    }

    public class RecentlyAddedItems
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Count { get; set; }
        public string RowCount { get; set; }
        public string State { get; set; }
    }

    public class SpecialItems
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Count { get; set; }
        public string State { get; set; }
    }

    public class PopularTags
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Count { get; set; }

    }

    public class ShoppingOption
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Range { get; set; }
    }

    public class FeaturedItem
    {
        public string Title { get; set; }
        public string URL { get; set; }
    }

    public class CompareItems
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Count { get; set; }
        public string State { get; set; }
    }
}
