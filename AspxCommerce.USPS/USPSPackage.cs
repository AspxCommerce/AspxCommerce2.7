using System;
using System.Collections.Generic;
using System.Text;

namespace AspxCommerce.USPS
{
    public class USPSPackage
    {  //MailType =packagetype
        public USPSPackage()
        {
            AddressServiceRequested = false;
            SeparateReceiptPage = false;
            // _labelType = LabelType.FullLabel;
            // _labelImageType = LabelImageType.TIF;
            // _serviceType = ServiceType.ALL;
        }

        private LabelType _labelType;

        public LabelType LabelType
        {
            get { return _labelType; }
            set { _labelType = value; }
        }


        private DestinationAddress _shipToAddress = new DestinationAddress();

        public DestinationAddress ShipToAddress
        {
            get { return _shipToAddress; }
            set { _shipToAddress = value; }
        }


        private string _serviceType = "ALL";

        public string ServiceType
        {
            get { return _serviceType; }
            set { _serviceType = value; }
        }

        public bool SeparateReceiptPage { get; set; }


        private LabelImageType _labelImageType = LabelImageType.PDF;

        public LabelImageType LabelImageType
        {
            get { return _labelImageType; }
            set { _labelImageType = value; }
        }

        private DateTime _shipDate = DateTime.Now;

        public DateTime ShipDate
        {
            get { return _shipDate; }
            set { _shipDate = value; }
        }

        private string _referenceNumber = "";

        public string ReferenceNumber
        {
            get { return _referenceNumber; }
            set { _referenceNumber = value; }
        }

        public bool AddressServiceRequested { get; set; }

        public byte[] ShippingLabel { get; set; }

        public PackageType PackageType { get; set; }

        private PackageSize _packageSize = PackageSize.Regular;

        public PackageSize PackageSize
        {
            get { return _packageSize; }
            set { _packageSize = value; }
        }

        public decimal ValueOfContents { get; set; }

        // <Pounds>0</Pounds> <Ounces>3</Ounces> 
        // <Width/> <Length/> <Height/> <Girth/> 
        public bool Machinable { get; set; }

        private Container _container = Container.NONE;

        public Container Container
        {
            get { return _container; }
            set { _container = value; }
        }

        public string WeightUnit { get; set; }

        public decimal WeightValue { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public decimal Length { get; set; }

        public int Girth { get; set; }

        public decimal Pounds { get; set; }

        public int Quantity { get; set; }

        private GiftFlag _giftFlag = GiftFlag.N;
        public GiftFlag GiftFlag
        {
            get { return _giftFlag; }
            set { _giftFlag = value; }
        }
        private PoBoxFlag _poBoxFlag = PoBoxFlag.N;
        public PoBoxFlag PoBoxFlag
        {
            get { return _poBoxFlag; }
            set { _poBoxFlag = value; }
        }
        private CommercialFlag _commercialFlag = CommercialFlag.N;
        public CommercialFlag CommercialFlag
        {
            get { return _commercialFlag; }
            set { _commercialFlag = value; }
        }

        private Size _size = Size.REGULAR;
        public Size Size
        {
            get { return _size; }
            set { _size = value; }
        }
        private MailType _mailType = MailType.Package;
        public MailType MailType
        {
            get { return _mailType; }
            set { _mailType = value; }
        }
    }
    public enum PackageType { None, Flat_Rate_Envelope, Flat_Rate_Box };
    public enum PackageSize { None, Regular, Large, Oversize };
    public enum LabelImageType { TIF, PDF, None };
    public enum ServiceType
    {
       Express, Priority, First_Class, Parcel_Post, Media_Mail, Library_Mail

        //FIRST_CLASS,
        //FIRST_CLASS_COMMERCIAL,
        //FIRST_CLASS_HFP_COMMERCIAL,
        //PRIORITY,
        //PRIORITY_COMMERCIAL,
        //PRIORITY_HFP_COMMERCIAL,
        //EXPRESS,
        //EXPRESS_COMMERCIAL,
        //EXPRESS_SH,
        //EXPRESS_SH_COMMERCIAL,
        //EXPRESS_HFP,
        //EXPRESS_HFP_COMMERCIAL,
        //PARCEL,
        //MEDIA,
        //LIBRARY,
        //ALL,
        //ONLINE,

    };
    public enum LabelType { FullLabel = 1, DeliveryConfirmationBarcode = 2 };
    public enum Container
    {
        VARIABLE, FLAT_RATE_ENVELOPE,
        PADDED_FLAT_RATE_ENVELOPE,
        LEGAL_FLAT_RATE_ENVELOPE,
        SM_FLAT_RATE_ENVELOPE,
        WINDOW_FLAT_RATE_ENVELOPE,
        GIFT_CARD_FLAT_RATE_ENVELOPE,
        FLAT_RATE_BOX,
        SM_FLAT_RATE_BOX,
        MD_FLAT_RATE_BOX,
        LG_FLAT_RATE_BOX,
        REGIONALRATEBOXA,
        REGIONALRATEBOXB,
        RECTANGULAR,
        NONRECTANGULAR,
        NONE
    };
    public enum WeightUnits
    {
        KGS,
        LBS,
        POUNDS,
        OUNCES,
    };
    public enum POBoxFlag
    {
        Y, N,
    };
    public enum MailType
    {
        Package,
        Postcards,
        aerogrammes,
        Envelope,
        LargeEnvelope,
        FlatRate,
    };

    public enum ImageLayout
    {
        ONEPERFILE,
        ALLINONEFILE,
        TRIMONEPERFILE,
        TRIMALLINONEFILE
    }

    public enum ImageType
    {
        PDF,
        TIF
    };

    public enum PoBoxFlag
    {
        Y,
        N

    } ;

    public enum GiftFlag
    {
        Y,
        N

    } ;
    public enum CommercialFlag
    {
        Y,
        N

    } ;

    public enum Size
    {
        REGULAR,
        LARGE
    } ;

   

    public enum FirstClassMailType
    {
        PARCEL,
        LETTER,
        FLAT
    }

    public enum NonDeliveryOption
    {
        ABANDON,
        RETURN,
        REDIRECT
       
    }

    public enum ContentType
    {
        MERCHANDISE,
        SAMPLE,
        GIFT,
        DOCUMENTS,
        RETURN,
        OTHER
    } ;


}
