using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class CustomerGeneralInfo
    {
        private int _CustomerID;
        private string _UserName;
        private int _PortalID;
        private int _StoreID;
        private DateTime _AddedOn;

        public CustomerGeneralInfo()
        {
        }

        public int CustomerID
        {
            get
            {
                return this._CustomerID;
            }
            set
            {
                if (this._CustomerID != value)
                {
                    this._CustomerID = value;
                }
            }
        }

        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if (this._UserName != value)
                {
                    this._UserName = value;
                }
            }
        }

        public int PortalID
        {
            get
            {
                return this._PortalID;
            }
            set
            {
                if (this._PortalID != value)
                {
                    this._PortalID = value;
                }
            }
        }

        public int StoreID
        {
            get
            {
                return this._StoreID;
            }
            set
            {
                if (this._StoreID != value)
                {
                    this._StoreID = value;
                }
            }
        }

        public DateTime AddedOn
        {
            get
            {
                return this._AddedOn;
            }
            set
            {
                if (this._AddedOn != value)
                {
                    this._AddedOn = value;
                }
            }
        }
    }
}
