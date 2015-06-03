/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [Serializable]
    public class AddressInfo
    {
        [DataMember]
        private int _addressID;

        [DataMember]
        private string _firstName;

        [DataMember]
        private string _lastName;

        [DataMember]
        private string _email;

        [DataMember]
        private string _company;

        [DataMember]
        private string _address1;

        [DataMember]
        private string _address2;

        [DataMember]
        private string _city;

        [DataMember]
        private string _state;

        [DataMember]
        private string _zip;

        [DataMember]
        private string _country;

        [DataMember]
        private string _phone;

        [DataMember]
        private string _mobile;

        [DataMember]
        private string _fax;

        [DataMember]
        private string _website;

        [DataMember]
        private System.Nullable<bool> _defaultBilling;

        [DataMember]
        private System.Nullable<bool> _defaultShipping;

        public AddressInfo()
        {
        }

        public int AddressID
        {
            get
            {
                return this._addressID;
            }
            set
            {
                if ((this._addressID != value))
                {
                    this._addressID = value;
                }
            }
        }

        public string FirstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                if ((this._firstName != value))
                {
                    this._firstName = value;
                }
            }
        }

        public string LastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                if ((this._lastName != value))
                {
                    this._lastName = value;
                }
            }
        }

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                if ((this._email != value))
                {
                    this._email = value;
                }
            }
        }

        public string Company
        {
            get
            {
                return this._company;
            }
            set
            {
                if ((this._company != value))
                {
                    this._company = value;
                }
            }
        }

        public string Address1
        {
            get
            {
                return this._address1;
            }
            set
            {
                if ((this._address1 != value))
                {
                    this._address1 = value;
                }
            }
        }

        public string Address2
        {
            get
            {
                return this._address2;
            }
            set
            {
                if ((this._address2 != value))
                {
                    this._address2 = value;
                }
            }
        }

        public string City
        {
            get
            {
                return this._city;
            }
            set
            {
                if ((this._city != value))
                {
                    this._city = value;
                }
            }
        }

        public string State
        {
            get
            {
                return this._state;
            }
            set
            {
                if ((this._state != value))
                {
                    this._state = value;
                }
            }
        }

        public string Zip
        {
            get
            {
                return this._zip;
            }
            set
            {
                if ((this._zip != value))
                {
                    this._zip = value;
                }
            }
        }

        public string Country
        {
            get
            {
                return this._country;
            }
            set
            {
                if ((this._country != value))
                {
                    this._country = value;
                }
            }
        }

        public string Phone
        {
            get
            {
                return this._phone;
            }
            set
            {
                if ((this._phone != value))
                {
                    this._phone = value;
                }
            }
        }

        public string Mobile
        {
            get
            {
                return this._mobile;
            }
            set
            {
                if ((this._mobile != value))
                {
                    this._mobile = value;
                }
            }
        }

        public string Fax
        {
            get
            {
                return this._fax;
            }
            set
            {
                if ((this._fax != value))
                {
                    this._fax = value;
                }
            }
        }

        public string Website
        {
            get
            {
                return this._website;
            }
            set
            {
                if ((this._website != value))
                {
                    this._website = value;
                }
            }
        }

        public System.Nullable<bool> DefaultBilling
        {
            get
            {
                return this._defaultBilling;
            }
            set
            {
                if ((this._defaultBilling != value))
                {
                    this._defaultBilling = value;
                }
            }
        }

        public System.Nullable<bool> DefaultShipping
        {
            get
            {
                return this._defaultShipping;
            }
            set
            {
                if ((this._defaultShipping != value))
                {
                    this._defaultShipping = value;
                }
            }
        }
    }

    public class AddressBasicInfo : AddressInfo
    {
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public string CostVariantIDs { get; set; }

    }
}


