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


namespace SageFrame.PortalSetting
{
    /// <summary>
    /// Business logic for portals
    /// </summary>
    public class PortalController
    {

        /// <summary>
        /// Returns list of portals details
        /// </summary>
        /// <returns>list of portals details</returns>
        public static List<PortalInfo> GetPortalList()
        {
            try
            {
                return PortalProvider.GetPortalList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Returns portal details by portal ID.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="UserName"> User's name.</param>
        /// <returns>Returns portal </returns>
        public static PortalInfo GetPortalByPortalID(int PortalID, string UserName)
        {
            try
            {
                return PortalProvider.GetPortalByPortalID(PortalID, UserName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Deletes portal. 
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="UserName">User's name.</param>
        public static void DeletePortal(int PortalID, string UserName)
        {
            try
            {
                PortalProvider.DeletePortal(PortalID, UserName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates portal details.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="PortalName">Portal name.</param>
        /// <param name="IsParent">Set true if the portal is parent portal.</param>
        /// <param name="UserName">User's name.</param>
        /// <param name="PortalURL">Portal URL.</param>
        /// <param name="ParentID">Portal's parents ID.</param>
        public static void UpdatePortal(int PortalID, string PortalName, bool IsParent, string UserName, string PortalURL, int ParentID)
        {
            try
            {
                PortalProvider.UpdatePortal(PortalID, PortalName, IsParent, UserName, PortalURL, ParentID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns portal modules list by portalID and username.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="UserName">User's name.</param>
        /// <returns>lists of portal modules.</returns>
        public static List<PortalInfo> GetPortalModulesByPortalID(int PortalID, string UserName)
        {
            try
            {
                return PortalProvider.GetPortalModulesByPortalID(PortalID, UserName);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Updates portal modules.
        /// </summary>
        /// <param name="ModuleIDs">Module IDs join by ','</param>
        /// <param name="IsActives">Set actives join by ',' if the respective modules are active.</param>
        /// <param name="PortalID">Portal ID</param>
        /// <param name="UpdatedBy">Modules updated user's name.</param>
        public static void UpdatePortalModules(string ModuleIDs, string IsActives, int PortalID, string UpdatedBy)
        {
            try
            {
                PortalProvider.UpdatePortalModules(ModuleIDs, IsActives, PortalID, UpdatedBy);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Returns parent portal lists
        /// </summary>
        /// <returns>list of parent portals</returns>
        public List<PortalInfo> GetParentPortalList()
        {
            try
            {
                PortalProvider objProvider = new PortalProvider();
                return objProvider.GetParentPortalList();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
