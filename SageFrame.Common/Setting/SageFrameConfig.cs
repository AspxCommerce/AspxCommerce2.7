#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Text;
using SageFrame.Shared;
using System.Collections;
using System.Web;
using System.Data;
using SageFrame.Web.Utilities;
using System.Web.Security;
using SageFrame.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using SageFrame.Application;
#endregion

namespace SageFrame.Web
{
    /// <summary>
    /// Summary about application configuration.
    /// </summary>
    [Serializable]
    public class SageFrameConfig
    {
        /// <summary>
        /// Initializes a new instance of SageFrameConfig.
        /// </summary>
        public SageFrameConfig() { }
        /// <summary>
        /// Get application user name.
        /// </summary>
        public string GetUsername
        {
            get
            {
                try
                {
                    string userName = GetUser(GetPortalID);
                    return userName;
                }
                catch
                {
                    return "anonymoususer";
                }
            }
        }
        /// <summary>
        /// Return application page settings.
        /// </summary>
        /// <param name="controlType">Application control type.</param>
        /// <param name="pageID">PageID</param>
        /// <returns>Page settings.</returns>
        public DataSet GetPageSettings(string controlType, string pageID)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();

                ParaMeterCollection.Add(new KeyValuePair<string, string>("@ControlType", controlType));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PageID", pageID));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", GetPortalID.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@UserName", GetUsername));
                DataSet ds = new DataSet();
                SQLHandler sagesql = new SQLHandler();
                ds = sagesql.ExecuteAsDataSet("dbo.sp_GetPageSetting", ParaMeterCollection);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Return application page settings based on page name.
        /// </summary>
        /// <param name="controlType">Application control type.</param>
        /// <param name="pageSEOName">Page name.</param>
        /// <param name="userName">User name.</param>
        /// <returns></returns>
        public DataSet GetPageSettingsByPageSEOName(string controlType, string pageSEOName, string userName)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();

                ParaMeterCollection.Add(new KeyValuePair<string, string>("@ControlType", controlType));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PageSEOName", pageSEOName));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", GetPortalID.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@UserName", userName));
                DataSet ds = new DataSet();
                SQLHandler sagesql = new SQLHandler();
                ds = sagesql.ExecuteAsDataSet("dbo.usp_GetPageSettingByPageSEOName", ParaMeterCollection);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Return modules assigne with page.
        /// </summary>
        /// <param name="controlType">Application control type.</param>
        /// <param name="pageSEOName">Page name.</param>
        /// <param name="userName">User name.</param>
        /// <param name="cultureCode">Culture code.</param>
        /// <param name="isPreview">"true" for availablity of preview .</param>
        /// <param name="previewCode">Preview code.</param>
        /// <returns>UserModuleInfo list.</returns>
        public List<UserModuleInfo> GetPageModules(string controlType, string pageSEOName, string userName, string cultureCode, bool isPreview, string previewCode)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();

                ParaMeterCollection.Add(new KeyValuePair<string, string>("@ControlType", controlType));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PageSEOName", pageSEOName));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", GetPortalID.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@UserName", userName));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@CultureCode", cultureCode));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@IsPreview", isPreview.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PreviewCode", previewCode));
                List<UserModuleInfo> lstPageModules = new List<UserModuleInfo>();
                SQLHandler sagesql = new SQLHandler();
                lstPageModules = sagesql.ExecuteAsList<UserModuleInfo>("[dbo].[usp_MasterPageGetPageModules]", ParaMeterCollection);
                return lstPageModules;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Return modules assigne with page for anonymous user.
        /// </summary>
        /// <param name="controlType">Application control type.</param>
        /// <param name="pageSEOName">Page name.</param>
        /// <param name="userName">User name.</param>
        /// <param name="cultureCode">Culture code.</param>
        /// <returns>UserModuleInfo list.</returns>
        public List<UserModuleInfo> GetPageModules_Anonymous(string controlType, string pageSEOName, string userName, string cultureCode)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@ControlType", controlType));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PageSEOName", pageSEOName));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", GetPortalID.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@UserName", userName));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@CultureCode", cultureCode));
                List<UserModuleInfo> lstPageModules = new List<UserModuleInfo>();
                SQLHandler sagesql = new SQLHandler();
                lstPageModules = sagesql.ExecuteAsList<UserModuleInfo>("[dbo].[usp_MasterPageGetPageModules_Anonymous]", ParaMeterCollection);
                return lstPageModules;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Return modules assigne with page for superuser.
        /// </summary>
        /// <param name="controlType">Application control type.</param>
        /// <param name="pageSEOName">Page name.</param>
        ///  <param name="GetUsername">User name.</param>
        /// <param name="cultureCode">Culture code.</param>
        /// <param name="isPreview">"true" for availablity of preview .</param>
        /// <param name="previewCode">Preview code.</param>
        /// <returns>UserModuleInfo list.</returns>
        public List<UserModuleInfo> GetPageModules_Superuser(string controlType, string pageSEOName, string GetUsername, string cultureCode, bool isPreview, string previewCode)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@ControlType", controlType));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PageSEOName", pageSEOName));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", GetPortalID.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@UserName", GetUsername));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@CultureCode", cultureCode));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@IsPreview", isPreview.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PreviewCode", previewCode));
                List<UserModuleInfo> lstPageModules = new List<UserModuleInfo>();
                SQLHandler sagesql = new SQLHandler();
                lstPageModules = sagesql.ExecuteAsList<UserModuleInfo>("[dbo].[usp_MasterPageGetPageModules_Superuser]", ParaMeterCollection);
                return lstPageModules;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Page setting based on page name for Admin.
        /// </summary>
        /// <param name="controlType">Application control type.</param>
        /// <param name="pageSEOName">Page name.</param>
        /// <param name="userName">User name.</param>
        /// <returns>Return page setting based on page name for Admin.</returns>
        public DataSet GetPageSettingsByPageSEONameForAdmin(string controlType, string pageSEOName, string userName)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();

                ParaMeterCollection.Add(new KeyValuePair<string, string>("@ControlType", controlType));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PageSEOName", pageSEOName));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", GetPortalID.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@UserName", userName));
                DataSet ds = new DataSet();
                SQLHandler sagesql = new SQLHandler();
                ds = sagesql.ExecuteAsDataSet("[dbo].[usp_GetPageSettingByPageSEONameForAdmin]", ParaMeterCollection);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Get "true" if page is parent.
        /// </summary>
        public bool IsParent
        {
            get
            {
                try
                {
                    int portalID = GetPortalID;
                    bool flag = false;
                    if (HttpContext.Current.Session[SessionKeys.IsParent + portalID] != null)
                    {
                        flag = bool.Parse(HttpContext.Current.Session[SessionKeys.IsParent + portalID].ToString()) == true ? true : false;
                    }
                    else
                    {
                        SageFrameConfig obj = new SageFrameConfig();
                        flag = obj.DecideIsParent(portalID);
                        HttpContext.Current.Session[SessionKeys.IsParent + portalID] = flag;
                    }
                    return flag;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of DecideIsParent.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Return "true" if page is parent.</returns>
        public bool DecideIsParent(int PortalID)
        {

            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLHandler sagesql = new SQLHandler();
                return sagesql.ExecuteAsScalar<bool>("[dbo].[usp_PortalIsParent]", ParaMeterCollection);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        ///Get portal parent URL
        /// </summary>
        public string GetParentURL
        {
            get
            {
                try
                {
                    string ParentURL = string.Empty;
                    if (HttpContext.Current.Session[SessionKeys.ParentURL] != null)
                    {
                        ParentURL = HttpContext.Current.Session[SessionKeys.ParentURL].ToString();

                    }
                    else
                    {
                        int PID = GetPortalID;
                        SageFrameConfig obj = new SageFrameConfig();
                        ParentURL = obj.GetPortalParentURL(PID);
                    }
                    if (ParentURL.ToLower() == "default")
                    {
                        ParentURL = "~/";
                    }
                    HttpContext.Current.Session[SessionKeys.ParentURL] = ParentURL;
                    return ParentURL;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of GetPortalParentURL.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Return portal parent URL</returns>
        public string GetPortalParentURL(int PortalID)
        {

            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLHandler sagesql = new SQLHandler();
                return sagesql.ExecuteAsScalar<string>("[dbo].[usp_PortalGetParentURL]", ParaMeterCollection);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Get current portal ID.
        /// </summary>
        public int GetPortalID
        {
            get
            {
                try
                {
                    Hashtable hstPortals = GetPortals();
                    string URL = HttpContext.Current.Request.Url.ToString();
                    if (URL.Contains("/portal/"))
                    {
                        var RegMatch = Regex.Matches(URL, @"^http[s]?\s*:.*\/portal\/([^/]+)+\/.*");
                        string PortalName = "";
                        foreach (Match match in RegMatch)
                        {
                            PortalName = match.Groups[1].Value;
                        }
                        int PortalID = Int32.Parse(hstPortals[PortalName].ToString());
                        //HttpContext.Current.Session[SessionKeys.SageFrame_PortalID] = PortalID.ToString();
                        return PortalID;
                    }
                    else
                    {
                        string ParentURL = HttpContext.Current.Request.Url.Authority + GetApplicationName;
                        int PID = 1;
                        foreach (DictionaryEntry entry in hstPortals)
                        {
                            if (ParentURL.ToString().ToLower() == entry.Key.ToString())
                                PID = int.Parse(entry.Value.ToString());
                        }
                        //HttpContext.Current.Session[SessionKeys.SageFrame_PortalID] = PID;
                        return PID;
                    }
                }
                catch
                {
                    return 1;
                }
            }
        }

        /// <summary>
        /// Get PortalID And Respective Name List
        /// </summary>
        /// <returns>PortalID And PortalName</returns>
        public Hashtable GetPortals()
        {
            Hashtable hstAll = new Hashtable();
            if (HttpRuntime.Cache[CacheKeys.Portals] != null)
            {
                hstAll = (Hashtable)HttpRuntime.Cache[CacheKeys.Portals];
            }
            else
            {
                SettingProvider objSP = new SettingProvider();
                List<SagePortals> sagePortals = objSP.PortalGetList();
                foreach (SagePortals portal in sagePortals)
                {
                    hstAll.Add(portal.SEOName.Replace("https://", "").Replace("http://", "").ToLower().Trim(), portal.PortalID);
                }
            }
            HttpRuntime.Cache.Insert(CacheKeys.Portals, hstAll);
            return hstAll;
        }

        /// <summary>
        /// Settings based on application key.
        /// </summary>
        /// <param name="Key">Application key.</param>
        /// <returns>Return settings based on application key.</returns>
        public string GetSettingsByKey(string Key)
        {
            try
            {
                string strValue = string.Empty;
                SettingProvider sep = new SettingProvider();
                Hashtable hst = new Hashtable();
                if (HttpRuntime.Cache[CacheKeys.SageSetting] != null)
                {

                    hst = (Hashtable)HttpRuntime.Cache[CacheKeys.SageSetting];
                }
                else
                {
                    DataTable dt = sep.GetSettingsByPortal(GetPortalID.ToString(), string.Empty); //GetSettingsByPortal();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                        }
                    }
                    //need to be cleared when any key is chnaged
                    HttpRuntime.Cache.Insert(CacheKeys.SageSetting, hst);
                }
                strValue = hst[SettingPortal + "." + Key].ToString();
                return strValue;
            }

            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        ///Individual setting based on application key.
        /// </summary>
        /// <param name="Key">Application key.</param>
        /// <returns>Return settings based on application key.</returns>
        public string GetSettingsByKeyIndividual(string Key)
        {
            try
            {
                string strValue = string.Empty;
                SettingProvider sep = new SettingProvider();
                Hashtable hst = new Hashtable();
                if (HttpRuntime.Cache[CacheKeys.SageSetting] != null)
                {
                    hst = (Hashtable)HttpRuntime.Cache[CacheKeys.SageSetting];
                }
                else
                {
                    DataTable dt = sep.GetSettingsByPortal("1", string.Empty); //GetSettingsByPortal();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                        }
                    }
                }
                //need to be cleared when any key is chnaged
                HttpRuntime.Cache.Insert(CacheKeys.SageSetting, hst);//
                strValue = hst[SettingPortal + "." + Key].ToString();
                return strValue;
            }

            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Resetsetting keys based on portal ID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        public void ResetSettingKeys(int PortalID)
        {
            SettingProvider sep = new SettingProvider();
            Hashtable hst = new Hashtable();
            DataTable dt = sep.GetSettingsByPortal(PortalID.ToString(), string.Empty);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                }
            }
            //need to be cleared when any key is chnaged
            HttpRuntime.Cache.Insert(CacheKeys.SageSetting, hst);//
        }
        /// <summary>
        /// Get default portal.
        /// </summary>
        private string SettingPortal
        {
            get
            {
                string strPortalName = "default";
                Hashtable hstPortals = GetPortals();
                strPortalName = GetDefaultPortalName(hstPortals, 1);
                try
                {
                    if (HttpContext.Current.Session[SessionKeys.SageFrame_PortalSEOName] != null)
                    {
                        strPortalName = HttpContext.Current.Session[SessionKeys.SageFrame_PortalSEOName].ToString();
                    }
                }
                catch
                {
                    strPortalName = GetDefaultPortalName(hstPortals, 1);
                }
                return strPortalName;
            }
        }
        /// <summary>
        /// Get default portal name by portalID
        /// </summary>
        /// <param name="hstPortals">HashTable containing PortalID and PortalName</param>
        /// <param name="portalID">PortalID</param>
        /// <returns>PortalName</returns>
        public string GetDefaultPortalName(Hashtable hstPortals, int portalID)
        {
            string portalname = string.Empty;
            foreach (string key in hstPortals.Keys)
            {
                if (Int32.Parse(hstPortals[key].ToString()) == portalID)
                {
                    portalname = key;
                }
            }
            return portalname;
        }

        /// <summary>
        /// Return integer SettingValue based upon key from cache.
        /// </summary>
        /// <param name="Key">Application key.</param>
        /// <returns>Integer SettingValue.</returns>
        public int GetSettingIntByKey(string Key)
        {
            try
            {
                return Int32.Parse(GetSettingsByKey(Key));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Return Boolean SettingValue based upon key from cache.
        /// </summary>
        /// <param name="Key">Application key.</param>
        /// <returns> Boolean SettingValue.</returns>
        public bool GetSettingBollByKey(string Key)
        {
            try
            {
                if (!string.IsNullOrEmpty(GetSettingsByKey(Key)))
                {
                    return bool.Parse(GetSettingsByKey(Key));
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Return the name of the logged in user name based upon PortalID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Logged in user name.</returns>
        public string GetUser(int portalID)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsCookieName(portalID)];
            string user = string.Empty;
            if (authCookie != null && authCookie.Value != string.Empty)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if (ticket != null)
                {
                    user = ticket.Name;
                }
                else
                {
                    user = "anonymoususer";
                }
            }
            else
            {
                user = "anonymoususer";
            }
            return user;
        }
        /// <summary>
        /// Return FormsAuthenticationTicket based upon portal ID.
        /// </summary>
        /// <param name="portalID">portalID</param>
        /// <returns>FormsAuthenticationTicket</returns>
        public FormsAuthenticationTicket GetUserTicket(int portalID)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsCookieName(portalID)];
            if (authCookie != null && authCookie.Value != string.Empty)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                return ticket;
            }
            else
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "anonymoususer", DateTime.Now,
                                                                            DateTime.Now.AddMinutes(30),
                                                                              true,
                                                                              portalID.ToString(),
                                                                              FormsAuthentication.FormsCookiePath);
                return ticket;
            }
        }
        /// <summary>
        /// Return forms cookies name.
        /// </summary>
        /// <param name="portalID">portalID</param>
        /// <returns>Cookies name.</returns>
        public string FormsCookieName(int portalID)
        {
            string formName = string.Empty;
            formName = FormsAuthentication.FormsCookieName + HttpContext.Current.Session + portalID.ToString();
            return formName;
        }
        /// <summary>
        /// Get application name.
        /// </summary>
        public static string GetApplicationName
        {
            get
            {
                return (HttpContext.Current.Request.ApplicationPath == "/" ? "" : HttpContext.Current.Request.ApplicationPath);
            }
        }

        /// <summary>
        /// Return string SettingValue based on application setting key.
        /// </summary>
        /// <param name="settingKey">Application setting key.</param>
        /// <returns>SettingValue based on application setting key.</returns>
        public string GetSettingValueByIndividualKey(string settingKey)
        {
            string value = string.Empty;
            try
            {
                SettingProvider objSetting = new SettingProvider();
                KeyValue objValue = objSetting.GetSettingValueByIndividualKey(settingKey, GetPortalID);
                if (objValue != null && objValue.Value != null)
                {
                    value = objValue.Value;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return value;
        }
        /// <summary>
        /// Return boolean setting value based upon individual key. 
        /// </summary>
        /// <param name="settingKey">Application setting key.</param>
        /// <returns>SettingValue based upon individual key.</returns>
        public bool GetSettingBoolValueByIndividualKey(string settingKey)
        {
            bool value = false;
            try
            {
                SettingProvider objSetting = new SettingProvider();
                KeyValue objValue = objSetting.GetSettingValueByIndividualKey(settingKey, GetPortalID);
                if (objValue != null && objValue.Value != null)
                {
                    value = bool.Parse(objValue.Value);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return value;
        }

        /// <summary>
        /// Return integer setting value based upon application setting key.
        /// </summary>
        /// <param name="settingKey">Application setting key.</param>
        /// <returns>setting value based upon application setting key.</returns>
        public int GetSettingIntValueByIndividualKey(string settingKey)
        {
            int value = 0;
            try
            {
                SettingProvider objSetting = new SettingProvider();
                KeyValue objValue = objSetting.GetSettingValueByIndividualKey(settingKey, GetPortalID);
                if (objValue != null && objValue.Value != null)
                {
                    value = int.Parse(objValue.Value);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return value;
        }
        /// <summary>
        /// Return application information.
        /// </summary>
        /// <param name="applicationName">Application name.</param>
        /// <returns>Application information.</returns>
        public ApplicationInfo GetApplicationInfo(string applicationName)
        {
            List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
            ParaMeterCollection.Add(new KeyValuePair<string, string>("@ApplicationName", applicationName.ToLower()));
            SQLHandler sagesql = new SQLHandler();
            ApplicationInfo objAppInfo = sagesql.ExecuteAsObject<ApplicationInfo>("[dbo].[usp_GetApplicationInfo]", ParaMeterCollection);
            return objAppInfo;
        }
    }
}
