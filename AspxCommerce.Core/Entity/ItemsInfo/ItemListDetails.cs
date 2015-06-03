using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class ItemListDetails
    {
        private string _weightUnit;
        private string _dimensionUnit;
        private List<BasketItem> _basketItems;
        private bool _isSingleCheckOut;
        private ShipToAddress _shipToAddress;
        private WareHouseAddress _wareHouseAddress;
        
        public string WeightUnit
        {
            get
            {
                return this._weightUnit;
            }
            set
            {
                this._weightUnit = value;
            }

        }
        public string DimensionUnit
        {
            get
            {
                return this._dimensionUnit;
            }
            set
            {
                this._dimensionUnit = value;
            }

        }
        public List<BasketItem> BasketItems
        {
            get
            {
                return this._basketItems;
            }
            set
            {
                this._basketItems = value;
            }

        }
        public bool IsSingleCheckOut
        {
            get
            {
                return this._isSingleCheckOut;
            }
            set
            {
                this._isSingleCheckOut = value;
            }

        }
        public ShipToAddress ShipToAddress
        {
            get
            {
                return this._shipToAddress;
            }
            set
            {
                this._shipToAddress = value;
            }

        }
        public WareHouseAddress WareHouseAddress
        {
            get
            {
                return this._wareHouseAddress;
            }
            set
            {
                this._wareHouseAddress = value;
            }

       }

        private Info _commonInfo;
        public Info CommonInfo
        {
            get
            {
                return this._commonInfo;
            }
            set
            {
                this._commonInfo = value;
            }
   
       }
    }
    public class BasketItem
    {
        private string _itemName;
        private string _height;
        private string _length;
        private string _width;
        private string _serviceCode;
        private string _packagingTypeCode;
        private decimal _weightValue;
        private int _quantity;
        private WareHouseAddress _shipToAddress;

        public string ItemName
        {
            get
            {
                return this._itemName;
            }
            set
            {
                this._itemName = value;
            }

        }
        public string Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }

        }
        public string Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }

        }
        public string Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }

        }

        public string ServiceCode
        {
            get
            {
                return this._serviceCode;
            }
            set
            {
                this._serviceCode = value;
            }

        }
        public string PackagingTypeCode
        {
            get
            {
                return this._packagingTypeCode;
            }
            set
            {
                this._packagingTypeCode = value;
            }

        }
        public decimal WeightValue
        {
            get
            {
                return this._weightValue;
            }
            set
            {
                this._weightValue = value;
            }

        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }

        }

        public WareHouseAddress ShipToAddress
        {
            get
            {
                return this._shipToAddress;
            }
            set
            {
                this._shipToAddress = value;
            }

        }
    }

    public class ShipToAddress
    {


        private string _toaddress;

        public string ToAddress
        {
            get
            {
                return this._toaddress;
            }
            set
            {
                this._toaddress = value;

            }
        }

        private string _tostreetAddress1;

        public string ToStreetAddress1
        {
            get
            {
                return this._tostreetAddress1;
            }
            set
            {
                this._tostreetAddress1 = value;

            }
        }
        private string _tostreetAddress2;
        public string ToStreetAddress2
        {
            get
            {
                return this._tostreetAddress2;
            }
            set
            {
                this._tostreetAddress2 = value;

            }
        }
        private string _tocity;

        public string ToCity
        {
            get
            {
                return this._tocity;
            }
            set
            {
                this._tocity = value;

            }
        }

        private string _tostate;

        public string ToState
        {
            get
            {
                return this._tostate;
            }
            set
            {
                this._tostate = value;

            }
        }

        private string _topostalCode;
        public string ToPostalCode
        {
            get
            {
                return this._topostalCode;
            }
            set
            {
                this._topostalCode = value;

            }
        }

        private string _tocountry;
        public string ToCountry
        {
            get
            {
                return this._tocountry;
            }
            set
            {
                this._tocountry = value;

            }
        }

        private string _tocountryName;
        public string ToCountryName
        {
            get
            {
                return this._tocountryName;
            }
            set
            {
                this._tocountryName = value;

            }
        }
  
    }

    public class Info
    {
        #region private members

        private int _portalID;
        private int _storeID;
        private string _cultureName;
        private string _userName;
        private int _customerID;
        private string _sessionCode;

        #endregion

        #region public members

        public int PortalID
        {
            get
            {
                return this._portalID;
            }
            set
            {
                if ((this._portalID != value))
                {
                    this._portalID = value;
                }
            }
        }

        public int StoreID
        {
            get
            {
                return this._storeID;
            }
            set
            {
                if ((this._storeID != value))
                {
                    this._storeID = value;
                }
            }
        }

        public string CultureName
        {
            get
            {
                return this._cultureName;
            }
            set
            {
                if ((this._cultureName != value))
                {
                    this._cultureName = value;
                }
            }
        }

        public string UserName
        {
            get
            {
                return this._userName;
            }
            set
            {
                if ((this._userName != value))
                {
                    this._userName = value;
                }
            }
        }
        public string SessionCode
        {
            get
            {
                return this._sessionCode;
            }
            set
            {
                if ((this._sessionCode != value))
                {
                    this._sessionCode = value;
                }
            }
        }
        public int CustomerID
        {
            get
            {
                return this._customerID;
            }
            set
            {
                if ((this._customerID != value))
                {
                    this._customerID = value;
                }
            }
        }

       

        #endregion
    }
}
