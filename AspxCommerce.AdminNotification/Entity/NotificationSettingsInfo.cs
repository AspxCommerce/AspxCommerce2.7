using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace AspxCommerce.AdminNotification
{
    public class NotificationSettingsInfo
    {
        #region Private Fields
        private Nullable<bool> _allActive;
        private Nullable<bool> _userNotificationActive;
        private Nullable<int> _userNotificationCount;
        private Nullable<bool> _subscriptionNotificationActive;
        private Nullable<int> _subscriptionNotificationCount;
        private Nullable<bool> _outofStockNotificationActive;
        private Nullable<int> _outofStockNotificationCount;
        private Nullable<bool> _itemsLowStockNotificationActive;
        private Nullable<int> _itemsLowStockCount;
        private Nullable<bool> _ordersNotificationAtive;
        private Nullable<int> _ordersNotificationCount;
        #endregion

        #region Public Fields

        public Nullable<bool> AllActive
        {
            get { return this._allActive; }
            set
            {
                if ((this._allActive != value))
                {
                    this._allActive = value;
                }
            }
        }
        public Nullable<bool> UserNotificationActive
        {
            get { return this._userNotificationActive; }
            set
            {
                if ((this._userNotificationActive != value))
                {
                    this._userNotificationActive = value;
                }
            }
        }
        public Nullable<int> UserNotificationCount
        {
            get { return this._userNotificationCount; }
            set
            {
                if ((this._userNotificationCount != value))
                {
                    this._userNotificationCount = value;
                }
            }
        }
        public Nullable<bool> SubscriptionNotificationActive
        {
            get { return this._subscriptionNotificationActive; }
            set
            {
                if ((this._subscriptionNotificationActive != value))
                {
                    this._subscriptionNotificationActive = value;
                }
            }
        }
        public Nullable<int> SubscriptionNotificationCount
        {
            get { return this._subscriptionNotificationCount; }
            set
            {
                if ((this._subscriptionNotificationCount != value))
                {
                    this._subscriptionNotificationCount = value;
                }
            }
        }
        public Nullable<bool> OutofStockNotificationActive
        {
            get { return this._outofStockNotificationActive; }
            set
            {
                if ((this._outofStockNotificationActive != value))
                {
                    this._outofStockNotificationActive = value;
                }
            }
        }
        public Nullable<int> OutofStockNotificationCount
        {
            get { return this._outofStockNotificationCount; }
            set
            {
                if ((this._outofStockNotificationCount != value))
                {
                    this._outofStockNotificationCount = value;
                }
            }
        }
        public Nullable<bool> ItemsLowStockNotificationActive
        {
            get { return this._itemsLowStockNotificationActive; }
            set
            {
                if ((this._itemsLowStockNotificationActive != value))
                {
                    this._itemsLowStockNotificationActive = value;
                }
            }
        }

        public Nullable<int> ItemsLowStockCount
        {
            get { return this._itemsLowStockCount; }
            set
            {
                if ((this._itemsLowStockCount != value))
                {
                    this._itemsLowStockCount = value;
                }
            }
        }

        public Nullable<bool> OrdersNotificationAtive
        {
            get { return this._ordersNotificationAtive; }
            set
            {
                if ((this._ordersNotificationAtive != value))
                {
                    this._ordersNotificationAtive = value;
                }
            }
        }
        public Nullable<int> OrdersNotificationCount
        {
            get { return this._ordersNotificationCount; }
            set
            {
                if ((this._ordersNotificationCount != value))
                {
                    this._ordersNotificationCount = value;
                }
            }
        }

        #endregion
    }
}
