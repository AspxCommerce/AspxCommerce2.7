using System;

namespace AspxCommerce.AdminNotification
{
    public class NotificationGetAllInfo
    {


        #region Private Fields
        
        private Nullable<int> _usersInfoCount;
        private Nullable<int> _itemsInfoCount;
        private Nullable<int> _newOrdersCount;
       
        #endregion

        #region Public Fields

        public Nullable<int> UsersInfoCount
        {
            get { return this._usersInfoCount; }
            set
            {
                if ((this._usersInfoCount != value))
                {
                    this._usersInfoCount = value;
                }
            }
        }
        public Nullable<int> ItemsInfoCount
        {
            get { return this._itemsInfoCount; }
            set
            {
                if ((this._itemsInfoCount != value))
                {
                    this._itemsInfoCount = value;
                }
            }
        }
        public Nullable<int> NewOrdersCount
        {
            get { return this._newOrdersCount; }
            set
            {
                if ((this._newOrdersCount != value))
                {
                    this._newOrdersCount = value;
                }
            }
        }


        #endregion
    }
}
