using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageFrame.Security.Entities
{
    /// <summary>
    /// This class holds the properties of SuspendedIPInfo.
    /// </summary>
    public class SuspendedIPInfo
    {
        /// <summary>
        /// Get or set IpAddressID
        /// </summary>
        public int IPAddressID { get; set; }
        /// <summary>
        /// Get or set IpAddress
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// Get or set SuspendedTime
        /// </summary>
        public string SuspendedTime { get; set; }
        /// <summary>
        /// Get or set IsSuspended
        /// </summary>
        public bool IsSuspended { get; set; }
        /// <summary>
        /// Initializes a new instance of the SuspendedIPInfo class.
        /// </summary>
        public SuspendedIPInfo() { }
    }
}
