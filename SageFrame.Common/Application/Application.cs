#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Web;
using System.Reflection;
using System.Net;
using System.Text;
using SageFrame.Web;
using Microsoft.Win32;
using System.Globalization;
#endregion

namespace SageFrame.Application
{
    /// <summary>
    /// Application summary
    /// </summary>
    [Serializable]
    public class Application
    {
        private static ReleaseMode _status = ReleaseMode.None;

        private System.Version _NETFrameworkVersion = System.Environment.Version;
        private string _ApplicationPath = string.Empty;
        private string _ApplicationMapPath = string.Empty;

        /// <summary>
        /// Initializes a new instance of the application class.
        /// </summary>
        public Application()
        {
            NETFrameworkIISVersion = GetNETFrameworkVersion();
            _ApplicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;

            _ApplicationMapPath = System.AppDomain.CurrentDomain.BaseDirectory.Substring(0, (System.AppDomain.CurrentDomain.BaseDirectory.Length - 1));
            _ApplicationMapPath = ApplicationMapPath.Replace("/", "\\");

        }
        /// <summary>
        /// Returns CurrentDotNetVersion.  
        /// </summary>
        /// <returns>string</returns>
        public static string CurrentDotNetVersion()
        {
            RegistryKey installed_versions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
            string[] version_names = installed_versions.GetSubKeyNames();
            //version names start with 'v', eg, 'v3.5' which needs to be trimmed off before conversion
            decimal Framework = Convert.ToDecimal(version_names[version_names.Length - 1].Remove(0, 1), CultureInfo.InvariantCulture);
            int SP = Convert.ToInt32(installed_versions.OpenSubKey(version_names[version_names.Length - 1]).GetValue("SP", 0));
            return Framework.ToString();
        }
        /// <summary>
        /// Get or set NETFrameworkIISVersion.
        /// </summary>
        public System.Version NETFrameworkIISVersion
        {
            get
            {
                return _NETFrameworkVersion;
            }
            set
            {
                _NETFrameworkVersion = value;
            }
        }
        /// <summary>
        /// Get or set ApplicationPath.
        /// </summary>
        public string ApplicationPath
        {
            get
            {
                return _ApplicationPath;
            }
            set
            {
                _ApplicationPath = value;
            }
        }
        /// <summary>
        /// Get or set ApplicationMapPath.
        /// </summary>
        public string ApplicationMapPath
        {
            get
            {
                return _ApplicationMapPath;
            }
            set
            {
                _ApplicationMapPath = value;
            }
        }
        /// <summary>
        /// Get company name.
        /// </summary>
        public string Company
        {
            get
            {
                return "BrainDigit Pty.";
            }
        }
        /// <summary>
        /// Get description.
        /// </summary>
        public string Description
        {
            get
            {
                return "SageFrame Community Edition";
            }
        }
        /// <summary>
        /// Get help URL.
        /// </summary>
        public string HelpUrl
        {
            get
            {
                return "http://www.sageframe.com/default.aspx";
            }
        }
        /// <summary>
        /// Get copyright information.
        /// </summary>
        public string LegalCopyright
        {
            get
            {
                return ("SageFrame� is copyright 2010-"
                            + (DateTime.Today.ToString("yyyy") + " by SageFrame Corporation"));
            }
        }
        /// <summary>
        /// Get corporation name.
        /// </summary>
        public string Name
        {
            get
            {
                return "SFECORP.CE";
            }
        }
        /// <summary>
        /// Get sageframe extension.
        /// </summary>
        public string SKU
        {
            get
            {
                return "SFE";
            }
        }
        /// <summary>
        /// Get SageFrame application release mode.
        /// </summary>
        public ReleaseMode Status
        {
            get
            {
                if ((_status == ReleaseMode.None))
                {
                    Assembly assy = System.Reflection.Assembly.GetExecutingAssembly();
                    if (Attribute.IsDefined(assy, typeof(AssemblyStatusAttribute)))
                    {
                        Attribute attr = Attribute.GetCustomAttribute(assy, typeof(AssemblyStatusAttribute));
                        if (attr != null)
                        {
                            _status = ((AssemblyStatusAttribute)(attr)).Status;
                        }
                    }
                }
                return _status;
            }
        }
        /// <summary>
        /// Get sageframe title.
        /// </summary>
        public string Title
        {
            get
            {
                return "SageFrame";
            }
        }
        /// <summary>
        /// Get SageFrame registered trademark.
        /// </summary>
        public string Trademark
        {
            get
            {
                return "SageFrame,SFE";
            }
        }
        /// <summary>
        /// Get SageFrame type.
        /// </summary>
        public string Type
        {
            get
            {
                return "Framework";
            }
        }
        /// <summary>
        /// Get SageFrame upgrade URL.
        /// </summary>
        public string UpgradeUrl
        {
            get
            {
                return "http://update.sageframe.com";
            }
        }
        /// <summary>
        /// Get SageFrame URL.
        /// </summary>
        public string Url
        {
            get
            {
                return "http://www.sageframe.com";
            }
        }
        /// <summary>
        /// Get assembly version.
        /// </summary>
        public System.Version Version
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
        }
        /// <summary>
        /// Get sqldataprovider
        /// </summary>
        public string DataProvider
        {
            get { return "SqlDataProvider"; }
        }
        /// <summary>
        /// Returns application version.  
        /// </summary>
        /// <param name="version">Represent the version number.</param>
        /// <param name="includeBuild">Boolean variable to check for inculding a version.</param>
        /// <returns></returns>
        public string FormatVersion(System.Version version, bool includeBuild)
        {
            string strVersion = (version.Major.ToString("00") + ("." + (version.Minor.ToString("00") + ("." + version.Build.ToString("00")))));
            if (includeBuild)
            {
                strVersion = (strVersion + (" (" + (version.Revision.ToString() + ")")));
            }
            return strVersion;
        }
        /// <summary>
        ///  Returns application version of shorter formats.
        /// </summary>
        /// <param name="version">Represent the version number.</param>
        /// <param name="includeBuild">Boolean variable to check for inculding a version.</param>
        /// <returns>Short version.</returns>
        public string FormatShortVersion(System.Version version, bool includeBuild)
        {
            
            string strVersion = (version.Major.ToString("0") + ("." + (version.Minor.ToString("0"))));
            return strVersion;
        }
        /// <summary>
        /// Return .NET framework version.
        /// </summary>
        /// <returns>.NET framework version. </returns>
        private static System.Version GetNETFrameworkVersion()
        {
            string version = System.Environment.Version.ToString(2);
            // Try and load a 3.0 Assembly
            System.Reflection.Assembly assembly;
            try
            {
                assembly = AppDomain.CurrentDomain.Load("System.Runtime.Serialization, Version=3.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089");
                version = "3.0";
            }
            catch
            {
            }
            // Try and load a 3.5 Assembly
            try
            {
                assembly = AppDomain.CurrentDomain.Load("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089");
                version = "3.5";
            }
            catch
            {
            }
            // Try and load a 4.0 Assembly
            try
            {
                assembly = AppDomain.CurrentDomain.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089");
                version = "4.0";
            }
            catch
            {
            }
            return new System.Version(version);
        }
        /// <summary>
        /// Get sever IP Address
        /// </summary>
        public string ServerIPAddress
        {
            get
            {
                IPAddress[] IPList = Dns.GetHostEntry(DNSName).AddressList;
                StringBuilder strIpAddress = new StringBuilder();
                string strTemp = string.Empty;
                if (IPList.Length > 0)
                {
                    foreach (IPAddress ip in IPList)
                    {
                        strIpAddress.Append(ip.ToString() + ", ");
                    }
                    strTemp = strIpAddress.ToString();
                    if (strTemp.Contains(","))
                        strTemp = strTemp.Remove(strTemp.LastIndexOf(","));
                }
                return strTemp;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DNSName
        {
            get
            {
                return Dns.GetHostName();
            }
        }
    }
}
