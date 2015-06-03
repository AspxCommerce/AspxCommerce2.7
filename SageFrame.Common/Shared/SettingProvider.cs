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
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using SageFrame.Web.Utilities;
using SageFrame.Web;
#endregion

namespace SageFrame.Shared
{
    /// <summary>
    /// Application setting provider.
    /// </summary>
    public class SettingProvider
    {
        /// <summary>
        /// Initializes a new instance of the SettingProvider.
        /// </summary>
        public SettingProvider()
        {
        }
        /// <summary>
        ///  Connects to database and get settings based on PortalID and SettingType.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="SettingType">Setting type.</param>
        /// <returns>DataSet</returns>
        private DataSet GetSettingsByPortalAndSettingType(string PortalID, string SettingType)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SettingType", SettingType));
                DataSet ds = new DataSet();
                SQLHandler sagesql = new SQLHandler();
                ds = sagesql.ExecuteAsDataSet("dbo.sp_GetAllSettings", ParaMeterCollection);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        ///  Connects to database and get all available portals.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetAllPortals()
        {
            try
            {
                DataSet ds = new DataSet();
                SQLHandler sagesql = new SQLHandler();
                ds = sagesql.ExecuteAsDataSet("dbo.sp_GetAllPortals");
                DataTable dt = ds.Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Get settings based on PortalID and SettingType.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="SettingType">Setting type.</param>
        /// <returns>DataTable</returns>
        public DataTable GetSettingsByPortal(string PortalID, string SettingType)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = GetSettingsByPortalAndSettingType(PortalID, SettingType);
                if (ds != null && ds.Tables != null && ds.Tables[0] != null)
                {
                    dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        ///  Connects to database and save application settings.
        /// </summary>
        /// <param name="SettingTypes">SettingTypes</param>
        /// <param name="SettingKeys">SettingKeys</param>
        /// <param name="SettingValues">SettingValues</param>
        /// <param name="Username">Username</param>
        /// <param name="PortalID">PortalID</param>
        public void SaveSageSettings(string SettingTypes, string SettingKeys, string SettingValues, string Username, string PortalID)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SettingTypes", SettingTypes));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SettingKeys", SettingKeys));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SettingValues", SettingValues));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@UserName", Username));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", PortalID));
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("dbo.sp_InsertUpdateSettings", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Connects to database and save application setting.
        /// </summary>
        /// <param name="SettingType">SettingType</param>
        /// <param name="SettingKey">SettingKey</param>
        /// <param name="SettingValue">SettingValue</param>
        /// <param name="Username">Username</param>
        /// <param name="PortalID">PortalID</param>
        public void SaveSageSetting(string SettingType, string SettingKey, string SettingValue, string Username, string PortalID)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SettingType", SettingType));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SettingKey", SettingKey));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SettingValue", SettingValue));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@UserName", Username));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", PortalID));
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("dbo.sp_InsertUpdateSetting", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region "Google Analytics"
        /// <summary>
        /// Returns list of GoogleAnalyticsInfo class.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of GoogleAnalyticsInfo.</returns>
        public List<GoogleAnalyticsInfo> GetGoogleAnalyticsActiveOnlyByPortalID(int PortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLHandler sqlH = new SQLHandler();
                List<GoogleAnalyticsInfo> defaultList = sqlH.ExecuteAsList<GoogleAnalyticsInfo>("dbo.sp_GoogleAnalyticsListActiveOnlyByPortalID", ParaMeterCollection);
                return defaultList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Returns object of GoogleAnalyticsInfo class based on PortalID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Object of GoogleAnalyticsInfo class</returns>
        public GoogleAnalyticsInfo GetGoogleAnalyticsByPortalID(int PortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLHandler sqlH = new SQLHandler();
                GoogleAnalyticsInfo defaultList = sqlH.ExecuteAsObject<GoogleAnalyticsInfo>("dbo.sp_GoogleAnalyticsListByPortalID", ParaMeterCollection);
                return defaultList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        ///  Connects to database and add or update Google analytic code.
        /// </summary>
        /// <param name="GoogleJSCode">Code</param>
        /// <param name="IsActive">true if active.</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="AddedBy">User name.</param>
        public void GoogleAnalyticsAddUpdate(string GoogleJSCode, bool IsActive, int PortalID, string AddedBy)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@GoogleJSCode", GoogleJSCode));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.sp_GoogleAnalyticsAddUpdate", ParaMeterCollection);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
        /// <summary>
        /// Return of applicatio's portal list.
        /// </summary>
        /// <returns>Applicatio's portal list</returns>
        public List<SagePortals> PortalGetList()
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<SagePortals> sagePortals = sqlH.ExecuteAsList<SagePortals>("dbo.sp_PortalGetList");
                return sagePortals;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Returns list of role based on User name and PortalID.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="portalID">PortalID</param>
        /// <returns>List of user role.</returns>
        public List<SageUserRole> RoleListGetByUsername(string userName, int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlH = new SQLHandler();
                List<SageUserRole> sagePortalList = sqlH.ExecuteAsList<SageUserRole>("dbo.sp_RoleGetByUsername", ParaMeterCollection);
                return sagePortalList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        /// <summary>
        /// Connects to database and change portal.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        public void ChangePortal(int PortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("dbo.usp_ChangePortal", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Returns application settings by individual key based on setting key and PortalID..
        /// </summary>
        /// <param name="settingKey">Setting key.</param>
        /// <param name="portalID">PortalID</param>
        /// <returns>Returns object of KeyValue class. </returns>
        public KeyValue GetSettingValueByIndividualKey(string settingKey, int portalID)
        {
            KeyValue value = new KeyValue();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SettingKey", settingKey));
                SQLHandler sagesql = new SQLHandler();
                value = sagesql.ExecuteAsObject<KeyValue>("dbo.usp_GetSettingValueByIndividualKey", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
            return value;
        }
        /// <summary>
        /// Connects to database and returns application settings.
        /// </summary>
        /// <param name="portalID">portalID</param>
        /// <param name="settingType">Setting type.</param>
        /// <returns>DataSet</returns>
        public DataSet GetAllSettings(string portalID, string settingType)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", portalID));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SettingType", settingType));
                DataSet dataset = new DataSet();
                SQLHandler sagesql = new SQLHandler();
                dataset = sagesql.ExecuteAsDataSet("dbo.usp_GetAllSettings", ParaMeterCollection);
                return dataset;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
    /// <summary>
    ///  Class holds the properties of application portals.
    /// </summary>
    public class SagePortals
    {

        private int _PortalID;
        private string _Name;
        private string _SEOName;
        private System.Nullable<bool> _IsParent;
        private string _DefaultPage;
        /// <summary>
        /// 
        /// </summary>
        public SagePortals() { }
        /// <summary>
        /// Initializes a new instance of the apllication portals.
        /// </summary>
        /// <param name="portalID">portalID</param>
        /// <param name="name">name</param>
        /// <param name="sEOName">sEOName</param>
        /// <param name="isParent">true for parent portal.</param>
        /// <param name="defaultPage">Portal default page.</param>
        public SagePortals(int portalID, string name, string sEOName, bool isParent, string defaultPage)
        {
            this.PortalID = portalID;
            this.Name = name;
            this.SEOName = sEOName;
            this.IsParent = isParent;
            this.DefaultPage = defaultPage;
        }
        /// <summary>
        /// get or set PortalID.
        /// </summary>
        public int PortalID
        {
            get
            {
                return this._PortalID;
            }
            set
            {
                if ((this._PortalID != value))
                {
                    this._PortalID = value;
                }
            }
        }

        /// <summary>
        /// Get or set portal name.
        /// </summary>
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this._Name = value;
                }
            }
        }

        /// <summary>
        /// Get or set portal SEO name.
        /// </summary>
        public string SEOName
        {
            get
            {
                return this._SEOName;
            }
            set
            {
                if ((this._SEOName != value))
                {
                    this._SEOName = value;
                }
            }
        }

        /// <summary>
        /// Get or set true if portal is parent portal.
        /// </summary>
        public System.Nullable<bool> IsParent
        {
            get
            {
                return this._IsParent;
            }
            set
            {
                if ((this._IsParent != value))
                {
                    this._IsParent = value;
                }
            }
        }

        /// <summary>
        /// Get or set portal default page.
        /// </summary>
        public string DefaultPage
        {
            get
            {
                return this._DefaultPage;
            }
            set
            {
                if ((this._DefaultPage != value))
                {
                    this._DefaultPage = value;
                }
            }
        }
    }
    /// <summary>
    /// Class holds the properties of application users role.
    /// </summary>
    public class SageUserRole
    {

        private System.Guid _RoleId;
        /// <summary>
        /// Initializes a new instance of the SageUserRole class.
        /// </summary>
        public SageUserRole() { }
        /// <summary>
        /// Initializes a new instance of the SageUserRole class
        /// </summary>
        /// <param name="roleId">RoleID.</param>
        public SageUserRole(System.Guid roleId)
        {
            this.RoleId = roleId;
        }
        /// <summary>
        /// Get or set RoleId.
        /// </summary>
        public System.Guid RoleId
        {
            get
            {
                return this._RoleId;
            }
            set
            {
                if ((this._RoleId != value))
                {
                    this._RoleId = value;
                }
            }
        }
    }
}
