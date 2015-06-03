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
using System.Data;
using System.Data.SqlClient;
using SageFrame.Web;
using SageFrame.Localization.Info;
//using SageFrame.Core;
#endregion


namespace SageFrame.Localization
{
    /// <summary>
    /// Manipulates data for LocaleController class.
    /// </summary>
    public class LocalizationSqlDataProvider
    {  
        /// <summary>
        /// Connects to database and obtains available languages.
        /// </summary>
        /// <returns>List of languages containing LanguageID,CultureCode,CultureName and FallbackCulture.</returns>
        public static List<Language> GetAvailableLocales()
        {
            List<Language> lstAvailableLocales = new List<Language>();
            string StoredProcedureName = "sp_LanguageGet";
            SqlDataReader SQLReader;
            try
            {
                SqlConnection SQLConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = StoredProcedureName;
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLConn.Open();
                SQLReader = SQLCmd.ExecuteReader();
            }
            catch (Exception e)
            {
                throw e;
            }

            while (SQLReader.Read())
            {

                Language obj = new Language(int.Parse(SQLReader["LanguageID"].ToString()), SQLReader["CultureName"].ToString(), SQLReader["CultureCode"].ToString());
                obj.LanguageN = SQLReader["CultureName"].ToString();
                obj.Country = SQLReader["CultureName"].ToString();
                lstAvailableLocales.Add(obj);
            }
            SQLReader.Close();
            return lstAvailableLocales;


        }
        /// <summary>
        /// Connects to database and insert language.
        /// </summary>
        /// <param name="objLanguage">Object of Language.</param>
        public static void AddLanguage(Language objLanguage)
        {
            List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
            ParaMeterCollection.Add(new KeyValuePair<string, string>("@CultureName", objLanguage.LanguageName));
            ParaMeterCollection.Add(new KeyValuePair<string, string>("@CultureCode", objLanguage.LanguageCode));
            ParaMeterCollection.Add(new KeyValuePair<string, string>("@FallbackCulture", objLanguage.FallBackLanguage));
            ParaMeterCollection.Add(new KeyValuePair<string, string>("@FallbackCultureCode", objLanguage.FallBackLanguageCode));
            ParaMeterCollection.Add(new KeyValuePair<string, string>("@CreatedByUserID", "0"));

            try
            {
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("sp_LanguageAdd", ParaMeterCollection);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Countries> GetCountryList()
        {
            List<Countries> lstCountries = new List<Countries>();
            DataSet ds = new DataSet();
            try
            {
                SQLHandler sagesql = new SQLHandler();
                ds = sagesql.ExecuteAsDataSet("sp_GetCountryList");
            }
            catch (Exception)
            {
                throw;
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                lstCountries.Add(new Countries("images/flags/" + row["Value"].ToString().ToLower() + ".png", row["Text"].ToString(), row["Culture"].ToString()));
            }
            return lstCountries;
        }
        /// <summary>
        /// Connects to database and enable language for given portalId,languageId,addedBy,isEnabled and isPublished.
        /// </summary>
        /// <param name="portalId">portalId</param>
        /// <param name="languageId">languageId</param>
        /// <param name="addedBy">addedBy</param>
        /// <param name="isEnabled">isEnabled</param>
        /// <param name="isPublished">isPublished</param>
        public static void EnableLanguage(int portalId, int languageId, string addedBy, int isEnabled, int isPublished)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@LanguageID", languageId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", addedBy));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsEnabled", isEnabled));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsPublished", isPublished));
            try
            {
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("usp_loc_EnableLanguage", ParaMeterCollection);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Connects to database and obtains portal lanaguages.
        /// </summary>
        /// <param name="portalId">PortalID</param>
        /// <returns>List of Language class conatining LanguageID,CultureName and CultureCode.</returns>
        public static List<Language> GetPortalLanguages(int portalId)
        {
            List<Language> lstPortalLanguages = new List<Language>();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            string StoredProcedureName = "usp_loc_PortalLanguagesGet";
            SqlDataReader SQLReader;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
            while (SQLReader.Read())
            {
                Language obj = new Language(int.Parse(SQLReader["LanguageID"].ToString()), SQLReader["CultureName"].ToString(), SQLReader["CultureCode"].ToString());
                obj.LanguageN = SQLReader["CultureName"].ToString();
                obj.Country = SQLReader["CultureName"].ToString();
                lstPortalLanguages.Add(obj);
            }
            SQLReader.Close();
            return lstPortalLanguages;
        }
        /// <summary>
        /// Connects to database and obtains cire modules.
        /// </summary>
        /// <returns>List of ModuleInfo class containing ModuleID and ModuleName.</returns>
        public static List<ModuleInfo> GetCoreModules()
        {
            List<ModuleInfo> lstCoreModules = new List<ModuleInfo>();
            string StoredProcedureName = "usp_loc_CoreModulesGet";
            SqlDataReader SQLReader;
            try
            {
                SqlConnection SQLConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = StoredProcedureName;
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLConn.Open();
                SQLReader = SQLCmd.ExecuteReader();
            }
            catch (Exception e)
            {
                throw e;
            }

            while (SQLReader.Read())
            {
                ModuleInfo obj = new ModuleInfo();
                obj.ModuleID = int.Parse(SQLReader["ModuleID"].ToString());
                obj.ModuleName = SQLReader["ModuleName"].ToString();
                lstCoreModules.Add(obj);
            }
            SQLReader.Close();
            return lstCoreModules;
        }
        /// <summary>
        /// Connects to database and add language switch settings for given lstKeyValue,UserModuleID and PortalID.
        /// </summary>
        /// <param name="lstKeyValue">List of object of LanguageSwitchKeyValue class.</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        public static void AddLanguageSwitchSettings(List<LanguageSwitchKeyValue> lstKeyValue, int UserModuleID, int PortalID)
        {
            foreach (LanguageSwitchKeyValue obj in lstKeyValue)
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SettingKey", obj.Key));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SettingValue", obj.Value));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", obj.IsActive));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedBy", obj.AddedBy));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", obj.AddedBy));

                try
                {
                    SQLHandler sagesql = new SQLHandler();
                    sagesql.ExecuteNonQuery("usp_loc_AddLanguageSwitchSettings", ParaMeterCollection);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        /// <summary>
        /// Connects to database and obtains language switch setting.
        /// </summary>
        /// <param name="portalId">portalId</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <returns>List of LanguageSwitchKeyValue class containing SettingKey and SettingValue.</returns>
        public static List<LanguageSwitchKeyValue> GetLanguageSwitchSettings(int portalId, int UserModuleID)
        {
            List<LanguageSwitchKeyValue> lstSettings = new List<LanguageSwitchKeyValue>();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
            string StoredProcedureName = "usp_loc_GetLanguageSwitchSettings";
            SqlDataReader SQLReader;
            try
            {
                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
            while (SQLReader.Read())
            {
                lstSettings.Add(new LanguageSwitchKeyValue(SQLReader["SettingKey"].ToString(), SQLReader["SettingValue"].ToString()));
            }
            SQLReader.Close();
            return lstSettings;
        }

        /// <summary>
        /// Connects to database and obtains local page name.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>List of LocalPageInfo class containing pageid and pagename. </returns>

        public static List<LocalPageInfo> GetLocalPageName(int PortalID, string CultureCode)
        {
            SQLHandler sqlHan = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode", CultureCode));
            return sqlHan.ExecuteAsList<LocalPageInfo>("[dbo].[usp_MenuLocalizeGetPages]", ParaMeterCollection);
        }

        /// <summary>
        /// Connects to database and obtains local module title
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>List of LocalModuleInfo class containing user module id and user module title.  </returns>
        public static List<LocalModuleInfo> GetLocalModuleTitle(int PortalID, string CultureCode)
        {
            SQLHandler sqlHan = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode", CultureCode));
            return sqlHan.ExecuteAsList<LocalModuleInfo>("[dbo].[usp_MenuLocalizeGetModuleTitle]", ParaMeterCollection);

        }
        /// <summary>
        /// Connects to database and adds or updates local page name.
        /// </summary>
        /// <param name="lstLocalPage">List of object of LocalPageInfo class.</param>
        public static void AddUpdateLocalPage(List<LocalPageInfo> lstLocalPage)
        {
            string StoredProcedureName = "[dbo].[usp_AddUpdateLocalPage]";
            SQLHandler sqlHan = new SQLHandler();

            foreach (LocalPageInfo objPage in lstLocalPage)
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PageID", objPage.PageID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@LocalPageName", objPage.LocalPageName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode", objPage.CultureCode));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@LocalPageCaption", objPage.LocalPageCaption));

                sqlHan.ExecuteNonQuery(StoredProcedureName, ParaMeterCollection);
            }
        }
        /// <summary>
        /// Connects to database and adds or updates local module title.
        /// </summary>
        /// <param name="lstLocalPage">List of object of LocalModuleInfo class. </param>
        public static void AddUpdateLocalModuleTitle(List<LocalModuleInfo> lstLocalPage)
        {
            string StoredProcedureName = "[dbo].[usp_AddUpdateLocalModuleTitle]";
            SQLHandler sqlHan = new SQLHandler();

            foreach (LocalModuleInfo objPage in lstLocalPage)
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", objPage.UserModuleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@LocalModuleTitle", objPage.LocalModuleTitle));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode", objPage.CultureCode));

                sqlHan.ExecuteNonQuery(StoredProcedureName, ParaMeterCollection);
            }
        }
        /// <summary>
        /// Connects to database and deletes language.
        /// </summary>
        /// <param name="code">code</param>
        public static void DeleteLanguage(string code)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@code", code));
                SQLHandler SQLH = new SQLHandler();

                SQLH.ExecuteNonQuery("sp_LanguageDelete", ParaMeterCollection);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

}
