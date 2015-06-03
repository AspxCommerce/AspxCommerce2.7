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

namespace SageFrame.GoogleAdsense
{
    /// <summary>
    /// Business logic for google adsense controller.
    /// </summary>
    public class GoogleAdsenseController
    {
        /// <summary>
        /// Counts  adsense settings for given.
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="PortalID">portalID</param>
        /// <returns>Count of adsense settings</returns>
        public int CountAdsenseSettings(int UserModuleID, int PortalID)
        {
            try
            {
                GoogleAdsenseProvider objProvider = new GoogleAdsenseProvider();
                return objProvider.CountAdsenseSettings(UserModuleID, PortalID);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        /// <summary>
        /// Returns list of google adsense settings by usermoduleID.
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="PortalID">portalID</param>
        /// <returns>Returns list of google adsense settings</returns>
        public List<GoogleAdsenseInfo> GetAdSenseSettingsByUserModuleID(int UserModuleID, int PortalID)
        {
            try
            {
                GoogleAdsenseProvider objProvider = new GoogleAdsenseProvider();
                return objProvider.GetAdSenseSettingsByUserModuleID(UserModuleID, PortalID);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Updates the google adsense.
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="SettingName">Setting name</param>
        /// <param name="SettingValue">Setting value</param>
        /// <param name="IsActive">Set true if adsense is active</param>
        /// <param name="PortalID">portalID</param>
        /// <param name="UpdatedBy">User's name updatating the data</param>
        /// <param name="UpdateFlag">Update Flag</param>
        public void AddUpdateAdSense(int UserModuleID, string SettingName, string SettingValue, bool IsActive, int PortalID, string UpdatedBy, bool UpdateFlag)
        {
            try
            {
                GoogleAdsenseProvider objProvider = new GoogleAdsenseProvider();
                objProvider.AddUpdateAdSense(UserModuleID, SettingName, SettingValue, IsActive, PortalID, UpdatedBy, UpdateFlag);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Deletes google adsense.
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="PortalID">portalID</param>
        public void DeleteAdSense(int UserModuleID, int PortalID)
        {
            try
            {
                GoogleAdsenseProvider objProvider = new GoogleAdsenseProvider();
                objProvider.DeleteAdSense(UserModuleID, PortalID);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Counts google adsense.
        /// </summary>
        /// <param name="UserModuleID">userModuleID</param>
        /// <param name="PortalID">portalID</param>
        /// <returns>Returns google adsense count</returns>
        public int CountAdSense(int UserModuleID, int PortalID)
        {
            try
            {
                GoogleAdsenseProvider objProvider = new GoogleAdsenseProvider();
                return objProvider.CountAdSense(UserModuleID, PortalID);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
