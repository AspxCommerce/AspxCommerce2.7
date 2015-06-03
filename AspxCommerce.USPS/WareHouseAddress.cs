using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.USPS
{
    //public class WareHouseAddress
    //{

    //    private string _cityField;
    //    private string _countryCodeField;
    //    private string _postalCodeField;
    //    private bool _residentialField;
    //    private bool _residentialFieldSpecified;
    //    private string _stateOrProvinceCodeField;
    //    private string[] _streetLinesField;
    //    private string _urbanizationCodeField;
    //    private string _address1 = "";
    //  public string Address1
    //    {
    //        get { return _address1; }
    //        set
    //        {
    //            _address1 = value;
    //        }
    //   }

    //    private string _address2 = "";
      
    //    public string Address2
    //    {
    //        get { return _address2; }
    //        set { _address2 = value; }
    //    }

    //    public string City
    //    {
    //        get
    //        {
    //            return this._cityField;
    //        }
    //        set
    //        {
    //            this._cityField = value;
    //        }
    //    }

    //    public string CountryCode
    //    {
    //        get
    //        {
    //            return this._countryCodeField;
    //        }
    //        set
    //        {
    //            this._countryCodeField = value;
    //        }
    //    }

    //    public string PostalCode
    //    {
    //        get
    //        {
    //            return this._postalCodeField;
    //        }
    //        set
    //        {
    //            this._postalCodeField = value;
    //        }
    //    }

    //    public bool Residential
    //    {
    //        get
    //        {
    //            return this._residentialField;
    //        }
    //        set
    //        {
    //            this._residentialField = value;
    //        }
    //    }


    //    public bool ResidentialSpecified
    //    {
    //        get
    //        {
    //            return this._residentialFieldSpecified;
    //        }
    //        set
    //        {
    //            this._residentialFieldSpecified = value;
    //        }
    //    }

    //    public string StateOrProvinceCode
    //    {
    //        get
    //        {
    //            return this._stateOrProvinceCodeField;
    //        }
    //        set
    //        {
    //            this._stateOrProvinceCodeField = value;
    //        }
    //    }


    //    public string[] StreetLines
    //    {
    //        get
    //        {
    //            return this._streetLinesField;
    //        }
    //        set
    //        {
    //            this._streetLinesField = value;
    //        }
    //    }

    //    public string UrbanizationCode
    //    {
    //        get
    //        {
    //            return this._urbanizationCodeField;
    //        }
    //        set
    //        {
    //            this._urbanizationCodeField = value;
    //        }
    //    }

    //}
    [DataContract]
    [Serializable]
    public class WareHouseAddress
    {

        [DataMember(Name = "_wareHouseId", Order = 1)]
        private int _wareHouseId;
        public int WareHouseID
        {
            get
            {
                return this._wareHouseId;
            }
            set
            {
                this._wareHouseId = value;

            }
        }
        [DataMember(Name = "_name", Order = 2)]
        private string _name;

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;

            }
        }
        [DataMember(Name = "_address", Order = 3)]
        private string _address;

        public string Address
        {
            get
            {
                return this._address;
            }
            set
            {
                this._address = value;

            }
        }
        [DataMember(Name = "_isPrimary", Order = 4)]
        private bool? _isPrimary;

        public bool? IsPrimary
        {
            get
            {
                return this._isPrimary;
            }
            set
            {
                this._isPrimary = value;

            }
        }
        [DataMember(Name = "_streetAddress1", Order = 5)]
        private string _streetAddress1;

        public string StreetAddress1
        {
            get
            {
                return this._streetAddress1;
            }
            set
            {
                this._streetAddress1 = value;

            }
        }
        [DataMember(Name = "_streetAddress2", Order = 6)]
        private string _streetAddress2;

        public string StreetAddress2
        {
            get
            {
                return this._streetAddress2;
            }
            set
            {
                this._streetAddress2 = value;

            }
        }
        [DataMember(Name = "_city", Order = 6)]
        private string _city;

        public string City
        {
            get
            {
                return this._city;
            }
            set
            {
                this._city = value;

            }
        }
        [DataMember(Name = "_state", Order = 7)]
        private string _state;

        public string State
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;

            }
        }
        [DataMember(Name = "_postalCode", Order = 8)]
        private string _postalCode;

        public string PostalCode
        {
            get
            {
                return this._postalCode;
            }
            set
            {
                this._postalCode = value;

            }
        }
        [DataMember(Name = "_country", Order = 9)]
        private string _country;

        public string Country
        {
            get
            {
                return this._country;
            }
            set
            {
                this._country = value;

            }
        }
        [DataMember(Name = "_phone", Order = 10)]
        private string _phone;

        public string Phone
        {
            get
            {
                return this._phone;
            }
            set
            {
                this._phone = value;

            }
        }
        [DataMember(Name = "_fax", Order = 11)]
        private string _fax;

        public string Fax
        {
            get
            {
                return this._fax;
            }
            set
            {
                this._fax = value;

            }
        }
        [DataMember(Name = "_email", Order = 12)]
        private string _email;

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;

            }
        }
        //[DataMember]
        //public int StoreID { get; set; }
        //[DataMember]
        //public int PortalID { get }



    }
}
