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


namespace SageFrame.Packages
{
    /// <summary>
    /// Manupulates data for PackagesProvider
    /// </summary>
    public class PackagesProvider
    {
   
        /// <summary>
        /// Connects to database and updates packages change.
        /// </summary>
        /// <param name="ModuleIDs">Module ID</param>
        /// <param name="IsActives">Set true if the respective modules are active.</param>
        /// <param name="UpdatedBy">Change updated user's name.</param>
        public static void UpdatePackagesChange(string ModuleIDs, string IsActives, string UpdatedBy)
        {
            string sp = "[dbo].[sp_PackagesUpdateChanges]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleIDs", ModuleIDs));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActives", IsActives));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", UpdatedBy));
               
                sagesql.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and returns package by portal ID
        /// </summary>
        /// <param name="PortalID">Portal ID</param>
        /// <param name="SearchText">Search Text</param>
        /// <returns>List of packages</returns>
        public static List<PackagesInfo> GetPackagesByPortalID(int PortalID, string SearchText)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@SearchText", SearchText));
               
                return SQLH.ExecuteAsList<PackagesInfo>("[dbo].[sp_PackagesGetByPortalID]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
