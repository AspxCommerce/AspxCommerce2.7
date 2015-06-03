#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace SageFrame.IpAccess
{
    /// <summary>
    /// This class holds the properties of IpRangeInfo.
    /// </summary>
    public class IpRangeInfo
    {
        /// <summary>
        ///Get or set IP access ID.
        /// </summary>
        public int IpAccessId { get; set; }
        /// <summary>
        /// Get or set IP from.
        /// </summary>
        public string IpFrom { get; set; }
        /// <summary>
        /// Get or set IP to.
        /// </summary>
        public string IpTo { get; set; }
        /// <summary>
        /// Get or set IP reason.
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// get or set true if IP range is active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
