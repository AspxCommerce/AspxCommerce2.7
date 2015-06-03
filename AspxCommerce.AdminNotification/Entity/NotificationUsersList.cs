using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.AdminNotification
{
    public class NotificationUsersList
    {
        public List<UsersInfo> UsersDetails { get; set; }
        public List<SubscriptionInfo> SubscriptionDetails { get; set; }
    }
    [Serializable]
    [DataContract]
    public class UsersInfo
    {
        //CustomerID,UserName,AddedOn,IsChecked,IsCheckedFull
        private string _customerID;
        private string _userName;
        private string _addedOn;
        private bool _isChecked;
        private bool _isCheckedFull;

        [DataMember]
        public string CustomerID
        {
            get { return this._customerID; }
            set
            {
                if (_customerID != value)
                {
                    _customerID = value;
                }
            }
        }

        [DataMember]
        public string UserName
        {
            get { return this._userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                }
            }
        }
        [DataMember]
        public string AddedOn
        {
            get { return this._addedOn; }
            set
            {
                if (_addedOn != value)
                {
                    _addedOn = value;
                }
            }
        }
        [DataMember]
        public bool IsChecked
        {
            get { return this._isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                }
            }
        }
        [DataMember]
        public bool IsCheckedFull
        {
            get { return this._isCheckedFull; }
            set
            {
                if (_isCheckedFull != value)
                {
                    _isCheckedFull = value;
                }
            }
        }
        
    }
    public class SubscriptionInfo
    {
        //SubTabPath, Visits,VisitsPer,AverageDuration,TotalVisits
        private string _userName;        
        private string _subscriptionEmail;
        private string _addedOn;
        private bool _isChecked;
        private bool _isCheckedFull;
        private string _customerID;


        [DataMember]
        public string UserName
        {
            get { return this._userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                }
            }
        }
        [DataMember]
        public string SubscriptionEmail
        {
            get { return this._subscriptionEmail; }
            set
            {
                if (_subscriptionEmail != value)
                {
                    _subscriptionEmail = value;
                }
            }
        }
        [DataMember]
        public string AddedOn
        {
            get { return this._addedOn; }
            set
            {
                if (_addedOn != value)
                {
                    _addedOn = value;
                }
            }
        }
        [DataMember]
        public bool IsChecked
        {
            get { return this._isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                }
            }
        }
        [DataMember]
        public bool IsCheckedFull
        {
            get { return this._isCheckedFull; }
            set
            {
                if (_isCheckedFull != value)
                {
                    _isCheckedFull = value;
                }
            }
        }

        [DataMember]
        public string CustomerID
        {
            get { return this._customerID; }
            set
            {
                if (_customerID != value)
                {
                    _customerID = value;
                }
            }
        }

    }
}
