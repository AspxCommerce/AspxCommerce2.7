using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.CDN
{
    public class CDNInfo
    {
        
        public int URLID { get; set; }
        public string URL { get; set; }
        public bool IsJS { get; set; }
        public int URLOrder { get; set; }
        public int PortalID { get; set; }
        public string Mode { get; set; }


        public CDNInfo() { }
    }
}
