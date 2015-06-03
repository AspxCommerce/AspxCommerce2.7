using System;
using System.Collections.Generic;
using System.Text;

namespace DashBoardControl
{
    /// <summary>
    /// This class holds the properties for DashBoardInfo.
    /// </summary>
    public class DashBoardInfo
    {   
        /// <summary>
        /// Gets or sets browser.
        /// </summary>
        public string Browser { get; set; }
        /// <summary>
        /// Gets or sets visited time.
        /// </summary>
        public string VisitTime { get; set; }
        /// <summary>
        /// Gets or sets visited page.
        /// </summary>
        public string VisitPage { get; set; }
        /// <summary>
        /// Gets or sets user IP.
        /// </summary>
        public string  SessionUserHostAddress { get; set; }
        /// <summary>
        /// Gets or sets country.
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Initializes a new instance of the DashBoardInfo class.
        /// </summary>
        public DashBoardInfo() { }
    }
}
