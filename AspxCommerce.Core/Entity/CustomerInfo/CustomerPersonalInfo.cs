using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class CustomerPersonalInfo
    {
        private int _CustomerID;
        private string _UserName;
        private int _PortalID;

        //Extra properties
        private String _LastLoggedIn;
        //private bool _ConfirmedEmail;
        private String _AccountCreatedOn;
        private string _AccountCreatedIn;
        private AddressInfo _BillingAddress;
        private AddressInfo _ShippingAddress;
        private string _LifetimeSales;
        private string _AverageSales;
        
        public CustomerPersonalInfo()
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

        public String LastLoggedIn
        {
            get
            {
                return this._LastLoggedIn;
            }
            set
            {
                if (this._LastLoggedIn != value)
                {
                    this._LastLoggedIn = value;
                }
            }
        }
        
        public String AccountCreatedOn
        {
            get
            {
                return this._AccountCreatedOn;
            }
            set
            {
                if (this._AccountCreatedOn != value)
                {
                    this._AccountCreatedOn = value;
                }
            }
        }

        public string AccountCreatedIn
        {
            get
            {
                return this._AccountCreatedIn;
            }
            set
            {
                if (this._AccountCreatedIn != value)
                {
                    this._AccountCreatedIn = value;
                }
            }
        }

        public string LifetimeSales
        {
            get
            {
                return this._LifetimeSales;
            }
            set
            {
                if (this._LifetimeSales != value)
                {
                    this._LifetimeSales = value;
                }
            }
        }

        public string AverageSales
        {
            get
            {
                return this._AverageSales;
            }
            set
            {
                if (this._AverageSales != value)
                {
                    this._AverageSales = value;
                }
            }
        }

        public AddressInfo BillingAddress
        {
            get
            {
                return this._BillingAddress;
            }
            set
            {
                if (this._BillingAddress != value)
                {
                    this._BillingAddress = value;
                }
            }
        }

        public AddressInfo ShippingAddress
        {
            get
            {
                return this._ShippingAddress;
            }
            set
            {
                if (this._ShippingAddress != value)
                {
                    this._ShippingAddress = value;
                }
            }
        }
    }
}
