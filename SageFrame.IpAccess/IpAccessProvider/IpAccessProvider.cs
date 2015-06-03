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
    /// Manupulates data for IpAccessController.
    /// </summary>
    public class IpAccessProvider
    {
        /// <summary>
        /// Initializes a new instance of the IpAccessProvider.
        /// </summary>
        public IpAccessProvider() { }
        /// <summary>
        ///  Connects to database  and return List of access IP list.
        /// </summary>
        /// <param name="portalId">PortalID</param>
        /// <returns>Access IP list.</returns>
        public List<IpRangeInfo> GetAccessIpList(int portalId)
        {
            try
            {
                var parameter = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@PortalId", portalId) };
                var sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<IpRangeInfo>("[usp_sf_GetAllAccessIpList]", parameter);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        ///  Connects to database  and save access IP list.
        /// </summary>
        /// <param name="ipinfo">Object of IpRangeInfo.</param>
        /// <param name="portalId">PortalID</param>
        /// <param name="userName">User name.</param>
        public void SaveIpToAccess(IpRangeInfo ipinfo, int portalId, string userName)
        {
            try
            {
                var parameter = new List<KeyValuePair<string, object>>
                                    {
                                        new KeyValuePair<string, object>("@IpAccessId", ipinfo.IpAccessId),
                                        new KeyValuePair<string, object>("@IpFrom", ipinfo.IpFrom),
                                        new KeyValuePair<string, object>("@IpTo", ipinfo.IpTo),
                                        new KeyValuePair<string, object>("@Reason", ipinfo.Reason),
                                        new KeyValuePair<string, object>("@IsActive", ipinfo.IsActive),
                                        new KeyValuePair<string, object>("@PortalId", portalId),
                                        new KeyValuePair<string, object>("@UserName", userName)
                                    };
                var sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[usp_sf_SaveIpToAccess]", parameter);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Connects to database abd delete access IP list.
        /// </summary>
        /// <param name="ids">Multiple ids with comma separator.</param>
        /// <param name="portalId">PortalID</param>
        /// <param name="userName">User name.</param>
        public void DeleteAccessIp(string ids, int portalId, string userName)
        {
            try
            {
                var parameter = new List<KeyValuePair<string, object>>
                                    {
                                        new KeyValuePair<string, object>("@IpAccessId", ids),
                                        new KeyValuePair<string, object>("@PortalId", portalId),
                                        new KeyValuePair<string, object>("@UserName", userName)
                                    };
                var sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[usp_sf_DeleteBlockedIp]", parameter);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        ///  Connects to database  return true if given IP range is exist.
        /// </summary>
        /// <param name="ipfrom">Ip rnge from.</param>
        /// <param name="ipTo">IP range to.</param>
        /// <param name="portalId">PortalID</param>
        /// <returns> True if given IP range is exist</returns>
        public bool IsExistIpRange(string ipfrom, string ipTo, int portalId)
        {
            try
            {
                var parameter = new List<KeyValuePair<string, object>>
                                    {
                                        new KeyValuePair<string, object>("@IpFrom", ipfrom),
                                        new KeyValuePair<string, object>("@PortalId", portalId),
                                        new KeyValuePair<string, object>("@IpTo", ipTo)
                                    };
                var sqlH = new SQLHandler();
                return sqlH.ExecuteAsScalar<bool>("[usp_sf_checkIpExists]", parameter);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
