using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AspxCommerce.USPS
{
    [Serializable()]
    public class DestinationAddress
    {
        public DestinationAddress()
        {
        }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private string _company = "";
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        public string Contact { get; set; }
        public string ContactEmail { get; set; }
        public string ToCountry { get; set; }
        public string ToCountryName { get; set; }

        private string _address = "";
        public string ToAddress {
            get { return _address; }
            set { _address = value; }
        }
        private string _address2 = "";
        public string ToAddress2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string ToStreetAddress1 { get; set; }

        public string ToStreetAddress2 { get; set; }

        public string ToCity { get; set; }
        private string _toState = "";
        public string ToState
        {
            get { return _toState; }
            set { _toState = value; }
        }

        private string _toFax = "";
        public string ToFax
        {
            get { return _toFax; }
            set { _toFax = value; }
        }
        public string ToPostalCode { get; set; }
        private string _zipPlus4 = "";
        public string ZipPlus4
        {
            get { return _zipPlus4; }
            set { _zipPlus4 = value; }
        }
        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Phone { get; set; }

        public string Zip { get; set; }

    }

    public class OriginAddress
    {
        private string _firstName = "";
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        private string _lastName = "";
        public string LastName { 
            get { return _lastName; }
            set { _lastName = value; }
        }


        public string Email { get; set; }

        public string Company { get; set; }

        private string _address1 = "";

        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        private string _address2 = "";
        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
        private string _phone = "";
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private string _mobile = "";

        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        private string _fax="";
        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }

        public string Name { get; set; }

        public string CountryName { get; set; }
        private string _zipPlus4 = "";
      
        public string ZipPlus4
        {
            get { return _zipPlus4; }
            set { _zipPlus4 = value; }
        }
    }
}
