#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Linq;
using SageFrame.Setting;
using System.Xml;
using System.Text;
#endregion

/// <summary>
/// Summary description for CommonHelper
/// </summary>
/// 
namespace SageFrame.Web
{
    /// <summary>
    /// Common helper.
    /// </summary>
    public static class CommonHelper
    {
        /// <summary>
        /// Return server information based on server variable.
        /// </summary>
        /// <param name="Name">Applied server variable name.</param>
        /// <returns>server information.</returns>
        public static string ServerVariables(string Name)
        {
            string tmpS = String.Empty;
            try
            {
                if (HttpContext.Current.Request.ServerVariables[Name] != null)
                {

                    tmpS = HttpContext.Current.Request.ServerVariables[Name].ToString();

                }
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }
        /// <summary>
        /// Ensures establishing an encrypted link between a server and a client (Secure Sockets Layer) .
        /// </summary>
        /// <param name="isSecureConnect">"true" for secure connection.</param>
        /// <param name="redirectPath">Redirect path.</param>
        public static void EnsureSSL(bool isSecureConnect, string redirectPath)
        {
            if (!HttpContext.Current.Request.IsSecureConnection)
            {
                if (!HttpContext.Current.Request.Url.IsLoopback) //Don't check when in development mode (i.e. localhost)
                {
                    ReloadCurrentPage(isSecureConnect, redirectPath);
                }
            }
        }
        /// <summary>
        /// Reload current page.
        /// </summary>
        /// <param name="UseSSL">"True" for use SSL</param>
        /// <param name="redirectPath">Redirect path.</param>
        public static void ReloadCurrentPage(bool UseSSL, string redirectPath)
        {
            string result = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_HOST"] != null)
            {
                result = HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString();
            }
            result = "http://" + result;
            if (!result.EndsWith("/"))
            {
                result += "/";
            }

            if (UseSSL)
            {
                result = result.Replace("http:", "https:");
                result = result.Replace("www.www", "www");
            }

            if (result.EndsWith("/"))
            {
                result = result.Substring(0, result.Length - 1);
            }
            string URL = result + HttpContext.Current.Request.RawUrl;
            HttpContext.Current.Response.Redirect(URL);
        }

        /// <summary>
        /// Get store location.
        /// </summary>
        /// <param name="UseSSL">"True" for enable SSL.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Application path.</returns>
        public static string GetStoreLocation(bool UseSSL, int PortalID)
        {
            string result = GetStoreHost(UseSSL, PortalID);
            if (result.EndsWith("/"))
                result = result.Substring(0, result.Length - 1);
            result = result + HttpContext.Current.Request.ApplicationPath;
            if (!result.EndsWith("/"))
                result += "/";

            return result;
        }
        /// <summary>
        /// Get admin store location.
        /// </summary>
        /// <param name="UseSSL">"True" for enable SSL.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Application path.</returns>
        public static string GetStoreAdminLocation(bool UseSSL, int PortalID)
        {
            string result = GetStoreLocation(UseSSL, PortalID);
            result += "Administration/";

            return result;
        }
        /// <summary>
        /// Return QueryString
        /// </summary>
        /// <param name="Name">QueryString name.</param>
        /// <returns>Return string value of  QueryString  based on it's name.</returns>
        public static string QueryString(string Name)
        {
            string result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString[Name] != null)
                result = HttpContext.Current.Request.QueryString[Name].ToString();
            return result;
        }
        /// <summary>
        /// Return QueryString
        /// </summary>
        /// <param name="Name">QueryString name.</param>
        /// <returns>Return int value of  QueryString  based on it's name.</returns>
        public static int QueryStringInt(string Name)
        {
            string resultStr = QueryString(Name).ToUpperInvariant();
            int result;
            Int32.TryParse(resultStr, out result);
            return result;
        }

        /// <summary>
        /// Return page name base on PortalID
        /// </summary>
        /// <param name="includeQueryString">"True" for includeQueryString.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Page URL.</returns>
        public static string GetThisPageURL(bool includeQueryString, int PortalID)
        {
            string URL = string.Empty;
            if (HttpContext.Current == null)
                return URL;

            if (includeQueryString)
            {
                string storeHost = GetStoreHost(false, PortalID);
                if (storeHost.EndsWith("/"))
                    storeHost = storeHost.Substring(0, storeHost.Length - 1);
                URL = storeHost + HttpContext.Current.Request.RawUrl;
            }
            else
            {
                URL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            }
            return URL;
        }

        /// <summary>
        /// Write XML file. 
        /// </summary>
        /// <param name="xml">XML</param>
        /// <param name="Filename">Filename.</param>
        public static void WriteResponseXML(string xml, string Filename)
        {
            if (!String.IsNullOrEmpty(xml))
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                ((XmlDeclaration)document.FirstChild).Encoding = "utf-8";
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/xml";
                response.AddHeader("content-disposition", string.Format("attachment; filename={0}", Filename));
                response.BinaryWrite(Encoding.UTF8.GetBytes(document.InnerXml));
                response.End();
            }
        }

        public static DateTime AvailableStartDateTime = DateTime.Parse("01/01/2000");
        public static DateTime AvailableEndDateTime = DateTime.Parse("01/01/2099");
        /// <summary>
        /// Truncate  long string in short form.
        /// </summary>
        /// <param name="str">Long string</param>
        /// <returns>Truncated string</returns>
        public static string FormatLargeString(string str)
        {
            if (str.Length > 100)
            {
                str = str.Remove(100);
            }
            return str;
        }

        public static string Country = "Country";
        public static string Region = "Region";
        public static string City = "Region";
        public static string DefaultPage = "Default.aspx";
        public static string LogInPage = "Login.aspx";
        public static string SelectedCountryName = "United States";

        /// <summary>
        /// Return short date time.
        /// </summary>
        /// <param name="ndate">Date</param>
        /// <returns>Formated date.</returns>

        public static string ShortTimeReturn(System.Nullable<DateTime> ndate)
        {
            string retStr = string.Empty;
            if (ndate != null)
            {
                retStr = DateTime.Parse(ndate.ToString()).ToShortDateString();
            }
            return retStr;
        }
        /// <summary>
        /// Match string on array collection.
        /// </summary>
        /// <param name="arrColl">Array of strings.</param>
        /// <param name="parentString">Searching string</param>
        /// <returns>Return "true" if arrray match searching string.</returns>
        public static bool Contains(string[] arrColl, string parentString)
        {
            bool status = false;
            foreach (string word in arrColl)
            {
                status = parentString.Contains(word) ? false : true;
                if (!status)
                {
                    break;
                }
            }
            return status;
        }
        /// <summary>
        /// Get host store.
        /// </summary>
        /// <param name="UseSSL">"true" for enable SSL.</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns></returns>
        public static string GetStoreHost(bool UseSSL, int PortalID)
        {
            string result = "http://" + ServerVariables("HTTP_HOST");
            if (!result.EndsWith("/"))
                result += "/";

            if (UseSSL)
            {
                //string str = dbSetting.sp_SettingPortalBySettingID((int)SettingKey.Common_SharedSSL, PortalID).SingleOrDefault().Value;
                //if (!String.IsNullOrEmpty(str))
                //{
                //    result = str;
                //}
                //else
                //{
                //    result = result.Replace("http:/", "https:/");
                //    result = result.Replace("www.www", "www");
                //}
            }

            if (!result.EndsWith("/"))
                result += "/";

            return result;
        }
        #region Obsolete
        [Obsolete("not Used in SageFrame2.1")]
        public static void GetCurrentVersion(int PortalID)
        {
            //return dbSetting.sp_SettingPortalBySettingID((int)SettingKey.Common_CurrentVersion, PortalID).SingleOrDefault().Value;
        }
        [Obsolete("not Used in SageFrame2.1")]
        public static void GetSettingValueBoolean(int SettingID, int PortalID)
        {
            //return bool.Parse(dbSetting.sp_SettingPortalBySettingID(SettingID, PortalID).SingleOrDefault().Value);
        }
        [Obsolete("not Used in SageFrame2.1")]
        public static void GetSettingValue(int SettingID, int PortalID)
        {
            //return dbSetting.sp_SettingPortalBySettingID(SettingID, PortalID).SingleOrDefault().Value;
        }
        #endregion
    }
}
