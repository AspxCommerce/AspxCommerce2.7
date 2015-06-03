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

#endregion


namespace SageFrame.NewLetterSubscriber
{
    /// <summary>
    /// Business logic for NewLetterSubscriberController
    /// </summary>
    public class NewLetterSubscriberController
    {
        /// <summary>
        /// Adds news letter subscribers detail.
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
            try
            {
                return NewLetterSubscriberProvider.AddNewLetterSubscribers(Email, ClientIP, IsActive, AddedBy, AddedOn, PortalID);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        /// <summary>
        /// Updates news letter settings.
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
            try
            {
                return NewLetterSubscriberProvider.UpdateNewLetterSettings(UserModuleID, SettingKey, SettingValue, IsActive, PortalID, UpdatedBy, AddedBy);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Returns news letter's setting
        /// </summary>
        /// <param name="usermoduleIDControl">User module ID</param>
        /// <param name="portalID">Portal ID</param>
        /// <returns>News letter setting</returns>
        public NewsLetterSettingsInfo GetNewsLetterSetting(int usermoduleIDControl, int portalID)
        {
            try
            {
                NewLetterSubscriberProvider objProvider = new NewLetterSubscriberProvider();
                NewsLetterSettingsInfo objNewsletterSettingInfo = objProvider.GetNewsLetterSetting(usermoduleIDControl, portalID);
                return objNewsletterSettingInfo;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
