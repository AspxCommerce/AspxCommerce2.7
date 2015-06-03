
using System;
using System.Collections.Generic;
using System.Text;

namespace AspxCommerce.USPS
{
    public class TrackingInfo
    {
        public TrackingInfo()
        {
            _details = new List<string>();
            IsFailed = false;
        }
        public bool IsFailed{get;set;}
        public string Error { get; set; }


        private string _trackingNumber;
        /// <summary>
        /// The tracking number for the package
        /// </summary>
        public string TrackingNumber
        {
            get { return _trackingNumber; }
            set { _trackingNumber = value; }
        }

        private string _summary;
        /// <summary>
        /// Summary information for the package
        /// </summary>
        public string Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }

        private List<string> _details;
        /// <summary>
        /// Tracking Details
        /// </summary>
        public List<string> Details
        {
            get { return _details; }
            set { _details = value; }
        }

        public static TrackingInfo FromXml(string xml)
        {
            int idx1 = 0;
            int idx2 = 0;
            var t = new TrackingInfo();
            if(xml.Contains("<TrackSummary>"))
            {
                idx1 = xml.IndexOf("<TrackSummary>") + 14;
                idx2 = xml.IndexOf("</TrackSummary>");
                t._summary = xml.Substring(idx1, idx2 - idx1);
            }
            int lastidx = 0;
            while (xml.IndexOf("<TrackDetail>", lastidx) > -1)
            {
                idx1 = xml.IndexOf("<TrackDetail>", lastidx) + 13;
                idx2 = xml.IndexOf("</TrackDetail>", lastidx + 13);
                t.Details.Add(xml.Substring(idx1, idx2 - idx1));
                lastidx = idx2;
            }
            return t;
        }


    }
}
