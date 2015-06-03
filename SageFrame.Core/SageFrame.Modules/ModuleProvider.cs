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
using SageFrame.Common;
using System.Data.SqlClient;

#endregion


namespace SageFrame.Modules
{
    /// <summary>
    /// Manupulates data for user modules.
    /// </summary>
    public class ModuleProvider
    {

        /// <summary>
        /// Connects to database and adds modules details.
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
            string sp = "[dbo].[sp_ModulesAdd]";
            //SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleName", objList.ModuleName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Name", objList.Name));
                ParamCollInput.Add(new KeyValuePair<string, object>("@FriendlyName", objList.FriendlyName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Description", objList.Description));
                ParamCollInput.Add(new KeyValuePair<string, object>("@FolderName", objList.FolderName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Version", objList.Version));

                ParamCollInput.Add(new KeyValuePair<string, object>("@isPremium", objList.isPremium));
                ParamCollInput.Add(new KeyValuePair<string, object>("@isAdmin", isAdmin));


                ParamCollInput.Add(new KeyValuePair<string, object>("@Owner", objList.Owner));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Organization", objList.Organization));
                ParamCollInput.Add(new KeyValuePair<string, object>("@URL", objList.URL));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Email", objList.Email));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ReleaseNotes", objList.ReleaseNotes));
                ParamCollInput.Add(new KeyValuePair<string, object>("@License", objList.License));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PackageType", objList.PackageType));
                ParamCollInput.Add(new KeyValuePair<string, object>("@supportedFeatures", objList.supportedFeatures));
                ParamCollInput.Add(new KeyValuePair<string, object>("@BusinessControllerClass", objList.BusinessControllerClass));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CompatibleVersions", objList.CompatibleVersions));
                ParamCollInput.Add(new KeyValuePair<string, object>("@dependencies", objList.dependencies));
                ParamCollInput.Add(new KeyValuePair<string, object>("@permissions", objList.permissions));

                ParamCollInput.Add(new KeyValuePair<string, object>("@PackageID", PackageID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));

                List<KeyValuePair<string, object>> ParamCollOutput = new List<KeyValuePair<string, object>>();
                ParamCollOutput.Add(new KeyValuePair<string, object>("@ModuleID", objList.ModuleID));
                ParamCollOutput.Add(new KeyValuePair<string, object>("@ModuleDefID", objList.ModuleDefID));

                SageFrameSQLHelper sagesql = new SageFrameSQLHelper();

                List<KeyValuePair<int, string>> OutputValColl = new List<KeyValuePair<int, string>>();
                OutputValColl = sagesql.ExecuteNonQueryWithMultipleOutput(sp, ParamCollInput, ParamCollOutput);
                int[] arrOutPutValue = new int[2];
                arrOutPutValue[0] = int.Parse(OutputValColl[0].Value);
                arrOutPutValue[1] = int.Parse(OutputValColl[1].Value);

                return arrOutPutValue;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Connects to database and adds portal modules.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="ModuleID">Module ID.</param>
        /// <param name="IsActive">Set true if the portal module is active.</param>
        /// <param name="AddedOn">Portal module added date.</param>
        /// <param name="AddedBy">Portal module added user's name.</param>
        /// <returns>Returns portal's module ID.</returns>
        public int AddPortalModules(int? PortalID, int? ModuleID, bool IsActive, DateTime AddedOn, string AddedBy)
        {

            string sp = "[dbo].[sp_PortalModulesAdd]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID", ModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));

                int pmID = sagesql.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@PortalModuleID");
                return pmID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Connects to database and returns permission for any permission code and permission key.
        /// </summary>
        /// <param name="PermissionCode">Permission Code.</param>
        /// <param name="PermissionKey">Permission key.</param>
        /// <returns>Returns Permission ID.</returns>
        public int GetPermissionByCodeAndKey(string PermissionCode, string PermissionKey)
        {

            string sp = "[dbo].[sp_GetPermissionByCodeAndKey]";
            SQLHandler SQLH = new SQLHandler();
            SqlDataReader reader = null;
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PermissionCode", PermissionCode));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PermissionKey", PermissionKey));

                reader = SQLH.ExecuteAsDataReader(sp, ParamCollInput);
                int PermissionID = 0;

                while (reader.Read())
                {
                    PermissionID = int.Parse(reader["PermissionID"].ToString());

                }
                return PermissionID;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Connects to database and adds module permission
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
            string sp = "[dbo].[sp_ModulesPermissionAdd]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleDefID", ModuleDefID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PermissionID", PermissionID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalModuleID", PortalModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AllowAccess", AllowAccess));

                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));

                //int ListID = sagesql.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@ModuleDefPermissionID");
                //return ListID;

                ModuleInfo objInfo = new ModuleInfo();
                List<KeyValuePair<string, object>> ParamCollOutput = new List<KeyValuePair<string, object>>();
                ParamCollOutput.Add(new KeyValuePair<string, object>("@ModuleDefPermissionID", objInfo.ModuleDefPermissionID));
                ParamCollOutput.Add(new KeyValuePair<string, object>("@PortalModulePermissionID", objInfo.PortalModulePermissionID));

                SageFrameSQLHelper objHelper = new SageFrameSQLHelper();

                List<KeyValuePair<int, string>> OutputValColl = new List<KeyValuePair<int, string>>();
                OutputValColl = objHelper.ExecuteNonQueryWithMultipleOutput(sp, ParamCollInput, ParamCollOutput);
                int[] arrOutPutValue = new int[2];
                arrOutPutValue[0] = int.Parse(OutputValColl[0].Value);
                arrOutPutValue[1] = int.Parse(OutputValColl[1].Value);

                return arrOutPutValue;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Connects to database and saves module control.
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

            string sp = "[dbo].[sp_ModuleControlsAdd]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleDefID", ModuleDefID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ControlKey", ControlKey));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ControlTitle", ControlTitle));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ControlSrc", ControlSrc));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IconFile", IconFile));

                ParamCollInput.Add(new KeyValuePair<string, object>("@ControlType", ControlType));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayOrder", DisplayOrder));
                ParamCollInput.Add(new KeyValuePair<string, object>("@HelpUrl", HelpUrl));
                ParamCollInput.Add(new KeyValuePair<string, object>("@SupportsPartialRendering", SupportsPartialRendering));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));

                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));


                int MCID = sagesql.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@ModuleControlID");
                return MCID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and updates module control.
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
            string sp = "[dbo].[sp_ModuleControlsUpdate]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleControlID", ModuleControlID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ControlKey", ControlKey));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ControlTitle", ControlTitle));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ControlSrc", ControlSrc));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IconFile", IconFile));

                ParamCollInput.Add(new KeyValuePair<string, object>("@ControlType", ControlType));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayOrder", DisplayOrder));
                ParamCollInput.Add(new KeyValuePair<string, object>("@HelpUrl", HelpUrl));
                ParamCollInput.Add(new KeyValuePair<string, object>("@SupportsPartialRendering", SupportsPartialRendering));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsModified", IsModified));

                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedOn", UpdatedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", UpdatedBy));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Connects to database and updates extension
        /// </summary>
        /// <param name="objInfo">ModuleInfo object</param>
        public void UpdateExtension(ModuleInfo objInfo)
        {
            string sp = "[dbo].[sp_ExtensionUpdate]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID", objInfo.ModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@FolderName", objInfo.FolderName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@BusinessControllerClass", objInfo.BusinessControllerClass));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Dependencies", objInfo.dependencies));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Permissions", objInfo.permissions));

                ParamCollInput.Add(new KeyValuePair<string, object>("@IsPortable", objInfo.IsPortable));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsSearchable", objInfo.IsSearchable));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsUpgradable", objInfo.IsUpgradable));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsPremium", objInfo.isPremium));


                ParamCollInput.Add(new KeyValuePair<string, object>("@PackageName", objInfo.PackageName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PackageDescription", objInfo.PackageDescription));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Version", objInfo.Version));
                ParamCollInput.Add(new KeyValuePair<string, object>("@License", objInfo.License));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ReleaseNotes", objInfo.ReleaseNotes));

                ParamCollInput.Add(new KeyValuePair<string, object>("@Owner", objInfo.Owner));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Organization", objInfo.Organization));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Url", objInfo.URL));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Email", objInfo.Email));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", objInfo.PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", objInfo.Username));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Connects to database and deletes packages by module ID.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="ModuleID">Module ID</param>
        public void DeletePackagesByModuleID(int PortalID, int ModuleID)
        {
            string sp = "[dbo].[sp_ModulesRollBack]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID", ModuleID));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns list of ModuleInfo class containing exisiting modules.
        /// </summary>
        /// <returns>list of ModuleInfo class containing exisibg modules.</returns>
        public List<ModuleInfo> GetAllExistingModule()
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                return SQLH.ExecuteAsList<ModuleInfo>("[dbo].[usp_ModuleGetAllExisting]");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// Connects to database and returns module control.
        /// </summary>
        /// <param name="ModuleControlID">Module control ID.</param>
        /// <returns>Module control.</returns>
        public ModuleEntities ModuleControlsGetByModuleControlID(int ModuleControlID)
        {
            try
            {
                string sp = "[dbo].[sp_ModuleControlsGetByModuleControlID]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleControlID", ModuleControlID));
                return SQLH.ExecuteAsObject<ModuleEntities>(sp, ParamCollInput);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Connects to database and  checks for unique module Control
        /// </summary>
        /// <param name="ModuleControlID">Module control ID </param>
        /// <param name="ModuleDefID">Module Definition</param>
        /// <param name="ControlType">Control type</param>
        /// <param name="PortalID">Portal ID</param>
        /// <param name="isEdit">Set true is the cotrol edit is true</param>
        /// <returns>Returns count of module control type</returns>
        public int CheckUnquieModuleControlsControlType(int ModuleControlID, int ModuleDefID, int ControlType, int PortalID, bool isEdit)
        {
            string sp = "[dbo].[sp_CheckUnquieModuleControlsControlType]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleControlID", ModuleControlID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleDefID", ModuleDefID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ControlType", ControlType));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@isEdit", isEdit));
                int Count = sagesql.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@Count");
                return Count;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Connects to database and deletes control by module control ID
        /// </summary>
        /// <param name="ModuleControlID">Module control ID</param>
        /// <param name="DeletedBy">Module deleted user's name</param>
        public void ModuleControlsDeleteByModuleControlID(int ModuleControlID, string DeletedBy)
        {
            try
            {
                string sp = "[dbo].[sp_ModuleControlsDeleteByModuleControlID]";
                SQLHandler sagesql = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleControlID", ModuleControlID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DeletedBy", DeletedBy));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Connects to database and updates module definitions.
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
            string sp = "sp_ModuleDefinitionsUpdate";
            SQLHandler SQLH = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleDefID", ModuleDefID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@FriendlyName", FriendlyName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DefaultCacheTime", DefaultCacheTime));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsModified", IsModified));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedOn", UpdatedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", UpdatedBy));
                SQLH.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Connects to database and returns module information. 
        /// </summary>
        /// <param name="ModuleID">Module ID.</param>
        /// <returns>Returns module details</returns>
        public ModuleInfo GetModuleInformationByModuleID(int ModuleID)
        {
            try
            {
                string sp = "[dbo].[sp_ModuleGetByModuleID]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID", ModuleID));

                return SQLH.ExecuteAsObject<ModuleInfo>(sp, ParamCollInput);

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
                string sp = "[dbo].[usp_GetAllBasicTableName]";
                SQLHandler SQLH = new SQLHandler();
                return SQLH.ExecuteAsList<KeyValue>(sp);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
