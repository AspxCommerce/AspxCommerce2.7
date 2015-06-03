using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.Web;
using AspxCommerce.USPS.Entity;
using SageFrame.Web.Utilities;
using System.Collections;
using AspxCommerce.Core;

namespace AspxCommerce.USPS
{
    public class USPS
    {

        private bool _isTracking = false;
        private string _defaultTrackingUrl = "http://trkcnfrm1.smi.usps.com/PTSInternetWeb/InterLabelInquiry.do?origTrackNum={0}";
        private string _liveModeUrl = "http://production.shippingapis.com/ShippingAPI.dll";
        private static readonly Dictionary<string, string> Apis = new Dictionary<string, string>();
        private string _TestModeUrl = "http://testing.shippingapis.com/ShippingAPI.dll";
        private string _userId = string.Empty;
        private bool _useTestMode;
        private bool _isShipment = false;
        private string _liveModeShipmentUrl = "https://secure.shippingapis.com/ShippingAPI.dll";
        private static readonly Dictionary<string, string> Services = new Dictionary<string, string>();
        private bool _isItemWiseCalculation = false;
        private string labelExtention = "";
        WareHouseAddress _originAddress = new WareHouseAddress();

        static USPS()
        {
            Apis.Add("DomesticRate", "RateV4Request");
            Apis.Add("InternationalRate", "IntlRateV2Request");
            Apis.Add("TestLabel", "DelivConfirmCertifyV3.0Request");
            Apis.Add("LiveLabel", "DeliveryConfirmationV3.0Request");
            Apis.Add("AddressValidate", "AddressValidateRequest");
            Apis.Add("TestSignatureConfirmation", "SigConfirmCertifyV3.0Request");
            Apis.Add("SignatureConfirmation", "SignatureConfirmationV3.0Request");
            //  Services.Add("LiveSignatureConfirmation", " SignatureConfirmationV3.0Request", "SignatureConfirmationV3");
            //  Services.Add("LiveTrack", "TrackRequest", "TrackV2");
            // "https://servername/ShippingAPI.dll?API=DelivConfirmCertifyV3&XML=<DelivConfirmCertifyV3.0Request USERID=\"username\"></DelivConfirmCertifyV3.0Request>"


            Services.Add("Priority Mail Express 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt;", "Priority Mail Express 1-Day");
            Services.Add("Priority Mail Express 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Hold For Pickup", "Priority Mail Express 1-Day Hold For Pickup");
            Services.Add("Priority Mail Express 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Flat Rate Boxes", "Priority Mail Express 1-Day Flat Rate Boxes");
            Services.Add("Priority Mail Express 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Flat Rate Boxes Hold For Pickup", "Priority Mail Express 1-Day Flat Rate Boxes Hold For Pickup");
            Services.Add("Priority Mail Express 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Flat Rate Envelope", "Priority Mail Express 1-Day Flat Rate Envelope");
            Services.Add("Priority Mail Express 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Flat Rate Envelope Hold For Pickup", "Priority Mail Express 1-Day Flat Rate Envelope Hold For Pickup");
            Services.Add("Priority Mail Express 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Legal Flat Rate Envelope", "Priority Mail Express 1-Day Legal Flat Rate Envelope");
            Services.Add("Priority Mail Express 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Legal Flat Rate Envelope Hold For Pickup", "Priority Mail Express 1-Day Legal Flat Rate Envelope Hold For Pickup");
            Services.Add("Priority Mail Express 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Padded Flat Rate Envelope", "Priority Mail Express 1-Day Padded Flat Rate Envelope");
            Services.Add("Priority Mail Express 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Padded Flat Rate Envelope Hold For Pickup", "Priority Mail Express 1-Day Padded Flat Rate Envelope Hold For Pickup");

            Services.Add("Priority Mail 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt;", "Priority Mail 1-Day");
            Services.Add("Priority Mail 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Large Flat Rate Box", "Priority Mail 1-Day Large Flat Rate Box");
            Services.Add("Priority Mail 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Medium Flat Rate Box", "Priority Mail 1-Day Medium Flat Rate Box");
            Services.Add("Priority Mail 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Small Flat Rate Box", "Priority Mail 1-Day Small Flat Rate Box");
            Services.Add("Priority Mail 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Flat Rate Envelope", "Priority Mail 1-Day Flat Rate Envelope");
            Services.Add("Priority Mail 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Legal Flat Rate Envelope", "Priority Mail 1-Day Legal Flat Rate Envelope");
            Services.Add("Priority Mail 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Padded Flat Rate Envelope", "Priority Mail 1-Day Padded Flat Rate Envelope");
            Services.Add("Priority Mail 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Gift Card Flat Rate Envelope", "Priority Mail 1-Day Gift Card Flat Rate Envelope");
            Services.Add("Priority Mail 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Small Flat Rate Envelope", "Priority Mail 1-Day Small Flat Rate Envelope");
            Services.Add("Priority Mail 1-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Window Flat Rate Envelope", "Priority Mail 1-Day Window Flat Rate Envelope");

            Services.Add("Priority Mail Express 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt;", "Priority Mail Express 2-Day");
            Services.Add("Priority Mail Express 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Hold For Pickup", "Priority Mail Express 2-Day Hold For Pickup");
            Services.Add("Priority Mail Express 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Flat Rate Boxes", "Priority Mail Express 2-Day Flat Rate Boxes");
            Services.Add("Priority Mail Express 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Flat Rate Boxes Hold For Pickup", "Priority Mail Express 2-Day Flat Rate Boxes Hold For Pickup");
            Services.Add("Priority Mail Express 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Flat Rate Envelope", "Priority Mail Express 2-Day Flat Rate Envelope");
            Services.Add("Priority Mail Express 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Flat Rate Envelope Hold For Pickup", "Priority Mail Express 2-Day Flat Rate Envelope Hold For Pickup");
            Services.Add("Priority Mail Express 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Legal Flat Rate Envelope", "Priority Mail Express 2-Day Legal Flat Rate Envelope");
            Services.Add("Priority Mail Express 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Legal Flat Rate Envelope Hold For Pickup", "Priority Mail Express 2-Day Legal Flat Rate Envelope Hold For Pickup");
            Services.Add("Priority Mail Express 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Padded Flat Rate Envelope", "Priority Mail Express 2-Day Padded Flat Rate Envelope");
            Services.Add("Priority Mail Express 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Padded Flat Rate Envelope Hold For Pickup", "Priority Mail Express 2-Day Padded Flat Rate Envelope Hold For Pickup");




            Services.Add("Priority Mail 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt;", "Priority Mail 2-Day");
            Services.Add("Priority Mail 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Large Flat Rate Box", "Priority Mail 2-Day Large Flat Rate Box");
            Services.Add("Priority Mail 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Medium Flat Rate Box", "Priority Mail 2-Day Medium Flat Rate Box");
            Services.Add("Priority Mail 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Small Flat Rate Box", "Priority Mail 2-Day Small Flat Rate Box");
            Services.Add("Priority Mail 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Flat Rate Envelope", "Priority Mail 2-Day Flat Rate Envelope");
            Services.Add("Priority Mail 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Legal Flat Rate Envelope", "Priority Mail 2-Day Legal Flat Rate Envelope");
            Services.Add("Priority Mail 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Padded Flat Rate Envelope", "Priority Mail 2-Day Padded Flat Rate Envelope");
            Services.Add("Priority Mail 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Gift Card Flat Rate Envelope", "Priority Mail 2-Day Gift Card Flat Rate Envelope");
            Services.Add("Priority Mail 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Small Flat Rate Envelope", "Priority Mail 2-Day Small Flat Rate Envelope");
            Services.Add("Priority Mail 2-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Window Flat Rate Envelope", "Priority Mail 2-Day Window Flat Rate Envelope");
            Services.Add("Media Mail&amp;lt;sup&amp;gt;&amp;#174;&amp;lt;/sup&amp;gt;", "Media Mail");
            Services.Add("Library Mail", "Library Mail");


            Services.Add("Priority Mail 3-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt;", "Priority Mail 3-Day");
            Services.Add("Priority Mail 3-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Large Flat Rate Box", "Priority Mail 3-Day Large Flat Rate Box");
            Services.Add("Priority Mail 3-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Medium Flat Rate Box", "Priority Mail 3-Day Medium Flat Rate Box");
            Services.Add("Priority Mail 3-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Small Flat Rate Box", "Priority Mail 3-Day Small Flat Rate Box");
            Services.Add("Priority Mail 3-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Flat Rate Envelope", "Priority Mail 3-Day Flat Rate Envelope");
            Services.Add("Priority Mail 3-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Legal Flat Rate Envelope", "Priority Mail 3-Day Legal Flat Rate Envelope");
            Services.Add("Priority Mail 3-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Padded Flat Rate Envelope", "Priority Mail 3-Day Padded Flat Rate Envelope");
            Services.Add("Priority Mail 3-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Gift Card Flat Rate Envelope", "Priority Mail 3-Day Gift Card Flat Rate Envelope");
            Services.Add("Priority Mail 3-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Small Flat Rate Envelope", "Priority Mail 3-Day Small Flat Rate Envelope");
            Services.Add("Priority Mail 3-Day&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Window Flat Rate Envelope", "Priority Mail 3-Day Window Flat Rate Envelope");





            Services.Add("Standard Post&amp;lt;sup&amp;gt;&amp;#174;&amp;lt;/sup&amp;gt;", "Standard Post");

            Services.Add("Global Express Guaranteed&amp;lt;sup&amp;gt;&amp;#174;&amp;lt;/sup&amp;gt; (GXG)**", "Global Express Guaranteed");
            Services.Add("Global Express Guaranteed&amp;lt;sup&amp;gt;&amp;#174;&amp;lt;/sup&amp;gt; Non-Document Rectangular", "Global Express Guaranteed Non-Document Rectangular");
            Services.Add("Global Express Guaranteed&amp;lt;sup&amp;gt;&amp;#174;&amp;lt;/sup&amp;gt; Non-Document Non-Rectangular", "Global Express Guaranteed Non-Document Non-Rectangular");
            Services.Add("USPS GXG&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Envelopes**", "USPS GXG Envelopes");
            Services.Add("Priority Mail Express International&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt;", "Priority Mail Express International");
            Services.Add("Priority Mail Express International&amp;lt;sup&amp;gt;&amp;#8482;&amp;lt;/sup&amp;gt; Flat Rate Boxes", "Priority Mail Express International Flat Rate Boxes");
            Services.Add("Priority Mail International&amp;lt;sup&amp;gt;&amp;#174;&amp;lt;/sup&amp;gt;", "Priority Mail International");
            Services.Add("Priority Mail International&amp;lt;sup&amp;gt;&amp;#174;&amp;lt;/sup&amp;gt; Large Flat Rate Box", "Priority Mail International Large Flat Rate Box");
            Services.Add("Priority Mail International&amp;lt;sup&amp;gt;&amp;#174;&amp;lt;/sup&amp;gt; Medium Flat Rate Box", "Priority Mail International Medium Flat Rate Box");




            //Services.Add("Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt;", "USPS Express Mail");
            //Services.Add("Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Hold For Pickup",
            //             "USPS Express Mail Hold For Pickup");
            //Services.Add("Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Flat Rate Boxes",
            //             "USPS Express Mail Flat Rate Boxes");
            //Services.Add(
            //    "Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Flat Rate Boxes Hold For Pickup",
            //    "USPS Express Mail Flat Rate Boxes Hold For Pickup");
            //Services.Add(
            //    "Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Sunday/Holiday Delivery Flat Rate Boxes",
            //    "USPS Express Mail Sunday/Holiday Delivery Flat Rate Boxes");
            //Services.Add(
            //    "Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Legal Flat Rate Envelope Hold For Pickup",
            //    "USPS Express Mail Legal Flat Rate Envelope Hold For Pickup");
            //Services.Add("First-Class Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Large Postcards",
            //             "USPS Express Mail Large Postcards");
            //Services.Add("Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Sunday/Holiday Delivery",
            //             "USPS Express Mail Sunday/Holiday Delivery");
            //Services.Add("Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Flat Rate Envelope",
            //             "USPS Express Mail Flat Rate Envelope");
            //Services.Add(
            //    "Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Sunday/Holiday Delivery Flat Rate Envelope",
            //    "USPS Express Mail Sunday/Holiday Delivery Flat Rate Envelope");
            //Services.Add("Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Legal Flat Rate Envelope",
            //             "USPS Express Mail Legal Flat Rate Envelope");
            //Services.Add(
            //    "Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Sunday/Holiday Delivery Legal Flat Rate Envelope",
            //    "USPS Express Mail Sunday/Holiday Delivery Legal Flat Rate Envelope");
            //Services.Add(
            //    "Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Flat Rate Envelope Hold For Pickup",
            //    "USPS Express Mail Flat Rate Envelope Hold For Pickup");
            //Services.Add(
            //     "Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Flat Rate Boxes",
            //     "USPS Express Mail International Flat Rate Boxes"); //®


            //Services.Add("Express Mail&lt;sup&gt;&amp;reg;&lt;/sup&gt; Sunday/Holiday Delivery",
            //             "USPS Express Mail Sunday/Holiday Guarantee");
            //Services.Add("Express Mail&lt;sup&gt;&amp;reg;&lt;/sup&gt; Sunday/Holiday Delivery Flat Rate Envelope",
            //             "USPS Express Mail Flat-Rate Envelope Sunday/Holiday Guarantee");
            ////Services.Add("Express Mail&lt;sup&gt;&amp;reg;&lt;/sup&gt; Flat Rate Envelope","Express Mail Flat Rate Envelope");
            //Services.Add("Express Mail&lt;sup&gt;&amp;reg;&lt;/sup&gt;", "USPS Express Mail");


            //Services.Add("Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Legal Flat Rate Envelope",
            //             "USPS Priority Mail Legal Flat Rate Envelope");
            //Services.Add("Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Padded Flat Rate Envelope",
            //             "USPS Priority Mail Padded Flat Rate Envelope");
            //Services.Add(
            //    "Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Gift Card Flat Rate Envelope",
            //    "USPS Priority Mail Gift Card Flat Rate Envelope");
            //Services.Add("Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Small Flat Rate Envelope",
            //             "USPS Priority Mail Small Flat Rate Envelope");
            //Services.Add("Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Window Flat Rate Envelope",
            //             "USPS Priority Mail Window Flat Rate Envelope");
            //Services.Add("Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt;", "USPS Priority Mail");
            //Services.Add("Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Large Flat Rate Box",
            //             "USPS Priority Mail Large Flat Rate Box");
            //Services.Add("Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Medium Flat Rate Box",
            //             "USPS Priority Mail Medium Flat Rate Box");
            //Services.Add("Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Small Flat Rate Box",
            //             "USPS Priority Mail Small Flat Rate Box");
            //Services.Add("Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Flat Rate Envelope",
            //             "USPS Priority Mail Flat Rate Envelope");
            //Services.Add("Priority Mail&lt;sup&gt;&amp;reg;&lt;/sup&gt;", "USPS Priority Mail");
            //Services.Add("First-Class Mail&lt;sup&gt;&amp;reg;&lt;/sup&gt; Letter", "USPS First-Class Mail");
            //Services.Add("First-Class Mail&lt;sup&gt;&amp;reg;&lt;/sup&gt; Large Envelope", "USPS First-Class Mail Flat");

            //Services.Add("First-Class Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Package",
            //             "USPS First-Class Mail Package");
            //Services.Add("First-Class Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Large Envelope",
            //             "USPS First-Class Mail Large Envelope");
            //Services.Add("First-Class Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Letter",
            //             "USPS First-Class Mail Letter");
            //Services.Add("First-Class Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Postcards",
            //             "USPS First-Class Mail Postcards");
            //Services.Add("Parcel Post&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt;", "USPS Parcel Post");
            //Services.Add("Media Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt;", "USPS Media Mail");
            //Services.Add("Library Mail", "USPS Library Mail");
            //Services.Add("Global Express Guaranteed&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; (GXG)",
            //             "USPS Global Express Guaranteed (GXG)");
            //Services.Add(
            //    "Global Express Guaranteed&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Non-Document Rectangular",
            //    "USPS Global Express Guaranteed Non-Document Rectangular");
            //Services.Add(
            //    "Global Express Guaranteed&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Non-Document Non-Rectangular",
            //    "USPS Global Express Guaranteed Non-Document Non-Rectangular");
            //Services.Add("USPS GXG&lt;sup&gt;&amp;trade;&lt;/sup&gt; Envelopes", "USPS GXG Envelopes");
            //Services.Add("Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International",
            //             "USPS Express Mail International");
            //Services.Add(
            //    "Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Flat Rate Envelope",
            //    "USPS Express Mail International Flat Rate Envelope");
            //Services.Add("Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International",
            //             "USPS Priority Mail International");
            //Services.Add(
            //    "Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Large Flat Rate Box",
            //    "USPS Priority Mail International Large Flat Rate Box");
            //Services.Add(
            //    "Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Medium Flat Rate Box",
            //    "USPS Priority Mail International Medium Flat Rate Box");
            //Services.Add(
            //    "Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Small Flat Rate Box",
            //    "USPS Priority Mail International Small Flat Rate Box");
            //Services.Add(
            //    "Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Flat Rate Envelope",
            //    "USPS Priority Mail International Flat Rate Envelope");
            //Services.Add("First-Class Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Package",
            //             "USPS First-Class Mail International Package");
            //Services.Add(
            //    "First-Class Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Large Envelope",
            //    "USPS First-Class Mail International Large Envelope");
            //Services.Add("First-Class Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Letter",
            //             "USPS First-Class Mail International Letter");
            //Services.Add("First-Class Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Parcel",
            //             "USPS First-Class Mail Parcel");
            //Services.Add("USPS GXG&amp;lt;sup&amp;gt;&amp;amp;trade;&amp;lt;/sup&amp;gt; Envelopes**",
            //             "USPS Global Express Guaranteed"); //®

            //INTERNATIONAL LIST
            //            ("USPS First-Class Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Letter**","")
            //            ;
            //"USPS First-Class Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Large Envelope**"=>103,
            //"USPS First-Class Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Package**"=>123,
            //"USPS Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Gift Card Flat Rate Envelope**"=>1195,
            //"USPS Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Small Flat Rate Envelope**"=>1195,
            //"USPS Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Window Flat Rate Envelope**"=>1195,
            //"USPS Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Padded Flat Rate Envelope**"=>1195,
            //"USPS Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Legal Flat Rate Envelope**"=>1195,
            //"USPS Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Flat Rate Envelope**"=>1195,
            //"USPS Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Large Video Flat Rate Box**"=>1195,
            //"USPS Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Small Flat Rate Box**"=>1195,
            //"USPS Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International DVD Flat Rate Box**"=>1195,
            //"USPS Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International"=>2125,
            //"USPS Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International"=>2695,
            //"USPS Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Flat Rate Envelope"=>2695,
            //"USPS Express Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Legal Flat Rate Envelope"=>2695,
            //"USPS Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Medium Flat Rate Box"=>2795,
            //"USPS Priority Mail&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; International Large Flat Rate Box"=>3550,
            //"USPS GXG&amp;lt;sup&amp;gt;&amp;amp;trade;&amp;lt;/sup&amp;gt; Envelopes**"=>3550,
            //"USPS Global Express Guaranteed&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Non-Document Non-Rectangular"=>3550,
            //"USPS Global Express Guaranteed&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; Non-Document Rectangular"=>3550,
            //"USPS Global Express Guaranteed&amp;lt;sup&amp;gt;&amp;amp;reg;&amp;lt;/sup&amp;gt; (GXG)**"=>3550
        }

        public USPS()
        {

        }

        #region Provider Services

        public static Dictionary<string, string> GetAvailableServiceMethod()
        {
            return Services;
        }
        public Dictionary<string, string> GetService()
        {
            return Services;
        }

        #endregion

        #region Configuration
        private void LoadConfig(int providerId, int storeId, int portalId)
        {

            try
            {
                List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
                paraMeter.Add(new KeyValuePair<string, object>("@StoreId", storeId));
                paraMeter.Add(new KeyValuePair<string, object>("@PortalId", portalId));
                paraMeter.Add(new KeyValuePair<string, object>("@ShippingProviderId", providerId));
                SQLHandler sqLh = new SQLHandler();
                UspsSetting info = sqLh.ExecuteAsObject<UspsSetting>("usp_Aspx_GetUSPSShippingProviderSetting", paraMeter);
                if (info != null)
                {
                    bool isSettingEmpty = string.IsNullOrEmpty(info.UspsUserId) ||
                                          string.IsNullOrEmpty(info.UspsRateApiUrl) ||
                                          string.IsNullOrEmpty(info.UspsTrackApiUrl) ||
                                          string.IsNullOrEmpty(info.UspsShipmentApiUrl);

                    _useTestMode = info.UspsTestMode;
                    _userId = info.UspsUserId;
                    if (!string.IsNullOrEmpty(info.UspsTrackApiUrl))
                    {
                        _defaultTrackingUrl = info.UspsTrackApiUrl;
                    }
                    if (!string.IsNullOrEmpty(info.UspsRateApiUrl))
                        _liveModeUrl = info.UspsRateApiUrl;
                    if (!string.IsNullOrEmpty(info.UspsShipmentApiUrl))
                        _liveModeShipmentUrl = info.UspsShipmentApiUrl;
                    if (_useTestMode)
                    {
                        //_TestModeUrl = _liveModeUrl;
                    }
                    if (isSettingEmpty)
                    {
                        throw new Exception("Invalid Settings!");
                    }



                }
                else
                {
                    throw new Exception("Invalid Shipping Provider!");
                }

                //_Password = "130ZZ84FP557";
                //_userId = "713BRAIN1876";
                //_UserIdActive = true;
                //_useTestMode = true;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void GetOriginAddress()
        {
            _originAddress.StreetAddress2 = "";
            _originAddress.City = "COLLIERVILLE ";
            _originAddress.Country = "US";
            _originAddress.PostalCode = "38017";
            _originAddress.State = "TN";
            _originAddress.StreetAddress1 = "SHIPPER ADDRESS LINE 1";//new string[1] { "SHIPPER ADDRESS LINE 1" }; 

        }

        private string GetUrl()
        {
            string url = "";
            if (_isShipment == true && _isTracking == false)
            {
                url = _liveModeShipmentUrl;
            }
            else if (_isShipment == false && _isTracking == true)
            {
                // url = _defaultTrackingUrl;
                url = _liveModeUrl;
                if (_useTestMode)
                    url = _TestModeUrl;
            }
            else
            {
                url = _liveModeUrl;
                if (_useTestMode)
                    url = _TestModeUrl;
            }
            return url;
        }
        #endregion

        #region Call to Service provider
        private string SendGetRequestToProvider(string url, string encoding)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            string str2;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                string str = current.Request.ServerVariables["HTTP_REFERER"];
                if (!string.IsNullOrEmpty(str))
                {
                    request.Referer = str;
                }
            }
            using (var reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding(encoding)))
            {
                str2 = reader.ReadToEnd();
                reader.Close();
            }
            return str2;
        }

        private XmlDocument SendRequestToProvider(XmlDocument request, string apiName)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            string str3;
            var request2 = (HttpWebRequest)WebRequest.Create(GetUrl());
            request2.Method = "POST";
            request2.ContentType = "application/x-www-form-urlencoded";
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                string str = current.Request.ServerVariables["HTTP_REFERER"];
                if (!string.IsNullOrEmpty(str))
                {
                    request2.Referer = str;
                }
            }
            string str2 = "API=" + apiName + "&XML=" + HttpUtility.UrlEncode("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + request.OuterXml);
            byte[] bytes = Encoding.UTF8.GetBytes(str2);
            request2.ContentLength = bytes.Length;
            using (var stream = request2.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
            using (var reader = new StreamReader(request2.GetResponse().GetResponseStream(), Encoding.UTF8))
            {
                str3 = reader.ReadToEnd();
                reader.Close();
            }

            var document = new XmlDocument();
            document.LoadXml(str3);
            return document;
        }
        #endregion

        #region Track

        public TrackingInfo GetTrackingInfo(string trackingIds, int providerId, AspxCommonInfo commonInfo)
        {
            try
            {

                const string api = "TrackV2";
                _isTracking = true;
                LoadConfig(providerId, commonInfo.StoreID, commonInfo.PortalID);
                XmlDocument track = new XmlDocument();
                XmlNode root = track.CreateNode(XmlNodeType.Element, "TrackRequest", "");
                XmlNode ids = track.CreateNode(XmlNodeType.Attribute, "USERID", "");
                ids.Value = this._userId;

                root.Attributes.SetNamedItem(ids); //save attribute value
                int x = 0;

                XmlNode node = track.CreateNode(XmlNodeType.Element, "TrackID", "");
                //  root.AppendChild(track.CreateNode(XmlNodeType.Element, "TrackID", ""));
                XmlNode trackingid = track.CreateNode(XmlNodeType.Attribute, "ID", "");
                trackingid.Value = trackingIds;
                node.Attributes.SetNamedItem(trackingid); //save attribute value
                root.AppendChild(node);
                x++;

                track.AppendChild(root);
                XmlDocument response = SendRequestToProvider(track, api);
                TrackingInfo tinfo = ParseTrackingResponse(response);

                return tinfo;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private TrackingInfo ParseTrackingResponse(XmlDocument tinfo)
        {
            try
            {
                TrackingInfo trackdetail = new TrackingInfo();
                var elem = tinfo.DocumentElement;

                if (tinfo.DocumentElement != null)
                {
                    if (elem.Name.ToLower().Trim() != "error")
                    {
                        XmlNodeList list = tinfo.DocumentElement.SelectNodes("TrackInfo");
                        if (list != null)
                            foreach (XmlNode node in list)
                            {
                                if (node.FirstChild.Name.ToLower() == "error")
                                {
                                    string error = node.FirstChild.InnerXml;
                                    trackdetail.IsFailed = true;
                                    trackdetail.Error = error;
                                    break;
                                }
                                else
                                {
                                    trackdetail.TrackingNumber =
                                        node.Attributes["ID"].Value;
                                    trackdetail.Summary = node.SelectSingleNode("TrackSummary").InnerXml;
                                    XmlNodeList detailslist = node.SelectNodes("TrackDetail");

                                    if (detailslist.Count > 0)
                                    {
                                        foreach (XmlNode detail in detailslist)
                                        {
                                            trackdetail.Details.Add(detail.InnerXml);
                                        }
                                    }
                                    return trackdetail;
                                }
                            }
                    }
                    else
                    {
                        string error = elem.InnerXml;
                        trackdetail.IsFailed = true;
                        trackdetail.Error = error;
                    }
                }
                return trackdetail;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region RATE

        public List<object> GetRateFromUsps(WareHouseAddress originAddress, DestinationAddress destination, ArrayList plist, int providerId, int storeId, int portalId)
        {
            var lists = new List<object>();
            List<USPSPackage> list = new List<USPSPackage>(plist.Count);
            list.AddRange(plist.Cast<USPSPackage>());
            try
            {
                if (originAddress.Country == "US")
                {
                    List<USPSRateResponse> rateResponses;
                    if (originAddress.Country.ToLower() == destination.ToCountry.ToLower())
                    {
                        rateResponses = GetRate(originAddress, destination, list, true, providerId, storeId, portalId);
                    }
                    else
                    {
                        rateResponses = GetRate(originAddress, destination, list, false, providerId, storeId, portalId);
                    }


                    foreach (var item in rateResponses)
                    {
                        if (string.IsNullOrEmpty(item.ErrorMessage))
                        {
                            if (item.IsDomestic)
                            {
                                lists.AddRange(item.Postage.Cast<object>());
                            }
                            else
                            {
                                lists.AddRange(item.IntRateList.Cast<object>());
                            }
                        }
                        else
                        {
                            lists.Add(item);
                        }

                    }




                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lists;
        }

        public List<USPSRateResponse> GetRate(WareHouseAddress originAddress, DestinationAddress destination, List<USPSPackage> plist, bool isDomestic, int providerId, int storeId, int portalId)
        {

            //GetOriginAddress()
            _originAddress = originAddress;
            List<USPSRateResponse> response;
            LoadConfig(providerId, storeId, portalId);
            XmlDocument xx = new XmlDocument();
            string api = ""; //RateV4Request
            XmlNode wrap;
            XmlNode userId;

            if (isDomestic)
            {
                api = "RateV4";
                wrap = xx.CreateNode(XmlNodeType.Element, "RateV4Request", "");
                userId = xx.CreateNode(XmlNodeType.Attribute, "USERID", "");
                userId.Value = this._userId;
                wrap.Attributes.SetNamedItem(userId);
            }
            else
            {
                if (_isItemWiseCalculation)
                {
                    api = "IntlRateV2";
                    wrap = xx.CreateNode(XmlNodeType.Element, "IntlRateV2Request", "");
                    userId = xx.CreateNode(XmlNodeType.Attribute, "USERID", "");
                    userId.Value = this._userId;
                    wrap.Attributes.SetNamedItem(userId);
                }
                else
                {
                    api = "IntlRate";
                    wrap = xx.CreateNode(XmlNodeType.Element, "IntlRateRequest", "");
                    userId = xx.CreateNode(XmlNodeType.Attribute, "USERID", "");
                    userId.Value = this._userId;
                    wrap.Attributes.SetNamedItem(userId);
                }



            }
            int i = 1;

            foreach (var package in plist)
            {
                XmlNode root = xx.CreateNode(XmlNodeType.Element, "Package", "");
                XmlNode ids = xx.CreateNode(XmlNodeType.Attribute, "ID", "");
                ids.Value = i.ToString();
                root.Attributes.SetNamedItem(ids); //save attribute value

                if (isDomestic)
                {

                    decimal totalWeight = 0;
                    totalWeight = package.Quantity * package.WeightValue;

                    XmlNode node = xx.CreateNode(XmlNodeType.Element, "Service", "");
                    root.AppendChild(node); // append node to root 
                    root.SelectSingleNode("Service").InnerXml = package.ServiceType.ToString();

                    //if  servicetype=all then no mailtype specification
                    //  root.AppendChild(xx.CreateNode(XmlNodeType.Element, "MailType", ""));
                    //  root.SelectSingleNode("MailType").InnerXml = package.ServiceType.ToString();

                    root.AppendChild(xx.CreateNode(XmlNodeType.Element, "ZipOrigination", ""));
                    root.SelectSingleNode("ZipOrigination").InnerXml = _originAddress.PostalCode; //package.OriginZipcode.ToString();

                    root.AppendChild(xx.CreateNode(XmlNodeType.Element, "ZipDestination", ""));
                    root.SelectSingleNode("ZipDestination").InnerXml = destination.ToPostalCode;
                    if (package.WeightUnit.ToLower() == WeightUnits.POUNDS.ToString().ToLower() ||
                            package.WeightUnit.ToLower() == "pound" ||
                            package.WeightUnit.ToLower() == "lb" ||
                          package.WeightUnit.ToLower() == WeightUnits.LBS.ToString().ToLower())
                    {
                        //pound to ounce conversion
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Pounds", ""));
                        root.SelectSingleNode("Pounds").InnerXml = totalWeight.ToString();

                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Ounces", ""));
                        root.SelectSingleNode("Ounces").InnerXml = "0";// (package.WeightValue * 16).ToString();
                    }
                    else if (package.WeightUnit.ToLower() == WeightUnits.KGS.ToString().ToLower() ||
                            package.WeightUnit.ToLower() == "kg")
                    {
                        //pound to ounce conversion
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Pounds", ""));
                        root.SelectSingleNode("Pounds").InnerXml =
                            (double.Parse(package.WeightValue.ToString()) * 2.20462262185).ToString();

                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Ounces", ""));
                        root.SelectSingleNode("Ounces").InnerXml = "0";
                        // (double.Parse(package.WeightValue.ToString()) * 2.20462262185 * 16).ToString();
                    }
                    root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Container", ""));
                    if (package.Container.ToString().ToLower() != "none")
                        root.SelectSingleNode("Container").InnerXml = package.Container.ToString();

                    root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Size", ""));
                    root.SelectSingleNode("Size").InnerXml = package.PackageSize.ToString();

                    root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Machinable", ""));
                    root.SelectSingleNode("Machinable").InnerXml = package.Machinable.ToString();

                    if (package.Container.ToString().ToLower() == "none")
                    {

                        //root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Width", ""));
                        //root.SelectSingleNode("Width").InnerXml = package.Width.ToString();

                        //root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Length", ""));
                        //root.SelectSingleNode("Length").InnerXml = package.Length.ToString();

                        //root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Height", ""));
                        //root.SelectSingleNode("Height").InnerXml = package.Height.ToString();

                        ////for international rate
                        //// The "girth" of the package as measured in inches rounded
                        ////to the nearest whole inch. Required to obtain GXG pricing
                        ////when pricing and when Size=”LARGE” and
                        ////Container=”NONRECTANGULAR”.
                        ////For example: <Girth>15</Girth>
                        //root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Girth", ""));
                        //root.SelectSingleNode("Girth").InnerXml = package.Girth.ToString();
                    }

                    // root.AppendChild(node);//append node to root 

                }
                else
                {
                    if (_isItemWiseCalculation)
                    {
                        decimal totalWeight = 0;
                        totalWeight = package.Quantity * package.WeightValue;
                        if (package.WeightUnit.ToLower() == WeightUnits.POUNDS.ToString().ToLower() ||
                            package.WeightUnit.ToLower() == "pound" ||
                            package.WeightUnit.ToLower() == "lb" ||
                          package.WeightUnit.ToLower() == WeightUnits.LBS.ToString().ToLower())
                        {
                            //pound to ounce conversion
                            root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Pounds", ""));
                            root.SelectSingleNode("Pounds").InnerXml = totalWeight.ToString();

                            root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Ounces", ""));
                            root.SelectSingleNode("Ounces").InnerXml = "0";//(totalWeight * 16).ToString();
                        }
                        else if (package.WeightUnit.ToLower() == WeightUnits.KGS.ToString().ToLower() ||
                            package.WeightUnit.ToLower() == "kg")
                        {
                            //pound to ounce conversion
                            root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Pounds", ""));
                            root.SelectSingleNode("Pounds").InnerXml =
                                (double.Parse(totalWeight.ToString()) * 2.20462262185).ToString();

                            root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Ounces", ""));
                            root.SelectSingleNode("Ounces").InnerXml = "0";
                            // (double.Parse(totalWeight.ToString())*2.20462262185*16).ToString();
                        }
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Machinable", ""));
                        root.SelectSingleNode("Machinable").InnerXml = package.Machinable.ToString();

                        //if  servicetype=all then no mailtype specification
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "MailType", ""));
                        root.SelectSingleNode("MailType").InnerXml = package.MailType.ToString();

                        XmlNode gxg = xx.CreateNode(XmlNodeType.Element, "GXG", "");
                        XmlNode poboxFlag = xx.CreateNode(XmlNodeType.Element, "POBoxFlag", "");
                        gxg.AppendChild(poboxFlag);
                        XmlNode giftFlag = xx.CreateNode(XmlNodeType.Element, "GiftFlag", "");
                        gxg.AppendChild(giftFlag);
                        gxg.SelectSingleNode("POBoxFlag").InnerXml = package.PoBoxFlag.ToString();
                        gxg.SelectSingleNode("GiftFlag").InnerXml = package.GiftFlag.ToString();

                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "ValueOfContents", ""));
                        root.SelectSingleNode("ValueOfContents").InnerXml = package.ValueOfContents.ToString();
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Country", ""));
                        root.SelectSingleNode("Country").InnerXml = destination.ToCountryName;
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Container", ""));
                        root.SelectSingleNode("Container").InnerXml = package.Container.ToString();
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Size", ""));
                        root.SelectSingleNode("Size").InnerXml = package.Size.ToString();
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Width", ""));
                        root.SelectSingleNode("Width").InnerXml = package.Width.ToString();
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Length", ""));
                        root.SelectSingleNode("Length").InnerXml = package.Length.ToString();
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Height", ""));
                        root.SelectSingleNode("Height").InnerXml = package.Height.ToString();
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Girth", ""));
                        root.SelectSingleNode("Girth").InnerXml = package.Girth.ToString();
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "CommercialFlag", ""));
                        root.SelectSingleNode("CommercialFlag").InnerXml = package.CommercialFlag.ToString();
                    }
                    else
                    {

                        decimal totalWeight = plist.Sum(item => item.Quantity * item.WeightValue);// plist.Sum(packageweight => packageweight.WeightValue);

                        if (package.WeightUnit.ToLower() == WeightUnits.POUNDS.ToString().ToLower() ||
                            package.WeightUnit.ToLower() == "pound" ||
                            package.WeightUnit.ToLower() == "lb" ||
                          package.WeightUnit.ToLower() == WeightUnits.LBS.ToString().ToLower())
                        {
                            //pound to ounce conversion
                            root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Pounds", ""));
                            root.SelectSingleNode("Pounds").InnerXml = totalWeight.ToString();

                            //applying only one weight is ok 
                            root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Ounces", ""));
                            root.SelectSingleNode("Ounces").InnerXml = "0";// (totalWeight * 16).ToString();
                        }
                        else if (package.WeightUnit.ToLower() == WeightUnits.KGS.ToString().ToLower() ||
                            package.WeightUnit.ToLower() == "kg")
                        {
                            //pound to ounce conversion
                            root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Pounds", ""));
                            root.SelectSingleNode("Pounds").InnerXml =
                                (double.Parse(totalWeight.ToString()) * 2.20462262185).ToString();

                            root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Ounces", ""));
                            root.SelectSingleNode("Ounces").InnerXml = "0";
                            // (double.Parse(totalWeight.ToString()) * 2.20462262185 * 16).ToString();
                        }
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "MailType", ""));
                        root.SelectSingleNode("MailType").InnerXml = package.MailType.ToString();
                        root.AppendChild(xx.CreateNode(XmlNodeType.Element, "Country", ""));
                        root.SelectSingleNode("Country").InnerXml = destination.ToCountryName;
                        wrap.AppendChild(root);
                        xx.AppendChild(wrap);

                        break;
                    }

                }


                //  root.AppendChild(node);
                wrap.AppendChild(root);
                xx.AppendChild(wrap);

                i++;
            }
            XmlDocument rateResponse = SendRequestToProvider(xx, api);
            //calling parsefunction
            if (!isDomestic)
            {
                if (_isItemWiseCalculation)
                    response = ParseRateInterNationalV2(rateResponse);
                else
                    response = ParseRateInterNational(rateResponse);
            }
            else
            {

                response = ParseRate(rateResponse);
            }

            return response;
        }

        private List<USPSRateResponse> ParseRateInterNationalV2(XmlDocument rateresponse)
        {
            List<USPSRateResponse> response = new List<USPSRateResponse>();

            USPSRateResponse ratedetail = new USPSRateResponse();
            var elem = rateresponse.DocumentElement;
            if (!rateresponse.InnerXml.Contains("<Error>"))
            {
                if (rateresponse.DocumentElement != null)
                {
                    XmlNodeList packges = rateresponse.DocumentElement.SelectNodes("Package");
                    if (packges != null)
                        foreach (XmlNode package in packges)
                        {

                            ratedetail.IsDomestic = false;
                            ratedetail.Prohibitions = package.SelectSingleNode("Prohibitions").InnerXml;
                            ratedetail.Restrictions = package.SelectSingleNode("Restrictions").InnerXml;
                            ratedetail.Observations = package.SelectSingleNode("Observations").InnerXml;
                            ratedetail.AdditionalRestrictions =
                                package.SelectSingleNode("AdditionalRestrictions").InnerXml;
                            ratedetail.AreasServed = package.SelectSingleNode("AreasServed").InnerXml;

                            XmlNodeList list = package.SelectNodes("Service");

                            var ilist = new List<InterNationalRateList>();
                            foreach (XmlNode node in list)
                            {
                                InterNationalRateList iRlist = new InterNationalRateList();
                                if (node.SelectSingleNode("SvcDescription") != null)
                                {
                                    if (node.SelectSingleNode("Pounds") != null)
                                        iRlist.Pounds = decimal.Parse(node.SelectSingleNode("Pounds").InnerXml);
                                    if (node.SelectSingleNode("Container") != null)
                                        iRlist.Container = node.SelectSingleNode("Container").InnerXml;
                                    if (node.SelectSingleNode("Size") != null)
                                        iRlist.Size = node.SelectSingleNode("Size").InnerXml;
                                    if (node.SelectSingleNode("Width") != null)
                                        iRlist.Width = node.SelectSingleNode("Width").InnerXml;
                                    if (node.SelectSingleNode("Length") != null)
                                        iRlist.Length = node.SelectSingleNode("Length").InnerXml;
                                    if (node.SelectSingleNode("Height") != null)
                                        iRlist.Height = node.SelectSingleNode("Height").InnerXml;
                                    if (node.SelectSingleNode("MailType") != null)
                                        iRlist.MailType = node.SelectSingleNode("MailType").InnerXml;
                                    if (node.SelectSingleNode("Country") != null)
                                        iRlist.Country = node.SelectSingleNode("Country").InnerXml;
                                    if (node.SelectSingleNode("Postage") != null)
                                        iRlist.TotalCharges = decimal.Parse(node.SelectSingleNode("Postage").InnerXml);
                                    if (node.SelectSingleNode("Girth") != null)
                                        iRlist.Girth = node.SelectSingleNode("Girth").InnerXml;
                                    if (node.SelectSingleNode("SvcCommitments") != null)
                                        iRlist.DeliveryTime = node.SelectSingleNode("SvcCommitments").InnerXml;

                                    string serv = "";
                                    Services.TryGetValue(node.SelectSingleNode("SvcDescription").InnerXml, out serv);
                                    iRlist.ShippingMethodName = serv;

                                    if (node.SelectSingleNode("MaxDimensions") != null)
                                        iRlist.MaxDimensions = node.SelectSingleNode("MaxDimensions").InnerXml;

                                    //  ratedetail.IntRateList.Add(iRlist);
                                    var asdf = new List<InterNationalRateList>();
                                    ilist.Add(iRlist);
                                }
                            }
                            ratedetail.IntRateList = ilist;
                            response.Add(ratedetail);
                        }
                }
            }

            return response;
        }

        private List<USPSRateResponse> ParseRateInterNational(XmlDocument rateresponse)
        {
            List<USPSRateResponse> response = new List<USPSRateResponse>();

            var ratedetail = new USPSRateResponse();
            var elem = rateresponse.DocumentElement;
            if (!rateresponse.InnerXml.Contains("<Error>"))
            {
                if (rateresponse.DocumentElement != null)
                {
                    XmlNodeList packges = rateresponse.DocumentElement.SelectNodes("Package");
                    if (packges != null)
                        foreach (XmlNode package in packges)
                        {

                            ratedetail.IsDomestic = false;
                            ratedetail.Prohibitions = package.SelectSingleNode("Prohibitions").InnerXml;
                            ratedetail.Restrictions = package.SelectSingleNode("Restrictions").InnerXml;
                            ratedetail.Observations = package.SelectSingleNode("Observations").InnerXml;
                            // ratedetail.AdditionalRestrictions =package.SelectSingleNode("AdditionalRestrictions").InnerXml;
                            ratedetail.AreasServed = package.SelectSingleNode("AreasServed").InnerXml;

                            XmlNodeList list = package.SelectNodes("Service");

                            var ilist = new List<InterNationalRateList>();
                            foreach (XmlNode node in list)
                            {
                                var iRlist = new InterNationalRateList();
                                if (node.SelectSingleNode("SvcDescription") != null)
                                {
                                    if (node.SelectSingleNode("Pounds") != null)
                                        iRlist.Pounds = decimal.Parse(node.SelectSingleNode("Pounds").InnerXml);

                                    if (node.SelectSingleNode("MailType") != null)
                                        iRlist.MailType = node.SelectSingleNode("MailType").InnerXml;
                                    if (node.SelectSingleNode("Country") != null)
                                        iRlist.Country = node.SelectSingleNode("Country").InnerXml;
                                    if (node.SelectSingleNode("Postage") != null)
                                        iRlist.TotalCharges = decimal.Parse(node.SelectSingleNode("Postage").InnerXml);

                                    if (node.SelectSingleNode("SvcCommitments") != null)
                                        iRlist.DeliveryTime = node.SelectSingleNode("SvcCommitments").InnerXml;

                                    string serv = "";
                                    Services.TryGetValue(node.SelectSingleNode("SvcDescription").InnerXml, out serv);
                                    iRlist.ShippingMethodName = serv;

                                    if (node.SelectSingleNode("MaxDimensions") != null)
                                        iRlist.MaxDimensions = node.SelectSingleNode("MaxDimensions").InnerXml;

                                    ilist.Add(iRlist);
                                }
                            }
                            ratedetail.IntRateList = ilist;
                            response.Add(ratedetail);
                        }
                }
            }
            else
            {
                XmlNodeList packges = rateresponse.DocumentElement.SelectNodes("Package");
                if (packges != null)
                    foreach (XmlNode package in packges)
                    {
                        var error = package.SelectSingleNode("Error");
                        string errDescription = error.SelectSingleNode("Description").InnerXml;
                        ratedetail.ErrorMessage = errDescription;
                        response.Add(ratedetail);
                    }

            }

            return response;
        }

        public List<USPSRateResponse> ParseRate(XmlDocument rateresponse)
        {
            List<USPSRateResponse> response = new List<USPSRateResponse>();

            USPSRateResponse ratedetail = new USPSRateResponse();
            var elem = rateresponse.DocumentElement;
            ratedetail.IsDomestic = true;
            if (rateresponse.DocumentElement != null)
            {
                XmlNodeList list = rateresponse.DocumentElement.SelectNodes("Package");
                if (list != null)
                    foreach (XmlNode node in list)
                    {
                        if (node.SelectSingleNode("ZipOrigination") != null)
                            ratedetail.OriginZip = node.SelectSingleNode("ZipOrigination").InnerXml;
                        if (node.SelectSingleNode("ZipDestination") != null)
                            ratedetail.DestZip = node.SelectSingleNode("ZipDestination").InnerXml;
                        if (node.SelectSingleNode("Pounds") != null)
                            ratedetail.Pounds = decimal.Parse(node.SelectSingleNode("Pounds").InnerXml);
                        if (node.SelectSingleNode("Ounces") != null)
                            ratedetail.Ounces = decimal.Parse(node.SelectSingleNode("Ounces").InnerXml);
                        if (node.SelectSingleNode("Container") != null)
                            ratedetail.Container = node.SelectSingleNode("Container").InnerXml;
                        if (node.SelectSingleNode("Size") != null)
                            ratedetail.Size = node.SelectSingleNode("Size").InnerXml;
                        if (node.SelectSingleNode("Zone") != null)
                            ratedetail.Zone = node.SelectSingleNode("Zone").InnerXml;


                        XmlNodeList postagelist = node.SelectNodes("Postage");
                        if (postagelist != null)
                            foreach (XmlNode postage in postagelist)
                            {
                                var p = new Postage();
                                if (postage.SelectSingleNode("MailService") != null)
                                {
                                    string serv = "";
                                    Services.TryGetValue(postage.SelectSingleNode("MailService").InnerXml.ToString(), out serv);
                                    p.ShippingMethodName = serv;
                                }

                                if (postage.SelectSingleNode("Rate") != null)
                                    p.TotalCharges = decimal.Parse(postage.SelectSingleNode("Rate").InnerXml);
                                ratedetail.Postage.Add(p);
                            }
                        response.Add(ratedetail);
                    }
            }

            return response;
        }

        #endregion

        #region ShipmentLabel
        private WareHouseAddress GetWareHouseAddress(int storeId, int portalId)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            WareHouseAddress cl = sqlHandler.ExecuteAsObject<WareHouseAddress>("[usp_Aspx_GetActiveWareHouse]", paramList);
            return cl;
        }


        public USPSShipmentResponse SendShipmentConfirmation(USPSShipment shipDetails, int providerId, int storeId, int portalId)
        {
            try
            {


                string api = "";
                _isShipment = true;
                XmlDocument shipment = new XmlDocument();
                XmlNode root = null;
                var domestic = true;
                LoadConfig(providerId, storeId, portalId);
                var wareHouseAddress = GetWareHouseAddress(storeId, portalId);
                if (wareHouseAddress.Country == "US" || wareHouseAddress.Country == "UNITED STATES")
                {
                    #region INTERNATIONAL
                    if (shipDetails.ToAddress.ToCountry.ToUpper().Trim() != "US" && shipDetails.ToAddress.ToCountry.ToUpper().Trim() != "UNITED STATES")
                    {
                        if (shipDetails.ServiceType.ToString().ToUpper().Contains("EXPRESS"))
                        {
                            root = shipment.CreateNode(XmlNodeType.Element, "ExpressMailIntlCertifyRequest", "");
                            api = "ExpressMailIntlCertify"; //
                        }
                        if (shipDetails.ServiceType.ToString().ToUpper().Contains("PRIORITY"))
                        {
                            root = shipment.CreateNode(XmlNodeType.Element, "PriorityMailIntlRequest", "");
                            api = "PriorityMailIntlCertify"; //
                        }
                        if (shipDetails.ServiceType.ToString().ToUpper().Contains("FIRSTCLASS"))
                        {
                            root = shipment.CreateNode(XmlNodeType.Element, "FirstClassMailIntlCertifyRequest", "");
                            api = "FirstClassMailIntlCertify"; //
                        }

                        XmlNode ids = shipment.CreateNode(XmlNodeType.Attribute, "USERID", "");
                        ids.Value = this._userId;
                        root.Attributes.SetNamedItem(ids); //save attribute value



                        domestic = false;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ImageParameters", ""));
                        // root.SelectSingleNode("Option").InnerXml = shipDetails.Option.ToString();

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromFirstName", ""));
                        root.SelectSingleNode("FromFirstName").InnerXml = shipDetails.FromAddress.FirstName;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromLastName", ""));
                        root.SelectSingleNode("FromLastName").InnerXml = shipDetails.FromAddress.LastName;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromFirm", ""));
                        root.SelectSingleNode("FromFirm").InnerXml = wareHouseAddress.Name;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromAddress1", ""));
                        root.SelectSingleNode("FromAddress1").InnerXml = wareHouseAddress.StreetAddress1;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromAddress2", ""));
                        root.SelectSingleNode("FromAddress2").InnerXml = wareHouseAddress.StreetAddress2;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromCity", ""));
                        root.SelectSingleNode("FromCity").InnerXml = wareHouseAddress.City;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromState", ""));
                        root.SelectSingleNode("FromState").InnerXml = wareHouseAddress.State;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromZip5", ""));
                        root.SelectSingleNode("FromZip5").InnerXml = wareHouseAddress.PostalCode;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromZip4", ""));
                        root.SelectSingleNode("FromZip4").InnerXml = shipDetails.FromAddress.ZipPlus4;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromPhone", ""));
                        root.SelectSingleNode("FromPhone").InnerXml = wareHouseAddress.Phone;


                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToName", ""));
                        root.SelectSingleNode("ToName").InnerXml = shipDetails.ToAddress.FirstName + " " +
                                                                   shipDetails.ToAddress.LastName;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToFirm", ""));
                        root.SelectSingleNode("ToFirm").InnerXml = shipDetails.ToAddress.Company;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToAddress1", ""));
                        root.SelectSingleNode("ToAddress1").InnerXml = shipDetails.ToAddress.ToAddress;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToAddress2", ""));
                        root.SelectSingleNode("ToAddress2").InnerXml = shipDetails.ToAddress.ToAddress2;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToAddress3", ""));
                        root.SelectSingleNode("ToAddress3").InnerXml = "";

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToCity", ""));
                        root.SelectSingleNode("ToCity").InnerXml = shipDetails.ToAddress.ToCity;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToProvince", ""));
                        root.SelectSingleNode("ToProvince").InnerXml = shipDetails.ToAddress.ToState;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToCountry", ""));
                        root.SelectSingleNode("ToCountry").InnerXml = shipDetails.ToAddress.ToCountry;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToPostalCode", ""));
                        root.SelectSingleNode("ToPostalCode").InnerXml = shipDetails.ToAddress.ToPostalCode;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToPOBoxFlag", ""));
                        root.SelectSingleNode("ToPOBoxFlag").InnerXml = "N";//Y OR NO

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToPhone", ""));
                        root.SelectSingleNode("ToPhone").InnerXml = shipDetails.ToAddress.Phone;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToFax", ""));
                        root.SelectSingleNode("ToFax").InnerXml = shipDetails.ToAddress.ToFax;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToEmail", ""));
                        root.SelectSingleNode("ToEmail").InnerXml = shipDetails.ToAddress.Email;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "NonDeliveryOption", ""));
                        root.SelectSingleNode("NonDeliveryOption").InnerXml = shipDetails.NonDeliveryOption.ToString();

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "Container", ""));
                        root.SelectSingleNode("Container").InnerXml = shipDetails.Container.ToString();



                        XmlNode
                            shippingContents = shipment.CreateNode(XmlNodeType.Element, "ShippingContents", "");

                        foreach (var item in shipDetails.Items)
                        {
                            XmlNode itemDetail = shipment.CreateNode(XmlNodeType.Element, "ItemDetail", "");

                            itemDetail.AppendChild(shipment.CreateNode(XmlNodeType.Element, "Description", ""));
                            itemDetail.SelectSingleNode("Description").InnerXml = item.Description;

                            itemDetail.AppendChild(shipment.CreateNode(XmlNodeType.Element, "Quantity", ""));
                            itemDetail.SelectSingleNode("Quantity").InnerXml = item.Quantity.ToString();

                            itemDetail.AppendChild(shipment.CreateNode(XmlNodeType.Element, "Value", ""));
                            itemDetail.SelectSingleNode("Value").InnerXml = item.Price.ToString();

                            itemDetail.AppendChild(shipment.CreateNode(XmlNodeType.Element, "NetPounds", ""));
                            itemDetail.SelectSingleNode("NetPounds").InnerXml = item.Weight.ToString();

                            itemDetail.AppendChild(shipment.CreateNode(XmlNodeType.Element, "NetOunces", ""));
                            itemDetail.SelectSingleNode("NetOunces").InnerXml = (item.Weight * 16).ToString();

                            itemDetail.AppendChild(shipment.CreateNode(XmlNodeType.Element, "CountryOfOrigin", ""));
                            itemDetail.SelectSingleNode("CountryOfOrigin").InnerXml = wareHouseAddress.Country;
                            shippingContents.AppendChild(itemDetail);
                        }

                        root.AppendChild(shippingContents);

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "GrossPounds", ""));
                        root.SelectSingleNode("GrossPounds").InnerXml = shipDetails.GrossPound.ToString();
                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "GrossOunces", ""));
                        root.SelectSingleNode("GrossOunces").InnerXml = shipDetails.GrossOunce.ToString();

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ContentType", ""));
                        root.SelectSingleNode("ContentType").InnerXml = shipDetails.ContentType.ToString();

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "Agreement", ""));
                        root.SelectSingleNode("Agreement").InnerXml = "Y"; //Y OR N

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "Comments", ""));
                        root.SelectSingleNode("Comments").InnerXml = "";//additional comments

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ImageType", ""));
                        root.SelectSingleNode("ImageType").InnerXml = shipDetails.ImageType.ToString();

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ImageLayout", ""));
                        root.SelectSingleNode("ImageLayout").InnerXml = shipDetails.ImageLayout.ToString();

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "POZipCode", ""));
                        root.SelectSingleNode("POZipCode").InnerXml = shipDetails.POZipCode;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "LabelDate", ""));
                        // root.SelectSingleNode("LabelDate").InnerXml = DateTime.Now.ToString("dd/mm/yy");

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "HoldForManifest", ""));
                        root.SelectSingleNode("HoldForManifest").InnerXml = "N"; //Y OR N

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "Length", ""));
                        root.SelectSingleNode("Length").InnerXml = shipDetails.Length.ToString("0.00");

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "Width", ""));
                        root.SelectSingleNode("Width").InnerXml = shipDetails.Width.ToString("0.00");

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "Height", ""));
                        root.SelectSingleNode("Height").InnerXml = shipDetails.Height.ToString("0.00");

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "Girth", ""));
                        root.SelectSingleNode("Girth").InnerXml = shipDetails.Grith.ToString("0.00");




                    }
                    #endregion
                    else
                    {
                        #region For DOMESTIC
                        if (_useTestMode)
                        {
                            if (shipDetails.Api.ToLower() == "signatureconfirmation")
                            {
                                root = shipment.CreateNode(XmlNodeType.Element, "SigConfirmCertifyV3.0Request", "");
                                api = "SignatureConfirmationCertifyV3";
                            }
                            else
                            {
                                root = shipment.CreateNode(XmlNodeType.Element, "DelivConfirmCertifyV3.0Request", "");
                                api = "DelivConfirmCertifyV3";
                            }

                        }
                        else
                        {
                            if (shipDetails.Api.ToLower() == "signatureconfirmation")
                            {
                                root = shipment.CreateNode(XmlNodeType.Element, "SignatureConfirmationV3.0Request", "");
                                api = "SignatureConfirmation";
                            }
                            else
                            {
                                root = shipment.CreateNode(XmlNodeType.Element, "DeliveryConfirmationV3.0Request", "");
                                api = "DeliveryConfirmationV3";
                            }

                        }
                        XmlNode ids = shipment.CreateNode(XmlNodeType.Attribute, "USERID", "");
                        ids.Value = this._userId;
                        root.Attributes.SetNamedItem(ids); //save attribute value



                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "Option", ""));
                        root.SelectSingleNode("Option").InnerXml = "1";
                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ImageParameters", ""));
                        //root.SelectSingleNode("ImageParameters").InnerXml = "";

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromName", ""));
                        root.SelectSingleNode("FromName").InnerXml = shipDetails.FromAddress.FirstName + " " + shipDetails.FromAddress.LastName;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromFirm", "")); //company
                        root.SelectSingleNode("FromFirm").InnerXml = wareHouseAddress.Name;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromAddress1", ""));
                        root.SelectSingleNode("FromAddress1").InnerXml = wareHouseAddress.StreetAddress1;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromAddress2", ""));
                        root.SelectSingleNode("FromAddress2").InnerXml = wareHouseAddress.StreetAddress2;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromCity", ""));
                        root.SelectSingleNode("FromCity").InnerXml = wareHouseAddress.City;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromState", ""));
                        root.SelectSingleNode("FromState").InnerXml = wareHouseAddress.State;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromZip5", ""));
                        root.SelectSingleNode("FromZip5").InnerXml = wareHouseAddress.PostalCode;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "FromZip4", ""));
                        root.SelectSingleNode("FromZip4").InnerXml = "";

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToName", ""));
                        root.SelectSingleNode("ToName").InnerXml = shipDetails.ToAddress.FirstName + " " + shipDetails.ToAddress.LastName;


                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToFirm", ""));
                        root.SelectSingleNode("ToFirm").InnerXml = shipDetails.ToAddress.Company;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToAddress1", ""));
                        root.SelectSingleNode("ToAddress1").InnerXml = shipDetails.ToAddress.ToAddress;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToAddress2", ""));
                        root.SelectSingleNode("ToAddress2").InnerXml = shipDetails.ToAddress.ToAddress2;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToCity", ""));
                        root.SelectSingleNode("ToCity").InnerXml = shipDetails.ToAddress.ToCity;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToState", ""));
                        root.SelectSingleNode("ToState").InnerXml = shipDetails.ToAddress.ToState;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToZip5", ""));
                        root.SelectSingleNode("ToZip5").InnerXml = shipDetails.ToAddress.ToPostalCode;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ToZip4", ""));
                        root.SelectSingleNode("ToZip4").InnerXml = shipDetails.ToAddress.ZipPlus4;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "WeightInOunces", ""));
                        root.SelectSingleNode("WeightInOunces").InnerXml = shipDetails.GrossOunce.ToString();

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ServiceType", ""));
                        root.SelectSingleNode("ServiceType").InnerXml = shipDetails.ServiceType.ToString();

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "SeparateReceiptPage", ""));
                        root.SelectSingleNode("SeparateReceiptPage").InnerXml = shipDetails.SeparateReceiptPage.ToString();

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "POZipCode", ""));
                        root.SelectSingleNode("POZipCode").InnerXml = shipDetails.POZipCode;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "ImageType", ""));
                        root.SelectSingleNode("ImageType").InnerXml = shipDetails.ImageType.ToString();

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "LabelDate", ""));
                        //root.SelectSingleNode("LabelDate").InnerXml = DateTime.Now.ToString("dd/mm/yy");

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "AddressServiceRequested", ""));
                        root.SelectSingleNode("AddressServiceRequested").InnerXml =
                            shipDetails.AddressServiceRequested.ToString();

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "SenderName", ""));
                        root.SelectSingleNode("SenderName").InnerXml = shipDetails.FromAddress.FirstName + " " + shipDetails.FromAddress.LastName;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "SenderEMail", ""));
                        root.SelectSingleNode("SenderEMail").InnerXml = shipDetails.FromAddress.Email;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "RecipientName", ""));
                        root.SelectSingleNode("RecipientName").InnerXml = shipDetails.ToAddress.FirstName + " " + shipDetails.ToAddress.LastName;

                        root.AppendChild(shipment.CreateNode(XmlNodeType.Element, "RecipientEMail", ""));
                        root.SelectSingleNode("RecipientEMail").InnerXml = shipDetails.ToAddress.Email;

                        #endregion
                    }

                    shipment.AppendChild(root);
                    labelExtention = shipDetails.ImageType.ToString().Trim();

                }

                XmlDocument shipmentResponse = SendRequestToProvider(shipment, api);
                USPSShipmentResponse shipmentinfo
                    = domestic ? ParseShipmentResponse(shipmentResponse) : ParseShipmentResponseIntl(shipmentResponse);
                // SaveLabel(shipmentinfo.DeliveryConfirmationLabel, shipmentinfo.Label);
                return shipmentinfo;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public USPSShipmentResponse ParseShipmentResponseIntl(XmlDocument shipmentResponse)
        {
            USPSShipmentResponse shipmentinfo = new USPSShipmentResponse();
            shipmentinfo.IsDomestic = false;
            XmlElement elem = shipmentResponse.DocumentElement;
            if (elem != null)
            {
                if (elem.Name.ToLower().Trim() != "error")
                {
                    if (elem.SelectSingleNode("Postage") != null)
                        shipmentinfo.IntlResponse.Postage = decimal.Parse(elem.SelectSingleNode("Postage").InnerXml);
                    //if (elem.SelectSingleNode("TotalValue") != null)
                    //    shipmentinfo.Postnet = elem.SelectSingleNode("TotalValue").InnerXml;
                    if (elem.SelectSingleNode("BarcodeNumber") != null)
                        shipmentinfo.IntlResponse.BarcodeNumber = elem.SelectSingleNode("BarcodeNumber").InnerXml;
                    if (elem.SelectSingleNode("SDRValue") != null)
                        shipmentinfo.IntlResponse.SDRValue = elem.SelectSingleNode("SDRValue").InnerXml;
                    if (elem.SelectSingleNode("Regulations") != null)
                        shipmentinfo.IntlResponse.Regulation = elem.SelectSingleNode("Regulations").InnerXml;
                    if (elem.SelectSingleNode("Prohibitions") != null)
                        shipmentinfo.IntlResponse.Prohibitions = elem.SelectSingleNode("Prohibitions").InnerXml;
                    if (elem.SelectSingleNode("Restrictions") != null)
                        shipmentinfo.IntlResponse.Restrictions = elem.SelectSingleNode("Restrictions").InnerXml;
                    if (elem.SelectSingleNode("Observations") != null)
                        shipmentinfo.IntlResponse.Observations = elem.SelectSingleNode("Observations").InnerXml;
                    if (elem.SelectSingleNode("AdditionalRestrictions") != null)
                        shipmentinfo.IntlResponse.AdditionalRestrictions =
                            elem.SelectSingleNode("AdditionalRestrictions").InnerXml;

                    if (elem.SelectSingleNode("LabelImage") != null)
                    {
                        var encoding = new ASCIIEncoding();
                        shipmentinfo.LabelString = elem.SelectSingleNode("LabelImage").InnerXml;
                        shipmentinfo.IntlResponse.Label =
                            Convert.FromBase64String(elem.SelectSingleNode("LabelImage").InnerXml.ToString().Trim());


                    }
                    //::TODO not checked yet InterNational Mode
                    shipmentinfo.TempLabelPath = SaveLabel(shipmentinfo.DeliveryConfirmationNumber, shipmentinfo.IntlResponse.Label);

                }
                else
                {
                    string error = elem.InnerXml;
                    shipmentinfo.IsFailed = true;
                    shipmentinfo.Error = error;
                }
            }
            return shipmentinfo;

        }

        public USPSShipmentResponse ParseShipmentResponse(XmlDocument shipmentResponse)
        {
            USPSShipmentResponse shipmentinfo = new USPSShipmentResponse();
            shipmentinfo.IsDomestic = true;
            XmlElement elem = shipmentResponse.DocumentElement;
            if (elem != null)
            {
                if (elem.Name.ToLower().Trim() != "error")
                {

                    if (elem.SelectSingleNode("DeliveryConfirmationNumber") != null)
                        shipmentinfo.DeliveryConfirmationNumber =
                            elem.SelectSingleNode("DeliveryConfirmationNumber").InnerXml;
                    if (elem.SelectSingleNode("Postnet") != null)
                        shipmentinfo.Postnet = elem.SelectSingleNode("Postnet").InnerXml;
                    if (elem.SelectSingleNode("ToName") != null)
                        shipmentinfo.ToName = elem.SelectSingleNode("ToName").InnerXml;
                    if (elem.SelectSingleNode("ToFirm") != null)
                        shipmentinfo.ToFirm = elem.SelectSingleNode("ToFirm").InnerXml;
                    if (elem.SelectSingleNode("ToAddress1") != null)
                        shipmentinfo.ToAddress1 = elem.SelectSingleNode("ToAddress1").InnerXml;
                    if (elem.SelectSingleNode("ToAddress2") != null)
                        shipmentinfo.ToAddress2 = elem.SelectSingleNode("ToAddress2").InnerXml;
                    if (elem.SelectSingleNode("ToState") != null)
                        shipmentinfo.ToState = elem.SelectSingleNode("ToState").InnerXml;
                    if (elem.SelectSingleNode("ToZip4") != null)
                        shipmentinfo.ToZip4 = elem.SelectSingleNode("ToZip4").InnerXml;
                    if (elem.SelectSingleNode("DeliveryConfirmationLabel") != null)
                    {
                        var encoding = new ASCIIEncoding();
                        shipmentinfo.LabelString = elem.SelectSingleNode("DeliveryConfirmationLabel").InnerXml;
                        shipmentinfo.Label =
                            Convert.FromBase64String(
                                elem.SelectSingleNode("DeliveryConfirmationLabel").InnerXml.ToString().Trim());


                    }
                    shipmentinfo.TempLabelPath = SaveLabel(shipmentinfo.DeliveryConfirmationNumber, shipmentinfo.Label);
                }
                else
                {
                    string error = elem.InnerXml;
                    shipmentinfo.IsFailed = true;
                    shipmentinfo.Error = error;
                }
            }

            return shipmentinfo;

        }

        //labelFileName = save location with name c://asdf.pdf
        private string SaveLabel(string labelFileName, byte[] labelBuffer)
        {
            if (labelFileName != null && labelBuffer != null)
            {
                string tempFile = "Temp\\" + labelFileName + "." + labelExtention;
                string fileSaveLocation = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp");
                if (!Directory.Exists(fileSaveLocation))
                {
                    Directory.CreateDirectory(fileSaveLocation);
                }
                string file = fileSaveLocation + "\\" + labelFileName + "." + labelExtention;
                // Save label buffer to file
                var labelFile = new FileStream(file, FileMode.Create);
                labelFile.Write(labelBuffer, 0, labelBuffer.Length);
                labelFile.Close();
                // Display label in Acrobat
                //DisplayLabel(file);
                return tempFile;
            }
            return null;
        }

        private void DisplayLabel(string labelFileName)
        {
            //var info = new System.Diagnostics.ProcessStartInfo(labelFileName) { UseShellExecute = false, Verb = "open" };
            //System.Diagnostics.Process.Start(info);
            string fullPath = labelFileName;
            // Next, we create a physical path from the virtual pat
            // Create a FileInfo object and write it to the response
            FileInfo theFile = new FileInfo(fullPath);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.WriteFile(theFile.FullName);
            HttpContext.Current.Response.End();
        }

        #endregion
    }




}
