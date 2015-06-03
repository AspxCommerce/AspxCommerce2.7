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
using SageFrame.Web.Utilities;
#endregion

namespace SageFrame.IpAccess
{
    /// <summary>
    /// Business logic for IpAccessController.
    /// </summary>
    public class IpAccessController
    {
        /// <summary>
        ///  Initializes a new instance of the IpAccessController.
        /// </summary>
        public IpAccessController() { }
        /// <summary>
        /// Return List of access IP.
        /// </summary>
        /// <param name="portalId">PortalID</param>
        /// <returns>Access IP list</returns>
        public List<IpRangeInfo> GetAccessIpList(int portalId)
        {
            var provider = new IpAccessProvider();
            return provider.GetAccessIpList(portalId);
            
        }
        /// <summary>
        /// Save access IP list.
        /// </summary>
        /// <param name="ipinfo">Object of IpRangeInfo.</param>
        /// <param name="portalId">PortalID</param>
        /// <param name="userName">User name.</param>
        public void SaveIpToAccess(IpRangeInfo ipinfo, int portalId, string userName)
        {
            var provider = new IpAccessProvider();
            provider.SaveIpToAccess(ipinfo, portalId, userName);
        }
        /// <summary>
        /// Delete access IP list.
        /// </summary>
        /// <param name="ids">Multiple ids with comma separator.</param>
        /// <param name="portalId">PortalID</param>
        /// <param name="userName">User name.</param>
        public void DeleteAccessIp(string ids, int portalId, string userName)
        {
            var provider = new IpAccessProvider();
            provider.DeleteAccessIp(ids, portalId, userName);
        }
        /// <summary>
        /// Return true if given IP range is exist.
        /// </summary>
        /// <param name="ipfrom">Ip rnge from.</param>
        /// <param name="ipTo">IP range to.</param>
        /// <param name="portalId">PortalID</param>
        /// <returns> True if given IP range is exist</returns>
        public bool IsExistIpRange(string ipfrom, string ipTo, int portalId)
        {
            var provider = new IpAccessProvider();
            return provider.IsExistIpRange(ipfrom, ipTo, portalId);
        }
        /// <summary>
        ///  Return true if given IP is accessable.
        /// </summary>
        /// <param name="ipAddress">IP address.</param>
        /// <param name="portalId">PortalID</param>
        /// <returns>True if given IP is exist.</returns>
        public bool CheckIpAccess(string ipAddress, int portalId)
        {
            var ipChecker = new Ipv4();
            return ipChecker.IsAccessIpAddress(ipAddress, portalId);
        }
    }
}
