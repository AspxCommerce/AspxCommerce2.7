using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.CDN
{ 
    /// <summary>
    ///  This class holds the properties for CDN.
    /// </summary>
    public class CDNInfo
    {  
        /// <summary>
        /// Gets or sets URLID.
        /// </summary>
        public int URLID { get; set; }
        /// <summary>
        /// Gets or sets URLID.
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// Gets or sets boolean value which is true if the Link is of Jquery.
        /// </summary>
        public bool IsJS { get; set; }
        /// <summary>
        /// Gets or sets URLOrder.
        /// </summary>
        public int URLOrder { get; set; }
        /// <summary>
        /// Gets or sets PortalID.
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Gets or sets login value.
        /// </summary>
        public string Mode { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the CDNInfo class.
        /// </summary>

        public CDNInfo() { }
    }
}
