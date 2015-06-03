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




namespace AspxCommerce.Core
{
    public class UserAddressInfo
    {
        #region private members

        private string _addressID;
        private string _firstName;
        private string _lastName;
        private string _companyName;
        private string _emailaddress;
        private string _address;
        private string _address2;
        private string _city;
        private string _state;
        private string _zip;
        private string _country;
        private string _phone;
        private string _mobile;
        private string _fax;
        private string _website;
        private bool _isDefaultBilling;
        private bool _isDefaultShipping;
        private bool _isBillingAsShipping;
        private string _userName;
      
        #endregion

        #region PUBLIC MEMBERS

        public string AddressID
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

        public string CompanyName
        {
            get
            {
                return this._companyName;
            }
            set
            {
                if ((this._companyName != value))
                {
                    this._companyName = value;
                }
            }
        }

        public string EmailAddress
        {
            get
            {
                return this._emailaddress;
            }
            set
            {
                if ((this._emailaddress != value))
                {
                    this._emailaddress = value;
                }
            }
        }

        public string Address
        {
            get
            {
                return this._address;
            }
            set
            {
                if ((this._address != value))
                {
                    this._address = value;
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
        public string WebSite
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


        public bool IsDefaultBilling
        {
            get
            {
                return this._isDefaultBilling;
            }
            set
            {
                if ((this._isDefaultBilling != value))
                {
                    this._isDefaultBilling = value;
                }
            }
        }

        public bool IsDefaultShipping
        {
            get
            {
                return this._isDefaultShipping;
            }
            set
            {
                if ((this._isDefaultShipping != value))
                {
                    this._isDefaultShipping = value;
                }
            }
        }

        public bool IsBillingAsShipping
        {
            get
            {
                return this._isBillingAsShipping;
            }
            set
            {
                if ((this._isBillingAsShipping != value))
                {
                    this._isBillingAsShipping = value;
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
       
        #endregion
    }
}
