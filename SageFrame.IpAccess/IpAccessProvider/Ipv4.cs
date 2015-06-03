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
using System.Web;
using SageFrame.Web.Utilities;
#endregion

namespace SageFrame.IpAccess
{
    public class Ipv4
    {
        System.Net.IPAddress _ipAddress = null;
        private bool IsValidIpAddress(string ipAddress)
        {
            bool isValidIp = System.Net.IPAddress.TryParse(ipAddress, out _ipAddress);
            return isValidIp;
        }

        public bool IsAccessIpAddress(string ipAddress, int portalId)
        {
            bool isAccess = false;
            if (IsValidIpAddress(ipAddress))
            {
                switch (_ipAddress.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        // we have IPv4
                        isAccess = CheckIp(_ipAddress, portalId);
                        break;
                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                        // we have IPv6
                        isAccess = false;
                        break;
                }

            }
            return isAccess;
        }

        private bool CheckIp(System.Net.IPAddress ipAddress, int portalId)
        {
            try
            {
                var paraMeterCollection = new List<KeyValuePair<string, object>>
                                              {
                                                  new KeyValuePair<string, object>("@IPAddress", ipAddress.ToString()),
                                                  new KeyValuePair<string, object>("@PortalID", portalId)
                                              };
                var sqlH = new SQLHandler();
                var value = sqlH.ExecuteAsScalar<bool>("dbo.usp_sf_CheckIpAccess", paraMeterCollection);
                return value;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}

