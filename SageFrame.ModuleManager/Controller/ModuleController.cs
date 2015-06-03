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
using SageFrame.ModuleManager.DataProvider;
#endregion

namespace SageFrame.ModuleManager.Controller
{
    /// <summary>
    /// Business logic for Module.
    /// </summary>
    public class ModuleController
    {
        /// <summary>
        /// Add user module.
        /// </summary>
        /// <param name="usermodule"></param>
        /// <returns>Object of UserModuleInfo class.</returns>
        public static string AddUserModule(UserModuleInfo usermodule)
        {
            try
            {
                ModuleDataProvider mod = new ModuleDataProvider();
                return(mod.AddUserModule(usermodule));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Obtain list of page modules.
        /// </summary>
        /// <param name="PageID">PageID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="IsHandheld">true if device is handheld.</param>
        /// <returns>List of UserModuleInfo class.</returns>
        public static List<UserModuleInfo> GetPageModules(int PageID, int PortalID,bool IsHandheld)
        {
            try
            {
                return (ModuleDataProvider.GetPageModules(PageID, PortalID,IsHandheld));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Delete user module.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="DeletedBy">User name.</param>
        public static void DeleteUserModule(int UserModuleID, int PortalID, string DeletedBy)
        {
            try
            {
                ModuleDataProvider.DeleteUserModule(UserModuleID, PortalID, DeletedBy);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Update page modules.
        /// </summary>
        /// <param name="lstPageModules">List of PageModuleInfo class.</param>
        public static void UpdatePageModule(List<PageModuleInfo> lstPageModules)
        {

            try
            {
                ModuleDataProvider.UpdatePageModule(lstPageModules);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Obtain users page module.
        /// </summary>
        /// <param name="IsAdmin">true if nodules for admin.</param>
        /// <returns>List of UserModuleInfo class.</returns>
        public static List<UserModuleInfo> GetPageUserModules(bool IsAdmin)
        {
            try
            {
                return (ModuleDataProvider.GetPageUserModules(IsAdmin));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Obtain user module details.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Object of UserModuleInfo class.</returns>
        public static UserModuleInfo GetUserModuleDetails(int UserModuleID, int PortalID)
        {
            try
            {
                return (ModuleDataProvider.GetUserModuleDetails(UserModuleID, PortalID));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Update user modules.
        /// </summary>
        /// <param name="module">Object of UserModuleInfo class.</param>
        public static void UpdateUserModule(UserModuleInfo module)
        {
            try
            {
                ModuleDataProvider.UpdateUserModule(module);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Public created newly page.
        /// </summary>
        /// <param name="pageId">pageId</param>
        /// <param name="isPublish">true if publish.</param>
        /// <returns>true if publish.</returns>
        public static bool PublishPage(int pageId,bool isPublish)
        {
            return ModuleDataProvider.PublishPage(pageId,isPublish);
        }
    }
}
