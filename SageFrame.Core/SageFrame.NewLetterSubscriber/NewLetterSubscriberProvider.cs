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


namespace SageFrame.NewLetterSubscriber
{
    /// <summary>
    /// Manupulates data for user NewLetterSubscriberProvider.
    /// </summary>
    public class NewLetterSubscriberProvider
    {
        /// <summary>
        /// Connects to database and adds news letter subscribers detail.
        /// </summary>
        /// <param name="Email">Subscriber email ID.</param>
        /// <param name="ClientIP">Subscriber IP.</param>
        /// <param name="IsActive">Set true if subscriber is active.</param>
        /// <param name="AddedBy">Subscribe added user's name.</param>
        /// <param name="AddedOn">Subscribe added date.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>Returns NewLetterSubscribersID.</returns>
        public static int AddNewLetterSubscribers(string Email, string ClientIP, bool IsActive, string AddedBy, DateTime AddedOn, int PortalID)
        {
            string sp = "[dbo].[sp_NewLetterSubscribersAdd]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@Email", Email));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ClientIP", ClientIP));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", AddedOn));

                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));


                int NSID = sagesql.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@NewLetterSubscribersID");
                return NSID;


            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connect to database and updates news letter settings.
        /// </summary>
        /// <param name="UserModuleID">User module ID.</param>
        /// <param name="SettingKey">Setting key.</param>
        /// <param name="SettingValue">Setting value.</param>
        /// <param name="IsActive">Set true if the setting is active.</param>
        /// <param name="PortalID">PortalID.</param>
        /// <param name="UpdatedBy">Setting updated user's name.</param>
        /// <param name="AddedBy">Setting added user's name.</param>
        /// <returns>Returns NewsLetterSettingValueID</returns>
        public static int UpdateNewLetterSettings(int UserModuleID, string SettingKey, string SettingValue, bool IsActive, int PortalID, string UpdatedBy, string AddedBy)
        {

            string sp = "[dbo].[sp_NewsLetterSettingsUpdate]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@SettingKey", SettingKey));
                ParamCollInput.Add(new KeyValuePair<string, object>("@SettingValue", SettingValue.ToString()));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", UpdatedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));

                int NSV = sagesql.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@NewsLetterSettingValueID");
                return NSV;


            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and returns news letter's setting
        /// </summary>
        /// <param name="usermoduleIDControl">User module ID</param>
        /// <param name="portalID">Portal ID</param>
        /// <returns>News letter setting</returns>
        public NewsLetterSettingsInfo GetNewsLetterSetting(int usermoduleIDControl,int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", usermoduleIDControl));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler objSql = new SQLHandler();
                NewsLetterSettingsInfo newsLetterSettingObj = new NewsLetterSettingsInfo();
                newsLetterSettingObj = objSql.ExecuteAsObject<NewsLetterSettingsInfo>("dbo.sp_NewsLetterSettingsGetAll", ParaMeterCollection);
                return newsLetterSettingObj;
            }
            catch (Exception)
            {
                throw;
            }
        } 
    }
}
