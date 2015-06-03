using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.USPS
{
    public class USPSShipmentResponse
    {
        public string Error { get; set; }
        public bool IsFailed { get; set; }

        private bool _isDomestic;
        public bool IsDomestic
        {

            get { return _isDomestic; }
            set { _isDomestic = value; }
        }
        private string _DeliveryConfirmationNumber;
        public string DeliveryConfirmationNumber
        {
            get { return _DeliveryConfirmationNumber; }
            set { _DeliveryConfirmationNumber = value; }
        }
        private string _Postnet;
        public string Postnet
        {
            get { return _Postnet; }
            set { _Postnet = value; }
        }
        private string _ToState;
        public string ToState
        {
            get { return _ToState; }
            set { _ToState = value; }
        }
        private string _ToZip5;
        public string ToZip5
        {
            get { return _ToZip5; }
            set { _ToZip5 = value; }
        }
        private string _ToZip4;
        public string ToZip4
        {
            get { return _ToZip4; }
            set { _ToZip4 = value; }
        }
        private string _ToCity;
        public string ToCity
        {
            get { return _ToCity; }
            set { _ToCity = value; }
        }
        private string _ToAddress2;
        public string ToAddress2
        {
            get { return _ToAddress2; }
            set { _ToAddress2 = value; }
        }

        private string _ToAddress1;
        public string ToAddress1
        {
            get { return _ToAddress1; }
            set { _ToAddress1 = value; }
        }
       
        private string _ToName;
        public string ToName
        {
            get { return _ToName; }
            set { _ToName = value; }
        }
        private string _ToFirm;
        public string ToFirm
        {
            get { return _ToFirm; }
            set { _ToFirm = value; }
        }
        private byte[] _Label;
        public byte[] Label
        {
            get { return _Label; }
            set { _Label = value; }
        }

        public string LabelString { get; set; }

        public string TempLabelPath { get; set; }

        private InternationalResponse _intlResponse;
        public InternationalResponse IntlResponse
        {
            get { return _intlResponse; }
            set { _intlResponse = value; }
        }
        
    }
    public class  InternationalResponse
    {
        private string _regulation;
        public string Regulation
        {
            get { return _regulation; }
            set { _regulation = value; }
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
        private string _additionalRestrictions;
        public string AdditionalRestrictions
        {
            get { return _additionalRestrictions; }
            set { _additionalRestrictions = value; }
        }

        private decimal _postage;
        public decimal  Postage
        {
              get { return _postage; }
            set { _postage = value; }
        }

        private string _barcodeNumber;
        public string BarcodeNumber
        {
              get { return _barcodeNumber; }
            set { _barcodeNumber = value; }
        }
          private string _sdrValue;
        public string SDRValue
        {
              get { return _sdrValue; }
            set { _sdrValue = value; }
        }
        private byte[] _Label;
        public byte[] Label
        {
            get { return _Label; }
            set { _Label = value; }
        }

    }
}
