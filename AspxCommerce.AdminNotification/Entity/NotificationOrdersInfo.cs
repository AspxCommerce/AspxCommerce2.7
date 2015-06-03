using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.AdminNotification
{
    public class NotificationOrdersInfo
    {

        #region Private Fields
        private Nullable<int> _orderID;
        private string _orderStatusAliasName;
        private string _itemCostVariantID;
        private string _addedOn;
        private Nullable<bool> _isChecked;
        private Nullable<bool> _isCheckedFull;
       
        #endregion

        #region Public Fields

        public Nullable<int> OrderID
        {
            get { return this._orderID; }
            set
            {
                if ((this._orderID != value))
                {
                    this._orderID = value;
                }
            }
        }
        public string OrderStatusAliasName
        {
            get { return this._orderStatusAliasName; }
            set
            {
                if ((this._orderStatusAliasName != value))
                {
                    this._orderStatusAliasName = value;
                }
            }
        }
        public string ItemCostVariantID
        {
            get { return this._itemCostVariantID; }
            set
            {
                if ((this._itemCostVariantID != value))
                {
                    this._itemCostVariantID = value;
                }
            }
        }
        public string AddedOn
        {
            get { return this._addedOn; }
            set
            {
                if ((this._addedOn != value))
                {
                    this._addedOn = value;
                }
            }
        }
        public Nullable<bool> IsChecked
        {
            get { return this._isChecked; }
            set
            {
                if ((this._isChecked != value))
                {
                    this._isChecked = value;
                }
            }
        }
        public Nullable<bool> IsCheckedFull
        {
            get { return this._isCheckedFull; }
            set
            {
                if ((this._isCheckedFull != value))
                {
                    this._isCheckedFull = value;
                }
            }
        }
       

        #endregion
    }
}
