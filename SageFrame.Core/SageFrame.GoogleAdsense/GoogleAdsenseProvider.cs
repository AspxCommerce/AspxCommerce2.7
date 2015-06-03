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


namespace SageFrame.GoogleAdsense
{
    /// <summary>
    /// Manupulates data for GoogleAdsenseProvider.
    /// </summary>
    public class GoogleAdsenseProvider
    {
        /// <summary>
        /// Connects to database and counts  adsense settings
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="PortalID">portalID</param>
        /// <returns>counts of adsense settings </returns>
        public int CountAdsenseSettings(int UserModuleID, int PortalID)
        {
            try
            {
                string sp = "[dbo].[sp_AdSenseSettingsCount]";
                SQLHandler sagesql = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));


                int UserModuleCount = sagesql.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@UserModuleCount");
                return UserModuleCount;

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and returns adsense setting  by  usermoduleID
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="PortalID">portalID</param>
        /// <returns>Lists of GoogleAdsenseInfo object</returns>
        public List<GoogleAdsenseInfo> GetAdSenseSettingsByUserModuleID(int UserModuleID, int PortalID)
        {
            try
            {
                string sp = "[dbo].[sp_AdSenseSettingsGetByUserModuleID]";
                SQLHandler sagesql = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                return sagesql.ExecuteAsList<GoogleAdsenseInfo>(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and adds  or updates the adsense setting name and values
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="SettingName">Setting key</param>
        /// <param name="SettingValue">Setting value</param>
        /// <param name="IsActive">Set true if the adsense is active</param>
        /// <param name="PortalID">portalID</param>
        /// <param name="UpdatedBy">the user who us updating the  value  in database</param>
        /// <param name="UpdateFlag"> Update flag</param>
        public void AddUpdateAdSense(int UserModuleID, string SettingName, string SettingValue, bool IsActive, int PortalID, string UpdatedBy, bool UpdateFlag)
        {
            try
            {
                string sp = "[dbo].[sp_AdSenseAddUpdate]";
                SQLHandler sagesql = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@SettingName", SettingName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@SettingValue", SettingValue));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", UpdatedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdateFlag", UpdateFlag));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Connects to database and deletes the adsense
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="PortalID">portalID</param>
        public void DeleteAdSense(int UserModuleID, int PortalID)
        {
            try
            {
                string sp = "[dbo].[sp_AdSenseDelete]";
                SQLHandler sageSql = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                sageSql.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and counts the Adsense 
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="PortalID">portalID</param>
        /// <returns></returns>
        public int CountAdSense(int UserModuleID, int PortalID)
        {
            try
            {
                string sp = "[dbo].[sp_AdSenseCount]";
                SQLHandler sagesql = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));


                int UserModuleCount = sagesql.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@UserModuleCount");
                return UserModuleCount;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
