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
    public class StoreLocatorInfo
    {
        private int _locationID;
        private int _storeID;
        private string _storeName;
        private string _streetName;
        private string _storeDescription;
        private string _localityName;
        private string _city;
        private string _state;
        private string _country;
        private string _zip;
        private double _latitude;
        private double _longitude;
        private int _portalID;
        private string _addedBy;
        private double _distance;
        private string _branchImage;

        public int LocationID
        {
            get { return _locationID; }
            set { this._locationID = value; }
        }

        public int StoreID
        {
            get { return _storeID; }
            set { this._storeID = value; }
        }
        public string StoreName
        {
            get { return _storeName; }
            set { this._storeName = value; }
        }


        public string StreetName
        {
            get { return _streetName; }
            set { this._streetName = value; }
        }

        public string StoreDescription
        {
            get { return _storeDescription; }
            set { this._storeDescription = value; }
        }

        public string LocalityName
        {
            get { return _localityName; }
            set { this._localityName = value; }
        }

        public string City
        {
            get { return _city; }
            set { this._city = value; }
        }

        public string State
        {
            get { return _state; }
            set { this._state = value; }
        }

        public string Country
        {
            get { return _country; }
            set { this._country = value; }
        }

        public string ZIP
        {
            get { return _zip; }
            set { this._zip = value; }
        }

        public double Latitude
        {
            get { return _latitude; }
            set { this._latitude = value; }
        }
        public double Longitude
        {
            get { return _longitude; }
            set { this._longitude = value; }
        }

        public string AddedBy
        {
            get { return _addedBy; }
            set { this._addedBy = value; }
        }

        public int PortalID
        {
            get { return _portalID; }
            set { this._portalID = value; }
        }
        public double Distance
        {
            get { return _distance; }
            set { this._distance = value; }
        }

        public string BranchImage
        {
            get { return _branchImage; }
            set { this._branchImage = value; }
        }


    }
}
