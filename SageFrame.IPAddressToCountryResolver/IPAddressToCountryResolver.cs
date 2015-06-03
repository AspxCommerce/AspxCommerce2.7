#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Net;
#endregion

namespace SageFrame.Framework
{
    /// <summary>
    /// IPAddress to country resolver.
    /// </summary>
    public class IPAddressToCountryResolver
    {
        /// <summary>
        /// Initializes a new instance of the IPAddressToCountryResolver class.
        /// </summary>
        public IPAddressToCountryResolver() { }
        /// <summary>
        /// Return country name from user host IPAddress.
        /// </summary>
        /// <param name="userHostIpAddress">User host IP Address.</param>
        /// <param name="countryName">Outpt parameter countryName.</param>
        /// <returns>Country name.</returns>
        public bool GetCountry(string userHostIpAddress, out string countryName)
        {
            bool result = false;
            countryName = string.Empty;

            if (string.IsNullOrEmpty(userHostIpAddress))
                return false;

            IPAddress ipAddress;
            userHostIpAddress = GetIP4Address(userHostIpAddress);
                if (IPAddress.TryParse(userHostIpAddress, out ipAddress))
                {
                    countryName = ipAddress.Country();

                    result = true;
                }
           

            return result;
        }
        /// <summary>
        /// Return IP V4 address.
        /// </summary>
        /// <param name="ipaddress">IP address.</param>
        /// <returns>IP V4 address.</returns>
        public static string GetIP4Address(string ipaddress)
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(ipaddress))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            if (IP4Address != String.Empty)
            {
                return IP4Address;
            }

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }
    }

}
