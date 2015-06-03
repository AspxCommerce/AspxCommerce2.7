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

namespace SageFrame.ModuleControls
{
    /// <summary>
    /// Manupulates data for ModuleControlDataProvider.
    /// </summary>
    public class ModuleControlDataProvider
    {
        /// <summary>
        /// Connect to database and obtain module ID based on user module ID.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <returns>Module ID.</returns>
        public static int GetModuleID(int UserModuleID)
        {
            SQLHandler Objsql = new SQLHandler();
           try
           {
               List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
               ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
               SQLHandler sqlh = new SQLHandler();
               int ModuleID = 0;
               ModuleID = sqlh.ExecuteAsScalar<int>("[dbo].[usp_ModuleControlGetModuleIdFromUserModuleId]", ParaMeterCollection);
               return ModuleID;
           }
           catch(Exception ex)
           {
               throw ex;
           
           }
        }
       /// <summary>
        /// Connect to database and obtain module name based on UserModuleID.
       /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
       /// <returns>Module name.</returns>
        public static string GetModuleName(int UserModuleID)
        {
            SQLHandler Objsql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                SQLHandler sqlh = new SQLHandler();
                string ModuleName = "";
                ModuleName = sqlh.ExecuteAsScalar<string>("[dbo].[usp_ModuleControlGetModuleNameFromUserModuleId]", ParaMeterCollection);
                return ModuleName;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        /// <summary>
        /// Connect to database and obtain application control type.
        /// </summary>
        /// <param name="ModuleDefID">ModuleDefID</param>
        /// <returns>List of ModuleControlInfo class.</returns>
        public static List<ModuleControlInfo> GetControlType(int ModuleDefID)
        {
            List<ModuleControlInfo> lstControl = new List<ModuleControlInfo>();
            string StoredProcedureName = "[dbo].[usp_ModuleControlGetControlTypeFromModuleID]";
            SQLHandler sqlh = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ModuleDefID", ModuleDefID));
            
            try
            {
                lstControl = sqlh.ExecuteAsList<ModuleControlInfo>(StoredProcedureName, ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstControl;
        }

    }
}
