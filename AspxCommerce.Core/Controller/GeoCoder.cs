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
using System.Web;
using System.Net;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    /// <summary>
    /// Summary description for GeoCoder
    /// </summary>
    [Serializable]
    [DataContract]
    public class GeoCoder
    {
        public GeoCoder()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        [Serializable]
        [DataContract]
        public class Coordinate
        {
            private decimal _latitude;
            private decimal _longitude;

            public Coordinate(decimal latitude, decimal longitude)
            {
                _latitude = latitude;
                _longitude = longitude;
            }

            [DataMember]
            public decimal Latitude
            {
                get { return _latitude; }
                set { this._latitude = value; }
            }

            [DataMember]
            public decimal Longitude
            {
                get { return _longitude; }
                set { this._longitude = value; }
            }

            public Coordinate()
            {
            }
        }


        public class Geocode
        {
            private const string GoogleUri = "http://maps.google.com/maps/geo?q=";
            private const string OutputType = "csv"; // Available options: csv, xml, kml, json

            private static Uri GetGeocodeUri(string address, string googleApiKey)
            {
                address = HttpUtility.UrlEncode(address);
                return new Uri(String.Format("{0}{1}&output={2}&key={3}", GoogleUri, address, OutputType, googleApiKey));
            }

            public static Coordinate GetCoordinates(string address, string googleApiKey)
            {
                Coordinate objCoordinate;
                WebClient client = new WebClient();
                Uri uri = GetGeocodeUri(address, googleApiKey);

                /*  The first number is the status code, 
                 * the second is the accuracy, 
                 * the third is the latitude, 
                 * the fourth one is the longitude.
                 */

                string[] geocodeInfo = client.DownloadString(uri).Split(',');

                if (Convert.ToDecimal(geocodeInfo[2]) > 0)
                {
                    objCoordinate = new Coordinate(Convert.ToDecimal(geocodeInfo[2]), Convert.ToDecimal(geocodeInfo[3]));
                }
                else
                {
                    objCoordinate = new Coordinate(Convert.ToDecimal(0), Convert.ToDecimal(0));
                }

                return objCoordinate;
            }
        }
    }
}