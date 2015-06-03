using System;
using System.Collections.Generic;
using System.Text;

namespace AspxCommerce.USPS
{
    public class USPSRateResponse
    {
        public USPSRateResponse()
        {
            _Postage = new List<Postage>();
        }

        private bool _isDomestic;
        public bool IsDomestic
        {

            get { return _isDomestic; }
            set { _isDomestic = value; }
        }
        private string _originZip = "";

        public string OriginZip
        {
            get { return _originZip; }
            set { _originZip = value; }
        }

        private string _destZip = "";

        public string DestZip
        {
            get { return _destZip; }
            set { _destZip = value; }
        }

        private decimal _pounds = 0M;

        public decimal Pounds
        {
            get { return _pounds; }
            set { _pounds = value; }
        }

        private decimal _ounces = 0M;

        public decimal Ounces
        {
            get { return _ounces; }
            set { _ounces = value; }
        }

        private string _container = "";

        public string Container
        {
            get { return _container; }
            set { _container = value; }
        }

        private string _size = "";

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }

        private string _zone = "";

        public string Zone
        {
            get { return _zone; }
            set { _zone = value; }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        private List<InterNationalRateList> _intRateList;
        public  List<InterNationalRateList> IntRateList
        {
            get { return _intRateList; }
            set { _intRateList = value; }
        }

        private List<Postage> _Postage;

        public List<Postage> Postage
        {
            get { return _Postage; }
            set { _Postage = value; }
        }

        public static USPSRateResponse FromXml(string xml)
        {
            USPSRateResponse r = new USPSRateResponse();
            int idx1 = 0;
            int idx2 = 0;

            if (xml.Contains("<ZipOrigination>"))
            {
                idx1 = xml.IndexOf("<ZipOrigination>") + 17;
                idx2 = xml.IndexOf("</ZipOrigination>");
                r.OriginZip = xml.Substring(idx1, idx2 - idx1);
            }

            if (xml.Contains("<ZipDestination>"))
            {
                idx1 = xml.IndexOf("<ZipDestination>") + 16;
                idx2 = xml.IndexOf("</ZipDestination>");
                r.OriginZip = xml.Substring(idx1, idx2 - idx1);
            }

            if (xml.Contains("<Pounds>"))
            {
                idx1 = xml.IndexOf("<Pounds>") + 8;
                idx2 = xml.IndexOf("</Pounds>");
                r.OriginZip = xml.Substring(idx1, idx2 - idx1);
            }

            if (xml.Contains("<Ounces>"))
            {
                idx1 = xml.IndexOf("<Ounces>") + 8;
                idx2 = xml.IndexOf("</Ounces>");
                r.OriginZip = xml.Substring(idx1, idx2 - idx1);
            }

            if (xml.Contains("<Container>"))
            {
                idx1 = xml.IndexOf("<Container>") + 11;
                idx2 = xml.IndexOf("</Container>");
                r.OriginZip = xml.Substring(idx1, idx2 - idx1);
            }

            if (xml.Contains("<Size>"))
            {
                idx1 = xml.IndexOf("<Size>") + 6;
                idx2 = xml.IndexOf("</Size>");
                r.OriginZip = xml.Substring(idx1, idx2 - idx1);
            }

            if (xml.Contains("<Zone>"))
            {
                idx1 = xml.IndexOf("<Zone>") + 6;
                idx2 = xml.IndexOf("</Zone>");
                r.OriginZip = xml.Substring(idx1, idx2 - idx1);
            }

            int lastidx = 0;
            while (xml.IndexOf("<MailService>", lastidx) > -1)
            {
                Postage p = new Postage();
                idx1 = xml.IndexOf("<MailService>", lastidx) + 13;
                idx2 = xml.IndexOf("</MailService>", lastidx + 13);
                p.ShippingMethodName = xml.Substring(idx1, idx2 - idx1);

                idx1 = xml.IndexOf("<Rate>", lastidx) + 6;
                idx2 = xml.IndexOf("</Rate>", lastidx + 13);
                p.TotalCharges = Decimal.Parse(xml.Substring(idx1, idx2 - idx1));
                r.Postage.Add(p);
                lastidx = idx2;
            }
            return r;
        }

        private string _prohibitions;
        public string Prohibitions
        {
            get { return _prohibitions; }
            set { _prohibitions = value; }
        }

        private string _restrictions;
        public string Restrictions
        {
            get { return _restrictions; }
            set { _restrictions = value; }
        }
        private string _observations;
        public string Observations
        {
            get { return _observations; }
            set { _observations = value; }
        }
        private string _customsForms;
        public string CustomsForms
        {
            get { return _customsForms; }
            set { _customsForms = value; }
        }
        private string _expressMail;
        public string ExpressMail
        {
            get { return _expressMail; }
            set { _expressMail = value; }
        }
        private string _areasServed;
        public string AreasServed
        {
            get { return _areasServed; }
            set { _areasServed = value; }
        }
        private string _additionalRestrictions;
        public string AdditionalRestrictions
        {
            get { return _additionalRestrictions; }
            set { _additionalRestrictions = value; }
        }
    }

    public class Postage
    {
        private string _shippingMethodName;

        public string ShippingMethodName
        {
            get { return _shippingMethodName; }
            set { _shippingMethodName = value; }
        }

        private decimal _totalCharges;

        public decimal TotalCharges
        {
            get { return _totalCharges; }
            set { _totalCharges = value; }
        }
    };

    public class InterNationalRateList
    {
       
        private decimal _pounds = 0M;

        public decimal Pounds
        {
            get { return _pounds; }
            set { _pounds = value; }
        }
        private string _container = "";

        public string Container
        {
            get { return _container; }
            set { _container = value; }
        }

        private string _mailType;

        public string MailType
        {
            get { return _mailType; }
            set { _mailType = value; }
        }

        private string _size;
        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }
        private string _width;
        public string Width
        {
            get { return _width; }
            set { _width = value; }
        }
        private string _height;
        public string Height
        {
            get { return _height; }
            set { _height = value; }
        }
        private string _length;
        public string Length
        {
            get { return _length; }
            set { _length = value; }
        }
        private string _girth;
        public string Girth
        {
            get { return _girth; }
            set { _girth = value; }
        }
        private string _country;
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        private decimal _totalCharges;
        public decimal TotalCharges
        {
            get { return _totalCharges; }
            set { _totalCharges = value; }
        }
        private string _deliveryTime;
        public string DeliveryTime
        {
            get { return _deliveryTime; }
            set { _deliveryTime = value; }
        }
        private string _shippingMethodName;
        public string ShippingMethodName
        {
            get { return _shippingMethodName; }
            set { _shippingMethodName = value; }
        }
        private string _maxDimensions;
        public string MaxDimensions
        {
            get { return _maxDimensions; }
            set { _maxDimensions = value; }
        }


    }
}
