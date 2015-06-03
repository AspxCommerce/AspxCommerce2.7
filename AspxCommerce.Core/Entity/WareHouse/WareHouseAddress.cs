using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
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
