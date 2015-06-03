using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class Notification
    {
        [DataMember(Name = "_RowTotal", Order = 0)]
        private System.Nullable<int> _RowTotal;

        [DataMember(Name = "_notificationID", Order = 1)]
        private int _notificationID;

        [DataMember(Name = "_itemID", Order = 2)]
        private int _itemID;

        [DataMember(Name = "_variantValueID", Order = 3)]
        private string _variantValueID;

        [DataMember(Name = "_itemSKU", Order = 4)]
        private string _itemSKU;

        [DataMember(Name = "_customer", Order = 5)]
        private string _customer;

        [DataMember(Name = "_email", Order = 6)]
        private string _email;

        [DataMember(Name = "_mailStatus", Order = 7)]
        private bool _mailStatus;

        [DataMember(Name = "_isActive", Order = 8)]
        private System.Nullable<bool> _isActive;


        [DataMember(Name = "_addedOn", Order = 9)]
        private DateTime _addedOn;


        [DataMember(Name = "_itemStatus", Order = 11)]
        private bool _itemStatus;

        public System.Nullable<int> RowTotal
        {
            get
            {
                return this._RowTotal;
            }
            set
            {
                if ((this._RowTotal != value))
                {
                    this._RowTotal = value;
                }
            }
        }

        public int NotificationID
        {
            get
            {
                return this._notificationID;

            }
            set
            {
                if (this._notificationID != value)
                {
                    _notificationID = value;
                }

            }


        }

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
                    _itemID = value;
                }

            }


        }

        public string VariantValueID
        {
            get
            {
                if (_variantValueID == "")
                { _variantValueID = "N/A"; }
                return this._variantValueID;
            }
            set
            {
                if (this._variantValueID != value)
                {
                    _variantValueID = value;
                }
            }

        }

        public string ItemSKU
        {
            get
            {
                return _itemSKU;
            }
            set
            {
                if (this._itemSKU != value)
                {
                    _itemSKU = value;
                }
            }

        }

        public string Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                if (this._customer != value)
                {
                    _customer = value;
                }
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (this._email != value)
                {
                    _email = value;
                }
            }
        }

        public bool MailStatus
        {


            get
            {
                return _mailStatus;
            }
            set
            {
                if (this._mailStatus != value)
                {
                    _mailStatus = value;
                }
            }
        }

        public System.Nullable<bool> IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if ((this._isActive != value))
                {
                    this._isActive = value;
                }
            }
        }

        public DateTime AddedOn
        {
            get
            {
                return this._addedOn;
            }
            set
            {
                if ((this._addedOn != value))
                {
                    this._addedOn = value;
                }
            }
        }

        public bool ItemStatus
        {
            get
            {
                return this._itemStatus;
            }
            set
            {
                if ((this._itemStatus != value))
                {
                    this._itemStatus = value;
                }
            }
        }

    }
}
