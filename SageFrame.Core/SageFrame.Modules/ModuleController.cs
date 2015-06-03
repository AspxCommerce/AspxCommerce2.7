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


namespace SageFrame.Modules
{
    /// <summary>
    /// Business logic for modules
    /// </summary>
    public class ModuleController
    {
        /// <summary>
        /// Adds modules details.
        /// </summary>
        /// <param name="objList">Object of ModuleInfo.</param>
        /// <param name="isAdmin">Set true if the module is admin.</param>
        /// <param name="PackageID">Package ID.</param>
        /// <param name="IsActive">Set true if the module is active.</param>
        /// <param name="AddedOn">Module added date.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="AddedBy">Module added user's name.</param>
        /// <returns>Returns array of int containing 1: ModuleID and 2: ModuleDefID.</returns>
        public int[] AddModules(ModuleInfo objList, bool isAdmin, int PackageID, bool IsActive, DateTime AddedOn, int PortalID, string AddedBy)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                return objProvider.AddModules(objList, isAdmin, PackageID, IsActive, AddedOn, PortalID, AddedBy);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Adds portal modules.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="ModuleID">Module ID.</param>
        /// <param name="IsActive">Set true if the portal module is active.</param>
        /// <param name="AddedOn">Portal module added date.</param>
        /// <param name="AddedBy">Portal module added user's name.</param>
        /// <returns>Returns portal's module ID.</returns>
        public int AddPortalModules(int? PortalID, int? ModuleID, bool IsActive, DateTime AddedOn, string AddedBy)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                return objProvider.AddPortalModules(PortalID, ModuleID, IsActive, AddedOn, AddedBy);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Returns permission for any permission code and permission key.
        /// </summary>
        /// <param name="PermissionCode">Permission Code.</param>
        /// <param name="PermissionKey">Permission key.</param>
        /// <returns>Returns Permossion ID.</returns>
        public int GetPermissionByCodeAndKey(string PermissionCode, string PermissionKey)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                return objProvider.GetPermissionByCodeAndKey(PermissionCode, PermissionKey);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Adds module permission
        /// </summary>
        /// <param name="ModuleDefID">Module's definition ID.</param>
        /// <param name="PermissionID">Permission ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="PortalModuleID">Portal module ID.</param>
        /// <param name="AllowAccess">Set true if the user is allowed access to this module.</param>
        /// <param name="Username">User's name.</param>
        /// <param name="IsActive">Set true if the permission is active.</param>
        /// <param name="AddedOn">Permission added date</param>
        /// <param name="AddedBy">Permission added user's name.</param>
        /// <returns>Returns array of int containing 1:ModuleDefPermissionID and 2:PortalModulePermissionID.</returns>
        public int[] AddModulePermission(int? ModuleDefID, int? PermissionID, int? PortalID, int? PortalModuleID, bool AllowAccess, string Username, bool IsActive, DateTime AddedOn, string AddedBy)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                return objProvider.AddModulePermission(ModuleDefID, PermissionID, PortalID, PortalModuleID, AllowAccess, Username, IsActive, AddedOn, AddedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Saves module control.
        /// </summary>
        /// <param name="ModuleDefID">Module's definition ID.</param>
        /// <param name="ControlKey">Control's key.</param>
        /// <param name="ControlTitle">Control's title.</param>
        /// <param name="ControlSrc">Control's source.</param>
        /// <param name="IconFile">Control's icon file.</param>
        /// <param name="ControlType">Control's type.</param>
        /// <param name="DisplayOrder">Control's display order.</param>
        /// <param name="HelpUrl">Controls help URL.</param>
        /// <param name="SupportsPartialRendering">Set true if the controls need to have partial rendering.</param>
        /// <param name="IsActive">Set true if the control is active.</param>
        /// <param name="AddedOn">Control adde date.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="AddedBy">Control added user's name.</param>
        /// <returns>Returns registered ModuleControlID.</returns>
        public int AddModuleCoontrols(int? ModuleDefID, string ControlKey, string ControlTitle, string ControlSrc, string IconFile, int ControlType, int DisplayOrder, string HelpUrl, bool SupportsPartialRendering, bool IsActive, DateTime AddedOn, int PortalID, string AddedBy)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                return objProvider.AddModuleCoontrols(ModuleDefID, ControlKey, ControlTitle, ControlSrc, IconFile, ControlType, DisplayOrder, HelpUrl, SupportsPartialRendering, IsActive, AddedOn, PortalID, AddedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates extension
        /// </summary>
        /// <param name="objInfo">ModuleInfo object</param>
        public void UpdateExtension(ModuleInfo objInfo)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                objProvider.UpdateExtension(objInfo);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Deletes packages by module ID.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="ModuleID">Module ID</param>
        public void DeletePackagesByModuleID(int PortalID, int ModuleID)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                objProvider.DeletePackagesByModuleID(PortalID, ModuleID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns list of ModuleInfo class containing exisiting modules.
        /// </summary>
        /// <returns>list of ModuleInfo class containing exisibg modules.</returns>
        public List<ModuleInfo> GetAllExistingModule()
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                return objProvider.GetAllExistingModule();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Returns module control.
        /// </summary>
        /// <param name="ModuleControlID">Module control ID.</param>
        /// <returns>Module control.</returns>
        public ModuleEntities ModuleControlsGetByModuleControlID(int ModuleControlID)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                return objProvider.ModuleControlsGetByModuleControlID(ModuleControlID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates module control.
        /// </summary>
        /// <param name="ModuleControlID">Module control ID.</param>
        /// <param name="ControlKey">Control's key.</param>
        /// <param name="ControlTitle">Control's title.</param>
        /// <param name="ControlSrc">Control's source.</param>
        /// <param name="IconFile">Control's Icon.</param>
        /// <param name="ControlType">Control type.</param>
        /// <param name="DisplayOrder">Contorl's Display Order.</param>
        /// <param name="HelpUrl">Help URL.</param>
        /// <param name="SupportsPartialRendering">Set true if the module needs partial rendering.</param>
        /// <param name="IsActive">Set true if the module is active.</param>
        /// <param name="IsModified">Set true if the module is modified.</param>
        /// <param name="UpdatedOn">Control updated date.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="UpdatedBy">Control updated user's name</param>
        public void UpdateModuleCoontrols(int ModuleControlID, string ControlKey, string ControlTitle, string ControlSrc, string IconFile, int ControlType, int DisplayOrder, string HelpUrl, bool SupportsPartialRendering, bool IsActive, bool IsModified, DateTime UpdatedOn, int PortalID, string UpdatedBy)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                objProvider.UpdateModuleCoontrols(ModuleControlID, ControlKey, ControlTitle, ControlSrc, IconFile, ControlType, DisplayOrder, HelpUrl, SupportsPartialRendering, IsActive, IsModified, UpdatedOn, PortalID, UpdatedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Checks for unique module Control
        /// </summary>
        /// <param name="ModuleControlID">Module control ID </param>
        /// <param name="ModuleDefID">Module Definition</param>
        /// <param name="ControlType">Control type</param>
        /// <param name="PortalID">Portal ID</param>
        /// <param name="isEdit">Set true is the cotrol edit is true</param>
        /// <returns>Returns count of module control type</returns>
        public int CheckUnquieModuleControlsControlType(int ModuleControlID, int ModuleDefID, int ControlType, int PortalID, bool isEdit)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                return objProvider.CheckUnquieModuleControlsControlType(ModuleControlID, ModuleDefID, ControlType, PortalID, isEdit);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Deletes control by module control ID
        /// </summary>
        /// <param name="ModuleControlID">Module control ID</param>
        /// <param name="DeletedBy">Module deleted user's name</param>
        public void ModuleControlsDeleteByModuleControlID(int ModuleControlID, string DeletedBy)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                objProvider.ModuleControlsDeleteByModuleControlID(ModuleControlID, DeletedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates module definitions.
        /// </summary>
        /// <param name="ModuleDefID">Module definition ID.</param>
        /// <param name="FriendlyName">Module's friendly name</param>
        /// <param name="DefaultCacheTime">Module's default cache time</param>
        /// <param name="IsActive">Set true if module is active</param>
        /// <param name="IsModified">Set true if the module is modified</param>
        /// <param name="UpdatedOn">Module updated date</param>
        /// <param name="PortalID">Portal ID</param>
        /// <param name="UpdatedBy">Module Updated user's name</param>
        public void UpdateModuleDefinitions(int ModuleDefID, string FriendlyName, int DefaultCacheTime, bool IsActive, bool IsModified, DateTime UpdatedOn, int PortalID, string UpdatedBy)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                objProvider.UpdateModuleDefinitions(ModuleDefID, FriendlyName, DefaultCacheTime, IsActive, IsModified, UpdatedOn, PortalID, UpdatedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns module information. 
        /// </summary>
        /// <param name="ModuleID">Module ID.</param>
        /// <returns>Returns module details</returns>
        public ModuleInfo GetModuleInformationByModuleID(int ModuleID)
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                return objProvider.GetModuleInformationByModuleID(ModuleID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<KeyValue> GetBasicTableName()
        {
            try
            {
                ModuleProvider objProvider = new ModuleProvider();
                return objProvider.GetBasicTableName();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
